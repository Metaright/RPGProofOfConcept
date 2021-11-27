using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalBattle
{
    partial class Actor
    {
        public int BigBoyFists(int inputDamage, out string message) 
        { 
            message = "Super fists of fury grant an extra point of damage for each attack.";
            return (inputDamage + 1); 
        }
        public int FieryFists(int inputDamage, out string message) 
        {
            message = "Disregard your own pain and deal an extra 2 points of damage for each attack.";
            return (inputDamage + 2); 
        }
        public int UnholyGamble(int inputDamage, out string message)
        {
            message = "Your odds of dealing nothing are 5-in-6, but you may also deal 4 times as much damage!";
            Random random = new Random();
            int damageRoll = random.Next(7);
            if(damageRoll == 0) { return inputDamage * 4; }
            else { return 0; }
        }
    }
}
