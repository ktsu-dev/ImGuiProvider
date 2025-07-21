using System.Numerics;
using System.Runtime.CompilerServices;
using ImGuiProvider.Interfaces;
using Hexa.NET.ImGui;

namespace ImGuiProvider.Implementations.HexaNet
{
    /// <summary>
    /// Implementation of IImGuiProvider using Hexa.NET.ImGui
    /// </summary>
    public unsafe class HexaNetImGuiProvider : IImGuiProvider
    {
        private bool _disposed = false;

        /// <inheritdoc />
        public nint CreateContext()
        {
            var context = ImGui.CreateContext();
            return context.Handle;
        }

        /// <inheritdoc />
        public void DestroyContext(nint context)
        {
            if (context != nint.Zero)
            {
                ImGui.DestroyContext(new ImGuiContextPtr(context));
            }
        }

        /// <inheritdoc />
        public nint GetCurrentContext()
        {
            var context = ImGui.GetCurrentContext();
            return context.Handle;
        }

        /// <inheritdoc />
        public void SetCurrentContext(nint context)
        {
            ImGui.SetCurrentContext(new ImGuiContextPtr(context));
        }

        /// <inheritdoc />
        public void NewFrame()
        {
            ImGui.NewFrame();
        }

        /// <inheritdoc />
        public void EndFrame()
        {
            ImGui.EndFrame();
        }

        /// <inheritdoc />
        public void Render()
        {
            ImGui.Render();
        }

        /// <inheritdoc />
        public nint GetDrawData()
        {
            var drawData = ImGui.GetDrawData();
            return drawData.Handle;
        }

        /// <inheritdoc />
        public nint GetIO()
        {
            var io = ImGui.GetIO();
            return (nint)(&io);
        }

        /// <inheritdoc />
        public nint GetStyle()
        {
            var style = ImGui.GetStyle();
            return (nint)style;
        }

        /// <inheritdoc />
        public void StyleColorsDark()
        {
            ImGui.StyleColorsDark();
        }

        /// <inheritdoc />
        public void StyleColorsLight()
        {
            ImGui.StyleColorsLight();
        }

        /// <inheritdoc />
        public void StyleColorsClassic()
        {
            ImGui.StyleColorsClassic();
        }

        /// <inheritdoc />
        public bool Begin(string name)
        {
            return ImGui.Begin(name);
        }

        /// <inheritdoc />
        public bool Begin(string name, bool* pOpen)
        {
            return ImGui.Begin(name, pOpen);
        }

        /// <inheritdoc />
        public void End()
        {
            ImGui.End();
        }

        /// <inheritdoc />
        public void ShowDemoWindow()
        {
            ImGui.ShowDemoWindow();
        }

        /// <inheritdoc />
        public void ShowDemoWindow(bool* pOpen)
        {
            ImGui.ShowDemoWindow(pOpen);
        }

        /// <inheritdoc />
        public void ShowMetricsWindow(bool* pOpen)
        {
            ImGui.ShowMetricsWindow(pOpen);
        }

        /// <inheritdoc />
        public void Text(string text)
        {
            ImGui.Text(text);
        }

        /// <inheritdoc />
        public bool Button(string label)
        {
            return ImGui.Button(label);
        }

        /// <inheritdoc />
        public bool Button(string label, Vector2 size)
        {
            return ImGui.Button(label, size);
        }

        /// <inheritdoc />
        public void Separator()
        {
            ImGui.Separator();
        }

        /// <inheritdoc />
        public void SameLine()
        {
            ImGui.SameLine();
        }

        /// <inheritdoc />
        public void Spacing()
        {
            ImGui.Spacing();
        }

        /// <inheritdoc />
        public void UpdatePlatformWindows()
        {
            ImGui.UpdatePlatformWindows();
        }

        /// <inheritdoc />
        public void RenderPlatformWindowsDefault()
        {
            ImGui.RenderPlatformWindowsDefault();
        }

        /// <inheritdoc />
        public string GetVersion()
        {
            return ImGui.GetVersion();
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
                    // Cleanup managed resources if needed
                }

                _disposed = true;
            }
        }
    }
}
