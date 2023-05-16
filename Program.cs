internal class Program
{
    private static void Main(string[] args)
    {        
        //making a list person objects, called people, to store the csv data
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

        // testing various code bits: 

        // foreach (var person in people)
        // {
        //     Console.WriteLine($"{person.Id}, {person.FromFloor}, {person.DestinationFloor}, {person.CallTime}");
        // }
        
        // Lift lift = new Lift();
        // string liftStatus = lift.GetLiftStatus();
        // Console.WriteLine(liftStatus);    
        

        //running the LiftController:
        LiftController liftController = new LiftController(people);
        liftController.ProcessCalls();
    }
}