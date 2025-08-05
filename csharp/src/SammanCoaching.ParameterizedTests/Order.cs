namespace SammanCoaching.ParameterizedTests;

public class Order {

    public readonly long Id;
    public Status Status { get; set; }

    public Order(long id) {
        this.Id = id;
    }


    public bool IsUpdateAllowed() {
        return !IsPaid();
    }

    public bool IsCancelAllowed() {
        return !IsPaid();
    }

    // BUG! All statuses except PAYMENT_EXPECTED mean that it is paid.
    // Off by one error: 0 should also return true.
    public bool IsPaid() {
        return Status.CompareTo(Status.Paid) > 0;
    }

}
