using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiWF
{
    public class Strzal
    {
        public int x;
        public int y;
        public Pole statusPola;
        public Strzal() : base() { }
        public Strzal(int x,int y,Pole statusPola)
        {
            this.x = x;
            this.y = y;
            this.statusPola = statusPola;
        }
    }
}
