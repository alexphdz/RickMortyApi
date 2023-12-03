using System.ComponentModel.DataAnnotations.Schema;

public class Episode
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Air_Date { get; set; }
    public string episode { get; set; }
    [NotMapped]
    public List<string> Characters { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }

    // Relación con Character
    public int CharacterId { get; set; }
    public Character Character { get; set; }
}
