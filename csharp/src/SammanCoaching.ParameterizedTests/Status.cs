namespace SammanCoaching.ParameterizedTests;

public enum Status
{
    /**
     * Placed, but not payed yet. Still changeable.
     */
    PaymentExpected = 0,

    /**
     * {@link Order} was payed. No changes allowed to it anymore.
     */
    Paid,

    /**
     * The {@link Order} is currently processed.
     */
    Preparing,

    /**
     * The {@link Order} is ready to be picked up by the customer.
     */
    Ready,

    /**
     * The {@link Order} was completed.
     */
    Taken
}