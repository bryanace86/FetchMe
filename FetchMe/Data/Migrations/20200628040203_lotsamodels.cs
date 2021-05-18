using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class lotsamodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BannerImageId",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardExcerpt",
                table: "Photographers",
                maxLength: 280,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInsured",
                table: "Photographers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LogoImageId",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pinterest",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileBio",
                table: "Photographers",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tumblr",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "YearStarted",
                table: "Photographers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    BadgeClass = table.Column<string>(nullable: true),
                    Graphic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateTimeSpans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTimeSpans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GigDateTimeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GigDateTimeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GigLocationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GigLocationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GigStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Ordinal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GigStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GigTags",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GigTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FormattedAddress = table.Column<string>(nullable: true),
                    AdministrativeAreaLevel1 = table.Column<string>(nullable: true),
                    AdministrativeAreaLevel2 = table.Column<string>(nullable: true),
                    AdministrativeAreaLevel3 = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Locality = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    Political = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Route = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true),
                    Lat = table.Column<decimal>(type: "decimal(9, 6)", nullable: true),
                    Lng = table.Column<decimal>(type: "decimal(9, 6)", nullable: true),
                    Serviceable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotographerLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PhotographerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotographerLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotographerLikes_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotographTag",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotographTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotographTags",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotographTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotographerBadges",
                columns: table => new
                {
                    BadgeId = table.Column<Guid>(nullable: false),
                    PhotographerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotographerBadges", x => new { x.PhotographerId, x.BadgeId });
                    table.ForeignKey(
                        name: "FK_PhotographerBadges_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotographerBadges_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gig",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LocationTypeId = table.Column<Guid>(nullable: false),
                    MinBudget = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    MaxBudget = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    GigDateTimeTypeId = table.Column<Guid>(nullable: false),
                    GigStatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gig_GigDateTimeTypes_GigDateTimeTypeId",
                        column: x => x.GigDateTimeTypeId,
                        principalTable: "GigDateTimeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gig_GigStatuses_GigStatusId",
                        column: x => x.GigStatusId,
                        principalTable: "GigStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gig_GigLocationTypes_LocationTypeId",
                        column: x => x.LocationTypeId,
                        principalTable: "GigLocationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotographerLocations",
                columns: table => new
                {
                    PhotographerId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotographerLocations", x => new { x.PhotographerId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_PhotographerLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotographerLocations_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photographs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageDescription = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    HideFromGallery = table.Column<bool>(nullable: false),
                    PhotographerId = table.Column<Guid>(nullable: false),
                    CameraBodyValue = table.Column<string>(nullable: true),
                    ExposureTimeValue = table.Column<string>(nullable: true),
                    FStopValue = table.Column<string>(nullable: true),
                    ISOValue = table.Column<string>(nullable: true),
                    LightSourceValue = table.Column<string>(nullable: true),
                    LensValue = table.Column<string>(nullable: true),
                    FocalLengthValue = table.Column<string>(nullable: true),
                    LocationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photographs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photographs_PhotographTag_CameraBodyValue",
                        column: x => x.CameraBodyValue,
                        principalTable: "PhotographTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photographs_PhotographTag_ExposureTimeValue",
                        column: x => x.ExposureTimeValue,
                        principalTable: "PhotographTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photographs_PhotographTag_FStopValue",
                        column: x => x.FStopValue,
                        principalTable: "PhotographTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photographs_PhotographTag_FocalLengthValue",
                        column: x => x.FocalLengthValue,
                        principalTable: "PhotographTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photographs_PhotographTag_ISOValue",
                        column: x => x.ISOValue,
                        principalTable: "PhotographTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photographs_PhotographTag_LensValue",
                        column: x => x.LensValue,
                        principalTable: "PhotographTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photographs_PhotographTag_LightSourceValue",
                        column: x => x.LightSourceValue,
                        principalTable: "PhotographTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photographs_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photographs_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FeePercent = table.Column<int>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    FullAmount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    FullAmountDue = table.Column<DateTime>(nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    DepositAmountDue = table.Column<DateTime>(nullable: false),
                    GigId = table.Column<Guid>(nullable: false),
                    PhotographerId = table.Column<Guid>(nullable: false),
                    MessageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Gig_GigId",
                        column: x => x.GigId,
                        principalTable: "Gig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bids_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bids_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GigDateTimes",
                columns: table => new
                {
                    GigId = table.Column<Guid>(nullable: false),
                    DateTimeSpanId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GigDateTimes", x => new { x.GigId, x.DateTimeSpanId });
                    table.ForeignKey(
                        name: "FK_GigDateTimes_DateTimeSpans_DateTimeSpanId",
                        column: x => x.DateTimeSpanId,
                        principalTable: "DateTimeSpans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GigDateTimes_Gig_GigId",
                        column: x => x.GigId,
                        principalTable: "Gig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GigLocations",
                columns: table => new
                {
                    GigId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GigLocations", x => new { x.GigId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_GigLocations_Gig_GigId",
                        column: x => x.GigId,
                        principalTable: "Gig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GigLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GigTagGigs",
                columns: table => new
                {
                    GigId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GigTagGigs", x => new { x.GigId, x.TagId });
                    table.ForeignKey(
                        name: "FK_GigTagGigs_Gig_GigId",
                        column: x => x.GigId,
                        principalTable: "Gig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GigTagGigs_GigTags_TagId",
                        column: x => x.TagId,
                        principalTable: "GigTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotographLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PhotographId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotographLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotographLikes_Photographs_PhotographId",
                        column: x => x.PhotographId,
                        principalTable: "Photographs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotographTagsPhotographs",
                columns: table => new
                {
                    PhotographId = table.Column<Guid>(nullable: false),
                    PhotographTagId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotographTagsPhotographs", x => new { x.PhotographId, x.PhotographTagId });
                    table.ForeignKey(
                        name: "FK_PhotographTagsPhotographs_Photographs_PhotographId",
                        column: x => x.PhotographId,
                        principalTable: "Photographs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotographTagsPhotographs_PhotographTags_PhotographTagId",
                        column: x => x.PhotographTagId,
                        principalTable: "PhotographTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_BannerImageId",
                table: "Photographers",
                column: "BannerImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_LogoImageId",
                table: "Photographers",
                column: "LogoImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_Slug",
                table: "Photographers",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_GigId",
                table: "Bids",
                column: "GigId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_MessageId",
                table: "Bids",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_PhotographerId",
                table: "Bids",
                column: "PhotographerId");

            migrationBuilder.CreateIndex(
                name: "IX_Gig_GigDateTimeTypeId",
                table: "Gig",
                column: "GigDateTimeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Gig_GigStatusId",
                table: "Gig",
                column: "GigStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Gig_LocationTypeId",
                table: "Gig",
                column: "LocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GigDateTimes_DateTimeSpanId",
                table: "GigDateTimes",
                column: "DateTimeSpanId");

            migrationBuilder.CreateIndex(
                name: "IX_GigLocations_LocationId",
                table: "GigLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_GigTagGigs_TagId",
                table: "GigTagGigs",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotographerBadges_BadgeId",
                table: "PhotographerBadges",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotographerLikes_PhotographerId",
                table: "PhotographerLikes",
                column: "PhotographerId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotographerLocations_LocationId",
                table: "PhotographerLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotographLikes_PhotographId",
                table: "PhotographLikes",
                column: "PhotographId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_CameraBodyValue",
                table: "Photographs",
                column: "CameraBodyValue");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_ExposureTimeValue",
                table: "Photographs",
                column: "ExposureTimeValue");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_FStopValue",
                table: "Photographs",
                column: "FStopValue");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_FocalLengthValue",
                table: "Photographs",
                column: "FocalLengthValue");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_ISOValue",
                table: "Photographs",
                column: "ISOValue");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_LensValue",
                table: "Photographs",
                column: "LensValue");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_LightSourceValue",
                table: "Photographs",
                column: "LightSourceValue");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_LocationId",
                table: "Photographs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographs_PhotographerId",
                table: "Photographs",
                column: "PhotographerId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotographTagsPhotographs_PhotographTagId",
                table: "PhotographTagsPhotographs",
                column: "PhotographTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photographers_Photographs_BannerImageId",
                table: "Photographers",
                column: "BannerImageId",
                principalTable: "Photographs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photographers_Photographs_LogoImageId",
                table: "Photographers",
                column: "LogoImageId",
                principalTable: "Photographs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photographers_Photographs_BannerImageId",
                table: "Photographers");

            migrationBuilder.DropForeignKey(
                name: "FK_Photographers_Photographs_LogoImageId",
                table: "Photographers");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "GigDateTimes");

            migrationBuilder.DropTable(
                name: "GigLocations");

            migrationBuilder.DropTable(
                name: "GigTagGigs");

            migrationBuilder.DropTable(
                name: "PhotographerBadges");

            migrationBuilder.DropTable(
                name: "PhotographerLikes");

            migrationBuilder.DropTable(
                name: "PhotographerLocations");

            migrationBuilder.DropTable(
                name: "PhotographLikes");

            migrationBuilder.DropTable(
                name: "PhotographTagsPhotographs");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "DateTimeSpans");

            migrationBuilder.DropTable(
                name: "Gig");

            migrationBuilder.DropTable(
                name: "GigTags");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "Photographs");

            migrationBuilder.DropTable(
                name: "PhotographTags");

            migrationBuilder.DropTable(
                name: "GigDateTimeTypes");

            migrationBuilder.DropTable(
                name: "GigStatuses");

            migrationBuilder.DropTable(
                name: "GigLocationTypes");

            migrationBuilder.DropTable(
                name: "PhotographTag");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Photographers_BannerImageId",
                table: "Photographers");

            migrationBuilder.DropIndex(
                name: "IX_Photographers_LogoImageId",
                table: "Photographers");

            migrationBuilder.DropIndex(
                name: "IX_Photographers_Slug",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "CardExcerpt",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "IsInsured",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "LogoImageId",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "Pinterest",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "ProfileBio",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "Tumblr",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "YearStarted",
                table: "Photographers");
        }
    }
}
