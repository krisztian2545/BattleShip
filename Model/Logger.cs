﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static string LogAndReturn(string smt)
        {
            Log("Logging and returning: " + smt);
            return smt;
        }
    }
}