C# Options Pricing Engine (Front-Office Simulation)
Overview
A high-performance options pricing engine built in C#/.NET, designed to simulate a front-office pricing service used by traders.
This project demonstrates:
•	Implementation of derivatives pricing models
•	Low-latency API design
•	Scalable architecture for pricing systems
•	Quantitative finance fundamentals applied in production-style code
________________________________________
Features
Pricing Models
•	Black-Scholes (analytical)
•	Monte Carlo simulation (parallelised)
•	Binomial Tree
Instruments
•	European Call/Put Options
•	Barrier Option
Risk Metrics (Greeks)
•	Delta, Gamma, Vega, Theta
API
•	RESTful pricing endpoint
•	JSON-based request/response
•	Designed for real-time pricing queries
Performance
•	Parallel Monte Carlo using Task Parallel Library
•	Optimised numerical calculations
•	Lightweight architecture for low latency
________________________________________
Architecture
•	Clean separation of concerns:
o	Pricing Models
o	Instruments
o	Market Data
o	Services
o	API Layer
•	Designed to reflect real-world pricing engines used in investment banks
________________________________________
Example Request
POST /price
{
"spot": 100,
"strike": 100,
"volatility": 0.2,
"rate": 0.05,
"maturity": 1,
"type": "call",
"model": "blackscholes"
}
________________________________________
Example Output
{
"price": 10.45,
"delta": 0.63,
"gamma": 0.02,
"vega": 0.25,
"theta": -0.01
}
________________________________________
Technologies
•	C# / .NET 8
•	ASP.NET Web API
•	Parallel processing (Task / Parallel.For)
•	xUnit (unit testing)
________________________________________
Future Enhancements
•	Volatility surface integration
•	Real-time market data feed simulation
•	Front-end trading UI (React)
________________________________________
Why This Project
This project was built to bridge quantitative finance theory and production-grade software engineering, reflecting the type of systems used in front-office pricing environments.

