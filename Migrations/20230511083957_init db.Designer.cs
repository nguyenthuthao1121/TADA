﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TADA.Model;

#nullable disable

namespace TADA.Migrations
{
    [DbContext(typeof(TadaContext))]
    [Migration("20230511083957_init db")]
    partial class initdb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TADA.Model.Entity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TADA.Model.Entity.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int>("WardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WardId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("TADA.Model.Entity.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTrade")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("TADA.Model.Entity.BillDetail", b =>
                {
                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BillId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BillDetail");
                });

            modelBuilder.Entity("TADA.Model.Entity.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Cover")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("ntext");

                    b.Property<string>("Genre")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int>("Pages")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("Promotion")
                        .HasColumnType("int");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("int");

                    b.Property<string>("Publisher")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProviderId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("TADA.Model.Entity.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("TADA.Model.Entity.CartDetail", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("CartDetail");
                });

            modelBuilder.Entity("TADA.Model.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TADA.Model.Entity.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("DateTime");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AddressId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TADA.Model.Entity.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("TADA.Model.Entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOrder")
                        .HasColumnType("datetime");

                    b.Property<decimal>("ShipFee")
                        .HasColumnType("money");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("char");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TADA.Model.Entity.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("TADA.Model.Entity.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("TADA.Model.Entity.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("TADA.Model.Entity.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<DateTime>("DateReview")
                        .HasColumnType("datetime");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("OrderId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("TADA.Model.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TADA.Model.Entity.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("TelephoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("char");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AddressId");

                    b.HasIndex("RoleId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("TADA.Model.Entity.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("TADA.Model.Entity.Ward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Wards");
                });

            modelBuilder.Entity("TADA.Model.Entity.Address", b =>
                {
                    b.HasOne("TADA.Model.Entity.Ward", "Ward")
                        .WithMany("Addresses")
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("TADA.Model.Entity.Bill", b =>
                {
                    b.HasOne("TADA.Model.Entity.Provider", "Provider")
                        .WithMany("Bills")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("TADA.Model.Entity.BillDetail", b =>
                {
                    b.HasOne("TADA.Model.Entity.Bill", "Bill")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TADA.Model.Entity.Book", "Book")
                        .WithMany("BillDetails")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("TADA.Model.Entity.Book", b =>
                {
                    b.HasOne("TADA.Model.Entity.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TADA.Model.Entity.Provider", "Provider")
                        .WithMany("Books")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("TADA.Model.Entity.Cart", b =>
                {
                    b.HasOne("TADA.Model.Entity.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TADA.Model.Entity.CartDetail", b =>
                {
                    b.HasOne("TADA.Model.Entity.Book", "Book")
                        .WithMany("CartDetails")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TADA.Model.Entity.Cart", "Cart")
                        .WithMany("CartDetails")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("TADA.Model.Entity.Customer", b =>
                {
                    b.HasOne("TADA.Model.Entity.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TADA.Model.Entity.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("TADA.Model.Entity.District", b =>
                {
                    b.HasOne("TADA.Model.Entity.Province", "Province")
                        .WithMany("Districts")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("TADA.Model.Entity.Order", b =>
                {
                    b.HasOne("TADA.Model.Entity.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TADA.Model.Entity.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TADA.Model.Entity.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Address");

                    b.Navigation("Customer");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("TADA.Model.Entity.OrderDetail", b =>
                {
                    b.HasOne("TADA.Model.Entity.Book", "Book")
                        .WithMany("OrderDetails")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TADA.Model.Entity.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TADA.Model.Entity.Provider", b =>
                {
                    b.HasOne("TADA.Model.Entity.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("TADA.Model.Entity.Review", b =>
                {
                    b.HasOne("TADA.Model.Entity.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TADA.Model.Entity.Order", "Order")
                        .WithMany("Reviews")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TADA.Model.Entity.Staff", b =>
                {
                    b.HasOne("TADA.Model.Entity.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TADA.Model.Entity.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TADA.Model.Entity.Role", "Role")
                        .WithMany("Staff")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Account");

                    b.Navigation("Address");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TADA.Model.Entity.Ward", b =>
                {
                    b.HasOne("TADA.Model.Entity.District", "District")
                        .WithMany("Wards")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("TADA.Model.Entity.Bill", b =>
                {
                    b.Navigation("BillDetails");
                });

            modelBuilder.Entity("TADA.Model.Entity.Book", b =>
                {
                    b.Navigation("BillDetails");

                    b.Navigation("CartDetails");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("TADA.Model.Entity.Cart", b =>
                {
                    b.Navigation("CartDetails");
                });

            modelBuilder.Entity("TADA.Model.Entity.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("TADA.Model.Entity.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("TADA.Model.Entity.District", b =>
                {
                    b.Navigation("Wards");
                });

            modelBuilder.Entity("TADA.Model.Entity.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("TADA.Model.Entity.Provider", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Books");
                });

            modelBuilder.Entity("TADA.Model.Entity.Province", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("TADA.Model.Entity.Role", b =>
                {
                    b.Navigation("Staff");
                });

            modelBuilder.Entity("TADA.Model.Entity.Status", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("TADA.Model.Entity.Ward", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
