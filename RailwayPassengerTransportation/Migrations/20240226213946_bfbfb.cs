using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayPassengerTransportation.Migrations
{
    public partial class bfbfb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "conductors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fathername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conductors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dispatchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fathername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispatchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "listDayWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listDayWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "machinists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fathername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_machinists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fathername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Money = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoordA = table.Column<double>(type: "float", nullable: false),
                    CoordB = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trainCompositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachinistId = table.Column<int>(type: "int", nullable: false),
                    ConductorId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "listStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNum = table.Column<int>(type: "int", nullable: false),
                    isStart = table.Column<bool>(type: "bit", nullable: false),
                    TimeToArrive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeToLeave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_listStations_routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_listStations_stations_StationId",
                        column: x => x.StationId,
                        principalTable: "stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "railwayCarriages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: false),
                    CountSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_railwayCarriages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_railwayCarriages_trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isMon = table.Column<bool>(type: "bit", nullable: false),
                    isThues = table.Column<bool>(type: "bit", nullable: false),
                    isWen = table.Column<bool>(type: "bit", nullable: false),
                    isThurs = table.Column<bool>(type: "bit", nullable: false),
                    isFrid = table.Column<bool>(type: "bit", nullable: false),
                    isSat = table.Column<bool>(type: "bit", nullable: false),
                    isSun = table.Column<bool>(type: "bit", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: true),
                    TrainCompositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_flights_routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flights_trainCompositions_TrainCompositionId",
                        column: x => x.TrainCompositionId,
                        principalTable: "trainCompositions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_flights_trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "trains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeLeave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trips_flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    StationStartId = table.Column<int>(type: "int", nullable: false),
                    StationEndId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    DateTimeStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTimeEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_trips_TripId",
                        column: x => x.TripId,
                        principalTable: "trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_flights_RouteId",
                table: "flights",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_TrainCompositionId",
                table: "flights",
                column: "TrainCompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_TrainId",
                table: "flights",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_listStations_RouteId",
                table: "listStations",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_listStations_StationId",
                table: "listStations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_railwayCarriages_TrainId",
                table: "railwayCarriages",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_TripId",
                table: "tickets",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_trainCompositions_ConductorId",
                table: "trainCompositions",
                column: "ConductorId");

            migrationBuilder.CreateIndex(
                name: "IX_trainCompositions_MachinistId",
                table: "trainCompositions",
                column: "MachinistId");

            migrationBuilder.CreateIndex(
                name: "IX_trips_FlightId",
                table: "trips",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "dispatchers");

            migrationBuilder.DropTable(
                name: "listDayWeeks");

            migrationBuilder.DropTable(
                name: "listStations");

            migrationBuilder.DropTable(
                name: "passengers");

            migrationBuilder.DropTable(
                name: "railwayCarriages");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "stations");

            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "routes");

            migrationBuilder.DropTable(
                name: "trainCompositions");

            migrationBuilder.DropTable(
                name: "trains");

            migrationBuilder.DropTable(
                name: "conductors");

            migrationBuilder.DropTable(
                name: "machinists");
        }
    }
}
