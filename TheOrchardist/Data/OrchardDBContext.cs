using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrchardist.Data;

namespace TheOrchardist.Data
{
    public class OrchardDBContext : DbContext
    {
      public OrchardDBContext(DbContextOptions<OrchardDBContext> options) : base(options)
      {

      }
    public DbSet<GlobalPlantList> GlobalPlantLists { get; set; }
    public DbSet<Orchard> Orchards { get; set; }
      public DbSet<UserPlantList> UserPlantLists { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {

      modelBuilder.Entity<Orchard>().ToTable("Orchard");

      modelBuilder.Entity<UserPlantList>().ToTable("UserPlantList");

      modelBuilder.Entity<GlobalPlantList>().ToTable("GlobalPlantList");

      modelBuilder.Entity<UserPlantList>().HasKey(k => new { k.OrchardID });
      base.OnModelCreating(modelBuilder);
    }
   

  }
}
