using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Chromosome
    {
        public List<int> Genes { get; set; }

        public int AdaptationFunctionResult { get; set; }

        public double AdaptationCoefficientReverted { get; set; }

        public double AdaptationCoefficient { get; set; }

        public Chromosome()
        {
            Genes = new List<int>();
        }
    }
}
