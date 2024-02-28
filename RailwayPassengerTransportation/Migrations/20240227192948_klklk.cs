using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayPassengerTransportation.Migrations
{
    public partial class klklk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_trainCompositions_TrainCompositionId",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_trains_TrainId",
                table: "flights");

            migrationBuilder.DropTable(
                name: "trainCompositions");

            migrationBuilder.DropIndex(
                name: "IX_flights_TrainCompositionId",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "TrainCompositionId",
                table: "flights");

            migrationBuilder.AlterColumn<int>(
                name: "TrainId",
                table: "flights",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConductorId",
                table: "flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MachinistId",
                table: "flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_flights_ConductorId",
                table: "flights",
                column: "ConductorId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_MachinistId",
                table: "flights",
                column: "MachinistId");

            migrationBuilder.AddForeignKey(
                name: "FK_flights_conductors_ConductorId",
                table: "flights",
                column: "ConductorId",
                principalTable: "conductors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_machinists_MachinistId",
                table: "flights",
                column: "MachinistId",
                principalTable: "machinists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_trains_TrainId",
                table: "flights",
                column: "TrainId",
                principalTable: "trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_conductors_ConductorId",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_machinists_MachinistId",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_trains_TrainId",
                table: "flights");

            migrationBuilder.DropIndex(
                name: "IX_flights_ConductorId",
                table: "flights");

            migrationBuilder.DropIndex(
                name: "IX_flights_MachinistId",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "ConductorId",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "MachinistId",
                table: "flights");

            migrationBuilder.AlterColumn<int>(
                name: "TrainId",
                table: "flights",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrainCompositionId",
                table: "flights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "trainCompositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConductorId = table.Column<int>(type: "int", nullable: false),
                    MachinistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainCompositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trainCompositions_conductors_ConductorId",
                        column: x => x.ConductorId,
                        principalTable: "conductors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trainCompositions_machinists_MachinistId",
                        column: x => x.MachinistId,
                        principalTable: "machinists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_flights_TrainCompositionId",
                table: "flights",
                column: "TrainCompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_trainCompositions_ConductorId",
                table: "trainCompositions",
                column: "ConductorId");

            migrationBuilder.CreateIndex(
                name: "IX_trainCompositions_MachinistId",
                table: "trainCompositions",
                column: "MachinistId");

            migrationBuilder.AddForeignKey(
                name: "FK_flights_trainCompositions_TrainCompositionId",
                table: "flights",
                column: "TrainCompositionId",
                principalTable: "trainCompositions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_flights_trains_TrainId",
                table: "flights",
                column: "TrainId",
                principalTable: "trains",
                principalColumn: "Id");
        }
    }
}
