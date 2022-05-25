using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiWF
{
    public abstract class Gracz
    {

        public List<Statek> statki;
        public List<Statek> statkiTemp;
        public Plansza MojeStatki;
        public Plansza MojeStrzaly;
        
        public Gracz()
        {
            statki=new List<Statek>();
            MojeStatki=new Plansza(10);
            MojeStrzaly=new Plansza(10);
            statkiTemp=new List<Statek>();
            int[] tab = new int[] { 2, 2, 3, 3, 4, 4, 6 };
            for (int i=0;i<tab.Length;i++)
            {
                Statek s = new Statek(tab[i]);
                statkiTemp.Add(s);
            }
        }
        public bool CzyPrzegralem()
        {
            foreach (Statek statek in statki)
            {
                for (int i = 0; i < statek.rozmiar; i++)
                {
                    if (statek.maszty[i] == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void ZniszczStatek(int x,int y)
        {
            if (MojeStrzaly.mapa[x, y].s != null)
            {
                if (MojeStrzaly.mapa[x, y].s.x == x)
                {
                    MojeStrzaly.mapa[x, y].s.maszty[x - MojeStrzaly.mapa[x, y].s.x] = false;
                }
                else if (MojeStrzaly.mapa[x, y].s.y == y)
                {
                    MojeStrzaly.mapa[x, y].s.maszty[y - MojeStrzaly.mapa[x, y].s.y] = false;
                }
            }
        }

    }
}
