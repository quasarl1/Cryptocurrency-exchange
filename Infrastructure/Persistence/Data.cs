using CryptoExchange.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace CryptoExchange.Data
{
    public class DataBase : DbContext
    {
        public DataBase(DbContextOptions<DataBase> options) : base(options){}
        public DbSet<User> users {get;set;}
        public DbSet<Account> accounts {get;set;}
    }
}