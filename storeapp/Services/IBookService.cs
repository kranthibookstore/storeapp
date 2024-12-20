﻿using BookStore.EFLib.Models;
using storeapp.Dtos;
using storeapp.Model;

namespace storeapp.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBookAsync();
        Task<Book> GetBookById(int id);
        Task AddBookAsync(BookModel book);
        Task UpdateBookAsync(BookModel book);
        Task DeleteBookAsync(int id);
        
    }
}
