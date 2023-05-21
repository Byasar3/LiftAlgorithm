<h1 align="center">Lift Algorithm </h1>

## Project Outline

This project is a prototype lift algorithm for a fictional building of 10 floors and 1 lift. The prototype assumes it takes 10 seconds for the lift to move from one floor to another and the lift has a max capacity of 8 people.

It takes in a CSV file of data which includes:
- The ID of the person calling the lift 
- Which floor they’re calling from
- Which floor they want to go to
- The time at which they call the lift

and outputs in a CSV file data that includes:
- The current time
- Which people are in the lift
- Which floor the lift is in
- Which floors are in the lift’s call queue, and in what order

The CSV input file is named `DataInput.csv` and is in the `Data` folder.

The CSV outpit file is named `DataOutput.csv`.

## Installing and running the prototype

These steps will be for running the project in VS Code.

Make sure you have set up .NET in VS Code. The version used for this project was .NET 7.0, so you will need to update your current version to .NET 7.0. (can be found at https://dotnet.microsoft.com/en-us/download)

You can also install the C# language support through the extensions tab in VS Code.

Clone the project, which includes the data input file
```
git clone git@github.com:Byasar3/LiftAlgorithm.git
```

Navigate to the project directory
```
cd LiftAlgorithm
```
As the data output is already saved, a new file will not be made. Any changes made to the code that changes the data output will be shown once the app is run. You can run the app using this command:
```
dotnet run
```
# Tech Stack
Project was built using C# and uses:

- VS Code
- .NET 7.0
- C# extension for VS Code (powered by omnisharp)


