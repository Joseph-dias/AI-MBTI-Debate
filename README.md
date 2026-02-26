# AI-MBTI-Debate

An AI-powered debate simulator where distinct MBTI personality types argue various topics — each remaining faithful to their type’s characteristic reasoning, values, communication style, and worldview tendencies.

## Core Features

- **Rich MBTI Personas** — Deeply developed, named personalities with refined trait profiles, consistent behavior, and worldview-informed responses
- **Structured Multi-Round Debates** — Turn-based format allowing each personality to present arguments sequentially
- **Debate Summarizer** — Automatically produces concise recaps of individual rounds and overall conclusions / key insights
- **Search & Retrieval Integration** — Enables AI participants to look up relevant facts, historical data, or supporting material during debate
- **Backend API Layer (in development)** — Service structure being built to expose debate creation, participation, transcripts, and summaries via HTTP endpoints

## Recent Project Improvements

- Established dedicated API project structure (`AIDebateAPI`) with foundational code and preparation for testing
- Greatly enhanced the personality system: increased depth, assigned distinct names, improved trait consistency and nuance
- Implemented automatic debate summarizer for clearer outcomes
- Repaired and enabled search functionality so AIs can retrieve useful information mid-debate
- Improved code organization: added comments, modularized components (`Debate Library`, `PersonFactory`, `Persona`, etc.)
- Introduced a multi-project solution file (`AI MBTI Debate.sln`) for better maintainability and potential expansion

Long-term vision: A clean, documented API allowing external clients (web apps, bots, research tools) to easily initiate debates, retrieve full transcripts, and obtain summaries.

## Tech Stack

- **Primary Language & Framework**: C# (.NET) — console application + class library
- **API Development**: ASP.NET Core (emerging in `AIDebateAPI`)
- **AI Integration**: OpenAI-compatible large language models for generating in-character responses
- **Supporting Features**: Structured prompting, retrieval-augmented generation, semantic search capabilities

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/Joseph-dias/AI-MBTI-Debate.git
   cd AI-MBTI-Debate
   ```

2. Open the solution in Visual Studio, Rider, or compatible IDE:
   ```
   AI MBTI Debate.sln
   ```

3. Restore NuGet packages and build the solution.

4. Configure required settings:
   - LLM API key (e.g. `OPENAI_API_KEY` in user secrets, appsettings, or environment variables)
   - Any search/retrieval service keys if used

5. Run the current console prototype (entry point subject to change as API matures):
   ```bash
   dotnet run --project "AI MBTI Debate"
   ```

6. API (work in progress):
   - Once endpoints are implemented, navigate to the `AIDebateAPI` folder/project
   - Typical launch: `dotnet run` from that directory

## Current Limitations

- API endpoints remain under active development — not yet fully functional
- Primary interaction currently occurs through console prototypes while the service layer is completed
- Detailed API route, request/response, and authentication documentation is forthcoming

## Contributing

Pull requests are welcome
