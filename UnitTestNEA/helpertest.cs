//using NUnit.Framework;
using NEAproject;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestNEA
{
    [TestClass]
    public class helpertest
    {
        
        
         //[Test]
         [TestMethod]
        
        public void lognfunctiontest()
        {
            int output = Helper.lognfunction(1);
            int expectedoutcome = 0;
            Assert.Equals(expectedoutcome, output);
           
        }
    }
}