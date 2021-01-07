using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSBibMatStudent.Complex;

namespace MN1_chyba
{
    class Parameters
    {
        public double E1 { get; set; }
        public double E2 { get; set; }
        public double f { get; set; }

        public Complex[] Z10 { get; set; } = new Complex[3];
        public Complex[] Z20 { get; set; } = new Complex[3];
        public Complex[] Z30 { get; set; } = new Complex[3];

        public Complex Z1 { get; set; }
        public Complex Z2 { get; set; }
        public Complex Z3 { get; set; }

        public Complex[,] A { get; set; } = new Complex[3 + 1, 3 + 1];
        public Complex[] B { get; set; } = new Complex[3 + 1];

        public Complex[] I { get; set; } = new Complex[3 + 1];
        


        public void CreateMatrix()
        {
            A[1, 1] = Z1;   A[1, 2] = Z2;   A[1, 3] = 0;
            A[2, 1] = 0;    A[2, 2] = -Z2;  A[2, 3] = Z3;
            A[3, 1] = -1;   A[3, 2] = 1;    A[3, 3] = 1;

            B[1] = E1;      B[2] = E2;      B[3] = 0;
        }
    }
}
