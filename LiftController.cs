
public class LiftController 
{
    private Lift lift;
    private Person person;
   
    

    public LiftController(List<Person> people)
    {
        lift = new Lift(people);
        person = new Person();
    }

    //methods:

    public void ProcessCalls()
    {
        // need to have a list of peoplewaiting to be picked up by lift
        
        // need to figure out where lift is going enxt 
        lift.GetNextDestination();


        // once we know what floor we are going to, which direction?
        lift.WhichDirection();
        
    }
}