namespace API.Model.Items;

public class ItemViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Amount { get; set; }
    public string Location { get; set; }
    public string? Shelf { get; set; }
    public DateOnly Timestamp { get; set; }
}