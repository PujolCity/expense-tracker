using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTracker.Modules.Expenses.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesToExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var otrosId = new Guid("66666666-6666-6666-6666-666666666666");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
            { new Guid("11111111-1111-1111-1111-111111111111"), "Alimentación" },
            { new Guid("22222222-2222-2222-2222-222222222222"), "Transporte" },
            { new Guid("33333333-3333-3333-3333-333333333333"), "Casa" },
            { new Guid("44444444-4444-4444-4444-444444444444"), "Salud" },
            { new Guid("55555555-5555-5555-5555-555555555555"), "Ocio" },
            { otrosId, "Otros" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "expenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql($"""
            UPDATE expenses
            SET "CategoryId" = '{otrosId}'
            WHERE "CategoryId" IS NULL
            OR "CategoryId" = '00000000-0000-0000-0000-000000000000';
            """);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "expenses",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_expenses_CategoryId",
                table: "expenses",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_expenses_Categories_CategoryId",
                table: "expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expenses_Categories_CategoryId",
                table: "expenses");

            migrationBuilder.DropIndex(
                name: "IX_expenses_CategoryId",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "expenses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
