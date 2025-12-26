using System;

namespace SmartHome.Common
{
    // 1. Базовий клас
    public abstract class SmartDevice
    {
        // Статичне поле (Static Field)
        public static string Manufacturer = "NUPP Tech";

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        // Статичний конструктор (Static Constructor)
        static SmartDevice()
        {
            Console.WriteLine("--- Ініціалізація системи SmartHome ---");
        }

        // Конструктор (Constructor)
        public SmartDevice(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsEnabled = false;
        }

        // Статичний метод (Static Method)
        public static void PrintSystemInfo() => Console.WriteLine($"Виробник: {Manufacturer}");

        // Метод (Method)
        public virtual void Info() => Console.WriteLine($"Пристрій: {Name}, Статус: {(IsEnabled ? "Увімк." : "Вимк.")}");
    }

    // 2. Клас-наслідник (Inheritance 1)
    public class Light : SmartDevice
    {
        public int Brightness { get; set; }
        public string Color { get; set; }
        public string Room { get; set; }

        // Делегат та Подія (Delegate & Event)
        public delegate void BrightnessChanged(int val);
        public event BrightnessChanged OnBrightnessChanged;

        public Light(string name, string room) : base(name) 
        {
            Room = room;
            Brightness = 0;
            Color = "White";
        }

        public void SetBrightness(int value)
        {
            Brightness = value;
            // Виклик події
            OnBrightnessChanged?.Invoke(value);
        }
    }

    // 3. Клас-наслідник (Inheritance 2)
    public class Thermostat : SmartDevice
    {
        public double TargetTemperature { get; set; }
        public double CurrentTemperature { get; set; }
        public string Mode { get; set; }

        public Thermostat(string name, double target) : base(name)
        {
            TargetTemperature = target;
            CurrentTemperature = 20.5;
            Mode = "Auto";
        }
    }

    // 4. Окремий клас
    public class HomeUser
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string AccessLevel { get; set; }

        public HomeUser(string name, string level)
        {
            Id = Guid.NewGuid();
            FullName = name;
            AccessLevel = level;
        }
    }

    // Метод розширення (Extension Method)
    public static class StringExtensions
    {
        public static string Decorate(this string str) => $">>> {str} <<<";
    }
}