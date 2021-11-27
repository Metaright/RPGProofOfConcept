using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalBattle
{
    class BoxingGloves : Gear
    {
        public BoxingGloves() 
        { 
            Name = "Boxing Gloves";
            OffensiveModifierName = "Big Boy Fists";
            Description = "These big ol' gloves raise your damage output by 1.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {OffensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.OffensiveModifiers.Add(user.BigBoyFists);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {OffensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.OffensiveModifiers.Add(user.BigBoyFists);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" has lost the power of {OffensiveModifierName}!");
            user.OffensiveModifiers.Remove(user.BigBoyFists);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class BucketOfSlime : Gear
    {
        public BucketOfSlime()
        {
            Name = "Bucket of Slime";
            DefensiveModifierName = "Slimy";
            Description = "Equal parts disgusting and effective.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {DefensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.DefensiveModifiers.Add(user.Slimy);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {DefensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.DefensiveModifiers.Add(user.Slimy);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" has lost the power of {DefensiveModifierName}!");
            user.DefensiveModifiers.Remove(user.Slimy);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class Dagger : Gear
    {
        public Dagger() : base()
        { 
            Name = "Dagger";
            AttackName = "Jab";
            Description = "This is really pointy. The Jab attack does a steady 1 point of damage.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.Jab);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.Jab);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can no longer use {AttackName}!");
            user.Actions.Remove(user.Jab);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class SanguineSparkler : Gear
    {
        public SanguineSparkler() : base()
        {
            Name = "Sanguine Sparkler";
            AttackName = "Camaraderie Comet";
            Description = "Celebrate your relationships with an attack that gets more powerful the more teammates you have.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.CamaraderieComet);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.CamaraderieComet);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can no longer use {AttackName}!");
            user.Actions.Remove(user.CamaraderieComet);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class FlamingBrassKnuckles : Gear
    {
        public FlamingBrassKnuckles()
        {
            Name = "Flaming Brass Knuckles";
            OffensiveModifierName = "Fiery Fists";
            Description = "Banned in 37 states, these increase your damage by 2.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {OffensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.OffensiveModifiers.Add(user.FieryFists);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {OffensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.OffensiveModifiers.Add(user.FieryFists);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" has lost the power of {OffensiveModifierName}!");
            user.OffensiveModifiers.Remove(user.FieryFists);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class LucifersDice : Gear
    {
        public LucifersDice()
        {
            Name = "Lucifer's Dice";
            OffensiveModifierName = "Unholy Gamble";
            Description = "It's not sacrilege; it's God's plan.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {OffensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.OffensiveModifiers.Add(user.UnholyGamble);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {OffensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.OffensiveModifiers.Add(user.UnholyGamble);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" has lost the power of {OffensiveModifierName}!");
            user.OffensiveModifiers.Remove(user.UnholyGamble);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class SterilizedNeedle : Gear
    {
        public SterilizedNeedle()
        {
            Name = "Sterilized Needle";
            AttackName = "Transfusion";
            Description = "Please only use this if your licensure is currently valid.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.Transfusion);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.Transfusion);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can no longer use {AttackName}!");
            user.Actions.Remove(user.Transfusion);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class SuitOfArmor : Gear
    {
        public SuitOfArmor()
        {
            Name = "SuitOfArmor";
            DefensiveModifierName = "Steel Armor";
            Description = "Fit for someone much more valiant than you.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {DefensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.DefensiveModifiers.Add(user.SteelArmor);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {DefensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.DefensiveModifiers.Add(user.SteelArmor);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" has lost the power of {DefensiveModifierName}!");
            user.DefensiveModifiers.Remove(user.SteelArmor);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class Sword : Gear
    {
        public Sword() 
        { 
            Name = "Sword";
            AttackName = "Slash";
            Description = "Don't lick this. The Slash attack does 2 damage, or your money back.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.Slash);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.Slash);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can no longer use {AttackName}!");
            user.Actions.Remove(user.Slash);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class TurtleShell : Gear
    {
        public TurtleShell()
        {
            Name = "Turtle Shell";
            DefensiveModifierName = "Stone Armor";
            Description = "It'a not evil if you didn't kill the turtle, I guess.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {DefensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.DefensiveModifiers.Add(user.StoneArmor);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" is imbued with the power of {DefensiveModifierName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.DefensiveModifiers.Add(user.StoneArmor);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped the {Name}! ");
            user.DisplayName();
            Console.WriteLine($" has lost the power of {DefensiveModifierName}!");
            user.DefensiveModifiers.Remove(user.StoneArmor);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }

    class VinsBow : Gear
    {
        public VinsBow() 
        { 
            Name = "Vin's Bow";
            AttackName = "Quick Shot";
            Description = "Vin Fletcher's favorite bow. The Quick Shot attack does either 3 damage, or nothing.";
        }
        public override void Equip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.QuickShot);
            user.EquippedGear.Add(this);
            user.Gear.Remove(this);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void EquipAtBeginning(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" equipped {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can now use {AttackName}!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Description);
            Console.ForegroundColor = ConsoleColor.White;
            user.Actions.Add(user.QuickShot);
            Equipped = true;
            Actor.PressAnyKeyPrompt();
        }

        public override void Unequip(List<Actor> actors, Actor user)
        {
            user.DisplayName();
            Console.Write($" unequipped {Name}! ");
            user.DisplayName();
            Console.WriteLine($" can no longer use {AttackName}!");
            user.Actions.Remove(user.QuickShot);
            user.EquippedGear.Remove(this);
            user.Gear.Add(this);
            Equipped = false;
            Actor.PressAnyKeyPrompt();
        }
    }


}
