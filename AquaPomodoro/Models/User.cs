using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquaPomodoro.Models;

[Table("users", Schema = "AquariumPomodoro")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("mail")]
    public string Mail { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [Column("password")]
    public string Password { get; set; } = string.Empty;

    [Column("coinBalance")]
    public int CoinBalance { get; set; } = 100;

    [Column("totalFocusTime")]
    public int TotalFocusTime { get; set; } = 0;

    [Column("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual ICollection<Aquarium> Aquariums { get; set; } = new List<Aquarium>();
} 