using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class Colony
    {
        public List<Cell> cellList = new List<Cell>();

        public enum Direction { Up, Down, Left, Right, Null }

        public Direction direction;

        public Colony(Colony.Direction direction = Colony.Direction.Null)
        {
            this.direction = direction;
        }

        public void ChooseDirection()
        {
            if (direction != Colony.Direction.Null)
            {
                foreach (var cell in cellList)
                {
                    switch (direction)
                    {
                        case Direction.Down:
                            cell.direction = Cell.Direction.Down;
                            break;
                        case Direction.Up:
                            cell.direction = Cell.Direction.Up;
                            break;
                        case Direction.Right:
                            cell.direction = Cell.Direction.Right;
                            break;
                        case Direction.Left:
                            cell.direction = Cell.Direction.Left;
                            break;
                        default:
                            direction = Direction.Null;
                            break;
                    }
                }
            }
            else
            {
                ChooseNewDirection();
            }
        }

        public void ChooseNewDirection()
        {
            Random random = new Random();
            //рандомно выбираем направление
            int randomNumber = random.Next(0, 3);
            switch (randomNumber)
            {
                case 1:
                    direction = Direction.Up;
                    break;
                case 2:
                    direction = Direction.Down;
                    break;
                case 3:
                    direction = Direction.Right;
                    break;
                case 0:
                    direction = Direction.Left;
                    break;
                default:
                    direction = Direction.Null;
                    break;
            }    
        }

        public void IsBorder()
        {
            foreach (Cell cell in cellList)
            {
                if(cell.x == Terrain.N - 1 || cell.y == Terrain.N - 1 || cell.x == 2 || cell.y == 2)
                {
                    switch (direction)
                    {
                        case Direction.Down:
                            direction = Direction.Up;
                            break;
                        case Direction.Up:
                            direction = Direction.Down;
                            break;
                        case Direction.Left:
                            direction = Direction.Right;
                            break;
                        case Direction.Right:
                            direction = Direction.Left;
                            break;
                        default:
                            direction = Direction.Null;
                            break;
                    }
                }
            }
        }

        public Cell.Direction GetDirection(Cell cell)
        {
            ChooseDirection();
            foreach (var thiscell in cellList)
            {
                if (cell.x == thiscell.x && cell.y == thiscell.y)
                {
                    return thiscell.direction;
                }
            }
            return Cell.Direction.Null;
        }

        public bool InColony(Cell cell)
        {
            foreach (var thiscell in cellList)
            {
                if (cell.x == thiscell.x && cell.y == thiscell.y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
