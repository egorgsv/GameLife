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
        TerrainDecorator terrain = new StatisticsTerrainDecorator(new Terrain());
        TerrainDecorator terrainScan = new ScannerTerrainDecorator(new Terrain());

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 300; //интервал таймера в мс
            timer1.Enabled = false;
            //terrain = new ScannerTerrainDecorator(terrain); //декоратор сканера
        }

        //ПОЕХАЛИ!
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Text = "Restart";
            timer1.Enabled = true;
            terrain.Start();
            terrainScan.Start();
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
            Bitmap bmpScan = new Bitmap(pictureBox2.Height, pictureBox2.Width);

            //АСТАНАВИТЕСЬ!!!!!
            if (!stopped) 
            {
                terrainScan.CopyFrom(terrain);
                terrain.MakeTurn();
                pictureBox1.Image = terrain.Draw(bmp);

                
                terrainScan.MakeTurn();
                pictureBox2.Image = terrainScan.Draw(bmpScan);

                StatisticsTerrainDecorator terr = terrain.terrain as StatisticsTerrainDecorator;
                if (terr != null) //вывод статистики 
                {
                    Statistics.Text = "Количество инфузорий: " + Convert.ToString(terr.stat.currentAliveCount) + "\n" +
                    "Процент изменения: " + Convert.ToString(terr.stat.percent) + "\n" +
                    "Время хода: " + Convert.ToString(terr.stat.time);
                }

            } 
        }

        //рисовать сетку????
        private void checkBoxDraw_CheckedChanged(object sender, EventArgs e)
        {
            drawGrid = !drawGrid;
            if (drawGrid)
            {
                terrain = new FramedCellsTerrainDecorator(terrain); //декоратор сетки
                terrainScan = new FramedCellsTerrainDecorator(terrainScan);
            }
            else
            {
                terrain = new StatisticsTerrainDecorator(terrain.terrain);
                terrainScan = new ScannerTerrainDecorator(terrainScan.terrain);
            }
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
