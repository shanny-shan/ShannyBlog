using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_db.Migrations
{
    /// <inheritdoc />
    public partial class InitCreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "abouts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    introduce = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    github = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    steam = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    web = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    biliBili = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abouts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "TEXT", nullable: false),
                    memo = table.Column<string>(type: "TEXT", nullable: false),
                    image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    href = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<long>(type: "bigint", nullable: false),
                    timelines = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    views = table.Column<int>(type: "int", nullable: false),
                    published = table.Column<bool>(type: "bit", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    nameEn = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    href = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    published = table.Column<bool>(type: "bit", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nameEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "timelines",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    articleId = table.Column<long>(type: "bigint", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timelines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tools",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    href = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    published = table.Column<bool>(type: "bit", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tools", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_details",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sex = table.Column<int>(type: "int", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_details", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    userId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.uuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abouts");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "medias");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "timelines");

            migrationBuilder.DropTable(
                name: "tools");

            migrationBuilder.DropTable(
                name: "user_details");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
