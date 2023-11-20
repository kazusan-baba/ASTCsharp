using System;

class Sample
{
    int i = 0;
    static void Main()
    {
        Console.WriteLine("Hello, Roslyn!");
    }

    int test()
    {
        int i = 1+1;
        return i;
    }
}