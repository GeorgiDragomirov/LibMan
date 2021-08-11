using System;
using System.Threading.Tasks;
using LibraryManagment.Data;
using LibraryManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace AspNetCoreTodo.UnitTests
{
    [TestClass]
    public class AmountOwedShould
    {
        [Fact]
        [TestMethod]
        public void CalculatingAmountOwed()
        {
            var testCalculation = LibraryManagment.Models.Lending.AmountOwed;

        }
    }
}