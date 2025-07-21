# ImGuiProvider

A dependency injection wrapper library for ImGui implementations, providing a clean abstraction over ImGui libraries with support for multiple backends.

## Features

- **Dependency Injection**: Clean DI integration with `Microsoft.Extensions.DependencyInjection`
- **Provider Abstraction**: `IImGuiProvider` interface allows swapping ImGui implementations
- **Backend Support**: Separate interfaces for platform and renderer backends
- **Hexa.NET.ImGui**: Built-in implementation using Hexa.NET.ImGui
- **SOLID Principles**: Follows SOLID design principles with clear separation of concerns
- **Context Management**: High-level context management with lifecycle handling

## Installation

```xml
<PackageReference Include="ImGuiProvider" Version="1.0.0" />
```

## Quick Start

### Basic Setup with Dependency Injection

```csharp
using ImGuiProvider.Extensions;
using Microsoft.Extensions.DependencyInjection;

// Configure services
var services = new ServiceCollection();
services.AddImGui(); // Uses Hexa.NET.ImGui by default

// Build service provider
var serviceProvider = services.BuildServiceProvider();

// Get ImGui context
var imguiContext = serviceProvider.GetRequiredService<ImGuiContext>();
```

### Advanced Setup with Backends

```csharp
using ImGuiProvider.Extensions;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Add ImGui provider
services.AddImGui();

// Add backends (assumes you have GLFW window handle)
services.AddImGuiGLFWBackend(windowHandle, installCallbacks: true);
services.AddImGuiOpenGL3Backend(glslVersion: "#version 330");

var serviceProvider = services.BuildServiceProvider();

// Initialize and use
var context = serviceProvider.GetRequiredService<ImGuiContext>();
var glfwBackend = serviceProvider.GetRequiredService<IPlatformBackend>();
var openglBackend = serviceProvider.GetRequiredService<IRendererBackend>();

// Initialize context and backends
context.Initialize();
context.AddBackend(glfwBackend);
context.AddBackend(openglBackend);
context.InitializeBackends();

// Main loop
while (running)
{
    // Process events...
    
    context.BeginFrame();
    
    // Your ImGui code here
    var provider = serviceProvider.GetRequiredService<IImGuiProvider>();
    provider.ShowDemoWindow();
    
    context.EndFrame();
    
    // Swap buffers...
}

context.Dispose();
```

## Core Interfaces

### IImGuiProvider

The main abstraction over ImGui functionality:

```csharp
public interface IImGuiProvider : IDisposable
{
    // Context management
    nint CreateContext();
    void SetCurrentContext(nint context);
    nint GetCurrentContext();
    void DestroyContext(nint context);

    // Frame management
    void NewFrame();
    void EndFrame();
    void Render();
    nint GetDrawData();

    // UI methods
    bool Begin(string name);
    void End();
    void Text(string text);
    bool Button(string label);
    void ShowDemoWindow();
    // ... and more
}
```

### IImGuiBackend

Base interface for all backends:

```csharp
public interface IImGuiBackend : IDisposable
{
    string Name { get; }
    bool Initialize();
    void Shutdown();
    void NewFrame();
    void RenderDrawData(nint drawData);
    void SetCurrentContext(nint context);
}
```

### IPlatformBackend & IRendererBackend

Specialized backend interfaces:

```csharp
public interface IPlatformBackend : IImGuiBackend
{
    void ProcessEvents();
}

public interface IRendererBackend : IImGuiBackend
{
    bool CreateDeviceObjects();
    void InvalidateDeviceObjects();
}
```

## Built-in Implementations

### HexaNetImGuiProvider

Uses Hexa.NET.ImGui as the underlying implementation:

```csharp
services.AddImGui(); // Default Hexa.NET implementation
```

### Available Backends

- **HexaNetOpenGL3Backend**: OpenGL 3+ renderer using Hexa.NET.ImGui.Backends
- **HexaNetGLFWBackend**: GLFW platform backend using Hexa.NET.ImGui.Backends.GLFW

## Custom Implementations

### Custom Provider

```csharp
public class MyImGuiProvider : IImGuiProvider
{
    // Implement interface methods
    public nint CreateContext() => /* your implementation */;
    // ... etc
}

// Register custom provider
services.AddImGui<MyImGuiProvider>();
```

### Custom Backend

```csharp
public class MyCustomBackend : IRendererBackend
{
    public string Name => "My Custom Backend";
    // Implement interface methods
}

// Register custom backend
services.AddImGuiBackend<MyCustomBackend>();
```

## Architecture

The library follows SOLID principles with clear separation:

```
┌─────────────────┐    ┌─────────────────────┐
│   Application   │────│  ImGuiContext       │
└─────────────────┘    └─────────────────────┘
                              │
                    ┌─────────┴─────────┐
                    │                   │
            ┌───────▼────────┐  ┌───────▼─────────┐
            │ IImGuiProvider │  │ IImGuiBackend   │
            └────────────────┘  └─────────────────┘
                    │                   │
        ┌───────────▼──────────┐       │
        │ HexaNetImGuiProvider │       │
        └──────────────────────┘       │
                               ┌───────▼─────────────┐
                               │ Platform & Renderer │
                               │     Backends        │
                               └─────────────────────┘
```

## Requirements

- .NET 8.0 or later
- Hexa.NET.ImGui 2.1.7 or later
- Microsoft.Extensions.DependencyInjection.Abstractions 8.0.0 or later

## License

MIT License 
