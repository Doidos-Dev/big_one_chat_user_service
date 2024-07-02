using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    NICK_NAME = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    STATUS = table.Column<int>(type: "integer", nullable: false),
                    PHOTO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PASSWORD = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LAST_SEEN = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
