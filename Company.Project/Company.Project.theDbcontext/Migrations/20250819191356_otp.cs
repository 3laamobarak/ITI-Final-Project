using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Company.Project.theDbcontext.Migrations
{
    /// <inheritdoc />
    public partial class otp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OTPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OTPs_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    BrandId1 = table.Column<int>(type: "int", nullable: true),
                    CategoryId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId1",
                        column: x => x.BrandId1,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessage_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Refund",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refund_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductId2 = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    ChatMessageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMember_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMember_ChatMessage_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMember_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronics Brand", false, "Apple", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Korean Electronics Brand", false, "Samsung", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Health Supplements Brand", false, "California Gold Nutrition", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Vitamins and multivitamins", false, "Vitamins", null },
                    { 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Dietary and herbal supplements", false, "Supplements", null },
                    { 3, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Skincare and personal hygiene", false, "Personal Care", null },
                    { 4, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Protein & performance nutrition", false, "Sports Nutrition", null },
                    { 5, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Baby health and care", false, "Baby", null }
                });

            migrationBuilder.InsertData(
                table: "ExClass",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Example 1", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Example 2", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BrandId1", "CategoryId", "CategoryId1", "CreatedAt", "Description", "ExpiryDate", "ImageUrl", "IsDeleted", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, null, 1, null, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "High potency vitamin C tablets", new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBwgHBgkIBwgKCgkLDRYPDQwMDRsUFRAWIB0iIiAdHx8kKDQsJCYxJx8fLT0tMTU3Ojo6Iys/RD84QzQ5OjcBCgoKDQwNGg8PGjclHyU3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3N//AABEIAMAAzAMBEQACEQEDEQH/xAAcAAEAAgMBAQEAAAAAAAAAAAAABAUCAwYBBwj/xABHEAABAwMCAwQFCAQMBwAAAAABAAIDBAUREiEGMUEHE1FhFCJxkaEVMkJScoGxwSOC0eIkJjM0NVNjc5KisuEWFyVDRFTw/8QAGgEBAAIDAQAAAAAAAAAAAAAAAAEEAgMFBv/EADQRAQACAQIEAwYFAwUBAAAAAAABAgMEEQUSITETQVEiMjNhcYGRobHB8CM0QhRS0eHxQ//aAAwDAQACEQMRAD8A+4oCAgICAgICAgxe9rBlzg0eZwgq6ziWxUWfS7zboCOfeVLG/iVMVmewgP494SYd+Ircfs1Ad+CmKWntAwPaFwk3c36jx9pT4V/QYjtF4PJwOIKLP208O/oN8fHfCUnLiK2D7VS1v4qJx29BZ0l5tdZj0O5Uk+eXdzNd+BWO2wnggnIwg9QEBAQEBAQEBAQEBAQEBAQEHKdp97qbDwdV1lDN3NSS2OOTAOkuPPfZZ0jedh+bK641l0kMl1r6irdnP6eUvGfIHYfcFYiIgbaL0KJwL2YPXRGT+AWUSlYXN1E+jaKSOcy53MsRY33lJkU+l7GYEdEeeSZsn8QsUMWSPZKxzI4g4fUdn81MDq6q6Uj6OPVQVUMmn1j3RLSfIrPdLmq00kr892NR+sxYzMISrLxPebFVRPtt0q442OGYe9Loy3O40HIWuYiR+q6Gb0mkgnxjvYmvx4ZGVWEhAQEBAQEBAQEBAQEBAQEBB8+7cnaeA5Bhp1VUQ9bpufitmL3h+e4nN5kghWIFtbqumic0vkaMHwWUTCVxxLdaSvtMVPSSOle1xJww496TI4026t3caV2CfEftWsa44JYpQXxuZhSO3HEETrPBSyCQSMbglzDjCz3HO1dVDI7LXtPlhRuK2VwzkYPkeSx80P15ZP6IoeX82j5fZCqeYnICAgICAgICAgICAgICAgIOE7aWSS8DTRw05ne6ojAY1heefMAdVnSdpHxa28D8VXBglgssrWOGWvmIZkezmPctviVgX1J2d8YRlpEdrix/W1P7qjxvkJVXwBxjWxCKruVmYxvINqP3QnjCsPZFfN9V2tZ8+/KjxYGI7I72D6t2tmf74p4sCw/5e8ZsjEbblaJWN+aDVfuqfGFfVdnfGG+aehmPhFOD+SeKOfunC/ENsIfW2aoa0EDUxgkH+XOB7Um8SP1LZ97XRZGD6PHt+qFo8xMQEBAQEBAQEBAQEBAQEBAQQbvQNuNG6mdUzU+SCJIX6XD70FNScP09OwRvdJVFv/eqTqe72oLSmo4IsaYm+wMQTNMWMd03l9VBo9GjO+nH6iDJlPE12S3P6iDOVkLm6TGP8CCrqrZTyh36GM58QgrXcLwVUzcVdTRNbvimk0h/kcoOrp4xDCyMOLgxoAJOScINiAgICAgICAgICAgICAg8KDXNL3Y23KDS31jl7tSDa0M8EGwBvRB7hAwgYQeEBBgQOgQaXkDoEGsTaHZBJ8igmRSCRuppQZoCAgICAgICAgICAgICCqqpj6Q9vhsgMlKCZC/PPkgkDHRBpnlLDsg0mpcEHnpTkEmKYPHIoPXyADZBAmm3OAghyTkFBMs83eGVvhugs0BAQEBAQEBAQEBAQEBBTVQ/hUntQZxtBwCgnQxtABQbsho3IAQaZBFMMh4djwOU7CK8Rh+nWNXgTugBjNWlzgD4Z3Ty3EuERtOGuGrG4yg9d3Txs8HfGxQQZ2NBOEECZo3QS7EMPl9iC4QEBAQEBAQEBAQEBAQEFPVfzqT2oPQ8RMdI7YNaXH2AZRMRvOyfSSNlhZJGToe3UPPKFqzWdpcr2j1k8NFR08MhY2eR2vBxnGMD2bq3pKxM2nbfZDhLq+78DcQzxWiSoqo5aXdvduc3U4bEgbZDsH2ZViOTPTmt0S1R8JyVdCa+qrqx1ydG6TvC52oO57HmPm9PE46LnZOJ1x6uNNydJ826uDenNurrbW3u48ZWCW4trtcE9PA+Uxva2QNlJDjtjcOAJ64yujNcdcd4hoh2HDsj3dqt7Y+V5bpkwxzjj5reQWm/TT1+yU7smHf2+tfMXPLJWFpc4noVGsjaYQ7Oo5k9SqYpLxc6O1wOmrqhkTRyBO7vYOqRG7OmO1/dhC7PL/Jf7jcpGxmOlha1kTTzPmfNN47LOp08YKxDukUxAQEBAQEBAQEBAQEAoKasIFW7UQMnAyeZRMRM9kK4VsLbbUO71oyTD+sdsKLdKzLdjx3546eW/wBkuwTtjsUT5ngMiBaXHwacLDFabUiUXi18vs95Jqdl9pqSqjAY1uXM7xuTg43+CZYyTtyW2a82Gcd+Tfs13KzS1l0ZVmo004ZpfEMgu2I5j2rTkwWvfm36ejTNZmVQ7hq4tJjiuf6DBGXA68e37ufmVpnR5N9ufp+e31RyX7bt9dw4+Sng9BrpYpImacvcT3m+c+3Kzy6aZiOS0xMJtj322kHDs9PAJ5rmxkrs63yNDQQemefxUxh1ER0yEY7+SHTX3hnhOkkgp52zTPdqe2mZncDAVysZJj253XsWjzX8nM3fjy73DUy1UopIzykf6zsfktd82LH71nRw8NiOturk56WarmNRcqqWomPVxyQqOXiMz0pGzpU01ax0jZ9J7I2RRxVwY3Dst5HbC3aON6c895cXil7Tl5J7Q+iK45ggICAgICAgICAgICDw8kHK8UvYTJTyBze8bmOUHZjhuFhktFY6t2H2Z593BV11mf37ZHtMMjmPe5hz64HP2/7LHHvaervabBz32jtEbfaVkziJk9jp6FneNL3udKG8zlx5fcpxUmKxVjptDamTmmPo70XKKlpY2siw9rADEDsw45ZS2akTtv1cTki+Sd7ef4pPpYpaH0i5y09NjJJc7DR4Ak9Vs7sbUibcuPeYcZeO0uy0rjHbmTXCX+xbpZ7yM/BT27rOPQ5bR7XRzNZxzxPcCRRRwW+I/Sxl+PaVVvrMFO87/R0MXCq97RuhQ01VXOMl0r6ipeeep5VO/FLR0pWHRx6SlO3RN9Dp4Wju4gD4ncqjfVZcnvWlvjHWESc5J2Wrz3SgydSsyX0bsqe30GqjAGoPyToIPlvyK7Wi+DH3eY4n/cT9Id4rbniAgICAgICAgICAgIPCg5viQ0Yjm+UXtjh23OR63TGN8+xY3rFq7W7NmPFbJPLWN3xyqZ3tcTBIS1xI0PcBnwO+Pf8AAKMOPwo6TvD1Gg0ltNWb83NG38/BIpf4NUua95Aidu8Hw6rdas3rtWdt17PvnwTETy7/AKOktV4rY97fENRGO9m3A82jl+K58VwaSZm1uvo4VOFYqzPWZhT3mklranvrpVzVcn1XnYKtl4na0/042dPFpMdO0fgiNhji2jY1o8h+a598t8nW0rUUiO0Njfh7crWy6rCjOySxlIm5KIQrZ+azhCFL5n/dZofTOy5//SZY8EFshJzjr9+V29H8GHluJf3E/Z2ytKIgICAgICAgICAgICAUHIcX0VRcHupI6YSwuAc4kt2d0O5/Ba8nPPuN+HJ4U81bbS+b8RWllqmbSyz9/VPGe7ZkgZ5Zceaiclqezv27y7ul4hfNMY6V6R3mdvyh5brcGvD6k95IPonkFztVxKbzy4ukOltNp3s6ODAaANsDlhcrrM7yiY2Vly/lCkNtVY9Sy+jxpUifRnkoYykTHZRDGVfN18t1nBKHJtus2M9n0nstY8W2ZxcdBI0s0ABvjvzOV3NJ8Gry/Ev7mzuFZURAQEBAQEBAQEBAQEBBR3ipipHVFROcRxM1O+4JvsyrWbzyx5vmVup5eIeITJJ6slU8uJ/q4/Af/dVyMl51GWMVfd/X5vT7V0Wnm23WP1dbDaOH66WagtlVIK2nBzqJIJH3YP3LO2jwWmaUnq59eIaqm17x7MqfS6KR8Ugw5ji0jwK5V6zW01nvDtVvW9YtXtPVhT2Wru1ZG2GJ4gc7Dp9Pqs8Vtw6a+WekdPVhqNZiwUmZnr6INTYpv+JH2alf30zNJMhbpABaHEn3j3rZk0t4zeHWd/m14uIY76fx7RtstJODg4TRUV0hqayIZkgxgj4/irE8O2jatt5Va8Z673p09UfhqzPuffyyyimpYP5WR/0fEKtp9PbNMzM7RC3rdbXTxEV6zPZLvNmhp6Blwt1Y2spXO0FzehOw9u+Fsz6OKV5qTvDTpdfOW/hZK8ssjw/bGPioKuql+VJojI1jW4a3yO2FYpo8cRyzPtSq24lmtabVj2YcXUMMckjDjUxxa72g4Kobbbx6OtFuesW9X0rstfG61TMYcva4ahvtldvSfBq81xL+5s7ZWVEQEBAQEBAQEBAQEBB47kg4LtMqu7po6X/2HjV9kDKq6vL4eLp5unwrFF828+TnuCqhlNxBTukO0gczyyeX4Ll6O0Uzx+Dr8SpOTTWiPqtKPhi5SXSeH14GAuIqMkBwJ6Eb5Wc6XLbPaI6fNXjX4K6WsW6+Wzdb6O1Fk7a26iKVsz2ajvrwfnb+aU0+O8zGS/WPzY5NXkxxXwsfs7FkuM8fElPbaSrL7f3sjSNIxJ6rjn3hbNNktjzxiid6sdXipn0ttRau1o/5TbaWntHvQcMvNNHpHsbHn8lbpt/qp+ihk3nh8enN+0qDgWKRvF1WS0iQPqDMepJkPP4Kpp+adXb6Sv6zkjh1ft/2t6UtqOFeKG0jS4Guqdm8yMjOP82FZ6Tiycvz/RTtvGXDzekET203A07p2lokqmhgcMZy5oC0ab2dLPN6rOs9rXV5PKGyvpKh3HEdw7s+iCna4zdMN1Z/EKxeszqaXjtsrYcla6O9J77vndXI2WeeVmHMkle9p8i4kFcrJO97T83e08cuGkT6Po3ZRUGa3VsZaAIZGtBzz2yutorzbHt6ODxPHFcsW9XdK45ogICAgICAgICAgICDw8kHy/tNlLr3BH0ZDn7yVy+I292r0HB67Utb5uYiOMEEg9CFzO3V2Nt+i+HEl2dT+iOuGlhGCdI149quRq9RNHPtwvBN+aIn9mNN3fdYbjAG2+VQtFt95heiIiOWOzVFWvttyirYY2vkiJ0tednEgt/NZ4cnh5Iv6Mc2GM2Kce+26LJeqt1+feg2OKocWjSzJbgNDTz8QFvy6qbZIyV6Sr4NBWmC2G87xKxq+M7nLA+Okpqamlkbh1QxpLseOFZniPTeK9VOODdY5rdEbh251VlJFG5rmOGHNk3DvM/tVPFqcmK02jruv6jQ482OKz027N17vFVdXR+myxMhjJLIWbNB8T4rbm1GTP05ejVptDTBbn33lX1F5rp6M0Rrn9xjSY2np4Z8FPjaiuPa28QytocM38TbqqXgAAAY25LQs9o6PonZGMUNyPQzt/0rq6CfYn6uBxf4lfo79X3JEBAQEBAQEBAQEBAQeFB8p7SD/GPHhC1cfiHxI+j0nCY/oT9XPRLnz2dR5TCkbVz/ACkyV7HsLYyz6Lj9I+zA969Fo8lZwxFe7LLGSccVxfdPZDZZcNbWSw5Iy55JPXPIcht7cdFb6+ana2qrPu/ghVdBay0uN4a46ZHafWGfUBaM/ayOW/P2YWrX0aL5M3bkVlbbKKBztF8EztLixsbeZ7t7wMk/WYG/rBY8lf8Aa01z5J2nl2/mzXFBbZoqVnynNFOY2OqNQ1NLnBpwPMZd7h5pyVjtH6Ji+aJnp5/onQ0NpDcm7vyMDGOZ1uHPGwwGnr85ZxWPKG2uTNP+CU5li0Avqagu2yGb52Gdy3xyp6x2hvi2o36ViPqgDu5axz6eHuoG5aBjdwBOCfM7ZVLX5K+DNd+q3HNFNrW3lm/fPh0XE82D6N2S/wBHV/8Aft/0rqcP9y31ef4vP9Sv0d4ug5IgICAgICAgICAgICDwoPlPaQMcSZ8YWrja/wCJ9npOE/A+7nY91QdRNh36ArHrHaU7QsaWngne2ObQ1pGNTmjAW3Fkvzbc0w1ZZmtd4jdrrbBQyOJjkpiAcZLBj5riNwNhsB95VmMmXvXLP4z6btEZvK2P+dv3VFJYqWrbqxFGWkg6owQ3Gndxz83fc9A3rlRTPmt/9JhszWrT/Df/AL8u3c/4fpvSXU7G07dOP5VhYdwTjAzjZp+CnxdRM8vifrCIth5d+Xf6fyPVLp7NG2aONwpWa26w4D1em2cY6jllYTbPM7Wv+afFpy81a9v56sqqKCORzaYtdGDgO0jfxWi97c3vNtN5jee6BNzP4LGGUwiSLNi+j9kn9GV/9+P9K7Gh9yXneK/FiPk7xXXLEBAQEBAQEBAQEBAQeFB8r7Sh/GJp/sGrj6/4v2ek4T8D7uXhlaXlo5g4PVUppMRu6EZazOyxg5gLVLanD5mOWdgsZIZXM2qWUODXRjDtmgjO+wzjwx5efjZtOG223RWxxqKx69kCtbaHuL4nSMyfVjZGTtgnqOZwBz2JBO2VNvAneYnZlinPERW23z/H9u8/KPVrENqALGVExGl2PVwM52HzMjPPlsRjdY8uKZ6yy8TUTtMx+/37/P167z27JrXW98bsSTvIaBG45zgNOBgjlnG2fHYbZW8KY6TLCK54ntH8lFeQWjcLQ3zshyrOBDkKzhh3fSuycYtdcc86j8l2ND8N53ivxndK65ggICAgICAgICAgIPMoMS4ckHzntHt9RLcI66KMyQtjDXady1cvX4rTbmh3eE6jHFZxzO0uBbDIKzXpbgkbgYPNU4vWabOjNLxk32dDT0lR3XeCMkEB2R4FafDvMb7N0ZccW2mV1R1dMynjiqKXMjW6Se7G/rZHuGPatmO9Yry2qrZMOTnm1Lfn8tlfUT291TUGoYS0kGPQ3nsQ7bpzDv1fNY8+Lntv0bYpnrSvL3/kx/x90OupLb8nzVNHO9zw4NZ3h5n1fLfYuOfLCzvTHyc9ZThyZ5yRjyR08/z/APGyKO01FTpkmEUDiBCImu1AE/TJ21dMjHXwU7YLW6z0/ndha2ppj5tp3895j8mynFla8EPncA/J0tJyMDPMDrlRHgb7eibRqvOI/Fvq5aKeN4prdKJntOHdA7PQZ8FNrY53itZYY65azHPeNoU1VR1EbXPfC5rRzcdvctfJaI9pYjLSbbRKraJJJHtLScnDQBkrKNp6V7sItNZmbdn1js4oJbdZZBUt0Syy6i3qNtsrsaTHbHj2s85xDNTLl3p5Os1BWlF7lB6gICAgICAg8QeEoMC5BqdKg0PqNOUFfNM102TzwiZ6zuhVVit1cS+amDXn6cXqn4Kvk0uLJO8wtYtdnw9K26fPqht4Qawk0dwmi8Gu3Cqzw6I9y2y7Xi2/xKbtjuHbs3Tpq4pS0gtztyz+1aZ0WeO1t26OI6We9ZhBqrNfWHDaSidh2dWBk8/2p/p9THTaGcarRz15pV3yNfmgAW6kJH0tAyo/0+p/2w2Tq9Jbfe89UeDhu+RVHex0rA4kndwxvn9q110eoid+VtvxHSTG02WsNn4gOnENFEQN3DTk/Bb402p9IVJ1miiZnmmW48NXWVjW1FwY1o5Bo5e5TGhzT71mM8S01fdxyyHCNOWj0yqqKjTyGcALbXh9P8rTLRfi19v6dYhLp7bQ0Df4PTxsP18Zd7yrdMOPH7sKGXUZcvvWlb26bRCQTzOVsaFgybKkb2vQbA5BkCg9QEBAQEGJQYlBqkzhBFlyghTl2CgqapzxnmoSjRXiWmOJQSB1CJ2WlJxBSvwC8Z8CcJujZbRXameAQfcVO6NnklZFIch+ENmBmj+so2NoYmaMfSTYPSomjJJUjRLdIGA5cPegq6i/xbtjOo+AUJ2RGVM1S4Ods3wQ2WtOXbYRCzhJwFIlxkoN7eSDY1BmgICAgIPMIPCEGDmoNL48oI8tPlBDmotQ5fBE7q6e1B+fVUTBurKixayct+CjZO6G6yVDD+iklb9lxQYehXSM4bUy4890DubwP/Jf/hCD0U94POqf9zQnUPk24yH16mY+w4Qbo7DITmQvcfMlBYwWXTjY5UxCN1lBbQwbt3Um6fDSgdEQlRw4Qb2sQbQ1B6AgyQEBAQEBAQeYQNIQYlgQYGFp5oMTAzwQazSsKDA0bDyCDE0LPD4IMfQGeHwQe+gM8PggyFE0dEGQpG9Qg2NpmhBmIWhBmGDwQZBoQegIPUBAQEBB/9k=", false, "Vitamin C 1000mg", 299.00m, 120, null },
                    { 2, 1, null, 2, null, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "EPA/DHA fish oil softgels", new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "data:image/jpeg;base64,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", false, "Omega-3 Fish Oil", 450.00m, 80, null },
                    { 3, 3, null, 1, null, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Vitamin D3 softgels for bone health", new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "data:image/jpeg;base64,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", false, "Vitamin D3 5000 IU", 220.00m, 200, null },
                    { 4, 1, null, 4, null, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Whey protein concentrate", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "data:image/jpeg;base64,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", false, "Whey Protein 2lb", 1250.00m, 35, null },
                    { 5, 3, null, 3, null, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Hydrating face serum", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "data:image/jpeg;base64,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", false, "Hyaluronic Acid Serum", 320.00m, 60, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_productId",
                table: "CartItems",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_userId",
                table: "CartItems",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_ApplicationUserId",
                table: "Chat",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMember_ChatId",
                table: "ChatMember",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMember_ChatMessageId",
                table: "ChatMember",
                column: "ChatMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMember_UserId",
                table: "ChatMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatId",
                table: "ChatMessage",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_SenderId",
                table: "ChatMessage",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OTPs_userId",
                table: "OTPs",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId1",
                table: "Products",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId1",
                table: "Products",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_OrderId",
                table: "Refund",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ProductId",
                table: "reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_UserId",
                table: "reviews",
                column: "UserId");
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
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ChatMember");

            migrationBuilder.DropTable(
                name: "ExClass");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "OTPs");

            migrationBuilder.DropTable(
                name: "Refund");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
