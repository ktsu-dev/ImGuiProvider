# ImGuiProvider

A dependency injection abstraction layer over ImGui implementations for .NET, providing clean interfaces and swappable backends.

## Features

- **Dependency Injection** - Clean integration with `Microsoft.Extensions.DependencyInjection`
- **Provider Abstraction** - `IImGuiProvider` interface wrapping 160+ ImGui methods allows swapping implementations
- **Backend Support** - Separate `IPlatformBackend` and `IRendererBackend` interfaces for windowing and rendering
- **Context Management** - `ImGuiContext` handles the full frame lifecycle (initialize, begin frame, end frame, dispose)
- **Built-in Implementation** - Ships with `HexaNetImGuiProvider` wrapping Hexa.NET.ImGui

## Installation

```bash
dotnet add package ktsu.ImGuiProvider
```

## Quick Start

```csharp
using ImGuiProvider.Extensions;
using ImGuiProvider.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddImGui(); // Registers HexaNetImGuiProvider as singleton

var serviceProvider = services.BuildServiceProvider();
var context = serviceProvider.GetRequiredService<ImGuiContext>();
```

## Usage with Backends

```csharp
var services = new ServiceCollection();
services.AddImGui();
services.AddImGuiBackend<MyPlatformBackend>();
services.AddImGuiBackend<MyRendererBackend>();

var serviceProvider = services.BuildServiceProvider();
var context = serviceProvider.GetRequiredService<ImGuiContext>();

// Resolve and attach backends
var platformBackend = serviceProvider.GetRequiredService<IPlatformBackend>();
var rendererBackend = serviceProvider.GetRequiredService<IRendererBackend>();

context.Initialize();
context.AddBackend(platformBackend);
context.AddBackend(rendererBackend);
context.InitializeBackends();

// Render loop
while (running)
{
    context.BeginFrame();

    // Your ImGui code here
    var provider = serviceProvider.GetRequiredService<IImGuiProvider>();
    provider.ShowDemoWindow();

    context.EndFrame();
}

context.Dispose();
```

## Custom Implementations

### Custom Provider

```csharp
public class MyProvider : IImGuiProvider
{
    // Implement all IImGuiProvider methods
}

services.AddImGui<MyProvider>();
// Or with a factory:
services.AddImGui(sp => new MyProvider());
```

### Custom Backend

```csharp
public class MyBackend : IRendererBackend
{
    public string Name => "My Backend";
    public bool Initialize() => true;
    public void Shutdown() { }
    public void NewFrame() { }
    public void RenderDrawData(nint drawData) { }
    public void SetCurrentContext(nint context) { }
    public bool CreateDeviceObjects() => true;
    public void InvalidateDeviceObjects() { }
    public void Dispose() { }
}

services.AddImGuiBackend<MyBackend>();
```

## Requirements

- .NET 8.0, 9.0, or 10.0
- Hexa.NET.ImGui 2.2.8.4
- Microsoft.Extensions.DependencyInjection.Abstractions 9.0.7

## License

MIT License
