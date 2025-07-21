// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ImGuiProvider.Extensions;
using ImGuiProvider.Implementations.HexaNet;
using ImGuiProvider.Interfaces;
using ImGuiProvider.Services;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for registering ImGui services with dependency injection containers
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds ImGui services using Hexa.NET.ImGui implementation
	/// </summary>
	/// <param name="services">The service collection</param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddImGui(this IServiceCollection services)
	{
		services.AddSingleton<IImGuiProvider, HexaNetImGuiProvider>();
		services.AddTransient<ImGuiContext>();

		return services;
	}

	/// <summary>
	/// Adds ImGui services with custom provider
	/// </summary>
	/// <typeparam name="TProvider">The provider implementation type</typeparam>
	/// <param name="services">The service collection</param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddImGui<TProvider>(this IServiceCollection services)
		where TProvider : class, IImGuiProvider
	{
		services.AddSingleton<IImGuiProvider, TProvider>();
		services.AddTransient<ImGuiContext>();

		return services;
	}

	/// <summary>
	/// Adds ImGui services with factory for custom configuration
	/// </summary>
	/// <param name="services">The service collection</param>
	/// <param name="factory">Factory function to create the provider</param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddImGui(
		this IServiceCollection services,
		Func<IServiceProvider, IImGuiProvider> factory)
	{
		services.AddSingleton(factory);
		services.AddTransient<ImGuiContext>();

		return services;
	}

	// Backend extension methods can be added here when specific backend implementations are available

	/// <summary>
	/// Adds a custom backend
	/// </summary>
	/// <typeparam name="TBackend">The backend implementation type</typeparam>
	/// <param name="services">The service collection</param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddImGuiBackend<TBackend>(this IServiceCollection services)
		where TBackend : class, IImGuiBackend
	{
		services.AddTransient<IImGuiBackend, TBackend>();

		return services;
	}

	/// <summary>
	/// Adds a custom backend with factory
	/// </summary>
	/// <typeparam name="TBackend">The backend interface type</typeparam>
	/// <param name="services">The service collection</param>
	/// <param name="factory">Factory function to create the backend</param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddImGuiBackend<TBackend>(
		this IServiceCollection services,
		Func<IServiceProvider, TBackend> factory)
		where TBackend : class, IImGuiBackend
	{
		services.AddTransient(factory);
		services.AddTransient<IImGuiBackend>(serviceProvider => serviceProvider.GetRequiredService<TBackend>());

		return services;
	}
}
