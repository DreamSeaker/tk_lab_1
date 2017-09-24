#define DEB

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tk_lab1
{
    enum MachStates
    {
        ScanNormal = 1,
        ScanQuoted = 2,
        ScanPunctuation = 3
    }

    class Program
    {
        public const string restricted_chars = " !<>{}[](),.?;:-+=*/&";
        static void Main(string[] args)
        {
            string StringPlane = "что !,:? ты сейчас \"сделал\"?";
            MachStates State = MachStates.ScanNormal;//init to 1
            char CurChar;
            string CurWord = "";

            List<string> finalList = new List<string>() { "" };//создаем пустой лист
            //заготовка под ввод с консоли
            /*
             *Console.ReadLine()...
            if(!Plane.length)
                >gg, wp, close*/
            Console.WriteLine(StringPlane);
            Console.WriteLine("\n");

            for (int Inx = 0; Inx < StringPlane.Length; Inx++)
            {
                CurChar = StringPlane[Inx];

                /*обработка состояния*/
                switch (State)
                {
                    case MachStates.ScanNormal:
#if (DEB)
                        Console.WriteLine("Entered Scan Normal!");
#endif
                        if (CurChar == '\"')
                        {
                            if (CurWord != "")
                                finalList.Add(CurWord);
                            CurWord = "\"";
                            State = MachStates.ScanQuoted;
                        }
                        //то есть Текущий символ не был найден в списке restricted
                        else if (ScanRestrictedChars(CurChar, restricted_chars) != 0)
                        {
                            if (CurWord != "")
                            {
                                finalList.Add(CurWord);
                                CurWord = "";
                            }
                            State = MachStates.ScanPunctuation;
                        }
                        else
                            CurWord += CurChar;
                        break;

                    case MachStates.ScanQuoted:
#if (DEB)
                        Console.WriteLine("Entered Scan Quoted!");
#endif
                        CurWord += CurChar;
                        if(CurChar == '\"')
                        {
                            finalList.Add(CurWord);
                            CurWord = " ";
                            State = MachStates.ScanNormal;
                        }
                        break;

                    case MachStates.ScanPunctuation:
#if (DEB)
                        Console.WriteLine("Entered Scan Punc!");
#endif
                        if (CurChar == '"')
                        {
                            CurWord = "\"";
                            State = MachStates.ScanQuoted;
                        }
                        else if(ScanRestrictedChars(CurChar, restricted_chars) == 0)
                        {
                            CurWord = CurChar.ToString();
                            State = MachStates.ScanNormal;
                        }

                        break;

                }
                /*if(State == MachStates.ScanQuoted) { Console.WriteLine("ERROR:по достижению строки текущее состояние - ScanQuoted"); }
                if (CurWord != " ")
                    finalList.Add(CurWord);*/
            }
            finalList.ForEach(delegate(String name)
            {
                Console.Write(name);
            }
                );
            Console.ReadLine();
        }
        public static int ScanRestrictedChars(Char CurChar, string S)
        {
        int Result = 0;
        Char[] locCharArray = S.ToCharArray();
        for(int i = 0; i < locCharArray.Length; i++)
             if(locCharArray[i] == CurChar) {
                Result = i;
                return Result;
             }
        return 0;
        }

    }
}
