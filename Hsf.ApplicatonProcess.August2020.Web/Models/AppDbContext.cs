using Hsf.ApplicatonProcess.August2020.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Web.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Applicant> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Applicant>().HasData(new Applicant
            {
                ID = 1,
                Name = "Alex Borne",
                FamilyName = "",
                Address = "Some kind of address",
                CountryOfOrigin = "Poland",
                EmailAddress = "email@gmail.com",
                Age = 21,
                Hired = true
            });
        }
    }
}
