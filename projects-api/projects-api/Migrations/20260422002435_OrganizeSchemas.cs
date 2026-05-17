using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projects_api.Migrations
{
    /// <inheritdoc />
    public partial class OrganizeSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbRefreshTokens_AspNetUsers_UserId",
                table: "DbRefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbRefreshTokens",
                table: "DbRefreshTokens");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "fin");

            migrationBuilder.RenameTable(
                name: "Proventos",
                newName: "Proventos",
                newSchema: "fin");

            migrationBuilder.RenameTable(
                name: "Ativos",
                newName: "Ativos",
                newSchema: "fin");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "DbRefreshTokens",
                newName: "RefreshTokens",
                newSchema: "auth");

            migrationBuilder.RenameIndex(
                name: "IX_DbRefreshTokens_UserId",
                schema: "auth",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "auth",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                schema: "auth",
                table: "RefreshTokens",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                schema: "auth",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "auth",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "Proventos",
                schema: "fin",
                newName: "Proventos");

            migrationBuilder.RenameTable(
                name: "Ativos",
                schema: "fin",
                newName: "Ativos");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "auth",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "auth",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "auth",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "auth",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "auth",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "auth",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "auth",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                schema: "auth",
                newName: "DbRefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "DbRefreshTokens",
                newName: "IX_DbRefreshTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbRefreshTokens",
                table: "DbRefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbRefreshTokens_AspNetUsers_UserId",
                table: "DbRefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
