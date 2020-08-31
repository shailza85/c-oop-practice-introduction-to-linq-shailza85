using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Practice_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> intList = SeedIntList();
            List<string> stringList = SeedStringList();
            List<List<int>> nestedList = SeedNestedList();
            List<Person> personList = SeedPersonList();


            // Operating on intList:

            /*
             *    static List<int> SeedIntList()
                    {
                        return new List<int>() { 3, 10, 2, 3, 12, 7, 12, 12, 24, 42, 101, 3, 25, 42, 60, 72, 5, 2, 100 };
                    }
             * */
            Console.WriteLine($"The sum of the numbers that are greater than or equal to 25: {intList.Where(x => x >= 25).Sum()}.");
            Console.WriteLine($"The lowest number that is divisible by both 2 and 3: {intList.Where(x => x % 2 == 0 && x % 3 == 0).Min()}.");
            Console.WriteLine($"The average of the numbers that are single digits: {intList.Where(x => x >= -9 && x <= 9).Average()}.");
            // If we wanted to include negatives, we could say string[0] == '2' || (string[0] == '-' && string[1] == '2').
            Console.WriteLine($"The number of distinct numbers starting with 2: {intList.Where(x => x.ToString()[0] == '2').Distinct().Count()}.");

            // Operating on stringList:

            /*
             * static List<string> SeedStringList()
                {
                    return new List<string>() { "Yes", "no", "yes", "Yes ", "no", "maybe", "NO", " no", " maYbe ", "definitely" };
                } 3y 4no maybe 2 def 1
             * */

            Console.WriteLine($"The number of distinct strings (trimmed and case insensitive): {stringList.Select(x => x.Trim().ToLower()).Distinct().Count()}.");
            // x operation (the first one): Select the trimmed words themselves and compare the lengths thereof to the y operation
            // y operation (the inner one): Select the max of the length of the trimmed words
            // Other solutions:
            // Bibu's (slightly modified) using the arguments of Max(): stringList.Where(x => x.Length == stringList.Max(y=>y.ToString().Trim().Length))
            // Andrew's using OrderBy() and First(): stringList.Select(x => x.Trim()).OrderByDescending(x => x.Length).First()
            // Shivani's using Aggregate(): stringList.Aggregate("", (x, y) => x.Length > y.Length ? x : y).Trim()
            // First is required because our last operator is a Where(), which returns a 'list' (IEnumerable).
            Console.WriteLine($"The longest string (trimmed): {stringList.Select(x => x.Trim()).Where(x => x.Length == stringList.Select(y => y.Trim().Length).Max()).First()}.");
            // If you don't like indexing, you can use stringList.OrderByDescending(x => x).ElementAt(1).
            // OrderByDescending is the same as doing OrderBy and then Reverse.
            Console.WriteLine($"The second string when ordered in reverse alphabetical order: {stringList.OrderByDescending(x => x).ToList()[1]}.");

            // Operating on nestedList: 

            /*
             * static List<List<int>> SeedNestedList()
        {
            return new List<List<int>>()
            {
                new List<int>() { 42, 12, 3, 7 }, odd-3/3
                new List<int>() { 12 },
                new List<int>() { 37, 12, 8, 12, 41, 63, 17, 2, 6, 3 }, odd-37,41,63/3,17,3/3
                new List<int>() { 90, 100, 12, 7, 17 }, odd-7,17
                new List<int>() { 4 * 2, 13 / 3, 90 / 12, 60 % 17, 3 * 3 * 2 } odd-7.5
            odd-3,7,37,41,63,17,7.5
            }; 
        }
             */
            // Operating on nestedList: 
            // x represents each child list, and the Select() returns a new list of all of the Max()'s, then we can Max() those.
            // After the select we have { 42, 12, 63, 100, 18 } and then the last Max() will take the max of those.
            // Osase's Using SelectMany(): nestedList.SelectMany(x => x).Max()
            Console.WriteLine($"The overall largest number across all lists: {nestedList.Select(x => x.Max()).Max()}.");
            Console.WriteLine($"The number of nested lists: {nestedList.Count()}.");
            // Select will collapse into { 4, 1, 10, 5, 5 } and then we can take the Min() which will be the count of the shortest list.
            Console.WriteLine($"The number of items in the shortest nested list: {nestedList.Select(x => x.Count).Min()}.");
            // The subquery will bring it down to the single longest nested list, we'll have to First() to get our value (the list), and then we can average.
            Console.WriteLine($"The average of items in the longest nested list: {nestedList.Where(x => x.Count == nestedList.Select(y => y.Count).Max()).First().Average()}.");
            // SelectMany() will grab all of the child items and collapse them into one big list. 
            Console.WriteLine($"The number of distinct even items across all lists: {nestedList.SelectMany(x => x).Where(x => x % 2 == 0).Distinct().Count()}.");
            // SelectMany() to get all of the child items, Where() to get the odd items (the left side of the &&) that are divisible by 3 or 5 (the right side of the &&), then Distinct() and Average().
            Console.WriteLine($"The average of distinct odd items divisible by either 3 or 5 across all lists: {nestedList.SelectMany(x => x).Where(x => x % 2 == 1 && (x % 3 == 0 || x % 5 == 0)).Distinct().Average()}.");

            // Operating on personList: 
            Console.WriteLine($"The number of females in the list: {personList.Where(x => x.Gender == Person.GenderValue.Female).Count()}.");
            Console.WriteLine($"The average number of characters in first names: {personList.Select(x => x.FirstName.Length).Average()}.");
            // Where() the date of birth is equal to the Max() (most recent) date of birth from the list.
            // Subquery method is arguably better because in order to do the concatenation we'd need to run the query twice (unless we store the object ref in a variable).
            Console.WriteLine($"The full name of the youngest person: {personList.Where(x => x.DateOfBirth == personList.Select(y => y.DateOfBirth).Max()).Select(x => x.FirstName + " " + x.LastName).First()}.");
            // personList.Where(x => x.LastName.Length == personList.Select(y => y.LastName.Length).Max()).Select(x => x.FirstName).First()
            Console.WriteLine($"The first name of the person with the longest last name: {personList.OrderByDescending(x => x.LastName.Length).First().FirstName}.");
            //personList.Where(x => x.DateOfBirth == personList.Select(y => y.DateOfBirth).Min()).Select(x => x.Gender).First()
            Console.WriteLine($"The gender of the oldest person: {personList.OrderBy(x => x.DateOfBirth).First().Gender}.");


        }

        static List<Person> SeedPersonList()
        {
            return new List<Person>()
            {
                new Person()
                {
                    FirstName = "Bob",
                    LastName = "Jones",
                    DateOfBirth = new DateTime(1992, 02, 12),
                    Gender = Person.GenderValue.Male
                },
                new Person()
                {
                    FirstName = "Sue",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1983, 6, 18),
                    Gender = Person.GenderValue.Female
                },
                new Person()
                {
                    FirstName = "Tommy",
                    LastName = "Lee",
                    DateOfBirth = new DateTime(1962, 10, 3),
                    Gender = Person.GenderValue.Male
                },
                new Person()
                {
                    FirstName = "Bob",
                    LastName = "Jones",
                    DateOfBirth = new DateTime(1992, 2, 12),
                    Gender = Person.GenderValue.Male
                },
                new Person()
                {
                    FirstName = "Barney",
                    LastName = "The Dinosaur",
                    DateOfBirth = new DateTime(1992, 4, 06),
                    Gender = Person.GenderValue.Male
                },
                new Person()
                {
                    FirstName = "Jane",
                    LastName = "Barber",
                    DateOfBirth = new DateTime(1997, 1, 1),
                    Gender = Person.GenderValue.Female
                },
                new Person()
                {
                    FirstName = "Avery",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(1985, 7, 8),
                    Gender = Person.GenderValue.NotStated
                },
                new Person()
                {
                    FirstName = "Emmett",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(1985, 6, 3),
                    Gender = Person.GenderValue.Male
                },
                new Person()
                {
                    FirstName = "Bob",
                    LastName = "Jones",
                    DateOfBirth = new DateTime(1992, 02, 12),
                    Gender = Person.GenderValue.Male
                },
                new Person()
                {
                    FirstName = "Hermione",
                    LastName = "Granger",
                    DateOfBirth = new DateTime(2001, 11, 16),
                    Gender = Person.GenderValue.Female
                }
            };
        }

        static List<List<int>> SeedNestedList()
        {
            return new List<List<int>>()
            {
                new List<int>() { 42, 12, 3, 7 },
                new List<int>() { 12 },
                new List<int>() { 37, 12, 8, 12, 41, 63, 17, 2, 6, 3 },
                new List<int>() { 90, 100, 12, 7, 17 },
                new List<int>() { 4 * 2, 13 / 3, 90 / 12, 60 % 17, 3 * 3 * 2 }
            };
        }

        static List<string> SeedStringList()
        {
            return new List<string>() { "Yes", "no", "yes", "Yes ", "no", "maybe", "NO", " no", " maYbe ", "definitely" };
        }

        static List<int> SeedIntList()
        {
            return new List<int>() { 3, 10, 2, 3, 12, 7, 12, 12, 24, 42, 101, 3, 25, 42, 60, 72, 5, 2, 100 };
        }
    }
}
