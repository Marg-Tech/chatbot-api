# Chatbot API

ASP.NET Core Web API project that serves as a server for a chatbot system, including endpoints for sending and receiving messages.

## Features

- **Send Messages**: POST endpoint to send messages to the chatbot
- **Receive Messages**: GET endpoint to retrieve all messages
- **Get Message by ID**: GET endpoint to retrieve a specific message
- **In-Memory Storage**: Messages are stored in memory (for production, consider a database)
- **Swagger Documentation**: Interactive API documentation available at `/swagger`

## Prerequisites

- .NET 8.0 SDK or later

## Getting Started

### Build the Project

```bash
dotnet build
```

### Run the API

```bash
cd ChatbotApi
dotnet run
```

The API will be available at `http://localhost:5178` (or the port specified in launchSettings.json)

### Access Swagger UI

Navigate to `http://localhost:5178/swagger` in your browser to view the interactive API documentation.

## API Endpoints

### POST /api/messages
Send a new message to the chatbot

**Request Body:**
```json
{
  "sender": "User1",
  "content": "Hello, chatbot!"
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "sender": "User1",
  "content": "Hello, chatbot!",
  "timestamp": "2026-01-29T08:44:21.960Z"
}
```

### GET /api/messages
Get all messages (most recent first)

**Query Parameters:**
- `limit` (optional): Maximum number of messages to retrieve (default: 100)

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "sender": "User1",
    "content": "Hello, chatbot!",
    "timestamp": "2026-01-29T08:44:21.960Z"
  },
  {
    "id": 2,
    "sender": "Bot",
    "content": "Hi there! How can I help you?",
    "timestamp": "2026-01-29T08:44:26.977Z"
  }
]
```

### GET /api/messages/{id}
Get a specific message by ID

**Response (200 OK):**
```json
{
  "id": 1,
  "sender": "User1",
  "content": "Hello, chatbot!",
  "timestamp": "2026-01-29T08:44:21.960Z"
}
```

## Project Structure

```
ChatbotApi/
├── Controllers/
│   └── MessagesController.cs   # API endpoints for messages
├── Models/
│   └── Message.cs               # Message data model
├── Services/
│   └── MessageService.cs        # In-memory message storage service
├── Program.cs                   # Application entry point
└── ChatbotApi.csproj           # Project file
```

## Examples using curl

### Send a message
```bash
curl -X POST http://localhost:5178/api/messages \
  -H "Content-Type: application/json" \
  -d '{"sender": "User1", "content": "Hello, chatbot!"}'
```

### Get all messages
```bash
curl http://localhost:5178/api/messages
```

### Get a specific message
```bash
curl http://localhost:5178/api/messages/1
```
