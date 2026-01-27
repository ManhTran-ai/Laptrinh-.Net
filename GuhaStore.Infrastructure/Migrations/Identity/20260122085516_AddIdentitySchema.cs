using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace GuhaStore.Infrastructure.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddIdentitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_users_AuthorId",
                table: "articles");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_orders_OrderId",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_products_ProductId",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_UserId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_product_reviews_products_ProductId",
                table: "product_reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_product_reviews_users_UserId",
                table: "product_reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_products_brands_BrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "users",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "users",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "users",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_users_Username",
                table: "users",
                newName: "IX_users_username");

            migrationBuilder.RenameIndex(
                name: "IX_users_Email",
                table: "users",
                newName: "IX_users_email");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                table: "products",
                newName: "stock_quantity");

            migrationBuilder.RenameColumn(
                name: "SalePrice",
                table: "products",
                newName: "sale_price");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "products",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "products",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "products",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "products",
                newName: "brand_id");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "products",
                newName: "IX_products_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandId",
                table: "products",
                newName: "IX_products_brand_id");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "product_reviews",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "product_reviews",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "product_reviews",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "product_reviews",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "product_reviews",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "product_reviews",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_product_reviews_UserId",
                table: "product_reviews",
                newName: "IX_product_reviews_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_reviews_ProductId_UserId",
                table: "product_reviews",
                newName: "IX_product_reviews_product_id_user_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "orders",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "orders",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "orders",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "orders",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "orders",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "orders",
                newName: "shipping_address");

            migrationBuilder.RenameColumn(
                name: "OrderCode",
                table: "orders",
                newName: "order_code");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "orders",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_orders_UserId",
                table: "orders",
                newName: "IX_orders_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_OrderCode",
                table: "orders",
                newName: "IX_orders_order_code");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "order_items",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "order_items",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "order_items",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "order_items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "order_items",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "order_items",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_items_ProductId",
                table: "order_items",
                newName: "IX_order_items_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_items_OrderId",
                table: "order_items",
                newName: "IX_order_items_order_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "categories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "categories",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "categories",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "brands",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "brands",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "brands",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "articles",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "articles",
                newName: "summary");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "articles",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "articles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "articles",
                newName: "published_at");

            migrationBuilder.RenameColumn(
                name: "IsPublished",
                table: "articles",
                newName: "is_published");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "articles",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "articles",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "articles",
                newName: "author_id");

            migrationBuilder.RenameIndex(
                name: "IX_articles_AuthorId",
                table: "articles",
                newName: "IX_articles_author_id");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "users",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "users",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "users",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "users",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "products",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "comment",
                table: "product_reviews",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "orders",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "shipping_address",
                table: "orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "categories",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "summary",
                table: "articles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "articles",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    session_id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    product_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    product_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    product_image = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.id);
                    table.ForeignKey(
                        name: "FK_carts_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_AspNetUserRoles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "NormalizedUserName",
                unique: true);

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
                name: "IX_carts_product_id",
                table: "carts",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_user_id",
                table: "carts",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_users_author_id",
                table: "articles",
                column: "author_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_orders_order_id",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_products_product_id",
                table: "order_items",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_reviews_products_product_id",
                table: "product_reviews",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_reviews_users_user_id",
                table: "product_reviews",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_brands_brand_id",
                table: "products",
                column: "brand_id",
                principalTable: "brands",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_users_author_id",
                table: "articles");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_orders_order_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_products_product_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_product_reviews_products_product_id",
                table: "product_reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_product_reviews_users_user_id",
                table: "product_reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_products_brands_brand_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_category_id",
                table: "products");

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
                name: "carts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "users");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "users");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "users");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "users");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_users_username",
                table: "users",
                newName: "IX_users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_users_email",
                table: "users",
                newName: "IX_users_Email");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "stock_quantity",
                table: "products",
                newName: "StockQuantity");

            migrationBuilder.RenameColumn(
                name: "sale_price",
                table: "products",
                newName: "SalePrice");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "products",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "products",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "products",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "brand_id",
                table: "products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_products_category_id",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_brand_id",
                table: "products",
                newName: "IX_products_BrandId");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "product_reviews",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "product_reviews",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "product_reviews",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "product_reviews",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "product_reviews",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "product_reviews",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_product_reviews_user_id",
                table: "product_reviews",
                newName: "IX_product_reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_product_reviews_product_id_user_id",
                table: "product_reviews",
                newName: "IX_product_reviews_ProductId_UserId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "orders",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "orders",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "shipping_address",
                table: "orders",
                newName: "ShippingAddress");

            migrationBuilder.RenameColumn(
                name: "order_code",
                table: "orders",
                newName: "OrderCode");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "orders",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_orders_user_id",
                table: "orders",
                newName: "IX_orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_order_code",
                table: "orders",
                newName: "IX_orders_OrderCode");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "order_items",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "order_items",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "order_items",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "order_items",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "order_items",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "order_items",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_order_items_product_id",
                table: "order_items",
                newName: "IX_order_items_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                newName: "IX_order_items_OrderId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "categories",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "categories",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "brands",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "brands",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "brands",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "articles",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "summary",
                table: "articles",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "articles",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "articles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "published_at",
                table: "articles",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "is_published",
                table: "articles",
                newName: "IsPublished");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "articles",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "articles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "articles",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_articles_author_id",
                table: "articles",
                newName: "IX_articles_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "customer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "products",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "product_reviews",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "orders",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShippingAddress",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "categories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "categories",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "articles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "articles",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_articles_users_AuthorId",
                table: "articles",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_orders_OrderId",
                table: "order_items",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_products_ProductId",
                table: "order_items",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_UserId",
                table: "orders",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_reviews_products_ProductId",
                table: "product_reviews",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_reviews_users_UserId",
                table: "product_reviews",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_brands_BrandId",
                table: "products",
                column: "BrandId",
                principalTable: "brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
