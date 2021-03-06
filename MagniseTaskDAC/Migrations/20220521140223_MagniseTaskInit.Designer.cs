// <auto-generated />
using System;
using MagniseTaskDAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagniseTaskDAC.Migrations
{
    [DbContext(typeof(MagniseTaskContext))]
    [Migration("20220521140223_MagniseTaskInit")]
    partial class MagniseTaskInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MagniseTaskDAC.Entity.Crypto", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Price_usd")
                        .HasColumnType("numeric(18,5)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Crypto");
                });

            modelBuilder.Entity("MagniseTaskDAC.Entity.CryptoHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CryptoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Price_usd")
                        .HasColumnType("numeric(18,5)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CryptoId");

                    b.ToTable("CryptoHistory");
                });

            modelBuilder.Entity("MagniseTaskDAC.Entity.CryptoHistory", b =>
                {
                    b.HasOne("MagniseTaskDAC.Entity.Crypto", "Crypto")
                        .WithMany()
                        .HasForeignKey("CryptoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crypto");
                });
#pragma warning restore 612, 618
        }
    }
}
