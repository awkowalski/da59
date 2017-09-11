using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication5.library
{
    class RTF_Handler
    {
        private StreamReader inputFile;
        private StreamWriter outputFile;
        private Regex placeHolder;

        public RTF_Handler(string input, string output)
        {
            inputFile = new StreamReader(input);
            outputFile = new StreamWriter(output);
        }

        public void replaceValues(string token, string line, string outputValue)
        {
            placeHolder = new Regex(token);

            if (token == "!GENREM" && placeHolder.IsMatch(line))
            {
                line = @"{\rtlch\fcs1 \af1\afs20 \ltrch\fcs0 \f1\fs20\cf2 }";
                outputFile.WriteLine(line);
                return;
            }
            placeHolder.Replace(line, outputValue);
            outputFile.WriteLine(line);

        }

        public string  getLine()
        {
            try
            {
                return inputFile.ReadLine();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool close()
        {
            try
            {
                outputFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
