using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate
{
    public abstract class ISettingsDirective
    {
        string m_nameSettingsDirective;
        static public ISettingsDirective SetupSettingsDirective()
        {
            Console.Clear();
            string[] text = new string[1] { "\n\n\t1)В текущей папке\nСоздать копию с переводом в той же папке что и файл" };
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
                        return new SDCurrentFolder("В текущей папке");
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
        public ISettingsDirective(string name)
        {
            m_nameSettingsDirective = name;
        }
        public override string ToString()
        {
            return m_nameSettingsDirective;
        }

        public virtual void StartWork(ITranslateTextMode mode, string path, string fromLanguage, string toLanguage) { }
    }
}
