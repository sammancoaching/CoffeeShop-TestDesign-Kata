using Xunit;

using SammanCoaching.ParameterizedTests;

namespace SammanCoaching.ParameterizedTests.Tests.Concrete;

public class OrderStateTest
{
    [Fact]
    public void NewOrder_ShouldBeOrdered()
    {
        Order order = new();
        Assert.Equal(Status.Ordered, order.Status);
    }

    [Fact]
    public void Pay_ChangesStatusToPaid()
    {
        Order order = new();
        order.Pay();
        Assert.Equal(Status.Paid, order.Status);
    }

    [Fact]
    public void Cancel_ChangesStatusToCancelled()
    {
        Order order = new();
        order.Cancel();
        Assert.Equal(Status.Cancelled, order.Status);
    }

    [Fact]
    public void Pay_WhenAlreadyPaid_DoesNotChangeStatus()
    {
        Order order = new();
        order.Pay();
        order.Pay();
        Assert.Equal(Status.Paid, order.Status);
    }

    [Fact]
    public void Cancel_WhenAlreadyCancelled_DoesNotChangeStatus()
    {
        Order order = new();
        order.Cancel();
        order.Cancel();
        Assert.Equal(Status.Cancelled, order.Status);
    }
}