// Copyright (c) 2023-2026 ktsu-dev contributors

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
	/// Adds a custom backend. The backend can be resolved as <typeparamref name="TBackend"/>, as
	/// <see cref="IImGuiBackend"/>, and as <see cref="IPlatformBackend"/> and/or <see cref="IRendererBackend"/>
	/// when it implements them.
	/// </summary>
	/// <typeparam name="TBackend">The backend implementation type</typeparam>
	/// <param name="services">The service collection</param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddImGuiBackend<TBackend>(this IServiceCollection services)
		where TBackend : class, IImGuiBackend
	{
		services.AddTransient<TBackend>();
		AddBackendForwards<TBackend>(services);

		return services;
	}

	/// <summary>
	/// Adds a custom backend with factory. The backend can be resolved as <typeparamref name="TBackend"/>, as
	/// <see cref="IImGuiBackend"/>, and as <see cref="IPlatformBackend"/> and/or <see cref="IRendererBackend"/>
	/// when <typeparamref name="TBackend"/> implements them.
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
		AddBackendForwards<TBackend>(services);

		return services;
	}

	/// <summary>
	/// Forwards each backend interface that <typeparamref name="TBackend"/> implements to the
	/// <typeparamref name="TBackend"/> registration. An interface that is <typeparamref name="TBackend"/>
	/// itself is already registered, and forwarding it to itself would recurse on resolution.
	/// </summary>
	private static void AddBackendForwards<TBackend>(IServiceCollection services)
		where TBackend : class, IImGuiBackend
	{
		if (typeof(TBackend) != typeof(IImGuiBackend))
		{
			services.AddTransient<IImGuiBackend>(serviceProvider => serviceProvider.GetRequiredService<TBackend>());
		}

		if (typeof(TBackend) != typeof(IPlatformBackend) && typeof(IPlatformBackend).IsAssignableFrom(typeof(TBackend)))
		{
			services.AddTransient(serviceProvider => (IPlatformBackend)serviceProvider.GetRequiredService<TBackend>());
		}

		if (typeof(TBackend) != typeof(IRendererBackend) && typeof(IRendererBackend).IsAssignableFrom(typeof(TBackend)))
		{
			services.AddTransient(serviceProvider => (IRendererBackend)serviceProvider.GetRequiredService<TBackend>());
		}
	}
}
