using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelLibrary.Models.Entities;

namespace TravelLibrary.Data
{
    public class TravelLibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Editorial> Editorials { get; set; }
        public DbSet<AuthorHasBook> AuthorHasBooks { get; set; }

        public TravelLibraryContext(DbContextOptions<TravelLibraryContext> options) : base(options)
        { }

        public TravelLibraryContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(b => {
                b.ToTable("Libro");
                b.HasKey(k => k.Isbn);
                b.Property(p => p.Isbn).ValueGeneratedNever();
                b.Property(p => p.EditorialId).HasColumnName("editorial_id");
                b.Property(p => p.Titlle).HasColumnName("titulo")
                                         .HasMaxLength(45);
                b.Property(p => p.Sypnosis).HasColumnName("sipnosis")
                                           .HasColumnType("nvarchar(max)");

                b.Property(p => p.NumPages).HasColumnName("n_paginas")
                                           .HasMaxLength(45);

                b.HasOne(e => e.Editorials)
                .WithMany(b => b.Books)
                .HasForeignKey(k => k.EditorialId);
            });

            modelBuilder.Entity<Author>(a => {
                a.ToTable("autor");
                a.HasKey(k => k.Id);
                a.Property(p => p.Id).ValueGeneratedNever();
                a.Property(p => p.Name).HasMaxLength(45)
                                       .HasColumnName("nombre");
                a.Property(p => p.LastName).HasColumnName("apellidos")
                                           .HasMaxLength(45);
            });

            modelBuilder.Entity<Editorial>(e => {
                e.ToTable("editorial");
                e.HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedNever();
                e.Property(p => p.Name).HasColumnName("nombre")
                                       .HasMaxLength(45);
                e.Property(p => p.Office).HasColumnName("sede")
                                         .HasMaxLength(45);
            });

            modelBuilder.Entity<AuthorHasBook>(a =>
            {
                a.ToTable("autor_has_libro");
                a.HasKey(k => new { k.AuthorID, k.BookIsbn });
                a.Property(p => p.AuthorID).HasColumnName("autor_id");
                a.Property(p => p.BookIsbn).HasColumnName("libro_ISBN");

                a.HasOne(a => a.Authors)
                .WithMany(a => a.AuthorHasBooks)
                .HasForeignKey(k => k.AuthorID);

                a.HasOne(a => a.Books)
                .WithMany(a => a.AuthorHasBooks)
                .HasForeignKey(k => k.BookIsbn);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //configure conection for a tests

            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                var _connectionString = configuration.GetSection("ConnectionStrings:TravelLibrary").Value;
                optionsBuilder.UseSqlServer(_connectionString.ToString());
            }
        }
    }  
}