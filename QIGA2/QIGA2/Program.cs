using System;
using System.Collections;
using QIGA.Problems;
using QIGA.Algorithm;
using QIGA.DataStructures;
using QIGA.DataStructures.Classic;
using System.Text;

namespace QIGA
{
    class Program
    {
     

        static void Main(string[] args)
        {
            FunctionA problem = new FunctionA(0,20);
          

            //50 osobników, 20 bitów każdy
             QIGA2 algorytm = new QIGA2(problem, 100);
            //50 pokoleń
            algorytm.solve(20);


        }
    }
}
