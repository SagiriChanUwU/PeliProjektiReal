using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliProjekti
{
    public class Units
    {
        public int dmg = 0;
        public string name;
        public int hp = 0;

        public Units(string name, int hp, int dmg)  
        {
            this.name = name;
            this.hp = hp;
            this.dmg = dmg;
        }


        public void PrintUnit()
        {

        }
    }
}
