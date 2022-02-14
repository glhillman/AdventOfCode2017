// See https://aka.ms/new-console-template for more information
using Day21;

DayClass day = new DayClass();

var watch = new System.Diagnostics.Stopwatch();
watch.Start();


day.Part1And2();

watch.Stop();
Console.WriteLine("Execution Time: {0} ms", watch.ElapsedMilliseconds);

Console.Write("Press Enter to continue...");
Console.ReadLine();
