using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Model
{
    public static class Logger
    {

        public static bool LogToFile = false;

        public static void Log(string message)
        {
            Debug.WriteLine(message);

            if (LogToFile)
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("log.txt", true))
                {
                    file.WriteLine(message);
                }
            }
        }

        public static string LogAndReturn(string smt)
        {
            Log("Logging and returning: " + smt);
            return smt;
        }

        public static string TwoDimArrayToString<T>(T[,] coll)
        {
            string output = "{\n";
            for(int i = 0; i < coll.GetLength(0); i++)
            {
                output += "[ ";
                for (int j = 0; j < coll.GetLength(1); j++)
                {
                    output += $"{coll[j, i]} ";
                }
                output += "]\n";
            }
            output += "}";
            return output;
        }
    }
}
