# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

ImGuiProvider is a .NET library that provides a dependency injection abstraction layer over ImGui implementations. It wraps the Hexa.NET.ImGui library behind interfaces (`IImGuiProvider`, `IImGuiBackend`) so consuming applications can swap ImGui backends without changing application code.

Part of the [ktsu.dev](https://ktsu.dev) ecosystem. Uses `ktsu.Sdk.Lib` for build configuration.

## Build Commands

```bash
# Build
dotnet build

# Build release
dotnet build --configuration Release

# Pack for NuGet
dotnet pack --configuration Release --output ./staging
```

No test project exists yet. The solution contains only the library project.

## Architecture

### Layer Structure

```
Extensions/ServiceCollectionExtensions.cs  ← DI registration entry point
Services/ImGuiContext.cs                   ← Context lifecycle manager
Interfaces/IImGuiProvider.cs               ← 160+ method ImGui API surface
Interfaces/IImGuiBackend.cs                ← Backend abstraction (3 interfaces)
Implementations/HexaNet/HexaNetImGuiProvider.cs ← Hexa.NET.ImGui wrapper
```

### Key Interfaces

- **`IImGuiProvider`** (2200+ lines) - Complete ImGui API abstraction covering windows, widgets, drawing, tables, tabs, docking, layout, clipboard, viewports. Uses `nint` for pointers and `System.Numerics` vectors.
- **`IImGuiBackend`** - Base backend interface (Initialize, Shutdown, NewFrame, RenderDrawData)
- **`IPlatformBackend`** extends `IImGuiBackend` - Adds `ProcessEvents()` for input/windowing (e.g., GLFW)
- **`IRendererBackend`** extends `IImGuiBackend` - Adds `CreateDeviceObjects()`/`InvalidateDeviceObjects()` for graphics APIs (e.g., OpenGL3)

### DI Registration

`ServiceCollectionExtensions.AddImGui()` registers `HexaNetImGuiProvider` as singleton `IImGuiProvider` and `ImGuiContext` as transient. Generic overloads (`AddImGui<TProvider>()`) and factory overloads allow custom providers. `AddImGuiBackend<T>()` registers backends.

### Frame Lifecycle

`ImGuiContext` orchestrates the render loop:
1. `Initialize()` → creates ImGui context via provider
2. `AddBackend()` → registers platform/renderer backends
3. `InitializeBackends()` → initializes all backends
4. `BeginFrame()` → sets context, calls `NewFrame()` on backends then provider
5. `EndFrame()` → calls `Render()`, gets draw data, renders via backends, updates platform windows
6. `Dispose()` → shuts down backends, destroys context, disposes provider

### Unsafe Code

`AllowUnsafeBlocks` is enabled. The `HexaNetImGuiProvider` performs pointer operations and type casting for ImGui interop. Methods accepting `nint` parameters represent native pointers to ImGui structs.

## Key Dependencies

- **Hexa.NET.ImGui** (2.2.8.4) - Core ImGui bindings
- **Microsoft.Extensions.DependencyInjection** (9.0.7) - DI framework
- Central Package Management via `Directory.Packages.props`

## Adding a New Backend Implementation

1. Create a class implementing `IPlatformBackend` or `IRendererBackend`
2. Implement `Initialize()`, `Shutdown()`, `NewFrame()`, `RenderDrawData(nint)`
3. Register via `services.AddImGuiBackend<YourBackend>()`

## Adding a New Provider Implementation

1. Create a class implementing `IImGuiProvider` (all 160+ methods)
2. Register via `services.AddImGui<YourProvider>()` or the factory overload
