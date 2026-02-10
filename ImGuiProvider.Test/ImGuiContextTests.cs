// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ImGuiProvider.Test;

using ImGuiProvider.Interfaces;
using ImGuiProvider.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

/// <summary>
/// Tests for <see cref="ImGuiContext"/> lifecycle management.
/// All tests use Moq to mock IImGuiProvider and IImGuiBackend,
/// avoiding any native ImGui library dependencies.
/// </summary>
[TestClass]
public class ImGuiContextTests
{
	private Mock<IImGuiProvider> _mockProvider = null!;

	[TestInitialize]
	public void Setup() => _mockProvider = new Mock<IImGuiProvider>();

	[TestMethod]
	public void Constructor_ThrowsOnNullProvider() =>
		Assert.ThrowsExactly<ArgumentNullException>(() => new ImGuiContext(null!));

	[TestMethod]
	public void IsInitialized_FalseByDefault()
	{
		using ImGuiContext context = new(_mockProvider.Object);
		Assert.IsFalse(context.IsInitialized);
	}

	[TestMethod]
	public void Initialize_CreatesContextViaProvider()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);

		using ImGuiContext context = new(_mockProvider.Object);
		bool result = context.Initialize();

		Assert.IsTrue(result);
		Assert.IsTrue(context.IsInitialized);
		_mockProvider.Verify(p => p.CreateContext(), Times.Once);
		_mockProvider.Verify(p => p.SetCurrentContext(fakeContext), Times.Once);
	}

	[TestMethod]
	public void Initialize_ReturnsFalseWhenProviderReturnsZero()
	{
		_mockProvider.Setup(p => p.CreateContext()).Returns(nint.Zero);

		using ImGuiContext context = new(_mockProvider.Object);
		bool result = context.Initialize();

		Assert.IsFalse(result);
		Assert.IsFalse(context.IsInitialized);
	}

	[TestMethod]
	public void Initialize_ReturnsTrueIfAlreadyInitialized()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);

		using ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		bool result = context.Initialize();

		Assert.IsTrue(result);
		_mockProvider.Verify(p => p.CreateContext(), Times.Once);
	}

	[TestMethod]
	public void AddBackend_SetsContextOnBackendWhenInitialized()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);
		Mock<IImGuiBackend> mockBackend = new();

		using ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		context.AddBackend(mockBackend.Object);

		mockBackend.Verify(b => b.SetCurrentContext(fakeContext), Times.Once);
	}

	[TestMethod]
	public void AddBackend_ThrowsOnNull()
	{
		using ImGuiContext context = new(_mockProvider.Object);
		Assert.ThrowsExactly<ArgumentNullException>(() => context.AddBackend(null!));
	}

	[TestMethod]
	public void AddBackend_DoesNotSetContextWhenNotInitialized()
	{
		Mock<IImGuiBackend> mockBackend = new();

		using ImGuiContext context = new(_mockProvider.Object);
		context.AddBackend(mockBackend.Object);

		mockBackend.Verify(b => b.SetCurrentContext(It.IsAny<nint>()), Times.Never);
	}

	[TestMethod]
	public void InitializeBackends_CallsInitializeOnAllBackends()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);
		Mock<IImGuiBackend> backend1 = new();
		Mock<IImGuiBackend> backend2 = new();
		backend1.Setup(b => b.Initialize()).Returns(true);
		backend2.Setup(b => b.Initialize()).Returns(true);

		using ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		context.AddBackend(backend1.Object);
		context.AddBackend(backend2.Object);
		bool result = context.InitializeBackends();

		Assert.IsTrue(result);
		backend1.Verify(b => b.Initialize(), Times.Once);
		backend2.Verify(b => b.Initialize(), Times.Once);
	}

	[TestMethod]
	public void InitializeBackends_ReturnsFalseIfAnyFails()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);
		Mock<IImGuiBackend> backend1 = new();
		Mock<IImGuiBackend> backend2 = new();
		backend1.Setup(b => b.Initialize()).Returns(true);
		backend2.Setup(b => b.Initialize()).Returns(false);

		using ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		context.AddBackend(backend1.Object);
		context.AddBackend(backend2.Object);
		bool result = context.InitializeBackends();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void InitializeBackends_WithNoBackends_ReturnsTrue()
	{
		using ImGuiContext context = new(_mockProvider.Object);
		bool result = context.InitializeBackends();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void BeginFrame_ThrowsIfNotInitialized()
	{
		using ImGuiContext context = new(_mockProvider.Object);
		Assert.ThrowsExactly<InvalidOperationException>(context.BeginFrame);
	}

	[TestMethod]
	public void BeginFrame_CallsBackendsBeforeProvider()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);
		Mock<IImGuiBackend> mockBackend = new();
		int callOrder = 0;
		int backendNewFrameOrder = 0;
		int providerNewFrameOrder = 0;
		mockBackend.Setup(b => b.NewFrame()).Callback(() => backendNewFrameOrder = ++callOrder);
		_mockProvider.Setup(p => p.NewFrame()).Callback(() => providerNewFrameOrder = ++callOrder);

		using ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		context.AddBackend(mockBackend.Object);
		context.BeginFrame();

		Assert.IsTrue(backendNewFrameOrder < providerNewFrameOrder,
			"Backend.NewFrame must be called before Provider.NewFrame");
	}

	[TestMethod]
	public void BeginFrame_SetsCurrentContext()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);

		using ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		_mockProvider.Invocations.Clear();
		context.BeginFrame();

		_mockProvider.Verify(p => p.SetCurrentContext(fakeContext), Times.Once);
	}

	[TestMethod]
	public void EndFrame_ThrowsIfNotInitialized()
	{
		using ImGuiContext context = new(_mockProvider.Object);
		Assert.ThrowsExactly<InvalidOperationException>(context.EndFrame);
	}

	[TestMethod]
	public void EndFrame_CallsRenderAndDrawData()
	{
		nint fakeContext = 0x1234;
		nint fakeDrawData = 0x5678;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);
		_mockProvider.Setup(p => p.GetDrawData()).Returns(fakeDrawData);
		Mock<IImGuiBackend> mockBackend = new();

		using ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		context.AddBackend(mockBackend.Object);
		context.EndFrame();

		_mockProvider.Verify(p => p.Render(), Times.Once);
		_mockProvider.Verify(p => p.GetDrawData(), Times.Once);
		mockBackend.Verify(b => b.RenderDrawData(fakeDrawData), Times.Once);
		_mockProvider.Verify(p => p.UpdatePlatformWindows(), Times.Once);
		_mockProvider.Verify(p => p.RenderPlatformWindowsDefault(), Times.Once);
	}

	[TestMethod]
	public void Dispose_ShutsDownAndDisposesBackends()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);
		Mock<IImGuiBackend> mockBackend = new();

		ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		context.AddBackend(mockBackend.Object);
		context.Dispose();

		mockBackend.Verify(b => b.Shutdown(), Times.Once);
		mockBackend.Verify(b => b.Dispose(), Times.Once);
		_mockProvider.Verify(p => p.DestroyContext(fakeContext), Times.Once);
		_mockProvider.Verify(p => p.Dispose(), Times.Once);
	}

	[TestMethod]
	public void Dispose_SafeToCallTwice()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);

		ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		context.Dispose();
		context.Dispose();

		_mockProvider.Verify(p => p.DestroyContext(fakeContext), Times.Once);
		_mockProvider.Verify(p => p.Dispose(), Times.Once);
	}

	[TestMethod]
	public void RemoveBackend_RemovesFromList()
	{
		nint fakeContext = 0x1234;
		_mockProvider.Setup(p => p.CreateContext()).Returns(fakeContext);
		Mock<IImGuiBackend> mockBackend = new();
		mockBackend.Setup(b => b.Initialize()).Returns(true);

		using ImGuiContext context = new(_mockProvider.Object);
		context.Initialize();
		context.AddBackend(mockBackend.Object);
		context.RemoveBackend(mockBackend.Object);
		context.InitializeBackends();

		mockBackend.Verify(b => b.Initialize(), Times.Never);
	}

	[TestMethod]
	public void RemoveBackend_NullDoesNotThrow()
	{
		using ImGuiContext context = new(_mockProvider.Object);
		context.RemoveBackend(null!);
	}
}
