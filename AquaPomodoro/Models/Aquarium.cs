using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquaPomodoro.Models;

[Table("aquariums", Schema = "AquariumPomodoro")]
public class Aquarium
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userID")]
    public int UserId { get; set; }

    [Column("fishID")]
    public int FishId { get; set; }

    [Column("addedAt")]
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("FishId")]
    public virtual Fish Fish { get; set; } = null!;
} 