using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class Colony
    {
        List<BlackCell> colony = new List<BlackCell>();

        public enum Direction { Up, Down, Left, Right, Null }

        public Direction direction;

        public Colony(Colony.Direction direction = Colony.Direction.Null)
        {
            this.direction = direction;
        }

        public void ChooseDirection()
        {
            if (direction != null)
            {
                foreach (var cell in colony)
                {
                    cell.direction = (Cell.Direction) direction;
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
            int randomNumber = random.Next(0, 99) % 4;
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
                    direction = Direction.Down;
                    break;
            }    
        }
    }
}
