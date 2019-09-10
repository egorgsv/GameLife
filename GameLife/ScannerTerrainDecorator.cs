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

        List<Colony> colonies = new List<Colony>();

        public bool inColonies(Cell cell)
        {
            foreach (var colony in colonies)
            {
                if (colony.cellList.Contains(cell)) return true;
            }

            return false;
        }

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

            void newColony(ref Cell cell, ref Colony colony)
            {
                if (!inColonies(cell) && cell.x != N && cell.y != N)
                {
                    colony.cellList.Add(cell);
                    if (terrain.field[cell.x + 1, cell.y].color == Cell.Color.Black && 
                        terrain.field[cell.x + 1, cell.y].Slate == Cell.CellSlate.Alive)
                    {
                        newColony(ref terrain.field[cell.x + 1, cell.y], ref colony);
                    }
                    if (terrain.field[cell.x, cell.y + 1].color == Cell.Color.Black &&
                        terrain.field[cell.x, cell.y + 1].Slate == Cell.CellSlate.Alive)
                    {
                        newColony(ref terrain.field[cell.x, cell.y + 1], ref colony);
                    }
                    if (terrain.field[cell.x + 1, cell.y + 1].color == Cell.Color.Black &&
                        terrain.field[cell.x + 1, cell.y + 1].Slate == Cell.CellSlate.Alive)
                    {
                        newColony(ref terrain.field[cell.x + 1, cell.y + 1], ref colony);
                    }
                }
            }

            for (int i = 1; i < N; i++)
            {
                for (int j = 1; j < N; j++)
                {
                    if (terrain.field[i, j].color == Cell.Color.Black && terrain.field[i, j].Slate == Cell.CellSlate.Alive && !inColonies(terrain.field[i, j]))
                    {
                        Colony colony = new Colony();
                        colonies.Add(colony);
                        newColony(ref terrain.field[i, j], ref colony);
                        colony.ChooseDirection();
                        colony.IsBorder();
                    }
                }
            }

            foreach (Colony colony in colonies)
            {
                colony.IsBorder();
                colony.ChooseDirection();
            }

            
            Joining();

            for (int i = 0; i < N + 2; i++)
            {
                for (int j = 0; j < N + 2; j++)
                {
                    if (i == 0 || i == N + 1 || j == 0 || j == N + 1)
                    {
                        terrain.field[i, j].color = Cell.Color.White;
                        terrain.field[i, j].direction = Cell.Direction.Null;
                        terrain.field[i, j].Slate = Cell.CellSlate.Dead;
                    }
                }
            }

            //for (int i = 1; i < N + 1; i++)
            //{
            //    for (int j = 1; j < N + 1; j++)
            //    {
            //        if (terrain.field[i, j].color == Cell.Color.Black && (i == 1 || i == N || j == 1 || j == N) && terrain.field[i, j].Slate == Cell.CellSlate.Alive)
            //        {
            //            terrain.field[i, j].ChooseNewDirection();
            //        }
            //    }
            //}

            //for (int i = 1; i < N + 1; i++)
            //{
            //    for (int j = 1; j < N + 1; j++)
            //    {
            //        terrain.field[i, j].ChooseDirection();

            //    }
            //}

            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    if (inColonies(terrain.field[i, j]))
                    {
                        terrain.field[i, j].color = Cell.Color.Black;
                        terrain.field[i, j].direction = GetDirection(terrain.field[i, j]);
                        terrain.field[i, j].Slate = Cell.CellSlate.Alive;

                    }
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
                        RemoveFromColony(terrain.field[i, j]);
                    }
                }
            }
            

            void RemoveFromColony(Cell cell)
            {
                foreach (Colony colony in colonies)
                {
                    colony.cellList.Remove(cell);
                }
                
            }

            void Joining()
            {

                List<Colony> newcolonies = new List<Colony>();
                foreach (Colony colony in colonies)
                {
                    
                    Colony newcolony = new Colony();
                    foreach (Cell cell in colony.cellList)
                    {
                        newcolony.cellList.Add(cell);
                        foreach (Cell neigh in cell.cellNeigh)
                        {
                            if (neigh != null)
                            { 
                                if (neigh.color == Cell.Color.Black && neigh.Slate == Cell.CellSlate.Alive && !colony.cellList.Contains(neigh))
                                {
                                    newcolony.cellList.Add(neigh);
                                }
                            }
                        }
                    }
                    newcolony.ChooseNewDirection();
                    newcolony.IsBorder();
                    newcolonies.Add(newcolony);
                    
                }
                colonies = newcolonies;
            }

            

            Cell.Direction GetDirection(Cell cell)
            {
                foreach (Colony colony in colonies)
                {
                    if (colony.GetDirection(cell) != Cell.Direction.Null)
                    {
                        return colony.GetDirection(cell);
                    }
                }
                return Cell.Direction.Null;
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
                                terrain.field[i, j - 1].color = Cell.Color.Black;
                                terrain.field[i, j - 1].direction = Cell.Direction.Up;
                                terrain.field[i, j - 1].Slate = Cell.CellSlate.Alive;

                                break;
                            case Cell.Direction.Down:
                                terrain.field[i, j + 1].color = Cell.Color.Black;
                                terrain.field[i, j + 1].direction = Cell.Direction.Down;
                                terrain.field[i, j + 1].Slate = Cell.CellSlate.Alive;

                                break;
                            case Cell.Direction.Right:
                                terrain.field[i + 1, j].color = Cell.Color.Black;
                                terrain.field[i + 1, j].direction = Cell.Direction.Right;
                                terrain.field[i + 1, j].Slate = Cell.CellSlate.Alive;

                                break;
                            case Cell.Direction.Left:
                                terrain.field[i - 1, j].color = Cell.Color.Black;
                                terrain.field[i - 1, j].direction = Cell.Direction.Left;
                                terrain.field[i - 1, j].Slate = Cell.CellSlate.Alive;

                                break;
                        }
                    }
                }

            }





            //colonies.Clear();  

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
