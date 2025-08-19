namespace Task3
{
    class Book
    {
        string title;
        string author;
        string ISBN;
        bool available;
        public bool borrowed = false;
        public bool returned = true;

        public Book(string title, string author, string iSBN, bool available = true)
        {
            this.title = title;
            this.author = author;
            ISBN = iSBN;
            this.available = available;
        }

        void SetTitle(string title)
        {
            this.title = title;
        }
        public string GetTitle()
        {
            return this.title;
        }
        void SetAuthor(string author)
        {
            this.author = author;
        }
        public string GetAuthor()
        {
            return this.author;
        }
        void SetISBN(string iSBN)
        {
            this.ISBN = iSBN; 
        }
        string GetISBN()
        {
            return this.ISBN;
        }
        void SetAvailable()
        {
            this.available = true;
        }
        bool GetAvailabilty()
        {
            return this.available;
        }
    }

    class Library
    {
        List<Book> books = new List<Book>();

        public void AddBook (Book book)
        {
            books.Add(book);
        }

        public bool SearchBook(string title)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].GetAuthor() == title || books[i].GetTitle() == title)
                {
                    return true;
                }
            }
            return false;
        }
        public string BorrowBook(string title)
        {
            if (SearchBook(title))
            {
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].GetTitle() == title || books[i].GetAuthor() == title)
                    {
                        books[i].borrowed = true;
                        books[i].returned = false;
                        return $"Borrowed Book {books[i].GetTitle()}";
                    }
                }
            }
            return "Book not found";
        }
        public string ReturnBook(string title)
        {
            if (SearchBook(title))
            {
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].GetTitle() == title || books[i].GetAuthor() == title)
                    {
                        if (books[i].borrowed)
                        {
                            books[i].borrowed = false;
                            books[i].returned = true;
                            return $"Returned Book {books[i].GetTitle()} Successfully";
                        }
                    }
                }
            }
            return "Book not borrowed";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            // Adding books to the library
            library.AddBook(new Book("Gatsby", "F. Scott Fitzgerald", "9780743273565"));
            library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084"));
            library.AddBook(new Book("1984", "George Orwell", "9780451524935"));

            // Searching and borrowing books
            Console.WriteLine("Searching and borrowing books...");
            Console.WriteLine( library.BorrowBook("Gatsby"));
            Console.WriteLine(library.BorrowBook("1984"));
            Console.WriteLine(library.BorrowBook("Harry Potter")); // This book is not in the library

            // Returning books
            Console.WriteLine("\nReturning books...");
            Console.WriteLine( library.ReturnBook("Gatsby"));
            Console.WriteLine( library.ReturnBook("Harry Potter")); // This book is not borrowed

            Console.ReadLine();
        }
    }
}
