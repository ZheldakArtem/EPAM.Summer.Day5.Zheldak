using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Polynomial : IEquatable<Polynomial>, ICloneable, IComparable<Polynomial>
    {

        private readonly double[] _coefficient;
        public int Exponent { get; }

        public double this[int number]
        {
            get
            {
                if (number > _coefficient.Length)
                    throw new ArgumentOutOfRangeException();
                return _coefficient[number];
            }
            private set
            {
                if (number > 0 || number < _coefficient.Length)
                {
                    this._coefficient[number] = value;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public Polynomial(params double[] coefficientArray)
        {
            if (coefficientArray == null)
                throw new ArgumentNullException();
            this._coefficient = new double[coefficientArray.Length];
            Array.Copy(coefficientArray, _coefficient, coefficientArray.Length);
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

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException("Argumetn is null");
            if (rhs == null)
                throw new ArgumentNullException("Argumetn is null");
            int max = 0;
            int min = 0;
            bool choice = true;
            if (lhs.Exponent >= rhs.Exponent)
            {
                max = lhs.Exponent;
                min = rhs.Exponent;
            }
            else
            {
                max = rhs.Exponent;
                min = lhs.Exponent;
                choice = false;
            }
            double[] coeficientArray = new double[max + 1];
            if (choice)
                lhs._coefficient.CopyTo(coeficientArray, 0);
            else
                rhs._coefficient.CopyTo(coeficientArray, 0);
            for (int i = 0; i < min + 1; i++)
            {
                if (choice)
                    coeficientArray[i] = coeficientArray[i] + rhs._coefficient[i];
                else
                    coeficientArray[i] = coeficientArray[i] + lhs._coefficient[i];
            }
            return new Polynomial(coeficientArray);
        }

        public static Polynomial Addition(Polynomial lhs, Polynomial rhs)
        {
            return lhs + rhs;
        }

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException("Empty argument");
            if (rhs == null)
                throw new ArgumentNullException("Empty argument");
            if (lhs.Exponent > rhs.Exponent)
            {
                double[] coeficientArray = new double[lhs._coefficient.Length];
                lhs._coefficient.CopyTo(coeficientArray, 0);
                for (int i = 0; i < rhs.Exponent + 1; i++)
                {
                    coeficientArray[i] = coeficientArray[i] - rhs._coefficient[i];
                }
                return new Polynomial(coeficientArray);
            }
            else
            {
                double[] coeficientArray = new double[rhs._coefficient.Length];
                for (int i = 0; i < coeficientArray.Length; i++)
                {
                    coeficientArray[i] = rhs._coefficient[i] * (-1);
                }
                for (int i = 0; i < lhs.Exponent + 1; i++)
                {
                    coeficientArray[i] = coeficientArray[i] + lhs._coefficient[i];
                }
                return new Polynomial(coeficientArray);
            }
        }

        public static Polynomial Subtraction(Polynomial lsh, Polynomial rsh)
        {
            return lsh - rsh;
        }

        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException("Argumetn is null");
            if (rhs == null)
                throw new ArgumentNullException("Argumetn is null");
            double[] coeficientArray = new double[lhs.Exponent + rhs.Exponent + 1];
            for (int i = 0; i < lhs.Exponent + 1; i++)
            {
                for (int j = 0; j < rhs.Exponent + 1; j++)
                {
                    coeficientArray[i + j] = coeficientArray[i + j] + lhs._coefficient[i] * rhs._coefficient[j];
                }
            }
            return new Polynomial(coeficientArray);
        }

        public static Polynomial Multiplication(Polynomial lhs, Polynomial rhs)
        {
            return lhs * rhs;
        }

        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;

            if (ReferenceEquals(lhs, null)) return false;

            return lhs.Equals(rhs);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;

            return Equals((Polynomial)obj);
        }

        public bool Equals(Polynomial otherPolynomial)
        {
            if (ReferenceEquals(null, otherPolynomial)) return false;

            if (ReferenceEquals(this, otherPolynomial)) return true;

            if (this._coefficient.Length != otherPolynomial._coefficient.Length)

                return false;

            for (var i = 0; i < this._coefficient.Length; i++)
            {

                if (!this._coefficient[i].Equals(otherPolynomial._coefficient[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode() => (int)_coefficient.Max();

        public object Clone() => new Polynomial(this);

        public int CompareTo(Polynomial other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException("Argumetn is null");
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

        public double GetValue(double argument)
        {
            if (double.IsNaN(argument) || double.IsNegativeInfinity(argument) || double.IsPositiveInfinity(argument))
                throw new ArgumentOutOfRangeException();

            double result = 0;
            for (int i = Exponent; i >= 0; i--)
            {
                result = result + _coefficient[i] * Math.Pow(argument, i);
            }
            return result;
        }
    }
}
