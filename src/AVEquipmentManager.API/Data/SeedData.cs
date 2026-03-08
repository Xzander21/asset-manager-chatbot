using AVEquipmentManager.Shared.Enums;
using AVEquipmentManager.Shared.Models;

namespace AVEquipmentManager.API.Data;

/// <summary>
/// Seed data for initial database population.
/// Modify this file to change the default equipment entries.
/// </summary>
public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Equipment.Any())
            return;

        var equipment = new List<Equipment>
        {
            // Room 1
            new Equipment
            {
                Name = "Projector",
                SerialNumber = "AV-R1-001",
                RoomName = "Room 1",
                DateInstalled = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                ExpectedLifeInYears = 5,
                Status = EquipmentStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new Equipment
            {
                Name = "Speaker System",
                SerialNumber = "AV-R1-002",
                RoomName = "Room 1",
                DateInstalled = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                ExpectedLifeInYears = 7,
                Status = EquipmentStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new Equipment
            {
                Name = "Microphone",
                SerialNumber = "AV-R1-003",
                RoomName = "Room 1",
                DateInstalled = new DateTime(2023, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                ExpectedLifeInYears = 3,
                Status = EquipmentStatus.UnderMaintenance,
                CreatedAt = DateTime.UtcNow
            },

            // Room 2
            new Equipment
            {
                Name = "LED Display",
                SerialNumber = "AV-R2-001",
                RoomName = "Room 2",
                DateInstalled = new DateTime(2023, 3, 20, 0, 0, 0, DateTimeKind.Utc),
                ExpectedLifeInYears = 8,
                Status = EquipmentStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new Equipment
            {
                Name = "Amplifier",
                SerialNumber = "AV-R2-002",
                RoomName = "Room 2",
                DateInstalled = new DateTime(2022, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                ExpectedLifeInYears = 6,
                Status = EquipmentStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new Equipment
            {
                Name = "Webcam",
                SerialNumber = "AV-R2-003",
                RoomName = "Room 2",
                DateInstalled = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                ExpectedLifeInYears = 4,
                Status = EquipmentStatus.Retired,
                CreatedAt = DateTime.UtcNow
            },

            // Room 3
            new Equipment
            {
                Name = "Interactive Whiteboard",
                SerialNumber = "AV-R3-001",
                RoomName = "Room 3",
                DateInstalled = new DateTime(2023, 9, 15, 0, 0, 0, DateTimeKind.Utc),
                ExpectedLifeInYears = 10,
                Status = EquipmentStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new Equipment
            {
                Name = "Soundbar",
                SerialNumber = "AV-R3-002",
                RoomName = "Room 3",
                DateInstalled = new DateTime(2024, 5, 20, 0, 0, 0, DateTimeKind.Utc),
                ExpectedLifeInYears = 5,
                Status = EquipmentStatus.Active,
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Equipment.AddRange(equipment);
        context.SaveChanges();
    }
}
