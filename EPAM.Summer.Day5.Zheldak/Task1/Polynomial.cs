using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Polynomial
    {

        private readonly IList<int> _coefficients;

        public int ExpPolinom
        {
            get
            {
                return CountCoef + 1;
            }
        }

        public int CountCoef
        {
            get { return _coefficients.Count; }
        }

        public Polynomial(IList<int> coefficients)
        {
            if (coefficients == null)
                throw new ArgumentException();


            _coefficients = coefficients;
        }

        static public Polynomial operator +(Polynomial first, Polynomial second)
        {
            var listCoef = new List<int>();
            if (first.ExpPolinom > second.ExpPolinom)
            {
                for (int i = 0; i < first.ExpPolinom + 1; i++)
                {
                    if (i <= second.ExpPolinom + 1)
                        listCoef.Add(first._coefficients[i] + second._coefficients[i]);
                    else
                        listCoef.Add(first._coefficients[i]);
                }
            }
            else
            {
                for (int i = 0; i < second.ExpPolinom + 1; i++)
                {
                    if (i <= first.ExpPolinom + 1)
                        listCoef.Add(first._coefficients[i] + second._coefficients[i]);
                    else
                        listCoef.Add(second._coefficients[i]);
                }
            }

            return new Polynomial(listCoef);
        }

        static public Polynomial operator -(Polynomial first, Polynomial second)
        {
            var listCoef = new List<int>();
            if (first.ExpPolinom > second.ExpPolinom)
            {
                for (int i = 0; i < first.ExpPolinom + 1; i++)
                {
                    if (i <= second.ExpPolinom + 1)
                        listCoef.Add(first._coefficients[i] - second._coefficients[i]);
                    else
                        listCoef.Add(first._coefficients[i]);
                }
            }
            else
            {
                for (int i = 0; i < second.ExpPolinom + 1; i++)
                {
                    if (i <= first.ExpPolinom + 1)
                        listCoef.Add(second._coefficients[i] - first._coefficients[i]);
                    else
                        listCoef.Add(second._coefficients[i]);
                }
            }

            return new Polynomial(listCoef);
        }

        static public Polynomial operator *(Polynomial first, Polynomial second)
        {
            var listCoef = new List<int>(first.CountCoef * second.CountCoef);
            for (int i = 0; i < first.CountCoef; i++)
            {
                for (int j = 0; j < second.CountCoef; j++)
                {
                    listCoef[i + j] = first._coefficients[i] * second._coefficients[j];
                }
            }
            return new Polynomial(listCoef);
        }

        static public bool operator ==(Polynomial first, Polynomial second)
        {
            if (first == null || second == null)
                throw new NullReferenceException();

            if (first.Equals(second))
                return true;
            return false;
        }

        static public bool operator !=(Polynomial first, Polynomial second)
        {
            if (first == null || second == null)
                throw new NullReferenceException();
            if (!first.Equals(second))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            double sum = 0;
            for (int i = 0; i < ExpPolinom; i++)
            {
                sum += _coefficients[i];
            }
            return sum.GetHashCode() * 137;
        }

        public bool Equals(Polynomial polinom)
        {
            if (polinom == null)
                return false;
            if (polinom.CountCoef != CountCoef)
                return false;
            for (int i = 0; i < polinom.CountCoef; i++)
            {
                if (_coefficients[i] != polinom._coefficients[i])
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Polynomial)obj);
        }

        public override string ToString()
        {
            var strBuild = new StringBuilder();
            for (int i = CountCoef-1; i > 0; i--)
            {
                strBuild.AppendFormat("{0}x^{1}", _coefficients[i], i);
            }
            strBuild.Remove(strBuild.Length, 1);
            return strBuild.ToString();
        }

        public double GetValueOfPolynomial(double x)
        {
            double result = 0;
            for (int i = CountCoef-1; i >0 ; i++)
            {
                result += _coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }
    }
}
