using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalBattle
{
    partial class Actor
    {
        public int Masochism(int inputDamage, out string description)
        {
            description = "Craving an end to the existential pain, you receive between 2 and 5 times more damage.";
            Random random = new Random();
            int damage = random.Next(4);
            if (damage == 0) { return inputDamage * 2; }
            else if (damage == 1) { return inputDamage * 3; }
            else if (damage == 2) { return inputDamage * 4; }
            else { return inputDamage * 5; }
        }
        public int Slimy(int inputDamage, out string description)
        {
            description = "You are coated in a viscous fluid, and take either 1 damage less (or more!).";
            Random random = new Random();
            int damage = random.Next(2);
            if (damage == 0) { return inputDamage - 1; }
            else { return inputDamage + 1; }
        }
        public int StoneArmor(int inputDamage, out string description) 
        {
            description = "With nerves of steel and bones of stone, incoming damage is reduced by 1 point.";
            return (inputDamage - 1); 
        }
        public int SteelArmor(int inputDamage, out string description) 
        {
            description = "One-size-fits-all armor reduces incoming damage by 3 points.";
            return (inputDamage - 3); 
        }
    }
}
