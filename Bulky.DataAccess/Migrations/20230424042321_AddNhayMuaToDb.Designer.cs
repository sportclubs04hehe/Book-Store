﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebBanSach.Data;

#nullable disable

namespace Bulky.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230424042321_AddNhayMuaToDb")]
    partial class AddNhayMuaToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bukly.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Bukly.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IBSN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Jone Wick",
                            Description = "Combo Sách Đầu Tư Bất Động Sản: Bất Động Sản Căn Bản + Donald Trump - Chiến Lược Đầu Tư Bất Động Sản (Tái Bản) + Trump - 100 Lời Khuyên Đầu Tư Bất Động Sản Khôn Ngoan Nhất + Đầu Tư Bất Động Sản - Cách Thức Khởi Nghiệp Và Thu Lợi Nhuận Lớn",
                            IBSN = "W874UI",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Book 1"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Daniel Goleman",
                            Description = "Everyone knows that a high IQ score does not guarantee a successful, happy and virtuous life",
                            IBSN = "JT87D02L",
                            ListPrice = 200.0,
                            Price = 170.0,
                            Price100 = 80.0,
                            Price50 = 150.0,
                            Title = "Emotional Intelligence (Reissue 2021)"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
