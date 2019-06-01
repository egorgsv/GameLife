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

        //ссылки на соседей
        public Cell[] cellNeigh = new Cell[8];

        //координаты
        public int x;
        public int y;

        //конструктор
        public Cell(int x = 0, int y = 0, CellSlate slate = CellSlate.Dead, Color color = Color.White) 
        {
            this.x = x;
            this.y = y;
            Slate = slate;
            this.color = color;
        }

        public Cell(Cell[] cellNeigh, int x = 0, int y = 0, CellSlate Slate = CellSlate.Dead, Color color = Color.White)
        {
            this.cellNeigh = cellNeigh;
            this.x = x;
            this.y = y;
            this.Slate = Slate;
            this.color = color;
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
        }

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
                }
                
            }
            cell.Slate = Slate;
            cell.color = color;
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
