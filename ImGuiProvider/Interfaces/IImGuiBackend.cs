// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ImGuiProvider.Interfaces;
/// <summary>
/// Provides abstraction over ImGui backend implementations (rendering, platform)
/// </summary>
public interface IImGuiBackend : IDisposable
{
	/// <summary>
	/// Gets the name of this backend implementation
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Initialize the backend
	/// </summary>
	/// <returns>True if initialization was successful</returns>
	public bool Initialize();

	/// <summary>
	/// Shutdown the backend
	/// </summary>
	public void Shutdown();

	/// <summary>
	/// Start a new frame for this backend
	/// </summary>
	public void NewFrame();

	/// <summary>
	/// Render the draw data with this backend
	/// </summary>
	/// <param name="drawData">ImGui draw data pointer</param>
	public void RenderDrawData(nint drawData);

	/// <summary>
	/// Set the current ImGui context for this backend
	/// </summary>
	/// <param name="context">Context pointer</param>
	public void SetCurrentContext(nint context);
}

/// <summary>
/// Represents a platform backend (handles input, windowing)
/// </summary>
public interface IPlatformBackend : IImGuiBackend
{
	/// <summary>
	/// Process platform events and update ImGui input state
	/// </summary>
	public void ProcessEvents();
}

/// <summary>
/// Represents a renderer backend (handles graphics API calls)
/// </summary>
public interface IRendererBackend : IImGuiBackend
{
	/// <summary>
	/// Create device objects (textures, buffers, etc.)
	/// </summary>
	/// <returns>True if creation was successful</returns>
	public bool CreateDeviceObjects();

	/// <summary>
	/// Invalidate device objects
	/// </summary>
	public void InvalidateDeviceObjects();
}
