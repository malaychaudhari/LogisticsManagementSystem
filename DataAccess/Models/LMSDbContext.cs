using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class LMSDbContext : DbContext
{
    public LMSDbContext()
    {
    }

    public LMSDbContext(DbContextOptions<LMSDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<ResourceMapping> ResourceMappings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MALAYKUMARC-GIF\\MSSQLSERVER2019;Database=CybageLogistics;User ID=sa;password=cybage@123456;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC07A402F50A");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductDescription).IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventori__Wareh__276EDEB3");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07E2DF0CD9");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__userId__34C8D9D1");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC0749617BA0");

            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Inventory).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Inven__38996AB5");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__37A5467C");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resource__3214EC07C0524BA4");

            entity.Property(e => e.IsAvailable).HasDefaultValue(true);

            entity.HasOne(d => d.ResourceNavigation).WithMany(p => p.Resources)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resources__Resou__3F466844");
        });

        modelBuilder.Entity<ResourceMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resource__3214EC074F026148");

            entity.ToTable("ResourceMapping");

            entity.HasOne(d => d.Manager).WithMany(p => p.ResourceMappings)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ResourceM__Manag__440B1D61");

            entity.HasOne(d => d.OrderDetails).WithMany(p => p.ResourceMappings)
                .HasForeignKey(d => d.OrderDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ResourceM__Order__4316F928");

            entity.HasOne(d => d.Resource).WithMany(p => p.ResourceMappings)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ResourceM__Resou__4222D4EF");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC079BB16910");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ShipmentDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipment__3214EC07A7FDB53D");

            entity.Property(e => e.Destination)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ExpectedArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.Origin)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.OrderDetails).WithMany(p => p.ShipmentDetails)
                .HasForeignKey(d => d.OrderDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShipmentD__Order__3B75D760");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D32FD459");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105341A64EB43").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__2D27B809");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserDeta__3214EC07D2B292C6");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.VechicleNumber)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.VehicleType)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.WarehouseId).HasDefaultValue(1);

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserDetai__UserI__30F848ED");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.WarehouseId)
                .HasConstraintName("FK__UserDetai__Wareh__31EC6D26");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Warehous__3214EC0710EB4546");

            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
