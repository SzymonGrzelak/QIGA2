using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures.Classic
{
    class ClassicIndividual : IEnumerator, IEnumerable
    {
        private int position = -1;

        //todo: do poprawki zrób Gene[], Genotype => selectmany
        public BitArray Chromosome { get; set; }

        public object Current => this.Chromosome[position];

        public ClassicIndividual(int chromosomeLenght)
        {
            this.Chromosome = new BitArray(chromosomeLenght);
          
        }
        //public ClassicIndividual(int chromosomeLenght, int registerLenght) => this.Chromosome = Enumerable.Repeat<Gene>(new Gene(registerLenght), chromosomeLenght/registerLenght).ToArray();

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < this.Chromosome.Length);
        }

        public void Reset()
        {
            position = 0;
        }
    }
}
