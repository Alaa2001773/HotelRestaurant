using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<BookaTable> BookaTables { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
               .HasKey(r => r.Id);


            modelBuilder.Entity<BookaTable>()
               .HasKey(b => b.Id);

            modelBuilder.Entity<BookaTable>()
                .HasOne(b => b.Restaurant)
                .WithMany(r => r.BookaTable)
                .HasForeignKey(b => b.RestaurantId);

            modelBuilder.Entity<BookaTable>()
                .HasOne(b => b.Guest)
                .WithMany(g => g.BookaTables)
                .HasForeignKey(b => b.GuestId);


            modelBuilder.Entity<GiftCard>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<GiftCard>()
                .HasOne(g => g.Restaurant)
                .WithMany(r => r.GiftCards)
                .HasForeignKey(g => g.RestaurantId);

            modelBuilder.Entity<GiftCard>()
                .HasOne(g => g.Guest)
                .WithMany(g => g.GiftCards)
                .HasForeignKey(g => g.GuestId);

            modelBuilder.Entity<Menu>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Restaurant)
                .WithMany(r => r.Menu)
                .HasForeignKey(m => m.RestaurantId);

            modelBuilder.Entity<Guest>()
              .HasKey(g => g.Id);

            modelBuilder.Entity<Guest>()
                .HasMany(g => g.BookaTables)
                .WithOne(b => b.Guest)
                .HasForeignKey(b => b.GuestId);

            modelBuilder.Entity<Guest>()
                .HasMany(g => g.GiftCards)
                .WithOne(gc => gc.Guest)
                .HasForeignKey(gc => gc.GuestId);


        }

    }
}
