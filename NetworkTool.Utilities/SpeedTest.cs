using System.Diagnostics;
using System.Net;

namespace NetworkTool.Utilities
{
    public class SpeedTest
    {
        /// <summary>
        /// Скорость загрузки в Мбит/с
        /// </summary>
        public double DownloadSpeed { get; private set; }

        /// <summary>
        /// Запуск тестирования скорости
        /// </summary>
        public async Task Start()
        {
            var watch = new Stopwatch();

            var client = new WebClient();
            
            client.DownloadDataCompleted += (s, e) =>
            {
                watch.Stop();
                DownloadSpeed = Math.Round((e.Result.LongLength * 8) / (1000000 * watch.Elapsed.TotalSeconds), 2);
            };

            watch.Start();
            await client.DownloadDataTaskAsync(new Uri("https://speedtest.selectel.ru/10MB"));
        }
    }
}
