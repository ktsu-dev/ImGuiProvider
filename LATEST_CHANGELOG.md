## v2.0.2 (patch)

Changes since v2.0.1:

- Throw ObjectDisposedException from ImGuiContext after Dispose [patch] (#103) (matt-edmondson)
- Shut an initialized backend down when RemoveBackend detaches it [patch] (#104) (matt-edmondson)
- Give the font atlas its own copy of AddFontFromMemoryTTF data [patch] (#90) (matt-edmondson)
- [patch] Stop ImGuiContext disposing the shared provider and its backends (#87) (matt-edmondson)
- [patch] Draw a pushed font at its own size instead of the current one (#86) (matt-edmondson)
- [patch] Resolve a registered backend as IPlatformBackend or IRendererBackend (#85) (matt-edmondson)
- Regenerate CompatibilitySuppressions.xml after the Polyfill 11.4.5 bump [patch] (#84) (matt-edmondson)

