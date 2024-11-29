﻿using BookStore.EFLib.Models;
using Microsoft.EntityFrameworkCore;

namespace storeapp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BookStoreContext _context;

        public AuthorService( BookStoreContext context)
        {
         _context = context;
            
        }

        public  async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task <Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
