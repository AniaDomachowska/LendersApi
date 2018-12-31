An example implementation of OData REST Api.

# Add new person:
    POST /Lenders/odata/people/addPerson
    Host: localhost
    {
	    "model": {
		    "FirstName" : "John",
		    "LastName" : "Doe"
	    }
    }
	
# Create loan:
    POST /Lenders/odata/loans/addLoan
    Host: localhost
    Content-Type: application/json
    {
	    "model" : {
		    "Amount" : 100.0,
		    "BorrowerId" : 28,
		    "LenderId" : 27
	    }
    }
	
# Pay loan:
    POST /Lenders/odata/loans/13/payLoan
    Host: localhost
    Content-Type: application/json
    {
	    "Amount" : 12.4
    }
	
# Get person's loans:
    GET /Lenders/odata/people(28)/Loans
    Host: localhost

# Get people list:
    GET /Lenders/odata/people
    Host: localhost
	
Postman request collection for testing is available in postman.json file.
	
Used technologies:
- Entity Framework Core + model first approach + fluent model configuration.
- Odata controllers.
- Nlog structured logging.
- Unit tests with NUnit and fluent assertions.
- Automapper for mapping between entity framework models and API Dtos.