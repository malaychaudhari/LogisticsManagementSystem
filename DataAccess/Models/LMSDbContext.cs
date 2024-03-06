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
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC07CCF4DAF9");

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
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0744DCF5E3");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__userId__35BCFE0A");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC07FFE4B025");

            entity.ToTable(tb => tb.HasTrigger("UpdateProductQuantity"));

            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Inventory).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Inven__398D8EEE");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__38996AB5");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resource__3214EC07FF497A03");

            entity.Property(e => e.IsAvailable).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.Resources)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resources__Resou__403A8C7D");
        });

        modelBuilder.Entity<ResourceMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resource__3214EC07EBE76BC7");

            entity.ToTable("ResourceMapping");

            entity.HasOne(d => d.Manager).WithMany(p => p.ResourceMappings)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ResourceM__Manag__44FF419A");

            entity.HasOne(d => d.OrderDetails).WithMany(p => p.ResourceMappings)
                .HasForeignKey(d => d.OrderDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ResourceM__Order__440B1D61");

            entity.HasOne(d => d.Resource).WithMany(p => p.ResourceMappings)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ResourceM__Resou__4316F928");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07E8D356B3");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ShipmentDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipment__3214EC07DC5B5435");

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
                .HasConstraintName("FK__ShipmentD__Order__3C69FB99");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07BE74D80E");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105347ED0DB09").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__UserDeta__3214EC07143C4850");

            entity.Property(e => e.IsApproved).HasDefaultValue(0);
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.VehicleType)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.WarehouseId).HasDefaultValue(1);

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserDetai__UserI__31EC6D26");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.WarehouseId)
                .HasConstraintName("FK__UserDetai__Wareh__32E0915F");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Warehous__3214EC07FD62431E");

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
