using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLife
{
    //FramedCellsTerrainDecorator – аналогично декоратор для Terrain и оборачивает Draw(), 
    //рисуя клетки после Terrain.Draw(). 
    //рисование сетки
    //удалить при смене галочки
    class FramedCellsTerrainDecorator
    {
        protected Terrain terrain;

        public void SetTerrain(Terrain t)
        {
            terrain = t;
        }

        public void Draw(PictureBox pictureBox)
        {
            float diam = pictureBox.Width / Terrain.N;
            Bitmap bmp = new Bitmap(pictureBox.Height, pictureBox.Width);
            Graphics g = Graphics.FromImage(bmp);

            if (terrain != null)
            {
                Pen pen = new Pen(Color.LightGray);
                for (int i = 1; i < Terrain.N + 1; i++)
                {
                    g.DrawLine(pen, 0, i * diam, pictureBox.Height, i * diam); //горизонтальные
                    g.DrawLine(pen, i * diam, 0, i * diam, pictureBox.Width); //вертикальные
                }

                terrain.Draw(pictureBox); //рисует инфузории
            }
        }

        
    }
}
