using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

namespace Utils
{
    public class Input
    {
        private string day;
        private string sessionkey;

        public Input(string day, string? sessionkey = null)
        {
            this.day = day;
            this.sessionkey = sessionkey ?? Environment.GetEnvironmentVariable("aocSessKey") ?? throw new ArgumentException();
        }

        public string GetInputFilePath([CallerFilePath] string callerFilePath = "")
        {
            var projDir = Directory.GetParent(callerFilePath);
            var filePath = Path.Combine(projDir!.FullName, "input.txt");

            if (File.Exists(filePath)){
                return filePath;
            }
            var file = GetInputFileFromWeb().Result;
            File.WriteAllBytes(filePath, file);

            return filePath;
        }


        private async Task<byte[]> GetInputFileFromWeb()
        {
            var baseAdr = new Uri("https://adventofcode.com");
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAdr, new Cookie("session", sessionkey));


            using var client = new HttpClient(new HttpClientHandler() { CookieContainer = cookieContainer });
            return await client.GetByteArrayAsync($"{baseAdr}2022/day/{day}/input");
        }
    }
}