using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLife
{
    //Terrain хранит матрицу Cell, делает ход(переадресуя действия всем Cell,
    //а потом снабжает каждую из них соседями), рисует каждую Cell. 
    public class Terrain//поле
    {
        public static int N = 30;

        public Cell[,] field = new Cell[N + 2, N + 2];

        private int aliveCountPrev = 0;

        public Terrain() //конструктор
        {
            //создаётся каждая ячейка
            for (int i = 0; i < N + 2; i++)
            {
                for (int j = 0; j < N + 2; j++)
                {
                    field[i, j] = new Cell(i, j);
                }
            }
        }

        //начало игры
        public void Start()
        {
            Random random = new Random();
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    //рандомно выбираем состояние клетки 
                    int randomNumber = random.Next(0, 999);

                    field[i, j].Slate = randomNumber % 5 == 0 ? Cell.CellSlate.Alive : Cell.CellSlate.Dead;

                    Cell[] cellNeigh = new Cell[8]; //массив соседей

                    int f = 0;
                    for (int k = i - 1; k < i + 2; k++)
                    {
                        for (int l = j - 1; l < j + 2; l++)
                        {
                            if (k != i || l != j)
                            {
                                cellNeigh[f] = field[k, l];
                                f++;
                            }
                        }
                    }

                    field[i, j].cellNeigh = cellNeigh;
                }
            }
        }

        //шаг игры
        public void MakeTurn()
        {
            aliveCountPrev = this.AliveCount();

            //подсчёт соседей
            int[,] aliveNeighCount = new int[N + 2, N + 2];
            AliveNeighCount(ref aliveNeighCount);

            //шаг игры
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    field[i, j].MakeTurn(aliveNeighCount[i, j]);
                }
            }
        }

        //подсчёт количества всех живых клеток на предыдущем шаге
        public int AliveCountPrev()
        {
            return aliveCountPrev;
        }

        //подсчёт количества всех живых клеток
        public int AliveCount()
        {
            int Count = 0;
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (field[i, j].Slate == Cell.CellSlate.Alive)
                    {
                        Count++;
                    }
                }
            }

            return Count;
        }

        //подсчёт соседей
        private void AliveNeighCount(ref int [,] aliveNeighCount)
        {
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    aliveNeighCount[i, j] = field[i, j].AliveNeighCount();
                }
            }
        }

        
        //поле рисует само себя
        public Image Draw(Image image)
        {
            float diam = image.Height / N;
            // Create solid brush.
            SolidBrush orangeBrush = new SolidBrush(Color.DarkOrange);
            Graphics g = Graphics.FromImage(image);

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (field[i, j].Slate == Cell.CellSlate.Alive)
                    {
                        g.FillEllipse(orangeBrush, (i + 0.1f - 1) * diam, (j + 0.1f - 1) * diam, diam * 0.8f, diam * 0.8f);
                    }
                }
            }
            return image;
        }

    }

}
