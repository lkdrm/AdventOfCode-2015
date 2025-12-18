using AdventOfCode2015.Tools;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 4 of the coding challenge.
/// </summary>
public static class Day4
{
    private const string Part1Md5HashPrefix = "00000";
    private const string Part2Md5HashPrefix = "000000";

    /// <summary>
    /// Finds the smallest positive integer that, when appended to the specified input string and hashed with MD5,
    /// produces a hash that starts with the required prefix for Part 1 of the puzzle.
    /// </summary>
    /// <param name="input">The base input string to which the secret key will be appended before hashing.</param>
    /// <returns>A string representation of the smallest positive integer that satisfies the hash prefix condition.</returns>
    public static string SolvePart1(string input)
    {
        TimerExtension.Start();
        int secretKey = 1;
        while (true)
        {
            string inputWithSecretKey = input + secretKey.ToString();
            string md5Hash = ComputeMd5Hash(inputWithSecretKey);

            if (md5Hash.StartsWith(Part1Md5HashPrefix))
            {
                break;
            }
            secretKey++;
        }
        return secretKey.ToString();
    }

    /// <summary>
    /// Computes the MD5 hash of the specified input string and returns the result as a lowercase hexadecimal string.
    /// </summary>
    /// <param name="inputWithSecretKey">The input string to hash. This value is typically a concatenation of the original data and a secret key.</param>
    /// <returns>A 32-character lowercase hexadecimal string representing the MD5 hash of the input.</returns>
    private static string ComputeMd5Hash(string inputWithSecretKey) => string.Concat(MD5.HashData(Encoding.UTF8.GetBytes(inputWithSecretKey)).Select(b => b.ToString("x2")));

    /// <summary>
    /// Finds the smallest positive integer that, when appended to the specified input string and hashed with MD5,
    /// produces a hash that starts with the required prefix for part 2 of the puzzle.
    /// </summary>
    /// <param name="input">The base input string to which the secret key will be appended before hashing.</param>
    /// <returns>A string representation of the smallest positive integer that satisfies the hash prefix condition.</returns>
    public static string SolvePart2(string input)
    {
        TimerExtension.Start();
        int secretKey = 1;
        while (true)
        {
            string inputWithSecretKey = input + secretKey.ToString();
            string md5Hash = ComputeMd5Hash(inputWithSecretKey);
            
            if (md5Hash.StartsWith(Part2Md5HashPrefix))
            {
                break;
            }
            secretKey++;
        }
        return secretKey.ToString();
    }
}
