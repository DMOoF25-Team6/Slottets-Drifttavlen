# ADR-001: Database Integration (MySQL)

**Status*: Accepted  

**Context:**
The Dashboard System requires persistent storage for citizens.
The system must be able to store and retrieve citizen data, and other relevant information efficiently.  
The system is built using ASP.NET Core in Visual Studio and a relational database is necessary to manage the relationships between different entities in the system. 


---
**Decision:**
The system will use **MySQL** as the database.  
The system will use **Entity Framework Core** with a custom `ApplicationDbContext` class to connect the application to the database. 
This setup allows the system to store and retrieve citizen data efficiently while keeping the code and database in sync.


---
**Consequences:**
The system will depend on a MySQL server being available to store and retrieve citizen data.  
Must keep the `ApplicationDbContext` models in sync with the database tables.  
Database changes will need to be managed through EF Core migrations.
The dashboard will be able to efficiently show the status of citizens at the hotel, but any downtime or misconfiguration of the database could affect the display of real-time information.


---
**Alternative Considered:**
Other options that were evaluated and why they were not chosen.
MySQL was chosen due to its widespread use and familiarity among the development team.
