using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures.Classic
{
    class Gene : IEnumerator, IEnumerable
    {
        private int position = -1;

        public BitArray Bits{ get; set; }

        public object Current => this.Bits[position];

        //todo: zrób porządek z tym 0.5
        public Gene(int size)
        {
            this.Bits = new BitArray(size);
        }

        public bool MoveNext()
        {
            position++;
            return (position < this.Bits.Length);
        }

        public void Reset()
        {
            position = 0;
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
    }
}
