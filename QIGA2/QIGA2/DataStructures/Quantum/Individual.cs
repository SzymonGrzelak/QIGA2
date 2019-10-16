using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures
{
    class Individual : IEnumerator, IEnumerable
    {
        private int position = -1;
        public Register[] Chromosome { get; set; }

        public object Current => this.Chromosome[position];

        public Individual(int chromosomeLenght, int registerLenght)
        {
            this.Chromosome = new Register[chromosomeLenght / registerLenght];
            for (int i = 0; i < this.Chromosome.Length; i++) this.Chromosome[i] = new Register(registerLenght);
        }
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
