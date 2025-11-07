# Snippet Gallery API

A personal code snippet management system with a .NET API backend and CLI client for quick snippet storage and retrieval with clipboard integration.

## Overview

Snippet Gallery helps developers store, organize, and quickly access code snippets. The project consists of two main components:

- **API Server**: .NET Web API for managing snippets with SQLite storage
- **CLI Client**: Command-line interface with clipboard integration for rapid workflow

## Features (MVP)

- 📋 **Clipboard Integration**: Add snippets directly from clipboard, retrieve snippets to clipboard
- 🔍 **Language Filtering**: Organize and search snippets by programming language
- 🖥️ **Dual Access**: Use via CLI for speed or HTTP API for flexibility
- 💾 **Simple Storage**: SQLite database for easy setup and portability

## Architecture

```
┌─────────────┐
│  CLI Client │ ──HTTP──> ┌──────────────┐
└─────────────┘           │  API Server  │
                          │   (.NET)     │
      ┌─────────┐         └──────┬───────┘
      │ Web UI  │ ──HTTP──>      │
      └─────────┘                │
                          ┌──────▼───────┐
                          │   SQLite DB  │
                          └──────────────┘
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Git

### Installation

```bash
# Clone the repository
git clone <repository-url>
cd snippet-gallery

# Run the API server
cd SnippetGallery.API
dotnet run

# In another terminal, run the CLI
cd SnippetGallery.CLI
dotnet build
```

## Usage

### API Server

```bash
# Start the server
cd SnippetGallery.API
dotnet run

# Server runs on http://localhost:5000
```

### CLI Client

```bash
# Add a snippet (grabs from clipboard)
snippet add python

# List all snippets
snippet list

# List snippets by language
snippet list --language python

# Get a specific snippet (copies to clipboard)
snippet get <id>

# Delete a snippet
snippet delete <id>
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/snippets` | Create new snippet |
| GET | `/api/snippets` | List all snippets |
| GET | `/api/snippets/{id}` | Get snippet by ID |
| GET | `/api/snippets?language={lang}` | Filter by language |
| DELETE | `/api/snippets/{id}` | Delete snippet |

## Data Model

```json
{
  "id": "guid",
  "language": "python",
  "code": "def example():\n    pass",
  "title": "Example Function",
  "description": "Optional description",
  "createdAt": "2024-11-07T10:00:00Z",
  "updatedAt": "2024-11-07T10:00:00Z"
}
```

## Roadmap

### MVP (Current)
- [x] Basic CRUD operations
- [x] Clipboard integration
- [x] Language filtering
- [x] CLI client

### Future Features
- [ ] Tags and categories
- [ ] Syntax highlighting in web UI
- [ ] Full-text search
- [ ] Snippet versioning
- [ ] Authentication & multi-user support
- [ ] Homelab deployment guide
- [ ] Import/export functionality

## Tech Stack

- **Backend**: ASP.NET Core Web API
- **Database**: SQLite (with EF Core)
- **CLI**: .NET Console App with System.CommandLine
- **Clipboard**: TextCopy library

## Configuration

The CLI client needs to know where the API is running. Configuration is stored in `appsettings.json`:

```json
{
  "ApiBaseUrl": "http://localhost:5000"
}
```

For homelab deployment, update this URL to your server's address.

## Development

```bash
# Run with hot reload
dotnet watch run

# Run tests
dotnet test

# Build for release
dotnet publish -c Release
```

## Contributing

This is a personal project, but feel free to fork and adapt for your own use!

## License

MIT License - do whatever you want with it.

---

**Note**: This project is designed for personal use and local/homelab deployment. Not intended for public internet exposure without proper security hardening.