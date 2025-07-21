// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ImGuiProvider.Implementations.HexaNet;
using System.Numerics;
using Hexa.NET.ImGui;
using ImGuiProvider.Interfaces;

/// <summary>
/// Implementation of IImGuiProvider using Hexa.NET.ImGui
/// </summary>
public unsafe class HexaNetImGuiProvider : IImGuiProvider
{
	private bool _disposed;

	/// <inheritdoc />
	public nint CreateContext()
	{
		ImGuiContextPtr context = ImGui.CreateContext();
		return (nint)context.Handle;
	}

	/// <inheritdoc />
	public void DestroyContext(nint context)
	{
		if (context != nint.Zero)
		{
			ImGui.DestroyContext((ImGuiContext*)context);
		}
	}

	/// <inheritdoc />
	public nint GetCurrentContext()
	{
		ImGuiContextPtr context = ImGui.GetCurrentContext();
		return (nint)context.Handle;
	}

	/// <inheritdoc />
	public void SetCurrentContext(nint context) => ImGui.SetCurrentContext((ImGuiContext*)context);

	/// <inheritdoc />
	public void NewFrame() => ImGui.NewFrame();

	/// <inheritdoc />
	public void EndFrame() => ImGui.EndFrame();

	/// <inheritdoc />
	public void Render() => ImGui.Render();

	/// <inheritdoc />
	public nint GetDrawData()
	{
		ImDrawDataPtr drawData = ImGui.GetDrawData();
		return (nint)drawData.Handle;
	}

	/// <inheritdoc />
	public nint GetIO()
	{
		ImGuiIOPtr io = ImGui.GetIO();
		return (nint)(&io);
	}

	/// <inheritdoc />
	public nint GetStyle()
	{
		ImGuiStylePtr style = ImGui.GetStyle();
		return (nint)(&style);
	}

	/// <inheritdoc />
	public void StyleColorsDark() => ImGui.StyleColorsDark();

	/// <inheritdoc />
	public void StyleColorsLight() => ImGui.StyleColorsLight();

	/// <inheritdoc />
	public void StyleColorsClassic() => ImGui.StyleColorsClassic();

	/// <inheritdoc />
	public bool Begin(string name) => ImGui.Begin(name);

	/// <inheritdoc />
	public bool Begin(string name, bool* pOpen) => ImGui.Begin(name, pOpen);

	/// <inheritdoc />
	public void EndWindow() => ImGui.End();

	/// <inheritdoc />
	public void ShowDemoWindow() => ImGui.ShowDemoWindow();

	/// <inheritdoc />
	public void ShowDemoWindow(bool* pOpen) => ImGui.ShowDemoWindow(pOpen);

	/// <inheritdoc />
	public void ShowMetricsWindow(bool* pOpen) => ImGui.ShowMetricsWindow(pOpen);

	/// <inheritdoc />
	public void Text(string text) => ImGui.Text(text);

	/// <inheritdoc />
	public bool Button(string label) => ImGui.Button(label);

	/// <inheritdoc />
	public bool Button(string label, Vector2 size) => ImGui.Button(label, size);

	/// <inheritdoc />
	public void Separator() => ImGui.Separator();

	/// <inheritdoc />
	public void SameLine() => ImGui.SameLine();

	/// <inheritdoc />
	public void Spacing() => ImGui.Spacing();

	/// <inheritdoc />
	public void UpdatePlatformWindows() => ImGui.UpdatePlatformWindows();

	/// <inheritdoc />
	public void RenderPlatformWindowsDefault() => ImGui.RenderPlatformWindowsDefault();

	/// <inheritdoc />
	public string GetVersion()
	{
		byte* versionPtr = ImGui.GetVersion();
		return new string((sbyte*)versionPtr);
	}

	/// <inheritdoc />
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <inheritdoc/>
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				// Cleanup managed resources if needed
			}

			_disposed = true;
		}
	}
}
