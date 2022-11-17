// See https://aka.ms/new-console-template for more information
using ProjectSample;

var animalsDictionary = new AnimalsDictionary();

Console.WriteLine($"{animalsDictionary.Add("animals", new List<string> { "Tiger", "Lion" })}");
Console.WriteLine($"{animalsDictionary.Add("animals", new List<string> { "Tiger", "Zebra" })}");
Console.WriteLine($"{animalsDictionary.Add("animals", new List<string> { "Lion", "Zebra" })}");

Console.WriteLine($"{ string.Join(",", animalsDictionary.Get("animals"))}");