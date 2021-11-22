using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using DataAnnotationValidator = System.ComponentModel.DataAnnotations.Validator;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticProcessor processor = new GeneticProcessor();
            int i = 0;
            while (true)
            {
                if(processor.CurrentSet
                .Where(ch => ch.AdaptationFunctionResult == Config.RightHandedValue)
                .Any())
                {
                    processor.PrintSet(processor.CurrentSet);
                    break;
                }
                else
                {
                    Console.WriteLine($"Elapsed generations: {i}");
                    i++;
                    processor.MoveGeneration();
                    processor.Mutate();
                    processor.CalculateAdaptation();
                }
            }
            Console.ReadLine();
        }
    }

}
