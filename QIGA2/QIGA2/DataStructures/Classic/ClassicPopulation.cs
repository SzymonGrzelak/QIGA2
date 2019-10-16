using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures.Classic
{
    class ClassicPopulation 
    {
        private int position = -1;
        public ClassicIndividual[] Individuals { get; set; }

        public object Current => this.Individuals[position];
        public ClassicPopulation(int size, int chromosomeLenght)
        {
            this.Individuals = new ClassicIndividual[size];
            for (int i = 0; i < this.Individuals.Length; i++) this.Individuals[i] = new ClassicIndividual(chromosomeLenght);
      
        }
          
        //public ClassicPopulation(int size, int chromosomeLenght, int registerLenght) => this.Individuals = Enumerable.Repeat(new ClassicIndividual(chromosomeLenght, registerLenght), size).ToArray();

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < this.Individuals.Length);
        }

        public void Reset()
        {
            position = 0;
        }
    }
}
