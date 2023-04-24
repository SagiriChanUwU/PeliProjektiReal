using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliProjekti
{
    internal class AttackAction
    {
        public Units attacker;
        public Units defender;


        public AttackAction(Units attacker, Units defender)
        {
            this.attacker = attacker;
            this.defender = defender;
        }

        public void Undo()
        {
            defender.hp += attacker.dmg;
        }
    }

}
