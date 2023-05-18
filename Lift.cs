public class Lift
{
   
    public int CurrentFloor { get; set; }
    public int LastProcessedTime { get; set;}
    public int Capacity { get; }
    public List<Person> PeopleInLift { get; set; }
    public List<Person> CallQueue { get; set; }
    public int CurrentTime { get; set; }

    public Lift()
    {
        CurrentFloor = 1;
        LastProcessedTime = 5;
        Capacity = 8;
        CallQueue = new List<Person>();
        PeopleInLift = new List<Person>();
        CurrentTime = 0;
    }


//methods:

//adding call to call queue:
public void AddCallToQueue (Person person) 
{
    CallQueue.Add(person);
}
//remove call from call queue:
public void RemoveCallFromQueue (Person person)
{
    CallQueue.Remove(person);
}

//adding a person to the lift:
public void AddPersonToLift (Person person)
{
    PeopleInLift.Add(person);
}

//removing person from the lift:
public void RemovePersonFromLift (Person person)
{
    PeopleInLift.Remove(person);
}

//checking to see whether the lift can move or not
//checking if there are any pending calls to process or people to move
public bool CanMove() 
{
    return CallQueue.Count > 0 || PeopleInLift.Count > 0;
}

//getting the next destination floor number 
public int GetNextDestination() 
{
    if (PeopleInLift.Count > 0)
    {
        return PeopleInLift[0].DestinationFloor;
    } 
    else if (CallQueue.Count > 0)
    {
        return CallQueue[0].DestinationFloor;
    }
    else
    {
        return 1; // if there is noone in the lift and no calls in the queue then return lift to floor 1 
    }   
}

//mimicking the process of the lift moving from one floor to another

public void ProcessNextDestination() {
    //calling the above method and setting that to variable 'destination'
    int destination = GetNextDestination();
    //calculating how long lift will take to move floors
    //and incorporating the fact the lift takes 10 seconds to move from one floor to another
    //math.abs used to make sure the number is always +ve, regardless of whether lift is going up or down
    int timeToMove = Math.Abs(destination - CurrentFloor) * 10;
    //keeping track of the time elapsed since previous call
    LastProcessedTime += timeToMove;
    //updating the current floor to the destination floor
    CurrentFloor = destination;

    

    // Update the LastProcessedTime when a call is processed
    int previousProcessedTime = LastProcessedTime; // New variable to store the previous processed time
    foreach (var person in PeopleInLift)
    {
        if (person.CallTime <= previousProcessedTime) // Check if the person's call time is before the previous processed time
        {
            LastProcessedTime = person.CallTime; // Update the LastProcessedTime accordingly
            break; // Exit the loop after updating the LastProcessedTime
        }
    }
        
        CurrentTime++;

    //iterating through PeopleInLift list
    //if they are at their floor, they are added to a new list 'people to remove'
    // then everyone in that list is removed from the PeopleInLift list
    List<Person> peopleToRemove = new List<Person>();
    
    foreach (var person in PeopleInLift)
    {
        if (person.DestinationFloor == CurrentFloor)
        {
            peopleToRemove.Add(person);
        }
    }

    foreach (var person in peopleToRemove)
    {
        RemovePersonFromLift(person);
    }
    
    // checking if there is space in the lift and if there are calls to be processed
    if (PeopleInLift.Count < Capacity && CallQueue.Count > 0)
    {
        var boardingPerson = CallQueue[0];
        CallQueue.RemoveAt(0);
        AddPersonToLift(boardingPerson);
    }

    // if there are no people in the lift and there are still calls, the call is removed
    if (PeopleInLift.Count == 0 && CallQueue.Count > 0)
    {
        CallQueue.RemoveAt(0);
    }

    // if there are multiple calls in call queue:
    // method will repeatedly run over and over again until there are either no more people in lift 
    // and there are no more calls

}

    public string GetLiftStatus()
    {
    string peopleInLiftStr = string.Join(", ", PeopleInLift.Select(person => person.Id.ToString()));
    string callQueueStr = string.Join(", ", CallQueue.Select(person => person.ToString()));
    return ($"{CurrentTime}, {CurrentFloor}, {peopleInLiftStr}, {callQueueStr} ");
    }
    
  
 }

 