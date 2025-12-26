using System;

namespace SmartHome.Common
{
    public abstract class SmartDevice
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public SmartDevice(string name) => Name = name;

        // Статичний метод (вимога ЛР №2)
        public static void ShowSystemInfo() => 
            Console.WriteLine("Система: SmartHome v2.0 (Асинхронність та LINQ)");

        public virtual void Info() => 
            Console.WriteLine($"[Пристрій] {Name} | Статус: {(IsEnabled ? "Активний" : "Вимкнений")}");
    }

    public class Light : SmartDevice
    {
        public int Brightness { get; set; } // Цифрове значення для статистики
        public string Room { get; set; }

        public Light(string name, string room) : base(name) => Room = room;

        // Статичний метод створення випадкового об'єкта (вимога ЛР №2)
        public static Light CreateNew()
        {
            var rnd = new Random();
            string[] rooms = { "Кухня", "Вітальня", "Спальня", "Офіс" };
            return new Light($"Лампа_{rnd.Next(100, 999)}", rooms[rnd.Next(rooms.Length)])
            {
                Brightness = rnd.Next(0, 101),
                IsEnabled = rnd.Next(0, 2) == 1
            };
        }
    }

    // Метод розширення (вимога ЛР №2)
    public static class TextExtensions
    {
        public static string BoldStyle(this string str) => $"*** {str.ToUpper()} ***";
    }
}