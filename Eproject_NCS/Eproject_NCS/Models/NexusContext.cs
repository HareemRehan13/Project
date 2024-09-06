using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eproject_NCS.Models;

public partial class NexusContext : DbContext
{
    public NexusContext()
    {
    }

    public NexusContext(DbContextOptions<NexusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DiscountScheme> DiscountSchemes { get; set; }

    public virtual DbSet<FeasibilityCheck> FeasibilityChecks { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RetailShop> RetailShops { get; set; }

    public virtual DbSet<ServicePlan> ServicePlans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=Nexus;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.BillingId).HasName("PK__Billing__F1656D139693A70F");

            entity.ToTable("Billing");

            entity.HasIndex(e => e.CustomerId, "idx_Billing_CustomerID");

            entity.Property(e => e.BillingId).HasColumnName("BillingID");
            entity.Property(e => e.AmountDue)
                .HasComputedColumnSql("([TotalAmount]-isnull([AmountPaid],(0)))", false)
                .HasColumnType("decimal(11, 2)");
            entity.Property(e => e.AmountPaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BillDate).HasColumnType("datetime");
            entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Connection).WithMany(p => p.Billings)
                .HasForeignKey(d => d.ConnectionId)
                .HasConstraintName("FK__Billing__Connect__5BE2A6F2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Billings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Billing__Custome__5AEE82B9");
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.ConnectionId).HasName("PK__Connecti__404A64F389F34874");

            entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");
            entity.Property(e => e.ConnectionType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PlanType).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Connections)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Connectio__Custo__571DF1D5");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B81B21C530");

            entity.HasIndex(e => e.AccountId, "UQ__Customer__349DA587ECAA3767").IsUnique();

            entity.HasIndex(e => e.Name, "idx_Customers_Name");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.AccountId)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AddressProof).HasMaxLength(255);
            entity.Property(e => e.ApplicationForm).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Receipt).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<DiscountScheme>(entity =>
        {
            entity.HasKey(e => e.SchemeId).HasName("PK__Discount__DB7E1A42A4D2C880");

            entity.Property(e => e.SchemeId).HasColumnName("SchemeID");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<FeasibilityCheck>(entity =>
        {
            entity.HasKey(e => e.CheckId).HasName("PK__Feasibil__868157063D7B84D6");

            entity.Property(e => e.CheckId).HasColumnName("CheckID");
            entity.Property(e => e.FeasibilityStatus).HasMaxLength(50);
            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OrderID");

            entity.HasOne(d => d.Order).WithMany(p => p.FeasibilityChecks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Feasibili__Order__6C190EBB");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF642BC7651");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OrderID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Feedback__Custom__6477ECF3");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Feedback__OrderI__656C112C");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF5B5E649B");

            entity.HasIndex(e => e.CustomerId, "idx_Orders_CustomerID");

            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OrderID");
            entity.Property(e => e.ConnectionType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.FeasibilityStatus).HasMaxLength(50);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__52593CB8");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK__Orders__Equipmen__534D60F1");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58F4444612");

            entity.HasIndex(e => e.CustomerId, "idx_Payments_CustomerID");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BillingId).HasColumnName("BillingID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Billing).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BillingId)
                .HasConstraintName("FK__Payments__Billin__60A75C0F");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Payments__Custom__5FB337D6");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED056D5420");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductType).HasMaxLength(50);
        });

        modelBuilder.Entity<RetailShop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__RetailSh__67C5562926D576AB");

            entity.Property(e => e.ShopId).HasColumnName("ShopID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ShopName).HasMaxLength(100);
        });

        modelBuilder.Entity<ServicePlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__ServiceP__755C22D7A640D23F");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.PlanType).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC070FEAAA30");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("((2))")
                .HasColumnName("roleId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendors__FC8618D308A1918A");

            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.VendorName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
