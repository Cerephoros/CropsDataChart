using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProject;

namespace DatasetUnitTest
{
    /**
     * Run test cases to ensure that each functions are working properly.
     * Author: Erik Njolstad
     * Date Modified: 11/10/2017
     */
    [TestClass]
    public class UnitTest1
    {

        //private Program prog = new Program();
        private DataInfo info = new DataInfo();
        private DatasetLoader data = new DatasetLoader();

        /**
         * Have both the actual and expected results match by calling the GetRefDate function.
         */
        [TestMethod]
        public void TestMethodGetSetRefDate()
        {
            info.SetRefDate(1992);
            int actual = info.GetRefDate();
            int expected = 1992;
            Assert.AreEqual(expected, actual, actual == expected ? "The expected and actual results match" : "The expected and actual results do not match");
        }

        /**
         * Have both the actual and expected results match by calling the GetOrigin function.
         */
        [TestMethod]
        public void TestMethodGetSetOrigin()
        {
            info.SetOrigin("Harvested area, potatoes (acres)");
            string actual = info.GetOrigin();
            string expected = "Harvested area, potatoes (acres)";
            Assert.AreEqual(expected, actual, actual == expected ? "The expected and actual results match" : "The expected and actual results do not match");
        }

        /**
         * Have both the actual and unexpected results not match by calling the GetValue function.
         */
        [TestMethod]
        public void TestMethodGetSetValue()
        {
            info.SetValue("..");
            string actual = info.GetValue();
            string unexpected = "902162.0";
            Assert.AreNotSame(unexpected, actual, actual == unexpected ? "The expected and actual should not match" : "There is an error with a function since this test should pass.");
        }

        /**
         * Takes two lists and compares them if they have the same string value.
         */
        [TestMethod]
        public void TestMethodGetList()
        {
            List<string> actual = new List<string>();
            actual = data.GetList();
            string[] array = { "Ref_Date", "GEO", "EST", "Vector", "Coordinate", "Value" };
            List<string> expected = new List<string>();
            expected.AddRange(array);
            Assert.IsTrue(expected.Contains(actual[1]));
        }

        /**
         * Takes the entire string from the GetLines() function and compares whether it is the
         * same as the expected value.
         */
        [TestMethod]
        public void TestMethodGetLines()
        {
            String[] actual = data.GetLines();
            string expected = "2003,Canada,\"Harvested area, potatoes (acres)\",v47195,1.6,447000.0";
            Assert.IsTrue(expected.Contains(actual[128]));
        }
    }
}
