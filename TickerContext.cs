using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab_11;

public partial class TickerContext : DbContext
{
    public TickerContext()
    {
    }

    public TickerContext(DbContextOptions<TickerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PriceEntry> PriceEntries { get; set; }

    public virtual DbSet<TickerEntry> TickerEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=lab10;User ID=sa;Password=HelloWorld10;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Price__3213E83F77726A14");

            entity.ToTable("PriceEntry");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.TickerId).HasColumnName("ticker_id");

            entity.HasOne(d => d.Ticker).WithMany(p => p.PriceEntries)
                .HasForeignKey(d => d.TickerId)
                .HasConstraintName("FK__Price__ticker_id__276EDEB3");
        });

        modelBuilder.Entity<TickerEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticker__3213E83FC4CD396D");

            entity.ToTable("TickerEntry");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Condition)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('Unknown')")
                .HasColumnName("condition");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
