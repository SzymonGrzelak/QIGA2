using QIGA.DataStructures;
using QIGA.DataStructures.Classic;
using QIGA.Problems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QIGA.Algorithm
{
    public class QIGA2
    {
        private Population quantumPopulation;
        private ClassicPopulation classicPopulation;
        private Random random = new Random();
        private IProblem<BitArray, float> problem;
        private float[] fval;
        private ClassicIndividual bestIndividual;
        private int populationSize;

        public QIGA2(IProblem<BitArray, float> problem, int populationSize)
        {
            this.problem = problem;
            this.fval = new float[populationSize];
            this.populationSize = populationSize;
        }

        void initialize(int populationSize, int chromosomeLength)
        {
            this.quantumPopulation = new Population(populationSize, chromosomeLength, 2);
            this.classicPopulation = new ClassicPopulation(populationSize, chromosomeLength);
        }

        void observe()
        {
            //todo: linq.zip; foreach
            for(int i=0; i<quantumPopulation.Individuals.Length; i++)
            {
                //Console.ReadLine();
                for (int j = 0; j < quantumPopulation.Individuals[i].Chromosome.Length; j++)
                {
                    //Console.WriteLine("Individual " + i + "gene number" + j);
                    double r = random.NextDouble(); //random.NextDouble() * (maxValue - minValue) + minValue;
                   // Console.WriteLine("random = " + r);
                    if (r < Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Qubits[0], 2))
                    {
                        classicPopulation.Individuals[i].Chromosome[j*2] = false;
                        classicPopulation.Individuals[i].Chromosome[j*2+1] = false;
                       // Console.WriteLine("00");
                    }
                    else if(r< Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Qubits[0], 2)+ Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Qubits[1], 2))
                    {
                        classicPopulation.Individuals[i].Chromosome[j * 2] = false;
                        classicPopulation.Individuals[i].Chromosome[j * 2 + 1] = true;
                       // Console.WriteLine("01");
                    }
                    else if(r< Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Qubits[0], 2) + Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Qubits[1], 2) + Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Qubits[3], 2))
                    {
                        classicPopulation.Individuals[i].Chromosome[j * 2] = true;
                        classicPopulation.Individuals[i].Chromosome[j * 2 + 1] = false;
                       // Console.WriteLine("10");
                    }
                    else
                    {
                        classicPopulation.Individuals[i].Chromosome[j * 2] = true;
                        classicPopulation.Individuals[i].Chromosome[j * 2 + 1] = true;
                      //  Console.WriteLine("11");
                    }
                   
                }
               // Console.WriteLine("Długość chromosomu: " + classicPopulation.Individuals[i].Chromosome.Length);
            }


        }

        //knapsack only
        void repair ()
        {
            for (int i = 0; i < classicPopulation.Individuals.Length; i++)
            {
                this.problem.Repair(classicPopulation.Individuals[i].Chromosome);
            }
        }

        void evaluate()
        {
            Console.WriteLine("EVALUATE: ");
            for(int i=0; i<classicPopulation.Individuals.Length; i++)
            {
                Console.WriteLine(this.problem.Evaluate(classicPopulation.Individuals[i].Chromosome));
                this.fval[i]=this.problem.Evaluate(classicPopulation.Individuals[i].Chromosome);
            }
        }

        //todo: zdecyduj się, parametry czy nie
        void update(ClassicIndividual bestIndividual)
        {
            for (int i = 0; i < quantumPopulation.Individuals.Length; i++)
            {
                for(int j=0; j<quantumPopulation.Individuals[i].Chromosome.Length; j++)
                {
                    double sum = 0;
                    int bestamp = Bin2dec(CopySlice(bestIndividual.Chromosome, j, 2));
                    for (int amp = 0; amp < quantumPopulation.Individuals[i].Chromosome[j].Qubits.Length; amp++)
                    {
                        if(amp != bestamp)
                        {
                            //todo: no to już poszalałem
                            sum+=Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Qubits[amp] *= 0.99,2);
                        }
                    }
                    quantumPopulation.Individuals[i].Chromosome[j].Qubits[bestamp] = Math.Sqrt(1 - sum);
                }
            }
        }

        void storebest()
        {//todo: przenieś fval do struktur danych, potem linq.max
            int i;
            float val = -1;
            for (i = 0; i < classicPopulation.Individuals.Length; i++)
            {
                if (this.fval[i] > val)
                {
                    val = this.fval[i];
                    this.bestIndividual = classicPopulation.Individuals[i];
                }
            }
        //todo: pomysł nowotniaka, stop gdy funkcja przystosowania przestanie się poprawiać, bestval
            
        }


        private BitArray CopySlice(BitArray source, int offset, int length)
        {
            BitArray ret = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                ret[i] = source[offset + i];
            }
            return ret;
        }
        private int Bin2dec(BitArray bitArray)
        {
            var result = new int[1];
            bitArray.CopyTo(result, 0);
            return result[0];
        }

        public float Getx(BitArray individual, float min, float max)
        {
            float x = Bin2dec(individual);
            x = min + x * (max - min) / (MathF.Pow(2, individual.Count) - 1);
            return x;
        }

        public void solve(int tmax)
        {
            int t = 0;
          //bestval = -1;
          //todo: nie wszytsko jest wyciągnięte
            initialize(this.populationSize,20);
            observe();
            repair();
            evaluate();
            Array.ForEach(fval, Console.WriteLine);
            storebest();
            //todo: tu da się krócej, może wszystko wrzucić w initialize
            while (t < tmax)
            {
                observe();
                repair();
                evaluate();
                update(this.bestIndividual);
                storebest();
                t++;
                Console.WriteLine("pokolenie: "+ t);
                Console.WriteLine("w przestrzeni problemu (oś x)");
                Console.WriteLine(Getx(this.bestIndividual.Chromosome, 0, 20));
                Console.WriteLine("w przestrzeni problemu (funkcja przystosowania)");
                Console.WriteLine(this.problem.Evaluate(this.bestIndividual.Chromosome));
                Console.WriteLine("fval:");
                Array.ForEach(fval, Console.WriteLine);
            };
            Console.WriteLine("best solution: ");
            Console.WriteLine("chromosome binary");
            Console.WriteLine(this.bestIndividual.Chromosome);
            Console.WriteLine("chromosome decimal");
            Console.WriteLine(Bin2dec(this.bestIndividual.Chromosome));
            Console.WriteLine("w przestrzeni problemu (oś x)");
            Console.WriteLine(Getx(this.bestIndividual.Chromosome, 0, 20));
            Console.WriteLine("w przestrzeni problemu (funkcja przystosowania)");
            Console.WriteLine(this.problem.Evaluate(this.bestIndividual.Chromosome));

            //printf("\nfitness: %f\n", bestval);
        }
    }
    
}


