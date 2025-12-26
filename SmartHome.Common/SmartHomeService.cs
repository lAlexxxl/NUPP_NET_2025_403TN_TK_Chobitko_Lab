using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome.Common
{
    public interface ICrudServiceAsync<T> : IEnumerable<T>
    {
        Task<bool> CreateAsync(T element);
        Task<T> ReadAsync(Guid id);
        Task<IEnumerable<T>> ReadAllAsync();
        Task<IEnumerable<T>> ReadAllAsync(int page, int amount);
        Task<bool> UpdateAsync(T element);
        Task<bool> RemoveAsync(T element);
        Task<bool> SaveAsync();
    }

    public class GenericCrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private List<T> _items = new List<T>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly string _path = "async_data.json";

        public async Task<bool> CreateAsync(T element)
        {
            await _semaphore.WaitAsync();
            try { _items.Add(element); return true; }
            finally { _semaphore.Release(); }
        }

        public async Task<IEnumerable<T>> ReadAllAsync() => _items;

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount) => 
            _items.Skip((page - 1) * amount).Take(amount);

        public async Task<T> ReadAsync(Guid id) => 
            _items.FirstOrDefault(x => (Guid)x.GetType().GetProperty("Id")?.GetValue(x) == id);

        public async Task<bool> SaveAsync()
        {
            await _semaphore.WaitAsync();
            try {
                var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_path, json);
                return true;
            } finally { _semaphore.Release(); }
        }

        public async Task<bool> UpdateAsync(T element) => true;
        public async Task<bool> RemoveAsync(T element) => true;

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}