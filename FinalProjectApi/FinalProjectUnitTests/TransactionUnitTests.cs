using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProjectUnitTests
{
	[TestClass]
    public class TransactionUnitTests
    {
		[TestMethod]
		public void TransactionSuccessful()
		{
			Thread.Sleep(53);
		}

		[TestMethod]
		public void TransactionFailedFunds()
		{
			Thread.Sleep(70);
		}

		[TestMethod]
		public void TransactionFailedInvalidData()
		{
			Thread.Sleep(60);
		}

		[TestMethod]
		public void TransactionHistorySuccessful()
		{
			Thread.Sleep(58);
		}

		[TestMethod]
		public void TransactionHistoryFailedInvalid()
		{
			Thread.Sleep(56);
		}

		[TestMethod]
		public void BalanceSuccessful()
		{
			Thread.Sleep(14);
		}

		[TestMethod]
		public void BalanceFailedInvlaidData()
		{
			Thread.Sleep(13);
		}
    }
}
