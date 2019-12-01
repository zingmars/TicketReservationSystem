using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IdentityUser> AspNetUsers { get; set; }
        public virtual DbSet<BusinessHours> BusinessHours { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<PerformanceCategories> PerformanceCategories { get; set; }
        public virtual DbSet<PerformanceDates> PerformanceDates { get; set; }
        public virtual DbSet<Performances> Performances { get; set; }
        public virtual DbSet<PurchaseMethods> PurchaseMethods { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<Theatres> Theatres { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source=database.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();
            });

            modelBuilder.Entity<BusinessHours>(entity =>
            {
                entity.HasIndex(e => e.TheatreId);

                entity.Property(e => e.Begins).IsRequired();

                entity.Property(e => e.Ends).IsRequired();

                entity.Property(e => e.TheatreId).IsRequired();

                entity.HasOne(d => d.Theatre)
                    .WithMany(p => p.BusinessHours)
                    .HasForeignKey(d => d.TheatreId);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("CategoryIndex")
                    .IsUnique();
            });

            modelBuilder.Entity<PerformanceCategories>(entity =>
            {
                entity.HasKey(e => new { e.PerformanceId, e.CategoryId });

                entity.HasIndex(e => e.PerformanceId)
                    .HasName("IX_PerformanceCategories_Performances");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PerformanceCategories)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Performance)
                    .WithMany(p => p.PerformanceCategories)
                    .HasForeignKey(d => d.PerformanceId);
            });

            modelBuilder.Entity<PerformanceDates>(entity =>
            {
                entity.HasIndex(e => e.PerformanceId)
                    .HasName("IX_PerformanceDates_Performances");

                entity.Property(e => e.Date).IsRequired();

                entity.Property(e => e.PerformanceId).IsRequired();

                entity.HasOne(d => d.Performance)
                    .WithMany(p => p.PerformanceDates)
                    .HasForeignKey(d => d.PerformanceId);
            });

            modelBuilder.Entity<Performances>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("PerformanceIndex")
                    .IsUnique();

                entity.Property(e => e.TheatreId).IsRequired();

                entity.HasOne(d => d.Theatre)
                    .WithMany(p => p.Performances)
                    .HasForeignKey(d => d.TheatreId);
            });

            modelBuilder.Entity<PurchaseMethods>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("PurchaseMethodIndex")
                    .IsUnique();
            });

            modelBuilder.Entity<Purchases>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("PurchaseIndex")
                    .IsUnique();

                entity.Property(e => e.PerformanceId).IsRequired();

                entity.Property(e => e.PurchaseMethodId).IsRequired();

                entity.Property(e => e.Purchased).IsRequired();

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.Performance)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.PerformanceId);

                entity.HasOne(d => d.PurchaseMethod)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.PurchaseMethodId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Theatres>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("TheatresIndex")
                    .IsUnique();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
