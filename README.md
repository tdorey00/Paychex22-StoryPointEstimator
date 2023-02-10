
# ScrumSpace

ScrumSpace (StoryPointEstimator) is a .NET web application built to aid with the Agile Development Process. It is currently connected to an SQL Server and hosted on Microsoft Azure.

One of the main advantages of ScrumSpace is the ability to vote on story points with your colleagues during Scrum Meetings. It all starts with creating a room. Once your room has been created, other users can simply select it from a drop down menu and join. There are two different types of users in rooms. Regular users who have the ability to vote and facilitators that have admin functionality such as clearing votes, removing users, deleting the room, etc.

The different voting categories that are in ScrumSpace are:
* Fibonacci Sequence
* Fist of Five
* T-Shirt Sizing
* Custom Voting

ScrumSpace has Timer and Stopwatch functionality which can also be very useful in Scrum Meetings.

## Screenshots

![screenshot2](https://user-images.githubusercontent.com/90643765/218004203-85299005-7028-409f-8fb4-27fa5b660b4c.png)

![screenshot3](https://user-images.githubusercontent.com/90643765/218004067-59d0cced-870b-4abe-b9f2-80a19bfda2e2.png)

## Documentation

[GitBook Link](https://scrumspace.gitbook.io/scrumspace-documentation)

## Deployment

When the Visual Studio solution is first opened after handoff, you will notice the SQL Project (Paychex_StoryPointDB) is not included in the .sln file and does not show up in the solution explorer for visual studio. This is because we deployed to Azure through GitHub which could not build SQL projects. If the SQL project is required simply re-add the .sqlproj file to the solution and the SQL project should be added.

When connecting a new database to the application, open the appsettings.json file located in the StoryPointEstimatorBlazorApp project and change the “Default” area of the “ConnectionStrings” section to the database connection string. Example below:

![pasted image 0](https://user-images.githubusercontent.com/90643765/218002780-b5db9a8c-927f-4de4-b4f9-affb8008d8ea.png)

## Project Diagram

![Picture2](https://user-images.githubusercontent.com/90643765/217997199-2c6405e8-c27d-48dd-999e-37b23fae7c1f.png)

## Authors
  - **Tyler Dorey** -
    [tdorey00](https://github.com/tdorey00)
  - **Rick Corsi** -
    [rickcorsi](https://github.com/rickcorsi)
  - **Peyton Miller** -
    [pmill99](https://github.com/pmill99)
  - **Derek Zeplowitz** -
    [Derk222](https://github.com/Derk222)
