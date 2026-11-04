// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ImGuiProvider.Test;

using System.Linq;
using ImGuiProvider.Extensions;
using ImGuiProvider.Implementations.HexaNet;
using ImGuiProvider.Interfaces;
using ImGuiProvider.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

/// <summary>
/// Tests for <see cref="ServiceCollectionExtensions"/>.
/// Uses service descriptor inspection to avoid resolving HexaNetImGuiProvider
/// (which loads native ImGui libraries that hang in test environments).
/// </summary>
[TestClass]
public class ServiceCollectionExtensionsTests
{
	[TestMethod]
	public void AddImGui_RegistersDefaultProviderAsSingleton()
	{
		ServiceCollection services = new();
		services.AddImGui();

		ServiceDescriptor? descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IImGuiProvider));
		Assert.IsNotNull(descriptor);
		Assert.AreEqual(typeof(HexaNetImGuiProvider), descriptor.ImplementationType);
		Assert.AreEqual(ServiceLifetime.Singleton, descriptor.Lifetime);
	}

	[TestMethod]
	public void AddImGui_RegistersImGuiContextAsTransient()
	{
		ServiceCollection services = new();
		services.AddImGui();

		ServiceDescriptor? descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(ImGuiContext));
		Assert.IsNotNull(descriptor);
		Assert.AreEqual(ServiceLifetime.Transient, descriptor.Lifetime);
	}

	[TestMethod]
	public void AddImGui_ReturnsSameServiceCollection()
	{
		ServiceCollection services = new();
		IServiceCollection result = services.AddImGui();

		Assert.AreSame(services, result);
	}

	[TestMethod]
	public void AddImGui_WithFactory_ResolvesFromFactory()
	{
		Mock<IImGuiProvider> mockProvider = new();
		ServiceCollection services = new();
		services.AddImGui(_ => mockProvider.Object);

		using ServiceProvider provider = services.BuildServiceProvider();
		IImGuiProvider result = provider.GetRequiredService<IImGuiProvider>();

		Assert.AreSame(mockProvider.Object, result);
	}

	[TestMethod]
	public void AddImGui_WithFactory_ProviderIsSingleton()
	{
		Mock<IImGuiProvider> mockProvider = new();
		ServiceCollection services = new();
		services.AddImGui(_ => mockProvider.Object);

		using ServiceProvider provider = services.BuildServiceProvider();
		IImGuiProvider first = provider.GetRequiredService<IImGuiProvider>();
		IImGuiProvider second = provider.GetRequiredService<IImGuiProvider>();

		Assert.AreSame(first, second);
	}

	[TestMethod]
	public void AddImGui_WithFactory_ContextIsTransient()
	{
		Mock<IImGuiProvider> mockProvider = new();
		ServiceCollection services = new();
		services.AddImGui(_ => mockProvider.Object);

		using ServiceProvider provider = services.BuildServiceProvider();
		ImGuiContext first = provider.GetRequiredService<ImGuiContext>();
		ImGuiContext second = provider.GetRequiredService<ImGuiContext>();

		Assert.AreNotSame(first, second);
	}

	[TestMethod]
	public void AddImGuiBackend_Generic_RegistersBackendType()
	{
		ServiceCollection services = new();
		services.AddImGuiBackend<MockBackend>();

		ServiceDescriptor? descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(MockBackend));
		Assert.IsNotNull(descriptor);
		Assert.AreEqual(typeof(MockBackend), descriptor.ImplementationType);
		Assert.AreEqual(ServiceLifetime.Transient, descriptor.Lifetime);

		using ServiceProvider provider = services.BuildServiceProvider();
		Assert.IsInstanceOfType<MockBackend>(provider.GetRequiredService<IImGuiBackend>());
		Assert.IsNull(provider.GetService<IPlatformBackend>());
		Assert.IsNull(provider.GetService<IRendererBackend>());
	}

	[TestMethod]
	public void AddImGuiBackend_Generic_ResolvesPlatformAndRendererBackendsAsTheReadmeShows()
	{
		ServiceCollection services = new();
		services.AddImGuiBackend<MockPlatformBackend>();
		services.AddImGuiBackend<MockRendererBackend>();

		using ServiceProvider provider = services.BuildServiceProvider();

		Assert.IsInstanceOfType<MockPlatformBackend>(provider.GetRequiredService<IPlatformBackend>());
		Assert.IsInstanceOfType<MockRendererBackend>(provider.GetRequiredService<IRendererBackend>());
		Assert.IsInstanceOfType<MockPlatformBackend>(provider.GetRequiredService<MockPlatformBackend>());
		Assert.IsInstanceOfType<MockRendererBackend>(provider.GetRequiredService<MockRendererBackend>());

		IImGuiBackend[] all = [.. provider.GetServices<IImGuiBackend>()];
		Assert.HasCount(2, all);
		Assert.IsTrue(all.Any(b => b is MockPlatformBackend));
		Assert.IsTrue(all.Any(b => b is MockRendererBackend));
	}

	[TestMethod]
	public void AddImGuiBackend_WithFactory_ResolvesPlatformAndRendererBackendsAsTheReadmeShows()
	{
		using MockPlatformBackend platform = new();
		using MockRendererBackend renderer = new();
		ServiceCollection services = new();
		services.AddImGuiBackend(_ => platform);
		services.AddImGuiBackend(_ => renderer);

		using ServiceProvider provider = services.BuildServiceProvider();

		Assert.AreSame(platform, provider.GetRequiredService<IPlatformBackend>());
		Assert.AreSame(renderer, provider.GetRequiredService<IRendererBackend>());
		Assert.AreSame(platform, provider.GetRequiredService<MockPlatformBackend>());
		Assert.AreSame(renderer, provider.GetRequiredService<MockRendererBackend>());

		IImGuiBackend[] all = [.. provider.GetServices<IImGuiBackend>()];
		Assert.HasCount(2, all);
		Assert.IsTrue(all.Contains(platform));
		Assert.IsTrue(all.Contains(renderer));
	}

	[TestMethod]
	public void AddImGuiBackend_WithFactoryTypedAsAnInterface_ResolvesWithoutForwardingToItself()
	{
		using MockPlatformBackend platform = new();
		using MockRendererBackend renderer = new();
		using MockBackend plain = new();
		ServiceCollection services = new();
		services.AddImGuiBackend<IPlatformBackend>(_ => platform);
		services.AddImGuiBackend<IRendererBackend>(_ => renderer);
		services.AddImGuiBackend<IImGuiBackend>(_ => plain);

		using ServiceProvider provider = services.BuildServiceProvider();

		Assert.AreSame(platform, provider.GetRequiredService<IPlatformBackend>());
		Assert.AreSame(renderer, provider.GetRequiredService<IRendererBackend>());

		IImGuiBackend[] all = [.. provider.GetServices<IImGuiBackend>()];
		Assert.HasCount(3, all);
		Assert.IsTrue(all.Contains(platform));
		Assert.IsTrue(all.Contains(renderer));
		Assert.IsTrue(all.Contains(plain));
	}

	[TestMethod]
	public void AddImGuiBackend_ReturnsSameServiceCollection()
	{
		ServiceCollection services = new();
		IServiceCollection result = services.AddImGuiBackend<MockBackend>();

		Assert.AreSame(services, result);
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by DI container")]
	private sealed class MockBackend : IImGuiBackend
	{
		public string Name => "Mock";
		public bool Initialize() => true;
		public void Shutdown() { }
		public void NewFrame() { }
		public void RenderDrawData(nint drawData) { }
		public void SetCurrentContext(nint context) { }
		public void Dispose() { }
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by DI container")]
	private sealed class MockPlatformBackend : IPlatformBackend
	{
		public string Name => "MockPlatform";
		public bool Initialize() => true;
		public void Shutdown() { }
		public void NewFrame() { }
		public void RenderDrawData(nint drawData) { }
		public void SetCurrentContext(nint context) { }
		public void ProcessEvents() { }
		public void Dispose() { }
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by DI container")]
	private sealed class MockRendererBackend : IRendererBackend
	{
		public string Name => "MockRenderer";
		public bool Initialize() => true;
		public void Shutdown() { }
		public void NewFrame() { }
		public void RenderDrawData(nint drawData) { }
		public void SetCurrentContext(nint context) { }
		public bool CreateDeviceObjects() => true;
		public void InvalidateDeviceObjects() { }
		public void Dispose() { }
	}
}
