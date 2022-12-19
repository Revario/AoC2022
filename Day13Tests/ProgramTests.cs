using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void CompareListsTest_SimpleListShouldReturn1()
        {
            int expected = 1;
            var left = "[1,1,3,1,1]";
            var right = "[1,1,5,1,1]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        public void CompareListsTest_SimpleListLastCharShouldReturn1()
        {
            int expected = 1;
            var left = "[1,1,5,1,1]";
            var right = "[1,1,5,1,2]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void CompareListsTest_SimpleListShouldReturnNegative()
        {
            int expected = -1;
            var left = "[1,1,5,1,1]";
            var right = "[1,1,3,1,1]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_SimpleListShouldReturnNeutral()
        {
            int expected = 0;
            var left = "[1,1,5,1,1]";
            var right = "[1,1,5,1,1]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal2ShouldReturnPositive()
        {
            int expected = 1;
            var left = "[[1],[2,3,4]]";
            var right = "[[1],4]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal3ShouldReturnNegative()
        {
            int expected = -1;
            var left = "[9]";
            var right = "[[8,7,6]]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal4ShouldReturnPositive()
        {
            int expected = 1;
            var left = "[[4,4],4,4]";
            var right = "[[4,4],4,4,4]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal5ShouldReturnNegative()
        {
            int expected = -1;
            var left = "[7,7,7,7]";
            var right = "[7,7,7]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal6ShouldReturnPositive()
        {
            int expected = 1;
            var left = "[]";
            var right = "[3]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal7ShouldReturnNegative()
        {
            int expected = -1;
            var left = "[[[]]]";
            var right = "[[]]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal4ShouldReturnNegative()
        {
            const int expected = -1;
            var left = "[1,[2,[3,[4,[5,6,7]]]],8,9]";
            var right = "[1,[2,[3,[4,[5,6,0]]]],8,9]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

       
    }
}