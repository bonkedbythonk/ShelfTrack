# ShelfTrack

A web application for keeping track of your own book collection and reading progress, without a social layer.
Individual project for Software Design & Engineering (Fontys ICT), semester 2.

Design and rationale: see `Technisch_Ontwerp.docx` (Dutch) in the Design folder.

## Structure

The solution follows the layered architecture from the technical design (chapter 3).

| Project | Layer | Contents |
|---|---|---|
| `src/ShelfTrack.Web` | Presentation | Razor Pages, forms, login |
| `src/ShelfTrack.Application` | Application | Services (`BookService`, `ShelfService`, `ReadingProgressService`) and the interfaces for storage and the book API |
| `src/ShelfTrack.Domain` | Domain | `Reader`, `Book`, `Shelf`, `ReadingEntry`, `ReadingStatus` |
| `src/ShelfTrack.Persistence` | Persistence | EF Core `DbContext`, migrations, repositories (SQLite) |
| `src/ShelfTrack.ApiWrapper` | API Wrapper | `BookApiClient` for Google Books, including the fallback |
| `tests/ShelfTrack.Tests` | Tests | Unit tests for the Application layer |

Dependencies:

- Domain depends on nothing.
- Application only depends on Domain. The interfaces (such as `IBookRepository` and `IBookApiClient`) live here.
- Persistence and ApiWrapper implement those interfaces.
- Web wires everything together with dependency injection in `Program.cs`.

This way the services can be unit tested with a fake repository, without a database.

## Getting started

```bash
dotnet build
dotnet run --project src/ShelfTrack.Web
dotnet test
```

Migrations (install the tool once: `dotnet tool install --global dotnet-ef`):

```bash
dotnet ef migrations add <Name> --project src/ShelfTrack.Persistence --startup-project src/ShelfTrack.Web
dotnet ef database update --project src/ShelfTrack.Persistence --startup-project src/ShelfTrack.Web
```
