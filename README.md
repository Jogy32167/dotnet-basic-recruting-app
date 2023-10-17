# Prerequisites
This repository contains a code which you are supposed to work with. Please create a fork of this repo. As a result of your task please send us back a link to your github repository. While working with the code, please commit all changes to your repository, for us to check your progress.

- .NET 6.0 SDK installed.
- Visual Studio 2022/Visual Studio Code/JetBrains Rider

# Application
The application is a simple Web Api that is responsible for managing static sports data, such as teams, locations and so on.

## Challenge
The task will be divided into two parts:
1. Extending current solution with new required features.
2. Refactoring of current solution which is written in a non-testable way and doesn't follow proper development patterns.

### Part 1
1. Integrating database to store Locations and Teams inside it. We suggest using SqlLiteDb, because it doesn't require any installation in the machine and can be integrated and run right away.
    1. Code first approach should be used.
    2. As a result all the data that api returns should come from the database.


2. Adding validation to the LocationController and based on that return proper HTTP Codes.
    1. Location
         1. Name is required and it's maximum length should be 255.
         2. City is required and it's maximum length should be 55. 
         3. There can't be more than one location with the same name.
### Part 2
1. As mentioned above, controllers and repositories aren't properly designed and don't follow general coding patterns such as SOLID, DRY, YAGNI. This part is all about you proposing how the code can be refactored in order to make it testable and more correct.
2. Writing unit tests for that would be additional benefit.

# How to run 
You have two alternatives:
1. Build and run solution in your IDE.
2. Run `dotnet run --project .\MatchDataManager.Api` from project root directory.
