using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortoFree.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_missing_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentHistories_AspNetUsers_UserId",
                table: "EmploymentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExamples_AspNetUsers_UserId",
                table: "WorkExamples");

            migrationBuilder.DropIndex(
                name: "IX_WorkExamples_UserId",
                table: "WorkExamples");

            migrationBuilder.DropIndex(
                name: "IX_EmploymentHistories_UserId",
                table: "EmploymentHistories");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkExamples");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmploymentHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "SenderPositon",
                table: "Comments",
                newName: "SenderPosition");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "WorkExamples",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "WorkExamples",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Skills",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "EmploymentHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkExamples_OwnerId",
                table: "WorkExamples",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistories_OwnerId",
                table: "EmploymentHistories",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OwnerId",
                table: "Comments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_OwnerId",
                table: "Comments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentHistories_AspNetUsers_OwnerId",
                table: "EmploymentHistories",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExamples_AspNetUsers_OwnerId",
                table: "WorkExamples",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_OwnerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentHistories_AspNetUsers_OwnerId",
                table: "EmploymentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExamples_AspNetUsers_OwnerId",
                table: "WorkExamples");

            migrationBuilder.DropIndex(
                name: "IX_WorkExamples_OwnerId",
                table: "WorkExamples");

            migrationBuilder.DropIndex(
                name: "IX_EmploymentHistories_OwnerId",
                table: "EmploymentHistories");

            migrationBuilder.DropIndex(
                name: "IX_Comments_OwnerId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "WorkExamples");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "EmploymentHistories");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "SenderPosition",
                table: "Comments",
                newName: "SenderPositon");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "WorkExamples",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WorkExamples",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EmploymentHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkExamples_UserId",
                table: "WorkExamples",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistories_UserId",
                table: "EmploymentHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentHistories_AspNetUsers_UserId",
                table: "EmploymentHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExamples_AspNetUsers_UserId",
                table: "WorkExamples",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
