using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Translate
{
    public class Programms
    {
        private string m_path;
        private string m_fromLanguage;
        private string m_toLanguage;
        private ITranslateTextMode m_textMode;
        private ISettingsDirective m_settingsDirective;
        
        public virtual void Print(string temp, ConsoleColor t = ConsoleColor.Red, ConsoleColor t1 = ConsoleColor.White, int t2 = 1000)
        {
            Console.ForegroundColor = t;
            Console.WriteLine(temp);
            Console.ForegroundColor = t1;
            Thread.Sleep(t2);
        }

        public virtual bool IsStart()
        {
            Console.Clear();
            if (m_settingsDirective == null || m_path == null || IsLanguage() || m_textMode == null)
            {
                Print("\n\n\t\tНе все настройки выбраны");
                return false;
            }
            return true;
        }

        public bool IsLanguage()
        {
            if (m_fromLanguage == null)
            {
                return true;
            }

            if (m_toLanguage == null)
            {
                return true;
            }

            return false;
        }

        public string GetTextModeName()
        {
            if (m_textMode == null)
                return "";

            return m_textMode.ToString();
        }
        public string GetSettingsDirective()
        {
            if (m_settingsDirective == null)
                return "";

            return m_settingsDirective.ToString();
        }
        public void Menu()
        {
            bool loop = true;
            int idMenu = 0;
            while (loop)
            {
                string[] text = { $"\n\n\tРежим работы:\n\t{GetTextModeName()}", $"\n\n\n\tНастройки директивы\n\t{GetSettingsDirective()}", $"\n\n\n\tПуть к директиве\n\t{m_path}", $"\n\n\n\tПеревести с \n\t{m_fromLanguage}", $"\n\n\n\tПеревести на \n\t{m_toLanguage}", "\n\n\n\tСтарт" };
                for (int i = 0; i < text.Length; i++)
                {
                    if (i == idMenu)
                    {
                        Print(text[i], ConsoleColor.Green, ConsoleColor.White, 0);
                    }
                    else
                    {
                        Console.WriteLine(text[i]);
                    }

                }
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        {
                            switch (idMenu)
                            {
                                case 0:
                                    {
                                        m_textMode = ITranslateTextMode.SetupMode();
                                        break;
                                    }
                                case 1:
                                    {
                                        m_settingsDirective = ISettingsDirective.SetupSettingsDirective();
                                        break;
                                    }
                                case 2:
                                    {
                                        Path();
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("С какого языка перевести:");
                                        m_fromLanguage = Convert.ToString(Console.ReadLine());
                                        break;
                                    }
                                case 4:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("На какой язык перевести:");
                                        m_toLanguage = Convert.ToString(Console.ReadLine());
                                        break;
                                    }
                                case 5:
                                    {
                                        Start();
                                        break;
                                    }
                                default:
                                    {
                                        Console.Clear();
                                        Print("\n\n\t\tНеверный ввод");
                                        break;
                                    }
                            }
                            break;
                        }
                    case ConsoleKey.D1:
                        {
                            idMenu = 0;
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            idMenu = 1;
                            break;
                        }
                    case ConsoleKey.D3:
                        {
                            idMenu = 2;
                            break;
                        }
                    case ConsoleKey.D4:
                        {
                            idMenu = 3;
                            break;
                        }
                    case ConsoleKey.D5:
                        {
                            idMenu = 4;
                            break;
                        }
                    case ConsoleKey.D6:
                        {
                            idMenu = 5;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            loop = false;
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Print("\n\n\t\tНеверный ввод");
                            break;
                        }
                }

                Console.Clear();
            }
        }


        private void Path()
        {
            Console.Clear();
            Console.Write("\n\n\tВведите путь к папке:");
            m_path = Convert.ToString(Console.ReadLine());
        }

        private void Start()
        {
            if (IsStart())
            {
                m_settingsDirective.StartWork(m_textMode, m_path, m_fromLanguage, m_toLanguage);
            }
        }
    }

    //public abstract class ITranslateWordsMode
    //{
    //    protected string m_fromLanguage;
    //    protected string m_toLanguage;
    //    public abstract string TranslateWords(string word);

    //    virtual public bool IsLanguage()
    //    {
    //        if(m_fromLanguage == null)
    //        {
    //            return false;
    //        }

    //        if (m_toLanguage == null)
    //        {
    //            return false;
    //        }

    //        return true;
    //    }
    //    virtual public string GetFromLanguage()
    //    {
    //        return m_fromLanguage;
    //    }
    //    virtual public string GetToLanguage()
    //    {
    //        return m_toLanguage;
    //    }
    //    virtual public void SetFromLanguage(string fromLanguage)
    //    {
    //        m_fromLanguage = fromLanguage;
    //    }
    //    virtual public void SetToLanguage(string toLanguage)
    //    {
    //        m_toLanguage = toLanguage;
    //    }
    //}

    //internal class TranslateWordsMode : ITranslateWordsMode
    //{
    //    public override string TranslateWords(string word)
    //    {
    //        var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={m_fromLanguage}&tl={m_toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
    //        var webClient = new WebClient
    //        {
    //            Encoding = System.Text.Encoding.UTF8
    //        };
    //        var result = webClient.DownloadString(url);
    //        return result;
    //    }

    //}
}
