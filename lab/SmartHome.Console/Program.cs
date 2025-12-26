using SmartHome.Common;
using System.Text;

// Налаштування для підтримки кирилиці в консолі
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("--- Запуск демонстрації ЛР №1 ---".Decorate());

// 1. Ініціалізація CRUD сервісу для ламп
var lightService = new GenericCrudService<Light>();

// 2. Створення об'єктів (Використання конструкторів)
var kitchenLamp = new Light("Кухонна люстра", "Кухня");
var bedroomLamp = new Light("Нічник", "Спальня");

// 3. Підписка на подію (Використання делегатів та подій)
kitchenLamp.OnBrightnessChanged += (level) => 
{
    Console.WriteLine($"[СИСТЕМА] Яскравість пристрою '{kitchenLamp.Name}' змінено на {level}%");
};

// 4. CRUD: Create (Додавання в колекцію)
lightService.Create(kitchenLamp);
lightService.Create(bedroomLamp);

// 5. CRUD: Read All (Виведення списку)
Console.WriteLine("\nСписок пристроїв у системі:");
foreach (var lamp in lightService.ReadAll())
{
    lamp.Info(); // Виклик методу класу
}

// 6. Демонстрація роботи події та методів
Console.WriteLine("\nНалаштування освітлення:");
kitchenLamp.IsEnabled = true;
kitchenLamp.SetBrightness(75);

// 7. Статичні методи та поля
Console.WriteLine("\nІнформація про систему:");
SmartDevice.PrintSystemInfo();

// 8. Додаткове завдання (Збереження у файл)
string fileName = "home_data.json";
lightService.Save(fileName);
Console.WriteLine($"\nСтан системи збережено у файл: {fileName}");

Console.WriteLine("\n--- Роботу програми завершено ---".Decorate());