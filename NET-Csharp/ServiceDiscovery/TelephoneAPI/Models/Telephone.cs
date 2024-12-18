namespace TelephoneAPI.Models;
public class Telephone {
    public long Id {get; set;}
    public string? Name {get; set;}
    public string? PhoneNumber {get; set;}
    public List<Link> Links {get; set;} = new List<Link>();
}