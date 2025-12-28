namespace SmartHome.Infrastructure.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // 1:N — В одній кімнаті багато пристроїв
        public List<SmartDeviceModel> Devices { get; set; } = new();
    }

    public class WarrantyModel
    {
        public int Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SmartDeviceModelId { get; set; } // FK
        public SmartDeviceModel Device { get; set; }
    }

    public class GroupModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<SmartDeviceModel> Devices { get; set; } = new();
    }
}