using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class TerrainDecorator : Terrain
    {
        public Terrain terrain { get; set; }

        public void CopyFrom(Terrain t)
        {
            
            for (int i = 1; i < N + 1; i++)
            {
                for (int j = 1; j < N + 1; j++)
                {
                    terrain.field[i, j] = (Cell) t.field[i, j].Clone();
                }
            }
        }
    }
}
