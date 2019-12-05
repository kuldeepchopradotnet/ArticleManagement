using System;
using AM.Data.Model;
using AM.Identity;
#region identity and entity framework core packages , install from nuget
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
#endregion

namespace AM.Api.Model
{
    //*** IdentityDbContext for asp.net core identiy 
    public partial class ArticleManagementContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, int>
    {
        public ArticleManagementContext(DbContextOptions<ArticleManagementContext> options)
        : base(options)
        {
        }

        //*** Database to Code, POCO
        //*** PM> Scaffold-DbContext "Server=Servername; Database=dbname; Integreated_Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


        //*** Code to Database
        //*** Add-Migration "{MigarationName}"
        //*** Upddate-Database

        //*** There is already an object named 'AspNetRoles' in the database
        //*** Add-Migration InitialMigrations -IgnoreChanges
        //*** update-database -verbose


        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<ArticleE> Article { get; set; }

        //public virtual DbSet<AspNetRoleClaims> AspNetRoleClaim { get; set; }
        //public virtual DbSet<AspNetRoles> AspNetRole { get; set; }
        //public virtual DbSet<AspNetUserClaims> AspNetUserClaim { get; set; }
        //public virtual DbSet<AspNetUserLogins> AspNetUserLogin { get; set; }
        //public virtual DbSet<AspNetUserRoles> AspNetUserRole { get; set; }
        //public virtual DbSet<AspNetUserTokens> AspNetUserToken { get; set; }
        //public virtual DbSet<AspNetUsers> AspNetUser { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            //modelBuilder.Entity<Category>(enitity => {
            //    enitity.HasIndex(e => e.Id);
            //    enitity.Property(e => e.Icon).HasMaxLength(100);
            //    enitity.Property(e => e.Id).ValueGeneratedNever();
            //    enitity.Property(e => e.Name).HasMaxLength(100);
            //});

            //modelBuilder.Entity<AspNetRoleClaims>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId);

            //    entity.Property(e => e.RoleId).IsRequired();

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetRoles>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName)
            //        .HasName("RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetUserClaims>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogins>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasIndex(e => e.UserId);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserRoles>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.RoleId });

            //    entity.HasIndex(e => e.RoleId);

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.RoleId);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserTokens>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUsers>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail)
            //        .HasName("EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName)
            //        .HasName("UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);
            //});
        }
    }
}
