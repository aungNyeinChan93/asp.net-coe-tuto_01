using System.Runtime.CompilerServices;

namespace asp_tuto_01.Classes.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int? Year { get; set; } = null;
        
        public Book() { }

        public Book(int id, string title, string author, int? year)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
        }

    }

    static class BookRepository
    {
        private static List<Book> _books = [
            new (1,"C# Beginner","Auther Aung",2024),
            new (2,"JAVA Beginner","Auther Chan",2024),
            new (3,"Dart Beginner","Auther Nyein",2024),
            ];

        public static List<Book> GetAllBooks() => BookRepository._books;

        public static void AddBook(Book? book) => BookRepository._books.Add(book!);

        public static bool UpdateBook(Book? book)
        {
            if(book is  null) return false;
            
            Book? oldBook = _books.FirstOrDefault(book => book.Id == book?.Id);

            if(oldBook is  null) return false;
            
                oldBook.Id = book.Id;
                oldBook.Title = book.Title;
                oldBook.Author = book.Author;
                oldBook.Year = book.Year;
                return true;
        }

        public static bool DeleteBook(int? id)
        {
            if (id is null) return false;

            var book = _books.FirstOrDefault(b => b.Id == id);

            if(book is null) return false;

            _books.Remove(book);

            return true;
        }
    }
}
