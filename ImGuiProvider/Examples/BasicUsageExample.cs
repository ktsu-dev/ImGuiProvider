// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ImGuiProvider.Examples;
using ImGuiProvider.Extensions;
using ImGuiProvider.Interfaces;
using ImGuiProvider.Services;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Basic usage example showing how to use ImGuiProvider with dependency injection
/// </summary>
public static class BasicUsageExample
{
	/// <summary>
	/// Example of basic setup with dependency injection
	/// </summary>
	public static void BasicSetupExample()
	{
		// Configure services
		ServiceCollection services = new();
		services.AddImGui(); // Uses Hexa.NET.ImGui by default

		// Build service provider
		using ServiceProvider serviceProvider = services.BuildServiceProvider();

		// Get the provider
		IImGuiProvider provider = serviceProvider.GetRequiredService<IImGuiProvider>();

		// Create and setup context
		nint context = provider.CreateContext();
		provider.SetCurrentContext(context);

		// Setup ImGui configuration
		nint io = provider.GetIO();
		// Configure IO settings here...

		provider.StyleColorsDark();

		// Example frame rendering
		provider.NewFrame();

		if (provider.Begin("Hello World"))
		{
			provider.Text("Hello from ImGuiProvider!");

			if (provider.Button("Click Me"))
			{
				Console.WriteLine("Button was clicked!");
			}

			provider.Separator();
			provider.Text("This is a basic example.");

			provider.EndWindow();
		}

		provider.ShowDemoWindow();

		provider.Render();
		// Note: In a real application, you would render the draw data here

		// Cleanup
		provider.DestroyContext(context);
	}

	/// <summary>
	/// Example of advanced setup with backends and context management
	/// </summary>
	/// <summary>
	/// Example of advanced setup with custom configuration
	/// </summary>
	public static void AdvancedSetupExample()
	{
		ServiceCollection services = new();

		// Add ImGui provider
		services.AddImGui();

		using ServiceProvider serviceProvider = services.BuildServiceProvider();

		// Get services
		using ImGuiContext imguiContext = serviceProvider.GetRequiredService<ImGuiContext>();
		IImGuiProvider provider = serviceProvider.GetRequiredService<IImGuiProvider>();

		// Initialize context and backends
		if (!imguiContext.Initialize())
		{
			throw new InvalidOperationException("Failed to initialize ImGui context");
		}

		// Note: Backend registration would be done here if backends were available

		if (!imguiContext.InitializeBackends())
		{
			throw new InvalidOperationException("Failed to initialize backends");
		}

		// Configure ImGui
		nint io = provider.GetIO();
		// Set up IO configuration flags, fonts, etc.

		provider.StyleColorsDark();

		// Simulate main loop
		for (int frame = 0; frame < 10; frame++) // Just 10 frames for example
		{
			// Begin frame
			imguiContext.BeginFrame();

			// Your ImGui UI code here
			if (provider.Begin("Example Window"))
			{
				provider.Text($"Frame: {frame}");

				if (provider.Button("Test Button"))
				{
					Console.WriteLine($"Button clicked on frame {frame}");
				}

				provider.Separator();
				provider.Text("Multi-backend rendering example");

				provider.EndWindow();
			}

			provider.ShowDemoWindow();

			// End frame (handles rendering)
			imguiContext.EndFrame();

			// In a real application, you would:
			// - Poll GLFW events
			// - Swap OpenGL buffers
			// - Handle window events

			Console.WriteLine($"Rendered frame {frame}");
		}

		Console.WriteLine("Example completed successfully!");
	}

	/// <summary>
	/// Example of using custom configuration
	/// </summary>
	public static void CustomConfigurationExample()
	{
		ServiceCollection services = new();

		// Custom provider factory
		services.AddImGui(serviceProvider =>
			// You could configure the provider here
			new Implementations.HexaNet.HexaNetImGuiProvider());

		using ServiceProvider serviceProvider = services.BuildServiceProvider();
		IImGuiProvider provider = serviceProvider.GetRequiredService<IImGuiProvider>();

		Console.WriteLine($"Provider: {provider.GetType().Name}");
		Console.WriteLine($"ImGui Version: {provider.GetVersion()}");
	}
}
