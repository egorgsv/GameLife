using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    public class Cell //клетка поля
    {
        public enum CellSlate { Dead, Alive };

        public CellSlate Slate;

        public Cell(CellSlate slate) //конструктор
        {
            Slate = slate;
        }

        public Cell() { }

        //делаем шаг
        public void MakeTurn(int aliveNeighCount)
        {

            if (Slate == CellSlate.Dead && aliveNeighCount == 3)
            {
                Slate = CellSlate.Alive;
            }

            if (Slate == CellSlate.Alive && (aliveNeighCount > 3 || aliveNeighCount < 2))
            {
                Slate = CellSlate.Dead;
            }
        }

        public void MakeTurnDead(int aliveNeighCount)
        {
            if (Slate == CellSlate.Alive && (aliveNeighCount > 3 || aliveNeighCount < 2))
            {
                Slate = CellSlate.Dead;
            }
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

        //соседи
        public Cell[] cellNeigh = new Cell[8];

        public void SetArrayNeigh(Cell[] array)
        {
            array.CopyTo(cellNeigh, 0);
        }
    }

}
