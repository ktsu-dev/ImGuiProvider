// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

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

		ServiceDescriptor? descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IImGuiBackend));
		Assert.IsNotNull(descriptor);
		Assert.AreEqual(typeof(MockBackend), descriptor.ImplementationType);
		Assert.AreEqual(ServiceLifetime.Transient, descriptor.Lifetime);
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
}
