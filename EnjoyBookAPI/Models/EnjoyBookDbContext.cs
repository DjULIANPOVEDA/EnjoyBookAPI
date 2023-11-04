using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EnjoyBookAPI.Models;

public partial class EnjoyBookDbContext : DbContext
{
    public EnjoyBookDbContext()
    {
    }

    public EnjoyBookDbContext(DbContextOptions<EnjoyBookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Libro> Libros { get; set; }
    public virtual DbSet<Renta> Rentas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-cl37p8ot3kic73d8ls5g-a.oregon-postgres.render.com;Database=enjoy_book_db;Username=admin;Password=QkoIBXDpQXBVbkkw4FlpyAx3eJQVcFcg");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Libro>()
            .Property(e => e.Estado)
            .HasConversion<string>()
            .HasColumnName("estado")
            .HasColumnType("enum(Estados)");
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
