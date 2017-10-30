using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
//using System.Threading.Tasks;

namespace DA59 {

    //This interface is for exposing dll functions to COM client. It will enable Intellisense in VB6 when early binding is used.

    [Guid("fac7c082-2d7b-4509-82d8-51df3e54f1ee")]
    [ComVisible(true)]
    public interface IDA59
    {
        bool setInputFile(string inputPath);
        bool setOutputFile(string outputPath);
        string getInputFilePath();
        string getOutputFilePath();
        int getPlaceHolderCounter(string line, string checkChar);
        bool isValid(string line, string checkChar);
        string replaceValues(string token, string line, string outputValue);
        string deleteLine(string line, string token);
        void writeLine(string line);
        string getLine();
        bool close();
        bool paginate();
        bool hasNext();

    }
    [Guid("fac7c082-2d7c-4508-82f8-51df3e54f1ee")]
    [ComVisible(true)]
    public class DA59 : IDA59
    {
             
        public StreamReader inputFile;
        public StreamWriter outputFile;
        public Regex placeHolder;
        public string inputFilePath;
        public string outputFilePath;
        public int placeHolderCounter;

        public DA59() { } 
        public DA59(string input, string output)
            {
                inputFile = new StreamReader(input);
                outputFile = new StreamWriter(output);
            }

        public bool setInputFile(string inputPath) {
                try
                {
                    this.inputFile = new StreamReader(inputPath);
                    this.inputFilePath = inputPath;
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        public bool setOutputFile(string outputPath)
                {
                try
                {
                    this.outputFile = new StreamWriter(outputPath);
                    this.outputFilePath = outputPath;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                } 
            }

        public string getInputFilePath()
        {
            return inputFilePath;
        }

        public string getOutputFilePath()
        {
            return outputFilePath;
        }

        public int getPlaceHolderCounter(string line, string checkChar)
        {
            Regex tokenMark = new Regex(checkChar);
            placeHolderCounter = tokenMark.Matches(line).Count;
            return placeHolderCounter; 
        }
        
        public bool isValid(string line, string checkChar)
            {
                Regex tokenMark = new Regex(checkChar);

                if (tokenMark.IsMatch(line))
                {
                
                    return true;
                }
                else
                {
                    return false;
                }
            }
        
        public string replaceValues(string token, string line, string outputValue)
            {

                placeHolder = new Regex(token);
            try
            {

                if (placeHolder.IsMatch(line))
                {
                    line = placeHolder.Replace(line, outputValue);
                  
                }
                
                return line;
            }
            catch (Exception)
            {
                return line;
            }
            }

        public string deleteLine(string line, string token)
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
        
            public void writeLine(string line)
            {
                outputFile.WriteLine(line);
            }

            public string getLine()
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
               
            public bool paginate()
            {
                outputFile.WriteLine(@"{\page }");
                return true;
            }

            public bool hasNext()
            {
            if (getLine() != null)
                {
                return true;
                }
                return false;
            }
        }
    }


