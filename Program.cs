using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TranslateProjectZomboid
{
    internal class Program
    {
        static void Main()
        {
            //string results = TranslateText("Hello, guys", "en", "ru");
            FileManagerTranslate();
            //Console.WriteLine(results);
        }

        public static void FileManagerTranslate()
        {
            string line;
            List<string> files = new List<string>();
            try
            {
                StreamReader sr = new StreamReader("E:\\Test\\Sandbox_RU.txt");         
                line = sr.ReadToEnd();

                //Console.WriteLine(line);

                int indexStart = 0;
                int indexEnd = 0;
                string translate = "";


                indexStart = line.IndexOf('\"', indexEnd + 1 + translate.Length);
                indexEnd = line.IndexOf('\"', indexStart + 1);

                Console.WriteLine("Length: {0}", line.Length);
                while (indexEnd != -1 && indexStart != -1)
                {
                    if (indexStart >= 0 && indexEnd >= 0)
                    {
                        translate = line.Substring(indexStart + 1, indexEnd - 1 - indexStart);
                        translate = TranslateText(translate);
                        line = line.Remove(indexStart + 1, indexEnd - 1 - indexStart);
                        line = line.Insert(indexStart + 1, translate);

                        indexStart = line.IndexOf('\"', indexStart + 2 + translate.Length);
                        indexEnd = line.IndexOf('\"', indexStart + 1);
                    }
                    else
                    {
                        Console.WriteLine("{0} - {1} Error", indexStart, indexEnd);
                    }
                }
                Console.WriteLine("_______________________________\n\n\n");
                Console.WriteLine(line);
                sr.Close();
                //WriterManagerFile(ref files, ref line);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
        public static void WriterManagerFile(ref List<string> files, ref string line)
        {
            try
            {
                StreamWriter sr = new StreamWriter("E:\\Sandbox_RU.txt", true);
                //StreamWriter sw = new StreamWriter("E:\\Sandbox_RU.txt");
                //Console.WriteLine(line);
                int indexStart = 0;
                int indexEnd = 0;

                while (indexStart != -1)
                {
                    indexStart = line.IndexOf('\"', indexEnd + 1);
                    indexEnd = line.IndexOf('\"', indexStart + 1);
                    if (indexStart >= 0 && indexEnd >= 0)
                    {
                        
                        //Console.WriteLine("{0}\t\t\t\t{1}", translate, TranslateText(translate));
                        //Console.WriteLine("Text: {0}", TranslateText(translate));
                    }
                }
                
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
        public static string TranslateText(string word, string fromLanguage = "en", string toLanguage = "ru")
        {
            //var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }
    }
}
