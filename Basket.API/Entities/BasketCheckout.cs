namespace Basket.API.Entities
{
    public class BasketCheckout
    {
        public required string Username { get; set; }
        public decimal TotalPrice { get; set; }

        //BillingAdress
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public required string EmailAdress { get; set; }
        public string AdressLine { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        //Payment
        public required string CardName { get; set; }
        public required string CardNumber { get; set; }
        public required string Expiration { get; set; }
        public required string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
