using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Translate
{
    public abstract class ITranslateTextMode
    {
        string m_nameMode;
        public static ITranslateTextMode SetupMode()
        {
            Console.Clear();
            string[] text = new string[1] { "\n\n\t1)Перевести строку без условий" };
            for (int i = 0; i < text.Length; i++)
            {
                Console.WriteLine(text[i]);
            }
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Escape:
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\tВы вышли");
                        break;
                    }
                case ConsoleKey.D1:
                    {
                        return new SolidTranslateText("Перевести строку без условий");
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\t\tНеверный ввод");
                        break;
                    }
            }
            return null;
        }
        public ITranslateTextMode(string name) { m_nameMode = name; }

        public override string ToString()
        {
            return m_nameMode;
        }
        virtual public string GetTranslateText(string word, string fromLanguage, string toLanguage)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            return result;
        }
    }

    internal class SolidTranslateText : ITranslateTextMode
    {
        public SolidTranslateText(string mode) : base(mode) { }
    }
}
