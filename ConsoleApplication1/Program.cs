using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;



namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {

            Keywrods a = new Keywrods();
            a.Func();

            string[] file = File.ReadAllLines("code.txt");
            a.TokenGen(file);

            Console.ReadKey();
        }
    }

    class Keywrods
    {
        public void Func()
        {
            op();
            punc();
            keyword();
        }

        string CP;
        public string[] linechange = { "\n" };

        public void tokensPrint(string Cp, string Vp, int Line)
        {
            File.AppendAllText("code.txt", "( ");
            File.AppendAllText("code.txt", Cp);
            File.AppendAllText("code.txt", ", ");
            File.AppendAllText("code.txt", Vp);
            File.AppendAllText("code.txt", ", ");
            File.AppendAllText("code.txt", Convert.ToString(Line));
            File.AppendAllText("code.txt", ")");
            File.AppendAllLines("code.txt", linechange);
            
                
        }
        Dictionary<string, string> keywords = new Dictionary<string, string>();
        public void keyword()
        {
            keywords.Add("int", "DT"); //Integer
            keywords.Add("float", "DT"); //Float
            keywords.Add("char", "DT"); //Char
            keywords.Add("string", "DT"); //String
            keywords.Add("bool", "DT"); //Bool
            keywords.Add("open", "AccessMod"); //Public
            keywords.Add("close", "AccessMod"); //Private
            keywords.Add("protected", "AccessMod"); //Protected
            keywords.Add("true", "TF"); //True
            keywords.Add("false", "fl"); //False
            keywords.Add("abstract", "abstract"); //Abstract
            keywords.Add("static", "static"); //Static
            keywords.Add("virtual", "virtual"); //Virtual
            keywords.Add("override", "override"); //override
            keywords.Add("sealed", "sealed"); //sealed
            keywords.Add("class", "class"); //class            
            keywords.Add("interface", "interface"); //Interface
            keywords.Add("null", "null"); //null
            keywords.Add("const", "const"); //const
            keywords.Add("default", "default"); //default
            keywords.Add("while", "while"); //while
            keywords.Add("for", "for"); //for
            keywords.Add("if", "if"); //if
            keywords.Add("else", "else"); //else
            keywords.Add("new", "new"); //new
            keywords.Add("return", "return"); //return
        }

        Dictionary<string, string> punctuators = new Dictionary<string, string>();
        public void punc()
        {
            punctuators.Add(".", ".");//Dot
            punctuators.Add(",", ",");//Separator
            punctuators.Add("(", "(");//(
            punctuators.Add(")", ")");//)
            punctuators.Add("\t", "\t");//{}
            punctuators.Add("[", "[");//[
            punctuators.Add("]", "]");//]
            punctuators.Add(":", ":");//:
            punctuators.Add("\r", "\r");//;
        }

        Dictionary<string, string> operators = new Dictionary<string, string>();
        public void op()
        {
            operators.Add("+", "MP");//+
            operators.Add("-", "MP");//-
            operators.Add("/", "DMM");// /
            operators.Add("*", "DMM");//*
            operators.Add("%", "DMM");//%
            operators.Add("--", "IncDec");//--
            operators.Add("++", "IncDec");//++
            operators.Add("=", "=");//=
            operators.Add("+=", "AssignOp");//+=
            operators.Add("-=", "AssignOp");//-=
            operators.Add("*=", "AssignOp");//*=
            operators.Add("/=", "AssignOp");///=
            operators.Add("%=", "AssignOp");//%=
            operators.Add("&&", "LogicOp");//&&
            operators.Add("||", "LogicOp");//||
            operators.Add("!", "LogicOp");//!
            operators.Add("<", "RelOp");//<
            operators.Add(">", "RelOp");//>
            operators.Add("!=", "RelOp");//!=
            operators.Add(">=", "RelOp");//>=
            operators.Add("<=", "RelOp");//<=
            operators.Add("==", "RelOp");//==

        }
        public bool CheckKeyWord(string word)
        {
            if (keywords.ContainsKey(word))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Checkpunc(string word)
        {
            if (punctuators.ContainsKey(word))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Checkop(string word)
        {
            if (operators.ContainsKey(word))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Checkidentifier(string word)
        {
            string exp = @"[0-9]?[a-zA-Z]+[0-9]?";
            bool pat = Regex.IsMatch(word, exp);
            if (pat == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkInteger(string word)
        {
            Regex pat = new Regex("[0-9]+");
            return pat.IsMatch(word);

        }

        public bool checkString(string word)
        {
            Regex pat = new Regex("^\"[a-zA-Z0-9 ]*\"");
            return pat.IsMatch(word);
        }

        public bool checkBoolean(string word)
        {
            Regex pat = new Regex("true|false");
            return pat.IsMatch(word);

        }
        public bool checkfloat(string word)
        {
            
            Regex pat = new Regex("^[-+]?[0-9]*.[0-9]+$");
            return pat.IsMatch(word);
        }

        public bool checkChar(string word)
        {
            Regex pat = new Regex("^'[0-9A-Za-z]{1}'");
            return pat.IsMatch("'aabc'");
        }
        public void TokenGen(string[] file)
        {
            int LineNo = 1;
            string VP = null;
            string cp = "Invalid Lexeme";

            for (int i = 0; i < file.Length; i += 2)
            {
                VP = file[i];
                if (i != file.Length - 2)
                {
                    if (file[i] == "" && file[i + 2] == "")
                    {
                        i += 2;
                    }
                    if (file[i] == "")
                    {
                        LineNo++;
                        continue;
                    }
                }


              else if  (checkInteger(VP))
                {
                    Console.WriteLine("(" + "IntConst" + " , " + VP + " , " + LineNo + ")");
                }
                else if (checkfloat(VP))
                {
                    Console.WriteLine("(" + "FloatConst" + " , " + VP + " , " + LineNo + ")");
                }
                else if (checkChar(VP))
                {
                    Console.WriteLine("(" + "CharConst" + " , " + VP + " , " + LineNo + ")");
                }
                else if (checkString(VP))
                {
                    Console.WriteLine("(" + "StringConst" + " , " + VP + " , " + LineNo + ")");
                }
                else
                {
                    Console.WriteLine("Invalid");
                }
            }


        }
    }
}
