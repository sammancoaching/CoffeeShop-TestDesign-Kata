using Xunit;

using SammanCoaching.ParameterizedTests;

namespace SammanCoaching.ParameterizedTests.Tests.Concrete
{
	public class OrderStateTest
	{
		[Fact]
		public void OrderCanBeUpdatedAndCanceledIfNotPaidYet()
		{
			var order = ExistingOrderWithStatus(Status.PaymentExpected);
			Assert.True(order.IsUpdateAllowed());
			Assert.True(order.IsCancelAllowed());
		}

		[Fact]
		public void OrderCannotBeUpdatedOrCanceledIfPaid()
		{
			var order = ExistingOrderWithStatus(Status.Paid);
			Assert.False(order.IsUpdateAllowed());
			Assert.False(order.IsCancelAllowed());
		}

		[Fact]
		public void OrderCannotBeUpdatedOrCanceledIfPreparing()
		{
			var order = ExistingOrderWithStatus(Status.Preparing);
			Assert.False(order.IsUpdateAllowed());
			Assert.False(order.IsCancelAllowed());
		}

		[Fact]
		public void OrderCannotBeUpdatedOrCanceledIfReady()
		{
			var order = ExistingOrderWithStatus(Status.Ready);
			Assert.False(order.IsUpdateAllowed());
			Assert.False(order.IsCancelAllowed());
		}

		[Fact]
		public void OrderCannotBeUpdatedOrCanceledIfTaken()
		{
			var order = ExistingOrderWithStatus(Status.Taken);
			Assert.False(order.IsUpdateAllowed());
			Assert.False(order.IsCancelAllowed());
		}

		private static Order ExistingOrderWithStatus(Status status)
		{
			var order = new Order(1L);
			order.Status = status;
			return order;
		}
	}
}