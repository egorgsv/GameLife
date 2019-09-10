using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    //Cell – хранит свои координаты, свой тип(занята или свободна), 
    //подсчитывает действие(появится или исчезнуть), хранит для этого ссылки на 8 соседей.
    public class Cell : ICloneable//клетка поля
    {
        public enum CellSlate { Dead, Alive };

        public CellSlate Slate;

        public enum Color { Black, White};

        public Color color;

        public enum Direction { Up, Down, Left, Right, Null }

        public Direction direction;

        //ссылки на соседей
        public Cell[] cellNeigh = new Cell[8];

        //координаты
        public int x;
        public int y;

        //конструктор
        public Cell(int x = 0, int y = 0, CellSlate slate = CellSlate.Dead, Color color = Color.White, Direction direction = Direction.Null) 
        {
            this.x = x;
            this.y = y;
            Slate = slate;
            this.color = color;
            this.direction = direction;
        }

        public Cell(Cell[] cellNeigh, int x = 0, int y = 0, CellSlate Slate = CellSlate.Dead, Color color = Color.White, Direction direction = Direction.Null)
        {
            this.cellNeigh = cellNeigh;
            this.x = x;
            this.y = y;
            this.Slate = Slate;
            this.color = color;
            this.direction = direction;
        }


        //делаем шаг
        public virtual void MakeTurn(int aliveNeighCount)
        {
            if (Slate == CellSlate.Alive && color == Color.White && AliveBlackCount() > AliveWhiteCount() + 1)
            {
                color = Color.Black;
            }

            if (Slate == CellSlate.Alive && color == Color.Black && AliveWhiteCount() > AliveBlackCount() + 1)
            {
                color = Color.White;
            }

            if (Slate == CellSlate.Dead && aliveNeighCount == 3 && color == Color.White)
            {
                Slate = CellSlate.Alive;
            }

            if (Slate == CellSlate.Alive && (aliveNeighCount > 3 || aliveNeighCount < 2))
            {
                Slate = CellSlate.Dead;
            }

            //ChooseDirection();
        }



                
                //public void ChooseDirection()
        //{
        //    if (this.color == Color.Black)
        //    {
        //        foreach (var neigh in cellNeigh)
        //        {
        //            if (neigh.color == Color.Black)
        //            {
        //                if (neigh.direction != Direction.Null)
        //                {
        //                    this.direction = neigh.direction;
        //                }
        //            }
        //        }

        //        if (this.direction != Direction.Null)
        //        {
        //            foreach (var neigh in cellNeigh)
        //            {
        //                if (neigh.color == Color.Black && neigh.Slate == CellSlate.Alive)
        //                {
        //                    neigh.direction = this.direction;
        //                    //neigh.ChooseDirection();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Random random = new Random();
        //            //рандомно выбираем направление
        //            int randomNumber = random.Next(0, 3);
        //            switch (randomNumber)
        //            {
        //                case 1:
        //                    direction = Direction.Up;
        //                    break;
        //                case 2:
        //                    direction = Direction.Down;
        //                    break;
        //                case 3:
        //                    direction = Direction.Right;
        //                    break;
        //                case 0:
        //                    direction = Direction.Left;
        //                    break;
        //            }

        //        }
        //    }
        //    else
        //    {
        //        direction = Direction.Null;
        //    }
        //}

        //public void ChooseNewDirection()
        //{
        //    if (this.color == Color.Black)
        //    {
        //        Random random = new Random();
        //        //рандомно выбираем направление
        //        int randomNumber = random.Next(0, 3);
        //        switch (randomNumber)
        //        {
        //            case 1:
        //                direction = Direction.Up;
        //                break;
        //            case 2:
        //                direction = Direction.Down;
        //                break;
        //            case 3:
        //                direction = Direction.Right;
        //                break;
        //            case 0:
        //                direction = Direction.Left;
        //                break;
        //        }


        //        foreach (var neigh in cellNeigh)
        //        {
        //            if (neigh.color == Color.Black && neigh.Slate == CellSlate.Alive)
        //            {
        //                neigh.direction = this.direction;
        //                //neigh.ChooseDirection();
        //            }
        //        }
        //    }}

        public int AliveBlackCount() //число живых чёрных соседей
        {
            int aliveBlackCount = 0;

            foreach (var neigh in cellNeigh)
            {
                if (neigh.Slate == CellSlate.Alive && neigh.color == Color.Black)
                {
                    aliveBlackCount++;
                }
            }

            return aliveBlackCount;
        }

        public int AliveWhiteCount() //число живых белых соседей
        {
            int aliveWhiteCount = 0;

            foreach (var neigh in cellNeigh)
            {
                if (neigh.Slate == CellSlate.Alive && neigh.color == Color.White)
                {
                    aliveWhiteCount++;
                }
            }

            return aliveWhiteCount;
        }

        //подсчёт количества живых соседей
        public int AliveNeighCount()
        {
            int aliveNeighCount = 0;

            foreach (var neigh in cellNeigh)
            {
                if (neigh.Slate == CellSlate.Alive)
                {
                    aliveNeighCount++;
                }
            }

            return aliveNeighCount;
        }
        

        public virtual object Clone()
        {
            Cell cell = new Cell();
            cell.cellNeigh = new Cell[cellNeigh.Length];
            for (int i = 0; i < cellNeigh.Length; i++)
            {
                if (cellNeigh[i] != null)
                {
                    cell.cellNeigh[i] = new Cell();
                    cell.cellNeigh[i].Slate = cellNeigh[i].Slate;
                    cell.cellNeigh[i].color = cellNeigh[i].color;
                    cell.cellNeigh[i].direction = cellNeigh[i].direction;
                }
            }
            cell.Slate = Slate;
            cell.color = color;
            cell.direction = direction;
            return cell;
        }

        public bool EqualsNeigh(Cell cell)
        {
            if (AliveNeighCount() != cell.AliveNeighCount()) return false;
            for (int i = 0; i < 8; i++)
            {
                if (cellNeigh[i].Slate != cell.cellNeigh[i].Slate)
                {
                    return false;
                }
            }
            return true;
        }

        public void MakeTurnDead(int aliveNeighCount)
        {
            if (Slate == CellSlate.Alive && (aliveNeighCount > 3 || aliveNeighCount < 2))
            {
                Slate = CellSlate.Dead;
            }
        }
    }
}
