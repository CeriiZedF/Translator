using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate
{
    internal class SDCurrentFolder : ISettingsDirective
    {
        public SDCurrentFolder(string name) : base(name) { }

        public override void StartWork(ITranslateTextMode mode, string path, string fromLanguage, string toLanguage)
        {
           
            string[] files = Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)
            {
                List<string> arr = new List<string>();
                using (StreamReader sr = new StreamReader(files[i]))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = mode.GetTranslateText(line, fromLanguage, toLanguage);
                        line = line.Substring(4, line.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                        arr.Add(line);
                    }
                }
                using (StreamWriter sw = new StreamWriter(files[i].Insert(files[i].Length - 4, toLanguage)))
                { 
                    for (int j = 0; j < arr.Count; j++)
                    {
                        sw.WriteLine(arr[j]);
                    }
                }
            }

        }
    }
}
