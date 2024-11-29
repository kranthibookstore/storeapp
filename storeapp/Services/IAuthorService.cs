using BookStore.EFLib.Models;

namespace storeapp.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync (Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync (int id);
    }
}
