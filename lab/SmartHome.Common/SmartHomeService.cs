using System.Text.Json;

namespace SmartHome.Common
{
    public interface ICrudService<T>
    {
        void Create(T element);
        T Read(Guid id);
        IEnumerable<T> ReadAll();
        void Update(T element);
        void Remove(T element);
    }

    public class GenericCrudService<T> : ICrudService<T> where T : class
    {
        private List<T> _items = new List<T>();

        public void Create(T element) => _items.Add(element);
        
        public IEnumerable<T> ReadAll() => _items;

        public T Read(Guid id) => _items.FirstOrDefault(x => (Guid)x.GetType().GetProperty("Id")?.GetValue(x) == id);

        public void Update(T element)
        {
            var id = (Guid)element.GetType().GetProperty("Id")?.GetValue(element);
            var index = _items.FindIndex(x => (Guid)x.GetType().GetProperty("Id")?.GetValue(x) == id);
            if (index != -1) _items[index] = element;
        }

        public void Remove(T element) => _items.Remove(element);

        // Додаткове завдання: Save/Load
        public void Save(string path) => File.WriteAllText(path, JsonSerializer.Serialize(_items));
        public void Load(string path) 
        {
            if (File.Exists(path))
                _items = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path)) ?? new List<T>();
        }
    }
}