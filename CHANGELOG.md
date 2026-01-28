## v1.0.1-pre.1 (prerelease)

Changes since v1.0.0:

- Sync .runsettings ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync .editorconfig ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync .gitignore ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync .github\workflows\update-sdks.yml ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync .gitattributes ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync scripts\update-winget-manifests.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync .github\workflows\dotnet.yml ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Merge remote-tracking branch 'refs/remotes/origin/main' ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync global.json ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync scripts\PSBuild.psm1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync global.json ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.0.0 (major)

- Initial commit: Add project structure with essential configuration files, including .editorconfig, .gitattributes, .gitignore, and CI/CD workflows. Introduce PowerShell build automation module (PSBuild) and related scripts for .NET applications. Include licensing, authorship, and changelog files for project documentation. ([@matt-edmondson](https://github.com/matt-edmondson))
- Add drawing API methods to IImGuiProvider and HexaNetImGuiProvider for low-level rendering operations. Implemented methods for drawing lines, rectangles, circles, triangles, and text, enhancing the rendering capabilities of the library. ([@matt-edmondson](https://github.com/matt-edmondson))
- Refactor HexaNetImGuiProvider to enhance context management and expand window handling capabilities. Updated methods to utilize new ImGuiContextPtr and added overloads for window creation with flags. Introduced additional methods for child windows and improved documentation for existing methods in IImGuiProvider interface. ([@matt-edmondson](https://github.com/matt-edmondson))
- Implement Image and ImageButton methods in HexaNetImGuiProvider with default parameter handling and appropriate overloads for ImGui usage. Enhance functionality by creating ImTextureRef from textureId and setting default values for UV coordinates, tint, and background colors. ([@matt-edmondson](https://github.com/matt-edmondson))
- Initial implementation of ImGuiProvider library, including core interfaces, context management, and backend implementations for Hexa.NET. Added project and solution files for Visual Studio, along with dependency injection support for ImGui services and OpenGL/GLFW backends. ([@matt-edmondson](https://github.com/matt-edmondson))
- Update ImGuiProvider library with new dependencies, improved context management, and added README documentation. Updated Hexa.NET.ImGui version to 2.2.8.4, introduced Microsoft.Extensions.DependencyInjection packages, and removed obsolete backend implementations. Added examples for basic and advanced usage. ([@matt-edmondson](https://github.com/matt-edmondson))
- Update Microsoft.Extensions.DependencyInjection packages to version 9.0.7 in Directory.Packages.props for improved dependency management. ([@matt-edmondson](https://github.com/matt-edmondson))
