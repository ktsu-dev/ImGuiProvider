// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ImGuiProvider.Test;

using Hexa.NET.ImGui;
using ImGuiProvider.Implementations.HexaNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Tests that run <see cref="HexaNetImGuiProvider"/> against the native cimgui library,
/// covering the pointer marshalling that a mocked provider cannot observe.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class HexaNetImGuiProviderTests
{
	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public unsafe void GetIO_ReturnsNativeIOHandle()
	{
		using HexaNetImGuiProvider provider = new();
		nint context = provider.CreateContext();
		try
		{
			provider.SetCurrentContext(context);
			nint expected = (nint)ImGui.GetIO().Handle;

			Assert.AreEqual(expected, provider.GetIO());
		}
		finally
		{
			provider.DestroyContext(context);
		}
	}
}
