using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheFinalBattle
{
    class GearCrystal : Item
    {
        public GearCrystal() { Name = "Gear Crystal"; }

        public override void Usage(Actor user, Actor target)
        {
            target.MaxGearCount += 2;
            target.CurrentHP = target.MaxHP;
            user.Items.Remove(this);
        }

        public override void Message(Actor target)
        {
            target.DisplayName();
            Console.Write(" permanently raised maximum gear slots!");
        }
    }

    class GemOfSmite : Item
    {
        public GemOfSmite() { Name = "Gem of Smite"; }

        public override void Usage(Actor user, Actor target)
        {
            target.Actions.Add(target.Smite);
            user.Items.Remove(this);
        }

        public override void Message(Actor target)
        {
            target.DisplayName();
            Console.Write(" can now use Smite!");
        }
    }

    class HPCrystal : Item
    {
        public HPCrystal() { Name = "HP Crystal"; }

        public override void Usage(Actor user, Actor target)
        {
            target.MaxHP += 5;
            target.CurrentHP = target.MaxHP;
            user.Items.Remove(this);
        }

        public override void Message(Actor target)
        {
            target.DisplayName();
            Console.Write(" permanently raised HP!");
        }
    }

    class GemOfThunderbolt : Item
    {
        public GemOfThunderbolt() { Name = "Gem of Thunderbolt"; }

        public override void Usage(Actor user, Actor target)
        {
            target.Actions.Add(target.Thunderbolt);
            user.Items.Remove(this);
        }

        public override void Message(Actor target)
        {
            target.DisplayName();
            Console.Write(" can now use Thunderbolt!");
        }
    }

    class HealthPotion : Item
    {
        public HealthPotion() { Name = "Health Potion"; }

        public override void Usage(Actor user, Actor target)
        {
            if (target.CurrentHP + 15 > target.MaxHP) target.CurrentHP = target.MaxHP;
            else target.CurrentHP = target.CurrentHP + 15;
            user.Items.Remove(this);
        }

        public override void Message(Actor target)
        {
            target.DisplayName();
            Console.Write(" restored 15 HP!");
        }
    }

    class SwiftnessPotion : Item
    {
        public SwiftnessPotion() { Name = "Swiftness Potion"; }

        public override void Usage(Actor user, Actor target)
        {
            target.SwiftnessPotion = true;
            user.Items.Remove(this);
        }

        public override void Message(Actor target)
        {
            target.DisplayName();
            Console.Write(" can take another turn!");
        }
    }

}
