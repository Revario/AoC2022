using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

namespace Utils
{
    public class Input
    {
        private int day;
        private string sessionkey;

        public Input(string day, string? sessionkey = null)
        {
            this.day = int.Parse(day);
            this.sessionkey = sessionkey ?? Environment.GetEnvironmentVariable("aocSessKey") ?? throw new ArgumentException();
        }

        public static string GetInputFilePath(int day, [CallerFilePath] string callerFilePath = "")
        {
            var sessKey = Environment.GetEnvironmentVariable("aocSessKey");
            ArgumentNullException.ThrowIfNullOrEmpty(sessKey);

            var projDir = Directory.GetParent(callerFilePath);
            var filePath = Path.Combine(projDir!.FullName, "input.txt");

            if (File.Exists(filePath)){
                return filePath;
            }

            var file = GetInputFileFromWeb(sessKey, day).Result;
            File.WriteAllBytes(filePath, file);

            return filePath;
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


        private Task<byte[]> GetInputFileFromWeb()
        {
            return Input.GetInputFileFromWeb(sessionkey, day);
        }

        private static async Task<byte[]> GetInputFileFromWeb(string sessionKey, int day)
        {
            var baseAdr = new Uri("https://adventofcode.com");
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAdr, new Cookie("session", sessionKey));


            using var client = new HttpClient(new HttpClientHandler() { CookieContainer = cookieContainer });
            return await client.GetByteArrayAsync($"{baseAdr}2022/day/{day}/input");
        }
    }
}