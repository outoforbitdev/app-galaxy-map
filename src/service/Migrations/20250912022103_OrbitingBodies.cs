using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyMapSiteApi.Migrations
{
    /// <inheritdoc />
    public partial class OrbitingBodies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "planet_governments");

            migrationBuilder.DropTable(
                name: "planets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organization_organizations",
                table: "organization_organizations");

            migrationBuilder.RenameColumn(
                name: "relationship_string",
                table: "organization_organizations",
                newName: "relationship");

            migrationBuilder.AlterColumn<int>(
                name: "start_date",
                table: "organization_organizations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "relationship",
                table: "organization_organizations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddPrimaryKey(
                name: "pk_organization_organizations",
                table: "organization_organizations",
                columns: new[] { "instance_id", "child_id", "parent_id", "start_date", "relationship" });

            migrationBuilder.CreateTable(
                name: "orbiting_bodies",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    system_id = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: true),
                    orbited_body_id = table.Column<string>(type: "text", nullable: true),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orbiting_bodies", x => new { x.instance_id, x.id });
                    table.ForeignKey(
                        name: "fk_orbiting_bodies_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orbiting_bodies_orbiting_bodies_instance_id_orbited_body_id",
                        columns: x => new { x.instance_id, x.orbited_body_id },
                        principalTable: "orbiting_bodies",
                        principalColumns: new[] { "instance_id", "id" });
                    table.ForeignKey(
                        name: "fk_orbiting_bodies_solar_systems_instance_id_system_id",
                        columns: x => new { x.instance_id, x.system_id },
                        principalTable: "solar_systems",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orbiting_body_governments",
                columns: table => new
                {
                    child_id = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true),
                    relationship = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    end_date = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orbiting_body_governments", x => new { x.instance_id, x.child_id, x.parent_id });
                    table.ForeignKey(
                        name: "fk_orbiting_body_governments_governments_instance_id_parent_id",
                        columns: x => new { x.instance_id, x.parent_id },
                        principalTable: "governments",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orbiting_body_governments_instances_instance_id",
                        column: x => x.instance_id,
                        principalTable: "instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orbiting_body_governments_orbiting_bodies_instance_id_child",
                        columns: x => new { x.instance_id, x.child_id },
                        principalTable: "orbiting_bodies",
                        principalColumns: new[] { "instance_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_orbiting_bodies_instance_id_orbited_body_id",
                table: "orbiting_bodies",
                columns: new[] { "instance_id", "orbited_body_id" });

            migrationBuilder.CreateIndex(
                name: "ix_orbiting_bodies_instance_id_system_id",
                table: "orbiting_bodies",
                columns: new[] { "instance_id", "system_id" });

            migrationBuilder.CreateIndex(
                name: "ix_orbiting_body_governments_instance_id_parent_id",
                table: "orbiting_body_governments",
                columns: new[] { "instance_id", "parent_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orbiting_body_governments");

            migrationBuilder.DropTable(
                name: "orbiting_bodies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organization_organizations",
                table: "organization_organizations");

            migrationBuilder.RenameColumn(
                name: "relationship",
                table: "organization_organizations",
                newName: "relationship_string");

            migrationBuilder.AlterColumn<int>(
                name: "start_date",
                table: "organization_organizations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "relationship_string",
                table: "organization_organizations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddPrimaryKey(
                name: "pk_organization_organizations",
                table: "organization_organizations",
                columns: new[] { "instance_id", "child_id", "parent_id" });

            migrationBuilder.CreateTable(
                name: "planets",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    system_id = table.Column<string>(type: "text", nullable: false),
                    end_date = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true)
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
                name: "planet_governments",
                columns: table => new
                {
                    child_id = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<string>(type: "text", nullable: false),
                    instance_id = table.Column<string>(type: "text", nullable: false),
                    end_date = table.Column<int>(type: "integer", nullable: true),
                    relationship_string = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<int>(type: "integer", nullable: true)
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
                name: "ix_planet_governments_instance_id_parent_id",
                table: "planet_governments",
                columns: new[] { "instance_id", "parent_id" });

            migrationBuilder.CreateIndex(
                name: "ix_planets_instance_id_system_id",
                table: "planets",
                columns: new[] { "instance_id", "system_id" });
        }
    }
}
