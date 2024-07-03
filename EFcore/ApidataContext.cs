using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KKHS_API.EFcore;

public partial class ApidataContext : DbContext
{
    public ApidataContext()
    {
    }

    public ApidataContext(DbContextOptions<ApidataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GoodsTitleList> GoodsTitleLists { get; set; }

    public virtual DbSet<NewProduct> NewProducts { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=apidata;User=sa;Password=!Zxc1010179;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GoodsTitleList>(entity =>
        {
            entity.ToTable("GoodsTitleList");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Part).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<NewProduct>(entity =>
        {
            entity.ToTable("NewProduct");

            entity.Property(e => e.Color).HasMaxLength(10);
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HotPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MarketPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(10);
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.ToTable("OrderInfo");

            entity.Property(e => e.Color).HasMaxLength(10);
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail).HasMaxLength(50);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.CustomerPhone).HasMaxLength(50);
            entity.Property(e => e.DeliveryName).HasMaxLength(50);
            entity.Property(e => e.DeliveryPhone).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(10);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Color).HasMaxLength(10);
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HotPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MarketPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Part).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(10);
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ShippingCart");

            entity.ToTable("ShoppingCart");

            entity.Property(e => e.Color).HasMaxLength(10);
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(10);
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserInfo");

            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");
            entity.Property(e => e.UserName).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
