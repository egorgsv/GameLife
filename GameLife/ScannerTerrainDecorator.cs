using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLife
{
    //ScannerTerrainDecorator будет декоратором для Terrain и оборачивает его метод MakeTurn(),
    //после которого удаляет из массива те Cell, которые не из паттернов и вызывает Terrain.Draw(), 
    //подсунув другой pictureBox1.Image. Он получает его в своем конструкторе.
    class ScannerTerrainDecorator : TerrainDecorator
    {
        public ScannerTerrainDecorator(Terrain terrain)
        {
            this.terrain = terrain;
            field = terrain.field;
        }

        Terrain terrainPrev = new Terrain();
        Terrain newterrain = new Terrain();
        public override void MakeTurn()
        {
            terrainPrev.Start();
            newterrain.Start();
            terrainPrev = (Terrain) terrain.Clone();

            terrain.MakeTurn();

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (terrainPrev.field[i, j].Slate == Cell.CellSlate.Alive &&
                        terrain.field[i, j].AliveNeighCount() == terrainPrev.field[i, j].AliveNeighCount())
                    {
                        newterrain.field[i, j].Slate = Cell.CellSlate.Alive;
                    }
                    else
                    {
                        newterrain.field[i, j].Slate = Cell.CellSlate.Dead;
                    }
                }
            }

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    terrain.field[i, j].Slate = newterrain.field[i, j].Slate;
                }
            }

            //for (int i = 1; i < Terrain.N + 1; i++)
            //{
            //    for (int j = 1; j < Terrain.N + 1; j++)
            //    {

            //        terrainPrev.field[i, j].Slate = terrain.field[i, j].Slate;
            //    }
            //}

            //for (int i = 1; i < Terrain.N + 1; i++)
            //{
            //    for (int j = 1; j < Terrain.N + 1; j++)
            //    {
            //        if (PrevAliveNeighCount[i, j] == terrain.field[i, j].AliveNeighCount() && terrainPrev.field[i, j].Slate == Cell.CellSlate.Alive)
            //        {
            //            terrainPrev.field[i, j].Slate = Cell.CellSlate.Alive;
            //        }
            //        else
            //        {
            //            terrainPrev.field[i, j].Slate = Cell.CellSlate.Dead;
            //        }
            //    }
            //}
        }

        public override Image Draw(Image image)
        {
            return terrain.Draw(image);
        }

        public override void Start()
        {
            terrain.Start();
        }


































        /*
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
        */
    }
}
