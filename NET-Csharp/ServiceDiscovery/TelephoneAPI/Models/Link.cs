namespace TelephoneAPI.Models;
public class Link {  
    public long Id {get; set;}
    public string? Href { get; set; }  
    public string? Rel { get; set; }  
    public string? Method { get; set; }  
    public Link(long Id, string href, string rel, string method) {
        this.Id = Id;
        this.Href = href;
        this.Rel = rel;
        this.Method = method;  
    }
}