using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures
{
    class Register : IEnumerator,IEnumerable
    {
        private int position = -1;

        public double[] Qubits{ get; set; } //todo: change qubits to amplitudes

        public object Current => this.Qubits[position];

        //todo: zrób porządek z tym 0.5
        public Register(int size) {
            this.Qubits = new double[(int)Math.Pow(2,size)];
            for (int i = 0; i < this.Qubits.Length; i++) this.Qubits[i] = 0.5;
        }

        public bool MoveNext()
        {
            position++;
            return (position < this.Qubits.Length);
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
