using ImGuiProvider.Interfaces;

namespace ImGuiProvider.Services
{
    /// <summary>
    /// Manages ImGui context lifecycle and provides high-level operations
    /// </summary>
    public class ImGuiContext : IDisposable
    {
        private readonly IImGuiProvider _provider;
        private readonly List<IImGuiBackend> _backends;
        private nint _context;
        private bool _disposed = false;

        public ImGuiContext(IImGuiProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _backends = new List<IImGuiBackend>();
        }

        /// <summary>
        /// Gets the underlying ImGui context handle
        /// </summary>
        public nint Context => _context;

        /// <summary>
        /// Gets whether the context has been initialized
        /// </summary>
        public bool IsInitialized => _context != nint.Zero;

        /// <summary>
        /// Initialize the ImGui context
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        public bool Initialize()
        {
            if (_context != nint.Zero)
                return true; // Already initialized

            _context = _provider.CreateContext();
            if (_context == nint.Zero)
                return false;

            _provider.SetCurrentContext(_context);
            return true;
        }

        /// <summary>
        /// Add a backend to this context
        /// </summary>
        /// <param name="backend">Backend to add</param>
        public void AddBackend(IImGuiBackend backend)
        {
            if (backend == null)
                throw new ArgumentNullException(nameof(backend));

            _backends.Add(backend);

            if (_context != nint.Zero)
            {
                backend.SetCurrentContext(_context);
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
            foreach (var backend in _backends)
            {
                if (_context != nint.Zero)
                {
                    backend.SetCurrentContext(_context);
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
            if (_context == nint.Zero)
                throw new InvalidOperationException("Context not initialized");

            _provider.SetCurrentContext(_context);

            // Call NewFrame on all backends first
            foreach (var backend in _backends)
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
            if (_context == nint.Zero)
                throw new InvalidOperationException("Context not initialized");

            _provider.SetCurrentContext(_context);
            _provider.Render();

            var drawData = _provider.GetDrawData();

            // Render with all backends
            foreach (var backend in _backends)
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Shutdown all backends
                    foreach (var backend in _backends)
                    {
                        backend.Shutdown();
                        backend.Dispose();
                    }
                    _backends.Clear();

                    // Destroy context
                    if (_context != nint.Zero)
                    {
                        _provider.DestroyContext(_context);
                        _context = nint.Zero;
                    }
                }

                _disposed = true;
            }
        }
    }
}
