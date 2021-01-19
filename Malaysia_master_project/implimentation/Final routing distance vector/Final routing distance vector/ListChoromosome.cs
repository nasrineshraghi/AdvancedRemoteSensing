using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Final_routing_distance_vector
{
    class ListChoromosome:IComparable
    {
        public static Random TheSeed = new Random();
        public static Random r = new Random();
        public ArrayList TheArray = new ArrayList();
        int Length;
        int TheMin;
        int TheMax;
        int CrossoverPoint1;
        int CrossoverPoint2;
        public int MutationIndex;
        public double CurrentFitness = 0.0;
        
        public ListChoromosome()
        {

        }
        public ListChoromosome(int length,int min,int max, Topology t)
        {
            Length = length;
            TheMin = min;
            TheMax = max;
            string binary="";
            string x = "";
            bool b;
            int count = 0;
            for (int k = 0; k < 3; k++)
                TheArray.Add(0);
            x += "0";
                for (int i = 0; TheArray.Count<Length - 3; i++)
                {
                    int nodeNum = (int)GenerateGeneValue(min + 1, max);
                    if (x == "")
                        x += nodeNum.ToString() ;
                    else
                    {
                        if (!CompareString(x, nodeNum) && CheckValidnum(nodeNum,x,t))
                            x += nodeNum.ToString();

                        else
                        {
                            b = false;
                            while (b == false)
                            {
                                
                                nodeNum = (int)GenerateGeneValue(min + 1, max);
                                if (count == 7)
                                    break;
                                if (!CompareString(x, nodeNum) && CheckValidnum(nodeNum,x,t))
                                {
                                    x += nodeNum.ToString();
                                    b = true;
                                    break;
                                }
                                count++;
                            }
                            count = 0;
                        }
                    }

                    binary = ToBinary(nodeNum);
                    
                    if (binary.Length == 3)
                    {
                        for (int j = 0; j < 3; j++)
                            TheArray.Add(int.Parse(binary.Substring(j, 1)));
                    }
                }
                binary = ToBinary(max);
            
                if (binary.Length == 3)
                {
                    for (int j = 0; j < 3; j++)
                        TheArray.Add(int.Parse(binary.Substring(j, 1)));
                }
            }
        public bool CompareString(string x,int num)
        {
           
           bool flag = false;
           for (int i = 0; i < x.Length; i++)
           {
               
                   int t=int.Parse(x.Substring(i, 1));
                   if (t == num)
                   {
                       flag = true;
                       break;
                   }
           }
           return flag;
        }
        public int CompareTo(object lc)
        {
            ListChoromosome lcCompare = (ListChoromosome)lc;
            return (this.CurrentFitness.CompareTo(lcCompare.CurrentFitness));
        }
       
        public string ToBinary(int number)
        {
            string BinaryResult = "";
            int BinaryHolder;
            char[] BinarryArray;
            if (number == 0)
                BinaryResult = "000";
            else
            {
                while (number > 0)
                {
                    BinaryHolder = number % 2;
                    BinaryResult += BinaryHolder;
                    number = number / 2;
                }
                BinarryArray = BinaryResult.ToCharArray();
                Array.Reverse(BinarryArray);
                BinaryResult = new string(BinarryArray);
            }
            if (BinaryResult.Length != 3)
            {
                if (BinaryResult.Length > 3)
                {
                    int dist = BinaryResult.Length - 3;
                    BinaryResult = BinaryResult.Substring(dist, BinaryResult.Length);
                }
                else
                {
                    int dist = 3 - BinaryResult.Length;
                    for (int j = 0; j < dist; j++)
                        BinaryResult = "0" + BinaryResult;

                }
            }
                return BinaryResult;
            
        }
        public bool CheckValidnum(int numbergenerated,string x,Topology t)
        {

            int neigh = int.Parse(x.Substring(x.Length-1));
            bool b = t.Neighbours((int)neigh, numbergenerated);
            return b;
        }
        public int GenerateGeneValue(int min, int max)
        {
            return (int)(TheSeed.Next(min,max));
        }
        public void SetCrossoverPoint(int cross1, int cross2)
        {
            cross1=CrossoverPoint1;
            cross2=CrossoverPoint2; 
        }
       
        public  double PathFitness(Topology t)
        {
            if (!t.ValidPath(TheArray))
            {
                CurrentFitness = 0;
                return 0.0;
            }

            else
            {
                CurrentFitness = (double)1 / t.CalculateTotalCost(TheArray);
                return CurrentFitness;
            }
           
            
        }
        public double CalculateFitness(Topology t)
        {
            CurrentFitness = PathFitness(t);
            return CurrentFitness;
        }
        public bool CanReproduce(double maxfitness,double fitness)
        {
            if (fitness >= maxfitness)
                return true;
            else
                return false;
        }
        public void CopyGeneInfo(ListChoromosome c)
        {
            ListChoromosome theGene = (ListChoromosome)c;
            theGene.Length = Length;
            theGene.TheMin = TheMin;
            theGene.TheMax = TheMax;
        }
        public ListChoromosome Crossover(ListChoromosome c)
        {
            ListChoromosome aGene1 = new ListChoromosome();
            ListChoromosome aGene2 = new ListChoromosome();
            c.CopyGeneInfo(aGene1);
            c.CopyGeneInfo(aGene2);
            ListChoromosome CrossingGene = (ListChoromosome)c;
            for (int i = 0; i < CrossoverPoint1; i++)
            {
                aGene1.TheArray.Add(CrossingGene.TheArray[i]);
                aGene2.TheArray.Add(TheArray[i]);
            }
            for (int j = CrossoverPoint1; j < CrossoverPoint2; j++)
            {
                aGene1.TheArray.Add(TheArray[j]);
                aGene2.TheArray.Add(CrossingGene.TheArray[j]);
            }
            for (int k = CrossoverPoint2; k < Length; k++)
            {
                aGene1.TheArray.Add(CrossingGene.TheArray[k]);
                aGene2.TheArray.Add(TheArray[k]);
            }
            if (TheSeed.Next(10) <= 7)
            {
                return aGene1;
            }
            else
                return aGene2;
        }
        public  void Mutate()
        {
            if ((double)TheSeed.Next(0, 1) > 0.3)
            {
                MutationIndex = TheSeed.Next(3, Length - 3);
                if ((int)TheArray[MutationIndex] == 0)
                    TheArray[MutationIndex] = 1;
                else
                    TheArray[MutationIndex] = 0;
            }
            
        }
        
        
    }
   
}
