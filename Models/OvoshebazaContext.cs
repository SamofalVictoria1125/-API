using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models
{
    public partial class OvoshebazaContext : DbContext
    {
        public OvoshebazaContext()
        {
        }

        public OvoshebazaContext(DbContextOptions<OvoshebazaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactPerson> ContactPeople { get; set; } = null!;
        public virtual DbSet<Counterparty> Counterparties { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DeliveryComposition> DeliveryCompositions { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<PurchaseComposition> PurchaseCompositions { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<Seller> Sellers { get; set; } = null!;
        public virtual DbSet<Shipment> Shipments { get; set; } = null!;
        public virtual DbSet<ShipmentComposition> ShipmentCompositions { get; set; } = null!;
        public virtual DbSet<Supply> Supplies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=PIPIN;database=Ovoshebaza;user=pipin;password=pipin;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactPerson>(entity =>
            {
                entity.ToTable("ContactPerson");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Counterparty>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdcontactPerson).HasColumnName("IDContactPerson");

                entity.HasOne(d => d.IdcontactPersonNavigation)
                    .WithMany(p => p.Counterparties)
                    .HasForeignKey(d => d.IdcontactPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Counterparties_ContactPerson");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idpartner).HasColumnName("IDPartner");

                entity.HasOne(d => d.IdpartnerNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Idpartner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Counterparties");
            });

            modelBuilder.Entity<DeliveryComposition>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

                entity.Property(e => e.Idsupply).HasColumnName("IDSupply");

                entity.HasOne(d => d.IdproductNavigation)
                    .WithMany(p => p.DeliveryCompositions)
                    .HasForeignKey(d => d.Idproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryCompositions_Products");

                entity.HasOne(d => d.IdsupplyNavigation)
                    .WithMany(p => p.DeliveryCompositions)
                    .HasForeignKey(d => d.Idsupply)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryCompositions_Supply");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdcontactPerson).HasColumnName("IDContactPerson");

                entity.HasOne(d => d.IdcontactPersonNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdcontactPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_ContactPerson");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCalculation).HasColumnType("datetime");

                entity.Property(e => e.Idmanager).HasColumnName("IDManager");

                entity.HasOne(d => d.IdmanagerNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Idmanager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchases_Employee");
            });

            modelBuilder.Entity<PurchaseComposition>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

                entity.Property(e => e.Idpurchase).HasColumnName("IDPurchase");

                entity.HasOne(d => d.IdproductNavigation)
                    .WithMany(p => p.PurchaseCompositions)
                    .HasForeignKey(d => d.Idproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseCompositions_Products");

                entity.HasOne(d => d.IdpurchaseNavigation)
                    .WithMany(p => p.PurchaseCompositions)
                    .HasForeignKey(d => d.Idpurchase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseCompositions_Purchases");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCalculation).HasColumnType("datetime");

                entity.Property(e => e.IdcurrencyCalculation).HasColumnName("IDCurrencyCalculation");

                entity.Property(e => e.Idmanager).HasColumnName("IDManager");

                entity.HasOne(d => d.IdmanagerNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Idmanager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Employee");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idpartner).HasColumnName("IDPartner");

                entity.HasOne(d => d.IdpartnerNavigation)
                    .WithMany(p => p.Sellers)
                    .HasForeignKey(d => d.Idpartner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sellers_Counterparties");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idmanager).HasColumnName("IDManager");

                entity.Property(e => e.ShipmentDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdmanagerNavigation)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.Idmanager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipment_Employee");
            });

            modelBuilder.Entity<ShipmentComposition>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

                entity.Property(e => e.Idshipment).HasColumnName("IDShipment");

                entity.HasOne(d => d.IdproductNavigation)
                    .WithMany(p => p.ShipmentCompositions)
                    .HasForeignKey(d => d.Idproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipmentCompositions_Products");

                entity.HasOne(d => d.IdshipmentNavigation)
                    .WithMany(p => p.ShipmentCompositions)
                    .HasForeignKey(d => d.Idshipment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipmentCompositions_Shipment");
            });

            modelBuilder.Entity<Supply>(entity =>
            {
                entity.ToTable("Supply");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idmanager).HasColumnName("IDManager");

                entity.Property(e => e.SupplyDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdmanagerNavigation)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.Idmanager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supply_Employee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
