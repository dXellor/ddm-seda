using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace seda_dll.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "incident_documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    employee_last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    security_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    targeted_name = table.Column<string>(type: "text", nullable: false),
                    incident_level = table.Column<string>(type: "text", nullable: false),
                    targeted_address = table.Column<string>(type: "text", nullable: false),
                    targeted_longitude = table.Column<double>(type: "double precision", nullable: false),
                    targeted_latitude = table.Column<double>(type: "double precision", nullable: false),
                    file_system_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incident_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.UniqueConstraint("AK_users_email", x => x.email);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "incident_documents");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
