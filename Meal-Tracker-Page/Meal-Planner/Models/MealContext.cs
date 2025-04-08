using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Meal_Planner.Models;

public partial class MealContext : DbContext
{
    public MealContext()
    {
    }

    public MealContext(DbContextOptions<MealContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FoodItem> FoodItems { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("food_Items_pk");

            entity.ToTable("food_Items");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ServingSize).HasColumnName("serving_size");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("ingredients_pk");

            entity.ToTable("ingredients");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.DietaryFiber).HasColumnName("dietary_fiber");
            entity.Property(e => e.MId).HasColumnName("m_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Protein).HasColumnName("protein");
            entity.Property(e => e.Sodium).HasColumnName("sodium");
            entity.Property(e => e.TotalFat).HasColumnName("total_fat");
            entity.Property(e => e.TotalSugers).HasColumnName("total_sugers");

            entity.HasOne(d => d.MIdNavigation).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.MId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredients_measurement");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.MealId).HasName("meals_pk");

            entity.ToTable("meals");

            entity.Property(e => e.MealId).HasColumnName("meal_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasMany(d => d.Items).WithMany(p => p.Meals)
                .UsingEntity<Dictionary<string, object>>(
                    "MealItem",
                    r => r.HasOne<FoodItem>().WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("meal_items_food_Items"),
                    l => l.HasOne<Meal>().WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("meal_items_meals"),
                    j =>
                    {
                        j.HasKey("MealId", "ItemId").HasName("meal_items_pk");
                        j.ToTable("meal_items");
                        j.IndexerProperty<int>("MealId").HasColumnName("meal_id");
                        j.IndexerProperty<int>("ItemId").HasColumnName("item_id");
                    });
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.MId).HasName("measurement_pk");

            entity.ToTable("measurement");

            entity.Property(e => e.MId).HasColumnName("m_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => new { e.ItemId, e.IngredientId }).HasName("recipe_ingredients_pk");

            entity.ToTable("recipe_ingredients");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Amount).HasColumnName("amount");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_ingredients_ingredients");

            entity.HasOne(d => d.Item).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_ingredients_food_Items");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
