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
        /// Объект WebClient используемый при тестировании
        /// </summary>
        public WebClient WebClient { get; private set; }

        /// <summary>
        /// Запуск тестирования скорости
        /// </summary>
        public async Task Start()
        {
            var watch = new Stopwatch();

            WebClient = new WebClient();

            WebClient.DownloadProgressChanged += (s, e) =>
            {

            };

            WebClient.DownloadDataCompleted += (s, e) =>
            {
                watch.Stop();
                DownloadSpeed = Math.Round((e.Result.LongLength * 8) / (1000000 * watch.Elapsed.TotalSeconds), 2);
            };

            watch.Start();
            await WebClient.DownloadDataTaskAsync(new Uri("https://speedtest.selectel.ru/10MB"));
        }
    }
}
