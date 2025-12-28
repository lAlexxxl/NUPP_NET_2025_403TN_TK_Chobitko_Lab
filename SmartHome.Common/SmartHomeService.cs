using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.Infrastructure; // Додаємо посилання на репозиторій

namespace SmartHome.Common
{
    public interface ICrudServiceAsync<T> : IEnumerable<T> where T : class
    {
        Task<bool> CreateAsync(T element);
        Task<T> ReadAsync(int id); // В ЛР3 зазвичай переходимо на int Id для БД
        Task<IEnumerable<T>> ReadAllAsync();
        Task<IEnumerable<T>> ReadAllAsync(int page, int amount);
        Task<bool> UpdateAsync(T element);
        Task<bool> RemoveAsync(T element);
    }

    public class GenericCrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly IRepository<T> _repository;

        // Впровадження залежності через конструктор
        public GenericCrudServiceAsync(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _repository.AddAsync(element);
            return true;
        }

        public async Task<IEnumerable<T>> ReadAllAsync() 
            => await _repository.GetAllAsync();

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            var all = await _repository.GetAllAsync();
            return all.Skip((page - 1) * amount).Take(amount);
        }

        public async Task<T> ReadAsync(int id) 
            => await _repository.GetByIdAsync(id);

        public async Task<bool> UpdateAsync(T element)
        {
            await _repository.Update(element);
            return true;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            await _repository.Delete(element);
            return true;
        }

        // Реалізація IEnumerable (беремо дані з репозиторію)
        public IEnumerator<T> GetEnumerator() 
            => _repository.GetAllAsync().GetAwaiter().GetResult().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}