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

// remove person from call queue/waiting list
public void RemoveFromPeopleWaiting(Person person)
{
    PeopleWaiting.Remove(person);
}

// adding a person to the lift:
public void AddPersonToLift (Person person)
{
    PeopleInLift.Add(person);
}

// removing person from the lift:
public void RemovePersonFromLift (Person person)
{
    PeopleInLift.Remove(person);
}


// checking to see whether the lift can move or not
// checking if there are any pending calls to process or people to move
public bool CanMove() 
{
    return PeopleWaiting.Count > 0 || PeopleInLift.Count > 0;
}

// getting the next destination floor number 
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
    if (CurrentFloor < PeopleWaiting[0].FromFloor || CurrentFloor < PeopleInLift[0].DestinationFloor)
    {
        LiftMovingUp();
    } 
    else if (CurrentFloor == 10 || CurrentFloor > PeopleWaiting[0].FromFloor || CurrentFloor > PeopleInLift[0].DestinationFloor)
    {
        LiftMovingDown();
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
    while (CurrentFloor == 10 || CurrentFloor > PeopleWaiting[0].FromFloor || CurrentFloor > PeopleInLift[0].DestinationFloor)
    {
        WhenOnNextFloorGoingDown();
    }
}

public void WhenOnNextFloorGoingDown()
{
    CurrentFloor--;
    CurrentTime = CurrentTime + 10;

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

 // on each floor we want to log: the time, current floor, people in the lift and where the lift is going(the destinations of people in the lift)
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

    return ($"{CurrentTime}| {CurrentFloor}| {peopleInLiftStr}| {destinationStr} ");
}

private void WriteOutputToCSV()
{
    string csvPath = "DataOutput.csv";
    
    using (var outputData = new StreamWriter(csvPath))
    {
        // writing header:
        outputData.WriteLine("Time| Current Floor| People In Lift| Call Queue");

        // writing the output lines:
        foreach (var line in output)
        {
            outputData.WriteLine(line);
        }
    }
    Console.WriteLine($"Output has been written to {csvPath}.");
}

}

 