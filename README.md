# AI-MBTI-Debate

An interactive AI-powered debate simulator that creates unique personalities with MBTI types and orchestrates intelligent discussions on any topic using advanced AI models.

## Overview

AI-MBTI-Debate is a C# console application that combines personality theory (Myers-Briggs Type Indicator) with AI-driven conversational intelligence to generate realistic, nuanced debates. The system creates AI personas with distinct MBTI personalities and moderates discussions where each character contributes their unique perspectives based on their psychological type.

## Features

- **Dynamic Persona Generation**: Automatically creates AI personas with distinct MBTI personality types
- **MBTI-Based Argumentation**: Each persona develops arguments aligned with their personality type
- **Real-time Streaming**: Responses stream character-by-character for a natural reading experience
- **Flexible Debate Topics**: Supports any user-defined debate topic
- **Automatic Summarization**: Generates a comprehensive summary after the debate concludes
- **Multi-model Support**: Compatible with various AI models (configured for Grok)

## Architecture

This project is organized into two main components:

### 1. **AI MBTI Debate** (Console Application)
The main executable that orchestrates the debate experience:
- `Program.cs`: Core debate orchestration logic
- Manages participant rotation and debate flow
- Handles user input and output

### 2. **Debate Library** (Class Library)
Reusable components for debate functionality:
- `AIDebateHandler`: Manages AI interactions and debate responses
- `PersonFactory`: Creates diverse AI personas with MBTI types
- `Persona`: Represents debate participants with personality traits
- `Personality`: MBTI type definitions and personality logic

## Prerequisites

- **.NET 8.0** or higher
- API access to a compatible AI model (currently configured for Grok-4-fast-reasoning)
- Valid API credentials for the AI service

## Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Joseph-dias/AI-MBTI-Debate.git
   cd AI-MBTI-Debate
