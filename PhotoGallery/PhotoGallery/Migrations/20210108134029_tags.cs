using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoGallery.Migrations
{
    public partial class tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlbumsTags",
                columns: table => new
                {
                    AlbumsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumsTags", x => new { x.AlbumsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_AlbumsTags_Albums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumsTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotosTags",
                columns: table => new
                {
                    PhotosId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosTags", x => new { x.PhotosId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PhotosTags_Photos_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotosTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumsTags_TagsId",
                table: "AlbumsTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosTags_TagsId",
                table: "PhotosTags",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumsTags");

            migrationBuilder.DropTable(
                name: "PhotosTags");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
