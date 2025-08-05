namespace SammanCoaching.ParameterizedTests
{
    public class Order
    {
        public Status Status { get; private set; }

        public Order()
        {
            Status = Status.Ordered;
        }

        public void Pay()
        {
            if (Status == Status.Ordered)
            {
                Status = Status.Paid;
            }
        }

        public void Cancel()
        {
            if (Status == Status.Ordered)
            {
                Status = Status.Cancelled;
            }
        }
    }
}
