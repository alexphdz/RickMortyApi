using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Character
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int id { get; set; }
    public string? name { get; set; }
    public string? status { get; set; }
    public string? species { get; set; }
    public string? type { get; set; }
    public string? gender { get; set; }
    [Required]
    public Origin? origin { get; set; }
    [Required]
    public CharacterLocation? location { get; set; }
    public string? image { get; set; }
    [NotMapped]
    public List<string> episode { get; set; }
    public string? url { get; set; }
    public DateTime created { get; set; }
}