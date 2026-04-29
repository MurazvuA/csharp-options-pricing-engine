C# Options Pricing Engine (Front-Office Simulation)

Overview

High-performance options pricing engine implemented in C# / .NET, built to simulate a front-office pricing service used by traders. The project demonstrates:

- Implementation of derivatives pricing models
- Low-latency API design
- Scalable architecture for pricing systems
- Application of quantitative finance fundamentals in production-style code

Features

Pricing models

- Black–Scholes (analytical)
- Monte Carlo simulation (parallelised)
- Binomial Tree

Instruments

- European call and put options
- Barrier option

Risk metrics (Greeks)

- Delta
- Gamma
- Vega
- Theta

API

- RESTful pricing endpoint
- JSON request / response
- Designed for real-time pricing queries

Performance

- Parallel Monte Carlo using Task Parallel Library
- Optimised numerical calculations
- Lightweight architecture for low latency

Architecture

Clean separation of concerns to reflect real-world pricing engines used in investment banks:

- Pricing models
- Instrument definitions
- Market data interfaces
- Services
- API layer

Example

Request
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

Response

{
  "price": 10.45,
  "delta": 0.63,
  "gamma": 0.02,
  "vega": 0.25,
  "theta": -0.01
}

Technologies

- C# / .NET 8
- ASP.NET Web API
- Parallel processing (Task / Parallel.For)
- xUnit for unit testing

Getting started

1. Clone the repository.
2. Build with .NET 8 SDK:

dotnet build

3. Run the API:

dotnet run --project src/Api

4. Send POST requests to /price with JSON payload as shown above.

Tests

Run unit tests with:

dotnet test

Future enhancements

Planned improvements:

- Volatility surface integration
- Real-time market data feed simulation
- Front-end trading UI (React)

Why this project

This project bridges quantitative finance theory and production-grade software engineering. It is intended to reflect the types of systems used in front-office pricing environments and provides a practical reference for implementing low-latency, scalable pricing services.
