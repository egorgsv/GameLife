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
    class ScannerTerrainDecorator1 : TerrainDecorator
    {
        public ScannerTerrainDecorator1(Terrain terrain)
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

            

            TerrainDecorator CopyTerrain = new StatisticsTerrainDecorator(new Terrain());
            CopyTerrain.CopyFrom(terrain);

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (CopyTerrain.field[i, j].Slate == Cell.CellSlate.Alive && CopyTerrain.field[i, j].color == Cell.Color.Black)
                    {
                        newterrain.field[i, j].color = Cell.Color.White;
                        newterrain.field[i, j].direction = Cell.Direction.Null;
                        newterrain.field[i, j].Slate = Cell.CellSlate.Dead;
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
                                newterrain.field[i - 1, j].color = Cell.Color.Black;
                                newterrain.field[i - 1, j].direction = Cell.Direction.Up;
                                newterrain.field[i - 1, j].Slate = Cell.CellSlate.Alive;
                                
                                break;
                            case Cell.Direction.Down:
                                newterrain.field[i + 1, j].color = Cell.Color.Black;
                                newterrain.field[i + 1, j].direction = Cell.Direction.Down;
                                newterrain.field[i + 1, j].Slate = Cell.CellSlate.Alive;
                                
                                break;
                            case Cell.Direction.Right:
                                newterrain.field[i, j + 1].color = Cell.Color.Black;
                                newterrain.field[i, j + 1].direction = Cell.Direction.Right;
                                newterrain.field[i, j + 1].Slate = Cell.CellSlate.Alive;
                               
                                break;
                            case Cell.Direction.Left:
                                newterrain.field[i, j - 1].color = Cell.Color.Black;
                                newterrain.field[i, j - 1].direction = Cell.Direction.Left;
                                newterrain.field[i, j - 1].Slate = Cell.CellSlate.Alive;
                                
                                break;
                        }
                    }
                }
            }

            


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
