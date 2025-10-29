using Microsoft.EntityFrameworkCore;
using ModelSecurity.Models;

namespace ModelSecurity.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // === Tablas en la base de datos ===
        public DbSet<Person> Person { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<RolUser> RolUser { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<FormModule> FormModule { get; set; }
        public DbSet<RolFormPermit> RolFormPermit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Person ↔ User (1:1) ===
            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonId);

            // === Rol ↔ RolUser (1:N) ===
            modelBuilder.Entity<RolUser>()
                .HasOne(ru => ru.Rol)
                .WithMany(r => r.RolUser)
                .HasForeignKey(ru => ru.RolId);

            // === User ↔ RolUser (1:N) ===
            modelBuilder.Entity<RolUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.RolUser)
                .HasForeignKey(ru => ru.UserId);

            // === Module ↔ FormModule (1:N) ===
            modelBuilder.Entity<FormModule>()
                .HasOne(fm => fm.Module)
                .WithMany(m => m.FormModule)
                .HasForeignKey(fm => fm.ModuleId);

            // === Form ↔ FormModule (1:N) ===
            modelBuilder.Entity<FormModule>()
                .HasOne(fm => fm.Form)
                .WithMany(f => f.FormModule)
                .HasForeignKey(fm => fm.FormId);

            // === Rol ↔ RolFormPermit (1:N) ===
            modelBuilder.Entity<RolFormPermit>()
                .HasOne(rfp => rfp.Rol)
                .WithMany(r => r.RolFormPermit)
                .HasForeignKey(rfp => rfp.RolId);

            // === Permission ↔ RolFormPermit (1:N) ===
            modelBuilder.Entity<RolFormPermit>()
                .HasOne(rfp => rfp.Permission)
                .WithMany(p => p.RolFormPermit)
                .HasForeignKey(rfp => rfp.PermissionId);

            // === Form ↔ RolFormPermit (1:N) ===
            modelBuilder.Entity<RolFormPermit>()
                .HasOne(rfp => rfp.Form)
                .WithMany(f => f.RolFormPermit)
                .HasForeignKey(rfp => rfp.FormId);
        }
    }
}
