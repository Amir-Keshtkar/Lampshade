﻿// <auto-generated />
using System;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountManagement.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20240202170240_AddSeedForAccount")]
    partial class AddSeedForAccount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccountManagement.Domain.AccountAgg.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ProfilePhoto")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreationDate = new DateTime(2024, 2, 2, 20, 32, 39, 809, DateTimeKind.Local).AddTicks(9561),
                            FullName = "بهداد بهرام آبادیان",
                            Mobile = "09396387926",
                            Password = "10000.DoLZEtw/g/OOPTMgKr08Yw==.Po/Jt+K22MbD9jRJfcSZw44N2UVeI2Cp8KoA4YYsAJ0=",
                            ProfilePhoto = "",
                            RoleId = 1L,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2L,
                            CreationDate = new DateTime(2024, 2, 2, 20, 32, 39, 809, DateTimeKind.Local).AddTicks(9576),
                            FullName = "کاربر تست",
                            Mobile = "09962999643",
                            Password = "10000.Hpa1e3Zxvwhs7+8BnEms3Q==.vVHpopaMrhSNE4TTigs7rmR9/dxJ+MXC9kkY/yaJj3U=",
                            ProfilePhoto = "",
                            RoleId = 2L,
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("AccountManagement.Domain.RoleAgg.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreationDate = new DateTime(2024, 2, 2, 20, 32, 39, 810, DateTimeKind.Local).AddTicks(7105),
                            Name = "مدیر سیستم"
                        },
                        new
                        {
                            Id = 2L,
                            CreationDate = new DateTime(2024, 2, 2, 20, 32, 39, 810, DateTimeKind.Local).AddTicks(7108),
                            Name = "کاربر سیستم"
                        });
                });

            modelBuilder.Entity("AccountManagement.Domain.AccountAgg.Account", b =>
                {
                    b.HasOne("AccountManagement.Domain.RoleAgg.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AccountManagement.Domain.RoleAgg.Role", b =>
                {
                    b.OwnsMany("AccountManagement.Domain.RoleAgg.Permission", "Permissions", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<int>("Code")
                                .HasMaxLength(50)
                                .HasColumnType("int");

                            b1.Property<long>("RoleId")
                                .HasColumnType("bigint");

                            b1.HasKey("Id");

                            b1.HasIndex("RoleId");

                            b1.ToTable("RolePermissions", (string)null);

                            b1.WithOwner("Role")
                                .HasForeignKey("RoleId");

                            b1.Navigation("Role");

                            b1.HasData(
                                new
                                {
                                    Id = 1L,
                                    Code = 10,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 2L,
                                    Code = 11,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 3L,
                                    Code = 12,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 4L,
                                    Code = 13,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 5L,
                                    Code = 14,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 6L,
                                    Code = 15,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 7L,
                                    Code = 16,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 8L,
                                    Code = 17,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 9L,
                                    Code = 18,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 10L,
                                    Code = 19,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 11L,
                                    Code = 20,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 12L,
                                    Code = 21,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 13L,
                                    Code = 22,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 14L,
                                    Code = 23,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 15L,
                                    Code = 24,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 16L,
                                    Code = 25,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 17L,
                                    Code = 26,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 18L,
                                    Code = 27,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 19L,
                                    Code = 28,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 20L,
                                    Code = 29,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 21L,
                                    Code = 30,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 22L,
                                    Code = 31,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 23L,
                                    Code = 32,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 24L,
                                    Code = 33,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 25L,
                                    Code = 34,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 26L,
                                    Code = 35,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 27L,
                                    Code = 36,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 28L,
                                    Code = 37,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 29L,
                                    Code = 38,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 30L,
                                    Code = 39,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 31L,
                                    Code = 40,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 32L,
                                    Code = 41,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 33L,
                                    Code = 42,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 34L,
                                    Code = 43,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 35L,
                                    Code = 44,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 36L,
                                    Code = 45,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 37L,
                                    Code = 46,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 38L,
                                    Code = 47,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 39L,
                                    Code = 48,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 40L,
                                    Code = 49,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 41L,
                                    Code = 50,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 42L,
                                    Code = 51,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 43L,
                                    Code = 52,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 44L,
                                    Code = 53,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 45L,
                                    Code = 54,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 46L,
                                    Code = 55,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 47L,
                                    Code = 56,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 48L,
                                    Code = 57,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 49L,
                                    Code = 58,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 50L,
                                    Code = 59,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 51L,
                                    Code = 60,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 52L,
                                    Code = 61,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 53L,
                                    Code = 62,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 54L,
                                    Code = 63,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 55L,
                                    Code = 64,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 56L,
                                    Code = 65,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 57L,
                                    Code = 66,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 58L,
                                    Code = 67,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 59L,
                                    Code = 68,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 60L,
                                    Code = 69,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 61L,
                                    Code = 70,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 62L,
                                    Code = 71,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 63L,
                                    Code = 72,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 64L,
                                    Code = 73,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 65L,
                                    Code = 74,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 66L,
                                    Code = 75,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 67L,
                                    Code = 76,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 68L,
                                    Code = 77,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 69L,
                                    Code = 78,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 70L,
                                    Code = 79,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 71L,
                                    Code = 80,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 72L,
                                    Code = 81,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 73L,
                                    Code = 82,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 74L,
                                    Code = 83,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 75L,
                                    Code = 84,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 76L,
                                    Code = 85,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 77L,
                                    Code = 86,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 78L,
                                    Code = 87,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 79L,
                                    Code = 88,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 80L,
                                    Code = 89,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 81L,
                                    Code = 90,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 82L,
                                    Code = 91,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 83L,
                                    Code = 92,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 84L,
                                    Code = 93,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 85L,
                                    Code = 94,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 86L,
                                    Code = 95,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 87L,
                                    Code = 96,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 88L,
                                    Code = 97,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 89L,
                                    Code = 98,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 90L,
                                    Code = 99,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 91L,
                                    Code = 100,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 92L,
                                    Code = 101,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 93L,
                                    Code = 102,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 94L,
                                    Code = 103,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 95L,
                                    Code = 104,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 96L,
                                    Code = 105,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 97L,
                                    Code = 106,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 98L,
                                    Code = 107,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 99L,
                                    Code = 108,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 100L,
                                    Code = 109,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 101L,
                                    Code = 110,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 102L,
                                    Code = 111,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 103L,
                                    Code = 112,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 104L,
                                    Code = 113,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 105L,
                                    Code = 114,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 106L,
                                    Code = 115,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 107L,
                                    Code = 116,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 108L,
                                    Code = 117,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 109L,
                                    Code = 118,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 110L,
                                    Code = 119,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 111L,
                                    Code = 120,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 112L,
                                    Code = 121,
                                    RoleId = 1L
                                },
                                new
                                {
                                    Id = 113L,
                                    Code = 122,
                                    RoleId = 1L
                                });
                        });

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("AccountManagement.Domain.RoleAgg.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}