using System;
using Xunit;
using NEAproject;
using System.Collections.Generic;
using NEAproject.Models;
//using NUnit.Framework;

namespace NEAUnitTests
{
    
    public class helpertest
    {
        //IDbContextGenerator generator;

        [Fact]
        public void logfunctionfact()
        {
            int output = Helper.lognfunction(1);
            int expectedoutcome = 0;
            Assert.Equal(expectedoutcome, output);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(4, 2)]
        [InlineData(8, 3)]
        [InlineData(16, 4)]
        [InlineData(32, 5)]
        public void lognfunctiontesttheory(int input, int expectedoutcome)
        {
            int output = Helper.lognfunction(input);

            Assert.Equal(expectedoutcome, output);
        }
        [Fact]
        public void getlineardatapointtest()
        {
            List<datapoint> output = Helper.getLineardatapoints(3);
            int i = 1;
            foreach (datapoint a in output)
            {
                datapoint temp = new datapoint(i.ToString(), i);
                Assert.Equal(temp.label, a.label);
                Assert.Equal(temp.y, a.y);
                i++;
            }
        }
        [Fact]
        public void twotothentest()
        {
            List<datapoint> output = Helper.get2totheNpoints(3);
            int i = 1;
            foreach (datapoint a in output)
            {
                var complexity = Math.Pow(2, i);
                datapoint temp = new datapoint(complexity.ToString(), complexity);
                Assert.Equal(temp.label, a.label);
                Assert.Equal(temp.y, a.y);
                i++;
            }
        }
        [Fact]
        public void ntothetwotest()
        {
            List<datapoint> output = Helper.getNtothe2points(3);
            int i = 1;
            foreach (datapoint a in output)
            {
                var complexity = Math.Pow(i, 2);
                datapoint temp = new datapoint(complexity.ToString(), complexity);
                Assert.Equal(temp.label, a.label);
                Assert.Equal(temp.y, a.y);
                i++;
            }
        }
        [Fact]
        public void Emailsenttest()
        {
            bool output = EmailSender.sendtestemail("arabellahurrell@gmail.com", "hi", "hi");
            bool expectedoutcome = false;
            Assert.Equal(expectedoutcome, output);
        }

        

        //[Theory]
        //[InlineData(3, new List<datapoint> {new datapoint("2",2),new datapoint("4", 4), new datapoint("8", 8) })]

        //public void twotothenthoery(int input, List<datapoint> expectedoutcome)
        //{
        //    List<datapoint> output = Helper.get2totheNpoints(input);
        //    int i = 1;
        //    foreach (datapoint a in output)
        //    {

        //        datapoint temp = expectedoutcome[i];
        //        Assert.Equal(temp.label, a.label);
        //        Assert.Equal(temp.y, a.y);
        //        i++;
        //    }

        //}
    }
}
