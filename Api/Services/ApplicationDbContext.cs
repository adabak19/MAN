using Microsoft.EntityFrameworkCore;
using LibraryManagement.Shared.Models;
namespace MAN.Api.Services;

public partial class ApplicationDbContext : DbContext
{

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookRead> BookReads => Set<BookRead>();
    public DbSet<BindingType> BindingTypes => Set<BindingType>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();
    public DbSet<CoAuthors> CoAuthors => Set<CoAuthors>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = library.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           modelBuilder.Entity<Author>().HasKey(k => k.Id);
           modelBuilder.Entity<Book>().HasKey(k => k.Id);
           modelBuilder.Entity<BindingType>().HasKey(k => k.Id);
           modelBuilder.Entity<BookGenre>().HasKey(k => new {k.GenreId, k.BookId});
           modelBuilder.Entity<BookRead>().HasKey(k => new {k.BookId, k.ProfileId});
           modelBuilder.Entity<CoAuthors>().HasKey(k => new {k.BookId, k.AuthorId});
           modelBuilder.Entity<Genre>().HasKey(k => k.Id);
           modelBuilder.Entity<Profile>().HasKey(k => k.Id);
           modelBuilder.Entity<Publisher>().HasKey(k => k.Id);
           modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedOnAdd();
           OnModelCreatingPartial(modelBuilder);
       

 

        // Configure many-to-many relationship between Book and Genre
        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Book)
            .WithMany(b => b.BookGenres)
            .HasForeignKey(bg => bg.BookId);

        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Genre)
            .WithMany(g => g.BookGenres)
            .HasForeignKey(bg => bg.GenreId);

        // Configure optional properties and constraints
        modelBuilder.Entity<Book>()
            .Property(b => b.ISBN)
            .HasMaxLength(15);

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Genre>()
            .Property(g => g.GenreName)
            .IsRequired()
            .HasMaxLength(50);

        // Call partial method for additional configurations
        OnModelCreatingPartial(modelBuilder);
    }


       partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}