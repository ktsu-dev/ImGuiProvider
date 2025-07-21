using System.Numerics;

namespace ImGuiProvider.Interfaces
{
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
        nint CreateContext();

        /// <summary>
        /// Destroys the specified ImGui context
        /// </summary>
        /// <param name="context">The context to destroy</param>
        void DestroyContext(nint context);

        /// <summary>
        /// Gets the current ImGui context
        /// </summary>
        /// <returns>Current context pointer/handle</returns>
        nint GetCurrentContext();

        /// <summary>
        /// Sets the current ImGui context
        /// </summary>
        /// <param name="context">Context to set as current</param>
        void SetCurrentContext(nint context);

        // Frame Management
        /// <summary>
        /// Start a new ImGui frame
        /// </summary>
        void NewFrame();

        /// <summary>
        /// End the current frame (call after all UI code)
        /// </summary>
        void EndFrame();

        /// <summary>
        /// Render the ImGui draw data
        /// </summary>
        void Render();

        /// <summary>
        /// Get the draw data for custom rendering
        /// </summary>
        /// <returns>Draw data pointer</returns>
        nint GetDrawData();

        // IO and Input
        /// <summary>
        /// Get the ImGui IO structure
        /// </summary>
        /// <returns>IO structure pointer</returns>
        nint GetIO();

        // Styling
        /// <summary>
        /// Get the ImGui style structure
        /// </summary>
        /// <returns>Style structure pointer</returns>
        nint GetStyle();

        /// <summary>
        /// Apply dark theme styling
        /// </summary>
        void StyleColorsDark();

        /// <summary>
        /// Apply light theme styling
        /// </summary>
        void StyleColorsLight();

        /// <summary>
        /// Apply classic theme styling
        /// </summary>
        void StyleColorsClassic();

        // Windows
        /// <summary>
        /// Begin a new window
        /// </summary>
        /// <param name="name">Window name</param>
        /// <returns>True if window is visible and should be rendered</returns>
        bool Begin(string name);

        /// <summary>
        /// Begin a new window with open/close state
        /// </summary>
        /// <param name="name">Window name</param>
        /// <param name="pOpen">Pointer to bool for close button</param>
        /// <returns>True if window is visible and should be rendered</returns>
        unsafe bool Begin(string name, bool* pOpen);

        /// <summary>
        /// End the current window
        /// </summary>
        void End();

        // Demo and Debug Windows
        /// <summary>
        /// Show the ImGui demo window
        /// </summary>
        void ShowDemoWindow();

        /// <summary>
        /// Show the ImGui demo window with open/close state
        /// </summary>
        /// <param name="pOpen">Pointer to bool for close button</param>
        unsafe void ShowDemoWindow(bool* pOpen);

        /// <summary>
        /// Show the ImGui metrics window
        /// </summary>
        /// <param name="pOpen">Pointer to bool for close button</param>
        unsafe void ShowMetricsWindow(bool* pOpen);

        // Basic Widgets
        /// <summary>
        /// Display text
        /// </summary>
        /// <param name="text">Text to display</param>
        void Text(string text);

        /// <summary>
        /// Display a button
        /// </summary>
        /// <param name="label">Button label</param>
        /// <returns>True if button was clicked</returns>
        bool Button(string label);

        /// <summary>
        /// Display a button with custom size
        /// </summary>
        /// <param name="label">Button label</param>
        /// <param name="size">Button size</param>
        /// <returns>True if button was clicked</returns>
        bool Button(string label, Vector2 size);

        // Layout
        /// <summary>
        /// Add a separator line
        /// </summary>
        void Separator();

        /// <summary>
        /// Move to the same line as previous widget
        /// </summary>
        void SameLine();

        /// <summary>
        /// Add spacing
        /// </summary>
        void Spacing();

        // Platform/Viewport Support
        /// <summary>
        /// Update platform windows (multi-viewport support)
        /// </summary>
        void UpdatePlatformWindows();

        /// <summary>
        /// Render platform windows (multi-viewport support)
        /// </summary>
        void RenderPlatformWindowsDefault();

        // Version Info
        /// <summary>
        /// Get ImGui version string
        /// </summary>
        /// <returns>Version string</returns>
        string GetVersion();
    }
}
