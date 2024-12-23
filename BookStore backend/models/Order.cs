namespace BookStore_backend.models
{
    public class Order
    {
        public int? Id { get; set; }
        public int? User_id { get; set; }
        public int? Book_id { get; set; }
        public int? Quantity { get; set; }
        public string? User_name { get; set; }
        public string? Book_name { get; set; }
        public int? Order_price { get; set; }
    }
}
