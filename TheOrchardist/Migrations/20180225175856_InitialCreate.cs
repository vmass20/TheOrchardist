using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheOrchardist.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalPlantList",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  Name = table.Column<string>(maxLength: 60, nullable: false),
                  FruitVariety = table.Column<string>(maxLength: 60, nullable: false),
                  Origin = table.Column<string>(nullable: true),
                  YearDeveloped = table.Column<string>(nullable: true),
                  Comments = table.Column<string>(nullable: true),                 
                    Use = table.Column<string>(nullable: true)
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
                    OrchardName = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orchard", x => x.OrchardID);
                });

            migrationBuilder.CreateTable(
                name: "UserPlantList",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualHarvestDate = table.Column<DateTime>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    DatePlanted = table.Column<DateTime>(nullable: false),
                    FruitVariety = table.Column<string>(maxLength: 60, nullable: false),
                    HarvestSeason = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    MaintainedHeight = table.Column<string>(nullable: true),
                    MaintainedWidth = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_UserPlantList", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalPlantList");

            migrationBuilder.DropTable(
                name: "Orchard");

            migrationBuilder.DropTable(
                name: "UserPlantList");
        }
    }
}
