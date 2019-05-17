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
    class FramedCellsTerrainDecorator : Terrain
    {
        protected Terrain terrain;
        public FramedCellsTerrainDecorator(Terrain terrain) 
        {
            this.terrain = terrain;
        }

        public Image Draw(Image image)
        {
            float diam = image.Width / N;
            Graphics g = Graphics.FromImage(image);

            if (terrain != null)
            {
                Pen pen = new Pen(Color.LightGray);
                for (int i = 1; i < Terrain.N + 1; i++)
                {
                    g.DrawLine(pen, 0, i * diam, image.Height, i * diam); //горизонтальные
                    g.DrawLine(pen, i * diam, 0, i * diam, image.Width); //вертикальные
                }

                image = terrain.Draw(image); //рисует инфузории
            }

            return image;
        }

        public override void Start()
        {
            terrain.Start();
        }

        public override void MakeTurn()
        {
            terrain.MakeTurn();
        }

        }
}
