using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheOrchardist.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalPlantList",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    FruitVariety = table.Column<string>(maxLength: 60, nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Origin = table.Column<string>(nullable: true),
                    Use = table.Column<string>(nullable: true),
                    YearDeveloped = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalPlantList", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orchard",
                columns: table => new
                {
                    OrchardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrchardName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orchard", x => x.OrchardID);
                });

            migrationBuilder.CreateTable(
                name: "UserPlantList",
                columns: table => new
                {
                    OrchardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualHarvestDate = table.Column<DateTime>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    DatePlanted = table.Column<DateTime>(nullable: false),
                    FruitVariety = table.Column<string>(maxLength: 60, nullable: false),
                    HarvestSeason = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    MaintainedHeight = table.Column<string>(nullable: true),
                    MaintainedWidth = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    OrchardID1 = table.Column<int>(nullable: true),
                    OrchardName = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(maxLength: 200, nullable: true),
                    Parentage = table.Column<string>(maxLength: 200, nullable: true),
                    PlantName = table.Column<string>(maxLength: 60, nullable: false),
                    Rootstock = table.Column<string>(nullable: true),
                    RootstockHeight = table.Column<string>(nullable: true),
                    RootstockWidth = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    TreeCount = table.Column<int>(nullable: false),
                    Use = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    YearDeveloped = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlantList", x => x.OrchardID);
                    table.ForeignKey(
                        name: "FK_UserPlantList_Orchard_OrchardID1",
                        column: x => x.OrchardID1,
                        principalTable: "Orchard",
                        principalColumn: "OrchardID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPlantList_OrchardID1",
                table: "UserPlantList",
                column: "OrchardID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalPlantList");

            migrationBuilder.DropTable(
                name: "UserPlantList");

            migrationBuilder.DropTable(
                name: "Orchard");
        }
    }
}
