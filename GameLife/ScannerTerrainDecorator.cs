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

        Terrain terrainPrev;
        Terrain newterrain;

        public override void MakeTurn()
        {
            terrainPrev = new Terrain();
            newterrain = new Terrain();
            //terrainPrev = (Terrain) terrain.Clone();
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    terrainPrev.field[i, j].Slate = terrain.field[i, j].Slate;
                }
            }

            terrain.MakeTurn();
            terrain.MakeTurn();

            ////////
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (terrain.field[i, j].Slate == Cell.CellSlate.Alive &&
                        terrain.field[i, j].EqualsNeigh(terrainPrev.field[i, j])
                        && terrainPrev.field[i, j].Slate == terrain.field[i, j].Slate)
                    {
                        newterrain.field[i, j].Slate = Cell.CellSlate.Alive;
                    }
                    else
                    {
                        newterrain.field[i, j].Slate = Cell.CellSlate.Dead;
                    }
                }
            }

            for (int k = 0; k < 6; k++)
            {
                newterrain.MakeTurnDead();
            }

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    //newterrain.field[i, j] = (BlackCell)newterrain.field[i, j];
                    newterrain.field[i, j] = new BlackCell(i, j, newterrain.field[i, j].Slate);
                    newterrain.CellNeigh();
                }
            }

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    //terrain.field[i, j] = (BlackCell)newterrain.field[i, j].Clone();

                    terrain.field[i, j] = new BlackCell(i, j, newterrain.field[i, j].Slate);
                    terrain.CellNeigh();
                }
            }


            /////////
            //for (int i = 1; i < N + 1; i++)
            //{
            //    for (int j = 1; j < N + 1; j++)
            //    {
            //        if (terrainPrev.field[i, j].Slate == Cell.CellSlate.Alive &&
            //            terrain.field[i, j].AliveNeighCount() == terrainPrev.field[i, j].AliveNeighCount()
            //            && terrainPrev.field[i, j].Slate == terrain.field[i, j].Slate)
            //        {
            //            newterrain.field[i, j].Slate = Cell.CellSlate.Alive;
            //        }
            //        else
            //        {
            //            newterrain.field[i, j].Slate = Cell.CellSlate.Dead;
            //        }
            //    }
            //}

            //for (int i = 1; i < N + 1; i++)
            //{
            //    for (int j = 1; j < N + 1; j++)
            //    {
            //        if (newterrain.field[i, j].Slate == Cell.CellSlate.Alive && terrain.field[i, j].AliveNeighCount() == 2)
            //        {
            //            int f = 0;
            //            for (int k = i - 1; k < i + 2; k++)
            //            {
            //                for (int l = j - 1; l < j + 2; l++)
            //                {
            //                    if (k != i || l != j)
            //                    {
            //                        newterrain.field[k, l].Slate = terrainPrev.field[i, j].cellNeigh[f].Slate;
            //                        f++;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //newterrain.MakeTurn();
        }

        public override Image Draw(Image image)
        {
            return newterrain.Draw(image);
        }

        public override void Start()
        {
            terrain.Start();
        }
    }
}
