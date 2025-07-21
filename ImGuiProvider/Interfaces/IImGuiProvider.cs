// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ImGuiProvider.Interfaces;
using System.Numerics;

/// <summary>
/// Provides abstraction over ImGui library implementations for dependency injection
/// </summary>
public interface IImGuiProvider : IDisposable
{
	// Context Management
	/// <summary>
	/// Creates a new ImGui context
	/// </summary>
	/// <returns>Context pointer/handle</returns>
	public nint CreateContext();

	/// <summary>
	/// Destroys the specified ImGui context
	/// </summary>
	/// <param name="context">The context to destroy</param>
	public void DestroyContext(nint context);

	/// <summary>
	/// Gets the current ImGui context
	/// </summary>
	/// <returns>Current context pointer/handle</returns>
	public nint GetCurrentContext();

	/// <summary>
	/// Sets the current ImGui context
	/// </summary>
	/// <param name="context">Context to set as current</param>
	public void SetCurrentContext(nint context);

	// Frame Management
	/// <summary>
	/// Start a new ImGui frame
	/// </summary>
	public void NewFrame();

	/// <summary>
	/// End the current frame (call after all UI code)
	/// </summary>
	public void EndFrame();

	/// <summary>
	/// Render the ImGui draw data
	/// </summary>
	public void Render();

	/// <summary>
	/// Get the draw data for custom rendering
	/// </summary>
	/// <returns>Draw data pointer</returns>
	public nint GetDrawData();

	// IO and Input
	/// <summary>
	/// Get the ImGui IO structure
	/// </summary>
	/// <returns>IO structure pointer</returns>
	public nint GetIO();

	// Styling
	/// <summary>
	/// Get the ImGui style structure
	/// </summary>
	/// <returns>Style structure pointer</returns>
	public nint GetStyle();

	/// <summary>
	/// Apply dark theme styling
	/// </summary>
	public void StyleColorsDark();

	/// <summary>
	/// Apply light theme styling
	/// </summary>
	public void StyleColorsLight();

	/// <summary>
	/// Apply classic theme styling
	/// </summary>
	public void StyleColorsClassic();

	// Windows
	/// <summary>
	/// Begin a new window
	/// </summary>
	/// <param name="name">Window name</param>
	/// <returns>True if window is visible and should be rendered</returns>
	public bool Begin(string name);

	/// <summary>
	/// Begin a new window with open/close state
	/// </summary>
	/// <param name="name">Window name</param>
	/// <param name="pOpen">Pointer to bool for close button</param>
	/// <returns>True if window is visible and should be rendered</returns>
	public unsafe bool Begin(string name, bool* pOpen);

	/// <summary>
	/// End the current window
	/// </summary>
	public void EndWindow();

	// Demo and Debug Windows
	/// <summary>
	/// Show the ImGui demo window
	/// </summary>
	public void ShowDemoWindow();

	/// <summary>
	/// Show the ImGui demo window with open/close state
	/// </summary>
	/// <param name="pOpen">Pointer to bool for close button</param>
	public unsafe void ShowDemoWindow(bool* pOpen);

	/// <summary>
	/// Show the ImGui metrics window
	/// </summary>
	/// <param name="pOpen">Pointer to bool for close button</param>
	public unsafe void ShowMetricsWindow(bool* pOpen);

	// Basic Widgets
	/// <summary>
	/// Display text
	/// </summary>
	/// <param name="text">Text to display</param>
	public void Text(string text);

	/// <summary>
	/// Display a button
	/// </summary>
	/// <param name="label">Button label</param>
	/// <returns>True if button was clicked</returns>
	public bool Button(string label);

	/// <summary>
	/// Display a button with custom size
	/// </summary>
	/// <param name="label">Button label</param>
	/// <param name="size">Button size</param>
	/// <returns>True if button was clicked</returns>
	public bool Button(string label, Vector2 size);

	// Layout
	/// <summary>
	/// Add a separator line
	/// </summary>
	public void Separator();

	/// <summary>
	/// Move to the same line as previous widget
	/// </summary>
	public void SameLine();

	/// <summary>
	/// Add spacing
	/// </summary>
	public void Spacing();

	// Platform/Viewport Support
	/// <summary>
	/// Update platform windows (multi-viewport support)
	/// </summary>
	public void UpdatePlatformWindows();

	/// <summary>
	/// Render platform windows (multi-viewport support)
	/// </summary>
	public void RenderPlatformWindowsDefault();

	// Version Info
	/// <summary>
	/// Get ImGui version string
	/// </summary>
	/// <returns>Version string</returns>
	public string GetVersion();
}
