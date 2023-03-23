using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC_NetCore.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS(Select Id from AspNetRoles where Id= '83209fad-65fd-44ae-9a56-a3b24f5a64f7')
                BEGIN
                    INSERT AspNetRoles (Id, [Name], [NormalizedName])
                    VALUES ('83209fad-65fd-44ae-9a56-a3b24f5a64f7','admin','ADMIN')
                END
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE AspNetRoles Where Id = '83209fad-65fd-44ae-9a56-a3b24f5a64f7'");

        }
    }
}
