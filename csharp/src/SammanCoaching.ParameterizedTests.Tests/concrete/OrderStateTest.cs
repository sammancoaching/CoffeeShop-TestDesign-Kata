using Xunit;
using SammanCoaching.ParameterizedTests;

namespace SammanCoaching.ParameterizedTests.Tests.Concrete
{
    public class OrderStateTest
    {
        [Fact]
        public void NewOrder_ShouldBeOrdered()
        {
            var order = new Order();
            Assert.Equal(Status.Ordered, order.Status);
        }

        [Fact]
        public void Pay_ChangesStatusToPaid()
        {
            var order = new Order();
            order.Pay();
            Assert.Equal(Status.Paid, order.Status);
        }

        [Fact]
        public void Cancel_ChangesStatusToCancelled()
        {
            var order = new Order();
            order.Cancel();
            Assert.Equal(Status.Cancelled, order.Status);
        }

        [Fact]
        public void Pay_WhenAlreadyPaid_DoesNotChangeStatus()
        {
            var order = new Order();
            order.Pay();
            order.Pay();
            Assert.Equal(Status.Paid, order.Status);
        }

        [Fact]
        public void Cancel_WhenAlreadyCancelled_DoesNotChangeStatus()
        {
            var order = new Order();
            order.Cancel();
            order.Cancel();
            Assert.Equal(Status.Cancelled, order.Status);
        }
    }
}
