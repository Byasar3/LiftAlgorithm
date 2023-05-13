
public class CsvParser 
{
    public static List<Person> ParsePerson (string filePath)
    {
        List<Person> persons = new List<Person>();

        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            Person person = new Person()
            {
                Id = int.Parse(values[0]),
                FromFloor = int.Parse(values[1]),
                DestinationFloor = int.Parse(values[2]),
                CallTime = int.Parse(values[3])
            };

            persons.Add(person);
        }

        return persons;
    
    }
}

