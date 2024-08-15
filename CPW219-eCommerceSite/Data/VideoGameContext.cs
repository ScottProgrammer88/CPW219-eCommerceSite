using CPW219_eCommerceSite.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlTypes;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Threading.Channels;
using System;
using Microsoft.VisualBasic;
using CPW219_eCommerceSite.Models;
namespace CPW219_eCommerceSite.Data
{

//    DbContext Class in Entity Framework
//Entity Framework(EF) is a tool that helps you work with databases using .NET code.When you want to use EF in your application, you need a special class called DbContext.Here's what it does and how it works:

//Main Role:

//The DbContext class is like the manager that handles everything related to the database in your application.It keeps track of the data, knows how to save changes to the database, and retrieves data when you need it.
//Creating the DbContext Class:

//To make your own DbContext class, you need to create a new class in your project that inherits(or derives) from the Microsoft.EntityFrameworkCore.DbContext class. Think of it like making a new recipe by starting with a basic recipe template.
//Including Entities:

//An "entity" is just a fancy term for an object that represents a table in your database. For example, if you have a table for storing information about books, you would have a Book entity.
//In your custom DbContext class, you need to tell EF which entities(or tables) you are working with.You do this by including properties of type DbSet<T>, where T is the entity type (like Book).
//Example
//Let's say you are creating an application to manage a library. You have a Book class that represents the books in the library.

//Step 1: Create the Entity Class

//public class Book
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Author { get; set; }
//    }
//    Step 2: Create the DbContext Class

//using Microsoft.EntityFrameworkCore;

//public class LibraryContext : DbContext
//    {
//        // This property represents the Books table in the database
//        public DbSet<Book> Books { get; set; }

//        // This method configures the database connection
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            // Set the database to use (e.g., a local SQLite database)
//            optionsBuilder.UseSqlite("Data Source=library.db");
//        }
//    }
//    Explanation
//    LibraryContext Class: This is your custom DbContext class. It derives from DbContext, which means it gets all the functionality of DbContext plus any custom behavior you add.

//    DbSet<Book> Books: This property tells EF that you have a Book entity, and you want to interact with a table in the database that holds books.The DbSet<Book> type is like a collection of Book objects.


//    OnConfiguring Method: This method is used to set up the connection to the database.In this example, it configures EF to use an SQLite database named library.db.

//    Putting It All Together
//    When you create an instance of LibraryContext and call its methods, EF will know how to connect to the database and work with the Books table.You can then perform CRUD operations on the Books table through this context.

//using (var context = new LibraryContext())
//{
//    // Create a new book
//    var book = new Book { Title = "1984", Author = "George Orwell" };
//    context.Books.Add(book);
//    context.SaveChanges();

//    // Read all books
//    var books = context.Books.ToList();

//    // Update a book
//    var firstBook = books.First();
//    firstBook.Title = "Animal Farm";
//    context.SaveChanges();

//    // Delete a book
//    context.Books.Remove(firstBook);
//    context.SaveChanges();
//}
//In this example:

//Add a new book to the Books table.
//ToList gets all the books from the Books table.
//Update changes the title of the first book.
//Remove deletes the first book from the table.
//SaveChanges commits these changes to the database.


    /// <summary>
    /// Video Game Context class to interact with the database
    /// </summary>

    public class VideoGameContext : DbContext // the colon is inheritance in C#
    {
        /// <summary>
        /// Constructor to pass in options to the base class
        /// </summary>
        /// <param name="options"></param>
        public VideoGameContext(DbContextOptions<VideoGameContext> options) : base(options)
        {

        }


        /// <summary>
        /// A DbSet is a collection of entities that represent a table in the database. 
        /// An entity is a class that represents a table in the database.
        /// </summary>
        public DbSet<Game> Games { get; set; } // property to represent the table in the database


        

    }
}
