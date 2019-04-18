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
    class ScannerTerrainDecorator : Terrain
    {
        protected Terrain terrain;

        public void SetTerrain(Terrain t)
        {
            terrain = t;
        }

        public void MakeTurn()
        {
            
        }

        public Image Draw(PictureBox pictureBox)
        {
            return null;
        }
    }
}
