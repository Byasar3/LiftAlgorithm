using System;
public class LiftController 
{
    private Lift lift;
    private List<Person> peopleWaiting;

    public LiftController()
    {
        lift = new Lift();
        peopleWaiting = new List<Person>();
    }

    //methods:

    public void ProcessCalls()
    {
        //making a new list of strings to store the output information
        List<string> output = new List<string>();

        //calling can move method: if there are calls or people in lift
        while (lift.CanMove())
        {
            // variable int to store the time 
            int time = lift.LastProcessedTime;
            lift.ProcessNextDestination();

            // creating a string to store information
            string liftStatus = $"{time}, {lift.CurrentFloor}, {lift.PeopleInLift}, {lift.CallQueue}";
            output.Add(liftStatus);
            Console.Write(output);
        }
        
    
    }
}