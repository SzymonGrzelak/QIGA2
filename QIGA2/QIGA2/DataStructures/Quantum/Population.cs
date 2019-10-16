using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures
{
    class Population : IEnumerator, IEnumerable
    {
        private int position = -1;
        public Individual[] Individuals { get; set; }

        public object Current => this.Individuals[position];

        public Population(int size, int chromosomeLenght, int registerLenght) 
        {
            this.Individuals = new Individual[size];
            for (int i = 0; i < this.Individuals.Length; i++) this.Individuals[i] = new Individual(chromosomeLenght,registerLenght);
        }

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
