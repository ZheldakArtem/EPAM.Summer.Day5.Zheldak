using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;
using NUnit.Framework;
namespace Task1.Tests
{

    public class PolynomialTest
    {
        [Test]
        public void PolynomialEqualsTest()
        {
            var lsh = new Polynomial(1, 2, 3, 4);
            var rsh = new Polynomial(1, 2, 3, 4);
            int a = 4;
            int b = -3;

            Polynomial c = rsh;
            Assert.AreEqual(lsh.Equals(rsh), true);
            Assert.AreEqual(lsh == rsh, true);
            Assert.AreEqual(lsh != rsh, false);
            Assert.AreEqual(c == rsh, true);
        }

        public IEnumerable<TestCaseData> TestAddData
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 4, 3, 8), new Polynomial(1, 2, 7)).Returns(new Polynomial(2, 6, 10, 8));
                yield return new TestCaseData(new Polynomial(1.5, -2, 3.3), new Polynomial(1.5, 2, 7, -8)).Returns(new Polynomial(new[] { 3, 0, 10.3, -8 }));
            }
        }

        [Test, TestCaseSource(nameof(TestAddData))]
        public Polynomial AddTest(Polynomial lsh, Polynomial rsh)
        {
            return lsh + rsh;
        }

        public IEnumerable<TestCaseData> TestSubData
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, -3.5, 0, 6), new Polynomial(-3, 0, 4, -8)).Returns(new Polynomial(4, 2, -7.5, 8, 6));
                yield return new TestCaseData(new Polynomial(5, 10, 5), new Polynomial(15, 5, 15, -8)).Returns(new Polynomial(-10, 5, -10, 8));
            }
        }

        [Test, TestCaseSource(nameof(TestSubData))]
        public Polynomial SubTest(Polynomial lsh, Polynomial rsh)
        {
            return lsh - rsh;
        }

        public IEnumerable<TestCaseData> TestMulData
        {
            get
            {
                yield return new TestCaseData(new Polynomial(3, 4, 1), new Polynomial(5, 2)).Returns(new Polynomial(15, 26, 13, 2));
            }
        }

        [Test, TestCaseSource(nameof(TestMulData))]
        public Polynomial MulTest(Polynomial a, Polynomial b)
        {
            return Polynomial.Multiplication(a, b);
        }

        [Test]
        public void GetValueTest()
        {
            double param = 4;
            var pol = new Polynomial(1, 2, -3);

            double result = pol.GetValue(param);

            Assert.AreEqual(result, -39);
        }

    }
}
