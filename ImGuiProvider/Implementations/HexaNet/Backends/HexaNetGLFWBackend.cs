using ImGuiProvider.Interfaces;
using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.GLFW;

namespace ImGuiProvider.Implementations.HexaNet.Backends
{
    /// <summary>
    /// GLFW platform backend implementation using Hexa.NET.ImGui.Backends.GLFW
    /// </summary>
    public unsafe class HexaNetGLFWBackend : IPlatformBackend
    {
        private bool _disposed = false;
        private bool _initialized = false;
        private readonly nint _windowHandle;
        private readonly bool _installCallbacks;

        public HexaNetGLFWBackend(nint windowHandle, bool installCallbacks = true)
        {
            _windowHandle = windowHandle;
            _installCallbacks = installCallbacks;
        }

        /// <inheritdoc />
        public string Name => "GLFW (Hexa.NET)";

        /// <inheritdoc />
        public bool Initialize()
        {
            if (_initialized)
                return true;

            var glfwWindow = new Hexa.NET.ImGui.Backends.GLFW.GLFWwindowPtr(_windowHandle);
            _initialized = ImGuiImplGLFW.InitForOpenGL(glfwWindow, installCallbacks: _installCallbacks);
            return _initialized;
        }

        /// <inheritdoc />
        public void Shutdown()
        {
            if (_initialized)
            {
                ImGuiImplGLFW.Shutdown();
                _initialized = false;
            }
        }

        /// <inheritdoc />
        public void NewFrame()
        {
            if (!_initialized)
                throw new InvalidOperationException("Backend not initialized");

            ImGuiImplGLFW.NewFrame();
        }

        /// <inheritdoc />
        public void RenderDrawData(nint drawData)
        {
            // Platform backends typically don't handle rendering
        }

        /// <inheritdoc />
        public void SetCurrentContext(nint context)
        {
            ImGuiImplGLFW.SetCurrentContext(new ImGuiContextPtr(context));
        }

        /// <inheritdoc />
        public void ProcessEvents()
        {
            // GLFW event processing would typically be handled by the application
            // This is a placeholder for any GLFW-specific event processing
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
