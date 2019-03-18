using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EastAdvising.Models;


namespace EastAdvising.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Advisor> Advisor { get; set; }
        public DbSet<Appointment> Appointment  { get; set; }
        public DbSet<Availability> Availability  { get; set; }
        public DbSet<College> College { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Major> Major { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Student> Student  { get; set; }



    }
}
