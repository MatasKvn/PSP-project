using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace POS_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartDiscounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    IsPercentage = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftCards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemDiscountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    IsPercentage = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Version = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    ImageURL = table.Column<string>(type: "text", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    ImageURL = table.Column<string>(type: "text", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    IsPercentage = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeVersionId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_AspNetUsers_EmployeeVersionId",
                        column: x => x.EmployeeVersionId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeVersionId = table.Column<int>(type: "integer", nullable: false),
                    CartDiscountId = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_EmployeeVersionId",
                        column: x => x.EmployeeVersionId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_CartDiscounts_CartDiscountId",
                        column: x => x.CartDiscountId,
                        principalTable: "CartDiscounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductModifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductVersionId = table.Column<int>(type: "integer", nullable: false),
                    ProductModificationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductModifications_Products_ProductVersionId",
                        column: x => x.ProductVersionId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOnItemDiscounts",
                columns: table => new
                {
                    LeftEntityId = table.Column<int>(type: "integer", nullable: false),
                    RightEntityId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOnItemDiscounts", x => new { x.LeftEntityId, x.RightEntityId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_ProductOnItemDiscounts_ItemDiscounts_RightEntityId",
                        column: x => x.RightEntityId,
                        principalTable: "ItemDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOnItemDiscounts_Products_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOnServices",
                columns: table => new
                {
                    EmployeeVersionId = table.Column<int>(type: "integer", nullable: false),
                    ServiceVersionId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOnServices", x => new { x.EmployeeVersionId, x.ServiceVersionId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_EmployeeOnServices_AspNetUsers_EmployeeVersionId",
                        column: x => x.EmployeeVersionId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeOnServices_Services_ServiceVersionId",
                        column: x => x.ServiceVersionId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOnItemDiscounts",
                columns: table => new
                {
                    LeftEntityId = table.Column<int>(type: "integer", nullable: false),
                    RightEntityId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOnItemDiscounts", x => new { x.LeftEntityId, x.RightEntityId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_ServiceOnItemDiscounts_ItemDiscounts_RightEntityId",
                        column: x => x.RightEntityId,
                        principalTable: "ItemDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOnItemDiscounts_Services_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOnTax",
                columns: table => new
                {
                    LeftEntityId = table.Column<int>(type: "integer", nullable: false),
                    RightEntityId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOnTax", x => new { x.LeftEntityId, x.RightEntityId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_ProductOnTax_Products_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOnTax_Taxes_RightEntityId",
                        column: x => x.RightEntityId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOnTax",
                columns: table => new
                {
                    LeftEntityId = table.Column<int>(type: "integer", nullable: false),
                    RightEntityId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOnTax", x => new { x.LeftEntityId, x.RightEntityId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_ServiceOnTax_Services_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOnTax_Taxes_RightEntityId",
                        column: x => x.RightEntityId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductVersionId = table.Column<int>(type: "integer", nullable: true),
                    ServiceVersionId = table.Column<int>(type: "integer", nullable: true),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsProduct = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Tip = table.Column<int>(type: "integer", nullable: true),
                    TransactionRef = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductModificationOnCartItems",
                columns: table => new
                {
                    LeftEntityId = table.Column<int>(type: "integer", nullable: false),
                    RightEntityId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModificationOnCartItems", x => new { x.LeftEntityId, x.RightEntityId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_ProductModificationOnCartItems_CartItems_RightEntityId",
                        column: x => x.RightEntityId,
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductModificationOnCartItems_ProductModifications_LeftEnt~",
                        column: x => x.LeftEntityId,
                        principalTable: "ProductModifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartItemId = table.Column<int>(type: "integer", nullable: false),
                    TimeSlotId = table.Column<int>(type: "integer", nullable: false),
                    BookingTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CustomerPhone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    isCancelled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceReservations_CartItems_CartItemId",
                        column: x => x.CartItemId,
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceReservations_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "EmployeeId", "EndDate", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "StartDate", "TwoFactorEnabled", "UserName", "Version" },
                values: new object[,]
                {
                    { 1, 0, new DateOnly(2000, 1, 2), "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "johndoe@example.com", true, 1, null, "John", false, "Doe", false, null, "johndoe@example.com", "johndoe", "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "3463466346", true, 0, "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", new DateOnly(2024, 1, 2), false, "johndoe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 0, new DateOnly(1996, 11, 12), "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "janedoe@example.com", true, 2, null, "Jane", false, "Doe", false, null, "janedoe@example.com", "janedoe", "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "77567455", true, 1, "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", new DateOnly(2022, 5, 6), false, "janedoe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 0, new DateOnly(2003, 1, 29), "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "adamsmith@example.com", true, 3, null, "Adam", false, "Smith", false, null, "adamsmith@example.com", "adamsmith", "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "4352335255", true, 2, "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", new DateOnly(2024, 11, 4), false, "adamsmith", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 0, new DateOnly(2002, 5, 6), "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "bobjohnson@example.com", true, 4, null, "Bob", false, "Johnson", false, null, "bobjohnson@example.com", "bobjohnson", "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "24142141241", true, 3, "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", new DateOnly(2023, 7, 7), false, "bobjohnson", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 0, new DateOnly(1990, 7, 7), "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "johndoe@example.com", true, 1, null, "Johnson", false, "Doe", false, null, "johndoe@example.com", "johnsondoe", "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", "546646564", true, 4, "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", new DateOnly(2009, 8, 14), false, "johnsondoe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ItemDiscounts",
                columns: new[] { "Id", "Description", "EndDate", "IsDeleted", "IsPercentage", "ItemDiscountId", "StartDate", "Value", "Version" },
                values: new object[,]
                {
                    { 1, "Desc1", null, true, true, 1, null, 12, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Desc2", new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 2, null, 15, new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1528) },
                    { 3, "Desc3", new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 500, new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1530) },
                    { 4, "Desc1 Update", null, true, true, 1, null, 18, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageURL", "IsDeleted", "Name", "Price", "ProductId", "Stock", "Version" },
                values: new object[,]
                {
                    { 1, "P1 desc", "", true, "Product1", 1099, 1, 10, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "P2 desc", "", true, "Product2", 199, 2, 5, new DateTime(2024, 10, 15, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "P1 v2 desc", "", true, "Product1 v2", 1099, 1, 15, new DateTime(2024, 10, 5, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "P1 v2 desc", "", false, "Product1 v3", 599, 1, 7, new DateTime(2024, 11, 1, 17, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "Duration", "EmployeeId", "ImageURL", "IsDeleted", "Name", "Price", "ServiceId", "Version" },
                values: new object[,]
                {
                    { 1, "S1 desc", 45, 1, "", false, "Service1", 2599, 1, new DateTime(2024, 10, 16, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "S2 desc", 25, 2, "", true, "Service2", 4599, 2, new DateTime(2024, 10, 18, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "S3 desc", 10, 2, "", true, "Service3", 1699, 3, new DateTime(2024, 10, 19, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "S2 v2 desc", 40, 3, "", false, "Service2 v2", 4099, 2, new DateTime(2024, 11, 1, 15, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Taxes",
                columns: new[] { "Id", "IsDeleted", "IsPercentage", "Name", "Rate", "TaxId", "Version" },
                values: new object[,]
                {
                    { 1, true, true, "Tax1", 5, 1, new DateTime(2024, 11, 1, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, false, true, "Tax2", 10, 2, new DateTime(2024, 11, 1, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, true, false, "Tax3", 299, 3, new DateTime(2024, 11, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, false, false, "Tax1 v2", 199, 1, new DateTime(2024, 11, 1, 20, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CartDiscountId", "DateCreated", "EmployeeVersionId", "IsDeleted", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 0 },
                    { 2, null, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, 2 },
                    { 3, null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, 1 },
                    { 4, null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, false, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductModifications",
                columns: new[] { "Id", "Description", "IsDeleted", "Name", "Price", "ProductModificationId", "ProductVersionId", "Version" },
                values: new object[,]
                {
                    { 1, "decs1", true, "Extra cheese", 99, 1, 1, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "decs1", false, "Extra cheese v2", 100, 1, 1, new DateTime(2024, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "decs1", true, "No cheese", 0, 2, 1, new DateTime(2024, 11, 2, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "decs1", false, "No cheese v2", 0, 2, 1, new DateTime(2024, 11, 3, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "decs1", false, "Extra fork", 50, 3, 2, new DateTime(2024, 11, 4, 9, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "EmployeeVersionId", "IsAvailable", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, true, new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1458) },
                    { 2, 1, true, new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1463) },
                    { 3, 2, false, new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1464) },
                    { 4, 3, true, new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1465) }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "IsDeleted", "IsProduct", "ProductVersionId", "Quantity", "ServiceVersionId" },
                values: new object[,]
                {
                    { 1, 1, false, true, 1, 2, null },
                    { 2, 1, false, false, null, 1, 1 },
                    { 3, 2, false, true, 2, 4, null },
                    { 4, 2, false, true, 3, 10, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CartDiscountId",
                table: "Carts",
                column: "CartDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_EmployeeVersionId",
                table: "Carts",
                column: "EmployeeVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOnServices_ServiceVersionId",
                table: "EmployeeOnServices",
                column: "ServiceVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModificationOnCartItems_RightEntityId",
                table: "ProductModificationOnCartItems",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModifications_ProductVersionId",
                table: "ProductModifications",
                column: "ProductVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOnItemDiscounts_RightEntityId",
                table: "ProductOnItemDiscounts",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOnTax_RightEntityId",
                table: "ProductOnTax",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOnItemDiscounts_RightEntityId",
                table: "ServiceOnItemDiscounts",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOnTax_RightEntityId",
                table: "ServiceOnTax",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservations_CartItemId",
                table: "ServiceReservations",
                column: "CartItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservations_TimeSlotId",
                table: "ServiceReservations",
                column: "TimeSlotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_EmployeeVersionId",
                table: "TimeSlots",
                column: "EmployeeVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CartId",
                table: "Transactions",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmployeeOnServices");

            migrationBuilder.DropTable(
                name: "GiftCards");

            migrationBuilder.DropTable(
                name: "ProductModificationOnCartItems");

            migrationBuilder.DropTable(
                name: "ProductOnItemDiscounts");

            migrationBuilder.DropTable(
                name: "ProductOnTax");

            migrationBuilder.DropTable(
                name: "ServiceOnItemDiscounts");

            migrationBuilder.DropTable(
                name: "ServiceOnTax");

            migrationBuilder.DropTable(
                name: "ServiceReservations");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ProductModifications");

            migrationBuilder.DropTable(
                name: "ItemDiscounts");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CartDiscounts");
        }
    }
}
