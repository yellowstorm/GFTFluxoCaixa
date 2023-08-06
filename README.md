# GFTFluxoCaixa

This is a challenge made by GFT.

Objective: A merchant needs to control his daily cash flow with entries (debits and credits), he also needs a report that provides the consolidated daily balance.

Solution:
- Rest Api
- Tecnology: .NET
- Librarys:
    * Swagger
    * Dapper
    * AutoMapper
    * Sqlite
 
<b>Softwares that you need:</b>
- Visual Studio / Visual Studio Code
- .NET SDK (3.1)
- Postman
   
<p float="left">

 <img src="doc/diagrama.png" width="50%" />

</p>


## <b>How to run</b>
1. Clone this repository
2. Start the api by running dotnet run from the command line in the project root folder

2.1. Example

2.1.1. Visual Studio
   Open solution and click on run button(GFTFluxoCaixa.Api)
   <img src="doc/visual_studio.png" width="100%" />

2.1.2. Visual Studio Code
   Open solution on Visual Studio Code, open Terminal and run dotnet run like this:
   <img src="doc/visual_studio_code.png" width="100%" />


## <b>How to test</b>

When you clone this repository there is a directory named "doc". There is a json file "GFTFluxoCaixa.postman_collection.json" with examples to how to test the API`s endpoints.


## <b>Healthcheck</b>

You can see the healthy checking the dashboard page.
http://localhost:5001/dashboard
![image](https://github.com/yellowstorm/GFTFluxoCaixa/assets/11304672/01e74332-b63a-4efb-9b4f-2ad820896f96)



