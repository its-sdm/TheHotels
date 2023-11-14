using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using TheHotels.Models;

namespace TheHotels.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Cart> carts {  get; set; }
        public DbSet<Hotel> hotel { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<RoomDetails> roomDetails { get; set; }
        public DbSet<Rooms> rooms { get; set; }
        public DataView DefaultView { get; internal set; }
    }
}
