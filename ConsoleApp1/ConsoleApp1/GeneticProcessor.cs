using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class GeneticProcessor
    {
        public List<Chromosome> CurrentSet { get; set; }
        public GeneticProcessor()
        {
            this.CurrentSet = new List<Chromosome>();
            Random random = new Random();

            for (int j = 0; j < Config.ChromosomeCount; j++)
            {
                CurrentSet.Add(new Chromosome());

                for (int i = 0; i < Config.VariableCount; i++)
                {
                    CurrentSet[j].Genes.Add(random.Next(Config.MinRandomValue, Config.MaxRandomValue));
                }
            }

            CalculateAdaptation();
            PrintSet(CurrentSet);
        }

        public void Mutate()
        {
            Random random = new Random();
            for(int i = 0; i < Config.MutatedChromosomeCount; i++)
            {
                CurrentSet[random.Next(0, Config.ChromosomeCount)]
                    .Genes[random.Next(0, Config.VariableCount)] = random.Next(Config.MinRandomValue, Config.MaxRandomValue);
            }
        }

        public void CalculateAdaptation()
        {
            for (int i = 0; i < Config.ChromosomeCount; i++)
            {
                for (int j = 0; j < Config.VariableCount; j++)
                {
                    CurrentSet[i].AdaptationFunctionResult +=
                        CurrentSet[i].Genes[j] * Config.Constants[j];
                }
                CurrentSet[i].AdaptationCoefficientReverted = 1 / (double)(Config.RightHandedValue - CurrentSet[i].AdaptationFunctionResult);
            }

            double totalRevertedCoefficient = CurrentSet.Sum(ch => ch.AdaptationCoefficientReverted);

            for (int i = 0; i < Config.ChromosomeCount; i++)
            {
                CurrentSet[i].AdaptationCoefficient = (CurrentSet[i].AdaptationCoefficientReverted / totalRevertedCoefficient) * 100;
            }
        }

        public void MoveGeneration()
        {
            Random random = new Random();
            double averageAdaptation = this.GetAverageAdaptationCoefficient();
            List<Chromosome> nextGenerationList = new List<Chromosome>();
            List<Chromosome> chosenParents = CurrentSet
                .Where(ch => ch.AdaptationCoefficient >= averageAdaptation)
                .ToList();

            for (int i = 0; i < Config.ChromosomeCount; i++)
            {
                List<int> chilrenGenes = new List<int>();

                int firstParentId = random.Next(0, chosenParents.Count);
                int secondParentId;

                do {
                    if(chosenParents.Count == 1)
                    {
                        chosenParents.Add(CurrentSet.Where(ch => ch.Genes != chosenParents.First().Genes).First());
                        secondParentId = 1;
                        break;
                    }
                    secondParentId = random.Next(0, chosenParents.Count);
                }
                while (secondParentId == firstParentId);



                (Chromosome, Chromosome) parents = (chosenParents[firstParentId], chosenParents[secondParentId]);
                int separationGeneId = random.Next(1, Config.VariableCount - 1);

                for (int j = 0; j < separationGeneId; j++)
                {
                    chilrenGenes.Add(parents.Item1.Genes[j]);
                }
                for (int k = separationGeneId; k < Config.VariableCount; k++)
                {
                    chilrenGenes.Add(parents.Item2.Genes[k]);
                }
                nextGenerationList.Add( new Chromosome { Genes = chilrenGenes} );
            }

            CurrentSet = nextGenerationList;
        }

        public void PrintBestChild()
        {
            Chromosome bestChild = CurrentSet
                .OrderBy(ch => ch.AdaptationCoefficient)
                .Select(ch => ch).Last();
            int averageAdaptation = this.GetAverageAdaptation();

            Console.WriteLine("-------------GENES-------------");
            foreach (int g in bestChild.Genes)
            {
                Console.Write($"{g}\t");
            }
            Console.WriteLine($"------------Adaptation value: {bestChild.AdaptationFunctionResult}");
            Console.WriteLine($"------------Average adaptation: {averageAdaptation}");

        }

        public void PrintSet(List<Chromosome> set)
        {
            Console.WriteLine("-------------GENES-------------");
            foreach (var ch in set)
            {
                foreach (int g in ch.Genes)
                {
                    Console.Write($"{g}\t");
                }
                Console.WriteLine($"------------Adaptation value: {ch.AdaptationFunctionResult}");
            }

        }

        private int GetAverageAdaptation()
        {
            return CurrentSet
                .Sum(ch => ch.AdaptationFunctionResult)
                    / Config.ChromosomeCount;
        }
        private double GetAverageAdaptationCoefficient()
        {
            return CurrentSet
                .Sum(ch => ch.AdaptationCoefficient)
                    / Config.ChromosomeCount;
        }
    }
}
