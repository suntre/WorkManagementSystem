# WorkManagementSystem
Work Management System is REST api created with purpose to manage current and previous task 

# Technologies
Api has been created using .NET 6 with Entity Framework, based on MsSQL localdb. 
For manual testing Swagger and Postman.
Automation Tests using XUnit with AutoMoq, Fluent Assertions and Auto Fixture.
Authentication done with JWT.

# Working
Client without jwt can only log in just for "security" purpose. 
There are 3 different roles available: Programmer, Manager, Superior.
Each user can create, end task (only this one which he has create) and edit them (as in end task excluding Manager and Superior they can edit every single task).
Users can also display his task, or every single task.
Manager and Superior are allowed to display full list of tasks or delete tasks.

Each user can get full list of workers, get single worker, or display them by role. 
Manager and Superior can also Add worker to database, they can also change workers data. 
Only Superior has permision to delete worker. This process will delete every task performed by given user.
