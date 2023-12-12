using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HelpArmyDB
{
    class ConnectEF : DbContext
    {
        public DbSet<Organization> organization { get; set; }

        public DbSet<Volunteer> volunteer { get; set; }

        public DbSet<Donate> donation { get; set; }

        public DbSet<Report> report { get; set; }

        public string DbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Host=localhost;Port=5432;Database=helparmy;Username=postgres;Password=001001001b;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.id_organization);
                entity.Property(e => e.name_organization).IsRequired();
                entity.Property(e => e.city_organization).IsRequired();
                entity.Property(e => e.email_organization).IsRequired();
                entity.Property(e => e.password_organization).IsRequired();
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasKey(e => e.id_volunteer);
                entity.Property(e => e.name_volunteer).IsRequired();
                entity.Property(e => e.surname_volunteer).IsRequired();
                entity.Property(e => e.city_volunteer).IsRequired();
                entity.Property(e => e.email_volunteer).IsRequired();
                entity.Property(e => e.password_volunteer).IsRequired();
            });

            modelBuilder.Entity<Donate>(entity =>
            {
                entity.HasKey(e => e.id_donation);
                entity.Property(e => e.category_donation).IsRequired();
                entity.Property(e => e.name_donation).IsRequired();
                entity.Property(e => e.type_donation).IsRequired();
                entity.Property(e => e.sum_donation).IsRequired();
                entity.Property(e => e.object_donation).IsRequired();
                entity.Property(e => e.number_donation).IsRequired();
                entity.Property(e => e.date_donation).IsRequired();
                entity.Property(e => e.photo_donation).IsRequired();
             
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.id_report);
                entity.Property(e => e.type_donation_report).IsRequired();
                entity.Property(e => e.date_start_report).IsRequired();
                entity.Property(e => e.date_end_report).IsRequired();
                entity.HasOne<Organization>()
                    .WithMany()
                    .HasForeignKey(e => e.id_organization)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
