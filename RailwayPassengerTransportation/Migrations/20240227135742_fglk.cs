using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayPassengerTransportation.Migrations
{
    public partial class fglk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_listStations_routes_RouteId",
                table: "listStations");

            migrationBuilder.DropForeignKey(
                name: "FK_listStations_stations_StationId",
                table: "listStations");

            migrationBuilder.AlterColumn<string>(
                name: "TimeToLeave",
                table: "listStations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TimeToArrive",
                table: "listStations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "StationId",
                table: "listStations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "listStations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrderNum",
                table: "listStations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "listStations",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "TimeToArriveDay",
                table: "listStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeToArriveHour",
                table: "listStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeToArriveMinutes",
                table: "listStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeToLeaveMinutes",
                table: "listStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_listStations_routes_RouteId",
                table: "listStations",
                column: "RouteId",
                principalTable: "routes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_listStations_stations_StationId",
                table: "listStations",
                column: "StationId",
                principalTable: "stations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_listStations_routes_RouteId",
                table: "listStations");

            migrationBuilder.DropForeignKey(
                name: "FK_listStations_stations_StationId",
                table: "listStations");

            migrationBuilder.DropColumn(
                name: "TimeToArriveDay",
                table: "listStations");

            migrationBuilder.DropColumn(
                name: "TimeToArriveHour",
                table: "listStations");

            migrationBuilder.DropColumn(
                name: "TimeToArriveMinutes",
                table: "listStations");

            migrationBuilder.DropColumn(
                name: "TimeToLeaveMinutes",
                table: "listStations");

            migrationBuilder.AlterColumn<string>(
                name: "TimeToLeave",
                table: "listStations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TimeToArrive",
                table: "listStations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StationId",
                table: "listStations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "listStations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderNum",
                table: "listStations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "listStations",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_listStations_routes_RouteId",
                table: "listStations",
                column: "RouteId",
                principalTable: "routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_listStations_stations_StationId",
                table: "listStations",
                column: "StationId",
                principalTable: "stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
