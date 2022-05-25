using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiWF
{
    public class PolePlanszy
    {
        public Pole pole;
        public Statek s;
        public Color color;

        public void NarysujPolePlanszy(PaintEventArgs e, float xPos,float yPos)
        {

            float x=xPos;
            float y=yPos;
            Graphics k = e.Graphics;
            Brush b=new SolidBrush(color);
            Pen p = new Pen(Color.Black);
            k.FillRectangle(b, new Rectangle((int)x, (int)y, 30, 30));
            k.DrawRectangle(p,x, y, 30, 30);
           

        }


    }
    
}
