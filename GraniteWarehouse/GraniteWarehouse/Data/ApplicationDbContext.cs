using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GraniteWarehouse.Models;

namespace GraniteWarehouse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductTypes> ProductTypes { get; set; }  
        public DbSet<SpecialTags> SpecialTags { get; set; }  
    }
}
