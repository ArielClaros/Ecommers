namespace Ecommers.API.Models
{
    public class Payment
    {
        public int Id {get; set; }

        public string UserName {get; set; } = "";

        public int TotalPrice {get; set; }
    }
}