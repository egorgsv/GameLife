using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLife
{
    
    public partial class Form1 : Form
    {
        public bool stopped = false; //АСТАНАВИТЕСЬ!!!!!
        public bool drawGrid = false; //рисовать ли сетку
        Terrain terrain = new Terrain();

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 100; //интервал таймера в мс
            timer1.Enabled = false;
        }

        //ПОЕХАЛИ!
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Text = "Restart";
            timer1.Enabled = true;
            terrain = new StatisticsTerrainDecorator(terrain);

            terrain.Start();
            
        }

        //АСТАНАВИТЕСЬ!!!!!
        private void buttonStop_Click(object sender, EventArgs e)
        {
            stopped = !stopped;
            if (stopped) { buttonStop.Text = "Continue"; }
            else { buttonStop.Text = "Stop"; }
        }

        //ТИК-ТАК-ТИК-ТАК-ТИК-ТАК
        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Height, pictureBox1.Width);

            //АСТАНАВИТЕСЬ!!!!!
            if (!stopped) 
            {
                pictureBox1.Image = terrain.Draw(bmp);
                terrain.MakeTurn(); 
                //Statistics.Text
            } 
        }


        

        //рисовать сетку????
        private void checkBoxDraw_CheckedChanged(object sender, EventArgs e)
        {
            drawGrid = !drawGrid;
            //terrain = new FramedCellsTerrainDecorator(terrain);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Stat_Click(object sender, EventArgs e)
        {

        }
    }
}
