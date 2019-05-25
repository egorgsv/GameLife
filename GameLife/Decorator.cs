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
    }
}
