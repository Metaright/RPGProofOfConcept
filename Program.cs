using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace TheFinalBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSequence();
        }

        public static void GameSequence()
        {
            int turn = 1;
            int wave = 1;
            bool gameOver = false;
            EventHandler eventHandler = new EventHandler();
            List<Actor> actors = new List<Actor>();
            actors.Add(eventHandler);

            InitializeGame(actors);


            while (!gameOver)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"=============================================");
                Console.WriteLine($"Turn {turn}");
                Console.ForegroundColor = ConsoleColor.White;
                for (int index = 0; index < actors.Count; index++)
                {
                    if (actors[index].IsHeroParty || actors[index].WaveID == wave)
                    { actors[index].TurnSequence(actors); }

                    if (CheckForWaveEnd(actors, wave))
                    {
                        if (CheckForGameEnd(actors, wave))
                        {
                            gameOver = true;
                        }
                        else
                        {
                            gameOver = false;
                            AdvanceWave(wave, eventHandler, actors); wave++;
                            Thread.Sleep(2000);
                            //foreach(Actor actor in actors) { Console.WriteLine(actor);} //<<Debug
                            //Console.WriteLine(gameOver); //<<Debug
                        }
                    }
                    else if (CheckForGameEnd(actors, wave)) { gameOver = true; }
                }
                turn++;
            }
        }

        public static void InitializeGame(List<Actor> list)
        {
            Console.WriteLine("Welcome to the game! Please choose between the following modes:\n" +
                "[1] You control the hero. The computer controls everyone else.\n" +
                "[2] You control the hero's whole party. The computer controls the enemies.\n" +
                "[3] You control the enemies. The computer controls the hero's party.\n" +
                "[4] The computer controls everyone.\n" +
                "[5] You control everyone.\n" +
                "Please note that in all modes, the game still advances/ends based on the hero's team's outcome.");


            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 5)
                {
                    switch (choice)
                    {
                        case 1:
                            InitializeActors(list, false, true, false);
                            break;
                        case 2:
                            InitializeActors(list, false, true, true);
                            break;
                        case 3:
                            InitializeActors(list, true, false, false);
                            break;
                        case 4:
                            InitializeActors(list, false, false, false);
                            break;
                        case 5:
                            InitializeActors(list, true, true, true);
                            break;
                    }
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid entry.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public static void AdvanceWave(int currentWave, EventHandler eventHandler, List<Actor> actors)
        {
            Console.Clear();
            Console.WriteLine($"Wave defeated! Advancing to wave {currentWave + 1}.");
            eventHandler.AdvanceWave();
        }

        public static bool CheckForWaveEnd(List<Actor> list, int wave)
        {
            IEnumerable<Actor> targetObject = from o in list
                                              where o.IsHeroParty == false
                                              where o.WaveID == wave
                                              select o;
            if (targetObject.Any<Actor>()) { return false; }
            else return true;
        }

        public static bool CheckForGameEnd(List<Actor> list, int wave)
        {
            bool enemyDefeated;
            IEnumerable<Actor> targetObject = from o in list //Sort through all present actors.
                                              where o.IsHeroParty == false //Filter by who is in the enemies' party.
                                              where o.WaveID >= wave //Filter by enemies who are at or above this wave.
                                              where o.Name != "EventHandler" //Exclude the event handler.
                                              select o;
            if (targetObject.Any<Actor>()) { enemyDefeated = false; }
            else { enemyDefeated = true; }

            bool playerDefeated;
            IEnumerable<Actor> targetObjectTwo = from o in list //Sort through all present actors.
                                                 where o.IsHeroParty == true //Filter by who is in the player's party.
                                                 where o.WaveID >= wave //Filter by enemies who are at or above this wave.
                                                 where o.Name != "EventHandler" //Exclude the event handler.
                                                 select o;
            if (targetObjectTwo.Any<Actor>()) { playerDefeated = false; }
            else { playerDefeated = true; }

            if (playerDefeated || enemyDefeated)
            {
                if (playerDefeated) { EndSequence(false); return true; }
                else if (enemyDefeated) { EndSequence(true); return true; }
                else { Console.WriteLine("ERROR in Program.CheckForGameEnd method."); return true; }
            }
            else return false;
        }

        public static void InitializeActors(List<Actor> actors, bool enemiesControlled, bool heroControlled, bool heroPartyControlled)
        {
            Player player = new Player(actors, heroControlled);
            actors.Add(player);

            List<Type> HeroPartyMembers = new List<Type>
            {
                typeof(VinFletcher),
                typeof(LarryCrackers),
                typeof(BrendaJohnston),
                typeof(SkeletonDefector)
            };

            List<Type> DiffOneEnemies = new List<Type>
            {
                typeof(Skeleton),
                typeof(StoneAmarok),
                typeof(OrneryImp),
                typeof(TreasureMasochist)
            };

            List<Type> DiffTwoEnemies = new List<Type>
            {
                typeof(SlightlyLargerSkeleton),
                typeof(MalevolentViper),
                typeof(SavageSprite),
                typeof(TreasureMasochist)
            };

            List<Type> DiffThreeEnemies = new List<Type>
            {
                typeof(SteelAmarok),
                typeof(PoultergeistPugilist),
                typeof(GrumpyCentaur),
                typeof(TreasureMasochist)
            };

            List<Type> DiffFourEnemies = new List<Type>
            {
                typeof(HumungoSkeleton),
                typeof(TwistedAcrobat),
                typeof(SpiritOfPestilence),
                typeof(TreasureMasochist)
            };

            List<Type> DiffFiveEnemies = new List<Type>
            {
                typeof(UncodedOne),
                typeof(Hydra),
                typeof(BroodQueen),
                typeof(TreasureMasochist)
            };

            List<Type> GearList = new List<Type>
            {
                typeof(BoxingGloves),
                typeof(Sword),
                typeof(Dagger),
                typeof(VinsBow),
                typeof(FlamingBrassKnuckles),
                typeof(BucketOfSlime),
                typeof(SanguineSparkler),
                typeof(TurtleShell),
                typeof(SuitOfArmor),
                typeof(LucifersDice),
                typeof(SterilizedNeedle)
            };

            List<Type> ItemList = new List<Type>
            {
                typeof(HealthPotion),
                typeof(SwiftnessPotion),
                typeof(GemOfThunderbolt),
                typeof(GemOfSmite),
                typeof(HPCrystal),
                typeof(GearCrystal)
            };

            Random random = new Random();
            int waveCount = 10;
            bool validSelectionCustomize = false;
            bool customizeIsGo = false;
            bool validSelectionWave = false;
            bool validSelectionMin = false;
            bool validSelectionMax = false;
            bool validSelectionPartyOdds = false;
            bool validSelectionGearOdds = false;
            bool validSelectionItemOdds = false;
            int oddsOfNewPartyMember = 21; //n = One in n-1 odds of getting a new party member each wave.
            int oddsOfGear = 6; //n = One in n-1 odds of enemies getting new gear each wave.
            int oddsOfItem = 6; //n = One in n-1 odds of getting a new party member each wave.
            int minEnemies = 1;
            int maxEnemies = 3;

            while(!validSelectionCustomize)
            {
                Console.Write("Do you want to customize various aspects of the game's generation? Enter [0] for yes or [1] for no. ");
                if (int.TryParse(Console.ReadLine(), out int result) && result == 0 || result == 1)
                {
                    if(result == 0) { customizeIsGo = true; }
                    else { customizeIsGo = false; }
                    validSelectionCustomize = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid entry. Entry must be [0] for yes or [1] for no.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            if (customizeIsGo)
            {
                while (!validSelectionWave)
                {
                    Console.Write("How many waves of enemies do you want? The value must be evenly divisible by 5. ");
                    if (int.TryParse(Console.ReadLine(), out int result) && result > 0 && result % 5 == 0)
                    {
                        waveCount = result;
                        validSelectionWave = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry. Entry must be positive and evenly divisible by 5.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.WriteLine("");

                while (!validSelectionMin)
                {
                    Console.Write("What is the minimum number of enemies you want per wave? ");
                    if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                    {
                        minEnemies = result;
                        validSelectionMin = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry. Entry must be above 0.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.WriteLine("");

                while (!validSelectionMax)
                {
                    Console.Write("What is the maximum number of enemies you want per wave? ");
                    if (int.TryParse(Console.ReadLine(), out int result) && result > minEnemies)
                    {
                        maxEnemies = result + 1;
                        validSelectionMax = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry. Entry must be above the minimum number entered previously.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.WriteLine("");

                while (!validSelectionPartyOdds)
                {
                    Console.Write("What would you like to be the odds of gaining a new party member between waves? (20 => 1-in-20 odds, etc.) ");
                    if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                    {
                        oddsOfNewPartyMember = result + 1;
                        validSelectionPartyOdds = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry. Entry must be above 0.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.WriteLine("");

                while (!validSelectionGearOdds)
                {
                    Console.Write("What would you like to be the odds of each enemy having gear that you can steal? (20 => 1-in-20 odds, etc.) ");
                    if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                    {
                        oddsOfGear = result + 1;
                        validSelectionGearOdds = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry. Entry must be above 0.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.WriteLine("");

                while (!validSelectionItemOdds)
                {
                    Console.Write("What would you like to be the odds of each enemy having items that you can steal? (20 => 1-in-20 odds, etc.) ");
                    if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                    {
                        oddsOfItem = result + 1;
                        validSelectionItemOdds = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry. Entry must be above 0.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            List<int> wavesList = new List<int>();
            for (int index = 1; index < waveCount + 1; index++) wavesList.Add(index);
            int oneFifthOfWaveCount = wavesList.Count / 5;

            for (int currentWave = 1; currentWave <= wavesList.Count; currentWave++)
            {
                if (currentWave <= oneFifthOfWaveCount) //First fifth of game, for level one enemies.
                {
                        PopulateActors(actors, HeroPartyMembers, GearList, ItemList, DiffOneEnemies, currentWave, enemiesControlled, heroPartyControlled, oddsOfGear, oddsOfItem, oddsOfNewPartyMember);
                }
                else if (currentWave <= oneFifthOfWaveCount * 2)
                {
                        PopulateActors(actors, HeroPartyMembers, GearList, ItemList, DiffTwoEnemies, currentWave, enemiesControlled, heroPartyControlled, oddsOfGear, oddsOfItem, oddsOfNewPartyMember);
                }
                else if (currentWave <= oneFifthOfWaveCount * 3)
                {
                        PopulateActors(actors, HeroPartyMembers, GearList, ItemList, DiffThreeEnemies, currentWave, enemiesControlled, heroPartyControlled, oddsOfGear, oddsOfItem, oddsOfNewPartyMember);
                }
                else if (currentWave <= oneFifthOfWaveCount * 4)
                {
                        PopulateActors(actors, HeroPartyMembers, GearList, ItemList, DiffFourEnemies, currentWave, enemiesControlled, heroPartyControlled, oddsOfGear, oddsOfItem, oddsOfNewPartyMember);
                }
                else if (currentWave <= oneFifthOfWaveCount * 5)
                {
                        PopulateActors(actors, HeroPartyMembers, GearList, ItemList, DiffFiveEnemies, currentWave, enemiesControlled, heroPartyControlled, oddsOfGear, oddsOfItem, oddsOfNewPartyMember);
                }
            }

        }

        static void EndSequence(bool endState) //TRUE: Player victory. FALSE: Enemy victory.
        {
            if (endState)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You did the mighty thing!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh no! You did a bad!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void PopulateActors(List<Actor> actors, List<Type> HeroPartyMembers, List<Type> GearList, List<Type> ItemList, List<Type> EnemiesList, int currentWave, bool enemiesControlled, bool heroPartyControlled, int oddsOfGear, int oddsOfItem, int oddsOfNewPartyMember)
        {
            Random random = new Random();
            int selectionCount = random.Next(1, 4); //Choose between 1 and 3 enemies for this wave.
            for (int index = 0; index < selectionCount; index++)
            {
                Type entryGear = GearList[random.Next(GearList.Count)];
                Object[] argsGear = new Object[] { };
                var newGear = Activator.CreateInstance(entryGear, argsGear);
                Gear newGearConverted = (Gear)newGear;

                Type entryItem = (ItemList[random.Next(ItemList.Count)]);
                Object[] argsItem = new Object[] { };
                object newItem = Activator.CreateInstance(entryItem, argsItem);
                Item newItemConverted = (Item)newItem;

                var entryItemTwo = (ItemList[random.Next(ItemList.Count)]);
                Object[] argsItemTwo = new Object[] { };
                object newItemTwo = Activator.CreateInstance(entryItem, argsItem);
                Item newItemConvertedTwo = (Item)newItemTwo;

                var entry = (EnemiesList[random.Next(EnemiesList.Count)]);
                Object[] argsWithGear = new Object[] { currentWave, enemiesControlled, newGearConverted };
                Object[] argsWithoutGear = new Object[] { currentWave, enemiesControlled };

                if (random.Next(1, oddsOfGear) == 1)
                {
                    object newActor = Activator.CreateInstance(entry, argsWithGear);

                    Actor newActorConverted = (Actor)newActor;

                    actors.Add(newActorConverted);

                    if (random.Next(1, oddsOfItem) == 1)
                    {
                        newActorConverted.Items.Add(newItemConverted);
                        newActorConverted.Items.Add(newItemConvertedTwo);
                    }
                }
                else
                {
                    object newActor = Activator.CreateInstance(entry, argsWithoutGear);
                    Actor newActorConverted = (Actor)newActor;

                    actors.Add(newActorConverted);

                    if (random.Next(1, oddsOfItem) == 1)
                    {
                        newActorConverted.Items.Add(newItemConverted);
                        newActorConverted.Items.Add(newItemConvertedTwo);
                    }
                }

                if (random.Next(1, oddsOfNewPartyMember) == 1) //Check for a hero party character spawning each wave.
                {
                    Type entryTwo = (HeroPartyMembers[random.Next(HeroPartyMembers.Count)]);
                    //Identify the type of a random member of HeroPartyMembers.

                    Object[] argsTwo = new Object[] { actors, currentWave, heroPartyControlled };
                    //Array with parameters for new heroes' construction.

                    object newHero = Activator.CreateInstance(entryTwo, argsTwo);
                    actors.Add((Actor)newHero);
                }
            }
        }

    }
}
