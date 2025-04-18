# Fibonacci API – Internship Test Task

This project is a C# Web API built with .NET 8 that generates a subsequence of Fibonacci numbers. It demonstrates multi-threading, async programming, caching, memory and timeout constraints.

Features Implemented
- Dependency Injection
- Async Programming
- Multi-threading
- Exception Handling
- JSON Serialization
- Generics & Extension Methods
- Swagger UI for testing

# Endpoint Description

GET /Fibonacci

Generates a subsequence of Fibonacci numbers between the provided indexes with artificial delay and resource constraints.

Parameters:

startIndex: index of the first Fibonacci number to generate
endIndex: index of the last Fibonacci number to generate 
useCache: can be true or false, enables caching to avoid recomputation
timeoutMs: max allowed time in milliseconds before aborting
maxMemoryBytes: maximum allowed memory usage during execution 

To run the project:
1. Clone the project (git clone)
2. Open the project in Visual Studio
3. Run without debugging
4. Navigate to Swagger UI: https://localhost:{port}/swagger
5. Test the /Fibonacci endpoint

Example input: 
To test we can use either Swagger UI or:

bash:
$ curl -k "https://localhost:7220/Fibonacci?startIndex=5&endIndex=10&useCache=true&timeoutMs=5000&maxMemoryBytes=100000000"

Output:
{"subsequence":[5,8,13,21,34,55],"timeoutOccurred":false,"memoryLimitExceeded":false}

Author:
**Omar Huseynli**
Email: omar.2005.22.04@gmail.com
Github: https://github.com/OmarHuseynli







