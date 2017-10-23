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
        bool isValid(string line, string checkChar);
        bool replaceValues(string token, string line, string outputValue);
        void deleteLine();
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
        
            public bool replaceValues(string token, string line, string outputValue)
            {

                placeHolder = new Regex(token);
            try
            {

                if (placeHolder.IsMatch(line))
                {
                    //if (token == "!GENREM")
                    //{
                        //line = deleteLine();
                                          
                    //}
                    //else
                    //{
                        line = placeHolder.Replace(line, outputValue);
                    //}

                    //return true;
                }
                outputFile.WriteLine(line);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            }

            public void deleteLine()
            {
                outputFile.WriteLine( @"{\rtlch\fcs1 \af1\afs20 \ltrch\fcs0 \f1\fs20\cf2 }");
            
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



