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
	private readonly HashSet<IImGuiBackend> _initializedBackends = new(ReferenceEqualityComparer.Instance);
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
	/// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
	public bool Initialize()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

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
	/// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
	public void AddBackend(IImGuiBackend backend)
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		Ensure.NotNull(backend);

		_backends.Add(backend);

		if (Context != nint.Zero)
		{
			backend.SetCurrentContext(Context);
		}
	}

	/// <summary>
	/// Remove a backend from this context, shutting it down first if this context initialized it
	/// </summary>
	/// <remarks>
	/// A backend that <see cref="InitializeBackends"/> initialized leaves state on the native
	/// context, which Dear ImGui asserts is gone when the context is destroyed. Removing such a
	/// backend therefore calls <see cref="IImGuiBackend.Shutdown"/> on it, with this context
	/// current, so a later <see cref="Dispose()"/> or a replacement backend's initialization
	/// does not fail. The backend is not disposed: its owner, usually the DI container, disposes it.
	/// </remarks>
	/// <param name="backend">Backend to remove</param>
	public void RemoveBackend(IImGuiBackend backend)
	{
		if (backend == null || !_backends.Remove(backend))
		{
			return;
		}

		if (_initializedBackends.Remove(backend))
		{
			if (Context != nint.Zero)
			{
				_provider.SetCurrentContext(Context);
				backend.SetCurrentContext(Context);
			}

			backend.Shutdown();
		}
	}

	/// <summary>
	/// Initialize all backends
	/// </summary>
	/// <returns>True if all backends initialized successfully</returns>
	/// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
	public bool InitializeBackends()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

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

			_initializedBackends.Add(backend);
		}
		return true;
	}

	/// <summary>
	/// Begin a new ImGui frame
	/// </summary>
	/// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
	public void BeginFrame()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

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
	/// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
	public void EndFrame()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

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
				ShutdownBackendsAndDestroyContext();
			}

			_disposed = true;
		}
	}

	/// <summary>
	/// Shuts the backends down and destroys the native context, with this context current.
	/// Backends shut down through the current ImGui context, like BeginFrame and EndFrame talk to
	/// it, so disposing this context while another one on the shared provider was current used to
	/// tear down that one's backend state. Whichever other context was current is current again
	/// afterwards.
	/// </summary>
	private void ShutdownBackendsAndDestroyContext()
	{
		nint destroyed = Context;
		nint previous = nint.Zero;
		if (destroyed != nint.Zero)
		{
			previous = _provider.GetCurrentContext();
			_provider.SetCurrentContext(destroyed);
		}

		// Detach the backends. They are not disposed: their owner, usually the DI container,
		// disposes them, and disposing them here as well disposed each one twice.
		foreach (IImGuiBackend backend in _backends)
		{
			backend.Shutdown();
		}
		_backends.Clear();
		_initializedBackends.Clear();

		if (destroyed == nint.Zero)
		{
			return;
		}

		// Destroy the native context, which is the only thing this object created. The provider
		// is not disposed: it is shared by every context resolved from one container, so
		// disposing it here broke every other live context.
		_provider.DestroyContext(destroyed);
		Context = nint.Zero;

		if (previous != nint.Zero && previous != destroyed)
		{
			_provider.SetCurrentContext(previous);
		}
	}
}
