﻿// <auto-generated />
using System;
using BoredApi.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BoredApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BoredApi.Data.DataModels.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.GroupActivity", b =>
                {
                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ActivityId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupActivities");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.JoinActivityRequest", b =>
                {
                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool?>("HasAccepted")
                        .HasColumnType("bit");

                    b.HasKey("ActivityId", "GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("JoinActivityRequests");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhotoId"), 1L, 1);

                    b.Property<int?>("GroupActivityActivityId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupActivityGroupId")
                        .HasColumnType("int");

                    b.Property<string>("PhotoText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhotoId");

                    b.HasIndex("GroupActivityActivityId", "GroupActivityGroupId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.UserGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UserEntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"), 1L, 1);

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActivityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.GroupActivity", b =>
                {
                    b.HasOne("Models.Activity", "Activity")
                        .WithMany("GroupActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoredApi.Data.DataModels.Group", "Group")
                        .WithMany("GroupActivities")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.JoinActivityRequest", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithMany("JoinActivityRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoredApi.Data.DataModels.GroupActivity", "GroupActivity")
                        .WithMany("JoinActivityRequests")
                        .HasForeignKey("ActivityId", "GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupActivity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.Photo", b =>
                {
                    b.HasOne("BoredApi.Data.DataModels.GroupActivity", null)
                        .WithMany("Photos")
                        .HasForeignKey("GroupActivityActivityId", "GroupActivityGroupId");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.UserGroup", b =>
                {
                    b.HasOne("BoredApi.Data.DataModels.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.Group", b =>
                {
                    b.Navigation("GroupActivities");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("BoredApi.Data.DataModels.GroupActivity", b =>
                {
                    b.Navigation("JoinActivityRequests");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Models.Activity", b =>
                {
                    b.Navigation("GroupActivities");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("JoinActivityRequests");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
