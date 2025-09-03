using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyMapSiteApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "instances",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instances", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "calendars",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    epoch = table.Column<int>(type: "integer", nullable: false),
                    minutes_per_hour = table.Column<int>(type: "integer", nullable: false),
                    hours_per_day = table.Column<int>(type: "integer", nullable: false),
                    days_per_year = table.Column<int>(type: "integer", nullable: false),
                    zero_year_after_epoch = table.Column<bool>(type: "boolean", nullable: false),
                    after_epoch_suffix = table.Column<string>(type: "text", nullable: true),
                    before_epoch_suffix = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_calendars", x => x.id);
                    table.ForeignKey(
                        name: "fk_calendars_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    organization_type = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => new { x.instance_id, x.id });
                    table.ForeignKey(
                        name: "fk_organizations_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "solar_systems",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    x = table.Column<int>(type: "integer", nullable: false),
                    y = table.Column<int>(type: "integer", nullable: false),
                    sector = table.Column<string>(type: "text", nullable: true),
                    region = table.Column<string>(type: "text", nullable: true),
                    focus = table.Column<string>(type: "text", nullable: true),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_solar_systems", x => new { x.instance_id, x.id });
                    table.ForeignKey(
                        name: "fk_solar_systems_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spacelanes",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    focus = table.Column<string>(type: "text", nullable: true),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_spacelanes", x => new { x.instance_id, x.id });
                    table.ForeignKey(
                        name: "fk_spacelanes_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "corporations",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    organization_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_corporations", x => new { x.instance_id, x.id });
                    table.ForeignKey(
                        name: "fk_corporations_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_corporations_organizations_instance_id_organization_id",
                        columns: x => new { x.instance_id, x.organization_id },
                        principalTable: "organizations",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "governments",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    color = table.Column<string>(type: "text", nullable: true),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    organization_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_governments", x => new { x.instance_id, x.id });
                    table.ForeignKey(
                        name: "fk_governments_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_governments_organizations_instance_id_organization_id",
                        columns: x => new { x.instance_id, x.organization_id },
                        principalTable: "organizations",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_organizations",
                columns: table => new
                {
                    child_id = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    relationship_string = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_organizations", x => new { x.instance_id, x.child_id, x.parent_id });
                    table.ForeignKey(
                        name: "fk_organization_organizations_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organization_organizations_organizations_instance_id_child_",
                        columns: x => new { x.instance_id, x.child_id },
                        principalTable: "organizations",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organization_organizations_organizations_instance_id_parent",
                        columns: x => new { x.instance_id, x.parent_id },
                        principalTable: "organizations",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planets",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    system_id = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planets", x => new { x.instance_id, x.id });
                    table.ForeignKey(
                        name: "fk_planets_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_planets_solar_systems_instance_id_system_id",
                        columns: x => new { x.instance_id, x.system_id },
                        principalTable: "solar_systems",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spacelane_segments",
                columns: table => new
                {
                    spacelane_id = table.Column<string>(type: "text", nullable: true),
                    origin_id = table.Column<string>(type: "text", nullable: false),
                    destination_id = table.Column<string>(type: "text", nullable: false),
                    start_reason_string = table.Column<string>(type: "text", nullable: true),
                    end_reason_string = table.Column<string>(type: "text", nullable: true),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_spacelane_segments_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_spacelane_segments_solar_systems_instance_id_destination_id",
                        columns: x => new { x.instance_id, x.destination_id },
                        principalTable: "solar_systems",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_spacelane_segments_solar_systems_instance_id_origin_id",
                        columns: x => new { x.instance_id, x.origin_id },
                        principalTable: "solar_systems",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_spacelane_segments_spacelanes_instance_id_spacelane_id",
                        columns: x => new { x.instance_id, x.spacelane_id },
                        principalTable: "spacelanes",
                        principalColumns: new[] { "instance_id", "id" });
                });

            migrationBuilder.CreateTable(
                name: "planet_governments",
                columns: table => new
                {
                    child_id = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    relationship_string = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planet_governments", x => new { x.instance_id, x.child_id, x.parent_id });
                    table.ForeignKey(
                        name: "fk_planet_governments_governments_instance_id_parent_id",
                        columns: x => new { x.instance_id, x.parent_id },
                        principalTable: "governments",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_planet_governments_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_planet_governments_planets_instance_id_child_id",
                        columns: x => new { x.instance_id, x.child_id },
                        principalTable: "planets",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_calendars_instance_id",
                table: "calendars",
                column: "instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_corporations_instance_id_organization_id",
                table: "corporations",
                columns: new[] { "instance_id", "organization_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_governments_instance_id_organization_id",
                table: "governments",
                columns: new[] { "instance_id", "organization_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_organization_organizations_instance_id_parent_id",
                table: "organization_organizations",
                columns: new[] { "instance_id", "parent_id" });

            migrationBuilder.CreateIndex(
                name: "ix_planet_governments_instance_id_parent_id",
                table: "planet_governments",
                columns: new[] { "instance_id", "parent_id" });

            migrationBuilder.CreateIndex(
                name: "ix_planets_instance_id_system_id",
                table: "planets",
                columns: new[] { "instance_id", "system_id" });

            migrationBuilder.CreateIndex(
                name: "ix_spacelane_segments_instance_id_destination_id",
                table: "spacelane_segments",
                columns: new[] { "instance_id", "destination_id" });

            migrationBuilder.CreateIndex(
                name: "ix_spacelane_segments_instance_id_origin_id",
                table: "spacelane_segments",
                columns: new[] { "instance_id", "origin_id" });

            migrationBuilder.CreateIndex(
                name: "ix_spacelane_segments_instance_id_spacelane_id",
                table: "spacelane_segments",
                columns: new[] { "instance_id", "spacelane_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calendars");

            migrationBuilder.DropTable(
                name: "corporations");

            migrationBuilder.DropTable(
                name: "organization_organizations");

            migrationBuilder.DropTable(
                name: "planet_governments");

            migrationBuilder.DropTable(
                name: "spacelane_segments");

            migrationBuilder.DropTable(
                name: "governments");

            migrationBuilder.DropTable(
                name: "planets");

            migrationBuilder.DropTable(
                name: "spacelanes");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "solar_systems");

            migrationBuilder.DropTable(
                name: "instances");
        }
    }
}
