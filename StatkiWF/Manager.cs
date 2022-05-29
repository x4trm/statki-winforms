using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiWF
{

    public class Manager
    {
        public Gracz player1;
        public Gracz player2;
        //public Gracz? winner;
        public Manager()
        {
            player1 = new Czlowiek();
            player2 = new Bot();
        }

       
    }
}
