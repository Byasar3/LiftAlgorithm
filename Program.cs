
using Microsoft.VisualBasic.FileIO;

string filePath = "/Users/Berna/Documents/LiftAlgorithm/DataInput.csv";

using (TextFieldParser parser = new TextFieldParser(filePath)) 
{
    // break up csv file with ","
    parser.Delimiters = new string[] { "," };

    Console.WriteLine(parser);

    // read the csv file row by row:
    while (!parser.EndOfData)
    {
        // added in '?' as it was failing to run incase the csv file was null
        string[]? fields = parser.ReadFields();

        Console.WriteLine(fields[0]);
        Console.WriteLine(fields[1]);
        Console.WriteLine(fields[2]);
        Console.WriteLine(fields[3]);
    }
}



