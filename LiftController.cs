
public class LiftController 
{
    private Lift lift;
    public List<Person> PeopleWaiting { get; }
    private List<string> output;

    public LiftController(List<Person> people)
    {
        lift = new Lift();
        PeopleWaiting = new List<Person>(people);
        output = new List<string>();
    }

    //methods:

    public void ProcessCalls()
    {
        // // Populate peopleWaiting list
        // foreach (var person in PeopleWaiting)
        // {
        //     PeopleWaiting.Add(person);
        // }

        //making a copy of the PeopleWaiting list to be able to modify it 
        var peopleToProcess = new List<Person>(PeopleWaiting);

        //calling can move method: if there are calls or people in lift
        while (peopleToProcess.Count > 0)
        {
            // Check if there are people waiting and their call time has arrived
            var callsToProcess = peopleToProcess.Where(p => p.CallTime <= lift.LastProcessedTime).ToList();

            foreach (var call in callsToProcess)
            {
                lift.AddCallToQueue(call);
                // then remove the processed calls from peopleToProcess List
                peopleToProcess.Remove(call);
                
            } 

            // checking if the lift can move
            if (lift.CanMove())
            {
                // variable int to store the time 
                int time = lift.LastProcessedTime;
                lift.ProcessNextDestination();
                Console.WriteLine($"Processing time: {time}");
                Console.WriteLine($"Current floor: {lift.CurrentFloor}");            

                // call GetLiftStatus method and add that to output
                string liftStatus = lift.GetLiftStatus();
                Console.WriteLine($"Lift status: {liftStatus}");

                output.Add(liftStatus);
                Console.WriteLine($"Output Count: {output.Count}");                
            }

        }

        Console.WriteLine($"Output Count: {output.Count}");
        WriteOutputToCSV();
    }

    private void WriteOutputToCSV()
    {
        string csvPath = "DataOutput.csv";

        using (var outputData = new StreamWriter(csvPath))
        {
            //writing header:
            outputData.WriteLine("Time, Current Floor, People In Lift, Call Queue");

            //writing the output lines:
            foreach (var line in output)
            {
                outputData.WriteLine(line);
            }
        }
        Console.WriteLine($"Output has been written to {csvPath}.");
    }
}