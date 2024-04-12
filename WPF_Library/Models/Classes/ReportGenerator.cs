using System;
using System.Collections.Generic;
using System.Linq;

public class ReportGenerator<T>
{
    public List<T> Data { get; set; }

    public ReportGenerator(List<T> data)
    {
        Data = data;
    }

    public void GenerateSummaryReport()
    {
        /*
        int sum = Data.Sum();
        string shortestString = Data.Min();
        decimal average = Data.Average();
        double maximum = Data.Max();
        
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Shortest String: {shortestString}");
            Console.WriteLine($"Average: {average}");
            Console.WriteLine($"Maximum: {maximum}");
        */

    }
}