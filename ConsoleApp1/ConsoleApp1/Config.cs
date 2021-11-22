using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    static class Config
    {
        public static int[] Constants = new int[] { 14, 2, 11, 4, 1 };

        public static int RightHandedValue = 10;

        public static int VariableCount = Config.Constants.Length;

        public static int ChromosomeCount = 10;

        public static int MutatedChromosomeCount = 2;

        public static int MinRandomValue = 0;

        public static int MaxRandomValue = 100;
    }
}
