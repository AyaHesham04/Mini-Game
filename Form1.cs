using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameMini
{
    class CActorBack
    {
        public Rectangle rcSrc;
        public Rectangle rcDst;
        public Bitmap im;

    }

    public class CActorSeaWeed
    {
        public int X, Y, W, H;
        public List<Bitmap> imgs;
        public int iFrame;
    }

    public class CActorRocks
    {
        public int X, Y, W, H;
        public Bitmap im;

    }

    public class CActorBubbles
    {
        public int X, Y, W, H;
        public Bitmap im;

    }

    public class CActorFish
    {
        public int X, Y, W, H;
        public List<Bitmap> imgs;
        public int iFrameF;
        public int speed;
        public int dirX;
        public int coins;

    }
    public class CActorLife
    {
        public int X, Y, W, H;
        public List<Bitmap> imgs;
        public int iFrame;

    }

    public class CActorWarning
    {
        public int X, Y, W, H;
        public List<Bitmap> imgs;
        public int iFrame;

    }

    class CActorHorseFish
    {
        public int X, Y, W, H;
        public Bitmap im;

    }

    class CActorBoat
    {
        public int X, Y, W, H;
        public Bitmap im;

    }

    public class CActorHook
    {
        public int X, Y, W, H;
        public Bitmap im;

    }

    public class CActorPlay
    {
        public int X, Y, W, H;
        public Bitmap im;

    }

    public class CActorArrows
    {
        public int X, Y, W, H;
        public Bitmap im;

    }
    public class CActorMoveCoin
    {
        public int X, Y, W, H;
        public Bitmap im;

    }

    public class CActorText
    {
        public int X, Y;
        public int W, H;
        public Color cl;
        public string text;
        public List<Bitmap> imC;
        public int iFrameC;
        public Bitmap im;
    }

    public class CActorFinal
    {
        public int X, Y, W, H;
        public Bitmap im;
        public string score;
        public string text1;

    }

    public partial class Form1 : Form
    {
        Bitmap off;
        Timer tt = new Timer();
        int YScroll = 0, p = 0, a=0, down = 0, up = 0, end = 0, kill = 0, l = 0, scrollUp = 10;
        int ctF = 0, xF = 0, fish = 1, catchF = 0;
        int colCoins = 0, ctC = 0;
        int ctTick = 0, ctS = 0, ctH = 0, ctW = 0;
        bool keyLeft = false, keyRight = false, keySpace = false;
        int hookSpeed = 5, originalX = -1;
        int touchDanger = 0, dangerF=-1;

        List<CActorBack> back = new List<CActorBack>();
        List<CActorPlay> play = new List<CActorPlay>();
        List<CActorArrows> arrows = new List<CActorArrows>();


        List<CActorBoat> boat = new List<CActorBoat>();
        List<CActorHook> hook = new List<CActorHook>();

        List<CActorFish> LFish = new List<CActorFish>();
        List<CActorFish> hookFish = new List<CActorFish>();
        List<CActorFish> colFish = new List<CActorFish>();
        List<CActorFish> dangerousFish = new List<CActorFish>();


        List<CActorSeaWeed> Lseaweed = new List<CActorSeaWeed>();
        List<CActorRocks> rocks = new List<CActorRocks>();
        List<CActorHorseFish> horseFish = new List<CActorHorseFish>();

        List<CActorBubbles> bubbles = new List<CActorBubbles>();

        List<CActorText> Ltext = new List<CActorText>();
        List<CActorMoveCoin> moveCoin = new List<CActorMoveCoin>();
        List<CActorLife> life = new List<CActorLife>();
        List<CActorWarning> warning = new List<CActorWarning>();


        List<CActorFinal> final = new List<CActorFinal>();


        public Form1()
        {
            this.Size = new Size(600, 700);
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            tt.Interval = 100; 
            tt.Start();
            tt.Tick += Tt_Tick;

        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if (keyRight)
            {
                if (hook[0].X < this.ClientSize.Width - 50)
                {
                    hook[0].X += hookSpeed;
                }
                for (int i = 0; i < hookFish.Count; i++)
                {
                    hookFish[i].X = hook[0].X - 20 + (2 * i);
                }
            }


            if (keyLeft)
            {
                if (hook[0].X > 50)
                {
                    hook[0].X -= hookSpeed;
                }
                for (int i = 0; i < hookFish.Count; i++)
                {
                    hookFish[i].X = hook[0].X - 20 + (2 * i);
                }
            }

            if (keySpace)
            {
                l = 0;
                colFish.Clear();
                final.Clear();
                colCoins = 0;
                Ltext[0].text = colCoins.ToString();
                YScroll = 0;
                if (end == 0)
                {
                    down = 1;
                }

                p = 1;
                
            }

            if (down == 1)
            {
                YScroll += 35;

                if (YScroll < 0)
                {
                    YScroll += 35;
                }
                else
                {
                    if ((YScroll + this.ClientSize.Height < (back[0].im.Height) - 500))
                    {
                        back[0].rcSrc.Y = YScroll;

                    }
                    else
                    {
                        YScroll -= 10;
                        up = 1;
                        down = 0;
                        catchF = 1;
                        a++;
                    }
                }

            }

            if (up == 1)
            {
                YScroll -= scrollUp;

                if (YScroll < 0)
                {
                    YScroll = 0;
                    up = 0;

                    end = 1;
                }
                else
                {
                    back[0].rcSrc.Y = YScroll;
                    arrows[0].Y = hook[0].Y + 50;
                }
            }

            if (ctTick % 5 == 0)
            {
                CreateFish();
                CreateBubbles();
            }
            if (ctTick % 3 == 0)
            {
                CreateDangerousFish();
            }
            MoveFish();
            MoveHorseFish();

            if (catchF == 1)
            {
                CheckHook();
                CheckDangerFish();

            }

            if (end == 1 && hookFish.Count > 0)
            {
                if (ctTick % 10 == 0)
                {
                    AddCol();
                }
            }

            if (end == 1 && hookFish.Count == 0 && ctTick % 25 == 0)
            {
                CreateFinal();
            }

            if (touchDanger == 1)
            {
                dangerousFish[dangerF].W += 10;
                dangerousFish[dangerF].H += 10;
                scrollUp = 2;
                
                if (dangerousFish[dangerF].W == 200 && dangerousFish[dangerF].H == 200)
                {
                    kill++;
                    dangerousFish.RemoveAt(dangerF);
                    hook[0].X = originalX;
                    for (int j = 0; j < hookFish.Count; j++)
                    {
                        hookFish[j].X = hook[0].X - 20 + (2 * j);
                    }

                    touchDanger = 0;
                    dangerF = -1;
                    scrollUp = 10;

                }
            }

            UpdateLife();
            if (warning.Count > 0)
            {
                MoveWarning();
            }
            MoveCoin();
            MoveSeaWeed();
            MoveBubbles();
            MoveFishCoin();

            ctTick++;

            DrawDubb(this.CreateGraphics());

        }

        void AddCol()
        {
            colFish.Clear(); //to remove the previous fish when a new fish appears

            CActorFish pnn = new CActorFish();
            pnn.X = 250;
            pnn.Y = 200;
            pnn.W = 100;
            pnn.H = 100;
            pnn.imgs = new List<Bitmap>();

            for (int k = 0; k < 2; k++)
            {
                Bitmap im = new Bitmap(hookFish[hookFish.Count - 1].imgs[k]);
                im.MakeTransparent();
                pnn.imgs.Add(im);
            }

            pnn.iFrameF = 0;
            pnn.dirX = 0;
            pnn.coins = hookFish[hookFish.Count - 1].coins;
            colFish.Add(pnn);
            hookFish.RemoveAt(hookFish.Count - 1);

            CreateMoveCoin();
            UpdateText(pnn);
        }
      
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    keyRight = true;
                    a+=2;
                    break;

                case Keys.Left:
                    keyLeft = true;
                    a+=2;
                    break;

                case Keys.Space:
                    keySpace = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    keyRight = false;
                    hook[0].X = originalX;
                    for (int i = 0; i < hookFish.Count; i++)
                    {
                        hookFish[i].X = hook[0].X - 20 + (2 * i);

                    }
                    break;

                case Keys.Left:
                    keyLeft = false;
                    hook[0].X = originalX;
                    for (int i = 0; i < hookFish.Count; i++)
                    {
                        hookFish[i].X = hook[0].X - 20 + (2 * i);

                    }
                    break;

                case Keys.Space:
                    keySpace = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(450, 50);
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            CreateBack();
            CreateBoat();
            CreateHook();
            CreateText();
            CreateSeaWeed();
            CreatePlay();
            CreateArrows();
            CreateRocks();
            CreateHorseFish();
            CreateDangerousFish();
            CreateLife();
            CreateWarning();

        }

        void CreatePlay()
        {
            CActorPlay pnn = new CActorPlay();
            pnn.X = 190;
            pnn.Y = 250;
            pnn.W = 200;
            pnn.H = 130;
            pnn.im = new Bitmap("play.png");

            play.Add(pnn);
        }

        void CreateArrows()
        {
            CActorArrows pnn= new CActorArrows();
            pnn.X = 200;
            pnn.Y = 250;
            pnn.W = 100;
            pnn.H = 40;
            pnn.im = new Bitmap("arrows.png");

            arrows.Add(pnn);
        }
        void CreateText()
        {
            CActorText pnn = new CActorText();
            pnn.text = colCoins.ToString();
            pnn.X = 35;
            pnn.Y = 40;
            pnn.W = 120;
            pnn.H = 60;
            pnn.im = new Bitmap("coins.png");

            pnn.cl = Color.RoyalBlue;

            pnn.imC = new List<Bitmap>();
            for (int i = 0; i < 3; i++)
            {
                Bitmap im = new Bitmap("c" + (i + 1) + ".png");
                im.MakeTransparent();
                pnn.imC.Add(im);
            }
            pnn.iFrameC = 0;

            Ltext.Add(pnn);
        }

        void UpdateText(CActorFish ptrav)
        {
            colCoins += ptrav.coins;
            Ltext[0].text = colCoins.ToString();
        }

        void CreateBack()
        {
            CActorBack pnn = new CActorBack();
            pnn.im = new Bitmap("back.png");
            pnn.rcDst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            pnn.rcSrc = new Rectangle(0, YScroll, this.ClientSize.Width, this.ClientSize.Height);

            back.Add(pnn);

        }

        void CreateBoat()
        {
            CActorBoat pnn = new CActorBoat();
            pnn.im = new Bitmap("boat.png");
            pnn.X = 0;
            pnn.Y = 350;
            pnn.W = 250;
            pnn.H = 200;

            boat.Add(pnn);
        }

        void CreateHook()
        {
            CActorHook pnn = new CActorHook();
            pnn.X = 250;
            pnn.Y = 400;
            pnn.W = 15;
            pnn.H = 30;
            pnn.im = new Bitmap("hook.png");
            originalX = 250;

            hook.Add(pnn);
        }

        void CheckHook()
        {
            for (int i = 0; i < LFish.Count; i++)
            {
                if (hook[0].X >= LFish[i].X && hook[0].X <= LFish[i].X + LFish[i].W && hook[0].Y + hook[0].H / 2 >= LFish[i].Y - YScroll && hook[0].Y + hook[0].H / 2 <= LFish[i].Y - YScroll + LFish[i].H)
                {
                    CActorFish pnn = new CActorFish();
                    pnn.W = LFish[i].W;
                    pnn.H = LFish[i].H + 5;
                    pnn.X = hook[0].X - 25 + xF;
                    xF += 2;
                    pnn.Y = hook[0].Y + hook[0].H - 10;
                    pnn.imgs = new List<Bitmap>();

                    for (int k = 0; k < 2; k++)
                    {
                        Bitmap im = LFish[i].imgs[k];
                        im.MakeTransparent();
                        pnn.imgs.Add(im);
                    }

                    pnn.iFrameF = 1;
                    pnn.dirX = 0;
                    pnn.coins = LFish[i].coins;
                    hookFish.Add(pnn);
                    LFish.RemoveAt(i);
                }

            }
        }

        void CreateFish()
        {
            Random rr = new Random();

            CActorFish pnn = new CActorFish();
            pnn.Y = rr.Next(660, this.ClientSize.Height + YScroll);
            pnn.W = 50;
            pnn.H = 50;
            pnn.imgs = new List<Bitmap>();

            fish++;

            if (fish == 6)
            {
                fish = 1;
            }

            for (int i = 0; i < 2; i++)
            {
                if (fish % 2 == 0)
                {
                    pnn.X = 0;
                    if (i == 0)
                    {
                        Bitmap im = new Bitmap("f" + (fish) + ".png");
                        im.MakeTransparent();
                        pnn.imgs.Add(im);
                    }
                    else if (i == 1)
                    {
                        Bitmap im = new Bitmap("f" + (fish) + "u.png");
                        im.MakeTransparent();
                        pnn.imgs.Add(im);
                    }

                    pnn.dirX = 1;

                }
                else if (fish % 2 != 0)
                {
                    pnn.X = this.ClientSize.Width;

                    if (i == 0)
                    {
                        Bitmap im = new Bitmap("f" + (fish) + ".png");
                        im.MakeTransparent();
                        pnn.imgs.Add(im);
                    }
                    else if (i == 1)
                    {
                        Bitmap im = new Bitmap("f" + (fish) + "u.png");
                        im.MakeTransparent();
                        pnn.imgs.Add(im);
                    }

                    pnn.dirX = -1;

                }

            }
            pnn.iFrameF = 0;
            pnn.speed = rr.Next(5, 15);
            pnn.coins = rr.Next(10, 20);
            LFish.Add(pnn);

        }

        void MoveFish()
        {
            for (int i = 0; i < LFish.Count; i++)
            {
                if (LFish[i].dirX == 1)
                {
                    LFish[i].X += LFish[i].speed;
                }
                else if (LFish[i].dirX == -1)
                {
                    LFish[i].X -= LFish[i].speed;
                }

                if (i % 2 == 0)
                {
                    if (ctF <= 5)
                    {
                        LFish[i].Y += 4;
                    }
                    else
                    {
                        LFish[i].Y -= 4;

                    }
                }
            }

            ctF++;

            if (ctF == 10)
            {
                ctF = 0;
            }

            for (int i = 0; i < dangerousFish.Count; i++)
            {
                if (dangerousFish[i].dirX == 1)
                {
                    dangerousFish[i].X += 5;
                }
                else if (dangerousFish[i].dirX == -1)
                {
                    dangerousFish[i].X -= 5;
                }
            }

        }

        void CreateDangerousFish()
        {
            Random rr = new Random();
            CActorFish pnn = new CActorFish();
            int d = rr.Next(0, 2);
            pnn.Y = rr.Next(660, this.ClientSize.Height + YScroll);
            pnn.W = 60;
            pnn.H = 60;
            pnn.X = 0;
            pnn.imgs = new List<Bitmap>();
            for (int k = 0; k < 2; k++)
            {
                Bitmap im = new Bitmap("df" + (k + 1) + ".png");
                im.MakeTransparent();
                pnn.imgs.Add(im);
            }
            if (d == 0)
            {
                pnn.iFrameF = 0;
                pnn.dirX = -1;
            }
            else if (d == 1)
            {
                pnn.iFrameF = 1;
                pnn.dirX = 1;
            }

            dangerousFish.Add(pnn);
        }

        void CheckDangerFish()
        {
            for (int i = 0; i < dangerousFish.Count; i++)
            {
                if (hook[0].X >= dangerousFish[i].X && hook[0].X <= dangerousFish[i].X + dangerousFish[i].W && hook[0].Y + hook[0].H / 2 >= dangerousFish[i].Y - YScroll && hook[0].Y + hook[0].H / 2 <= dangerousFish[i].Y - YScroll + dangerousFish[i].H)
                {
                    touchDanger = 1;
                    dangerF = i;
                }
            }
        }

        void CreateSeaWeed()
        {
            int yS = 890;

            for (int i = 0; i < 2; i++)
            {
                CActorSeaWeed pnn = new CActorSeaWeed();
                pnn.X = 0 - 10;
                pnn.Y = yS;
                yS += 400;
                pnn.W = 100;
                pnn.H = 60;
                pnn.imgs = new List<Bitmap>();
                for (int k = 1; k < 4; k++)
                {
                    Bitmap im = new Bitmap("sw" + (k) + ".png");
                    im.MakeTransparent();
                    pnn.imgs.Add(im);
                }

                pnn.iFrame = 0;

                Lseaweed.Add(pnn);

            }

            yS = 1050;
            for (int i = 0; i < 2; i++)
            {
                CActorSeaWeed pnn = new CActorSeaWeed();
                pnn.X = this.ClientSize.Width - 90;
                pnn.Y = yS;
                yS += 30;
                pnn.W = 100;
                pnn.H = 60;
                pnn.imgs = new List<Bitmap>();
                for (int k = 4; k < 7; k++)
                {
                    Bitmap im = new Bitmap("sw" + (k) + ".png");
                    im.MakeTransparent();
                    pnn.imgs.Add(im);
                }

                pnn.iFrame = 0;

                Lseaweed.Add(pnn);

            }
        }

        void MoveSeaWeed()
        {
            if (ctS == 0)
            {
                for (int i = 0; i < Lseaweed.Count; i++)
                {
                    Lseaweed[i].iFrame = 0;
                }

            }
            else if (ctS == 1)
            {
                for (int i = 0; i < Lseaweed.Count; i++)
                {
                    Lseaweed[i].iFrame = 1;
                }

            }
            else if (ctS == 2)
            {
                for (int i = 0; i < Lseaweed.Count; i++)
                {
                    Lseaweed[i].iFrame = 2;
                }

            }
            else if (ctS == 3)
            {
                for (int i = 0; i < Lseaweed.Count; i++)
                {
                    Lseaweed[i].iFrame = 1;
                }

            }

            ctS++;
            if (ctS == 4)
            {
                ctS = 0;
            }
        }

        void CreateHorseFish()
        {
            CActorHorseFish pnn = new CActorHorseFish();
            pnn.X = this.ClientSize.Width - 100;
            pnn.Y = 1300;
            pnn.W = 30;
            pnn.H = 40;
            pnn.im = new Bitmap("hf.png");
            horseFish.Add(pnn);

            pnn = new CActorHorseFish();
            pnn.X = this.ClientSize.Width - 120;
            pnn.Y = 1320;
            pnn.W = 20;
            pnn.H = 30;
            pnn.im = new Bitmap("hf.png");
            horseFish.Add(pnn);

            pnn = new CActorHorseFish();
            pnn.X = this.ClientSize.Width - 130;
            pnn.Y = 1330;
            pnn.W = 10;
            pnn.H = 20;
            pnn.im = new Bitmap("hf.png");
            horseFish.Add(pnn);

        }

        void MoveHorseFish()
        {
            if (ctH % 2 == 0)
            {
                horseFish[0].Y += 5;
            }
            else
            {
                horseFish[0].Y -= 5;
            }

            ctH++;
        }

        void CreateRocks()
        {
            CActorRocks pnn = new CActorRocks();
            pnn.X = 0;
            pnn.Y = 900;
            pnn.W = 150;
            pnn.H = 450;
            pnn.im = new Bitmap("r1.png");
            rocks.Add(pnn);

            pnn = new CActorRocks();
            pnn.X = this.ClientSize.Width - 100;
            pnn.Y = 1000;
            pnn.W = 150;
            pnn.H = 450;
            pnn.im = new Bitmap("r2.png");
            rocks.Add(pnn);

        }

        void CreateBubbles()
        {
            Random rr = new Random();
            CActorBubbles pnn = new CActorBubbles();
            pnn.X = rr.Next(10, this.ClientSize.Width - 20);
            pnn.Y = rr.Next(660, this.ClientSize.Height + YScroll);
            pnn.W = 30;
            pnn.H = 50;
            pnn.im = new Bitmap("bubbles.png");

            bubbles.Add(pnn);
        }

        void MoveBubbles()
        {
            for (int i = 0; i < bubbles.Count; i++)
            {
                if (bubbles[i].Y > 700)
                {
                    bubbles[i].Y -= 5;
                }
                else
                {
                    bubbles.RemoveAt(i);
                }
            }
        }

        void CreateMoveCoin()
        {
            CActorMoveCoin pnn = new CActorMoveCoin();
            pnn.X = 245;
            pnn.Y = 200;
            pnn.W = 20;
            pnn.H = 20;
            pnn.im = new Bitmap("c1.png");

            moveCoin.Add(pnn);
        }

        void MoveCoin()
        {
            if (ctC == 0)
            {
                Ltext[0].iFrameC = 0;
            }
            else if (ctC == 1)
            {
                Ltext[0].iFrameC = 1;

            }
            else if (ctC == 2)
            {
                Ltext[0].iFrameC = 2;

            }
            else if (ctC == 3)
            {
                Ltext[0].iFrameC = 1;

            }

            ctC++;
            if (ctC == 4)
            {
                ctC = 0;
            }
        }

        void MoveFishCoin()
        {
            for (int i = 0; i < moveCoin.Count; i++)
            {
                moveCoin[i].X -= 30;
                moveCoin[i].Y -= 25;

                if (moveCoin[i].X <= Ltext[0].X && moveCoin[i].Y <= Ltext[0].Y)
                {
                    moveCoin.RemoveAt(i);
                }
            }
        }

        void CreateLife()
        {
            CActorLife pnn = new CActorLife();
            pnn.X = this.ClientRectangle.Width - 110;
            pnn.Y = 40;
            pnn.W = 100;
            pnn.H = 50;
            pnn.imgs = new List<Bitmap>();
            for (int k = 1; k < 4; k++)
            {
                Bitmap im = new Bitmap("life" + (k) + ".png");
                im.MakeTransparent();
                pnn.imgs.Add(im);
            }

            pnn.iFrame = 0;

            life.Add(pnn);
        }

        void UpdateLife()
        {
            if (kill == 0)
            {
                life[0].iFrame = 0;
            }
            else if (kill == 1)
            {
                life[0].iFrame = 1;
            }
            else if (kill == 2)
            {
                life[0].iFrame = 2;
                CActorFinal pnn = new CActorFinal();
                pnn.X = 130;
                pnn.Y = 100;
                pnn.W = this.ClientSize.Width / 2;
                pnn.H = this.ClientSize.Height / 2 + 100;
                pnn.im = new Bitmap("lost.png");
                final.Add(pnn);

                tt.Stop();
            }
        }

        void CreateWarning()
        {
            CActorWarning pnn = new CActorWarning();
            pnn.X = this.ClientRectangle.Width - 160;
            pnn.Y = 45;
            pnn.W = 35;
            pnn.H = 35;
            pnn.imgs = new List<Bitmap>();
            for (int k = 0; k < 3; k++)
            {
                Bitmap im = new Bitmap("w" + (k + 1) + ".png");
                im.MakeTransparent();
                pnn.imgs.Add(im);
            }

            pnn.iFrame = 0;

            warning.Add(pnn);
        }

        void MoveWarning()
        {
            if (ctW == 0)
            {
                warning[0].iFrame = 0;
            }
            else if (ctW == 1)
            {
                warning[0].iFrame = 1;

            }
            else if (ctW == 2)
            {
                warning[0].iFrame = 2;

            }
            else if (ctW == 3)
            {
                warning[0].iFrame = 1;
            }

            ctW++;
            if (ctW == 4)
            {
                ctW = 0;

            }
        }

        void CreateFinal()
        {
            CActorFinal pnn = new CActorFinal();
            pnn.X = 170;
            pnn.Y = 130;
            pnn.W = 250;
            pnn.H = 250;
            pnn.im = new Bitmap("final.png");
            pnn.score = colCoins.ToString();
            pnn.text1 = "Gain score higher \n than 40 to win";

            final.Add(pnn);

            if (colCoins < 40)
            {
                l = 1;
                colCoins = 0;
                down = 0;
                up = 0;
                end = 0;
                kill = 0;
                xF = 0; catchF = 0;
                ctH = 0;
            }
            else
            {
                l = 0;
                pnn = new CActorFinal();
                pnn.X = 50;
                pnn.Y = 35;
                pnn.W = 460;
                pnn.H = 460;
                pnn.im = new Bitmap("won.png");

                final.Add(pnn);

                tt.Stop();
            }

        }

        void DrawScene(Graphics g)
        {
            for (int i = 0; i < back.Count; i++)
            {
                g.DrawImage(back[i].im, back[i].rcDst, back[i].rcSrc, GraphicsUnit.Pixel);
            }

            for (int i = 0; i < boat.Count; i++)
            {
                g.DrawImage(boat[i].im, boat[i].X, boat[i].Y - YScroll, boat[i].W, boat[i].H);
            }
            for (int i = 0; i < rocks.Count; i++)
            {
                g.DrawImage(rocks[i].im, rocks[i].X, rocks[i].Y - YScroll, rocks[i].W, rocks[i].H);
            }

            for (int i = 0; i < horseFish.Count; i++)
            {
                g.DrawImage(horseFish[i].im, horseFish[i].X, horseFish[i].Y - YScroll, horseFish[i].W, horseFish[i].H);
            }

            if (a == 1)
            {
                for (int i = 0; i < arrows.Count; i++)
                {
                    g.DrawImage(arrows[i].im, arrows[i].X, arrows[i].Y, arrows[i].W, arrows[i].H);
                }
            }

            for (int i = 0; i < hook.Count; i++)
            {
                g.DrawImage(hook[i].im, hook[i].X - hook[i].W / 2, hook[i].Y, hook[i].W, hook[i].H);
                g.DrawLine(new Pen(Color.OrangeRed, 2), boat[0].X + boat[0].W, boat[0].Y - YScroll, hook[i].X, hook[i].Y);

            }

            for (int i = 0; i < Lseaweed.Count; i++)
            {
                g.DrawImage(Lseaweed[i].imgs[Lseaweed[i].iFrame], Lseaweed[i].X, Lseaweed[i].Y - YScroll, Lseaweed[i].W, Lseaweed[i].H);

            }

            for (int i = 0; i < LFish.Count; i++)
            {
                g.DrawImage(LFish[i].imgs[LFish[i].iFrameF], LFish[i].X, LFish[i].Y - YScroll, LFish[i].W, LFish[i].H);

            }


            for (int i = 0; i < hookFish.Count; i++)
            {
                g.DrawImage(hookFish[i].imgs[hookFish[i].iFrameF], hookFish[i].X, hookFish[i].Y, hookFish[i].W, hookFish[i].H);

            }

            for (int i = 0; i < dangerousFish.Count; i++)
            {
                g.DrawImage(dangerousFish[i].imgs[dangerousFish[i].iFrameF], dangerousFish[i].X, dangerousFish[i].Y - YScroll, dangerousFish[i].W, dangerousFish[i].H);

            }

            for (int i = 0; i < moveCoin.Count; i++)
            {
                g.DrawImage(moveCoin[i].im, moveCoin[i].X, moveCoin[i].Y - YScroll, moveCoin[i].W, moveCoin[i].H);
            }

            Font Fnt = new Font("Comic Sans MS", 16, FontStyle.Bold);
            for (int i = 0; i < colFish.Count; i++)
            {
                g.DrawImage(colFish[i].imgs[colFish[i].iFrameF], colFish[i].X, colFish[i].Y, colFish[i].W, colFish[i].H);

                g.DrawString("+" + colFish[i].coins.ToString(), Fnt, Brushes.Black, colFish[i].X + 10, colFish[i].Y - 40);

            }

            for (int i = 0; i < bubbles.Count; i++)
            {
                g.DrawImage(bubbles[i].im, bubbles[i].X, bubbles[i].Y - YScroll, bubbles[i].W, bubbles[i].H);
            }

            g.DrawString(Ltext[0].text, Fnt, Brushes.Black, Ltext[0].X, Ltext[0].Y - YScroll);
            g.DrawImage(Ltext[0].imC[Ltext[0].iFrameC], Ltext[0].X - 30, Ltext[0].Y - YScroll, 25, 30);

            if (p == 0)
            {
                g.DrawImage(play[0].im, play[0].X, play[0].Y - YScroll, play[0].W, play[0].H);
            }

            for (int i = 0; i < life.Count; i++)
            {
                g.DrawImage(life[i].imgs[life[i].iFrame], life[i].X, life[i].Y, life[i].W, life[i].H);

            }

            if (kill > 0)
            {
                for (int i = 0; i < warning.Count; i++)
                {
                    g.DrawImage(warning[i].imgs[warning[i].iFrame], warning[i].X, warning[i].Y, warning[i].W, warning[i].H);
                }
            }

            Fnt = new Font("Comic Sans MS", 18, FontStyle.Bold);
            for (int i = 0; i < final.Count; i++)
            {
                g.DrawImage(final[i].im, final[i].X, final[i].Y, final[i].W, final[i].H);
                g.DrawString(final[i].score, Fnt, Brushes.White, final[i].X + 109, final[i].Y + 133);


                Fnt = new Font("Comic Sans MS", 12, FontStyle.Bold);
                
                if (l == 1)
                {
                    g.DrawString(final[i].text1, Fnt, Brushes.White, final[i].X + 55, final[i].Y + 180);

                }

            }

        }

        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
