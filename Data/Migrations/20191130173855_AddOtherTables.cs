using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketReservationSystem.Data.Migrations
{
    public partial class AddOtherTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // NOTE: In future Seats should be turned into a separate table
            // so that it's possible to implement per-seat pricing and allow
            // users to pick their seats. This however, is outside the scope of
            // this excercise.
            migrationBuilder.CreateTable(
                name: "Theatres",
                columns: table => new 
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    Seats = table.Column<int>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theatres", x => x.Id);
                }
            );
            migrationBuilder.CreateTable(
                name: "BusinessHours",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TheatreId = table.Column<string>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Begins = table.Column<DateTimeOffset>(nullable: false),
                    Ends = table.Column<DateTimeOffset>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessHours_Theatres_TheatreId",
                        column: x => x.TheatreId,
                        principalTable: "Theatres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );
            // NOTE: In future pricing should be split off to a separate table
            // just like the seats from "Theatres" table. Once again however,
            // this is beyond the scope of the requirements for this excercise.
            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TheatreId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    Price = table.Column<int>(nullable: true), // 1 unit = 1 cent
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performances_Theatres_TheatreId",
                        column: x => x.TheatreId,
                        principalTable: "Theatres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                }
            );
            migrationBuilder.CreateTable(
                name: "PerformanceCategories",
                columns: table => new
                {
                    PerformanceId = table.Column<string>(nullable: false),
                    CategoryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceCategories", x => new { x.PerformanceId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_PerformanceCategories_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_PerformanceCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );
            migrationBuilder.CreateTable(
                name: "PerformanceDates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PerformanceId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceDates_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );
            // NOTE: This functionality won't be implemented
            migrationBuilder.CreateTable(
                name: "PurchaseMethods",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseMethods", x => x.Id);
                }
            );
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    PurchaseMethodId = table.Column<string>(nullable: false),
                    PerformanceId = table.Column<string>(nullable: false),
                    SeatId = table.Column<int>(nullable: true), // See above comments about separating seats
                    AmountPaid = table.Column<int>(nullable: true), // This can be 0 if it was a reservation
                    Purchased = table.Column<DateTime>(nullable: false)                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Purchases_PurchaseMethods_PurchaseMethodId",
                        column: x => x.PurchaseMethodId,
                        principalTable: "PurchaseMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Purchases_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "TheatresIndex",
                table: "Theatres",
                column: "NormalizedName",
                unique: true
            );
            migrationBuilder.CreateIndex(
                name: "IX_BusinessHours_TheatreId",
                table: "BusinessHours",
                column: "TheatreId"
            );
            migrationBuilder.CreateIndex(
                name: "PerformanceIndex",
                table: "Performances",
                column: "NormalizedName",
                unique: true
            );
            migrationBuilder.CreateIndex(
                name: "CategoryIndex",
                table: "Categories",
                column: "NormalizedName",
                unique: true
            );
            migrationBuilder.CreateIndex(
                name: "IX_PerformanceCategories_Performances",
                table: "PerformanceCategories",
                column: "PerformanceId"
            );
            migrationBuilder.CreateIndex(
                name: "IX_PerformanceDates_Performances",
                table: "PerformanceDates",
                column: "PerformanceId"
            );
            migrationBuilder.CreateIndex(
                name: "PurchaseMethodIndex",
                table: "PurchaseMethods",
                column: "NormalizedName",
                unique: true
            );
            migrationBuilder.CreateIndex(
                name: "PurchaseIndex",
                table: "Purchases",
                column: "Id",
                unique: true
            );

            // Seed / Dummy data
            migrationBuilder.InsertData(
                table: "PurchaseMethods",
                columns: new[] { "Id", "Name", "NormalizedName", "Description", "ConcurrencyStamp" },
                values: new object[] { "df51617d-3a7c-40ee-9efc-d0b0b402b344", "Fake payment method",
                    "FAKE PAYMENT METHOD", "Dummy payment method", "abbdebc1-f0e8-41dd-b204-7a6289c7baeb" }
            );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "NormalizedName", "Description", "ConcurrencyStamp" },
                values: new object[] { "df51617d-3a7c-40ee-9efc-d0b0b402b344", "Musical", "MUSICAL", "", "1bfb0803-f829-426e-b2f1-43b8cd54a924" }
            );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "NormalizedName", "Description", "ConcurrencyStamp" },
                values: new object[] { "36b9e5f6-ee14-4280-823e-3b0594fa2089", "Romance", "ROMANCE", "", "143190cd-81dc-49df-9b3a-f45e50a3dba8" }
            );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "NormalizedName", "Description", "ConcurrencyStamp" },
                values: new object[] { "02056236-77f3-45d4-a0b9-28346e28ea9b", "Comedy", "COMEDY", "", "60130814-f2ae-40ef-a35a-2479854d7539" }
            );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "NormalizedName", "Description", "ConcurrencyStamp" },
                values: new object[] { "458c2350-e0ab-4542-a693-b4424407137d", "Tragedy", "TAGEDY", "", "f13a3446-dae5-4b17-8418-1ea8a98843ea" }
            );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "NormalizedName", "Description", "ConcurrencyStamp" },
                values: new object[] { "2b516d53-14f9-42b0-9468-c7fd2313fc4b", "Horror", "HORROR", "", "0005c76d-eb97-4494-91b8-1a902daa32cd" }
            );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "NormalizedName", "Description", "ConcurrencyStamp" },
                values: new object[] { "5ae237d9-e241-46b8-955e-968c84ec7855", "Fantasy", "FANTASY", "", "8cc33627-e960-476b-9d66-61583e4d48d5" }
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases"
            );
            migrationBuilder.DropTable(
                name: "PurchaseMethods"
            );
            migrationBuilder.DropTable(
                name: "PerformanceDates"
            );
            migrationBuilder.DropTable(
                name: "PerformanceCategories"
            );
            migrationBuilder.DropTable(
                name: "Categories"
            );
            migrationBuilder.DropTable(
                name: "Performances"
            );
            migrationBuilder.DropTable(
                name: "BusinessHours"
            );
            migrationBuilder.DropTable(
                name: "Theatres"
            );
        }
    }
}
