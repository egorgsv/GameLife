using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class BlackCell : Cell
    {
        public BlackCell(int x = 0, int y = 0, CellSlate slate = CellSlate.Dead) : base(x, y, slate)
        {
        }

        public BlackCell(Cell[] cellNeigh, int x = 0, int y = 0, CellSlate Slate = CellSlate.Dead) : base(cellNeigh, x, y, Slate)
        {
        }

        public BlackCell(WhiteCell cell)
        {

        }

        //Черная особь больше не рождается, но умирает, как и белая.
        public override void MakeTurn(int aliveNeighCount) 
        {
            if (Slate == CellSlate.Alive && (aliveNeighCount > 3 || aliveNeighCount < 2))
            {
                Slate = CellSlate.Dead;
            }
        }

        public override object Clone()
        {
            Cell cell = new BlackCell();
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
    }
}
