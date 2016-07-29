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

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        /// <param name="number">The zero-based index of the element to get or set.</param>
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

        /// <summary>
        /// Addition of two polynomials.
        /// </summary>
        /// <param name="lhs">The left polinomials.</param>
        /// <param name="rhs">The right polinomials.</param>
        /// <returns>Summ of polinomials</returns>
        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null)
                throw new ArgumentNullException();
            if (rhs == null)
                throw new ArgumentNullException();
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

        /// <summary>
        /// Overloaded addition operator of two polynomials.
        /// </summary>
        /// <param name="lhs">The left polinomials.</param>
        /// <param name="rhs">The right polinomials.</param>
        /// <returns></returns>
        public static Polynomial Addition(Polynomial lhs, Polynomial rhs)
        {
            return lhs + rhs;
        }

        /// <summary>
        /// Overloaded multiplication operator.
        /// </summary>
        /// <param name="lhs">The first polynomial.</param>
        /// <param name="rhs">The second polynomial.</param>
        /// <returns>The substraction of two polynomials.</returns>
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

        /// <summary>
        /// The substraction of two polynomials.
        /// </summary>
        /// <param name="lsh">The first polynomial.</param>
        /// <param name="rsh">The second polynomial.</param>
        /// <returns>The substraction of two polynomials.</returns>
        public static Polynomial Subtraction(Polynomial lsh, Polynomial rsh)
        {
            return lsh - rsh;
        }

        /// <summary>
        /// Overloaded multiplication operator.
        /// </summary>
        /// <param name="lhs">The first polynomial.</param>
        /// <param name="rhs">The second polynomial.</param>
        /// <returns>Multiplication of two polynomials.</returns>
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

        /// <summary>
        /// Multiplication of two polynomials.
        /// </summary>
        /// <param name="lhs">The first polynomial.</param>
        /// <param name="rhs">The second polynomial.</param>
        /// <returns>Multiplication of two polynomials.</returns>
        public static Polynomial Multiplication(Polynomial lhs, Polynomial rhs)
        {
            return lhs * rhs;
        }

        /// <summary>
        /// Determines whether two specified polynomials have different values.
        /// </summary>
        /// <returns>
        /// True if the value of <paramref name="lhs"/> is different from the value of <paramref name="rhs"/>; otherwise, false.
        /// </returns>
        /// <param name="lhs">The first polynomial to compare, or null. </param><param name="rhs">The second polynomial to compare, or null. </param>
        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Determines whether two specified polynomials have the same value.
        /// </summary>
        /// <returns>
        /// True if the value of <paramref name="lhs"/> is the same as the value of <paramref name="rhs"/>; otherwise, false.
        /// </returns>
        /// <param name="lhs">The first polynomial to compare, or null. </param><param name="rhs">The second polynomial to compare, or null. </param>
        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;

            if (ReferenceEquals(lhs, null)) return false;

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// True if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;

            return Equals((Polynomial)obj);
        }

        /// <summary>
        /// Determines whether the polynomial is equal to the current polynomial.
        /// </summary>
        /// <returns>
        /// True if the specified polynomial  is equal to the current polynomial; otherwise, false.
        /// </returns>
        /// <param name="otherPolynomial">The polynomial to compare with the current polynomial. </param>
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

        /// <summary>
        /// Serves as the hash function.
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode() => (int)_coefficient.Max().GetHashCode();

        /// <summary>
        /// Creates a shallow copy of the polynomial.
        /// </summary>
        /// <returns>
        /// A shallow copy of the current polinomial.
        /// </returns>
        public object Clone() => new Polynomial(this);

        /// <summary>
        /// Determines whether the current  polinomial precedes, occurs in the same position as, or follows another polinomial in the sort order.
        /// </summary>
        /// <returns>
        /// An integer that indicates the relationship of the current polinomial to other, as shown in the following table.Return valueDescription-1 The current instance precedes <paramref name="other"/>.0 The current instance and <paramref name="other"/> are equal.1 The current instance follows <paramref name="other"/>.
        /// </returns>
        /// <param name="other">The object to compare with the current instance.</param>
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

        /// <summary>
        /// The method calculates the value of a polynomial
        /// </summary>
        /// <param name="argument">Argument of polinomial</param>
        /// <returns>The polynomial value</returns>
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
