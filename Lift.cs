public class Lift
{
    public int CurrentFloor { get; set; }
    public int CurrentTime { get; set;}
    public int Capacity { get; }
    public List<Person> PeopleInLift { get; set; }
    public List<Person> PeopleWaiting { get; set; }
    public Person person;
    public List<string> output;



    public Lift(List<Person> people)
    {
        CurrentFloor = 1;
        CurrentTime = 0;
        Capacity = 8;
        PeopleWaiting = new List<Person>(people);
        PeopleInLift = new List<Person>();
        output = new List<string>();
        person = new Person();
    }


//methods:

//remove person from call queue/waiting list
public void RemoveFromPeopleWaiting(Person person)
{
    PeopleWaiting.Remove(person);
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
    return PeopleWaiting.Count > 0 || PeopleInLift.Count > 0;
}

//getting the next destination floor number 
public int GetNextDestination() 
{
    if (PeopleInLift.Count > 0)
    {
        return PeopleInLift[0].DestinationFloor;
    } 
    else if (PeopleWaiting.Count > 0)
    {
        return PeopleWaiting[0].FromFloor;
    }
    else
    {
        return 1; // if there is noone in the lift and no calls in the queue then return lift to floor 1 
    }   
}


// deciding if the lift is moving up or down based on call and people in lift:

public void WhichDirection()
{


    if (CurrentFloor < PeopleWaiting[0].FromFloor)
    {
        LiftMovingUp();
    } 
}

public void LiftMovingUp()
{
    while (CurrentFloor < PeopleWaiting[0].FromFloor || CurrentFloor < PeopleInLift[0].DestinationFloor)
    {
        WhenOnNextFloorGoingUp();
    }

}

public void WhenOnNextFloorGoingUp()
{
    // on each floor we want to log: the time, current floor, people in the lift and where the lift is going(the destinations of people in the lift)
    
    CurrentFloor++;
    CurrentTime = CurrentTime + 10; // time takem to move to floor

    // check if anyone is waiting on floor
    var peopleWaitingOnFloor = PeopleWaiting.Where(person => person.FromFloor == CurrentFloor && person.CallTime <= CurrentTime).ToList();

    foreach (var person in peopleWaitingOnFloor)
    {
        AddPersonToLift(person);
        RemoveFromPeopleWaiting(PeopleWaiting[0]);
    }

    // check if anyone is leaving the lift
    var peopleLeavingTheLift = PeopleInLift.Where(person => person.DestinationFloor == CurrentFloor).ToList();
    foreach (var person in peopleLeavingTheLift)
    {
        RemovePersonFromLift(person);
    }

    string liftStatus = GetLiftStatus();
    output.Add(liftStatus);
    WriteOutputToCSV();

}

public void LiftMovingDown()
{
    // 
}

public string GetLiftStatus()
{
    List<string> peopleInLiftList = new List<string>();
    List<string> destinationList = new List<string>();

    foreach (Person personInLift in PeopleInLift)
    {
        peopleInLiftList.Add(personInLift.Id.ToString());
        destinationList.Add(personInLift.DestinationFloor.ToString());
    }

    string peopleInLiftStr = string.Join(", ", peopleInLiftList);
    string destinationStr = string.Join(", ", destinationList);

    Console.WriteLine($"{CurrentTime}, {CurrentFloor}, {peopleInLiftStr}, {destinationStr} ");

    return ($"{CurrentTime}, {CurrentFloor}, {peopleInLiftStr}, {destinationStr} ");
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



public void ProcessNextDestination() 
{

    //calling the above method and setting that to variable 'destination'
    int destination = GetNextDestination();

    //calculating how long lift will take to move floors
    //and incorporating the fact the lift takes 10 seconds to move from one floor to another
    //math.abs used to make sure the number is always +ve, regardless of whether lift is going up or down
    int timeToMove = Math.Abs(destination - CurrentFloor) * 10;
    //keeping track of the time elapsed since previous call
    CurrentTime += timeToMove;
    //updating the current floor to the destination floor
    CurrentFloor = destination;

    

    // Update the CurrentTime when a call is processed
    int previousProcessedTime = CurrentTime; // New variable to store the previous processed time
    foreach (var person in PeopleInLift)
    {
        if (person.CallTime <= previousProcessedTime) // Check if the person's call time is before the previous processed time
        {
            CurrentTime = person.CallTime; // Update the CurrentTime accordingly
            break; // Exit the loop after updating the CurrentTime
        }
    }
        


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
    if (PeopleInLift.Count < Capacity && PeopleWaiting.Count > 0)
    {
        var boardingPerson = PeopleWaiting[0];
        PeopleWaiting.RemoveAt(0);
        AddPersonToLift(boardingPerson);
    }

    // if there are no people in the lift and there are still calls, the call is removed
    if (PeopleInLift.Count == 0 && PeopleWaiting.Count > 0)
    {
        PeopleWaiting.RemoveAt(0);
    }

    // if there are multiple calls in call queue:
    // method will repeatedly run over and over again until there are either no more people in lift 
    // and there are no more calls

}
}

 