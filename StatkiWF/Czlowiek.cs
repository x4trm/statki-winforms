using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiWF
{
    public class Czlowiek : Gracz
    {
        public Czlowiek() : base() { }



/*        public override Strzal WykonajStrzal(MouseEventArgs e)
        {
            int x = (e.X - 400) / 30;
            int y = (e.Y - 90) / 30;
            Strzal strzal = new Strzal(y,x,Pole.STRZELONY);
            return strzal;
        }*/

        public override string ToString()
        {
            return "Czlowiek";
        }


    }
}

