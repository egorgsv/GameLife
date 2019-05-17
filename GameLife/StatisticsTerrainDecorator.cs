using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    //StatisticsTerrainDecorator – будет декоратором для Terrain 
    //и оборачивает его метод MakeTurn(), замеряя время до и 
    //после вызова этого метода, также собирая другую статистику.
    class StatisticsTerrainDecorator : Terrain
    {
        protected Terrain terrain;
        public StatisticsTerrainDecorator(Terrain terrain) 
        {
            this.terrain = terrain;
        }

        public struct Statistics
        {
            public int currentAliveCount;
            public double percent;
            public double time;
        }

        public Statistics stat;

        public override void MakeTurn()
        {
            int prevAliveCount = terrain.AliveCount(); //количество до хода

            //время хода
            Stopwatch sw = Stopwatch.StartNew();
            terrain.MakeTurn();
            sw.Stop();

            int currentAliveCount = terrain.AliveCount(); //количество после хода

            //Процент изменения
            double percent = 0;
            if (prevAliveCount != 0)
            {
                percent = Math.Round((double) (currentAliveCount - prevAliveCount) / (double) prevAliveCount * 10000) / 100;
            }
            stat.percent = percent;
            stat.time = sw.Elapsed.TotalMilliseconds;
            stat.currentAliveCount = currentAliveCount;
        }

        public override void Start()
        {
            terrain.Start();
        }

        public override Image Draw(Image image)
        {
            return terrain.Draw(image);
        }
    }
}
