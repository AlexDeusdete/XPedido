public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Photo { get; set; }
    public float Price { get; set; }
    public int? Category_id { get; set; }
    public Category Category { get; set; }
}
