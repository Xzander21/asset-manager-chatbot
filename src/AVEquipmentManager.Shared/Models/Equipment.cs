using System.ComponentModel.DataAnnotations;
using AVEquipmentManager.Shared.Enums;

namespace AVEquipmentManager.Shared.Models;

public class Equipment
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string SerialNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string RoomName { get; set; } = string.Empty;

    [Required]
    public DateTime DateInstalled { get; set; }

    [Required]
    [Range(1, 100)]
    public int ExpectedLifeInYears { get; set; }

    public EquipmentStatus Status { get; set; } = EquipmentStatus.Active;

    [MaxLength(1000)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
