namespace SmartHome.REST.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LightDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Brightness { get; set; }
        public int RoomId { get; set; }
    }
}