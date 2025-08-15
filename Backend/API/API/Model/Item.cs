using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

namespace API.Model;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public string Location { get; set; }
}