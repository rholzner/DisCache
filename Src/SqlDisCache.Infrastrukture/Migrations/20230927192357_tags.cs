using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlDisCache.Infrastrukture.Migrations
{
    /// <inheritdoc />
    public partial class tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CacheTag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CacheTagTestCache",
                columns: table => new
                {
                    CachItemsId = table.Column<string>(type: "nvarchar(449)", nullable: false),
                    TagsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheTagTestCache", x => new { x.CachItemsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CacheTagTestCache_CacheTag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "CacheTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CacheTagTestCache_DistributedCache_CachItemsId",
                        column: x => x.CachItemsId,
                        principalTable: "DistributedCache",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CacheTagTestCache_TagsId",
                table: "CacheTagTestCache",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CacheTagTestCache");

            migrationBuilder.DropTable(
                name: "CacheTag");
        }
    }
}
