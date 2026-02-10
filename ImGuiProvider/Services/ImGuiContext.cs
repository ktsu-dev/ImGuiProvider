// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ImGuiProvider.Services;

using ImGuiProvider.Interfaces;

/// <summary>
/// Manages ImGui context lifecycle and provides high-level operations
/// </summary>
public class ImGuiContext(IImGuiProvider provider) : IDisposable
{
	private readonly IImGuiProvider _provider = Ensure.NotNull(provider);
	private readonly List<IImGuiBackend> _backends = [];
	private bool _disposed;

	/// <summary>
	/// Gets the underlying ImGui context handle
	/// </summary>
	public nint Context { get; private set; }

	/// <summary>
	/// Gets whether the context has been initialized
	/// </summary>
	public bool IsInitialized => Context != nint.Zero;

	/// <summary>
	/// Initialize the ImGui context
	/// </summary>
	/// <returns>True if initialization was successful</returns>
	public bool Initialize()
	{
		if (Context != nint.Zero)
		{
			return true; // Already initialized
		}

		Context = _provider.CreateContext();
		if (Context == nint.Zero)
		{
			return false;
		}

		_provider.SetCurrentContext(Context);
		return true;
	}

	/// <summary>
	/// Add a backend to this context
	/// </summary>
	/// <param name="backend">Backend to add</param>
	public void AddBackend(IImGuiBackend backend)
	{
		Ensure.NotNull(backend);

		_backends.Add(backend);

		if (Context != nint.Zero)
		{
			backend.SetCurrentContext(Context);
		}
	}

	/// <summary>
	/// Remove a backend from this context
	/// </summary>
	/// <param name="backend">Backend to remove</param>
	public void RemoveBackend(IImGuiBackend backend)
	{
		if (backend != null)
		{
			_backends.Remove(backend);
		}
	}

	/// <summary>
	/// Initialize all backends
	/// </summary>
	/// <returns>True if all backends initialized successfully</returns>
	public bool InitializeBackends()
	{
		foreach (IImGuiBackend backend in _backends)
		{
			if (Context != nint.Zero)
			{
				backend.SetCurrentContext(Context);
			}

			if (!backend.Initialize())
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Begin a new ImGui frame
	/// </summary>
	public void BeginFrame()
	{
		if (Context == nint.Zero)
		{
			throw new InvalidOperationException("Context not initialized");
		}

		_provider.SetCurrentContext(Context);

		// Call NewFrame on all backends first
		foreach (IImGuiBackend backend in _backends)
		{
			backend.NewFrame();
		}

		// Then call ImGui NewFrame
		_provider.NewFrame();
	}

	/// <summary>
	/// End the current ImGui frame and render
	/// </summary>
	public void EndFrame()
	{
		if (Context == nint.Zero)
		{
			throw new InvalidOperationException("Context not initialized");
		}

		_provider.SetCurrentContext(Context);
		_provider.Render();

		nint drawData = _provider.GetDrawData();

		// Render with all backends
		foreach (IImGuiBackend backend in _backends)
		{
			backend.RenderDrawData(drawData);
		}

		// Handle multi-viewport rendering
		_provider.UpdatePlatformWindows();
		_provider.RenderPlatformWindowsDefault();
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
				// Shutdown all backends
				foreach (IImGuiBackend backend in _backends)
				{
					backend.Shutdown();
					backend.Dispose();
				}
				_backends.Clear();

				// Destroy context
				if (Context != nint.Zero)
				{
					_provider.DestroyContext(Context);
					Context = nint.Zero;
				}

				// Dispose provider
				_provider.Dispose();
			}

			_disposed = true;
		}
	}
}
