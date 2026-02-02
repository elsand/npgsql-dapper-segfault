# Dapper + Npgsql array segfault repro

## Prerequisite
PostgreSQL available on localhost:5432 (eg. in Podman or Docker). Either change the connection string in `Program.cs` or supply `PG_CONNECTION_STRING` as an environment variable.

## Running
```bash
DOTNET_DbgEnableMiniDump=1 \
DOTNET_DbgMiniDumpType=4 \
DOTNET_DbgMiniDumpName=/tmp/dappersegfault_%p.dmp \
PG_CONNECTION_STRING='Host=localhost;Database=postgres;Username=postgres;Password=postgres' \
dotnet run -f net10.0 # or -f net9.0
```

This writes a dump file to `/tmp` on crash (replace the path if desired).
