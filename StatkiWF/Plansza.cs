using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiWF
{
    public enum Pole
    {
        STRZELONY,
        PUSTY='_',
        TRAFIONY='x',//strzelony i trafiony
        OBECNY='s', //znaczy ze jest statek
        PUDLO='n', //strzelony w puste miejsce
        ZAJETY='z', //juz wczesniej ktos strzelil i nie mozna jeszcze raz strzelac
    }

    public class Plansza
    {
        public PolePlanszy[,] mapa;
        public int wymiar;
        public Plansza(int wymiar)
        {
            this.wymiar = wymiar;
            mapa= new PolePlanszy[wymiar,wymiar];
            for(int i=0;i<wymiar;i++)
            {
                for(int j=0;j<wymiar;j++)
                {
                    mapa[i, j] = new PolePlanszy();
                    mapa[i, j].pole = Pole.PUSTY;
                    mapa[i, j].s = null;
                }
            }
            Wyczysc();
        }
        public bool CzyKliknietaPlansza(MouseEventArgs e, float xPos, float yPos)
        {
            if (e.X < xPos || e.Y <yPos || e.X>xPos+300 || e.Y>yPos+300)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public void NarysujPlansze(PaintEventArgs e, float xPos, float yPos)
        {
            float x = xPos;
            float y = yPos;
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            for (int i = 0; i < wymiar; i++)
            {
                for(int j=0;j<wymiar;j++)
                {
                    mapa[i,j].NarysujPolePlanszy(e,x,y);
                    x += 30;
                }
                y += 30;
                x = xPos;
            }

        }
        public void Wyczysc()
        {
            for(int i=0;i<wymiar;i++)
            {
                for (int j = 0; j < wymiar; j++)
                {
                    mapa[i, j].pole = Pole.PUSTY;
                    mapa[i, j].s = null;
                }
            }
        }
        public bool CzyMoznaDostawicStatek(Statek S)
        {
            byte x = S.x;
            byte y = S.y;
            kierunek k = S.k;
            int rozmiar = S.rozmiar;
            if (k == kierunek.POZIOMO)
            {
                if (x < 0 || x >= wymiar || y < 0 || y + rozmiar > wymiar)
                {
                    return false;
                }
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j < y + rozmiar + 1; j++)
                    {
                        if (i >= 0 && i < wymiar && j >= 0 && j < this.wymiar)
                        {
                            if (mapa[i, j].pole != Pole.PUSTY) return false;
                        }
                    }
                }
            }
            else
            {
                if (x < 0 || x + rozmiar > wymiar || y < 0 || y >= wymiar)
                {
                    return false;
                }
                for (int i = x - 1; i <= x + rozmiar + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i <wymiar && j >= 0 && j < wymiar)
                        {
                            if (mapa[i, j].pole != Pole.PUSTY) return false;
                        }
                    }
                }
            }
           
            return true;
        }
        public void DostawStatek(Statek S)
        {
            byte x = S.x;
            byte y = S.y;
            kierunek k = S.k;
            int rozmiar = S.rozmiar;
            if (k == kierunek.POZIOMO)
            {
                for (int i = 0; i < rozmiar; i++)
                {
                    mapa[x, y + i].pole = Pole.OBECNY;
                    mapa[x, y + i].color = Color.Black;
                    mapa[x, y + i].s = S;
                }
            }
            else
            {
                for (int i = 0; i < rozmiar; i++)
                {
                    mapa[x + i, y].pole = Pole.OBECNY;
                    mapa[x + i, y].color = Color.Black;
                    mapa[x+i, y].s = S;

                }
            }
        }

        public Pole SprawdzPoleMapy(int x,int y)
        {
            if (mapa[x, y].pole == Pole.PUSTY)
            {
                mapa[x, y].pole = Pole.PUDLO;

            }
            if (mapa[x, y].pole == Pole.PUDLO || mapa[x, y].pole == Pole.TRAFIONY)
            {
                mapa[x, y].pole = Pole.PUDLO;

            }
            if (mapa[x, y].pole == Pole.ZAJETY)
            {
                mapa[x, y].pole = Pole.PUDLO;

            }
            if (mapa[x, y].pole == Pole.OBECNY)
            {
                mapa[x, y].pole = Pole.TRAFIONY;

            }
            return mapa[x, y].pole;
        }
        public void ZaznaczNaMojejMapie(int x,int y,Pole pole)
        {
            if(pole==Pole.PUDLO)
            {
                mapa[x,y].pole=Pole.PUDLO;
                mapa[x, y].color = Color.Red;
            }
            if(pole==Pole.TRAFIONY)
            {
                mapa[x, y].pole = Pole.TRAFIONY;
                mapa[x, y].color = Color.Green;
            }
            
        }
        public void AtakNaMape(int x,int y)
        {
            if(mapa[x,y].pole != Pole.OBECNY)
            {
                mapa[x, y].pole = Pole.PUDLO;
                mapa[x,y].color= Color.Red;
            }
            if(mapa[x,y].pole==Pole.PUDLO || mapa[x,y].pole==Pole.TRAFIONY)
            {
                return;
            }
            else
            {
                mapa[x,y].pole = Pole.TRAFIONY;
                mapa[x,y].color = Color.Green;
            }
        }

    }
}
