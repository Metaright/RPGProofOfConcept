using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheFinalBattle
{
    class Player : Actor
    {
        public override string Type => "player";
        public override int MaxHP { get => 25; }

        public Player(List<Actor> actors, bool playerControlled, Gear gear)
        {
            WaveID = 1;
            PlayerControlled = playerControlled;
            EventHandler eventHandler = SearchForEventHandler(actors);
            eventHandler.WaveAdvance += OnWaveAdvance;

            PopulateActions();
            IsHeroParty = true;
            Console.Write("Please enter a name for our hero. ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Gear.Add(gear);
            CurrentHP = MaxHP;
        }

        public Player(List<Actor> actors, bool playerControlled)
        {
            WaveID = 1;
            PlayerControlled = playerControlled;
            EventHandler eventHandler = SearchForEventHandler(actors);
            eventHandler.WaveAdvance += OnWaveAdvance;

            PopulateActions();
            IsHeroParty = true;
            Console.Write("Please enter a name for our hero. ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            CurrentHP = MaxHP;
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Punch, SuperPunch }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Punch }; }
        }
        public override void Display()
        {
            Console.Write($"\nIt is ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{Name}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"'s turn. || HP: {CurrentHP} / {MaxHP}");
        }

        public override void DisplayName()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{Name}");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }

    class BrendaJohnston : Actor
    {
        public override string Type => "Brenda Johnston";
        public override int MaxHP => 15;

        public BrendaJohnston(List<Actor> actors, int wave, bool playerControlled)
        {
            Name = "Brenda Johnston";
            WaveID = wave;
            PlayerControlled = playerControlled;
            EventHandler eventHandler = SearchForEventHandler(actors);
            eventHandler.WaveAdvance += OnWaveAdvance;
            //EquippedGear.Add(new Dagger());

            PopulateActions();
            IsHeroParty = true;
            Console.Write("You are joined by Brenda Johnston, holy cleric.");
            CurrentHP = MaxHP;
        }

        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Transfusion }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Transfusion }; }
        }

        public override void DisplayName()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{Name}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class BroodQueen : Actor
    {
        public override string Type => "BroodQueen";
        public override int MaxHP { get => 30; }
        public override int DifficultyID { get => 5; }

        public BroodQueen(int wave, bool playerControlled, Gear gear)
        {
            Name = "Brood Queen";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            DefensiveModifiers.Add(Slimy);
            PopulateActions();
        }

        public BroodQueen(int wave, bool playerControlled)
        {
            Name = "Brood Queen";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            DefensiveModifiers.Add(Slimy);
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Transfusion }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Transfusion }; }
        }
    }

    class SkeletonDefector : Actor
    {
        public override string Type => "Skeleton Defector";
        public override int MaxHP => 15;

        public SkeletonDefector(List<Actor> actors, int wave, bool playerControlled)
        {
            Name = "Skeleton Defector";
            WaveID = wave;
            PlayerControlled = playerControlled;
            EventHandler eventHandler = SearchForEventHandler(actors);
            eventHandler.WaveAdvance += OnWaveAdvance;
            EquippedGear.Add(new Dagger());

            PopulateActions();
            IsHeroParty = true;
            Console.Write("You are joined by a skeleton who has defected from the evil army.");
            CurrentHP = MaxHP;
        }

        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, BoneCrunch }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, BoneCrunch }; }
        }

        public override void DisplayName()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{Name}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class GrumpyCentaur : Actor
    {
        public override string Type => "GrumpyCentaur";
        public override int MaxHP { get => 9; }
        public override int DifficultyID { get => 3; }

        public GrumpyCentaur(int wave, bool playerControlled, Gear gear)
        {
            Name = "Grumpy Centaur";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public GrumpyCentaur(int wave, bool playerControlled)
        {
            Name = "Grumpy Centaur";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, QuickShot }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, QuickShot }; }
        }
    }

    class HumungoSkeleton : Actor
    {
        public override string Type => "humungoskeleton";
        public override int MaxHP { get => 15; }
        public override int DifficultyID { get => 4; }

        public HumungoSkeleton(int wave, bool playerControlled, Gear gear)
        {
            Name = "Humungo Skeleton";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public HumungoSkeleton(int wave, bool playerControlled) //Skeleton objects initilize to not being in hero's party.
        {
            Name = "Humungo Skeleton";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, BoneCrunch }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, BoneCrunch }; }
        }
    }

    class Hydra : Actor
    {
        public override string Type => "Hydra";
        public override int MaxHP { get => 20; }
        public override int DifficultyID { get => 5; }

        public Hydra(int wave, bool playerControlled, Gear gear) //Initilize to not being in hero's party.
        {
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            CurrentHP = MaxHP;
            WaveID = wave;
            Name = "Hydra";
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public Hydra(int wave, bool playerControlled) //Initilize to not being in hero's party.
        {
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            CurrentHP = MaxHP;
            WaveID = wave;
            Name = "Hydra";
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, FireBreath }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, FireBreath }; }
        }

    }

    class LarryCrackers : Actor
    {
        public override string Type => "Larry Crackers";
        public override int MaxHP => 20;

        public LarryCrackers(List<Actor> actors, int wave, bool playerControlled)
        {
            Name = "Larry Crackers";
            WaveID = wave;
            PlayerControlled = playerControlled;
            EventHandler eventHandler = SearchForEventHandler(actors);
            eventHandler.WaveAdvance += OnWaveAdvance;
            EquippedGear.Add(new TurtleShell());

            PopulateActions();
            IsHeroParty = true;
            Console.Write("You are joined by Larry Crackers, hefty man of great girth and spirit.");
            CurrentHP = MaxHP;
        }

        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, HeavyChop }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, HeavyChop }; }
        }

        public override void DisplayName()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{Name}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class OrneryImp : Actor
    {
        public override string Type => "OrneryImp";
        public override int MaxHP { get => 3; }
        public override int DifficultyID { get => 1; }

        public OrneryImp(int wave, bool playerControlled, Gear gear)
        {
            Name = "Ornery Imp";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public OrneryImp(int wave, bool playerControlled)
        {
            Name = "Ornery Imp";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Punch }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Punch }; }
        }
    }

    class MalevolentViper : Actor
    {
        public override string Type => "Malevolentiper";
        public override int MaxHP { get => 6; }
        public override int DifficultyID { get => 2; }

        public MalevolentViper(int wave, bool playerControlled, Gear gear) //Initilize to not being in hero's party.
        {
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            CurrentHP = MaxHP;
            WaveID = wave;
            Name = "Malevolent Viper";
            EquippedGear.Add(gear);
            PopulateActions();
            DefensiveModifiers.Add(Slimy);
        }

        public MalevolentViper(int wave, bool playerControlled) //Initilize to not being in hero's party.
        {
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            CurrentHP = MaxHP;
            WaveID = wave;
            Name = "Malevolent Viper";
            PopulateActions();
            DefensiveModifiers.Add(Slimy);
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Bite }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Bite }; }
        }

    }

    class PoultergeistPugilist : Actor
    {
        public override string Type => "PoultergeistPugilist";
        public override int MaxHP { get => 8; }
        public override int DifficultyID { get => 3; }

        public PoultergeistPugilist(int wave, bool playerControlled, Gear gear)
        {
            Name = "Pugilist Poultergeist";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
            OffensiveModifiers.Add(BigBoyFists);
        }

        public PoultergeistPugilist(int wave, bool playerControlled) //Skeleton objects initilize to not being in hero's party.
        {
            Name = "Stone Amarok";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
            OffensiveModifiers.Add(BigBoyFists);
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Punch }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Punch }; }
        }
    }

    class SavageSprite : Actor
    {
        public override string Type => "SavageSprite";
        public override int MaxHP { get => 5; }
        public override int DifficultyID { get => 2; }

        public SavageSprite(int wave, bool playerControlled, Gear gear)
        {
            Name = "Savage Sprite";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public SavageSprite(int wave, bool playerControlled)
        {
            Name = "Savage Sprite";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, QuickShot }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, QuickShot }; }
        }
    }

    class Skeleton : Actor
    {
        public override string Type => "skeleton";
        public override int MaxHP { get => 5; }
        public override int DifficultyID { get => 1; }

        public Skeleton(int wave, bool playerControlled, Gear gear)
        {
            Name = "Skeleton";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
        }
        
        public Skeleton(int wave, bool playerControlled) //Skeleton objects initilize to not being in hero's party.
        {
            Name = "Skeleton";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
        } 
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, BoneCrunch }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, BoneCrunch }; }
        }

        public string NameRandom()
        {
            Random random = new Random();
            return random.Next(10) switch
            {
                0 => "Bob",
                1 => "Barnaby",
                2 => "Periwinkle",
                3 => "Fluffy",
                4 => "Hazelnut",
                5 => "Princess Twinkles",
                6 => "2017 Dodge Rover",
                7 => "Not President Obama",
                8 => "Harold",
                9 => "Mr. Spooks"
            };
        }

    }

    class SlightlyLargerSkeleton : Actor
    {
        public override string Type => "sllightlylargerskeleton";
        public override int MaxHP { get => 10; }
        public override int DifficultyID { get => 2; }

        public SlightlyLargerSkeleton(int wave, bool playerControlled, Gear gear)
        {
            Name = "Skeleton";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public SlightlyLargerSkeleton(int wave, bool playerControlled) //Skeleton objects initilize to not being in hero's party.
        {
            Name = "Skeleton";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, BoneCrunch }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, BoneCrunch }; }
        }
    }

    class SteelAmarok : Actor
    {
        public override string Type => "steelAmarok";
        public override int MaxHP { get => 8; }
        public override int DifficultyID { get => 3; }

        public SteelAmarok(int wave, bool playerControlled, Gear gear)
        {
            Name = "Steel Amarok";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
            DefensiveModifiers.Add(SteelArmor);
        }

        public SteelAmarok(int wave, bool playerControlled) //Skeleton objects initilize to not being in hero's party.
        {
            Name = "Stone Amarok";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
            DefensiveModifiers.Add(SteelArmor);
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Bite }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Bite }; }
        }
    }

    class SpiritOfPestilence : Actor
    {
        public override string Type => "SpiritOfPestilence";
        public override int MaxHP { get => 15; }
        public override int DifficultyID { get => 4; }

        public SpiritOfPestilence(int wave, bool playerControlled, Gear gear)
        {
            Name = "Spirit of Pestilence";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public SpiritOfPestilence(int wave, bool playerControlled)
        {
            Name = "Spirit of Pestilence";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Transfusion }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Transfusion }; }
        }
    }

    class StoneAmarok : Actor
    {
        public override string Type => "stoneAmarok";
        public override int MaxHP { get => 4; }
        public override int DifficultyID { get => 1; }

        public StoneAmarok(int wave, bool playerControlled, Gear gear)
        {
            Name = "Stone Amarok";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
            DefensiveModifiers.Add(StoneArmor);
        }

        public StoneAmarok(int wave, bool playerControlled) //Skeleton objects initilize to not being in hero's party.
        {
            Name = "Stone Amarok";
            CurrentHP = MaxHP;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
            DefensiveModifiers.Add(StoneArmor);
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Bite }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Bite }; }
        }
    }

    class TreasureMasochist : Actor
    {
        public override string Type => "Treasure Masochist";
        public override int MaxHP { get => 150; }
        public override int DifficultyID { get => 1; } //Possible to apepar in all waves.

        public TreasureMasochist(int wave, bool playerControlled, Gear gear)
        {
            Name = "Treasure Masochist";
            CurrentHP = MaxHP;
            MaxGearCount = 10;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            EquippedGear.Add(gear);
            PopulateActions();
            DefensiveModifiers.Add(Masochism);

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

            Type entryGear = GearList[random.Next(GearList.Count)];
            Object[] argsGear = new Object[] { };
            var newGear = Activator.CreateInstance(entryGear, argsGear);
            Gear newGearConverted = (Gear)newGear;

            Type entryGearTwo = GearList[random.Next(GearList.Count)];
            Object[] argsGearTwo = new Object[] { };
            var newGearTwo = Activator.CreateInstance(entryGear, argsGear);
            Gear newGearConvertedTwo = (Gear)newGear;

            Type entryItem = (ItemList[random.Next(ItemList.Count)]);
            Object[] argsItem = new Object[] { };
            object newItem = Activator.CreateInstance(entryItem, argsItem);
            Item newItemConverted = (Item)newItem;

            Type entryItemTwo = (ItemList[random.Next(ItemList.Count)]);
            Object[] argsItemTwo = new Object[] { };
            object newItemTwo = Activator.CreateInstance(entryItem, argsItem);
            Item newItemConvertedTwo = (Item)newItem;

            Gear.Add(newGearConverted);
            Gear.Add(newGearConvertedTwo);
            Items.Add(newItemConverted);
            Items.Add(newItemConvertedTwo);
        }

        public TreasureMasochist(int wave, bool playerControlled)
        {
            Name = "Treasure Masochist";
            CurrentHP = MaxHP;
            MaxGearCount = 10;
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            WaveID = wave;
            PopulateActions();
            DefensiveModifiers.Add(Masochism);

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

            Type entryGear = GearList[random.Next(GearList.Count)];
            Object[] argsGear = new Object[] { };
            var newGear = Activator.CreateInstance(entryGear, argsGear);
            Gear newGearConverted = (Gear)newGear;

            Type entryGearTwo = GearList[random.Next(GearList.Count)];
            Object[] argsGearTwo = new Object[] { };
            var newGearTwo = Activator.CreateInstance(entryGear, argsGear);
            Gear newGearConvertedTwo = (Gear)newGear;

            Type entryItem = (ItemList[random.Next(ItemList.Count)]);
            Object[] argsItem = new Object[] { };
            object newItem = Activator.CreateInstance(entryItem, argsItem);
            Item newItemConverted = (Item)newItem;

            Type entryItemTwo = (ItemList[random.Next(ItemList.Count)]);
            Object[] argsItemTwo = new Object[] { };
            object newItemTwo = Activator.CreateInstance(entryItem, argsItem);
            Item newItemConvertedTwo = (Item)newItem;

            Gear.Add(newGearConverted);
            Gear.Add(newGearConvertedTwo);
            Items.Add(newItemConverted);
            Items.Add(newItemConvertedTwo);
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { WinTheGame, Transcend, DoNothing }; }
            else { Actions = new List<Attack> { WinTheGame, Transcend, DoNothing }; }
        }
    }

    class TwistedAcrobat : Actor
    {
        public override string Type => "TwistedAcrobat";
        public override int MaxHP { get => 13; }
        public override int DifficultyID { get => 4; }

        public TwistedAcrobat(int wave, bool playerControlled, Gear gear) //Initilize to not being in hero's party.
        {
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            CurrentHP = MaxHP;
            WaveID = wave;
            Name = "Twisted Acrobat";
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public TwistedAcrobat(int wave, bool playerControlled) //Initilize to not being in hero's party.
        {
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            CurrentHP = MaxHP;
            WaveID = wave;
            Name = "Twisted Acrobat";
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Jab, CamaraderieComet }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Jab, CamaraderieComet }; }
        }

    }

    class UncodedOne : Actor
    {
        public override string Type => "The Unocoded One";
        public override int MaxHP { get => 15; }
        public override int DifficultyID { get => 5; }

        public UncodedOne(int wave, bool playerControlled, Gear gear) //Initilize to not being in hero's party.
        {
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            CurrentHP = MaxHP;
            WaveID = wave;
            Name = "The Uncoded One";
            EquippedGear.Add(gear);
            PopulateActions();
        }

        public UncodedOne(int wave, bool playerControlled) //Initilize to not being in hero's party.
        {
            IsHeroParty = false;
            PlayerControlled = playerControlled;
            CurrentHP = MaxHP;
            WaveID = wave;
            Name = "The Uncoded One";
            PopulateActions();
        }
        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, Unraveling }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, Unraveling }; }
        }

    }

    class VinFletcher : Actor
    {
        public override string Type => "Vin Fletcher";
        public override int MaxHP => 15;

        public VinFletcher(List<Actor> actors, int wave, bool playerControlled)
        {
            Name = "Vin Fletcher";
            WaveID = wave;
            PlayerControlled = playerControlled;
            EventHandler eventHandler = SearchForEventHandler(actors);
            eventHandler.WaveAdvance += OnWaveAdvance;
            EquippedGear.Add(new Dagger());

            PopulateActions();
            IsHeroParty = true;
            Console.Write("You are joined by Vin Fletcher, master of archery.");
            CurrentHP = MaxHP;
        }

        public override void PopulateActions()
        {
            if (PlayerControlled) { Actions = new List<Attack> { Rest, UseAnItem, UseGear, QuickShot }; }
            else { Actions = new List<Attack> { Rest, UseAnItem, QuickShot }; }
        }

        public override void DisplayName()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{Name}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class EventHandler : Actor
    {
        public event Action WaveAdvance;
        public override bool IsEventHandler => true;

        public EventHandler() : base()
        {
            Name = "EventHandler";
        }

        public void AdvanceWave() { WaveAdvance(); }
    }

}
