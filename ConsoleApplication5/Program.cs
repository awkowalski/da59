using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication5.library;
using System.Text.RegularExpressions;

namespace ConsoleApplication5
{
    class Program
    {
        private static string line;

        public static string deleteLine(string line, string token)
        {
            Regex regex = new Regex("({.*" + token + ".*?})");
            Regex regexParagraph = new Regex(@"\\par\b");

            if (regex.IsMatch(line))
            {
                line = regex.Replace(line, "");
                if (Regex.IsMatch(line, @"\\par}"))
                {
                    line = regexParagraph.Replace(line,"");
                }

                return line;
            }
            else
            {
                return line;
            }

        }

        static void Main(string[] args)
        {
            string inputPath = @"D:\TFS\HE\test.rtf";
            string outputPath = @"D:\TFS\HE\test_output.rtf";
        
            RTF_Handler my_rtf = new RTF_Handler(inputPath, outputPath);



            Console.WriteLine(deleteLine(@"\par} {test} 2312", "test"));
            Console.ReadLine();

            //while ((line = my_rtf.getLine()) != null)
            //{
            //    if (my_rtf.isValid(line, "!"))
            //    {
            //         my_rtf.replaceValues("!GENREM", line, "TEST");
            //         my_rtf.replaceValues("!OFFWAR", line, "DUPA");
            //         my_rtf.replaceValues("!ADRANR", line, "ADRES_SPOTTED!");
                    
            //        my_rtf.replaceValues("!GENNXT", line, "GENNXT_SPOTTED!");
            //    }
            //    else
            //    {   
            //        my_rtf.writeLine(line);
            //    }
            //}

            //my_rtf.close();
        }
    }
}
