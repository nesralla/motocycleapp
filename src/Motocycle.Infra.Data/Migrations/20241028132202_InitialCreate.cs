using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motocycle.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiErrorLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RootCause = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", nullable: false),
                    ExceptionStackTrace = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ModifyAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiErrorLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliveryman",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Identification = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    NationalID = table.Column<string>(type: "varchar(14)", nullable: false),
                    DateBorn = table.Column<DateTime>(type: "date", nullable: false),
                    DriveLicense = table.Column<string>(type: "varchar(11)", nullable: false),
                    LicenseType = table.Column<int>(type: "int", nullable: false),
                    DriveLicenseFile = table.Column<string>(type: "varchar(500)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ModifyAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveryman", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motocy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Identification = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    MotocyModel = table.Column<string>(type: "varchar(100)", nullable: false),
                    LicensePlate = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ModifyAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motocy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "varchar(15)", nullable: false),
                    DurationDays = table.Column<int>(type: "integer", nullable: false),
                    CostPerDay = table.Column<decimal>(type: "numeric", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ModifyAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Identification = table.Column<string>(type: "text", nullable: false),
                    DeliverymanId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotocyId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    PreviousEndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousValue = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    FinishValue = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    RentDays = table.Column<int>(type: "int", nullable: false),
                    RentPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentTypePlan = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ModifyAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rent_Deliveryman_DeliverymanId",
                        column: x => x.DeliverymanId,
                        principalTable: "Deliveryman",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rent_Motocy_MotocyId",
                        column: x => x.MotocyId,
                        principalTable: "Motocy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rent_Plan_RentPlanId",
                        column: x => x.RentPlanId,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rent_DeliverymanId",
                table: "Rent",
                column: "DeliverymanId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_MotocyId",
                table: "Rent",
                column: "MotocyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_RentPlanId",
                table: "Rent",
                column: "RentPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiErrorLog");

            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropTable(
                name: "Deliveryman");

            migrationBuilder.DropTable(
                name: "Motocy");

            migrationBuilder.DropTable(
                name: "Plan");
        }
    }
}
