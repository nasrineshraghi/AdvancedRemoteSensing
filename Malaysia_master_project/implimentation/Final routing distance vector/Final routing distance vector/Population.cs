using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows;

namespace Final_routing_distance_vector
{
    class Population
    {
       public ArrayList chromosomes = new ArrayList();
        ArrayList chromosomeReproducer = new ArrayList();
        ArrayList chromosomeResult = new ArrayList();
        ArrayList chromosomeFamily = new ArrayList();
        public int chromosomeLength;
        const int initialPopulation = 500;
        const int populationLimit = 500;
        int currentpopulation = initialPopulation;
        int cMin;
        int cMax;
        public int generation = 1;
        bool best2 = true;
        Topology topology;
        public Population(int cMin,int cMax,int chromosomeLength,Topology topology)
        {
            this.cMin = cMin;
            this.cMax = cMax;
            this.chromosomeLength = chromosomeLength;
            this.topology = topology;
            int Crossp1 = (int)ListChoromosome.TheSeed.Next(cMin+3, chromosomeLength / 2);
            int CrossP2 = (int)ListChoromosome.TheSeed.Next(Crossp1+1, chromosomeLength-3);
            for (int i = 0; i < initialPopulation; i++)
            {
                ListChoromosome gene = new ListChoromosome(chromosomeLength,cMin,cMax, topology);
                gene.SetCrossoverPoint(Crossp1,CrossP2);
                gene.CalculateFitness(topology);
                chromosomes.Add(gene);
            }
            
        }
        public string WriteNextGeneration()
        {
           
            //Console.WriteLine("Generation {0}\n",generation);
            string x="";
            string output = "Generation " + generation.ToString() + " : \r\n";
            for (int i = 0; i < chromosomes.Count; i++)
            {
                //int x=((ArrayList)((ListChoromosome)chromosomes[i]).TheArray[i]).Count;

                for (int j = 0; j < ((ListChoromosome)chromosomes[i]).TheArray.Count; j = j + 3)
                {
                     x+=(((ListChoromosome)chromosomes[i]).TheArray[j].ToString());
                     x +=","+ (((ListChoromosome)chromosomes[i]).TheArray[j+1].ToString());
                     x += ","+(((ListChoromosome)chromosomes[i]).TheArray[j+2].ToString());
                    // x += ","+(((ListChoromosome)chromosomes[i]).TheArray[j+3].ToString());
                     int numer=ConvertToInt(x);
                     output +=numer.ToString()+" - ";

                    x = "";
                }
                output += "  : " + ((ListChoromosome)chromosomes[i]).CurrentFitness.ToString();
                output+="\r\n";
            }
            return output;
        }
        public int ConvertToInt(string x)
        {
            string[] num = (x.Split(','));
            int y = int.Parse( num[0]) * 4 + int.Parse(num[1]) * 2 + int.Parse(num[2]) * 1;
            return y;
            
        }
        public double Smallest()
        {
            ListChoromosome lc = (ListChoromosome)chromosomes[0];
            double Smallest = lc.CurrentFitness;
            for (int i = 1; i < chromosomes.Count; i++)
            {
                lc = (ListChoromosome)chromosomes[i];
                if (lc.CurrentFitness < Smallest)
                    Smallest = lc.CurrentFitness;
            }
            return Smallest;
        }
        public double LargestFitness()
        {
            ListChoromosome lc = (ListChoromosome)chromosomes[0];
            double largest = lc.CurrentFitness;
            for (int i = 1; i < chromosomes.Count; i++)
            {
                lc = (ListChoromosome)chromosomes[i];
                if (lc.CurrentFitness > largest)
                    largest = lc.CurrentFitness;
            }
            return largest;
        }
        public void NextGeneration()
        {
            generation++;
            int current;
            for (int i = 0; i < chromosomes.Count; i++)
            {
                double minFitness = Smallest();
                ListChoromosome lc = (ListChoromosome)(chromosomes[i]);
                current = i;
                if (lc.CurrentFitness==minFitness)
                {
                    chromosomes.RemoveAt(i);
                    i = current;
                    
                }
                chromosomeReproducer.Clear();
                chromosomeResult.Clear();
                for (int j = 0; j < chromosomes.Count; j++)
                {
                    double largest=LargestFitness();
                    if (((ListChoromosome)chromosomes[j]).CanReproduce(largest, ((ListChoromosome)chromosomes[j]).CurrentFitness))
                        chromosomeReproducer.Add(chromosomes[j]);
                }
                DoCrossover(chromosomeReproducer);
                chromosomes = (ArrayList)chromosomeResult.Clone();
                
                for (int k = 0; k < chromosomes.Count; k++)
                    ((ListChoromosome)chromosomes[k]).Mutate();
                for(int l=0;l<chromosomes.Count;l++)
                    ((ListChoromosome)chromosomes[l]).CalculateFitness(topology);
                
            }
        }
        public void DoCrossover(ArrayList genes)
        {
            ArrayList GeneMom = new ArrayList();
            ArrayList GeneDad = new ArrayList();
            for (int i = 0; i < genes.Count; i++)
            {
                if (ListChoromosome.TheSeed.Next(100) % 2 == 1)
                    GeneMom.Add(genes[i]);
                else
                    GeneDad.Add(genes[i]);
               
            }
            if (GeneMom.Count > GeneDad.Count)
            {
                while (GeneMom.Count > GeneDad.Count)
                {
                    GeneDad.Add(GeneMom[GeneMom.Count - 1]);
                    GeneMom.RemoveAt(GeneMom.Count - 1);
                }
                if (GeneDad.Count > GeneMom.Count)
                {
                    GeneDad.RemoveAt(GeneDad.Count - 1);
                }
            }
            else
            {
                while (GeneDad.Count > GeneMom.Count)
                {
                    GeneMom.Add(GeneDad[GeneDad.Count-1]);
                    GeneDad.RemoveAt(GeneDad.Count-1);
                }
                if (GeneMom.Count > GeneDad.Count)
                    GeneMom.RemoveAt(GeneMom.Count-1);
            }
            for (int j = 0; j < GeneDad.Count; j++)
            {
                ListChoromosome child1=(ListChoromosome)((ListChoromosome)GeneDad[j]).Crossover((ListChoromosome)GeneMom[j]);
                ListChoromosome child2=(ListChoromosome)((ListChoromosome)GeneMom[j]).Crossover((ListChoromosome)GeneDad[j]);
                chromosomeFamily.Clear();
                chromosomeFamily.Add(GeneDad[j]);
                chromosomeFamily.Add(GeneMom[j]);
                chromosomeFamily.Add(child1);
                chromosomeFamily.Add(child2);
                CalculateFitnessForAll(chromosomeFamily);
                chromosomeFamily.Sort();
                if (best2)
                {
                    chromosomeResult.Add(chromosomeFamily[0]);
                    chromosomeResult.Add(chromosomeFamily[1]);
                }
                else
                {

                    chromosomeResult.Add(child1);
                    chromosomeResult.Add(child2);
                }
               
            }
        }
        public void CalculateFitnessForAll(ArrayList genes)
        {
            for (int i = 0; i < genes.Count; i++)
                ((ListChoromosome)genes[i]).CalculateFitness(topology);
                
        }

    }
}
