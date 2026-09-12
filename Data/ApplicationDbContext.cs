using Microsoft.EntityFrameworkCore;
using SouvenirShop.Models;

namespace SouvenirShop.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){

    }
    public DbSet<Souvenir> Souvenirs {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<Order> Orders {get; set;}
    public DbSet<Review> Reviews {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
    }
}