namespace StatkiWF
{
    public partial class Form1 : Form
    {

        public Manager manager=new Manager();
        Strzal strzal=new Strzal();
        bool flaga = false;
        StanGry stanGry= new StanGry();
        bool flagb = false;
        int rozmiar=24;
        int iloscTrafienGracza=0;
        int iloscTrafienBota=0;
        bool czyRysowacStatki = false;
        bool flagaCzyKliknietyStatek = false;
        int indexStatku=-1;
        public Form1()
        {

            InitializeComponent();
        }

        private void NarysujStatki(PaintEventArgs e, int xPos,int yPos,bool test)
        {
            int x = xPos;
            int y = yPos;
            Graphics k = e.Graphics;
            Brush b = new SolidBrush(Color.Black);
            Pen p = new Pen(Color.Black);
            
            

            for (int i = 0; i < manager.player1.statkiTemp.Count; i++)
            {
                if (!test)
                {
                    manager.player1.statkiTemp[i].xPx = x;
                    manager.player1.statkiTemp[i].yPy = y;
                }
                manager.player1.statkiTemp[i].NarysujStatek(e);
                y += 40;
                x = xPos;
            }
            pictureBox1.Invalidate();

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            manager.player1.MojeStatki.NarysujPlansze(e, 10, 90);
            manager.player1.MojeStrzaly.NarysujPlansze(e, 400, 90);
            stanGry = StanGry.RuchGracza;
            if (czyRysowacStatki == true)
            {
                NarysujStatki(e, 10, 400,false);
                czyRysowacStatki = false;
             
            }
            else
                NarysujStatki(e,0,0,true);
                //pictureBox1.Invalidate();
           
        }
        private int  KlikanietyStatek(MouseEventArgs e)
        {
            for(int i=0;i< manager.player1.statkiTemp.Count;i++)
            {
                if(manager.player1.statkiTemp[i].CzyKlikniety(e)==true)
                {
                    flagaCzyKliknietyStatek = true;
                    return i;
                }
            }
            return -1;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            indexStatku = KlikanietyStatek(e);
            

            if (flagb == false)
            {
                if (stanGry == StanGry.RuchGracza)
                {
                    flaga = manager.player1.MojeStrzaly.CzyKliknietaPlansza(e, 400, 90);

                    if (flaga == true)
                    {
                        int x = (e.X - 400) / 30;
                        int y = (e.Y - 90) / 30;
                        Pole pole = manager.player2.MojeStatki.SprawdzPoleMapy(y, x);
                        manager.player1.MojeStrzaly.ZaznaczNaMojejMapie(y, x, pole);
                        if (pole == Pole.TRAFIONY)
                        {
                            iloscTrafienGracza++;
                            if (iloscTrafienGracza == rozmiar)
                            {
                                flagb = true;
                            }
                        }
                       
                        stanGry = StanGry.RuchBota;

                    }
                }
                if (stanGry == StanGry.RuchBota)
                {
                    Random rand = new Random();
                    int x = rand.Next(0, manager.player1.MojeStatki.wymiar);
                    int y = rand.Next(0, manager.player1.MojeStatki.wymiar);
                    Pole pole = manager.player1.MojeStatki.SprawdzPoleMapy(y, x);
                    manager.player1.MojeStatki.ZaznaczNaMojejMapie(y, x, pole);
                    if (pole == Pole.TRAFIONY)
                    {
                        iloscTrafienBota++;
                        if (iloscTrafienBota == rozmiar)
                        {
                            flagb = true;
                        }
                    }
                    stanGry = StanGry.RuchGracza;
                }
            }
            if(flagb==true)
            {
                MessageBox.Show("Koniec");
            }
            pictureBox1.Invalidate();
        }
        private void DostawLosowoStatki(Gracz p)
        {
            Random random = new Random();
            int kier;
            byte x;
            byte y;
            bool warunek = true;
            int wymiar = p.MojeStatki.wymiar;
            int[] tab = new int[] { 2, 2, 3, 3, 4, 4, 6 };
            for (int i = 0; i < tab.Length; i++)
            {
                while (warunek)
                {
                    x = (byte)random.Next(0, wymiar);
                    kier = random.Next(2);
                    y = (byte)random.Next(0, wymiar);
                    Statek s = new(x, y, (kierunek)kier, tab[i]);
                    if (p.MojeStatki.CzyMoznaDostawicStatek(s) == true)
                    {
                        p.MojeStatki.DostawStatek(s);
                        p.statki.Add(s);
                        warunek = false;
                        
                    }
                }
                warunek = true;
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DostawLosowoStatki(manager.player1);
            DostawLosowoStatki(manager.player2);
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            czyRysowacStatki = true;
            
            pictureBox1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(indexStatku!=-1 && flagaCzyKliknietyStatek==true)
            {
                manager.player1.statkiTemp[indexStatku].xPx = Convert.ToByte(((e.X) / 30))*30+10;
                manager.player1.statkiTemp[indexStatku].yPy = Convert.ToByte(((e.Y) / 30))*30;
                if(e.Button==MouseButtons.Right)
                {
                    if(manager.player1.statkiTemp[indexStatku].k==kierunek.PIONOWO)
                    {
                        manager.player1.statkiTemp[indexStatku].k = kierunek.POZIOMO;
                    }
                    else
                    {
                        manager.player1.statkiTemp[indexStatku].k = kierunek.PIONOWO;
                    }
                }


            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            manager.player1.statkiTemp[indexStatku].x= Convert.ToByte(((e.X - 10) / 30));
            manager.player1.statkiTemp[indexStatku].y = Convert.ToByte(((e.Y - 90) / 30));

            flagaCzyKliknietyStatek = false;
            pictureBox1.Invalidate();
            //pictureBox1.Invalidate();
        }
    }
}