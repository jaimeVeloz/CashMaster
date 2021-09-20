using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Core;
using Practica.Models;
using Practica.Service;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_CurrencyUS()
        {
            Currency.CurrencyCode = "US";
            Currency.CurrencyDenomination = new List<double> { 0.01, 0.05, 0.10, 0.25, 0.50, 1.00, 2.00, 5.00, 10.00, 20.00, 50.00, 100.00 };
            CalculateService instance = new CalculateService();
            CalculateChange model = new CalculateChange()
            {
                itemPrice = 37.5,
                payment = 100
            };
            var result = instance.calculateChange(model);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_CurrencyMX()
        {
            Currency.CurrencyCode = "MX";
            Currency.CurrencyDenomination = new List<double> { 0.05, 0.10, 0.20, 0.50, 1.00, 2.00, 5.00, 10.00, 20.00, 50.00, 100.00, 200.00, 500.00 };
            CalculateService instance = new CalculateService();
            CalculateChange model = new CalculateChange()
            {
                itemPrice = 37.5,
                payment = 500
            };
            var result = instance.calculateChange(model);

            Assert.IsNotNull( result);
        }


        [TestMethod]
        public void Test_Exception()
        {
            try
            {
                Currency.CurrencyCode = "MX";
                Currency.CurrencyDenomination = new List<double> { 0.05, 0.10, 0.20, 0.50, 1.00, 2.00, 5.00, 10.00, 20.00, 50.00, 100.00, 200.00, 500.00 };
                CalculateService instance = new CalculateService();
                CalculateChange model = new CalculateChange()
                {
                    itemPrice = 37.5,
                    payment = 5
                };
                instance.calculateChange(model);

                Assert.Fail();
            }
            catch (Exception ex)
            {
                // Catches the assertion exception, and the test passes
                Assert.IsTrue(ex is TException);
            }
        }
    }
}
