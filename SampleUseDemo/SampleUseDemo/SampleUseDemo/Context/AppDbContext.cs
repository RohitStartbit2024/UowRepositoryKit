using Microsoft.EntityFrameworkCore;
using SampleUseDemo.Models;
using System.Collections.Generic;

namespace SampleUseDemo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
