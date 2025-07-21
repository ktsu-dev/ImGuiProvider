// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ImGuiProvider.Implementations.HexaNet;
using System.Numerics;
using Hexa.NET.ImGui;
using ImGuiProvider.Interfaces;

/// <summary>
/// Implementation of IImGuiProvider using Hexa.NET.ImGui
/// </summary>
public unsafe class HexaNetImGuiProvider : IImGuiProvider
{
	private bool _disposed;

	/// <inheritdoc />
	public nint CreateContext()
	{
		ImGuiContextPtr context = ImGui.CreateContext();
		return (nint)context.Handle;
	}

	/// <inheritdoc />
	public void DestroyContext(nint context)
	{
		if (context != nint.Zero)
		{
			ImGui.DestroyContext(new ImGuiContextPtr((ImGuiContext*)context));
		}
	}

	/// <inheritdoc />
	public nint GetCurrentContext()
	{
		ImGuiContextPtr context = ImGui.GetCurrentContext();
		return (nint)context.Handle;
	}

	/// <inheritdoc />
	public void SetCurrentContext(nint context) => ImGui.SetCurrentContext(new ImGuiContextPtr((ImGuiContext*)context));

	/// <inheritdoc />
	public void NewFrame() => ImGui.NewFrame();

	/// <inheritdoc />
	public void EndFrame() => ImGui.EndFrame();

	/// <inheritdoc />
	public void Render() => ImGui.Render();

	/// <inheritdoc />
	public nint GetDrawData()
	{
		ImDrawDataPtr drawData = ImGui.GetDrawData();
		return (nint)drawData.Handle;
	}

	/// <inheritdoc />
	public nint GetIO()
	{
		ImGuiIOPtr io = ImGui.GetIO();
		return (nint)(&io);
	}

	/// <inheritdoc />
	public nint GetStyle()
	{
		ImGuiStylePtr style = ImGui.GetStyle();
		return (nint)style.Handle;
	}

	/// <inheritdoc />
	public void StyleColorsDark() => ImGui.StyleColorsDark();

	/// <inheritdoc />
	public void StyleColorsLight() => ImGui.StyleColorsLight();

	/// <inheritdoc />
	public void StyleColorsClassic() => ImGui.StyleColorsClassic();

	// Windows
	/// <inheritdoc />
	public bool Begin(string name) => ImGui.Begin(name);

	/// <inheritdoc />
	public bool Begin(string name, int flags) => ImGui.Begin(name, (ImGuiWindowFlags)flags);

	/// <inheritdoc />
	public bool Begin(string name, bool* pOpen) => ImGui.Begin(name, pOpen);

	/// <inheritdoc />
	public bool Begin(string name, bool* pOpen, int flags) => ImGui.Begin(name, pOpen, (ImGuiWindowFlags)flags);

	/// <inheritdoc />
	public void EndWindow() => ImGui.End();

	// Child Windows
	/// <inheritdoc />
	public bool BeginChild(string strId, Vector2 size = default, int childFlags = 0, int windowFlags = 0) => ImGui.BeginChild(strId, size, (ImGuiChildFlags)childFlags, (ImGuiWindowFlags)windowFlags);

	/// <inheritdoc />
	public void EndChild() => ImGui.EndChild();

	// Demo and Debug Windows
	/// <inheritdoc />
	public void ShowDemoWindow() => ImGui.ShowDemoWindow();

	/// <inheritdoc />
	public void ShowDemoWindow(bool* pOpen) => ImGui.ShowDemoWindow(pOpen);

	/// <inheritdoc />
	public void ShowMetricsWindow(bool* pOpen) => ImGui.ShowMetricsWindow(pOpen);

	/// <inheritdoc />
	public void ShowDebugLogWindow(bool* pOpen) => ImGui.ShowDebugLogWindow(pOpen);

	/// <inheritdoc />
	public void ShowIDStackToolWindow(bool* pOpen) => ImGui.ShowIDStackToolWindow(pOpen);

	/// <inheritdoc />
	public void ShowAboutWindow(bool* pOpen) => ImGui.ShowAboutWindow(pOpen);

	/// <inheritdoc />
	public void ShowStyleEditor(nint style = 0)
	{
		if (style != 0)
		{
			ImGui.ShowStyleEditor((ImGuiStyle*)style);
		}
		else
		{
			ImGui.ShowStyleEditor();
		}
	}

	/// <inheritdoc />
	public bool ShowStyleSelector(string label) => ImGui.ShowStyleSelector(label);

	/// <inheritdoc />
	public void ShowFontSelector(string label) => ImGui.ShowFontSelector(label);

	/// <inheritdoc />
	public void ShowUserGuide() => ImGui.ShowUserGuide();

	// Basic Widgets - Text
	/// <inheritdoc />
	public void Text(string text) => ImGui.Text(text);

	/// <inheritdoc />
	public void TextColored(Vector4 color, string text) => ImGui.TextColored(color, text);

	/// <inheritdoc />
	public void TextDisabled(string text) => ImGui.TextDisabled(text);

	/// <inheritdoc />
	public void TextWrapped(string text) => ImGui.TextWrapped(text);

	/// <inheritdoc />
	public void BulletText(string text) => ImGui.BulletText(text);

	/// <inheritdoc />
	public void LabelText(string label, string text) => ImGui.LabelText(label, text);

	// Basic Widgets - Buttons
	/// <inheritdoc />
	public bool Button(string label) => ImGui.Button(label);

	/// <inheritdoc />
	public bool Button(string label, Vector2 size) => ImGui.Button(label, size);

	/// <inheritdoc />
	public bool SmallButton(string label) => ImGui.SmallButton(label);

	/// <inheritdoc />
	public bool InvisibleButton(string strId, Vector2 size, int flags = 0) => ImGui.InvisibleButton(strId, size, (ImGuiButtonFlags)flags);

	/// <inheritdoc />
	public bool ArrowButton(string strId, int dir) => ImGui.ArrowButton(strId, (ImGuiDir)dir);

	// Checkboxes
	/// <inheritdoc />
	public bool Checkbox(string label, ref bool v) => ImGui.Checkbox(label, ref v);

	/// <inheritdoc />
	public bool CheckboxFlags(string label, ref int flags, int flagsValue) => ImGui.CheckboxFlags(label, ref flags, flagsValue);

	/// <inheritdoc />
	public bool CheckboxFlags(string label, ref uint flags, uint flagsValue) => ImGui.CheckboxFlags(label, ref flags, flagsValue);

	// Radio Buttons
	/// <inheritdoc />
	public bool RadioButton(string label, bool active) => ImGui.RadioButton(label, active);

	/// <inheritdoc />
	public bool RadioButton(string label, ref int v, int vButton) => ImGui.RadioButton(label, ref v, vButton);

	// Progress Bar
	/// <inheritdoc />
	public void ProgressBar(float fraction, Vector2 sizeArg = default, string? overlay = null) => ImGui.ProgressBar(fraction, sizeArg, overlay);

	/// <inheritdoc />
	public void Bullet() => ImGui.Bullet();

	// Images
	/// <inheritdoc />
	public void Image(nint textureId, Vector2 imageSize, Vector2 uv0 = default, Vector2 uv1 = default, Vector4 tintCol = default, Vector4 borderCol = default) =>
		// TODO: Implement proper Image method once we understand the Hexa.NET.ImGui API better
		throw new NotImplementedException("Image method needs to be implemented with correct Hexa.NET.ImGui API usage");

	/// <inheritdoc />
	public bool ImageButton(string strId, nint textureId, Vector2 imageSize, Vector2 uv0 = default, Vector2 uv1 = default, Vector4 bgCol = default, Vector4 tintCol = default) =>
		// TODO: Implement proper ImageButton method once we understand the Hexa.NET.ImGui API better
		throw new NotImplementedException("ImageButton method needs to be implemented with correct Hexa.NET.ImGui API usage");

	// Combos
	/// <inheritdoc />
	public bool BeginCombo(string label, string previewValue, int flags = 0) => ImGui.BeginCombo(label, previewValue, (ImGuiComboFlags)flags);

	/// <inheritdoc />
	public void EndCombo() => ImGui.EndCombo();

	/// <inheritdoc />
	public bool Combo(string label, ref int currentItem, string[] items, int popupMaxHeightInItems = -1) => ImGui.Combo(label, ref currentItem, items, popupMaxHeightInItems);

	// Input Text
	/// <inheritdoc />
	public bool InputText(string label, byte* buf, nuint bufSize, int flags = 0) => ImGui.InputText(label, buf, bufSize, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputTextMultiline(string label, byte* buf, nuint bufSize, Vector2 size = default, int flags = 0) => ImGui.InputTextMultiline(label, buf, bufSize, size, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputTextWithHint(string label, string hint, byte* buf, nuint bufSize, int flags = 0) => ImGui.InputTextWithHint(label, hint, buf, bufSize, (ImGuiInputTextFlags)flags);

	// Input Scalars
	/// <inheritdoc />
	public bool InputFloat(string label, ref float v, float stepSize = 0.0f, float stepFast = 0.0f, string format = "%.3f", int flags = 0) =>
		ImGui.InputFloat(label, ref v, stepSize, stepFast, format, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputFloat2(string label, ref Vector2 v, string format = "%.3f", int flags = 0) => ImGui.InputFloat2(label, ref v, format, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputFloat3(string label, ref Vector3 v, string format = "%.3f", int flags = 0) => ImGui.InputFloat3(label, ref v, format, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputFloat4(string label, ref Vector4 v, string format = "%.3f", int flags = 0) => ImGui.InputFloat4(label, ref v, format, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputInt(string label, ref int v, int stepSize = 1, int stepFast = 100, int flags = 0) =>
		ImGui.InputInt(label, ref v, stepSize, stepFast, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputInt2(string label, int* v, int flags = 0) => ImGui.InputInt2(label, v, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputInt3(string label, int* v, int flags = 0) => ImGui.InputInt3(label, v, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputInt4(string label, int* v, int flags = 0) => ImGui.InputInt4(label, v, (ImGuiInputTextFlags)flags);

	/// <inheritdoc />
	public bool InputDouble(string label, ref double v, double stepSize = 0.0, double stepFast = 0.0, string format = "%.6f", int flags = 0) =>
		ImGui.InputDouble(label, ref v, stepSize, stepFast, format, (ImGuiInputTextFlags)flags);

	// Sliders
	/// <inheritdoc />
	public bool SliderFloat(string label, ref float v, float vMin, float vMax, string format = "%.3f", int flags = 0) => ImGui.SliderFloat(label, ref v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool SliderFloat2(string label, ref Vector2 v, float vMin, float vMax, string format = "%.3f", int flags = 0) => ImGui.SliderFloat2(label, ref v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool SliderFloat3(string label, ref Vector3 v, float vMin, float vMax, string format = "%.3f", int flags = 0) => ImGui.SliderFloat3(label, ref v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool SliderFloat4(string label, ref Vector4 v, float vMin, float vMax, string format = "%.3f", int flags = 0) => ImGui.SliderFloat4(label, ref v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool SliderAngle(string label, ref float vRad, float vDegreesMin = -360.0f, float vDegreesMax = 360.0f, string format = "%.0f deg", int flags = 0) => ImGui.SliderAngle(label, ref vRad, vDegreesMin, vDegreesMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool SliderInt(string label, ref int v, int vMin, int vMax, string format = "%d", int flags = 0) => ImGui.SliderInt(label, ref v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool SliderInt2(string label, int* v, int vMin, int vMax, string format = "%d", int flags = 0) => ImGui.SliderInt2(label, v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool SliderInt3(string label, int* v, int vMin, int vMax, string format = "%d", int flags = 0) => ImGui.SliderInt3(label, v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool SliderInt4(string label, int* v, int vMin, int vMax, string format = "%d", int flags = 0) => ImGui.SliderInt4(label, v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	// Vertical Sliders
	/// <inheritdoc />
	public bool VSliderFloat(string label, Vector2 size, ref float v, float vMin, float vMax, string format = "%.3f", int flags = 0) => ImGui.VSliderFloat(label, size, ref v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool VSliderInt(string label, Vector2 size, ref int v, int vMin, int vMax, string format = "%d", int flags = 0) => ImGui.VSliderInt(label, size, ref v, vMin, vMax, format, (ImGuiSliderFlags)flags);

	// Drags (Alternative to Sliders)
	/// <inheritdoc />
	public bool DragFloat(string label, ref float v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", int flags = 0) => ImGui.DragFloat(label, ref v, vSpeed, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragFloat2(string label, ref Vector2 v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", int flags = 0) => ImGui.DragFloat2(label, ref v, vSpeed, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragFloat3(string label, ref Vector3 v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", int flags = 0) => ImGui.DragFloat3(label, ref v, vSpeed, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragFloat4(string label, ref Vector4 v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", int flags = 0) => ImGui.DragFloat4(label, ref v, vSpeed, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragInt(string label, ref int v, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", int flags = 0) => ImGui.DragInt(label, ref v, vSpeed, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragInt2(string label, int* v, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", int flags = 0) => ImGui.DragInt2(label, v, vSpeed, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragInt3(string label, int* v, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", int flags = 0) => ImGui.DragInt3(label, v, vSpeed, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragInt4(string label, int* v, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", int flags = 0) => ImGui.DragInt4(label, v, vSpeed, vMin, vMax, format, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragFloatRange2(string label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, string format = "%.3f", string? formatMax = null, int flags = 0) => ImGui.DragFloatRange2(label, ref vCurrentMin, ref vCurrentMax, vSpeed, vMin, vMax, format, formatMax, (ImGuiSliderFlags)flags);

	/// <inheritdoc />
	public bool DragIntRange2(string label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed = 1.0f, int vMin = 0, int vMax = 0, string format = "%d", string? formatMax = null, int flags = 0) => ImGui.DragIntRange2(label, ref vCurrentMin, ref vCurrentMax, vSpeed, vMin, vMax, format, formatMax, (ImGuiSliderFlags)flags);

	// Color Controls
	/// <inheritdoc />
	public bool ColorEdit3(string label, ref Vector3 col, int flags = 0) => ImGui.ColorEdit3(label, ref col, (ImGuiColorEditFlags)flags);

	/// <inheritdoc />
	public bool ColorEdit4(string label, ref Vector4 col, int flags = 0) => ImGui.ColorEdit4(label, ref col, (ImGuiColorEditFlags)flags);

	/// <inheritdoc />
	public bool ColorPicker3(string label, ref Vector3 col, int flags = 0) => ImGui.ColorPicker3(label, ref col, (ImGuiColorEditFlags)flags);

	/// <inheritdoc />
	public bool ColorPicker4(string label, ref Vector4 col, int flags = 0, float* refCol = null) => ImGui.ColorPicker4(label, ref col, (ImGuiColorEditFlags)flags, refCol);

	/// <inheritdoc />
	public bool ColorButton(string descId, Vector4 col, int flags = 0, Vector2 size = default) => ImGui.ColorButton(descId, col, (ImGuiColorEditFlags)flags, size);

	/// <inheritdoc />
	public void SetColorEditOptions(int flags) => ImGui.SetColorEditOptions((ImGuiColorEditFlags)flags);

	// Trees
	/// <inheritdoc />
	public bool TreeNode(string label) => ImGui.TreeNode(label);

	/// <inheritdoc />
	public bool TreeNode(string strId, string fmt) => ImGui.TreeNode(strId, fmt);

	/// <inheritdoc />
	public bool TreeNode(void* ptrId, string fmt) => ImGui.TreeNode(ptrId, fmt);

	/// <inheritdoc />
	public bool TreeNodeExtended(string label, int flags = 0) =>
		ImGui.TreeNodeEx(label, (ImGuiTreeNodeFlags)flags);

	/// <inheritdoc />
	public bool TreeNodeExtended(string strId, int flags, string fmt) =>
		ImGui.TreeNodeEx(strId, (ImGuiTreeNodeFlags)flags, fmt);

	/// <inheritdoc />
	public bool TreeNodeExtended(void* ptrId, int flags, string fmt) =>
		ImGui.TreeNodeEx(ptrId, (ImGuiTreeNodeFlags)flags, fmt);

	/// <inheritdoc />
	public void TreePop() => ImGui.TreePop();

	/// <inheritdoc />
	public float GetTreeNodeToLabelSpacing() => ImGui.GetTreeNodeToLabelSpacing();

	/// <inheritdoc />
	public bool CollapsingHeader(string label, int flags = 0) => ImGui.CollapsingHeader(label, (ImGuiTreeNodeFlags)flags);

	/// <inheritdoc />
	public bool CollapsingHeader(string label, ref bool pVisible, int flags = 0) => ImGui.CollapsingHeader(label, ref pVisible, (ImGuiTreeNodeFlags)flags);

	/// <inheritdoc />
	public void SetNextItemOpen(bool isOpen, int cond = 0) => ImGui.SetNextItemOpen(isOpen, (ImGuiCond)cond);

	// Selectables
	/// <inheritdoc />
	public bool Selectable(string label, bool selected = false, int flags = 0, Vector2 size = default) => ImGui.Selectable(label, selected, (ImGuiSelectableFlags)flags, size);

	/// <inheritdoc />
	public bool Selectable(string label, ref bool pSelected, int flags = 0, Vector2 size = default) => ImGui.Selectable(label, ref pSelected, (ImGuiSelectableFlags)flags, size);

	// List Boxes
	/// <inheritdoc />
	public bool BeginListBox(string label, Vector2 size = default) => ImGui.BeginListBox(label, size);

	/// <inheritdoc />
	public void EndListBox() => ImGui.EndListBox();

	/// <inheritdoc />
	public bool ListBox(string label, ref int currentItem, string[] items, int heightInItems = -1) => ImGui.ListBox(label, ref currentItem, items, heightInItems);

	// Plot/Graph widgets (simple versions)
	/// <inheritdoc />
	public void PlotLines(string label, float* values, int valuesCount, int valuesOffset = 0, string? overlayText = null, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, Vector2 graphSize = default, int stride = sizeof(float)) => ImGui.PlotLines(label, values, valuesCount, valuesOffset, overlayText, scaleMin, scaleMax, graphSize, stride);

	/// <inheritdoc />
	public void PlotHistogram(string label, float* values, int valuesCount, int valuesOffset = 0, string? overlayText = null, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, Vector2 graphSize = default, int stride = sizeof(float)) => ImGui.PlotHistogram(label, values, valuesCount, valuesOffset, overlayText, scaleMin, scaleMax, graphSize, stride);

	// Menus
	/// <inheritdoc />
	public bool BeginMainMenuBar() => ImGui.BeginMainMenuBar();

	/// <inheritdoc />
	public void EndMainMenuBar() => ImGui.EndMainMenuBar();

	/// <inheritdoc />
	public bool BeginMenuBar() => ImGui.BeginMenuBar();

	/// <inheritdoc />
	public void EndMenuBar() => ImGui.EndMenuBar();

	/// <inheritdoc />
	public bool BeginMenu(string label, bool enabled = true) => ImGui.BeginMenu(label, enabled);

	/// <inheritdoc />
	public void EndMenu() => ImGui.EndMenu();

	/// <inheritdoc />
	public bool MenuItem(string label, string? shortcut = null, bool selected = false, bool enabled = true) => ImGui.MenuItem(label, shortcut, selected, enabled);

	/// <inheritdoc />
	public bool MenuItem(string label, string shortcut, ref bool pSelected, bool enabled = true) => ImGui.MenuItem(label, shortcut, ref pSelected, enabled);

	// Tooltips
	/// <inheritdoc />
	public void BeginTooltip() => ImGui.BeginTooltip();

	/// <inheritdoc />
	public void EndTooltip() => ImGui.EndTooltip();

	/// <inheritdoc />
	public void SetTooltip(string text) => ImGui.SetTooltip(text);

	// Popups, Modals
	/// <inheritdoc />
	public bool BeginPopup(string strId, int flags = 0) => ImGui.BeginPopup(strId, (ImGuiWindowFlags)flags);

	/// <inheritdoc />
	public bool BeginPopupModal(string name, ref bool pOpen, int flags = 0) => ImGui.BeginPopupModal(name, ref pOpen, (ImGuiWindowFlags)flags);

	/// <inheritdoc />
	public void EndPopup() => ImGui.EndPopup();

	/// <inheritdoc />
	public void OpenPopup(string strId, int popupFlags = 0) => ImGui.OpenPopup(strId, (ImGuiPopupFlags)popupFlags);

	/// <inheritdoc />
	public void OpenPopupOnItemClick(string strId = "", int popupFlags = 1) => ImGui.OpenPopupOnItemClick(strId, (ImGuiPopupFlags)popupFlags);

	/// <inheritdoc />
	public void CloseCurrentPopup() => ImGui.CloseCurrentPopup();

	/// <inheritdoc />
	public bool BeginPopupContextItem(string strId = "", int popupFlags = 1) => ImGui.BeginPopupContextItem(strId, (ImGuiPopupFlags)popupFlags);

	/// <inheritdoc />
	public bool BeginPopupContextWindow(string strId = "", int popupFlags = 1) => ImGui.BeginPopupContextWindow(strId, (ImGuiPopupFlags)popupFlags);

	/// <inheritdoc />
	public bool BeginPopupContextVoid(string strId = "", int popupFlags = 1) => ImGui.BeginPopupContextVoid(strId, (ImGuiPopupFlags)popupFlags);

	/// <inheritdoc />
	public bool IsPopupOpen(string strId, int flags = 0) => ImGui.IsPopupOpen(strId, (ImGuiPopupFlags)flags);

	// Tables
	/// <inheritdoc />
	public bool BeginTable(string strId, int columnsCount, int flags = 0, Vector2 outerSize = default, float innerWidth = 0.0f) => ImGui.BeginTable(strId, columnsCount, (ImGuiTableFlags)flags, outerSize, innerWidth);

	/// <inheritdoc />
	public void EndTable() => ImGui.EndTable();

	/// <inheritdoc />
	public void TableNextRow(int rowFlags = 0, float minRowHeight = 0.0f) => ImGui.TableNextRow((ImGuiTableRowFlags)rowFlags, minRowHeight);

	/// <inheritdoc />
	public bool TableNextColumn() => ImGui.TableNextColumn();

	/// <inheritdoc />
	public bool TableSetColumnIndex(int columnN) => ImGui.TableSetColumnIndex(columnN);

	/// <inheritdoc />
	public void TableSetupColumn(string label, int flags = 0, float initWidthOrWeight = 0.0f, uint userId = 0) => ImGui.TableSetupColumn(label, (ImGuiTableColumnFlags)flags, initWidthOrWeight, userId);

	/// <inheritdoc />
	public void TableSetupScrollFreeze(int cols, int rows) => ImGui.TableSetupScrollFreeze(cols, rows);

	/// <inheritdoc />
	public void TableHeadersRow() => ImGui.TableHeadersRow();

	/// <inheritdoc />
	public void TableHeader(string label) => ImGui.TableHeader(label);

	/// <inheritdoc />
	public nint TableGetSortSpecs()
	{
		ImGuiTableSortSpecsPtr sortSpecs = ImGui.TableGetSortSpecs();
		return (nint)sortSpecs.Handle;
	}

	/// <inheritdoc />
	public int TableGetColumnCount() => ImGui.TableGetColumnCount();

	/// <inheritdoc />
	public int TableGetColumnIndex() => ImGui.TableGetColumnIndex();

	/// <inheritdoc />
	public int TableGetRowIndex() => ImGui.TableGetRowIndex();

	/// <inheritdoc />
	public string TableGetColumnName(int columnN = -1)
	{
		byte* namePtr = ImGui.TableGetColumnName(columnN);
		return new string((sbyte*)namePtr);
	}

	/// <inheritdoc />
	public int TableGetColumnFlags(int columnN = -1) => (int)ImGui.TableGetColumnFlags(columnN);

	/// <inheritdoc />
	public void TableSetColumnEnabled(int columnN, bool v) => ImGui.TableSetColumnEnabled(columnN, v);

	/// <inheritdoc />
	public void TableSetBgColor(int target, uint color, int columnN = -1) => ImGui.TableSetBgColor((ImGuiTableBgTarget)target, color, columnN);

	// Tab Bars, Tabs
	/// <inheritdoc />
	public bool BeginTabBar(string strId, int flags = 0) => ImGui.BeginTabBar(strId, (ImGuiTabBarFlags)flags);

	/// <inheritdoc />
	public void EndTabBar() => ImGui.EndTabBar();

	/// <inheritdoc />
	public bool BeginTabItem(string label, ref bool pOpen, int flags = 0) => ImGui.BeginTabItem(label, ref pOpen, (ImGuiTabItemFlags)flags);

	/// <inheritdoc />
	public bool BeginTabItem(string label, int flags = 0) => ImGui.BeginTabItem(label, (ImGuiTabItemFlags)flags);

	/// <inheritdoc />
	public void EndTabItem() => ImGui.EndTabItem();

	/// <inheritdoc />
	public void SetTabItemClosed(string tabOrDockedWindowLabel) => ImGui.SetTabItemClosed(tabOrDockedWindowLabel);

	// Docking
	/// <inheritdoc />
	public uint DockSpace(uint id, Vector2 size = default, int flags = 0, nint windowClass = 0) => ImGui.DockSpace(id, size, (ImGuiDockNodeFlags)flags, (ImGuiWindowClass*)windowClass);

	/// <inheritdoc />
	public uint DockSpaceOverViewport(nint viewport = 0, int flags = 0, nint windowClass = 0) => ImGui.DockSpaceOverViewport(viewport == 0 ? null : (ImGuiViewport*)viewport, (ImGuiDockNodeFlags)flags, (ImGuiWindowClass*)windowClass);

	/// <inheritdoc />
	public void SetNextWindowDockID(uint dockId, int cond = 0) => ImGui.SetNextWindowDockID(dockId, (ImGuiCond)cond);

	/// <inheritdoc />
	public void SetNextWindowClass(nint windowClass) => ImGui.SetNextWindowClass((ImGuiWindowClass*)windowClass);

	/// <inheritdoc />
	public uint GetWindowDockID() => ImGui.GetWindowDockID();

	/// <inheritdoc />
	public bool IsWindowDocked() => ImGui.IsWindowDocked();

	// Layout
	/// <inheritdoc />
	public void Separator() => ImGui.Separator();

	/// <inheritdoc />
	public void SameLine(float offsetFromStartX = 0.0f, float spacing = -1.0f) => ImGui.SameLine(offsetFromStartX, spacing);

	/// <inheritdoc />
	public void NewLine() => ImGui.NewLine();

	/// <inheritdoc />
	public void Spacing() => ImGui.Spacing();

	/// <inheritdoc />
	public void Dummy(Vector2 size) => ImGui.Dummy(size);

	/// <inheritdoc />
	public void Indent(float indentW = 0.0f) => ImGui.Indent(indentW);

	/// <inheritdoc />
	public void Unindent(float indentW = 0.0f) => ImGui.Unindent(indentW);

	/// <inheritdoc />
	public void BeginGroup() => ImGui.BeginGroup();

	/// <inheritdoc />
	public void EndGroup() => ImGui.EndGroup();

	/// <inheritdoc />
	public Vector2 GetCursorPos() => ImGui.GetCursorPos();

	/// <inheritdoc />
	public float GetCursorPosX() => ImGui.GetCursorPosX();

	/// <inheritdoc />
	public float GetCursorPosY() => ImGui.GetCursorPosY();

	/// <inheritdoc />
	public void SetCursorPos(Vector2 localPos) => ImGui.SetCursorPos(localPos);

	/// <inheritdoc />
	public void SetCursorPosX(float localX) => ImGui.SetCursorPosX(localX);

	/// <inheritdoc />
	public void SetCursorPosY(float localY) => ImGui.SetCursorPosY(localY);

	/// <inheritdoc />
	public Vector2 GetCursorStartPos() => ImGui.GetCursorStartPos();

	/// <inheritdoc />
	public Vector2 GetCursorScreenPos() => ImGui.GetCursorScreenPos();

	/// <inheritdoc />
	public void SetCursorScreenPos(Vector2 pos) => ImGui.SetCursorScreenPos(pos);

	/// <inheritdoc />
	public void AlignTextToFramePadding() => ImGui.AlignTextToFramePadding();

	/// <inheritdoc />
	public float GetTextLineHeight() => ImGui.GetTextLineHeight();

	/// <inheritdoc />
	public float GetTextLineHeightWithSpacing() => ImGui.GetTextLineHeightWithSpacing();

	/// <inheritdoc />
	public float GetFrameHeight() => ImGui.GetFrameHeight();

	/// <inheritdoc />
	public float GetFrameHeightWithSpacing() => ImGui.GetFrameHeightWithSpacing();

	// ID stack/scopes
	/// <inheritdoc />
	public void PushID(string strId) => ImGui.PushID(strId);

	/// <inheritdoc />
	public void PushID(int intId) => ImGui.PushID(intId);

	/// <inheritdoc />
	public void PushID(void* ptrId) => ImGui.PushID(ptrId);

	/// <inheritdoc />
	public void PopID() => ImGui.PopID();

	/// <inheritdoc />
	public uint GetID(string strId) => ImGui.GetID(strId);

	/// <inheritdoc />
	public uint GetID(void* ptrId) => ImGui.GetID(ptrId);

	// Widgets Utilities
	/// <inheritdoc />
	public Vector2 GetItemRectMin() => ImGui.GetItemRectMin();

	/// <inheritdoc />
	public Vector2 GetItemRectMax() => ImGui.GetItemRectMax();

	/// <inheritdoc />
	public Vector2 GetItemRectSize() => ImGui.GetItemRectSize();

	/// <inheritdoc />
	public void SetNextItemWidth(float itemWidth) => ImGui.SetNextItemWidth(itemWidth);

	/// <inheritdoc />
	public float CalcItemWidth() => ImGui.CalcItemWidth();

	/// <inheritdoc />
	public void PushItemWidth(float itemWidth) => ImGui.PushItemWidth(itemWidth);

	/// <inheritdoc />
	public void PopItemWidth() => ImGui.PopItemWidth();

	/// <inheritdoc />
	public void SetNextItemAllowOverlap() => ImGui.SetNextItemAllowOverlap();

	// Miscellaneous Utilities
	/// <inheritdoc />
	public bool IsItemHovered(int flags = 0) => ImGui.IsItemHovered((ImGuiHoveredFlags)flags);

	/// <inheritdoc />
	public bool IsItemActive() => ImGui.IsItemActive();

	/// <inheritdoc />
	public bool IsItemFocused() => ImGui.IsItemFocused();

	/// <inheritdoc />
	public bool IsItemClicked(int mouseButton = 0) => ImGui.IsItemClicked((ImGuiMouseButton)mouseButton);

	/// <inheritdoc />
	public bool IsItemVisible() => ImGui.IsItemVisible();

	/// <inheritdoc />
	public bool IsItemEdited() => ImGui.IsItemEdited();

	/// <inheritdoc />
	public bool IsItemActivated() => ImGui.IsItemActivated();

	/// <inheritdoc />
	public bool IsItemDeactivated() => ImGui.IsItemDeactivated();

	/// <inheritdoc />
	public bool IsItemDeactivatedAfterEdit() => ImGui.IsItemDeactivatedAfterEdit();

	/// <inheritdoc />
	public bool IsAnyItemHovered() => ImGui.IsAnyItemHovered();

	/// <inheritdoc />
	public bool IsAnyItemActive() => ImGui.IsAnyItemActive();

	/// <inheritdoc />
	public bool IsAnyItemFocused() => ImGui.IsAnyItemFocused();

	/// <inheritdoc />
	public uint GetItemID() => ImGui.GetItemID();

	/// <inheritdoc />
	public double GetTime() => ImGui.GetTime();

	/// <inheritdoc />
	public int GetFrameCount() => ImGui.GetFrameCount();

	// Background/Foreground Draw Lists
	/// <inheritdoc />
	public nint GetBackgroundDrawList()
	{
		ImDrawListPtr drawList = ImGui.GetBackgroundDrawList();
		return (nint)drawList.Handle;
	}

	/// <inheritdoc />
	public nint GetForegroundDrawList()
	{
		ImDrawListPtr drawList = ImGui.GetForegroundDrawList();
		return (nint)drawList.Handle;
	}

	/// <inheritdoc />
	public nint GetWindowDrawList()
	{
		ImDrawListPtr drawList = ImGui.GetWindowDrawList();
		return (nint)drawList.Handle;
	}

	// Text Utilities
	/// <inheritdoc />
	public Vector2 CalcTextSize(string text, bool hideTextAfterDoubleHash = false, float wrapWidth = -1.0f) => ImGui.CalcTextSize(text, hideTextAfterDoubleHash, wrapWidth);

	// Color Utilities
	/// <inheritdoc />
	public void ColorConvertHSVtoRGB(float h, float s, float v, out float r, out float g, out float b)
	{
		r = 0;
		g = 0;
		b = 0;
		ImGui.ColorConvertHSVtoRGB(h, s, v, ref r, ref g, ref b);
	}

	/// <inheritdoc />
	public void ColorConvertRGBtoHSV(float r, float g, float b, out float h, out float s, out float v)
	{
		h = 0;
		s = 0;
		v = 0;
		ImGui.ColorConvertRGBtoHSV(r, g, b, ref h, ref s, ref v);
	}

	// Inputs Utilities: Keyboard
	/// <inheritdoc />
	public int GetKeyIndex(int key) =>
		// In newer ImGui versions, key index is the same as the key value
		key;

	/// <inheritdoc />
	public bool IsKeyDown(int key) => ImGui.IsKeyDown((ImGuiKey)key);

	/// <inheritdoc />
	public bool IsKeyPressed(int key, bool repeat = true) => ImGui.IsKeyPressed((ImGuiKey)key, repeat);

	/// <inheritdoc />
	public bool IsKeyReleased(int key) => ImGui.IsKeyReleased((ImGuiKey)key);

	/// <inheritdoc />
	public int GetKeyPressedAmount(int key, float repeatDelay, float rate) => ImGui.GetKeyPressedAmount((ImGuiKey)key, repeatDelay, rate);

	/// <inheritdoc />
	public string GetKeyName(int key)
	{
		byte* namePtr = ImGui.GetKeyName((ImGuiKey)key);
		return new string((sbyte*)namePtr);
	}

	/// <inheritdoc />
	public void SetNextFrameWantCaptureKeyboard(bool wantCaptureKeyboard) => ImGui.SetNextFrameWantCaptureKeyboard(wantCaptureKeyboard);

	// Inputs Utilities: Mouse
	/// <inheritdoc />
	public bool IsMouseDown(int button) => ImGui.IsMouseDown((ImGuiMouseButton)button);

	/// <inheritdoc />
	public bool IsMouseClicked(int button, bool repeat = false) => ImGui.IsMouseClicked((ImGuiMouseButton)button, repeat);

	/// <inheritdoc />
	public bool IsMouseReleased(int button) => ImGui.IsMouseReleased((ImGuiMouseButton)button);

	/// <inheritdoc />
	public bool IsMouseDoubleClicked(int button) => ImGui.IsMouseDoubleClicked((ImGuiMouseButton)button);

	/// <inheritdoc />
	public int GetMouseClickedCount(int button) => ImGui.GetMouseClickedCount((ImGuiMouseButton)button);

	/// <inheritdoc />
	public bool IsMouseHoveringRect(Vector2 rMin, Vector2 rMax, bool clip = true) => ImGui.IsMouseHoveringRect(rMin, rMax, clip);

	/// <inheritdoc />
	public bool IsMousePosValid() => ImGui.IsMousePosValid();

	/// <inheritdoc />
	public bool IsAnyMouseDown() => ImGui.IsAnyMouseDown();

	/// <inheritdoc />
	public Vector2 GetMousePos() => ImGui.GetMousePos();

	/// <inheritdoc />
	public Vector2 GetMousePosOnOpeningCurrentPopup() => ImGui.GetMousePosOnOpeningCurrentPopup();

	/// <inheritdoc />
	public bool IsMouseDragging(int button, float lockThreshold = -1.0f) => ImGui.IsMouseDragging((ImGuiMouseButton)button, lockThreshold);

	/// <inheritdoc />
	public Vector2 GetMouseDragDelta(int button = 0, float lockThreshold = -1.0f) => ImGui.GetMouseDragDelta((ImGuiMouseButton)button, lockThreshold);

	/// <inheritdoc />
	public void ResetMouseDragDelta(int button = 0) => ImGui.ResetMouseDragDelta((ImGuiMouseButton)button);

	/// <inheritdoc />
	public float GetMouseWheel() => ImGui.GetIO().MouseWheel;

	/// <inheritdoc />
	public float GetMouseWheelH() => ImGui.GetIO().MouseWheelH;

	/// <inheritdoc />
	public void SetNextFrameWantCaptureMouse(bool wantCaptureMouse) => ImGui.SetNextFrameWantCaptureMouse(wantCaptureMouse);

	// Clipboard Utilities
	/// <inheritdoc />
	public string GetClipboardText()
	{
		byte* textPtr = ImGui.GetClipboardText();
		return new string((sbyte*)textPtr);
	}

	/// <inheritdoc />
	public void SetClipboardText(string text) => ImGui.SetClipboardText(text);

	// Settings/.Ini Utilities
	/// <inheritdoc />
	public void LoadIniSettingsFromDisk(string iniFilename) => ImGui.LoadIniSettingsFromDisk(iniFilename);

	/// <inheritdoc />
	public void LoadIniSettingsFromMemory(string iniData, nuint iniSize = 0) => ImGui.LoadIniSettingsFromMemory(iniData, iniSize);

	/// <inheritdoc />
	public void SaveIniSettingsToDisk(string iniFilename) => ImGui.SaveIniSettingsToDisk(iniFilename);

	/// <inheritdoc />
	public string SaveIniSettingsToMemory(nuint* outIniSize = null)
	{
		byte* iniDataPtr = ImGui.SaveIniSettingsToMemory(outIniSize);
		return new string((sbyte*)iniDataPtr);
	}

	// Platform/Viewport Support
	/// <inheritdoc />
	public void UpdatePlatformWindows() => ImGui.UpdatePlatformWindows();

	/// <inheritdoc />
	public void RenderPlatformWindowsDefault() => ImGui.RenderPlatformWindowsDefault();

	/// <inheritdoc />
	public void DestroyPlatformWindows() => ImGui.DestroyPlatformWindows();

	/// <inheritdoc />
	public nint GetMainViewport()
	{
		ImGuiViewportPtr viewport = ImGui.GetMainViewport();
		return (nint)viewport.Handle;
	}

	// Version Info
	/// <inheritdoc />
	public string GetVersion()
	{
		byte* versionPtr = ImGui.GetVersion();
		return new string((sbyte*)versionPtr);
	}

	/// <inheritdoc />
	public string GetVersionNumber()
	{
		// Return the same as GetVersion for now
		byte* versionPtr = ImGui.GetVersion();
		return new string((sbyte*)versionPtr);
	}

	/// <inheritdoc />
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				// Dispose managed state (managed objects)
			}

			_disposed = true;
		}
	}

	/// <inheritdoc />
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
