# AI-MBTI-Debate

An AI-powered debate simulator where distinct MBTI personality types argue various topics—each remaining faithful to their type’s characteristic reasoning, values, communication style, and worldview.

## Core Features

- **Rich MBTI Personas**: Deeply developed, named personalities with refined trait profiles, consistent behavior, and worldview-informed responses.
- **Structured Multi-Round Debates**: Turn-based format allowing each personality to present arguments sequentially.
- **Debate Summarizer**: Automatically produces concise recaps of individual rounds and overall conclusions/key insights.
- **Search & Retrieval Integration**: Enables AI participants to look up relevant facts, historical data, or supporting material during debates.
- **Backend API Layer**: Service structure to expose debate creation, participation, transcripts, and summaries via HTTP endpoints, using ASP.NET Core and SignalR for real-time features.

## Recent Project Improvements

- Established dedicated API project structure (AIDebateAPI) with foundational code, including Program.cs for app setup, configuration files, and preparations for controllers, DTOs, and SignalR.
- Enhanced the personality system: increased depth, assigned distinct names, improved trait consistency and nuance through classes like Persona.cs and Personality.cs.
- Implemented automatic debate summarizer within AIDebateHandler.cs for clearer outcomes.
- Repaired and enabled search functionality in AI_Tools.cs so AIs can retrieve useful information mid-debate.
- Improved code organization: added comments, modularized components in Debate Library (e.g., PersonFactory.cs, IPersonFactory.cs, BASE_AI.cs).
- Introduced a multi-project solution file (AI MBTI Debate.sln) for better maintainability and potential expansion.

## Tech Stack

- **Primary Language & Framework**: C# (.NET) — console application + class library.
- **API Development**: ASP.NET Core (in AIDebateAPI), with SignalR for real-time updates.
- **AI Integration**: OpenAI-compatible large language models for generating in-character responses.
- **Supporting Features**: Structured prompting, retrieval-augmented generation, semantic search capabilities.

## Project Structure

The repository is organized into directories for separation of concerns:

- **AI MBTI Debate**: Console application prototype.
  - **AI MBTI Debate.csproj**: Project file.
  - **Program.cs**: Main entry point for running the console-based debate simulation.
  - **ConnectToSignalR.cs**: Handles connection to SignalR for potential real-time debate features in the console.

- **AIDebateAPI**: ASP.NET Core API project (under development).
  - **AIDebateAPI.csproj**: Project file.
  - **Program.cs**: Configures the web host, services, middleware, routing, and endpoints.
  - **AIDebateAPI.http**: For testing API endpoints.
  - **appsettings.json** and **appsettings.Development.json**: Configuration files for settings like logging and connections.
  - **Controllers/**: Intended for API controllers (currently empty or minimal).
  - **DTO/**: For data transfer objects (currently empty or minimal).
  - **Properties/**: Project properties, likely including launchSettings.json.
  - **SignalR/**: For SignalR hubs to enable real-time communication (currently empty or minimal).

- **Debate Library**: Shared library with core debate logic and personality components.
  - **Debate Library.csproj**: Project file.
  - **AIDebateHandler.cs**: Manages the debate process, including rounds, participant interactions, and summarization.
  - **AINamer.cs**: Generates names for AI personas.
  - **AI_Tools.cs**: Provides tools for AI, such as search and retrieval functions.
  - **BASE_AI.cs**: Base class for AI participants, defining common behaviors.
  - **IPersonFactory.cs**: Interface for creating personas.
  - **PersonFactory.cs**: Implements persona creation based on MBTI types.
  - **Persona.cs**: Defines the persona class, encapsulating MBTI traits, name, and behavior.
  - **Personality.cs**: Handles personality traits and consistency.
  - **RandomExtensions.cs**: Extensions for random operations, possibly for trait variation or selection.

Root files include .gitattributes, .gitignore, AI MBTI Debate.sln, and README.md.

## Getting Started

1. Clone the repository:  
   `git clone https://github.com/Joseph-dias/AI-MBTI-Debate.git`  
   `cd AI-MBTI-Debate`  

2. Open the solution in Visual Studio, Rider, or compatible IDE:  
   `AI MBTI Debate.sln`

3. Restore NuGet packages and build the solution.

4. Configure required settings:  
   - LLM API key (e.g., XAI_API_KEY in user secrets, appsettings, or environment variables).  
   - Any search/retrieval service keys if used.

5. Run the console prototype:  
   `dotnet run --project "AI MBTI Debate"`

6. For the API (work in progress):  
   Navigate to AIDebateAPI and run `dotnet run`.

## Current Limitations

- API endpoints are under active development—not yet fully functional.
- Primary interaction is through the console prototype while the service layer matures.
- Detailed API documentation for routes, requests/responses, and authentication is forthcoming.
- Some subdirectories in AIDebateAPI are placeholders for future implementations.

## Contributing

Pull requests are welcome. Focus on API development, adding more MBTI fidelity, or enhancing debate logic.

## License

This project is licensed under the [GNU General Public License v2](./LICENSE).