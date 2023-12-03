// using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Character
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Species { get; set; }
    public string Type { get; set; }
    public string Gender { get; set; }
    public Origin Origin { get; set; }
    public CharacterLocation Location { get; set; }
    public string Image { get; set; }
    [NotMapped]
    public List<string> Episode {get;set;}
    public List<Episode> Episodes { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
}
