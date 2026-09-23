# ShelfTrack

Webapplicatie om je eigen boekencollectie en leesvoortgang bij te houden, zonder sociale laag.
Individueel project, Software Design & Engineering (Fontys ICT), semester 2.

Ontwerp en onderbouwing: zie `Technisch_Ontwerp.docx` in de map Design.

## Opbouw

De solution volgt de gelaagde architectuur uit het Technisch Ontwerp (hoofdstuk 3).

| Project | Laag | Wat komt hierin |
|---|---|---|
| `src/ShelfTrack.Web` | Presentation | Razor Pages, formulieren, login |
| `src/ShelfTrack.Application` | Application | Services (BoekService, PlankService, LeesvoortgangService) en de interfaces voor opslag en de boeken-API |
| `src/ShelfTrack.Domain` | Domain | Lezer, Boek, Plank, Leesregistratie, Leesstatus |
| `src/ShelfTrack.Persistence` | Persistence | EF Core DbContext, migraties, repositories (SQLite) |
| `src/ShelfTrack.ApiWrapper` | API Wrapper | BoekenApiClient voor Google Books, met de fallback (R7) |
| `tests/ShelfTrack.Tests` | Tests | Unit tests op de Application-laag |

Afhankelijkheden:

- Domain kent niemand.
- Application kent alleen Domain. De interfaces (bijvoorbeeld `IBoekRepository` en `IBoekenApiClient`) staan hier.
- Persistence en ApiWrapper implementeren die interfaces.
- Web koppelt alles aan elkaar via dependency injection in `Program.cs`.

Zo kan ik de services unit testen met een nagemaakte repository, zonder database.

## Starten

```bash
dotnet build
dotnet run --project src/ShelfTrack.Web
dotnet test
```

Migraties (eenmalig de tool installeren: `dotnet tool install --global dotnet-ef`):

```bash
dotnet ef migrations add <Naam> --project src/ShelfTrack.Persistence --startup-project src/ShelfTrack.Web
dotnet ef database update --project src/ShelfTrack.Persistence --startup-project src/ShelfTrack.Web
```
