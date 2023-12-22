using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using IMS.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IMS.Models
{
    public partial class IMSDBContext : DbContext
    {
        public IMSDBContext()
        {
        }

        public IMSDBContext(DbContextOptions<IMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClassTb> ClassTb { get; set; }
        public virtual DbSet<GstTb> GstTb { get; set; }
        public virtual DbSet<ItemTb> ItemTb { get; set; }
        public virtual DbSet<PurchaseTb> PurchaseTb { get; set; }
        public virtual DbSet<RoleTb> RoleTb { get; set; }
        public virtual DbSet<SalesTb> SalesTb { get; set; }
        public virtual DbSet<StateTb> StateTb { get; set; }
        public virtual DbSet<SupplierTb> SupplierTb { get; set; }
        public virtual DbSet<TransactionTb> TransactionTb { get; set; }
        public virtual DbSet<UserTb> UserTb { get; set; }

        public virtual DbSet<stokes> Stokes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.EnableSensitiveDataLogging();
                ConfigurationBuilder confb = new ConfigurationBuilder();
                optionsBuilder.UseSqlServer(confb.Build().GetSection("DBConnectionStrings").Value);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassTb>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("classTB");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.Cgst)
                    .HasColumnName("CGST")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasColumnName("class_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClassStatus)
                    .HasColumnName("class_status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GstId).HasColumnName("gst_id");

                entity.Property(e => e.Igst)
                    .HasColumnName("IGST")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Sgst)
                    .HasColumnName("SGST")
                    .HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Gst)
                    .WithMany(p => p.ClassTb)
                    .HasForeignKey(d => d.GstId)
                    .HasConstraintName("FK_classTB_gstTB");
            });

            modelBuilder.Entity<GstTb>(entity =>
            {
                entity.HasKey(e => e.GstId);

                entity.ToTable("gstTB");

                entity.Property(e => e.GstId).HasColumnName("gst_id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Cgst)
                    .HasColumnName("CGST")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GstName)
                    .IsRequired()
                    .HasColumnName("gst_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Igst)
                    .HasColumnName("IGST")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Sgst)
                    .HasColumnName("SGST")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<ItemTb>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("itemTB");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ItemClassId).HasColumnName("item_class_id");

                entity.Property(e => e.ItemHsn)
                    .HasColumnName("item_HSN")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasColumnName("item_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemPurchaseRate).HasColumnName("item_purchase_rate");

                entity.Property(e => e.ItemSalesRate).HasColumnName("item_sales_rate");

                entity.Property(e => e.ItemStatus)
                    .HasColumnName("item_status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ItemClass)
                    .WithMany(p => p.ItemTb)
                    .HasForeignKey(d => d.ItemClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_classTB_itemTB");
            });

            modelBuilder.Entity<PurchaseTb>(entity =>
            {
                entity.HasKey(e => e.PurId);

                entity.ToTable("purchaseTB");

                entity.Property(e => e.PurId).HasColumnName("pur_id");

                entity.Property(e => e.CgstAmount).HasColumnName("CGST_amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.GstId).HasColumnName("gst_id");

                entity.Property(e => e.IgstAmount).HasColumnName("IGST_amount");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.PerDate)
                    .HasColumnName("per_date")
                    .HasColumnType("date");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SgstAmount).HasColumnName("SGST_amount");

                entity.Property(e => e.SupId).HasColumnName("sup_id");

                entity.Property(e => e.Total1).HasColumnName("total1");

                entity.Property(e => e.Total2).HasColumnName("total2");

                entity.Property(e => e.Total3).HasColumnName("total3");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Gst)
                    .WithMany(p => p.PurchaseTb)
                    .HasForeignKey(d => d.GstId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_purchaseTB_gstTB");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.PurchaseTb)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_purchaseTB_itemTB");

                entity.HasOne(d => d.Sup)
                    .WithMany(p => p.PurchaseTb)
                    .HasForeignKey(d => d.SupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_purchaseTB_supplierTB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PurchaseTb)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_purchaseTB_userTB");
            });

            modelBuilder.Entity<RoleTb>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("roleTB");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PermissionCreateUpdate).HasColumnName("permission_create_update");

                entity.Property(e => e.PermissionDelete).HasColumnName("permission_delete");

                entity.Property(e => e.PermissionView).HasColumnName("permission_view");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleStatus).HasColumnName("role_status");
            });

            modelBuilder.Entity<SalesTb>(entity =>
            {
                entity.HasKey(e => e.SalesId);

                entity.ToTable("salesTB");

                entity.Property(e => e.SalesId).HasColumnName("sales_id");

                entity.Property(e => e.CgstAmount).HasColumnName("CGST_amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date");

                entity.Property(e => e.CustomerAddress)
                    .IsRequired()
                    .HasColumnName("customer_address")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerMobile)
                    .HasColumnName("customer_mobile")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customer_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerType)
                    .IsRequired()
                    .HasColumnName("customer_type")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.GstId).HasColumnName("gst_id");

                entity.Property(e => e.IgstAmount).HasColumnName("IGST_amount");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SalesDate)
                    .HasColumnName("sales_date")
                    .HasColumnType("date");

                entity.Property(e => e.SalesTyep)
                    .IsRequired()
                    .HasColumnName("sales_tyep")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.SgstAmount).HasColumnName("SGST_amount");

                entity.Property(e => e.Total1).HasColumnName("total1");

                entity.Property(e => e.Total2).HasColumnName("total2");

                entity.Property(e => e.Total3).HasColumnName("total3");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Gst)
                    .WithMany(p => p.SalesTb)
                    .HasForeignKey(d => d.GstId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_salesTB_gstTB");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.SalesTb)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_salesTB_itemTB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesTb)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_salesTB_userTB");
            });

            modelBuilder.Entity<StateTb>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.ToTable("stateTB");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasColumnName("state_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierTb>(entity =>
            {
                entity.HasKey(e => e.SupId);

                entity.ToTable("supplierTB");

                entity.Property(e => e.SupId).HasColumnName("sup_id");

                entity.Property(e => e.SupAddress)
                    .IsRequired()
                    .HasColumnName("sup_address")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.SupGstNumber)
                    .HasColumnName("sup_gst_number")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SupMobile)
                    .HasColumnName("sup_mobile")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.SupName)
                    .IsRequired()
                    .HasColumnName("sup_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupType)
                    .IsRequired()
                    .HasColumnName("sup_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransactionTb>(entity =>
            {
                entity.HasKey(e => e.TrnId);

                entity.ToTable("transactionTB");

                entity.Property(e => e.TrnId).HasColumnName("trn_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Remark).HasColumnName("remark");

                entity.Property(e => e.TrnTrnType)
                    .HasColumnName("trn_trn_type")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.TrnType)
                    .HasColumnName("trn_type")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserTb>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("userTB");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserTb)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_roleTB_userTB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<IMS.Models.stokes> stokes { get; set; }
    }
}
