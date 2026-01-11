using System.Text.RegularExpressions;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 15 of the coding challenge.
/// </summary>
public static class Day15
{
    private const int MaxTeaspoons = 100;
    private const int RequiredCalories = 500;
    private static readonly List<(string name, int capacity, int durability, int flavor, int texture, int calories)> _ingredients = [];
    private static int[]? _recipe;

    /// <summary>
    /// Calculates the highest possible score for a recipe based on the provided ingredient descriptions.
    /// </summary>
    /// <param name="input">An array of strings, each representing the properties of an ingredient to be used in the recipe. Each string
    /// should follow the expected input format for ingredient parsing.</param>
    /// <returns>A string representation of the maximum achievable score for any valid recipe combination using the given
    /// ingredients.</returns>
    public static string SolvePart1(string[] input)
    {
        foreach (var item in input)
        {
            _ingredients.Add(ParseInput(item));
        }

        _recipe = new int[_ingredients.Count];
        long result = 0;

        GeneratePossibleRecipes(_ingredients, _recipe, 0, MaxTeaspoons, recipe =>
        {
            long score = CalculateScore(_ingredients, _recipe);
            if (score > result)
            {
                result = score;
            }
        });
        return result.ToString();
    }

    /// <summary>
    /// Calculates the highest possible recipe score using the provided input, subject to a fixed calorie constraint.
    /// </summary>
    /// <param name="input">An array of strings representing the ingredient specifications for the recipe calculation.</param>
    /// <returns>A string representation of the highest recipe score that meets the required calorie condition. Returns "0" if no
    /// valid recipe is found.</returns>
    public static string SolvePart2(string[] input)
    {
        long result = 0;

        GeneratePossibleRecipes(_ingredients, _recipe, 0, MaxTeaspoons, recipe =>
        {
            int calculatedCalories = CalculateCalories(_ingredients, _recipe);
            if (calculatedCalories != RequiredCalories)
            {
                return;
            }

            long score = CalculateScore(_ingredients, _recipe);
            if (score > result)
            {
                result = score;
            }
        });

        return result.ToString();
    }

    /// <summary>
    /// Parses a formatted input string and extracts the ingredient name and its associated properties.
    /// </summary>
    /// <param name="input">A string containing the ingredient name followed by its properties in the format "Name: capacity X, durability
    /// Y, flavor Z, texture W, calories V". The string must include all properties in the specified order.</param>
    /// <returns>A tuple containing the ingredient name and its capacity, durability, flavor, texture, and calories values, in
    /// that order.</returns>
    private static (string name, int capacity, int durability, int flavor, int texture, int calories) ParseInput(string input)
    {
        var splitName = input.Split(':');
        var match = Regex.Matches(input, @"(-?\d+)").Select(n => int.Parse(n.Value)).ToList();

        string name = splitName[0];
        int capacity = match[0];
        int durability = match[1];
        int flavor = match[2];
        int texture = match[3];
        int calories = match[4];

        return (name, capacity, durability, flavor, texture, calories);
    }

    /// <summary>
    /// Generates all possible combinations of ingredient amounts that sum to a specified total and invokes a callback
    /// for each valid combination.
    /// </summary>
    /// <param name="ingredients">A list of ingredient tuples, each containing the name and properties of an ingredient. The order of ingredients
    /// determines the order of amounts in each combination.</param>
    /// <param name="amounts">An array used to store the current combination of amounts for each ingredient. The array is updated in place and
    /// passed to the callback for each valid combination.</param>
    /// <param name="ingredientIndex">The zero-based index of the ingredient currently being assigned an amount. Used to track progress through the
    /// ingredients list.</param>
    /// <param name="remaining">The remaining total amount to distribute among the ingredients. Must be zero or positive.</param>
    /// <param name="callback">A callback action that is invoked for each valid combination of ingredient amounts. Receives the amounts array
    /// representing one possible recipe.</param>
    private static void GeneratePossibleRecipes(List<(string name, int capacity, int durability, int flavor, int texture, int calories)> ingredients,
        int[] amounts, int ingredientIndex, int remaining, Action<int[]> callback)
    {
        if (ingredientIndex == ingredients.Count - 1)
        {
            amounts[ingredientIndex] = remaining;
            callback(amounts);
            return;
        }

        for (int amount = 0; amount <= remaining; amount++)
        {
            amounts[ingredientIndex] = amount;
            GeneratePossibleRecipes(ingredients, amounts, ingredientIndex + 1, remaining - amount, callback);
        }
    }

    /// <summary>
    /// Calculates the total score for a recipe based on the provided ingredient properties and their assigned amounts.
    /// </summary>
    /// <param name="ingredients">A list of ingredient tuples, where each tuple contains the name and property values (capacity, durability,
    /// flavor, texture, and calories) for an ingredient. The order of ingredients must correspond to the order of
    /// amounts.</param>
    /// <param name="amounts">An array of integers specifying the quantity to use for each ingredient. Each element represents the amount for
    /// the ingredient at the corresponding index in the ingredients list.</param>
    /// <returns>The calculated score as a long integer, determined by multiplying the total capacity, durability, flavor, and
    /// texture values (each clamped to zero if negative).</returns>
    private static long CalculateScore(List<(string name, int capacity, int durability, int flavor, int texture, int calories)> ingredients, int[] amounts)
    {
        int capacity = 0;
        int durability = 0;
        int flavor = 0;
        int texture = 0;

        for (int i = 0; i < ingredients.Count; i++)
        {
            capacity += ingredients[i].capacity * amounts[i];
            durability += ingredients[i].durability * amounts[i];
            flavor += ingredients[i].flavor * amounts[i];
            texture += ingredients[i].texture * amounts[i];
        }

        capacity = Math.Max(0, capacity);
        durability = Math.Max(0, durability);
        flavor = Math.Max(0, flavor);
        texture = Math.Max(0, texture);

        return (long)capacity * durability * flavor * texture;
    }

    /// <summary>
    /// Calculates the total number of calories based on the provided ingredient list and their corresponding amounts.
    /// </summary>
    /// <remarks>The length of <paramref name="amounts"/> must match the number of ingredients in <paramref
    /// name="ingredients"/>. Each amount is multiplied by the calories value of its corresponding ingredient.</remarks>
    /// <param name="ingredients">A list of ingredient tuples, where each tuple contains the ingredient's name and its nutritional properties,
    /// including calories per unit.</param>
    /// <param name="amounts">An array of integers specifying the quantity to use for each ingredient. Each element corresponds to the
    /// ingredient at the same index in the <paramref name="ingredients"/> list.</param>
    /// <returns>The total number of calories resulting from the specified amounts of each ingredient.</returns>
    private static int CalculateCalories(List<(string name, int capacity, int durability, int flavor, int texture, int calories)> ingredients, int[] amounts)
    {
        int calories = 0;

        for (int i = 0; i < ingredients.Count; i++)
        {
            calories += ingredients[i].calories * amounts[i];
        }

        return calories;
    }
}