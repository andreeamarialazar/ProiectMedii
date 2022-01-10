using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectMedii.Models;
using Microsoft.EntityFrameworkCore;

namespace ProiectMedii.Data
{
    public class LibraryContext:DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) 
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<PublishedAlbum> PublishedAlbums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<Publisher>().ToTable("Publisher");
            modelBuilder.Entity<PublishedAlbum>().ToTable("PublishedAlbum");
            modelBuilder.Entity<PublishedAlbum>()
            .HasKey(c => new { c.AlbumID, c.PublisherID });//configureaza cheiaprimara compusa
        }
    }
}
