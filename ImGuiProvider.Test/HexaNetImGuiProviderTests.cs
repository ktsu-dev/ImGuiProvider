// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ImGuiProvider.Test;

using System.Numerics;
using Hexa.NET.ImGui;
using ImGuiProvider.Implementations.HexaNet;
using ImGuiProvider.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Tests that run <see cref="HexaNetImGuiProvider"/> against the native cimgui library,
/// covering the pointer and argument marshalling that a mocked provider cannot observe.
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

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Image_WithoutTint_DrawsUntinted()
	{
		uint[] colours = ImageVertexColoursAfter(provider => provider.Image(1, new Vector2(32, 32)));

		Assert.IsNotEmpty(colours);
		Assert.IsTrue(Array.TrueForAll(colours, colour => colour == ImGui.ColorConvertFloat4ToU32(Vector4.One)));
	}

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Image_WithTint_DrawsTinted()
	{
		Vector4 red = new(1, 0, 0, 1);

		uint[] colours = ImageVertexColoursAfter(provider => provider.Image(1, new Vector2(32, 32), tintCol: red));

		Assert.IsNotEmpty(colours);
		Assert.IsTrue(Array.TrueForAll(colours, colour => colour == ImGui.ColorConvertFloat4ToU32(red)));
	}

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Image_WithTintAndUvs_DrawsTinted()
	{
		Vector4 red = new(1, 0, 0, 1);

		uint[] colours = ImageVertexColoursAfter(provider =>
			provider.Image(1, new Vector2(32, 32), new Vector2(0.25f, 0.25f), new Vector2(0.75f, 0.75f), red));

		Assert.IsNotEmpty(colours);
		Assert.IsTrue(Array.TrueForAll(colours, colour => colour == ImGui.ColorConvertFloat4ToU32(red)));
	}

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Image_WithBorderColour_DrawsABorderInThatColour()
	{
		Vector4 green = new(0, 1, 0, 1);

		uint[] colours = ImageVertexColoursAfter(provider => provider.Image(1, new Vector2(32, 32), borderCol: green));

		Assert.Contains(ImGui.ColorConvertFloat4ToU32(green), colours);
	}

	[TestMethod]
	[Timeout(30000, CooperativeCancellation = true)]
	public void Image_WithBorderColour_RestoresTheStyle()
	{
		Vector4 green = new(0, 1, 0, 1);
		uint border = ImGui.ColorConvertFloat4ToU32(green);

		uint[] colours = ImageVertexColoursAfter(provider =>
		{
			provider.Image(1, new Vector2(32, 32), borderCol: green);
			provider.Image(1, new Vector2(32, 32));
		});

		// The second image is drawn after the first one's border, so none of its vertices can be green.
		int lastBorderVertex = Array.LastIndexOf(colours, border);
		Assert.IsTrue(lastBorderVertex >= 0);
		Assert.IsTrue(colours.Skip(lastBorderVertex + 1).Any());
		Assert.IsFalse(colours.Skip(lastBorderVertex + 1).Contains(border));
	}

	/// <summary>
	/// Native ImGui reads glyph ranges as <c>ImWchar</c>, which the Hexa binding builds 32 bits wide.
	/// A provider parameter of a narrower element type can only be passed on by reinterpreting the
	/// pointer, so native code reads each pair of 16-bit entries as one code point and runs past the
	/// end of the caller's buffer looking for the terminator.
	/// </summary>
	[TestMethod]
	[DataRow(nameof(IImGuiProvider.AddFontFromFileTTF))]
	[DataRow(nameof(IImGuiProvider.AddFontFromMemoryTTF))]
	public void AddFont_GlyphRangesMatchNativeImWcharWidth(string method)
	{
		Type provided = GlyphRangesElementType(typeof(IImGuiProvider).GetMethod(method)!);

		Type[] native = [.. typeof(ImFontAtlasPtr).GetMethods()
			.Where(m => m.Name == method)
			.Select(m => m.GetParameters().LastOrDefault())
			.Where(p => p is { Name: "glyphRanges" } && p.ParameterType.IsPointer)
			.Select(p => p!.ParameterType.GetElementType()!)
			.Distinct()];

		Assert.HasCount(1, native, $"Hexa's {method} overloads disagree on the glyph range type");
		Assert.AreEqual(native[0], provided, $"{method} glyph ranges must use the native ImWchar width");
		Assert.AreEqual(
			typeof(HexaNetImGuiProvider).GetMethod(method)!.GetParameters().Last().ParameterType,
			typeof(IImGuiProvider).GetMethod(method)!.GetParameters().Last().ParameterType);
	}

	private static Type GlyphRangesElementType(System.Reflection.MethodInfo method)
	{
		System.Reflection.ParameterInfo parameter = method.GetParameters().Single(p => p.Name == "glyphRanges");
		Assert.IsTrue(parameter.ParameterType.IsPointer, $"{method.Name}: glyphRanges is not a pointer");
		return parameter.ParameterType.GetElementType()!;
	}

	/// <summary>
	/// Runs one frame and returns the colours of the vertices <paramref name="draw"/> added to the window.
	/// </summary>
	private static unsafe uint[] ImageVertexColoursAfter(Action<HexaNetImGuiProvider> draw)
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
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();
			int before = drawList.VtxBuffer.Size;
			draw(provider);
			uint[] colours = new uint[drawList.VtxBuffer.Size - before];
			for (int i = 0; i < colours.Length; i++)
			{
				colours[i] = drawList.VtxBuffer.Data[before + i].Col;
			}

			provider.EndWindow();
			provider.EndFrame();

			return colours;
		}
		finally
		{
			provider.DestroyContext(context);
		}
	}

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
