## v1.0.2 (patch)

Changes since v1.0.1:

- docs: update documentation for clarity and add testing instructions ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.0.1 (patch)

Changes since v1.0.0:

- chore: remove unused package versions from Directory.Packages.props ([@matt-edmondson](https://github.com/matt-edmondson))
- test: add ImGuiContext lifecycle tests with Moq ([@matt-edmondson](https://github.com/matt-edmondson))
- test: add ServiceCollectionExtensions DI registration tests ([@matt-edmondson](https://github.com/matt-edmondson))
- chore: remove examples from library (examples are in README) ([@matt-edmondson](https://github.com/matt-edmondson))
- fix: use standard ktsu.Sdk pattern in csproj to fix build ([@matt-edmondson](https://github.com/matt-edmondson))
- chore: add API compat suppression file for Polyfill multi-targeting ([@matt-edmondson](https://github.com/matt-edmondson))
- Add CLAUDE.md for project guidance and architecture overview ([@matt-edmondson](https://github.com/matt-edmondson))
- feat: add MSTest project with Moq for unit testing ([@matt-edmondson](https://github.com/matt-edmondson))
- Remove .github\workflows\project.yml ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.0.0 (major)

- Initial commit: Add project structure with essential configuration files, including .editorconfig, .gitattributes, .gitignore, and CI/CD workflows. Introduce PowerShell build automation module (PSBuild) and related scripts for .NET applications. Include licensing, authorship, and changelog files for project documentation. ([@matt-edmondson](https://github.com/matt-edmondson))
- Add drawing API methods to IImGuiProvider and HexaNetImGuiProvider for low-level rendering operations. Implemented methods for drawing lines, rectangles, circles, triangles, and text, enhancing the rendering capabilities of the library. ([@matt-edmondson](https://github.com/matt-edmondson))
- Refactor HexaNetImGuiProvider to enhance context management and expand window handling capabilities. Updated methods to utilize new ImGuiContextPtr and added overloads for window creation with flags. Introduced additional methods for child windows and improved documentation for existing methods in IImGuiProvider interface. ([@matt-edmondson](https://github.com/matt-edmondson))
- Implement Image and ImageButton methods in HexaNetImGuiProvider with default parameter handling and appropriate overloads for ImGui usage. Enhance functionality by creating ImTextureRef from textureId and setting default values for UV coordinates, tint, and background colors. ([@matt-edmondson](https://github.com/matt-edmondson))
- Initial implementation of ImGuiProvider library, including core interfaces, context management, and backend implementations for Hexa.NET. Added project and solution files for Visual Studio, along with dependency injection support for ImGui services and OpenGL/GLFW backends. ([@matt-edmondson](https://github.com/matt-edmondson))
- Update ImGuiProvider library with new dependencies, improved context management, and added README documentation. Updated Hexa.NET.ImGui version to 2.2.8.4, introduced Microsoft.Extensions.DependencyInjection packages, and removed obsolete backend implementations. Added examples for basic and advanced usage. ([@matt-edmondson](https://github.com/matt-edmondson))
- Update Microsoft.Extensions.DependencyInjection packages to version 9.0.7 in Directory.Packages.props for improved dependency management. ([@matt-edmondson](https://github.com/matt-edmondson))
