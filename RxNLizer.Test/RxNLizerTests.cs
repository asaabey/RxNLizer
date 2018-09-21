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
    public class RxNLizerTests
    {
        [TestMethod()]
        public void GetPreferredRxcuiFromTextTestEmpty()
        {
            string input = string.Empty;

            RxNLizer rxNLizer = new RxNLizer();

            string output = rxNLizer.GetPreferredRxcuiFromText(input);

            string expected = string.Empty;

            Assert.AreEqual(expected,output);
        }

        [TestMethod()]
        public void GetPreferredRxcuiFromTextTestNuisance()
        {
            string input = "Alpha";

            RxNLizer rxNLizer = new RxNLizer();

            string output = rxNLizer.GetPreferredRxcuiFromText(input);

            string expected = string.Empty;

            Assert.AreEqual(expected, output);
        }

        [TestMethod()]
        public void GetPreferredRxcuiFromTextTestUnknown()
        {
            string input = "6967969697";

            RxNLizer rxNLizer = new RxNLizer();

            string output = rxNLizer.GetPreferredRxcuiFromText(input);

            string expected = string.Empty;

            Assert.AreEqual(expected, output);
        }
    }
}