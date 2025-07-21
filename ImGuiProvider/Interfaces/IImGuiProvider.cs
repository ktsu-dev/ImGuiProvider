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
	/// Begin a new window with flags
	/// </summary>
	/// <param name="name">Window name</param>
	/// <param name="flags">Window flags</param>
	/// <returns>True if window is visible and should be rendered</returns>
	public bool Begin(string name, int flags);

	/// <summary>
	/// Begin a new window with open/close state
	/// </summary>
	/// <param name="name">Window name</param>
	/// <param name="pOpen">Pointer to bool for close button</param>
	/// <returns>True if window is visible and should be rendered</returns>
	public unsafe bool Begin(string name, bool* pOpen);

	/// <summary>
	/// Begin a new window with open/close state and flags
	/// </summary>
	/// <param name="name">Window name</param>
	/// <param name="pOpen">Pointer to bool for close button</param>
	/// <param name="flags">Window flags</param>
	/// <returns>True if window is visible and should be rendered</returns>
	public unsafe bool Begin(string name, bool* pOpen, int flags);

	/// <summary>
	/// End the current window
	/// </summary>
	public void EndWindow();

	// Child Windows
	/// <summary>
	/// Begin a scrolling region
	/// </summary>
	/// <param name="strId">String identifier</param>
	/// <param name="size">Size of the region</param>
	/// <param name="childFlags">Child flags</param>
	/// <param name="windowFlags">Window flags</param>
	/// <returns>True if region is visible</returns>
	public bool BeginChild(string strId, Vector2 size = default, int childFlags = 0, int windowFlags = 0);

	/// <summary>
	/// End the current child region
	/// </summary>
	public void EndChild();

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

	/// <summary>
	/// Show the ImGui debug log window
	/// </summary>
	/// <param name="pOpen">Pointer to bool for close button</param>
	public unsafe void ShowDebugLogWindow(bool* pOpen);

	/// <summary>
	/// Show the ImGui ID stack tool window
	/// </summary>
	/// <param name="pOpen">Pointer to bool for close button</param>
	public unsafe void ShowIDStackToolWindow(bool* pOpen);

	/// <summary>
	/// Show the ImGui about window
	/// </summary>
	/// <param name="pOpen">Pointer to bool for close button</param>
	public unsafe void ShowAboutWindow(bool* pOpen);

	/// <summary>
	/// Show the ImGui style editor
	/// </summary>
	/// <param name="style">Style structure pointer (optional)</param>
	public void ShowStyleEditor(nint style = 0);

	/// <summary>
	/// Show the ImGui style selector
	/// </summary>
	/// <param name="label">Label for the selector</param>
	/// <returns>True if style was changed</returns>
	public bool ShowStyleSelector(string label);

	/// <summary>
	/// Show the ImGui font selector
	/// </summary>
	/// <param name="label">Label for the selector</param>
	public void ShowFontSelector(string label);

	/// <summary>
	/// Show user guide
	/// </summary>
	public void ShowUserGuide();

	// Basic Widgets - Text
	/// <summary>
	/// Display text
	/// </summary>
	/// <param name="text">Text to display</param>
	public void Text(string text);

	/// <summary>
	/// Display colored text
	/// </summary>
	/// <param name="color">Text color</param>
	/// <param name="text">Text to display</param>
	public void TextColored(Vector4 color, string text);

	/// <summary>
	/// Display disabled text
	/// </summary>
	/// <param name="text">Text to display</param>
	public void TextDisabled(string text);

	/// <summary>
	/// Display wrapped text
	/// </summary>
	/// <param name="text">Text to display</param>
	public void TextWrapped(string text);

	/// <summary>
	/// Display bullet point text
	/// </summary>
	/// <param name="text">Text to display</param>
	public void BulletText(string text);

	/// <summary>
	/// Display labeled text (label: value)
	/// </summary>
	/// <param name="label">Label</param>
	/// <param name="text">Value text</param>
	public void LabelText(string label, string text);

	// Basic Widgets - Buttons
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

	/// <summary>
	/// Display a small button
	/// </summary>
	/// <param name="label">Button label</param>
	/// <returns>True if button was clicked</returns>
	public bool SmallButton(string label);

	/// <summary>
	/// Display an invisible button (for custom drawing)
	/// </summary>
	/// <param name="strId">String identifier</param>
	/// <param name="size">Button size</param>
	/// <param name="flags">Button flags</param>
	/// <returns>True if button was clicked</returns>
	public bool InvisibleButton(string strId, Vector2 size, int flags = 0);

	/// <summary>
	/// Display an arrow button
	/// </summary>
	/// <param name="strId">String identifier</param>
	/// <param name="dir">Arrow direction</param>
	/// <returns>True if button was clicked</returns>
	public bool ArrowButton(string strId, int dir);

	// Checkboxes
	/// <summary>
	/// Display a checkbox
	/// </summary>
	/// <param name="label">Checkbox label</param>
	/// <param name="v">Reference to bool value</param>
	/// <returns>True if checkbox was clicked</returns>
	public bool Checkbox(string label, ref bool v);

	/// <summary>
	/// Display checkbox flags
	/// </summary>
	/// <param name="label">Checkbox label</param>
	/// <param name="flags">Reference to flags value</param>
	/// <param name="flagsValue">Flag value to toggle</param>
	/// <returns>True if checkbox was clicked</returns>
	public bool CheckboxFlags(string label, ref int flags, int flagsValue);

	/// <summary>
	/// Display checkbox flags (uint version)
	/// </summary>
	/// <param name="label">Checkbox label</param>
	/// <param name="flags">Reference to flags value</param>
	/// <param name="flagsValue">Flag value to toggle</param>
	/// <returns>True if checkbox was clicked</returns>
	public bool CheckboxFlags(string label, ref uint flags, uint flagsValue);

	// Radio Buttons
	/// <summary>
	/// Display a radio button
	/// </summary>
	/// <param name="label">Radio button label</param>
	/// <param name="active">Whether this radio button is active</param>
	/// <returns>True if radio button was clicked</returns>
	public bool RadioButton(string label, bool active);

	/// <summary>
	/// Display a radio button with int value
	/// </summary>
	/// <param name="label">Radio button label</param>
	/// <param name="v">Reference to current value</param>
	/// <param name="vButton">Value for this radio button</param>
	/// <returns>True if radio button was clicked</returns>
	public bool RadioButton(string label, ref int v, int vButton);

	// Progress Bar
	/// <summary>
	/// Display a progress bar
	/// </summary>
	/// <param name="fraction">Progress fraction (0.0f to 1.0f)</param>
	/// <param name="sizeArg">Progress bar size</param>
	/// <param name="overlay">Overlay text</param>
	public void ProgressBar(float fraction, Vector2 sizeArg = default, string? overlay = null);

	/// <summary>
	/// Display a bullet point
	/// </summary>
	public void Bullet();

	// Images
	/// <summary>
	/// Display an image
	/// </summary>
	/// <param name="textureId">Texture ID</param>
	/// <param name="imageSize">Image size</param>
	/// <param name="uv0">UV coordinate for top-left</param>
	/// <param name="uv1">UV coordinate for bottom-right</param>
	/// <param name="tintCol">Tint color</param>
	/// <param name="borderCol">Border color</param>
	public void Image(nint textureId, Vector2 imageSize, Vector2 uv0 = default, Vector2 uv1 = default, Vector4 tintCol = default, Vector4 borderCol = default);

	/// <summary>
	/// Display a clickable image button
	/// </summary>
	/// <param name="strId">String identifier</param>
	/// <param name="textureId">Texture ID</param>
	/// <param name="imageSize">Image size</param>
	/// <param name="uv0">UV coordinate for top-left</param>
	/// <param name="uv1">UV coordinate for bottom-right</param>
	/// <param name="bgCol">Background color</param>
	/// <param name="tintCol">Tint color</param>
	/// <returns>True if image button was clicked</returns>
	public bool ImageButton(string strId, nint textureId, Vector2 imageSize, Vector2 uv0 = default, Vector2 uv1 = default, Vector4 bgCol = default, Vector4 tintCol = default);

	// Combos
	/// <summary>
	/// Begin a combo box
	/// </summary>
	/// <param name="label">Combo label</param>
	/// <param name="previewValue">Preview text</param>
	/// <param name="flags">Combo flags</param>
	/// <returns>True if combo is open</returns>
	public bool BeginCombo(string label, string previewValue, int flags = 0);

	/// <summary>
	/// End the current combo box
	/// </summary>
	public void EndCombo();

	/// <summary>
	/// Display a simple combo box
	/// </summary>
	/// <param name="label">Combo label</param>
	/// <param name="currentItem">Reference to current item index</param>
	/// <param name="items">Array of items</param>
	/// <param name="popupMaxHeightInItems">Maximum height in items</param>
	/// <returns>True if selection changed</returns>
	public bool Combo(string label, ref int currentItem, string[] items, int popupMaxHeightInItems = -1);

	// Input Text
	/// <summary>
	/// Display a text input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="buf">Input buffer</param>
	/// <param name="bufSize">Buffer size</param>
	/// <param name="flags">Input text flags</param>
	/// <returns>True if text was edited</returns>
	public unsafe bool InputText(string label, byte* buf, nuint bufSize, int flags = 0);

	/// <summary>
	/// Display a multiline text input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="buf">Input buffer</param>
	/// <param name="bufSize">Buffer size</param>
	/// <param name="size">Input size</param>
	/// <param name="flags">Input text flags</param>
	/// <returns>True if text was edited</returns>
	public unsafe bool InputTextMultiline(string label, byte* buf, nuint bufSize, Vector2 size = default, int flags = 0);

	/// <summary>
	/// Display a text input field with hint
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="hint">Hint text</param>
	/// <param name="buf">Input buffer</param>
	/// <param name="bufSize">Buffer size</param>
	/// <param name="flags">Input text flags</param>
	/// <returns>True if text was edited</returns>
	public unsafe bool InputTextWithHint(string label, string hint, byte* buf, nuint bufSize, int flags = 0);

	// Input Scalars
	/// <summary>
	/// Display a float input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Reference to float value</param>
	/// <param name="stepSize">Step amount</param>
	/// <param name="stepFast">Fast step amount</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public bool InputFloat(string label, ref float v, float stepSize = 0.0f, float stepFast = 0.0f, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float2 input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Reference to Vector2 value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public bool InputFloat2(string label, ref Vector2 v, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float3 input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Reference to Vector3 value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public bool InputFloat3(string label, ref Vector3 v, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float4 input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Reference to Vector4 value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public bool InputFloat4(string label, ref Vector4 v, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display an int input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Reference to int value</param>
	/// <param name="stepSize">Step amount</param>
	/// <param name="stepFast">Fast step amount</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public bool InputInt(string label, ref int v, int stepSize = 1, int stepFast = 100, int flags = 0);

	/// <summary>
	/// Display an int2 input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Array of 2 int values</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool InputInt2(string label, int* v, int flags = 0);

	/// <summary>
	/// Display an int3 input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Array of 3 int values</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool InputInt3(string label, int* v, int flags = 0);

	/// <summary>
	/// Display an int4 input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Array of 4 int values</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool InputInt4(string label, int* v, int flags = 0);

	/// <summary>
	/// Display a double input field
	/// </summary>
	/// <param name="label">Input label</param>
	/// <param name="v">Reference to double value</param>
	/// <param name="stepSize">Step amount</param>
	/// <param name="stepFast">Fast step amount</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Input flags</param>
	/// <returns>True if value was edited</returns>
	public bool InputDouble(string label, ref double v, double stepSize = 0.0, double stepFast = 0.0, string format = "%.6f", int flags = 0);

	// Sliders
	/// <summary>
	/// Display a float slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="v">Reference to float value</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool SliderFloat(string label, ref float v, float vMin, float vMax, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float2 slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="v">Reference to Vector2 value</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool SliderFloat2(string label, ref Vector2 v, float vMin, float vMax, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float3 slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="v">Reference to Vector3 value</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool SliderFloat3(string label, ref Vector3 v, float vMin, float vMax, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float4 slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="v">Reference to Vector4 value</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool SliderFloat4(string label, ref Vector4 v, float vMin, float vMax, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display an angle slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="vRad">Reference to angle in radians</param>
	/// <param name="vDegreesMin">Minimum angle in degrees</param>
	/// <param name="vDegreesMax">Maximum angle in degrees</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool SliderAngle(string label, ref float vRad, float vDegreesMin = -360.0f, float vDegreesMax = 360.0f, string format = "%.0f deg", int flags = 0);

	/// <summary>
	/// Display an int slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="v">Reference to int value</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool SliderInt(string label, ref int v, int vMin, int vMax, string format = "%d", int flags = 0);

	/// <summary>
	/// Display an int2 slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="v">Array of 2 int values</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool SliderInt2(string label, int* v, int vMin, int vMax, string format = "%d", int flags = 0);

	/// <summary>
	/// Display an int3 slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="v">Array of 3 int values</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool SliderInt3(string label, int* v, int vMin, int vMax, string format = "%d", int flags = 0);

	/// <summary>
	/// Display an int4 slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="v">Array of 4 int values</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool SliderInt4(string label, int* v, int vMin, int vMax, string format = "%d", int flags = 0);

	// Vertical Sliders
	/// <summary>
	/// Display a vertical float slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="size">Slider size</param>
	/// <param name="v">Reference to float value</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool VSliderFloat(string label, Vector2 size, ref float v, float vMin, float vMax, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a vertical int slider
	/// </summary>
	/// <param name="label">Slider label</param>
	/// <param name="size">Slider size</param>
	/// <param name="v">Reference to int value</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool VSliderInt(string label, Vector2 size, ref int v, int vMin, int vMax, string format = "%d", int flags = 0);

	// Drags (Alternative to Sliders)
	/// <summary>
	/// Display a float drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="v">Reference to float value</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool DragFloat(string label, ref float v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float2 drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="v">Reference to Vector2 value</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool DragFloat2(string label, ref Vector2 v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float3 drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="v">Reference to Vector3 value</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool DragFloat3(string label, ref Vector3 v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display a float4 drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="v">Reference to Vector4 value</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool DragFloat4(string label, ref Vector4 v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", int flags = 0);

	/// <summary>
	/// Display an int drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="v">Reference to int value</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool DragInt(string label, ref int v, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", int flags = 0);

	/// <summary>
	/// Display an int2 drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="v">Array of 2 int values</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool DragInt2(string label, int* v, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", int flags = 0);

	/// <summary>
	/// Display an int3 drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="v">Array of 3 int values</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool DragInt3(string label, int* v, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", int flags = 0);

	/// <summary>
	/// Display an int4 drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="v">Array of 4 int values</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum value</param>
	/// <param name="vMax">Maximum value</param>
	/// <param name="format">Format string</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public unsafe bool DragInt4(string label, int* v, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", int flags = 0);

	/// <summary>
	/// Display a float range drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="vCurrentMin">Reference to minimum value</param>
	/// <param name="vCurrentMax">Reference to maximum value</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum allowed value</param>
	/// <param name="vMax">Maximum allowed value</param>
	/// <param name="format">Format string for min</param>
	/// <param name="formatMax">Format string for max</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", string? formatMax = null, int flags = 0);

	/// <summary>
	/// Display an int range drag control
	/// </summary>
	/// <param name="label">Drag label</param>
	/// <param name="vCurrentMin">Reference to minimum value</param>
	/// <param name="vCurrentMax">Reference to maximum value</param>
	/// <param name="vSpeed">Drag speed</param>
	/// <param name="vMin">Minimum allowed value</param>
	/// <param name="vMax">Maximum allowed value</param>
	/// <param name="format">Format string for min</param>
	/// <param name="formatMax">Format string for max</param>
	/// <param name="flags">Slider flags</param>
	/// <returns>True if value was edited</returns>
	public bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", string? formatMax = null, int flags = 0);

	// Color Controls
	/// <summary>
	/// Display a color edit control (RGB)
	/// </summary>
	/// <param name="label">Color label</param>
	/// <param name="col">Reference to Vector3 color</param>
	/// <param name="flags">Color edit flags</param>
	/// <returns>True if color was edited</returns>
	public bool ColorEdit3(string label, ref Vector3 col, int flags = 0);

	/// <summary>
	/// Display a color edit control (RGBA)
	/// </summary>
	/// <param name="label">Color label</param>
	/// <param name="col">Reference to Vector4 color</param>
	/// <param name="flags">Color edit flags</param>
	/// <returns>True if color was edited</returns>
	public bool ColorEdit4(string label, ref Vector4 col, int flags = 0);

	/// <summary>
	/// Display a color picker (RGB)
	/// </summary>
	/// <param name="label">Color label</param>
	/// <param name="col">Reference to Vector3 color</param>
	/// <param name="flags">Color edit flags</param>
	/// <returns>True if color was edited</returns>
	public bool ColorPicker3(string label, ref Vector3 col, int flags = 0);

	/// <summary>
	/// Display a color picker (RGBA)
	/// </summary>
	/// <param name="label">Color label</param>
	/// <param name="col">Reference to Vector4 color</param>
	/// <param name="flags">Color edit flags</param>
	/// <param name="refCol">Reference color pointer</param>
	/// <returns>True if color was edited</returns>
	public unsafe bool ColorPicker4(string label, ref Vector4 col, int flags = 0, float* refCol = null);

	/// <summary>
	/// Display a color button
	/// </summary>
	/// <param name="descId">Description ID</param>
	/// <param name="col">Color</param>
	/// <param name="flags">Color button flags</param>
	/// <param name="size">Button size</param>
	/// <returns>True if button was clicked</returns>
	public bool ColorButton(string descId, Vector4 col, int flags = 0, Vector2 size = default);

	/// <summary>
	/// Set color edit options
	/// </summary>
	/// <param name="flags">Color edit flags</param>
	public void SetColorEditOptions(int flags);

	// Trees
	/// <summary>
	/// Begin a tree node
	/// </summary>
	/// <param name="label">Tree node label</param>
	/// <returns>True if tree node is open</returns>
	public bool TreeNode(string label);

	/// <summary>
	/// Begin a tree node with string ID
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="fmt">Format string</param>
	/// <returns>True if tree node is open</returns>
	public bool TreeNode(string strId, string fmt);

	/// <summary>
	/// Begin a tree node with pointer ID
	/// </summary>
	/// <param name="ptrId">Pointer ID</param>
	/// <param name="fmt">Format string</param>
	/// <returns>True if tree node is open</returns>
	public unsafe bool TreeNode(void* ptrId, string fmt);

	/// <summary>
	/// End a tree node (call after TreeNode returns true)
	/// </summary>
	public void TreePop();

	/// <summary>
	/// Get tree node to label spacing
	/// </summary>
	/// <returns>Spacing amount</returns>
	public float GetTreeNodeToLabelSpacing();

	/// <summary>
	/// Begin a collapsing header
	/// </summary>
	/// <param name="label">Header label</param>
	/// <param name="flags">Tree node flags</param>
	/// <returns>True if header is open</returns>
	public bool CollapsingHeader(string label, int flags = 0);

	/// <summary>
	/// Begin a collapsing header with close button
	/// </summary>
	/// <param name="label">Header label</param>
	/// <param name="pVisible">Reference to visibility state</param>
	/// <param name="flags">Tree node flags</param>
	/// <returns>True if header is open</returns>
	public bool CollapsingHeader(string label, ref bool pVisible, int flags = 0);

	/// <summary>
	/// Set next item open state
	/// </summary>
	/// <param name="isOpen">Open state</param>
	/// <param name="cond">Condition flags</param>
	public void SetNextItemOpen(bool isOpen, int cond = 0);

	// Selectables
	/// <summary>
	/// Display a selectable item
	/// </summary>
	/// <param name="label">Selectable label</param>
	/// <param name="selected">Whether item is selected</param>
	/// <param name="flags">Selectable flags</param>
	/// <param name="size">Item size</param>
	/// <returns>True if item was clicked</returns>
	public bool Selectable(string label, bool selected = false, int flags = 0, Vector2 size = default);

	/// <summary>
	/// Display a selectable item with reference state
	/// </summary>
	/// <param name="label">Selectable label</param>
	/// <param name="pSelected">Reference to selected state</param>
	/// <param name="flags">Selectable flags</param>
	/// <param name="size">Item size</param>
	/// <returns>True if item was clicked</returns>
	public bool Selectable(string label, ref bool pSelected, int flags = 0, Vector2 size = default);

	// List Boxes
	/// <summary>
	/// Begin a list box
	/// </summary>
	/// <param name="label">List box label</param>
	/// <param name="size">List box size</param>
	/// <returns>True if list box is active</returns>
	public bool BeginListBox(string label, Vector2 size = default);

	/// <summary>
	/// End the current list box
	/// </summary>
	public void EndListBox();

	/// <summary>
	/// Display a simple list box
	/// </summary>
	/// <param name="label">List box label</param>
	/// <param name="currentItem">Reference to current item index</param>
	/// <param name="items">Array of items</param>
	/// <param name="heightInItems">Height in items</param>
	/// <returns>True if selection changed</returns>
	public bool ListBox(string label, ref int currentItem, string[] items, int heightInItems = -1);

	// Plot/Graph widgets (simple versions)
	/// <summary>
	/// Display a plot lines graph
	/// </summary>
	/// <param name="label">Graph label</param>
	/// <param name="values">Array of values</param>
	/// <param name="valuesCount">Number of values</param>
	/// <param name="valuesOffset">Values offset</param>
	/// <param name="overlayText">Overlay text</param>
	/// <param name="scaleMin">Scale minimum</param>
	/// <param name="scaleMax">Scale maximum</param>
	/// <param name="graphSize">Graph size</param>
	/// <param name="stride">Value stride</param>
	public unsafe void PlotLines(string label, float* values, int valuesCount, int valuesOffset = 0, string? overlayText = null, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, Vector2 graphSize = default, int stride = sizeof(float));

	/// <summary>
	/// Display a plot histogram
	/// </summary>
	/// <param name="label">Histogram label</param>
	/// <param name="values">Array of values</param>
	/// <param name="valuesCount">Number of values</param>
	/// <param name="valuesOffset">Values offset</param>
	/// <param name="overlayText">Overlay text</param>
	/// <param name="scaleMin">Scale minimum</param>
	/// <param name="scaleMax">Scale maximum</param>
	/// <param name="graphSize">Graph size</param>
	/// <param name="stride">Value stride</param>
	public unsafe void PlotHistogram(string label, float* values, int valuesCount, int valuesOffset = 0, string? overlayText = null, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, Vector2 graphSize = default, int stride = sizeof(float));

	// Menus
	/// <summary>
	/// Begin a main menu bar
	/// </summary>
	/// <returns>True if menu bar is active</returns>
	public bool BeginMainMenuBar();

	/// <summary>
	/// End the main menu bar
	/// </summary>
	public void EndMainMenuBar();

	/// <summary>
	/// Begin a menu bar
	/// </summary>
	/// <returns>True if menu bar is active</returns>
	public bool BeginMenuBar();

	/// <summary>
	/// End the current menu bar
	/// </summary>
	public void EndMenuBar();

	/// <summary>
	/// Begin a menu
	/// </summary>
	/// <param name="label">Menu label</param>
	/// <param name="enabled">Whether menu is enabled</param>
	/// <returns>True if menu is open</returns>
	public bool BeginMenu(string label, bool enabled = true);

	/// <summary>
	/// End the current menu
	/// </summary>
	public void EndMenu();

	/// <summary>
	/// Display a menu item
	/// </summary>
	/// <param name="label">Menu item label</param>
	/// <param name="shortcut">Keyboard shortcut text</param>
	/// <param name="selected">Whether item is selected</param>
	/// <param name="enabled">Whether item is enabled</param>
	/// <returns>True if menu item was clicked</returns>
	public bool MenuItem(string label, string? shortcut = null, bool selected = false, bool enabled = true);

	/// <summary>
	/// Display a menu item with reference state
	/// </summary>
	/// <param name="label">Menu item label</param>
	/// <param name="shortcut">Keyboard shortcut text</param>
	/// <param name="pSelected">Reference to selected state</param>
	/// <param name="enabled">Whether item is enabled</param>
	/// <returns>True if menu item was clicked</returns>
	public bool MenuItem(string label, string shortcut, ref bool pSelected, bool enabled = true);

	// Tooltips
	/// <summary>
	/// Begin a tooltip
	/// </summary>
	public void BeginTooltip();

	/// <summary>
	/// End the current tooltip
	/// </summary>
	public void EndTooltip();

	/// <summary>
	/// Display a simple tooltip
	/// </summary>
	/// <param name="text">Tooltip text</param>
	public void SetTooltip(string text);

	// Popups, Modals
	/// <summary>
	/// Begin a popup
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="flags">Window flags</param>
	/// <returns>True if popup is open</returns>
	public bool BeginPopup(string strId, int flags = 0);

	/// <summary>
	/// Begin a popup modal
	/// </summary>
	/// <param name="name">Modal name</param>
	/// <param name="pOpen">Reference to open state</param>
	/// <param name="flags">Window flags</param>
	/// <returns>True if modal is open</returns>
	public bool BeginPopupModal(string name, ref bool pOpen, int flags = 0);

	/// <summary>
	/// End the current popup
	/// </summary>
	public void EndPopup();

	/// <summary>
	/// Open a popup
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="popupFlags">Popup flags</param>
	public void OpenPopup(string strId, int popupFlags = 0);

	/// <summary>
	/// Open a popup on item click
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="popupFlags">Popup flags</param>
	public void OpenPopupOnItemClick(string strId = "", int popupFlags = 1);

	/// <summary>
	/// Close the current popup
	/// </summary>
	public void CloseCurrentPopup();

	/// <summary>
	/// Begin a popup context item
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="popupFlags">Popup flags</param>
	/// <returns>True if popup is open</returns>
	public bool BeginPopupContextItem(string strId = "", int popupFlags = 1);

	/// <summary>
	/// Begin a popup context window
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="popupFlags">Popup flags</param>
	/// <returns>True if popup is open</returns>
	public bool BeginPopupContextWindow(string strId = "", int popupFlags = 1);

	/// <summary>
	/// Begin a popup context void
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="popupFlags">Popup flags</param>
	/// <returns>True if popup is open</returns>
	public bool BeginPopupContextVoid(string strId = "", int popupFlags = 1);

	/// <summary>
	/// Check if popup is open
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="flags">Popup flags</param>
	/// <returns>True if popup is open</returns>
	public bool IsPopupOpen(string strId, int flags = 0);

	// Tables
	/// <summary>
	/// Begin a table
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="columnsCount">Number of columns</param>
	/// <param name="flags">Table flags</param>
	/// <param name="outerSize">Outer size</param>
	/// <param name="innerWidth">Inner width</param>
	/// <returns>True if table is active</returns>
	public bool BeginTable(string strId, int columnsCount, int flags = 0, Vector2 outerSize = default, float innerWidth = 0.0f);

	/// <summary>
	/// End the current table
	/// </summary>
	public void EndTable();

	/// <summary>
	/// Move to next table row
	/// </summary>
	/// <param name="rowFlags">Row flags</param>
	/// <param name="minRowHeight">Minimum row height</param>
	public void TableNextRow(int rowFlags = 0, float minRowHeight = 0.0f);

	/// <summary>
	/// Move to next table column
	/// </summary>
	/// <returns>True if column is visible</returns>
	public bool TableNextColumn();

	/// <summary>
	/// Move to specific table column
	/// </summary>
	/// <param name="columnN">Column index</param>
	/// <returns>True if column is visible</returns>
	public bool TableSetColumnIndex(int columnN);

	/// <summary>
	/// Setup a table column
	/// </summary>
	/// <param name="label">Column label</param>
	/// <param name="flags">Column flags</param>
	/// <param name="initWidthOrWeight">Initial width or weight</param>
	/// <param name="userId">User ID</param>
	public void TableSetupColumn(string label, int flags = 0, float initWidthOrWeight = 0.0f, uint userId = 0);

	/// <summary>
	/// Setup table scroll freeze
	/// </summary>
	/// <param name="cols">Number of columns to freeze</param>
	/// <param name="rows">Number of rows to freeze</param>
	public void TableSetupScrollFreeze(int cols, int rows);

	/// <summary>
	/// Submit table headers
	/// </summary>
	public void TableHeadersRow();

	/// <summary>
	/// Submit a table header
	/// </summary>
	/// <param name="label">Header label</param>
	public void TableHeader(string label);

	/// <summary>
	/// Get table sort specs
	/// </summary>
	/// <returns>Sort specs pointer</returns>
	public nint TableGetSortSpecs();

	/// <summary>
	/// Get table column count
	/// </summary>
	/// <returns>Column count</returns>
	public int TableGetColumnCount();

	/// <summary>
	/// Get table column index
	/// </summary>
	/// <returns>Current column index</returns>
	public int TableGetColumnIndex();

	/// <summary>
	/// Get table row index
	/// </summary>
	/// <returns>Current row index</returns>
	public int TableGetRowIndex();

	/// <summary>
	/// Get table column name
	/// </summary>
	/// <param name="columnN">Column index</param>
	/// <returns>Column name</returns>
	public string TableGetColumnName(int columnN = -1);

	/// <summary>
	/// Get table column flags
	/// </summary>
	/// <param name="columnN">Column index</param>
	/// <returns>Column flags</returns>
	public int TableGetColumnFlags(int columnN = -1);

	/// <summary>
	/// Set table column enabled state
	/// </summary>
	/// <param name="columnN">Column index</param>
	/// <param name="v">Enabled state</param>
	public void TableSetColumnEnabled(int columnN, bool v);

	/// <summary>
	/// Set table background color
	/// </summary>
	/// <param name="target">Background target</param>
	/// <param name="color">Background color</param>
	/// <param name="columnN">Column index</param>
	public void TableSetBgColor(int target, uint color, int columnN = -1);

	// Tab Bars, Tabs
	/// <summary>
	/// Begin a tab bar
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="flags">Tab bar flags</param>
	/// <returns>True if tab bar is active</returns>
	public bool BeginTabBar(string strId, int flags = 0);

	/// <summary>
	/// End the current tab bar
	/// </summary>
	public void EndTabBar();

	/// <summary>
	/// Begin a tab item
	/// </summary>
	/// <param name="label">Tab label</param>
	/// <param name="pOpen">Reference to open state</param>
	/// <param name="flags">Tab item flags</param>
	/// <returns>True if tab is selected</returns>
	public bool BeginTabItem(string label, ref bool pOpen, int flags = 0);

	/// <summary>
	/// Begin a tab item without close button
	/// </summary>
	/// <param name="label">Tab label</param>
	/// <param name="flags">Tab item flags</param>
	/// <returns>True if tab is selected</returns>
	public bool BeginTabItem(string label, int flags = 0);

	/// <summary>
	/// End the current tab item
	/// </summary>
	public void EndTabItem();

	/// <summary>
	/// Set tab item closed
	/// </summary>
	/// <param name="tabOrDockedWindowLabel">Tab label</param>
	public void SetTabItemClosed(string tabOrDockedWindowLabel);

	// Docking
	/// <summary>
	/// Dock space
	/// </summary>
	/// <param name="id">Dock space ID</param>
	/// <param name="size">Dock space size</param>
	/// <param name="flags">Dock node flags</param>
	/// <param name="windowClass">Window class</param>
	/// <returns>Dock space ID</returns>
	public uint DockSpace(uint id, Vector2 size = default, int flags = 0, nint windowClass = 0);

	/// <summary>
	/// Dock space over viewport
	/// </summary>
	/// <param name="viewport">Viewport pointer</param>
	/// <param name="flags">Dock node flags</param>
	/// <param name="windowClass">Window class</param>
	/// <returns>Dock space ID</returns>
	public uint DockSpaceOverViewport(nint viewport = 0, int flags = 0, nint windowClass = 0);

	/// <summary>
	/// Set next window dock ID
	/// </summary>
	/// <param name="dockId">Dock ID</param>
	/// <param name="cond">Condition flags</param>
	public void SetNextWindowDockID(uint dockId, int cond = 0);

	/// <summary>
	/// Set next window class
	/// </summary>
	/// <param name="windowClass">Window class</param>
	public void SetNextWindowClass(nint windowClass);

	/// <summary>
	/// Get window dock ID
	/// </summary>
	/// <returns>Dock ID</returns>
	public uint GetWindowDockID();

	/// <summary>
	/// Check if window is docked
	/// </summary>
	/// <returns>True if window is docked</returns>
	public bool IsWindowDocked();

	// Layout
	/// <summary>
	/// Add a separator line
	/// </summary>
	public void Separator();

	/// <summary>
	/// Move to the same line as previous widget
	/// </summary>
	/// <param name="offsetFromStartX">Offset from start X</param>
	/// <param name="spacing">Spacing</param>
	public void SameLine(float offsetFromStartX = 0.0f, float spacing = -1.0f);

	/// <summary>
	/// Start a new line
	/// </summary>
	public void NewLine();

	/// <summary>
	/// Add vertical spacing
	/// </summary>
	public void Spacing();

	/// <summary>
	/// Add a dummy item of given size
	/// </summary>
	/// <param name="size">Item size</param>
	public void Dummy(Vector2 size);

	/// <summary>
	/// Indent content
	/// </summary>
	/// <param name="indentW">Indent width</param>
	public void Indent(float indentW = 0.0f);

	/// <summary>
	/// Unindent content
	/// </summary>
	/// <param name="indentW">Indent width</param>
	public void Unindent(float indentW = 0.0f);

	/// <summary>
	/// Begin a group
	/// </summary>
	public void BeginGroup();

	/// <summary>
	/// End the current group
	/// </summary>
	public void EndGroup();

	/// <summary>
	/// Get cursor position
	/// </summary>
	/// <returns>Cursor position</returns>
	public Vector2 GetCursorPos();

	/// <summary>
	/// Get cursor position X
	/// </summary>
	/// <returns>Cursor X position</returns>
	public float GetCursorPosX();

	/// <summary>
	/// Get cursor position Y
	/// </summary>
	/// <returns>Cursor Y position</returns>
	public float GetCursorPosY();

	/// <summary>
	/// Set cursor position
	/// </summary>
	/// <param name="localPos">Local position</param>
	public void SetCursorPos(Vector2 localPos);

	/// <summary>
	/// Set cursor position X
	/// </summary>
	/// <param name="localX">Local X position</param>
	public void SetCursorPosX(float localX);

	/// <summary>
	/// Set cursor position Y
	/// </summary>
	/// <param name="localY">Local Y position</param>
	public void SetCursorPosY(float localY);

	/// <summary>
	/// Get cursor start position
	/// </summary>
	/// <returns>Cursor start position</returns>
	public Vector2 GetCursorStartPos();

	/// <summary>
	/// Get cursor screen position
	/// </summary>
	/// <returns>Cursor screen position</returns>
	public Vector2 GetCursorScreenPos();

	/// <summary>
	/// Set cursor screen position
	/// </summary>
	/// <param name="pos">Screen position</param>
	public void SetCursorScreenPos(Vector2 pos);

	/// <summary>
	/// Align text to frame padding
	/// </summary>
	public void AlignTextToFramePadding();

	/// <summary>
	/// Get text line height
	/// </summary>
	/// <returns>Text line height</returns>
	public float GetTextLineHeight();

	/// <summary>
	/// Get text line height with spacing
	/// </summary>
	/// <returns>Text line height with spacing</returns>
	public float GetTextLineHeightWithSpacing();

	/// <summary>
	/// Get frame height
	/// </summary>
	/// <returns>Frame height</returns>
	public float GetFrameHeight();

	/// <summary>
	/// Get frame height with spacing
	/// </summary>
	/// <returns>Frame height with spacing</returns>
	public float GetFrameHeightWithSpacing();

	// ID stack/scopes
	/// <summary>
	/// Push string ID
	/// </summary>
	/// <param name="strId">String ID</param>
	public void PushID(string strId);

	/// <summary>
	/// Push int ID
	/// </summary>
	/// <param name="intId">Int ID</param>
	public void PushID(int intId);

	/// <summary>
	/// Push pointer ID
	/// </summary>
	/// <param name="ptrId">Pointer ID</param>
	public unsafe void PushID(void* ptrId);

	/// <summary>
	/// Pop ID from stack
	/// </summary>
	public void PopID();

	/// <summary>
	/// Get ID from string
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <returns>ID</returns>
	public uint GetID(string strId);

	/// <summary>
	/// Get ID from pointer
	/// </summary>
	/// <param name="ptrId">Pointer ID</param>
	/// <returns>ID</returns>
	public unsafe uint GetID(void* ptrId);

	// Widgets Utilities
	/// <summary>
	/// Get item rect min
	/// </summary>
	/// <returns>Item rect min</returns>
	public Vector2 GetItemRectMin();

	/// <summary>
	/// Get item rect max
	/// </summary>
	/// <returns>Item rect max</returns>
	public Vector2 GetItemRectMax();

	/// <summary>
	/// Get item rect size
	/// </summary>
	/// <returns>Item rect size</returns>
	public Vector2 GetItemRectSize();

	/// <summary>
	/// Set next item width
	/// </summary>
	/// <param name="itemWidth">Item width</param>
	public void SetNextItemWidth(float itemWidth);

	/// <summary>
	/// Calculate item width
	/// </summary>
	/// <returns>Item width</returns>
	public float CalcItemWidth();

	/// <summary>
	/// Push item width
	/// </summary>
	/// <param name="itemWidth">Item width</param>
	public void PushItemWidth(float itemWidth);

	/// <summary>
	/// Pop item width from stack
	/// </summary>
	public void PopItemWidth();

	/// <summary>
	/// Set next item allow overlap
	/// </summary>
	public void SetNextItemAllowOverlap();

	// Miscellaneous Utilities
	/// <summary>
	/// Check if item is hovered
	/// </summary>
	/// <param name="flags">Hovered flags</param>
	/// <returns>True if item is hovered</returns>
	public bool IsItemHovered(int flags = 0);

	/// <summary>
	/// Check if item is active
	/// </summary>
	/// <returns>True if item is active</returns>
	public bool IsItemActive();

	/// <summary>
	/// Check if item is focused
	/// </summary>
	/// <returns>True if item is focused</returns>
	public bool IsItemFocused();

	/// <summary>
	/// Check if item was clicked
	/// </summary>
	/// <param name="mouseButton">Mouse button</param>
	/// <returns>True if item was clicked</returns>
	public bool IsItemClicked(int mouseButton = 0);

	/// <summary>
	/// Check if item is visible
	/// </summary>
	/// <returns>True if item is visible</returns>
	public bool IsItemVisible();

	/// <summary>
	/// Check if item was edited
	/// </summary>
	/// <returns>True if item was edited</returns>
	public bool IsItemEdited();

	/// <summary>
	/// Check if item was activated
	/// </summary>
	/// <returns>True if item was activated</returns>
	public bool IsItemActivated();

	/// <summary>
	/// Check if item was deactivated
	/// </summary>
	/// <returns>True if item was deactivated</returns>
	public bool IsItemDeactivated();

	/// <summary>
	/// Check if item was deactivated after edit
	/// </summary>
	/// <returns>True if item was deactivated after edit</returns>
	public bool IsItemDeactivatedAfterEdit();

	/// <summary>
	/// Check if any item is hovered
	/// </summary>
	/// <returns>True if any item is hovered</returns>
	public bool IsAnyItemHovered();

	/// <summary>
	/// Check if any item is active
	/// </summary>
	/// <returns>True if any item is active</returns>
	public bool IsAnyItemActive();

	/// <summary>
	/// Check if any item is focused
	/// </summary>
	/// <returns>True if any item is focused</returns>
	public bool IsAnyItemFocused();

	/// <summary>
	/// Get item ID
	/// </summary>
	/// <returns>Item ID</returns>
	public uint GetItemID();

	/// <summary>
	/// Get time since application start
	/// </summary>
	/// <returns>Time in seconds</returns>
	public double GetTime();

	/// <summary>
	/// Get frame count
	/// </summary>
	/// <returns>Frame count</returns>
	public int GetFrameCount();

	// Background/Foreground Draw Lists
	/// <summary>
	/// Get background draw list
	/// </summary>
	/// <returns>Draw list pointer</returns>
	public nint GetBackgroundDrawList();

	/// <summary>
	/// Get foreground draw list
	/// </summary>
	/// <returns>Draw list pointer</returns>
	public nint GetForegroundDrawList();

	/// <summary>
	/// Get window draw list
	/// </summary>
	/// <returns>Draw list pointer</returns>
	public nint GetWindowDrawList();

	// Text Utilities
	/// <summary>
	/// Calculate text size
	/// </summary>
	/// <param name="text">Text to measure</param>
	/// <param name="hideTextAfterDoubleHash">Hide text after ##</param>
	/// <param name="wrapWidth">Wrap width</param>
	/// <returns>Text size</returns>
	public Vector2 CalcTextSize(string text, bool hideTextAfterDoubleHash = false, float wrapWidth = -1.0f);

	// Color Utilities
	/// <summary>
	/// Convert color from HSV to RGB
	/// </summary>
	/// <param name="h">Hue</param>
	/// <param name="s">Saturation</param>
	/// <param name="v">Value</param>
	/// <param name="r">Red output</param>
	/// <param name="g">Green output</param>
	/// <param name="b">Blue output</param>
	public void ColorConvertHSVtoRGB(float h, float s, float v, out float r, out float g, out float b);

	/// <summary>
	/// Convert color from RGB to HSV
	/// </summary>
	/// <param name="r">Red</param>
	/// <param name="g">Green</param>
	/// <param name="b">Blue</param>
	/// <param name="h">Hue output</param>
	/// <param name="s">Saturation output</param>
	/// <param name="v">Value output</param>
	public void ColorConvertRGBtoHSV(float r, float g, float b, out float h, out float s, out float v);

	// Inputs Utilities: Keyboard
	/// <summary>
	/// Map ImGuiKey to user's key index
	/// </summary>
	/// <param name="key">ImGui key</param>
	/// <returns>Key index</returns>
	public int GetKeyIndex(int key);

	/// <summary>
	/// Check if key is being held
	/// </summary>
	/// <param name="key">Key</param>
	/// <returns>True if key is down</returns>
	public bool IsKeyDown(int key);

	/// <summary>
	/// Check if key was pressed
	/// </summary>
	/// <param name="key">Key</param>
	/// <param name="repeat">Allow repeat</param>
	/// <returns>True if key was pressed</returns>
	public bool IsKeyPressed(int key, bool repeat = true);

	/// <summary>
	/// Check if key was released
	/// </summary>
	/// <param name="key">Key</param>
	/// <returns>True if key was released</returns>
	public bool IsKeyReleased(int key);

	/// <summary>
	/// Get key pressed count
	/// </summary>
	/// <param name="key">Key</param>
	/// <param name="repeatDelay">Repeat delay</param>
	/// <param name="rate">Repeat rate</param>
	/// <returns>Press count</returns>
	public int GetKeyPressedAmount(int key, float repeatDelay, float rate);

	/// <summary>
	/// Get key name
	/// </summary>
	/// <param name="key">Key</param>
	/// <returns>Key name</returns>
	public string GetKeyName(int key);

	/// <summary>
	/// Set next frame want capture keyboard
	/// </summary>
	/// <param name="wantCaptureKeyboard">Want capture keyboard</param>
	public void SetNextFrameWantCaptureKeyboard(bool wantCaptureKeyboard);

	// Inputs Utilities: Mouse
	/// <summary>
	/// Check if mouse button is being held
	/// </summary>
	/// <param name="button">Mouse button</param>
	/// <returns>True if button is down</returns>
	public bool IsMouseDown(int button);

	/// <summary>
	/// Check if mouse button was clicked
	/// </summary>
	/// <param name="button">Mouse button</param>
	/// <param name="repeat">Allow repeat</param>
	/// <returns>True if button was clicked</returns>
	public bool IsMouseClicked(int button, bool repeat = false);

	/// <summary>
	/// Check if mouse button was released
	/// </summary>
	/// <param name="button">Mouse button</param>
	/// <returns>True if button was released</returns>
	public bool IsMouseReleased(int button);

	/// <summary>
	/// Check if mouse button was double-clicked
	/// </summary>
	/// <param name="button">Mouse button</param>
	/// <returns>True if button was double-clicked</returns>
	public bool IsMouseDoubleClicked(int button);

	/// <summary>
	/// Get mouse clicked count
	/// </summary>
	/// <param name="button">Mouse button</param>
	/// <returns>Click count</returns>
	public int GetMouseClickedCount(int button);

	/// <summary>
	/// Check if mouse is hovering over given rectangle
	/// </summary>
	/// <param name="rMin">Rectangle min</param>
	/// <param name="rMax">Rectangle max</param>
	/// <param name="clip">Clip to current clipping rectangle</param>
	/// <returns>True if mouse is hovering</returns>
	public bool IsMouseHoveringRect(Vector2 rMin, Vector2 rMax, bool clip = true);

	/// <summary>
	/// Check if mouse is over any window or popup
	/// </summary>
	/// <returns>True if mouse is over any window</returns>
	public bool IsMousePosValid();

	/// <summary>
	/// Check if any mouse button is down
	/// </summary>
	/// <returns>True if any mouse button is down</returns>
	public bool IsAnyMouseDown();

	/// <summary>
	/// Get mouse position
	/// </summary>
	/// <returns>Mouse position</returns>
	public Vector2 GetMousePos();

	/// <summary>
	/// Get mouse position at time of clicking
	/// </summary>
	/// <returns>Mouse position on click</returns>
	public Vector2 GetMousePosOnOpeningCurrentPopup();

	/// <summary>
	/// Check if mouse is being dragged
	/// </summary>
	/// <param name="button">Mouse button</param>
	/// <param name="lockThreshold">Lock threshold</param>
	/// <returns>True if mouse is being dragged</returns>
	public bool IsMouseDragging(int button, float lockThreshold = -1.0f);

	/// <summary>
	/// Get mouse drag delta
	/// </summary>
	/// <param name="button">Mouse button</param>
	/// <param name="lockThreshold">Lock threshold</param>
	/// <returns>Drag delta</returns>
	public Vector2 GetMouseDragDelta(int button = 0, float lockThreshold = -1.0f);

	/// <summary>
	/// Reset mouse drag delta
	/// </summary>
	/// <param name="button">Mouse button</param>
	public void ResetMouseDragDelta(int button = 0);

	/// <summary>
	/// Get mouse wheel delta
	/// </summary>
	/// <returns>Mouse wheel delta</returns>
	public float GetMouseWheel();

	/// <summary>
	/// Get mouse wheel H delta
	/// </summary>
	/// <returns>Mouse wheel H delta</returns>
	public float GetMouseWheelH();

	/// <summary>
	/// Set next frame want capture mouse
	/// </summary>
	/// <param name="wantCaptureMouse">Want capture mouse</param>
	public void SetNextFrameWantCaptureMouse(bool wantCaptureMouse);

	// Clipboard Utilities
	/// <summary>
	/// Get clipboard text
	/// </summary>
	/// <returns>Clipboard text</returns>
	public string GetClipboardText();

	/// <summary>
	/// Set clipboard text
	/// </summary>
	/// <param name="text">Text to set</param>
	public void SetClipboardText(string text);

	// Settings/.Ini Utilities
	/// <summary>
	/// Call after CreateContext() and before first call to NewFrame()
	/// </summary>
	/// <param name="iniFilename">INI filename</param>
	public void LoadIniSettingsFromDisk(string iniFilename);

	/// <summary>
	/// Call after CreateContext() and before first call to NewFrame()
	/// </summary>
	/// <param name="iniData">INI data</param>
	/// <param name="iniSize">INI data size</param>
	public void LoadIniSettingsFromMemory(string iniData, nuint iniSize = 0);

	/// <summary>
	/// Save settings to disk
	/// </summary>
	/// <param name="iniFilename">INI filename</param>
	public void SaveIniSettingsToDisk(string iniFilename);

	/// <summary>
	/// Save settings to memory
	/// </summary>
	/// <param name="outIniSize">Output INI size</param>
	/// <returns>INI data</returns>
	public unsafe string SaveIniSettingsToMemory(nuint* outIniSize = null);

	// Platform/Viewport Support
	/// <summary>
	/// Update platform windows (multi-viewport support)
	/// </summary>
	public void UpdatePlatformWindows();

	/// <summary>
	/// Render platform windows (multi-viewport support)
	/// </summary>
	public void RenderPlatformWindowsDefault();

	/// <summary>
	/// Call in main loop after EndFrame() and after platform/OS windows have been swapped
	/// </summary>
	public void DestroyPlatformWindows();

	/// <summary>
	/// Get main viewport
	/// </summary>
	/// <returns>Main viewport pointer</returns>
	public nint GetMainViewport();

	// Version Info
	/// <summary>
	/// Get ImGui version string
	/// </summary>
	/// <returns>Version string</returns>
	public string GetVersion();

	/// <summary>
	/// Get compiled ImGui version
	/// </summary>
	/// <returns>Compiled version string</returns>
	public string GetVersionNumber();

	/// <summary>
	/// Begin a tree node with flags
	/// </summary>
	/// <param name="flags">Tree node flags</param>
	/// <param name="label">Tree node label</param>
	/// <returns>True if tree node is open</returns>
	public bool TreeNodeExtended(string label, int flags = 0);

	/// <summary>
	/// Begin a tree node with string ID and flags
	/// </summary>
	/// <param name="strId">String ID</param>
	/// <param name="flags">Tree node flags</param>
	/// <param name="fmt">Format string</param>
	/// <returns>True if tree node is open</returns>
	public bool TreeNodeExtended(string strId, int flags, string fmt);

	/// <summary>
	/// Begin a tree node with pointer ID and flags
	/// </summary>
	/// <param name="ptrId">Pointer ID</param>
	/// <param name="flags">Tree node flags</param>
	/// <param name="fmt">Format string</param>
	/// <returns>True if tree node is open</returns>
	public unsafe bool TreeNodeExtended(void* ptrId, int flags, string fmt);
}
