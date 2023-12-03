using Microsoft.EntityFrameworkCore;

[Owned]
public class Origin
{
    public string? Name { get; set; }
    public string? Url { get; set; }
}