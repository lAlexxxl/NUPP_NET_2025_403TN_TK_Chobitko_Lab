using SmartHome.Common;
using System.Text;

// Налаштування кирилиці
Console.OutputEncoding = Encoding.UTF8;

// Створюємо асинхронний сервіс
var service = new GenericCrudServiceAsync<Light>();
const int totalItems = 1200; // Вимога ЛР №2: понад 1000 об'єктів

// Виклик методу розширення BoldStyle (замість Decorate)
Console.WriteLine("ЛАБОРАТОРНА РОБОТА №2".BoldStyle());
Console.WriteLine($"Запуск паралельного створення {totalItems} об'єктів...");

// 1. ПАРАЛЕЛЬНЕ СТВОРЕННЯ через Parallel.For (Вимога ЛР №2)
Parallel.For(0, totalItems, i => {
    var lamp = Light.CreateNew();
    // Оскільки CreateAsync асинхронний, чекаємо на результат
    service.CreateAsync(lamp).GetAwaiter().GetResult();
});

var allData = await service.ReadAllAsync();

// 2. LINQ: АНАЛІЗ ЦИФРОВИХ ЗНАЧЕНЬ (Вимога ЛР №2)
var avgBrightness = allData.Average(l => l.Brightness);
var maxBrightness = allData.Max(l => l.Brightness);
var minBrightness = allData.Min(l => l.Brightness);

Console.WriteLine("\n--- РЕЗУЛЬТАТИ АНАЛІЗУ LINQ ---");
Console.WriteLine($"Середня яскравість: {avgBrightness:F2}%");
Console.WriteLine($"Максимальна яскравість: {maxBrightness}%");
Console.WriteLine($"Мінімальна яскравість: {minBrightness}%");

// 3. ПРИМІТИВИ СИНХРОНІЗАЦІЇ (LOCK та SEMAPHORE)
Console.WriteLine("\n--- ДЕМОНСТРАЦІЯ СИНХРОНІЗАЦІЇ ---");
object myLock = new();
lock(myLock) { Console.WriteLine("[Lock] Критична секція пройдена."); }

Semaphore semaphore = new Semaphore(1, 1);
semaphore.WaitOne();
Console.WriteLine("[Semaphore] Доступ до ресурсу отримано.");
semaphore.Release();

// 4. СТАТИЧНІ МЕТОДИ ТА ЗБЕРЕЖЕННЯ
SmartDevice.ShowSystemInfo(); // Викликаємо нову назву методу
await service.SaveAsync();

Console.WriteLine("\nДані збережено асинхронно у файл async_data.json");
Console.WriteLine("ВИКОНАННЯ ЗАВЕРШЕНО".BoldStyle());