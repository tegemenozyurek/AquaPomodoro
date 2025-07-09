using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquaPomodoro.Models;

[Table("fishes", Schema = "AquariumPomodoro")]
public class Fish
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public int Price { get; set; }

    [Column("type")]
    public FishType Type { get; set; } = FishType.Small;

    [MaxLength(500)]
    [Column("gifURL")]
    public string GifUrl { get; set; } = "no url";

    [Column("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual ICollection<Aquarium> Aquariums { get; set; } = new List<Aquarium>();
} 