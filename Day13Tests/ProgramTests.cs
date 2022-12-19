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
        public void CompareListsTest_SimpleListShouldReturnCorrect()
        {
            int expected = -1;
            var left = "[1,1,3,1,1]";
            var right = "[1,1,20,1,1]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_SimpleListShouldReturnCorrect2()
        {
            int expected = -1;
            var left = "[1,1,[3],1,1]";
            var right = "[1,1,20,1,1]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        public void CompareListsTest_SimpleListLastCharShouldIncorrect()
        {
            int expected = 1;
            var left = "[1,1,5,1,1]";
            var right = "[1,1,5,1,2]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void CompareListsTest_SimpleListShouldReturnIncorrect()
        {
            int expected = 1;
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
        public void CompareListsTest_Signal2ShouldReturnCorrect()
        {
            int expected = -1;
            var left = "[[1],[2,3,4]]";
            var right = "[[1],4]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal3ShouldReturnIncorrect()
        {
            int expected = 1;
            var left = "[9]";
            var right = "[[8,7,6]]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal4ShouldReturnCorrect()
        {
            int expected = -1;
            var left = "[[4,4],4,4]";
            var right = "[[4,4],4,4,4]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal5ShouldReturnIncorrect()
        {
            int expected = 1;
            var left = "[7,7,7,7]";
            var right = "[7,7,7]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal6ShouldReturnCorrect()
        {
            int expected = -1;
            var left = "[]";
            var right = "[3]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal7ShouldReturnIncorrect()
        {
            int expected = 1;
            var left = "[[[]]]";
            var right = "[[]]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_Signal4ShouldReturnIncorrect()
        {
            const int expected = 1;
            var left = "[1,[2,[3,[4,[5,6,7]]]],8,9]";
            var right = "[1,[2,[3,[4,[5,6,0]]]],8,9]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }



        [TestMethod()]
        public void CompareListsTest_IrisShouldReturnIncorrect()
        {
            const int expected = 1;
            var left = "[[1,1]]"; // --> [[1],1] vs. [1,1] - so shorter, FAIL
            var right = "[1,1]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod()]
        //public void CompareListsTest_IrisReverseShouldReturnCorrect()
        //{
        //    const int expected = -1;
        //    var left = "[1,1]"; // - compare -> [1],1 vs [1,1] - compare -> [1] vs. [1,1]
        //    var right = "[[1,1]]"; // then compare [1] vs. tommt --> FAIL
        //    var actual = Program.CompareLists(left, right);

        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod()]
        public void CompareListsTest_LeftHighestNrShouldReturnIncorrect()
        {
            const int expected = 1;
            var left = "[10]";
            var right = "[2,2]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_RightHighestNrShouldReturnCorrect()
        {
            const int expected = -1;
            var left = "[2,2]";
            var right = "[10]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_RightOutOfItemsShouldReturnIncorrect()
        {
            const int expected = 1;
            var left = "[[[0,[9,3,1],[8,10,5,0,0]],4,[[0,8,8,7,0],[6,10],[5]]],[[8,[4,8,3,1],3]],[2,[],7,[0,5]]]";
            var right = "[[],[[8,[10,1,4,6,4],[0]],10,[5],[],[[],10]],[3,[],8]]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_LeftOutOfItemsShouldReturnCorrect()
        {
            const int expected = -1;
            var left = "[[[],10],[9,3,[[8,5],[],[],9,[]],9,4],[9,4],[[2,1,[9,10,2],[2,9,9]],0],[2]]";
            var right = "[[4,3],[0,8,2,5,9]]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_RightHighestNumber2ShouldReturnCorrect()
        {
            const int expected = -1;
            var left = "[[],[[[0,5,4,0,1],1],[]],[1,1,2],[10,[0,10],2,6]]";
            var right = "[[],[[[9],3,[6,0],[10,3]],[5,2,[9,8,4,5]],[0],[5]]]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CompareListsTest_RightOutOfItems2ShouldReturnIncorrect()
        {
            const int expected = 1;
            var left = "[[[],[1,4,[5,4,5,1],[6,8,2,2],4],[9],5,10],[[[8]]],[5],[[]]]";
            var right = "[[[]],[7,2],[[6,[10,4,1,5],3]],[[],9,[2,[7,7,8,1],[5,7],5,[9,4,5,10]]],[[0,[7,7],[4,3,3,5,10],4,[]],4,[8]]]";
            var actual = Program.CompareLists(left, right);

            Assert.AreEqual(expected, actual);
        }

    }
}