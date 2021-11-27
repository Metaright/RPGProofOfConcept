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
        public void Bite(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Bite";
            int damage = 2;
            description = $"Utilize your teeth, you madman, for {damage} of damage.";
            if(!justDescription) AttackEffects(actors, name, damage);
        }

        public void BoneCrunch(List<Actor> actors, bool justDescription, out string description)
        {
            Random random = new Random();
            string name = "Bone Crunch";
            int damage = random.Next(3);
            description = "Your brittle bones deal between 0 and 2 damage.";
            if(!justDescription) AttackEffects(actors, name, damage);
        }

        public void CamaraderieComet(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Camaraderie Comet";
            int damage = 2;
            description = $"Friendship is magic! Deal {damage} damage, or double it for each teammate you have.";
            int wave = WaveID;
            if(IsHeroParty)
            {
                foreach (Actor actor in actors) { if (WaveID == wave && IsHeroParty) damage = damage + 2; }
            }
            else
            {
                foreach (Actor actor in actors) { if (WaveID == wave && !IsHeroParty) damage = damage + 2; }
            }
            
            if(!justDescription) AttackEffects(actors, name, damage);
        }

        public void FireBreath(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Fire Breath";
            int damage = 4;
            description = $"Summon the flames of Hell from ya mouth for {damage} damage.";
            if (!justDescription) AttackEffects(actors, name, damage);
        }

        public void HeavyChop(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "HeavyChop";
            int damage = 3;
            description = $"Use your heft to your advantage for {damage} damage.";
            if (!justDescription) AttackEffects(actors, name, damage);
        }

        public void Jab(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Jab";
            int damage = 2;
            description = $"Shove a pointy thing into someone's ribs for {damage} damage.";
            if(!justDescription) AttackEffects(actors, name, damage);
        }

        public void Punch(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Punch";
            description = "Your unprotected, ineffectual fist deals a single point of damage.";
            int damage = 1;
            if (!justDescription)
                AttackEffects(actors, name, damage);
        }

        public void QuickShot(List<Actor> actors, bool justDescription, out string description)
        {
            description = "Deal either 3 damage, or nothing.";
            Random random = new Random();
            string name = "Quick Shot";
            int damage;
            if (random.Next(2) == 0)
                damage = 0;
            else
                damage = 3;
            if (!justDescription)
                AttackEffects(actors, name, damage);
        }

        public void Slash(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Slash";
            int damage = 2;
            description = $"Swipe right on your enemies to deal {damage} damage.";
            if (!justDescription)
                AttackEffects(actors, name, damage);
        }

        public void Smite(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Smite";
            Random random = new Random();
            int damageRoll = random.Next(2);
            int damage;
            if (damageRoll == 0) damage = 2;
            else damage = 6;
            description = "Appeal to various gods and do either 6 damage or 2 damage.";
            if (!justDescription)
                AttackEffects(actors, name, damage);
        }

        public void SuperPunch(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Super Punch";
            int damage = 100;
            description = $"I'm not sure how you got the attack used for debugging, but it deals {damage} damage.";
            if (!justDescription)
                AttackEffects(actors, name, damage);
        }

        public void Thunderbolt(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Thunderbolt";
            int damage = 4;
            description = $"Make Zeus proud, and, as a bonus, do {damage} damage.";
            if (!justDescription)
                AttackEffects(actors, name, damage);
        }

        public void Transfusion(List<Actor> actors, bool justDescription, out string description)
        {
            string name = "Transfusion";
            Random random = new Random();
            int damage = random.Next(3, 7);
            CurrentHP = CurrentHP + damage;
            description = "Steal 3 to 6 HP from your foe.";
            if (!justDescription)
                AttackEffects(actors, name, damage);
        }

        public void Unraveling(List<Actor> actors, bool justDescription, out string description)
        {
            description = "Unravel the fabric of existence around your foe for either 5 or 8 damage.";
            Random random = new Random();
            string name = "Unraveling";
            int damage;
            if (random.Next(2) == 0)
                damage = 5;
            else
                damage = 8;
            if (!justDescription)
                AttackEffects(actors, name, damage);
        }

        virtual public void Rest(List<Actor> actors, bool justDescription, out string description)
        {
            description = "Forego attacking this turn to restore between 1 and 4 HP.";
            if (!justDescription)
            {
                Random random = new Random();
                Console.WriteLine("");
                switch (random.Next(4))
                {
                    case 0:
                        DisplayName();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(" is watching carefully...\n");
                        break;
                    case 1:
                        DisplayName();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(" is Resting...\n");
                        break;
                    case 2:
                        DisplayName();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(" is resting right now...\n");
                        break;
                    case 3:
                        DisplayName();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(" is assessing the battlefield...\n");
                        break;
                }

                int healthRestore = random.Next(101);
                int restoreFigure = 0;
                if (healthRestore <= 33) { CurrentHP++; restoreFigure = 1; }
                else if (healthRestore <= 66) { CurrentHP = CurrentHP + 2; restoreFigure = 2; }
                else if (healthRestore <= 99) { CurrentHP = CurrentHP + 3; restoreFigure = 3; }
                else if (healthRestore == 100) { CurrentHP = CurrentHP + 4; restoreFigure = 4; }

                DisplayName();
                Console.WriteLine($" restored {restoreFigure} health!");

                if (CurrentHP > MaxHP) CurrentHP = MaxHP;

                Console.ForegroundColor = ConsoleColor.White;
                PressAnyKeyPrompt();
            }
        }
        virtual public void DoNothing(List<Actor> actors, bool justDescription, out string description)
        {
            description = " ...";
            if(!justDescription) { Console.WriteLine("Treasure Masochist chose to abstain from combat."); }
        }
        virtual public void WinTheGame(List<Actor> actors, bool justDescription, out string description)
        {
            description = "Instantly win the game.";
        }
        virtual public void Transcend(List<Actor> actors, bool justDescription, out string description)
        {
            description = "Achieve enlightenment.";
        }



    }
}
