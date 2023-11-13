﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NPaperless.DataAccess.Sql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NPaperless.DataAccess.Sql.Migrations
{
    [DbContext(typeof(NPaperlessDbContext))]
    [Migration("20231113041329_MakeSlugNullable")]
    partial class MakeSlugNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NPaperless.DataAccess.Entities.Correspondent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("DocumentCount")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsInsensitive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastCorrespondence")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MatchingAlgorithm")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Correspondents");
                });

            modelBuilder.Entity("NPaperless.DataAccess.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Added")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ArchiveSerialNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ArchivedFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Correspondent")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DocumentType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OriginalFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("StoragePath")
                        .HasColumnType("integer");

                    b.Property<List<int>>("Tags")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("NPaperless.DataAccess.Entities.DocumentType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("DocumentCount")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsInsensitive")
                        .HasColumnType("boolean");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MatchingAlgorithm")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("NPaperless.DataAccess.Entities.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("DocumentCount")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsInboxTag")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsInsensitive")
                        .HasColumnType("boolean");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MatchingAlgorithm")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("NPaperless.DataAccess.Entities.UserInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
