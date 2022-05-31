using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiWF
{
    public enum kierunek
    {
        PIONOWO,
        POZIOMO
    }
    public class Statek
    {
        public kierunek k { get; set; }
        public byte x { get; set; }
        public byte y { get; set; }
        public int xPx { get; set; }
        public int yPy { get; set; }
        public int rozmiar { get; set; }
        public bool[] maszty { get; set; }
        public bool czyRysowacStatek = false;
        public Statek(int size)
        {
            rozmiar= size;
            maszty = new bool[size];
            for (int i = 0; i < size; i++)
            {
                maszty[i] = true;
            }
            k = kierunek.POZIOMO;
        }
        public Statek(byte x,byte y,kierunek k,int size)
        {

            this.x = x;
            this.y = y;
            rozmiar = size;
            maszty = new bool[size];
            this.k= k;
            for(int i = 0; i < size; i++)
            {
                maszty[i] = true;
            }
        }
        public void NarysujStatek(PaintEventArgs e)
        {
            if (czyRysowacStatek == true)
            {
                Graphics k = e.Graphics;
                Brush b = new SolidBrush(Color.Black);
                Pen p = new Pen(Color.Black);
                for (int j = 0; j < rozmiar; j++)
                {

                    k.FillRectangle(b, new Rectangle((int)xPx + j * 30, (int)yPy, 30, 30));
                    k.DrawRectangle(p, xPx + j * 30, yPy, 30, 30);

                }
            }
        }
        public bool CzyKlikniety(MouseEventArgs e)
        {

            int x = e.X;
            int y = e.Y;

            if (x >= xPx && x <= xPx+30*rozmiar && y >= yPy && y <= yPy+30)
            {
                return true;
            }
            return false;
        }
        public void ZmienReczniePolozenie(MouseEventArgs e)
        {
            xPx = e.X;
            yPy = e.Y;
        }
    }
}
