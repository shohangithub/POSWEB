﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POSWEB.Server.Context;

#nullable disable

namespace POSWEB.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("POSWEB.Server.Entitites.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime");

                    b.Property<string>("CustomBarcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinishedGoods")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRawMaterial")
                        .HasColumnType("bit");

                    b.Property<int?>("LastUpdatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<short>("ProductCategoryId")
                        .HasColumnType("smallint");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("ProductUnitId")
                        .HasColumnType("smallint");

                    b.Property<decimal?>("PurchaseRate")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("ReOrederLevel")
                        .HasColumnType("int");

                    b.Property<decimal?>("SellingRate")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("VatPercent")
                        .HasColumnType("decimal(3, 2)");

                    b.Property<decimal?>("WholesalePrice")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LastUpdatedById");

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("ProductUnitId");

                    b.ToTable("Products", "product");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.ProductCategory", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("LastUpdatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LastUpdatedById");

                    b.ToTable("ProductCategories", "product");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.ProductUnit", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("LastUpdatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LastUpdatedById");

                    b.ToTable("ProductUnits", "lookup");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", "user");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.Product", b =>
                {
                    b.HasOne("POSWEB.Server.Entitites.User", "CreatedBy")
                        .WithMany("ProductsCreated")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("POSWEB.Server.Entitites.User", "LastUpdatedBy")
                        .WithMany("ProductsUpdated")
                        .HasForeignKey("LastUpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("POSWEB.Server.Entitites.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POSWEB.Server.Entitites.ProductUnit", "ProductUnit")
                        .WithMany("Products")
                        .HasForeignKey("ProductUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("LastUpdatedBy");

                    b.Navigation("ProductCategory");

                    b.Navigation("ProductUnit");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.ProductCategory", b =>
                {
                    b.HasOne("POSWEB.Server.Entitites.User", "CreatedBy")
                        .WithMany("ProductCategoriesCreated")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("POSWEB.Server.Entitites.User", "LastUpdatedBy")
                        .WithMany("ProductCategoriesUpdated")
                        .HasForeignKey("LastUpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedBy");

                    b.Navigation("LastUpdatedBy");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.ProductUnit", b =>
                {
                    b.HasOne("POSWEB.Server.Entitites.User", "CreatedBy")
                        .WithMany("ProductUnitsCreated")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("POSWEB.Server.Entitites.User", "LastUpdatedBy")
                        .WithMany("ProductUnitsUpdated")
                        .HasForeignKey("LastUpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedBy");

                    b.Navigation("LastUpdatedBy");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.ProductUnit", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("POSWEB.Server.Entitites.User", b =>
                {
                    b.Navigation("ProductCategoriesCreated");

                    b.Navigation("ProductCategoriesUpdated");

                    b.Navigation("ProductUnitsCreated");

                    b.Navigation("ProductUnitsUpdated");

                    b.Navigation("ProductsCreated");

                    b.Navigation("ProductsUpdated");
                });
#pragma warning restore 612, 618
        }
    }
}
