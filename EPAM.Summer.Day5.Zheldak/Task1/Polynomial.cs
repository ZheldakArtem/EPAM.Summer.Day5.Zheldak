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
        /// <summary>
        /// 
        /// </summary>
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

       

        static public Polynomial operator+(Polynomial first, Polynomial second)
        {
            var listCoef=new List<int>();
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
                        listCoef.Add(second._coefficients[i]- first._coefficients[i]);
                    else
                        listCoef.Add(second._coefficients[i]);
                }
            }

            return new Polynomial(listCoef);
        }
        
        static public Polynomial operator *(Polynomial first, Polynomial second)
        {
            var listCoef = new List<int>(first.CountCoef*second.CountCoef);
            for (int i = 0; i < first.CountCoef; i++)
            {
                for (int j = 0; j < second.CountCoef; j++)
                {
                   listCoef[i+j]=first._coefficients[i]*second._coefficients[j];
                }
            }
            return new Polynomial(listCoef);
        }

      // public double GetValueOfPolynomial(double x)
    }
}
