# Running instructions
Step 1. Setup
1.0 Add a new inbound rule in Windows Firewall for TCP, port 1433, assuming you run this on a Windows machine.
1.1 Find your local IP address by runing ipconfig and update the Connection string in appsettings.json with your ip
1.2 Ensure Docker is installed and configured for Linux containers

Step 2. In root folder of solution, run : "docker-compose up"

Step 3. Run the application (dotnet run in API folder or F5 in visual studio) 
- wasn't able to properly expose the API container, we also need to run the app.

# Notes
- For applying a migration to the database, just go to the OrderManagementAPI folder and run "dotnet-ef database update"
- Event sourcing was added, since the use of a message bus and events. 
- Product implementation is not fully completed.
- Integration tests do not fully respect TestPriority order.

#Requirements
Our company is migration from monolith architecture to microservices architecture. Now we must
build new application using some guidelines like ASP.NET core, Docker, CQRS and others new
technologies and methodologies.
Based on that we have to build a new microservices we will build is the Orders Management. The
Product Team create some specifications about what the new microservice must do:
- Create a new order
- Update the order delivery address
- Update the order items
- Cancel an order
- Retrieve a single order
- Retrieve a paginated list of orders
* Bonus point: manage the inventory stock for products

The architecture team defined some requirements to apply for all new microservices:
• .NET 6
• Docker
• SQL Server 2019 (database in memory should be fine)
• Unit Tests
• Swagger Documentation
* Bonus point: Any authentication method e.g., Firebase, okta, auth0 or another
* Bonus point 2: Integration Tests

Additional requirements:
- The solution has to build and execute without errors
- The solution has to be production ready (deployable)

Additional notes
- A product can be just a ProductId: GUID and ProductName: String
- There is no expectation of validating the product, this would be outside the scope of this assignment
- Feel free to use whatever persistence layer you see fit
- Async communication, usually this is done over a messaging bus, but it can be simplified using an
inmemory approach
- The application will be deployed on Docker, so create a README file with step-by-step to deploy and
run

#Inspiration : 
https://www.twilio.com/blog/containerize-your-aspdotnet-core-application-and-sql-server-with-docker
https://github.com/EduardoPires/EquinoxProject
https://github.com/vnathv/Docker-DotNetCore-SqlServer