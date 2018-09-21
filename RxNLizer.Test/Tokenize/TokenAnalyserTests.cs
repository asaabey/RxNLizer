using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxNLizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxNLizer.Tests
{
    [TestClass()]
    public class TokenAnalyserTests
    {


        [TestMethod()]
        public void SelectCandidateTokenTestWithNuisance()
        {
            string[] input = new string[] { "Alpha", "Amoxicillin", "500", "mg", "Clavulanic", "acid", "125", "mg", "coated", "tablet;", "500", "mg,", "125", "mg", "500", "mg/125", "mg" };

            string expected = "Amoxicillin";

            string output = TokenAnalyser.SelectCandidateToken(input);


            Assert.AreEqual(expected, output, true);
        }

        [TestMethod()]
        public void SelectCandidateTokenTestWithoutNuisance()
        {
            string[] input = new string[] { "Amoxicillin", "500", "mg", "Clavulanic", "acid", "125", "mg", "coated", "tablet;", "500", "mg,", "125", "mg", "500", "mg/125", "mg" };

            string expected = "Amoxicillin";

            string output = TokenAnalyser.SelectCandidateToken(input);

            Assert.AreEqual(expected, output, true);
        }

        [TestMethod()]
        public void IsNotInBlackListTestAlpha()
        {
            string input = "Alpha";

            bool output=TokenAnalyser.IsNotInBlackList(input);

            bool expected = false;

            Assert.AreEqual(expected,output);
        }

        [TestMethod()]
        public void IsNotInBlackListTestOmega()
        {
            string input = "Omega";

            bool output = TokenAnalyser.IsNotInBlackList(input);

            bool expected = true;

            Assert.AreEqual(expected, output);
        }
    }
}