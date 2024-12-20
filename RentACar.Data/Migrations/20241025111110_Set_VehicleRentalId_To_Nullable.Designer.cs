﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentACar.Data;

#nullable disable

namespace RentACar.Data.Migrations
{
    [DbContext(typeof(RentACarDbContext))]
    [Migration("20241025111110_Set_VehicleRentalId_To_Nullable")]
    partial class Set_VehicleRentalId_To_Nullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RentACar.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("RentACar.Data.Models.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the branch.");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("The address of the branch.");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(85)
                        .HasColumnType("nvarchar(85)")
                        .HasComment("The city of the branch.");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("The country of the branch.");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("The name of the branch.");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("RentACar.Data.Models.Engine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CylindersCount")
                        .HasColumnType("int")
                        .HasComment("The cylinders count of the engine.");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Optional description of the engine.");

                    b.Property<decimal>("Displacement")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("The measurement of the total volume of all of an engine's cylinders.");

                    b.Property<decimal>("FuelEfficiency")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("How much the engine burns per 100km.");

                    b.Property<int>("FuelType")
                        .HasColumnType("int")
                        .HasComment("The type of fuel that the engine uses.");

                    b.Property<int>("HP")
                        .HasColumnType("int")
                        .HasComment("The horse power of the engine.");

                    b.Property<bool>("IsElectric")
                        .HasColumnType("bit")
                        .HasComment("Whether the engine is electric or not.");

                    b.Property<int?>("Torque")
                        .HasColumnType("int")
                        .HasComment("The torque of the car in Nm.");

                    b.HasKey("Id");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("RentACar.Data.Models.Make", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the make.");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Country of the make.");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Name of the make.");

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("RentACar.Data.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the payment.");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("The amount of the payment.");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2")
                        .HasComment("The date of payment.");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int")
                        .HasComment("The method of the payment.");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasComment("The status of the payment.");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("RentACar.Data.Models.Rental", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the rental.");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the customer.");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasComment("The rental ending date.");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the payment.");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasComment("The rental starting date.");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total price of the rental.");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("RentACar.Data.Models.Transmission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the transmission.");

                    b.Property<int>("GearsCount")
                        .HasColumnType("int")
                        .HasComment("The count of gears for the transmission.");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasComment("The type of the transmission(manual or automatic).");

                    b.HasKey("Id");

                    b.ToTable("Transmissions");
                });

            modelBuilder.Entity("RentACar.Data.Models.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the vehicle.");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("datetime2")
                        .HasComment("The date of addition of the car.");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("The place from which the customer can take the car.");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Color of the car.");

                    b.Property<int>("DoorsCount")
                        .HasColumnType("int")
                        .HasComment("The number of doors of the car.");

                    b.Property<Guid>("EngineId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("The Id of the engine.");

                    b.Property<string>("ImageUrl")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)")
                        .HasDefaultValue("/img/no-image.jpg")
                        .HasComment("Url of the image of the car.");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("MakeId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("The Id of the vehicle make.");

                    b.Property<int>("Mileage")
                        .HasColumnType("int")
                        .HasComment("Mileage of the car.");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("The model of the car.");

                    b.Property<decimal>("PricePerDay")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("The price of the vehicle per day.");

                    b.Property<string>("RegistrationNumber")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasComment("The registration number of the car.");

                    b.Property<Guid?>("RentalId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id of the current rental for this car.");

                    b.Property<int>("SeatsCount")
                        .HasColumnType("int")
                        .HasComment("The number of seats of the car.");

                    b.Property<Guid>("TransmissionId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("The Id of the transmission of the car.");

                    b.Property<string>("VINNumber")
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)")
                        .HasComment("The VIN number of the car.");

                    b.Property<Guid>("VehicleTypeId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("The Id of the type of the vehicle(SUV,Coupe,HatchBack...).");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2")
                        .HasComment("Year of manufacturing.");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("EngineId");

                    b.HasIndex("MakeId");

                    b.HasIndex("RentalId");

                    b.HasIndex("TransmissionId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("RentACar.Data.Models.VehicleType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Identifier of the vehicle type.");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Description of the vehicle type.");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Name of the vehicle type.");

                    b.HasKey("Id");

                    b.ToTable("VehicleTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("RentACar.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("RentACar.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentACar.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("RentACar.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentACar.Data.Models.Rental", b =>
                {
                    b.HasOne("RentACar.Data.Models.ApplicationUser", "Customer")
                        .WithMany("Rentals")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentACar.Data.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("RentACar.Data.Models.Vehicle", b =>
                {
                    b.HasOne("RentACar.Data.Models.Branch", "Branch")
                        .WithMany("Vehicles")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentACar.Data.Models.Engine", "Engine")
                        .WithMany()
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentACar.Data.Models.Make", "Make")
                        .WithMany("Vehicles")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentACar.Data.Models.Rental", "Rental")
                        .WithMany()
                        .HasForeignKey("RentalId");

                    b.HasOne("RentACar.Data.Models.Transmission", "Transmission")
                        .WithMany()
                        .HasForeignKey("TransmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentACar.Data.Models.VehicleType", "VehicleType")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Engine");

                    b.Navigation("Make");

                    b.Navigation("Rental");

                    b.Navigation("Transmission");

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("RentACar.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("RentACar.Data.Models.Branch", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("RentACar.Data.Models.Make", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("RentACar.Data.Models.VehicleType", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
