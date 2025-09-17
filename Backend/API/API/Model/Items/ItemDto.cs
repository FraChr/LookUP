namespace API.Model.Items;

public class ItemDto
{
    public string? Name { get; set; }
    public int Amount { get; set; }
    public int LocationId { get; set; }
    public int? ShelfId { get; set; }
}