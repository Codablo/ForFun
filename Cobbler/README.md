This project was built using dotnet core 3.1

I've setup swagger documentation at the root of the project. 

I did basic CRUD for Plans and Allocations.  The client should be able to satisfy all the user stores.  However, there can be issues if we get large enough to require paging on the server side.

I was unable to have the time to implement the history user story.  I would also like to create an Error handler that would catch the custom Exceptions to product 400 Status Codes with a message. I also started to implement for various currency units and fractional currency for internationalization but quickly threw that away.  

It's also been a while since I've done Code First DB.  I was going to try to do an in-memory database but canned that so I could get more functionality done. 


   