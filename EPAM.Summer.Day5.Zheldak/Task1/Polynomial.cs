using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Polynomial : IEquatable<Polynomial>, ICloneable, IComparable<Polynomial>
    {
        public int Exponent { get; private set; }
        private readonly double[] _coefficient;
        public double this[int i] => _coefficient[i];

        public Polynomial(params double[] coefficientArray)
        {
            if (coefficientArray == null)
                throw new ArgumentNullException();
            this._coefficient = new double[coefficientArray.Length];
            Exponent = coefficientArray.Length - 1;
        }

        public Polynomial(Polynomial polynomial)
        {
            if (polynomial == null)
                throw new ArgumentNullException();

            _coefficient = new double[polynomial._coefficient.Length];
            Exponent = polynomial.Exponent;
            Array.Copy(polynomial._coefficient, _coefficient, polynomial._coefficient.Length);
        }

        public static Polynomial operator +(Polynomial lsh, Polynomial rsh)
        {
            if (lsh == null)
                throw new ArgumentNullException("Empty argument");
            if (rsh == null)
                throw new ArgumentNullException("Empty argument");
            int max = 0;
            int min = 0;
            bool choice = true;
            if (lsh.Exponent >= rsh.Exponent)
            {
                max = lsh.Exponent;
                min = rsh.Exponent;
            }
            else
            {
                max = rsh.Exponent;
                min = lsh.Exponent;
                choice = false;
            }
            double[] coeficientArray = new double[max];
            if (choice)
                lsh._coefficient.CopyTo(coeficientArray, 0);
            else
                rsh._coefficient.CopyTo(coeficientArray, 0);
            for (int i = 0; i < min; i++)
            {
                if (choice)
                    coeficientArray[i] = coeficientArray[i] + rsh._coefficient[i];
                else
                    coeficientArray[i] = coeficientArray[i] + lsh._coefficient[i];
            }
            return new Polynomial(coeficientArray);
        }

        public static Polynomial Addition(Polynomial lsh, Polynomial rsh)
        {
            return lsh + rsh;
        }

        public static Polynomial operator -(Polynomial lsh, Polynomial rsh)
        {
            if (lsh == null)
                throw new ArgumentNullException("Empty argument");
            if (rsh == null)
                throw new ArgumentNullException("Empty argument");
            if (lsh.Exponent > rsh.Exponent)
            {
                double[] coeficientArray = new double[lsh._coefficient.Length];
                lsh._coefficient.CopyTo(coeficientArray, 0);
                for (int i = 0; i < rsh.Exponent; i++)
                {
                    coeficientArray[i] = coeficientArray[i] + rsh._coefficient[i];
                }
                return new Polynomial(coeficientArray);
            }
            else
            {
                double[] coeficientArray = new double[rsh._coefficient.Length];
                for (int i = 0; i < coeficientArray.Length; i++)
                {
                    coeficientArray[i] = rsh._coefficient[i] * (-1);
                }
                for (int i = 0; i < lsh.Exponent; i++)
                {
                    coeficientArray[i] = coeficientArray[i] - lsh._coefficient[i];
                }
                return new Polynomial(coeficientArray);
            }
        }

        public static Polynomial Subtraction(Polynomial lsh, Polynomial rsh)
        {
            return lsh - rsh;
        }

        public static Polynomial operator *(Polynomial lsh, Polynomial rsh)
        {
            if (lsh == null)
                throw new ArgumentNullException("Empty argument");
            if (rsh == null)
                throw new ArgumentNullException("Empty argument");
            double[] coeficientArray = new double[lsh.Exponent + rsh.Exponent];
            for (int i = 0; i < lsh.Exponent; i++)
            {
                for (int j = 0; j < rsh.Exponent; j++)
                {
                    coeficientArray[i + j] = coeficientArray[i + j] + lsh._coefficient[i] * rsh._coefficient[j];
                }
            }
            return new Polynomial(coeficientArray);
        }

        public static Polynomial Multiplication(Polynomial lsh, Polynomial rsh)
        {
            return lsh * rsh;
        }

        public static bool operator !=(Polynomial lsh, Polynomial rsh)
        {
            if (ReferenceEquals(lsh, rsh))
                return false;
            return !(lsh.Equals(rsh));
        }

        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (!ReferenceEquals(lhs, null))
                return lhs.Equals(rhs);
            return ReferenceEquals(rhs, null);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Polynomial)obj);
        }

        public bool Equals(Polynomial otherPolynomial)
        {
            if (ReferenceEquals(this, otherPolynomial))
                return true;
            if (ReferenceEquals(otherPolynomial, null))
                return false;

            if (otherPolynomial.Exponent != Exponent)
                return false;

            return CompareTo(otherPolynomial) == 0;
        }

        public override int GetHashCode() => (int)_coefficient.Max();

        public object Clone() => new Polynomial(this);

        public int CompareTo(Polynomial other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (ReferenceEquals(other, null))
                throw new NullReferenceException();
            if (Exponent > other.Exponent)
                return 1;
            if (Exponent < other.Exponent)
                return -1;

            for (int i = 0; i <= Exponent; i++)
            {
                if (other[i] < this[i])
                    return 1;
                if (other[i] > this[i])
                    return -1;
            }

            return 0;
        }
    }
}
