using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication5.library;

namespace ConsoleApplication5
{
    class Program
    {
        private static string line;

        static void Main(string[] args)
        {
            string inputPath = @"E:\ETG\HE\test.rtf";
            string outputPath = @"E:\ETG\HE\test_output.rtf";
        
            RTF_Handler my_rtf = new RTF_Handler(inputPath, outputPath);

            while((line = my_rtf.getLine()) != null)
            {
                my_rtf.replaceValues("!GENREM", line, "TEST");
            }
            my_rtf.close();
        }
    }
}
