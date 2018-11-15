using Microsoft.EntityFrameworkCore;
using NightOwl.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NightOwl.WebService.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }

}
