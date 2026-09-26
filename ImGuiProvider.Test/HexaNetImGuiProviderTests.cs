// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ImGuiProvider.Test;

using System.Numerics;
using Hexa.NET.ImGui;
using ImGuiProvider.Implementations.HexaNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Tests that run <see cref="HexaNetImGuiProvider"/> against the native cimgui library,
/// covering the argument marshalling that a mocked provider cannot observe.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class HexaNetImGuiProviderTests
{
	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Columns_DefaultFlags_DrawsBorders() =>
		Assert.AreEqual(ImGuiOldColumnFlags.None, ColumnsFlagsAfter(provider => provider.Columns("c", 2)));

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Columns_NoBorderFlag_HidesBorders() =>
		Assert.AreEqual(ImGuiOldColumnFlags.NoBorder, ColumnsFlagsAfter(provider => provider.Columns("c", 2, (int)ImGuiOldColumnFlags.NoBorder)));

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Columns_PassesEveryFlagThrough()
	{
		ImGuiOldColumnFlags flags = ImGuiOldColumnFlags.NoResize | ImGuiOldColumnFlags.NoPreserveWidths;

		Assert.AreEqual(flags, ColumnsFlagsAfter(provider => provider.Columns("c", 2, (int)flags)));
	}

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Columns_RepeatedWithSameFlags_KeepsCurrentSet() =>
		Assert.AreEqual(ImGuiOldColumnFlags.NoBorder, ColumnsFlagsAfter(provider =>
		{
			provider.Columns("c", 2, (int)ImGuiOldColumnFlags.NoBorder);
			provider.Columns("c", 2, (int)ImGuiOldColumnFlags.NoBorder);
		}));

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Columns_ChangedFlags_ReplacesCurrentSet() =>
		Assert.AreEqual(ImGuiOldColumnFlags.None, ColumnsFlagsAfter(provider =>
		{
			provider.Columns("c", 2, (int)ImGuiOldColumnFlags.NoBorder);
			provider.Columns("c", 2);
		}));

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Columns_CountOfOne_EndsCurrentSet() =>
		Assert.IsNull(ColumnsFlagsAfter(provider =>
		{
			provider.Columns("c", 2);
			provider.Columns("c", 1);
		}));

	/// <summary>
	/// Runs <paramref name="columns"/> inside a window in a real frame, and returns the flags of the
	/// columns set it left current, or <see langword="null"/> when it left none.
	/// </summary>
	private static unsafe ImGuiOldColumnFlags? ColumnsFlagsAfter(Action<HexaNetImGuiProvider> columns)
	{
		using HexaNetImGuiProvider provider = new();
		nint context = provider.CreateContext();
		try
		{
			provider.SetCurrentContext(context);
			ImGuiIOPtr io = ImGui.GetIO();
			io.DisplaySize = new Vector2(800, 600);
			io.DeltaTime = 1f / 60f;
			io.BackendFlags |= ImGuiBackendFlags.RendererHasTextures;

			provider.NewFrame();
			provider.Begin("window");
			columns(provider);
			ImGuiOldColumns* current = ImGuiP.GetCurrentWindow().DC.CurrentColumns;
			ImGuiOldColumnFlags? flags = current == null ? null : current->Flags;
			provider.Columns(1);
			provider.EndWindow();
			provider.EndFrame();

			return flags;
		}
		finally
		{
			provider.DestroyContext(context);
		}
	}
}
