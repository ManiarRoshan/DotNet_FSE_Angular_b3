namespace EntityFC.Models
{
    public class Product
    {
        public int Id { get; set; }
        // Primary Key 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
