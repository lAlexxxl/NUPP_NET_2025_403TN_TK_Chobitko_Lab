using SmartHome.Infrastructure;
using SmartHome.Infrastructure.Models;
using SmartHome.Common;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

// Ініціалізація Контексту
using (var context = new SmartHomeContext())
{
    // ЦЕЙ РЯДОК СТВОРЮЄ БАЗУ ДАНИХ АВТОМАТИЧНО БЕЗ МІГРАЦІЙ
    Console.WriteLine("Перевірка та створення бази даних...");
    context.Database.EnsureCreated(); 

    var repository = new Repository<LightModel>(context);
    var service = new GenericCrudServiceAsync<LightModel>(repository);

    Console.WriteLine("--- ЛАБОРАТОРНА РОБОТА №3: SmartHome EF Core ---");

    // Створення кімнати та пристрою
    var room = new RoomModel { Name = "Вітальня" };
    var lamp = new LightModel 
    { 
        Name = "Смарт-Лампа БД", 
        Brightness = 90, 
        Room = room,
        Warranty = new WarrantyModel { ExpiryDate = DateTime.Now.AddYears(1) }
    };

    Console.WriteLine("Додавання запису в SQLite...");
    await service.CreateAsync(lamp);

    // Читання
    var devices = await service.ReadAllAsync();
    Console.WriteLine("\nДані з бази даних:");
    foreach (var d in devices)
    {
        Console.WriteLine($"- Пристрій: {d.Name}, Кімната: {d.RoomId}");
    }
}