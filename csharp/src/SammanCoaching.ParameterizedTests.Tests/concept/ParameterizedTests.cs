using Xunit;
using SammanCoaching.ParameterizedTests;

namespace SammanCoaching.ParameterizedTests.Tests.Concept
{
    public class ParameterizedTests
    {
        [Theory]
        [InlineData(Status.Ordered, "Pay", Status.Paid)]
        [InlineData(Status.Ordered, "Cancel", Status.Cancelled)]
        [InlineData(Status.Paid, "Pay", Status.Paid)]
        [InlineData(Status.Paid, "Cancel", Status.Paid)]
        [InlineData(Status.Cancelled, "Pay", Status.Cancelled)]
        [InlineData(Status.Cancelled, "Cancel", Status.Cancelled)]
        public void Order_State_Transitions(Status initialStatus, string action, Status expectedStatus)
        {
            var order = new Order();
            typeof(Order).GetProperty("Status").SetValue(order, initialStatus);
            if (action == "Pay")
                order.Pay();
            else if (action == "Cancel")
                order.Cancel();
            Assert.Equal(expectedStatus, order.Status);
        }
    }
}
