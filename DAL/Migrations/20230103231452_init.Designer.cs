// <auto-generated />
using System;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230103231452_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DAL.Models.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BuildDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastCollectDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerInformationId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("XCoordinate")
                        .HasColumnType("int");

                    b.Property<int>("YCoordinate")
                        .HasColumnType("int");

                    b.HasKey("BuildingId");

                    b.HasIndex("PlayerInformationId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("DAL.Models.Classified", b =>
                {
                    b.Property<int>("ClassifiedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Item")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerInformationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ReplacementAmount")
                        .HasColumnType("int");

                    b.Property<int>("ReplacementItem")
                        .HasColumnType("int");

                    b.HasKey("ClassifiedId");

                    b.HasIndex("PlayerInformationId");

                    b.ToTable("Classifieds");
                });

            modelBuilder.Entity("DAL.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsOpened")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<int?>("PlayerInformationId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("NotificationId");

                    b.HasIndex("PlayerInformationId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("DAL.Models.PlayerInformation", b =>
                {
                    b.Property<int>("PlayerInformationId")
                        .HasColumnType("int");

                    b.Property<int>("Ability")
                        .HasColumnType("int");

                    b.Property<int>("Coins")
                        .HasColumnType("int");

                    b.Property<int>("ExperiencePoint")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastBattleDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastExpeditionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SelectedIsland")
                        .HasColumnType("int");

                    b.Property<int>("Stones")
                        .HasColumnType("int");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("Woods")
                        .HasColumnType("int");

                    b.HasKey("PlayerInformationId");

                    b.ToTable("PlayerInformations");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("ResetToken")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("ValidationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ValidationToken")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.Building", b =>
                {
                    b.HasOne("DAL.Models.PlayerInformation", "PlayerInformation")
                        .WithMany("Buildings")
                        .HasForeignKey("PlayerInformationId");

                    b.Navigation("PlayerInformation");
                });

            modelBuilder.Entity("DAL.Models.Classified", b =>
                {
                    b.HasOne("DAL.Models.PlayerInformation", "PlayerInformation")
                        .WithMany("Classifieds")
                        .HasForeignKey("PlayerInformationId");

                    b.Navigation("PlayerInformation");
                });

            modelBuilder.Entity("DAL.Models.Notification", b =>
                {
                    b.HasOne("DAL.Models.PlayerInformation", "PlayerInformation")
                        .WithMany("Notifications")
                        .HasForeignKey("PlayerInformationId");

                    b.Navigation("PlayerInformation");
                });

            modelBuilder.Entity("DAL.Models.PlayerInformation", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithOne("PlayerInformation")
                        .HasForeignKey("DAL.Models.PlayerInformation", "PlayerInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.PlayerInformation", b =>
                {
                    b.Navigation("Buildings");

                    b.Navigation("Classifieds");

                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Navigation("PlayerInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
