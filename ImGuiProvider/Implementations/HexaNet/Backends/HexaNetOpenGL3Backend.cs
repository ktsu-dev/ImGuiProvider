using ImGuiProvider.Interfaces;
using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends;

namespace ImGuiProvider.Implementations.HexaNet.Backends
{
    /// <summary>
    /// OpenGL3 renderer backend implementation using Hexa.NET.ImGui.Backends
    /// </summary>
    public class HexaNetOpenGL3Backend : IRendererBackend
    {
        private bool _disposed = false;
        private bool _initialized = false;
        private readonly string? _glslVersion;

        public HexaNetOpenGL3Backend(string? glslVersion = null)
        {
            _glslVersion = glslVersion;
        }

        /// <inheritdoc />
        public string Name => "OpenGL3 (Hexa.NET)";

        /// <inheritdoc />
        public bool Initialize()
        {
            if (_initialized)
                return true;

            _initialized = ImGuiImplOpenGL3.Init(_glslVersion);
            return _initialized;
        }

        /// <inheritdoc />
        public void Shutdown()
        {
            if (_initialized)
            {
                ImGuiImplOpenGL3.Shutdown();
                _initialized = false;
            }
        }

        /// <inheritdoc />
        public void NewFrame()
        {
            if (!_initialized)
                throw new InvalidOperationException("Backend not initialized");

            ImGuiImplOpenGL3.NewFrame();
        }

        /// <inheritdoc />
        public void RenderDrawData(nint drawData)
        {
            if (!_initialized)
                throw new InvalidOperationException("Backend not initialized");

            ImGuiImplOpenGL3.RenderDrawData(new ImDrawDataPtr(drawData));
        }

        /// <inheritdoc />
        public void SetCurrentContext(nint context)
        {
            ImGuiImplOpenGL3.SetCurrentContext(new ImGuiContextPtr(context));
        }

        /// <inheritdoc />
        public bool CreateDeviceObjects()
        {
            return ImGuiImplOpenGL3.CreateDeviceObjects();
        }

        /// <inheritdoc />
        public void InvalidateDeviceObjects()
        {
            ImGuiImplOpenGL3.DestroyDeviceObjects();
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
                    Shutdown();
                }

                _disposed = true;
            }
        }
    }
}
