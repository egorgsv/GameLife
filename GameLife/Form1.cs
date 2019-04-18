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
        //terrain = new StatisticsTerrainDecorator();

        // Create solid brush.
        SolidBrush orangeBrush = new SolidBrush(Color.DarkOrange);

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 200; //интервал таймера в мс
            timer1.Enabled = false;
        }

        //ПОЕХАЛИ!
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Text = "Restart";
            terrain.Start();
            timer1.Enabled = true;
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
            if (drawGrid) //рисуем сетку
            {
                //terrain = new FramedCellsTerrainDecorator();
            }

            //АСТАНАВИТЕСЬ!!!!!
            if (!stopped) 
            {
                pictureBox1.Image = terrain.Draw(pictureBox1);
            } 
        }

        public void Scanner(Cell.CellSlate[,] PrevField)
        {
            Terrain terrainScan = new Terrain();
            terrainScan.Start();

            for (int i = 1; i < Terrain.N + 1; i++)
            {
                for (int j = 1; j < Terrain.N + 1; j++)
                {
                    terrainScan.field[i, j].Slate = PrevField[i, j];
                }
            }

            for (int i = 1; i < Terrain.N + 1; i++)
            {
                for (int j = 1; j < Terrain.N + 1; j++)
                {
                    if (terrainScan.field[i, j].Slate == terrain.field[i, j].Slate && terrain.field[i, j].Slate == Cell.CellSlate.Alive)
                    {
                        int count = 0;
                        for (int f = 0; f < 8; f++)
                        {
                            if (terrainScan.field[i, j].cellNeigh[f].Slate == terrain.field[i, j].cellNeigh[f].Slate)
                            {
                                count++;
                            }
                        }

                        if (count == 8) { terrainScan.field[i, j].Slate = Cell.CellSlate.Alive; }
                        else { terrainScan.field[i, j].Slate = Cell.CellSlate.Dead; }
                    }

                }
            }

            pictureBox2.Image = bmpScan;
        }

        //рисовать сетку????
        private void checkBoxDraw_CheckedChanged(object sender, EventArgs e)
        {
            drawGrid = !drawGrid;
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
