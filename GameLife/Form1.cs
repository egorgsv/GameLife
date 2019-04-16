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
            float diam = pictureBox1.Width / Terrain.N;
            Bitmap bmp = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            Graphics g = Graphics.FromImage(bmp);
            Pen penOrange = new Pen(Color.DarkOrange);

            if (drawGrid) //рисуем сетку
            {
                Pen pen = new Pen(Color.LightGray);
                for (int i = 1; i < Terrain.N + 1; i++)
                {
                    g.DrawLine(pen, 0, i * diam, Height, i * diam); //горизонтальные
                    g.DrawLine(pen, i * diam, 0, i * diam, Width); //вертикальные
                }
            }

            //АСТАНАВИТЕСЬ!!!!!
            if (!stopped) 
            {
                /*int[,] PrevAliveNeighCount = new int[Terrain.N + 2, Terrain.N + 2];

                for (int i = 1; i < Terrain.N + 1; i++)
                {
                    for (int j = 1; j < Terrain.N + 1; j++)
                    {
                        PrevAliveNeighCount[i, j] = terrain.field[i, j].AliveNeighCount();
                    }
                }
                */

                Cell.CellSlate[,] PrevField = new Cell.CellSlate[Terrain.N + 2, Terrain.N + 2];

                for (int i = 0; i < Terrain.N + 2; i++)
                {
                    for (int j = 0; j < Terrain.N + 2; j++)
                    {
                        PrevField[i, j] = terrain.field[i, j].Slate;
                    }
                }

                //время хода
                Stopwatch sw = Stopwatch.StartNew();
                terrain.MakeTurn();
                sw.Stop();

                //рисуем инфузории
                for (int i = 1; i < Terrain.N + 1; i++)
                {
                    for (int j = 1; j < Terrain.N + 1; j++)
                    {
                        if (terrain.field[i, j].Slate == Cell.CellSlate.Alive)
                        {
                            g.FillEllipse(orangeBrush, (i + 0.1f - 1) * diam, (j + 0.1f - 1) * diam, diam * 0.8f, diam * 0.8f);
                        }
                    }
                }

                pictureBox1.Image = bmp;

                //время сканера
                Stopwatch swScan = Stopwatch.StartNew();
                Scanner(PrevField);
                swScan.Stop();

                //вывод статистики в форму
                Statistics.Text = "Количество инфузорий: " + terrain.AliveCount() + "\n" +
                    "Процент изменения: " + Percent() + "\n" + "Время хода: " + sw.Elapsed.TotalMilliseconds + 
                    "ms \nВремя сканирования: " + swScan.Elapsed.TotalMilliseconds + "ms";
            } 
        }

        //считаем процент изменения количества инфузорий за ход
        private double Percent()
        {
            double percent = 0;
            if (terrain.AliveCountPrev() != 0)
            {
                percent = Math.Round((double)(terrain.AliveCount() - terrain.AliveCountPrev()) / (double)terrain.AliveCountPrev() * 10000) / 100;
            }
            return percent;
        }

        public void Scanner(Cell.CellSlate[,] PrevField)
        {
            float diamScan = pictureBox2.Width / Terrain.N;
            Bitmap bmpScan = new Bitmap(pictureBox2.Height, pictureBox2.Width);
            Graphics gScan = Graphics.FromImage(bmpScan);
            Pen penOrangeScan = new Pen(Color.DarkOrange);

            if (drawGrid)
            {
                Pen pen = new Pen(Color.LightGray);
                for (int i = 1; i < Terrain.N + 1; i++)
                {
                    gScan.DrawLine(pen, 0, i * diamScan, Height, i * diamScan);
                    gScan.DrawLine(pen, i * diamScan, 0, i * diamScan, Width);
                }
            }

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

            for (int i = 0; i < 6; i++)
            {
                terrainScan.MakeTurnDead();
            }

            //рисуем инфузории
            for (int i = 1; i < Terrain.N + 1; i++)
            {
                for (int j = 1; j < Terrain.N + 1; j++)
                {
                    if (terrainScan.field[i, j].Slate == Cell.CellSlate.Alive )
                    {
                        gScan.FillEllipse(orangeBrush, (i + 0.1f - 1) * diamScan, (j + 0.1f - 1) * diamScan, diamScan * 0.8f, diamScan * 0.8f);
                    }
                }
            }


            

            /* сканер по количеству соседей
            Terrain terrainScan = new Terrain();
            terrainScan.Start();
            for (int i = 1; i < Terrain.N + 1; i++)
            {
                for (int j = 1; j < Terrain.N + 1; j++)
                {
                    
                    terrainScan.field[i, j].Slate = terrain.field[i, j].Slate;
                }
            }

            for (int i = 1; i < Terrain.N + 1; i++)
            {
                for (int j = 1; j < Terrain.N + 1; j++)
                {
                    if (PrevAliveNeighCount[i, j] == terrain.field[i, j].AliveNeighCount() && terrainScan.field[i, j].Slate == Cell.CellSlate.Alive)
                    {
                        terrainScan.field[i, j].Slate = Cell.CellSlate.Alive;
                    }
                    else
                    {
                        terrainScan.field[i, j].Slate = Cell.CellSlate.Dead;
                    }
                }
            }

            for (int i = 1; i < Terrain.N + 1; i++)
            {
                for (int j = 1; j < Terrain.N + 1; j++)
                {
                    if (terrainScan.field[i, j].AliveNeighCount() > 0 && terrainScan.field[i, j].Slate == Cell.CellSlate.Alive)
                    {
                        gScan.FillEllipse(orangeBrush, (i + 0.1f - 1) * diamScan, (j + 0.1f - 1) * diamScan, diamScan * 0.8f, diamScan * 0.8f);
                    }
                }
            }
            */

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
