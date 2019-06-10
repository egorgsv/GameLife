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
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    terrainPrev.field[i, j].Slate = terrain.field[i, j].Slate;
                    terrainPrev.field[i, j].color = terrain.field[i, j].color;
                    terrainPrev.field[i, j].direction = terrain.field[i, j].direction;
                }
            }

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
                    if (newterrain.field[i, j].Slate == Cell.CellSlate.Alive)
                    {
                        newterrain.field[i, j].color = Cell.Color.Black;
                        terrain.field[i, j].color = Cell.Color.Black;
                    }

                }
            }

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    terrain.field[i, j].ChooseDirection();

                }
            }

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (terrain.field[i, j].color == Cell.Color.Black && (i == 1 || i == N || j == 1 || j == N) && terrain.field[i, j].Slate == Cell.CellSlate.Alive)
                    {
                        terrain.field[i, j].ChooseNewDirection();
                    }
                }
            }

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    terrain.field[i, j].ChooseDirection();

                }
            }


            TerrainDecorator CopyTerrain = new StatisticsTerrainDecorator(new Terrain());
            CopyTerrain.CopyFrom(terrain);
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (CopyTerrain.field[i, j].Slate == Cell.CellSlate.Alive && CopyTerrain.field[i, j].color == Cell.Color.Black)
                    {
                        terrain.field[i, j].color = Cell.Color.White;
                        terrain.field[i, j].direction = Cell.Direction.Null;
                        terrain.field[i, j].Slate = Cell.CellSlate.Dead;
                    }
                }
            }

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (CopyTerrain.field[i, j].Slate == Cell.CellSlate.Alive && CopyTerrain.field[i, j].color == Cell.Color.Black)
                    {
                        switch (CopyTerrain.field[i, j].direction)
                        {
                            case Cell.Direction.Up:
                                terrain.field[i - 1, j].color = Cell.Color.Black;
                                terrain.field[i - 1, j].direction = Cell.Direction.Up;
                                terrain.field[i - 1, j].Slate = Cell.CellSlate.Alive;

                                break;
                            case Cell.Direction.Down:
                                terrain.field[i + 1, j].color = Cell.Color.Black;
                                terrain.field[i + 1, j].direction = Cell.Direction.Down;
                                terrain.field[i + 1, j].Slate = Cell.CellSlate.Alive;

                                break;
                            case Cell.Direction.Right:
                                terrain.field[i, j + 1].color = Cell.Color.Black;
                                terrain.field[i, j + 1].direction = Cell.Direction.Right;
                                terrain.field[i, j + 1].Slate = Cell.CellSlate.Alive;

                                break;
                            case Cell.Direction.Left:
                                terrain.field[i, j - 1].color = Cell.Color.Black;
                                terrain.field[i, j - 1].direction = Cell.Direction.Left;
                                terrain.field[i, j - 1].Slate = Cell.CellSlate.Alive;

                                break;
                        }
                    }
                }
            }

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    terrain.field[i, j].ChooseDirection();
                }
            }

            for (int i = 0; i < N + 2; i++)
            {
                for (int j = 0; j < N + 2; j++)
                {
                    if (i == 0 || i == N + 1 || j == 0 || j == N + 1)
                    {
                        terrain.field[i, j].Slate = Cell.CellSlate.Dead;
                    }
                }
            }


            //for (int i = 1; i < N + 1; i++)
            //{
            //    for (int j = 1; j < N + 1; j++)
            //    {
            //        if (terrain.field[i, j].color == Cell.Color.Black && (i == 1 || i == N || j == 1 || j == N) && terrain.field[i, j].pause == false)
            //        {
            //            terrain.field[i, j].pause = true;

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
    }
}
