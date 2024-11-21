using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppleTimer
{
    public partial class Form1 : Form
    {
        Timer WalkTimer = new Timer();
        Timer hitTimer = new Timer();
        Timer gravity = new Timer();

        int hitCount = 0;
        int requiredHits = 10;
        int hitDirection = 1; //1:barla;0;nyugalmai allapot; -1: Jobbra
        int hitFrames = 0;
        int hitMaxFrames = 3;
        int apples = 0;
        int storageCapacity = 20;

        bool holdingAnApple = false;
        int priceofBiggerStorage = 10;
        int priceofFasterHit = 30;
       

        public Form1()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            StartTimer();
            AddEvent();
        }
        void AddEvent()
        {
            buybiggerstorgae.Click += BuyBiggerStorage;
            buyfasterhit.Click += BuyFasterHit;
        }
        void StartTimer()
        {
            WalkTimer.Interval = 16;
            hitTimer.Interval = 16;
               gravity.Interval = 16;

            WalkTimer.Start();
            WalkTimer.Tick += WalkEvent;
            hitTimer.Tick += HitEvent;
            gravity.Tick += GravityEvent;


        }

        void WalkEvent(object s , EventArgs e)
        {
            if (kez.Left > torzs.Right && !holdingAnApple)
            {

            fej.Left -= 3;
            test.Left -= 3;
            kez.Left -= 3;
            }
            else if(!holdingAnApple)
            {
                WalkTimer.Stop();
                hitDirection = 1;
                hitTimer.Start();
            }
            else if (holdingAnApple && test.Right <kosar.Left)
            {
                fej.Left +=3;
                test.Left+= 3;
                kez.Left +=3;
                alma.Left += 3;
            }
            else if(holdingAnApple && test.Right >= kosar.Left)
            {
                WalkTimer.Stop();
                gravity.Start();
            }

        }

        void HitEvent(object s, EventArgs e)
        {
            if (hitDirection == 1)
            {
                kez.Left -= 6;
                hitFrames++;
                if (hitFrames == hitMaxFrames)
                {
                    hitDirection = -1;
                    hitFrames = 0;
                }
            }
            else if (hitDirection == -1)
            {
                kez.Left += 6;
                hitFrames++;
                if (hitFrames == hitMaxFrames)
                {
                    hitFrames = 0;
                    hitCount++;
                    this.Text = hitCount.ToString();
                    if (hitCount == requiredHits)
                    {
                        hitDirection = 0;
                        hitTimer.Stop();
                        alma.Left = kez.Left;
                        alma.Top = lomb.Bottom - alma.Height;
                        alma.Show();

                        gravity.Start();
                    }
                    else
                    {
                        hitDirection = 1;
                    }
                }
            }
        }

        void GravityEvent(object s, EventArgs e)
        {
            if(alma.Bottom < kez.Top && !holdingAnApple)
            {

            alma.Top += 3;
            }
            else if(!holdingAnApple)
            {
                holdingAnApple = true;
                gravity.Stop();
                kez.Left = test.Left + test.Width / 2;
                alma.Left = kez.Right - alma.Width;
                WalkTimer.Start();
            }
            else if (holdingAnApple)
            {
                alma.Top += 3;
            }
            if(alma.Top > kosar.Top)
            {
                gravity.Stop();
                holdingAnApple = false;
                kez.Left = test.Left + test.Width / 2 - kez.Width;

                hitCount = 0;

                if (apples < storageCapacity)
                {

                apples++;
                }
                updateAppleLable();

                WalkTimer.Start();
            }
            

        }
        void BuyBiggerStorage(object s, EventArgs e)
        {
            if(apples > priceofBiggerStorage)
            {
                apples -= priceofBiggerStorage;
                storageCapacity += 5;
                priceofBiggerStorage += 2;
                buybiggerstorgae.Text = $"{priceofBiggerStorage} apple";
                updateAppleLable();
                updateStorageLable();
            }
        }

        void BuyFasterHit(object s, EventArgs e)
        {
            if(apples >= priceofFasterHit && requiredHits >3)
            {
                apples -= priceofFasterHit;
                requiredHits--;
                priceofFasterHit += 30;
                buyfasterhit.Text = $"{priceofFasterHit} apple";

                updateAppleLable();

            }
        }

        void updateAppleLable()
        {
            AppleCounter.Text = $"Apple Count Number: {apples}";
        }

        void updateStorageLable()
        {
            storageLabel.Text = $"Apple Count Number: {storageCapacity} Apple";
        }

       
    }
}
