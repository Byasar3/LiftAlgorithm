
public class LiftController 
{
    private Lift lift;
    
    public LiftController(List<Person> people)
    {
        lift = new Lift(people);
    }

    //methods:

    public void ProcessCalls()
    {
        // need to have a list of peoplewaiting to be picked up by lift
        while (lift.PeopleWaiting.Count > 0 || lift.PeopleInLift.Count >0)
        {
            // need to figure out where lift is going enxt 
            lift.GetNextDestination();

            // once we know what floor we are going to, which direction?
            lift.WhichDirection();
        } 
    }
}