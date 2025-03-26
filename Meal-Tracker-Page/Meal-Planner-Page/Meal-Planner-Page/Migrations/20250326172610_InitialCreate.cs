using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meal_Planner_Page.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "food_Items",
                columns: table => new
                {
                    item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    serving_size = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("food_Items_pk", x => x.item_id);
                });

            migrationBuilder.CreateTable(
                name: "meals",
                columns: table => new
                {
                    meal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("meals_pk", x => x.meal_id);
                });

            migrationBuilder.CreateTable(
                name: "measurement",
                columns: table => new
                {
                    m_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("measurement_pk", x => x.m_id);
                });

            migrationBuilder.CreateTable(
                name: "meal_items",
                columns: table => new
                {
                    meal_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("meal_items_pk", x => new { x.meal_id, x.item_id });
                    table.ForeignKey(
                        name: "meal_items_food_Items",
                        column: x => x.item_id,
                        principalTable: "food_Items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "meal_items_meals",
                        column: x => x.meal_id,
                        principalTable: "meals",
                        principalColumn: "meal_id");
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    ingredient_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    m_id = table.Column<int>(type: "int", nullable: false),
                    calories = table.Column<float>(type: "real", nullable: false),
                    protein = table.Column<float>(type: "real", nullable: false),
                    sodium = table.Column<float>(type: "real", nullable: false),
                    total_sugers = table.Column<float>(type: "real", nullable: false),
                    total_fat = table.Column<float>(type: "real", nullable: false),
                    dietary_fiber = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ingredients_pk", x => x.ingredient_id);
                    table.ForeignKey(
                        name: "ingredients_measurement",
                        column: x => x.m_id,
                        principalTable: "measurement",
                        principalColumn: "m_id");
                });

            migrationBuilder.CreateTable(
                name: "recipe_ingredients",
                columns: table => new
                {
                    item_id = table.Column<int>(type: "int", nullable: false),
                    ingredient_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recipe_ingredients_pk", x => new { x.item_id, x.ingredient_id });
                    table.ForeignKey(
                        name: "recipe_ingredients_food_Items",
                        column: x => x.item_id,
                        principalTable: "food_Items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "recipe_ingredients_ingredients",
                        column: x => x.ingredient_id,
                        principalTable: "ingredients",
                        principalColumn: "ingredient_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_m_id",
                table: "ingredients",
                column: "m_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_items_item_id",
                table: "meal_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredients_ingredient_id",
                table: "recipe_ingredients",
                column: "ingredient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meal_items");

            migrationBuilder.DropTable(
                name: "recipe_ingredients");

            migrationBuilder.DropTable(
                name: "meals");

            migrationBuilder.DropTable(
                name: "food_Items");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "measurement");
        }
    }
}
