using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JWTWebApi.Migrations
{
    public partial class JWTWebApiModelsSalonApiContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE [dbo].[sp_QueueExist]
	                                @Veterinar int, 
	                                @AppointmnetDate nvarchar(100),
	                                @retVal int OUTPUT
                                AS
                                BEGIN
	                                SET @retVal = (SELECT COUNT(*) FROM Groomings g WHERE g.Appointment = @AppointmnetDate AND g.VeterinarId = @Veterinar);
	                                SELECT @retVal;
                                END";

            migrationBuilder.Sql(procedure);

            migrationBuilder.CreateTable(
                name: "Groomings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Appointment = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PetName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PetTypeId = table.Column<int>(nullable: false),
                    PetTypeName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    VeterinarId = table.Column<int>(nullable: false),
                    VeterinarName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groomings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    firstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Veterinars",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    firstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinars", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "firstName", "lastName", "password", "username" },
                values: new object[] { 1, "Shalom", "Shalom", "nimda", "admin" });

            migrationBuilder.InsertData(
                table: "Veterinars",
                columns: new[] { "id", "firstName", "lastName" },
                values: new object[,]
                {
                    { 1, "Stanley", "M.McRoy" },
                    { 2, "David", "Juarez" },
                    { 3, "Louis", "Mike Starson" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE [dbo].[ExistGroomingAppointmnet]";
            migrationBuilder.Sql(procedure);

            migrationBuilder.DropTable(
                name: "Groomings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Veterinars");
        }
    }
}
