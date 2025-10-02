# TaskTracker CLI

  A simple task manager in terminal, made in .NET 8.
  Allows you to add, update, erease, list & change the state of tasks saved in a local JSON file


## Instalation

### Clone the repository 

```
  git clone https://github.com/BenjaminTardivo/TaskTracker.git
  cd TaskTracker
 ```
### Package the tool

```
  dotnet pack -c Release
```

### Global Install

```
dotnet tool install --global --add-source ./bin/Release TaskTracker
```

## Use

```
#[List all commands]
tasktracker help

#[Add a new Task]
tasktracker add "buy bread"

#[Update a task]
tasktracker update 1 "buy bread and butter"

#[Delete a task]
tasktracker delete 1

#[Change status of a task]
tasktracker mark-in-progress 2
tasktracker mark-done 2

#[List tasks]
tasktracker list

#[List by status]
tasktracker list todo
tasktracker list in-progress
tasktracker list done
```

## Dependencies
[.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
