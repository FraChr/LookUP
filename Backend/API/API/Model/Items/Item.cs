namespace API.Model.Items;

public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Amount { get; set; }

    public int LocationId { get; set; }
    public int UserId { get; set; }
    public Location.Location Location { get; set; }
    public User.User User { get; set; }
}