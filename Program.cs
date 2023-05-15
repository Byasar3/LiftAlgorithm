/*
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
*/

internal class Program
{
    private static void Main(string[] args)
    {
         // List<Person> personsFromData = CsvParser.ParsePerson("/Users/Berna/Documents/LiftAlgorithm/DataInput.csv");
        
        //making a list of people to store the csv data
        List<Person> people = new List<Person>();

        //file path:
        string filePath = "/Users/Berna/Documents/LiftAlgorithm/Data/DataInput.csv";

        using ( var data = new StreamReader(filePath))
        {
            //skipping the header line
            data.ReadLine();

            //parse remaining lines and make person object
            while(!data.EndOfStream) //if it is not the end of the file, do below:
            {
                var line = data.ReadLine(); //read each line and store as 'line'
                var valuesOfColumns = line.Split(','); //split at each ',' and store as 'valuesOfColumns'

                var person = new Person
                {
                    Id = int.Parse(valuesOfColumns[0]),
                    FromFloor = int.Parse(valuesOfColumns[1]),
                    DestinationFloor = int.Parse(valuesOfColumns[2]),
                    CallTime = int.Parse(valuesOfColumns[3])
                };

                people.Add(person);
            }
        }
        foreach (var person in people){
            Console.WriteLine($"{person.Id}, {person.FromFloor}, {person.DestinationFloor}, {person.CallTime}");
        }
        
    }
}