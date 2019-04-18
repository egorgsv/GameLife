using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void SetTerrain(Terrain t)
        {
            terrain = t;
        }

        public void MakeTurn()
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
                percent = Math.Round((double)(terrain.AliveCount() - prevAliveCount) / (double)prevAliveCount * 10000) / 100;
            }

            //вывод статистики в форму
            Statistics.Text = "Количество инфузорий: " + currentAliveCount + "\n" +
                "Процент изменения: " + percent + "\n" + "Время хода: " + sw.Elapsed.TotalMilliseconds;
                //"ms \nВремя сканирования: " + swScan.Elapsed.TotalMilliseconds + "ms";
        }

        
    }
}
