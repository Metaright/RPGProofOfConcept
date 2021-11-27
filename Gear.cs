using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalBattle
{
    public class Gear
    {
        public string Name { get; set; }
        public string AttackName { get; set; }
        public string DefensiveModifierName { get; set; }
        public string OffensiveModifierName { get; set; }
        public string Description { get; init; }
        public bool Equipped { get; set; } = false;
        public Gear() { }
        virtual public void Equip(List<Actor> actors, Actor user) { }
        virtual public void EquipAtBeginning(List<Actor> actors, Actor user) { }
        virtual public void Unequip(List<Actor> actors, Actor user) { }
    }
}
