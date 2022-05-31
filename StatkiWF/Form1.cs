namespace StatkiWF
{
    public enum StanGry
    {
        RuchGracza,
        RuchBota,
        WyborTrybuGry,
        RozstawienieStatkow,
        KoniecGry
    }
    public partial class Form1 : Form
    {

        public Manager manager = new Manager();
        Strzal strzal = new Strzal();
        bool flaga = false;
        StanGry stanGry = new StanGry();
        bool czyKoniecGry = false;
        int rozmiar = 24;
        int iloscTrafienGracza = 0;
        int iloscTrafienBota = 0;
        bool czyRysowacStatki = false;
        bool flagaCzyKliknietyStatek = false;
        int indexStatku = -1;
        public Form1()
        {
            stanGry = StanGry.WyborTrybuGry;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (stanGry != StanGry.WyborTrybuGry)
                return;
            else
            {
                for(int i=0;i<manager.player1.statkiTemp.Count;i++)
                {
                    manager.player1.statkiTemp[i].czyRysowacStatek = true;
                }
                stanGry = StanGry.RozstawienieStatkow;
            }
            flota.Invalidate();

        }
        private void NarysujStatki(PaintEventArgs e, int xPos, int yPos, bool test)
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
            flota.Invalidate();

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            manager.player1.MojeStatki.NarysujPlansze(e, 10, 90);
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
                        czyKoniecGry = true;
                        manager.winner = "Bot";
                    }
                }
                stanGry = StanGry.RuchGracza;

            }
            if(stanGry==StanGry.RozstawienieStatkow)
            {
                NarysujStatki(e, 10, 400, false);
            }
            if (czyKoniecGry == true)
            {
                if(manager.winner=="Bot")
                {
                    MessageBox.Show("Przegrales");
                }

                MessageBox.Show("Wygrales");
                czyKoniecGry = false;
                stanGry = StanGry.KoniecGry;
            }
            flota.Invalidate();

        }
        private void strzaly_Paint(object sender, PaintEventArgs e)
        {
            manager.player1.MojeStrzaly.NarysujPlansze(e, 10, 90);
        }
        private int KlikanietyStatek(MouseEventArgs e)
        {
            for (int i = 0; i < manager.player1.statkiTemp.Count; i++)
            {
                if (manager.player1.statkiTemp[i].CzyKlikniety(e) == true)
                {
                    flagaCzyKliknietyStatek = true;
                    return i;
                }
            }
            return -1;
        }
        private void strzaly_MouseDown(object sender, MouseEventArgs e)
        {
            if (stanGry == StanGry.RuchGracza)
            {
                flaga = manager.player1.MojeStrzaly.CzyKliknietaPlansza(e, 10, 90);

                if (flaga == true)
                {
                    int x = (e.X - 10) / 30;
                    int y = (e.Y - 90) / 30;
                    Pole pole = manager.player2.MojeStatki.SprawdzPoleMapy(y, x);
                    manager.player1.MojeStrzaly.ZaznaczNaMojejMapie(y, x, pole);
                    if (pole == Pole.TRAFIONY)
                    {
                        iloscTrafienGracza++;
                        if (iloscTrafienGracza == rozmiar)
                        {
                            czyKoniecGry = true;
                        }
                    }

                    stanGry = StanGry.RuchBota;

                }
            }

            strzaly.Invalidate();
            
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
            if (stanGry != StanGry.WyborTrybuGry) return;
            else
            {
                DostawLosowoStatki(manager.player1);
                DostawLosowoStatki(manager.player2);
                stanGry = StanGry.RuchGracza;
            }
            flota.Invalidate();
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

              indexStatku = KlikanietyStatek(e);


        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (indexStatku != -1 && flagaCzyKliknietyStatek == true)
            {
                manager.player1.statkiTemp[indexStatku].xPx = (int)((e.X) / 30) * 30 + 10;
                manager.player1.statkiTemp[indexStatku].yPy = (int)((e.Y) / 30) * 30;

            }
            flota.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            byte x;
            byte y;
            if (stanGry != StanGry.RozstawienieStatkow)
                return;
            if (e.X < 310 && e.X > 10 && e.Y > 90 && e.Y < 390)
            {
                x = Convert.ToByte(((e.X - 10) / 30));
                y = Convert.ToByte(((e.Y - 90) / 30));
                //Statek s = new Statek(y, x, manager.player1.statkiTemp[indexStatku].k, rozmiar);
                manager.player1.statkiTemp[indexStatku].x = y;
                manager.player1.statkiTemp[indexStatku].y = x;
                if (manager.player1.MojeStatki.CzyMoznaDostawicStatek(manager.player1.statkiTemp[indexStatku]) == true)
                {
                    manager.player1.MojeStatki.DostawStatek(manager.player1.statkiTemp[indexStatku]);
                    manager.player1.statki.Add(manager.player1.statkiTemp[indexStatku]);
                    manager.player1.statkiTemp[indexStatku].czyRysowacStatek = false;
                    manager.player1.statkiTemp[indexStatku].xPx = 3000;
                    manager.player1.statkiTemp[indexStatku].yPy = 3000;
                    manager.player1.statkiTemp.Remove(manager.player1.statkiTemp[indexStatku]);

                }
                flagaCzyKliknietyStatek = false;
                indexStatku = -1;
                
            }
            if(manager.player1.statkiTemp.Count==0)
            {
                DostawLosowoStatki(manager.player2);
                //MessageBox.Show("Zacznij strzelac");
                stanGry = StanGry.RuchGracza;
            }
            flota.Invalidate();
        }


    }
}