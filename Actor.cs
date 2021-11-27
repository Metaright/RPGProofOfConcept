using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheFinalBattle
{
    public partial class Actor
    {
        public virtual string Name { get; init; } //Name property, only settable at construction.

        public virtual string Type { get; } //Simplified type name (e.g. "Skeleton", not "Actor.Skeleton".

        //public List<Attack> Actions { get; set; } //Array of methods (with 1 parameter and no returns).
        //This delegate requies methods to take one parameter, which must be a list of Actor objects.
        public List<Attack> Actions { get; set; }

        public bool IsHeroParty { get; init; } //True = Player's party; False = Enemy's party.
        public virtual bool IsEventHandler { get; init; } = false;
        public virtual int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int WaveID { get; set; }
        public virtual int DifficultyID { get; init; }
        public bool PlayerControlled { get; init; }
        public bool IsTurn { get; set; }
        public bool SwiftnessPotion { get; set; } = false;
        public int MaxGearCount { get; set; } = 2; //Maximum number of gear items actors can equip at once.
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Gear> Gear { get; set; } = new List<Gear>();
        public List<Gear> EquippedGear { get; set; } = new List<Gear>();
        public static List<Item> DroppedItems { get; set; } = new List<Item>();
        public static List<Gear> DroppedGear { get; set; } = new List<Gear>();
        //public List<Func<int, int>> DefensiveModifiers { get; set; } = new List<Func<int, int>>();
        //public List<Func<int, int>> OffensiveModifiers { get; set; } = new List<Func<int, int>>();
        public List<Modifier> DefensiveModifiers { get; set; } = new List<Modifier>();
        public List<Modifier> OffensiveModifiers { get; set; } = new List<Modifier>();

        public Actor() { } //Parameterless constructor is empty to allow for derived classes to make their onw.
        public Actor(bool IsInHeroParty)
        {
            Console.Write($"Would you like to provide a name for this {Type}? [y]/[n] ");
            bool validName = false;
            while(!validName)
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case 'y':
                        Console.Write("\nPlease enter a name. ");
                        Name = Console.ReadLine();
                        validName = true;
                        break;
                    case 'n':
                        Console.WriteLine("\nUsing defualt name.");
                        Name = $"{Type}";
                        validName = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid entry. Please enter [y] or [n].");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }

            switch(IsInHeroParty)
            {
                case true:
                    IsHeroParty = true;
                    break;
                case false:
                    IsHeroParty = false;
                    break;
            }

            PopulateActions();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nCharacter initialized!");
            Console.ForegroundColor = ConsoleColor.White;
            PressAnyKeyPrompt();
            Console.Clear();
        }

        virtual public void TurnSequence(List<Actor> actors)
        {
            Console.Clear();
            IsTurn = true;

            if (CurrentHP > 0)
            {
                DisplayGameStatus(actors);
                Display();
                bool validChoice = false;
                bool shouldUseRest = CurrentHP < MaxHP;
                bool shouldUseItem = Items.Count > 0;

                for (int index = 0; index < EquippedGear.Count; index++)
                {
                    if (!EquippedGear[index].Equipped && index == 0)
                    { EquippedGear[index].EquipAtBeginning(actors, this); }
                }

                if (PlayerControlled && IsHeroParty)
                {
                    foreach (Item item in DroppedItems) { Items.Add(item); }
                    foreach (Gear gear in DroppedGear) { Gear.Add(gear); }

                    DroppedItems.Clear();
                    DroppedGear.Clear();
                }

                while (!validChoice)
                {
                    DisplayName();
                    Console.WriteLine($" can do the following:");
                    //foreach (Action<List<Actor>> action in Actions) Console.WriteLine($"{Array.FindIndex(Actions, 0, a => action.Equals(a))}. {action.Method.Name}");
                    Attack[] converted = Actions.ToArray();
                    for (int index = 0; index < converted.Length; index++) 
                    { 
                        Console.Write($"{index}. ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{converted[index].Method.Name}: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        converted[index](actors, true, out string description);
                        Console.WriteLine(description);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //for (int index = 0; index < Actions.Count; index++) Console.WriteLine($"{index}. { Actions[index]}");

                    if (PlayerControlled)
                    {
                        Console.Write("\nWhat would you like to do? ");
                        if (int.TryParse(Console.ReadLine(), out int choice))
                        {
                            try
                            {
                                Actions[choice](actors, false, out string description);
                                validChoice = true;
                                break;
                            }
                            catch (Exception e) when (e is IndexOutOfRangeException || e is ArgumentOutOfRangeException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nInvalid entry.\n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid entry.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        validChoice = true;
                        Random random = new Random();
                        Thread.Sleep(1500);
                        int choice = random.Next(Actions.Count);
                        while ((!shouldUseRest && choice == 0) || (!shouldUseItem && choice == 1)) //Rest is index 0 in all actors' action lists.
                        {
                            choice = random.Next(Actions.Count);
                        }
                        //Choose random action from the list avaiable. See PopulateActions().
                        //If health is full, keep choosing at random until Rest is not chosen.
                        //If inventory is empty, also keep choosing at random until using an item is not chosen.
                        int shouldUseHealthPotion = random.Next(3);

                        if (shouldUseHealthPotion == 0 && CurrentHP < (MaxHP * 0.5) && Items.OfType<HealthPotion>().Any())
                        {
                            Console.WriteLine("");
                            DisplayName();
                            Console.WriteLine(" used a Health Potion!");
                            for (int index = 0; index < Items.Count; index++)
                            {
                                if (Items[index].GetType() == typeof(HealthPotion))
                                //{ Items[index].ItemEffects(this, actors, Items[index].Usage); break; }
                                {
                                    if (CurrentHP + 15 > MaxHP) CurrentHP = MaxHP;
                                    else CurrentHP = CurrentHP + 15;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("");
                            DisplayName();
                            Console.WriteLine($" used {converted[choice].Method.Name}!");
                            Actions[choice](actors, false, out string description);
                        }
                    }
                }
            }
            else DeathSequence(actors);
            IsTurn = false;
            //PressAnyKeyPrompt();
            if(SwiftnessPotion) { SwiftnessPotion = false; TurnSequence(actors); }
        }

        public virtual Actor TargetChoiceEnemy(List<Actor> actors) //Used for enemies.
        {
            Random random = new Random();
            IEnumerable<Actor> targetObject;
            if (!IsHeroParty)
            {
                targetObject = from o in actors //Sort through all present actors.
                               where o.IsHeroParty == true //Filter by who is in the player's party.
                               where o.Name != "EventHandler"
                               //where o.WaveID == WaveID
                               where o.CurrentHP > 0
                               select o;
            }
            else
            {
                targetObject = from o in actors //Sort through all present actors.
                               where o.IsHeroParty == false //Filter by who is in the player's party.
                               where o.Name != "EventHandler"
                               where o.WaveID == WaveID
                               where o.CurrentHP > 0
                               select o;
            }
            List<Actor> targetObjectList = targetObject.ToList(); //Convert to indexable list.
            int avaiableTargets = -1;
            int target;
            foreach (Actor actor in targetObjectList) avaiableTargets++;
            //Increase availableTargets by 1 for each actor in the player's party. Start at -1 so
            //the first member of the player's party gets index position 0.
            Actor selection = null;
            if (avaiableTargets >= 0)
            {
                target = random.Next(avaiableTargets + 1); //Choose a random target from the player's party.
                selection = targetObjectList[target];
            }
            Thread.Sleep(1500); //Pause for dramatic effect.
            return selection;
        }

        public virtual List<Actor> TargetChoicePlayer(List<Actor> actors) //Used for player party.
        {
            IEnumerable<Actor> targetObject;

            if (IsHeroParty)
            {
                targetObject = from o in actors //Sort through all present actors.
                               where o.IsHeroParty == false //Filter by who is in the enemies' party.
                               where o.Name != "EventHandler"
                               where o.WaveID == WaveID //Make sure the actors are on the correct wave.
                               where o.CurrentHP > 0
                               select o;
            }
            else
            {
                targetObject = from o in actors //Sort through all present actors.
                               where o.IsHeroParty == true //Filter by who is in the enemies' party.
                               where o.Name != "EventHandler"
                               where o.WaveID == WaveID //Make sure the actors are on the correct wave.
                               where o.CurrentHP > 0
                               select o;
            }

            List<Actor> targetObjectList = targetObject.ToList(); //Convert to indexable list.
            return targetObjectList;
        }

        public List<Actor> PlayerControlledTargeting(List<Actor> actors)
        {
                Console.WriteLine("\nPlease choose a target. Enter [b] to go back.");
                List<Actor> targets = TargetChoicePlayer(actors);
                for (int index = 0; index < targets.Count; index++)
                {
                    Console.Write($"{index}. ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{targets[index].Name}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            return targets;
        }

        public List<Actor> TargetingForPotions(List<Actor> actors)
        {
            Console.WriteLine("\nPlease choose a target. Enter [b] to go back.");
            IEnumerable<Actor> targetObjects;

            if (IsHeroParty)
            {
                targetObjects = from o in actors //Sort through all present actors.
                                where o.IsHeroParty == true //Filter by who is in the heroes' party.
                                where o.Name != "EventHandler"
                                where o.WaveID == WaveID //Make sure the actors are on the correct wave.
                                where o.CurrentHP > 0
                                select o;
            }
            else
            {
                targetObjects = from o in actors //Sort through all present actors.
                                where o.IsHeroParty == false //Filter by who is in the heroes' party.
                                where o.Name != "EventHandler"
                                where o.WaveID == WaveID //Make sure the actors are on the correct wave.
                                where o.CurrentHP > 0
                                select o;
            }

            List<Actor> targets = targetObjects.ToList();

            for (int index = 0; index < targets.Count; index++)
            {
                Console.Write($"{index}. ");
                targets[index].DisplayName();
                Console.ForegroundColor = ConsoleColor.White;
            }
            return targets;
        }

        public void ComputerControlledTargeting(List<Actor> actors)
        {
            if (CurrentHP > 0)
            {
                Random random = new Random();
                Display();
                DisplayName();
                Console.WriteLine(" can do the following:");

                //foreach (Action<List<Actor>> action in Actions) Console.WriteLine($"{ action. }. {action.ToString()}");
                for (int index = 0; index < Actions.Count; index++) Console.WriteLine($"{index}. {Actions[index].ToString()}");

                Thread.Sleep(1500);
                int choice = random.Next(Actions.Count);
                //Choose random action from the list avaiable. See PopulateActions().

                Console.WriteLine("");
                DisplayName();
                Console.WriteLine($" used {Actions[choice].ToString()}!");
                Actions[choice](actors, false, out string description);
            }
            else DeathSequence(actors);
        }

        public virtual void DeathSequence(List<Actor> actors)
        {
            Console.Clear();
            Random random = new Random();

            switch(random.Next(4))
            {
                case 0:
                    DisplayName();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" has died!");
                    break;
                case 1:
                    DisplayName();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" has perished!");
                    break;
                case 2:
                    DisplayName();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" is no longer with us.");
                    break;
                case 3:
                    DisplayName();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" is en route to Hell.");
                    break;
            }
            
            for(int index = 0; index < EquippedGear.Count; index++)
            {
                DisplayName();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($" dropped {EquippedGear[index].Name}!");
                DroppedGear.Add(EquippedGear[index]);
            }

            for (int index = 0; index < Gear.Count; index++)
            {
                DisplayName();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($" dropped {Gear[index].Name}!");
                DroppedGear.Add(Gear[index]);
            }

            for (int index = 0; index < Items.Count; index++)
            {
                DisplayName();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($" dropped {Items[index].Name}!");
                DroppedItems.Add(Items[index]);
            }

            Console.ForegroundColor = ConsoleColor.White;

            actors.Remove(this); //Removes the newly dead character from the shared list of active characters.
        }

        virtual public void Display()
            //Defaults to red text for Actor.Name so only the player's team has to override it.
        {
            Console.Write($"\nIt is ");
            DisplayName();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"'s turn. || HP: {CurrentHP} / {MaxHP}\n");

            if (DefensiveModifiers.Count > 0 || OffensiveModifiers.Count > 0)
            {
                DisplayName();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" has the following modifiers:");

                if (DefensiveModifiers.Count > 0)
                {
                    for (int index = 0; index < DefensiveModifiers.Count; index++)
                    {
                        Modifier[] defensiveModifiersConverted = new Modifier[DefensiveModifiers.Count];
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(defensiveModifiersConverted[index]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        int blah = DefensiveModifiers[index](0, out string description);
                        Console.WriteLine(description);
                    }
                }

                if (OffensiveModifiers.Count > 0)
                {
                    for (int index = 0; index < OffensiveModifiers.Count; index++)
                    {
                        Modifier[] offensiveModifiersConverted = new Modifier[OffensiveModifiers.Count];
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(offensiveModifiersConverted[index]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        int blah = OffensiveModifiers[index](0, out string description);
                        Console.WriteLine(description);
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        virtual public void DisplayName()
            //Defaults to red text for Actor.Name so only the player's team has to override it.
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Name}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayGameStatus(List<Actor> actors)
        {
            IEnumerable<Actor> filteredHeroes = from o in actors
                                          where o.IsEventHandler == false
                                          where o.IsHeroParty == true
                                          where o.WaveID == WaveID
                                          select o;

            List<Actor> filteredHeroesList = filteredHeroes.ToList();

            IEnumerable<Actor> filteredEnemies = from o in actors
                                                where o.IsEventHandler == false
                                                where o.IsHeroParty == false
                                                where o.WaveID == WaveID
                                                select o;

            List<Actor> filteredEnemiesList = filteredEnemies.ToList();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n\n");
            Console.WriteLine($"===============BATTLE STATUS==========Wave {WaveID}");

            
            for(int index = 0; index < filteredHeroesList.Count; index++)
            {
                filteredHeroesList[index].DisplayName();

                if (filteredHeroesList[index].IsTurn)
                { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("<-"); }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("     ( "); // Five spaces preceding (

                if (filteredHeroesList[index].CurrentHP > 0)
                { Console.Write($"{filteredHeroesList[index].CurrentHP}"); }
                else
                { Console.Write("0"); }

                Console.Write(" / ");
                Console.Write($"{filteredHeroesList[index].MaxHP}");
                Console.WriteLine(" )");
            }
            Console.WriteLine("------------------ versus -----------------");

            for(int index = 0; index < filteredEnemiesList.Count; index++)
            {
                if(filteredEnemiesList[index].IsTurn)
                { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("   ->"); }
                else { Console.Write("     "); } // Five spaces

                filteredEnemiesList[index].DisplayName();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("     ( "); // Five spaces preceding (

                if (filteredEnemiesList[index].CurrentHP > 0)
                { Console.Write($"{filteredEnemiesList[index].CurrentHP}"); }
                else
                { Console.Write("0"); }

                Console.Write(" / ");
                Console.Write($"{filteredEnemiesList[index].MaxHP}");
                Console.WriteLine(" )");
            }
            Console.WriteLine("===========================================");
            Console.Write("\n\n");
        }

        virtual public void PopulateActions()
        {
            Actions = new List<Attack> { Rest };
        }

        public EventHandler SearchForEventHandler(List<Actor> list)
        {
            IEnumerable<Actor> filtered = from o in list
                                          where o.IsEventHandler == true
                                          select o;
            List < Actor > filteredActors = filtered.ToList();
            return (EventHandler)filteredActors[0];
        }
        public void UseAnItem(List<Actor> actors, bool justDescription, out string description)
        {
            description = "Use an item.";

            if (!justDescription)
            {
                if (Items.Count != 0)
                {
                    Console.WriteLine("");
                    DisplayName();
                    Console.WriteLine("'s items:");
                    for (int index = 0; index < Items.Count; index++)
                    { Console.WriteLine($"{index + 1}. {Items[index].Name}"); }
                    SelectItem(actors);
                }
                else
                {
                    Console.WriteLine("");
                    DisplayName();
                    Console.WriteLine(" has no items!");
                    PressAnyKeyPrompt();
                    TurnSequence(actors);
                }
            }
        }

        public void SelectItem(List<Actor> actors)
        {
            Console.Write("\nUse which item? Enter [b] to go back. ");
            bool validChoice = false;

            if (PlayerControlled)
            {
                while (!validChoice)
                {
                    string selection = Console.ReadLine();

                    if (selection == "B" || selection == "b") { validChoice = true; TurnSequence(actors); }
                    else if (int.TryParse(selection, out int choice) && choice > 0 && choice <= Items.Count)
                    {
                        Item item = Items[choice - 1];
                        item.ItemEffects(this, actors, item.Usage);
                        Items.Remove(item);
                        validChoice = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            else
            {
                Random random = new Random();
                int itemChoice = random.Next(Items.Count);
                Items[itemChoice].ItemEffects(this, actors, Items[itemChoice].Usage);
            }
        }

        public void UseGear(List<Actor> actors, bool justDescription, out string description)
        {
            description = "Equip or unequip gear you've collected.";

            if (!justDescription)
            {
                if (EquippedGear.Count != 0)
                {
                    Console.WriteLine("");
                    DisplayName();
                    Console.WriteLine("'s equipped gear: ");
                    Gear[] converted = EquippedGear.ToArray();
                    for (int index = 0; index < converted.Length; index++) { Console.WriteLine($"{index}. {converted[index].Name}"); }
                    Console.WriteLine("");
                    SelectGear(actors);
                }
                else
                {
                    Console.WriteLine("");
                    DisplayName();
                    Console.WriteLine(" has not equipped any gear.\n");
                }

                if (Gear.Count != 0)
                {
                    Console.WriteLine("");
                    DisplayName();
                    Console.WriteLine("'s available gear:");
                    for (int index = 0; index < Gear.Count; index++)
                    { Console.WriteLine($"{index + 1}. {Gear[index].Name}"); }
                    SelectGear(actors);
                }
                else if (Gear.Count == 0 && EquippedGear.Count == 0)
                {
                    Console.WriteLine("");
                    DisplayName();
                    Console.WriteLine(" has no gear! Oh, frick!");
                    PressAnyKeyPrompt();
                    TurnSequence(actors);
                }
                else { PressAnyKeyPrompt(); TurnSequence(actors); }
            }
        }

        public void SelectGear(List<Actor> actors)
        {
            Console.Write("Do you want to equip gear [0], or unequip gear [1]? Enter [b] to go back. ");
            bool initValidChoice = false;
            int equipChoice = -1; //0 = Equip gear. 1 = Unequip gear.

            while (!initValidChoice)
            {
                string selection = Console.ReadLine();

                if (selection == "B" || selection == "b") { initValidChoice = true; TurnSequence(actors); }
                else if (int.TryParse(selection, out int choice) && (choice == 0 || choice == 1))
                {
                    if (choice == 0 && EquippedGear.Count >= MaxGearCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have too much gear on! Unequip something first.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        equipChoice = choice;
                        initValidChoice = true;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid entry.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            if (equipChoice == 0) //Equipping gear.
            {
                Console.Write("\nEquip which gear? Enter [b] to go back. ");
                bool validChoice = false;

                while (!validChoice)
                {
                    string selection = Console.ReadLine();

                    if (selection == "B" || selection == "b") { validChoice = true; TurnSequence(actors); }
                    else if (int.TryParse(selection, out int choice) && choice > 0 && choice <= Gear.Count)
                    {
                        Gear[choice - 1].Equip(actors, this);
                        validChoice = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            else if (equipChoice == 1) //Unequipping gear.
            {
                Console.Write("\nUnequip which gear? Enter [b] to go back. ");
                bool validChoice = false;

                while (!validChoice)
                {
                    string selection = Console.ReadLine();

                    if (selection == "B" || selection == "b") { validChoice = true; TurnSequence(actors); }
                    else if (int.TryParse(selection, out int choice) && choice > 0 && choice <= EquippedGear.Count)
                    {
                        Gear[choice - 1].Unequip(actors, this);
                        validChoice = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        public void OnWaveAdvance()
        {
            WaveID++;
            if (!IsHeroParty)
            {
                DroppedGear.Clear();
                DroppedItems.Clear();
            }
        }

        public static void PressAnyKeyPrompt()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void AttackEffects(List<Actor> actors, string name, int damage)
        {
            if (PlayerControlled)
            {
                bool validChoice = false;
                while (!validChoice)
                {
                    List<Actor> targets = PlayerControlledTargeting(actors);
                    string selection = Console.ReadLine();

                    if (selection == "B" || selection == "b") { validChoice = true; TurnSequence(actors); }
                    else if (int.TryParse(selection, out int choice) && choice < targets.Count && choice >= 0)
                    { //                                                  ^^Make sure the choice is a valid target.
                      //                                                    Otherwise, OutOfRange exception.
                        try
                        {
                            if(OffensiveModifiers.Count != 0)
                            {
                                for(int index = 0; index < OffensiveModifiers.Count; index++)
                                {
                                    damage = damage + OffensiveModifiers[index](damage, out string description);
                                }
                            }
                            
                            if (targets[choice].DefensiveModifiers.Count != 0)
                            {
                                int newDamage = targets[choice].DefensiveModifiers[0](damage, out string description);
                                Modifier[] modifersConverted = targets[choice].DefensiveModifiers.ToArray();

                                if (newDamage == damage)
                                {
                                    Console.WriteLine("");
                                    targets[choice].DisplayName();
                                    Console.WriteLine($"'s {modifersConverted[0].Method.Name} was ineffective.");
                                    Console.WriteLine("");
                                    DisplayName();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write($" used {name} on ");
                                    targets[choice].DisplayName();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($". It did {damage} damage!");
                                    targets[choice].CurrentHP = targets[choice].CurrentHP - damage;
                                    targets[choice].DisplayName();
                                }
                                else
                                {
                                    Console.WriteLine("");
                                    targets[choice].DisplayName();
                                    Console.WriteLine($"'s {modifersConverted[0].Method.Name} altered its damage!");
                                    Console.WriteLine("");
                                    DisplayName();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write($" used {name} on ");
                                    targets[choice].DisplayName();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($". It did {newDamage} damage!");
                                    targets[choice].CurrentHP = targets[choice].CurrentHP - newDamage;
                                    targets[choice].DisplayName();
                                }
                            }
                            else
                            {
                                Console.WriteLine("");
                                DisplayName();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write($" used {name} on ");
                                targets[choice].DisplayName();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($". It did {damage} damage!");
                                targets[choice].CurrentHP = targets[choice].CurrentHP - damage;
                                targets[choice].DisplayName();
                            }

                            if (targets[choice].CurrentHP <= 0)
                            {
                                Console.WriteLine($" : " +
                                $"0 / {targets[choice].MaxHP}");
                                PressAnyKeyPrompt();
                                targets[choice].DeathSequence(actors);
                            }
                            else
                            {
                                Console.WriteLine($" : " +
                                    $"{targets[choice].CurrentHP} / {targets[choice].MaxHP}");
                            }

                            validChoice = true;
                            break;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid entry.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            else
            {
                DisplayName();
                Console.WriteLine(" is choosing a target.");
                var target = TargetChoiceEnemy(actors);

                if (OffensiveModifiers.Count != 0)
                {
                    for (int index = 0; index < OffensiveModifiers.Count; index++)
                    {
                        damage = damage + OffensiveModifiers[index](damage, out string description);
                    }
                }

                if (target.DefensiveModifiers.Count != 0)
                {
                    int newDamage = target.DefensiveModifiers[0](damage, out string description);
                    Modifier[] modifersConverted = target.DefensiveModifiers.ToArray();

                    if (newDamage == damage)
                    {
                        Console.WriteLine("");
                        target.DisplayName();
                        Console.WriteLine($"'s {modifersConverted[0].Method.Name} was ineffective.");
                        Console.WriteLine("");
                        DisplayName();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($" used {name} on ");
                        target.DisplayName();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($". It did {damage} damage!");
                        target.CurrentHP = target.CurrentHP - damage;
                        target.DisplayName();
                    }
                    else
                    {
                        Console.WriteLine("");
                        target.DisplayName();
                        Console.WriteLine($"'s {modifersConverted[0].Method.Name} altered its damage!");
                        Console.WriteLine("");
                        DisplayName();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($" used {name} on ");
                        target.DisplayName();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($". It did {newDamage} damage!");
                        target.CurrentHP = target.CurrentHP - newDamage;
                        target.DisplayName();
                    }
                }
                else
                {
                    Console.WriteLine("");
                    DisplayName();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($" used {name} on ");
                    target.DisplayName();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($". It did {damage} damage!");
                    target.CurrentHP = target.CurrentHP - damage;
                    target.DisplayName();
                }

                if (target.CurrentHP <= 0)
                {
                    Console.WriteLine($" : " +
                    $"0 / {target.MaxHP}");
                    PressAnyKeyPrompt();
                    target.DeathSequence(actors);
                }
                else
                {
                    Console.WriteLine($" : " +
                        $"{target.CurrentHP} / {target.MaxHP}");
                }
            }

            PressAnyKeyPrompt();
        }

        public delegate int Modifier(int inputDamage, out string description);
        public delegate void Attack(List<Actor> actors, bool justDescription, out string description);
    }
}
