using System.ComponentModel.DataAnnotations;

namespace SmartHome.Infrastructure.Models
{
    public abstract class SmartDeviceModel
    {
        [Key]
        public int Id { get; set; }
        public Guid ExternalId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        
        // 1:N — Пристрій належить до однієї кімнати
        public int RoomId { get; set; }
        public RoomModel Room { get; set; }

        // 1:1 — У кожного пристрою є один гарантійний талон
        public WarrantyModel Warranty { get; set; }

        // N:N — Пристрій може бути в багатьох групах (Бонусні бали)
        public List<GroupModel> Groups { get; set; } = new();
    }

    public class LightModel : SmartDeviceModel
    {
        public int Brightness { get; set; }
    }

    public class ThermostatModel : SmartDeviceModel
    {
        public double TargetTemperature { get; set; }
    }
}