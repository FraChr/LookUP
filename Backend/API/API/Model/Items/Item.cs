using System.Runtime.InteropServices.JavaScript;

namespace API.Model.Items;

public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Amount { get; set; }
    public int LocationId { get; set; }
    public int UserId { get; set; }
    public int? ShelfsId { get; set; }
    public DateOnly Timestamp { get; set; }

    public Shelf.Shelfs Shelfs { get; set; }
    public Location.Location Location { get; set; }
    public User.User User { get; set; }
}