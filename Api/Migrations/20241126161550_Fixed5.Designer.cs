﻿// <auto-generated />
using System;
using MAN.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241126161550_Fixed5")]
    partial class Fixed5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("MAN.Shared.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("MAN.Shared.Models.BindingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BindingTypes");
                });

            modelBuilder.Entity("MAN.Shared.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BindingTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ISBN")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<int?>("PageCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("YearPublished")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BindingTypeId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("MAN.Shared.Models.BookGenre", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(0);

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(1);

                    b.HasKey("GenreId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookGenres");
                });

            modelBuilder.Entity("MAN.Shared.Models.BookRead", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(1);

                    b.Property<int>("ProfileId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(0);

                    b.Property<DateOnly?>("DateFinished")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("DateStarted")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Review")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasMaxLength(7)
                        .HasColumnType("TEXT");

                    b.HasKey("BookId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("BookReads");
                });

            modelBuilder.Entity("MAN.Shared.Models.CoAuthors", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(0);

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(1);

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("CoAuthors");
                });

            modelBuilder.Entity("MAN.Shared.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MAN.Shared.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("MAN.Shared.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublisherName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("MAN.Shared.Models.Book", b =>
                {
                    b.HasOne("MAN.Shared.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MAN.Shared.Models.BindingType", "BindingType")
                        .WithMany("Books")
                        .HasForeignKey("BindingTypeId");

                    b.HasOne("MAN.Shared.Models.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId");

                    b.Navigation("Author");

                    b.Navigation("BindingType");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("MAN.Shared.Models.BookGenre", b =>
                {
                    b.HasOne("MAN.Shared.Models.Book", "Book")
                        .WithMany("BookGenres")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MAN.Shared.Models.Genre", "Genre")
                        .WithMany("BookGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("MAN.Shared.Models.BookRead", b =>
                {
                    b.HasOne("MAN.Shared.Models.Book", "Book")
                        .WithMany("BookReads")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MAN.Shared.Models.Profile", "Profile")
                        .WithMany("BookReads")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("MAN.Shared.Models.CoAuthors", b =>
                {
                    b.HasOne("MAN.Shared.Models.Author", "Author")
                        .WithMany("Coauthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MAN.Shared.Models.Book", "Book")
                        .WithMany("Coauthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("MAN.Shared.Models.Author", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Coauthors");
                });

            modelBuilder.Entity("MAN.Shared.Models.BindingType", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MAN.Shared.Models.Book", b =>
                {
                    b.Navigation("BookGenres");

                    b.Navigation("BookReads");

                    b.Navigation("Coauthors");
                });

            modelBuilder.Entity("MAN.Shared.Models.Genre", b =>
                {
                    b.Navigation("BookGenres");
                });

            modelBuilder.Entity("MAN.Shared.Models.Profile", b =>
                {
                    b.Navigation("BookReads");
                });
#pragma warning restore 612, 618
        }
    }
}
