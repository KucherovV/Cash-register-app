using System;

namespace kassa
{
    class OneRecipie
    {
        private int id;
        private DateTime dateTime;
        private string productList;
        private int price;
        private string paymentType;
        private bool takeAway;
        private string comment;
        private string cashier; 

        public OneRecipie(int id, DateTime dateTime, string productList, int price, string paymentType,
            bool takeAway, string comment, string cashier)
        {
            Id = id;
            DateTime = dateTime;
            ProductList = productList;
            Price = price;
            PaymentType = paymentType;
            TakeAway = takeAway;
            Comment = comment;
            Cashier = cashier;
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string ProductList { get; set; }
        public int Price { get; set; }
        public string PaymentType { get; set; }
        public bool TakeAway { get; set; }
        public string Comment { get; set; }
        public string Cashier { get; set; }
    }
}
