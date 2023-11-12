namespace EcommersStoreApi.Models
{
    public class EcommenrsSSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksColectionName { get; set; } = null!;
        public string CartColectionName { get; set; } = null!;
    }
}