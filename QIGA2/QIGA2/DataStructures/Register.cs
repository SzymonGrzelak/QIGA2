using System;
using System.Collections.Generic;
using System.Text;

namespace QIGA.DataStructures
{
    class Register
    {
        public List<Qubit> Qubits{ get; set; }

        Register(params Qubit[] list) => this.Qubits = new List<Qubit>(list);
    }
}
