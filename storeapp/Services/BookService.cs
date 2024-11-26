
using AutoMapper;
using BookStore.EFLib.Models;
using storeapp.Model;
using storeapp.Repositories;


namespace storeapp.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper) 
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task AddBookAsync(BookModel book)
        {
            Book addBook = new Book
            {
                BookId = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                Stock = book.Stock
            };
            await _bookRepository.AddBookAsync(addBook);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            var result = await _bookRepository.GetAllBooksAsync();
            return result.FirstOrDefault(_ => _.BookId == id)!;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            // _mapper.Map<Book>(book);
            return book;

        }
    

        public async Task UpdateBookAsync(BookModel book)
        {
            var bookUpdate = new Book
            {
                BookId = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                Stock = book.Stock
            };
            await _bookRepository.UpdateBookAsync(bookUpdate);
        }

       
    }
}
