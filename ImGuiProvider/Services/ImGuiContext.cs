// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ImGuiProvider.Services;

using ImGuiProvider.Interfaces;

/// <summary>
/// Manages ImGui context lifecycle and provides high-level operations
/// </summary>
/// <remarks>
/// The context owns only the native ImGui context it creates. It does not own the
/// <see cref="IImGuiProvider"/> it is given or the backends added to it: the provider is shared by
/// every context resolved from one container, and both are disposed by whoever created them, which
/// is the DI container when they come from <c>AddImGui</c> and <c>AddImGuiBackend</c>.
/// </remarks>
public class ImGuiContext(IImGuiProvider provider) : IDisposable
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "The provider is shared by every context and owned by whoever created it, usually the DI container")]
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

	/// <summary>
	/// Shuts down the attached backends and destroys the native ImGui context. The provider and
	/// the backends are not disposed; their owner disposes them.
	/// </summary>
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
				// Detach the backends. They are not disposed: their owner, usually the DI container,
				// disposes them, and disposing them here as well disposed each one twice.
				foreach (IImGuiBackend backend in _backends)
				{
					backend.Shutdown();
				}
				_backends.Clear();

				// Destroy the native context, which is the only thing this object created
				if (Context != nint.Zero)
				{
					_provider.DestroyContext(Context);
					Context = nint.Zero;
				}

				// The provider is not disposed: it is shared by every context resolved from one
				// container, so disposing it here broke every other live context.
			}

			_disposed = true;
		}
	}
}
