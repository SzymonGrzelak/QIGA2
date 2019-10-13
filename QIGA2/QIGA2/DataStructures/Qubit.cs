using System;
using System.Collections.Generic;
using System.Text;

namespace QIGA.DataStructures
{
    class Qubit
    {
        public double[] Amplitudes {get; set;}

        Qubit(double alfa, double beta) => this.Amplitudes = new double[2] {alfa, beta};

        Qubit(double alfa) => new Qubit(alfa, 1 - alfa);

        Qubit() => new Qubit(0.5, 0.5);
        
    }
}
