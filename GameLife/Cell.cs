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

        //ссылки на соседей
        public Cell[] cellNeigh = new Cell[8];

        //координаты
        public int x;
        public int y;

        //конструктор
        public Cell(int x = 0, int y = 0, CellSlate slate = CellSlate.Dead) 
        {
            this.x = x;
            this.y = y;
            Slate = slate;
        }

        public Cell(Cell[] cellNeigh, int x = 0, int y = 0, CellSlate Slate = CellSlate.Dead)
        {
            this.cellNeigh = cellNeigh;
            this.x = x;
            this.y = y;
            this.Slate = Slate;
        }


        //делаем шаг
        public virtual void MakeTurn(int aliveNeighCount) { }

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
                }
                
            }
            cell.Slate = Slate;
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
