using Microsoft.Extensions.DependencyInjection;
using ImGuiProvider.Interfaces;
using ImGuiProvider.Implementations.HexaNet;
using ImGuiProvider.Implementations.HexaNet.Backends;
using ImGuiProvider.Services;

namespace ImGuiProvider.Extensions
{
    /// <summary>
    /// Extension methods for registering ImGui services with dependency injection containers
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds ImGui services using Hexa.NET.ImGui implementation
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddImGui(this IServiceCollection services)
        {
            services.AddSingleton<IImGuiProvider, HexaNetImGuiProvider>();
            services.AddTransient<ImGuiContext>();

            return services;
        }

        /// <summary>
        /// Adds ImGui services with custom provider
        /// </summary>
        /// <typeparam name="TProvider">The provider implementation type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddImGui<TProvider>(this IServiceCollection services)
            where TProvider : class, IImGuiProvider
        {
            services.AddSingleton<IImGuiProvider, TProvider>();
            services.AddTransient<ImGuiContext>();

            return services;
        }

        /// <summary>
        /// Adds ImGui services with factory for custom configuration
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="factory">Factory function to create the provider</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddImGui(
            this IServiceCollection services,
            Func<IServiceProvider, IImGuiProvider> factory)
        {
            services.AddSingleton(factory);
            services.AddTransient<ImGuiContext>();

            return services;
        }

        /// <summary>
        /// Adds OpenGL3 renderer backend
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="glslVersion">GLSL version string (optional)</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddImGuiOpenGL3Backend(
            this IServiceCollection services,
            string? glslVersion = null)
        {
            services.AddTransient<IRendererBackend>(serviceProvider =>
                new HexaNetOpenGL3Backend(glslVersion: glslVersion));

            return services;
        }

        /// <summary>
        /// Adds GLFW platform backend
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="windowHandle">GLFW window handle</param>
        /// <param name="installCallbacks">Whether to install GLFW callbacks</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddImGuiGLFWBackend(
            this IServiceCollection services,
            nint windowHandle,
            bool installCallbacks = true)
        {
            services.AddTransient<IPlatformBackend>(serviceProvider =>
                new HexaNetGLFWBackend(windowHandle: windowHandle, installCallbacks: installCallbacks));

            return services;
        }

        /// <summary>
        /// Adds GLFW platform backend with factory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="factory">Factory function to create the backend</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddImGuiGLFWBackend(
            this IServiceCollection services,
            Func<IServiceProvider, HexaNetGLFWBackend> factory)
        {
            services.AddTransient<IPlatformBackend>(factory);

            return services;
        }

        /// <summary>
        /// Adds a custom backend
        /// </summary>
        /// <typeparam name="TBackend">The backend implementation type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddImGuiBackend<TBackend>(this IServiceCollection services)
            where TBackend : class, IImGuiBackend
        {
            services.AddTransient<IImGuiBackend, TBackend>();

            return services;
        }

        /// <summary>
        /// Adds a custom backend with factory
        /// </summary>
        /// <typeparam name="TBackend">The backend interface type</typeparam>
        /// <param name="services">The service collection</param>
        /// <param name="factory">Factory function to create the backend</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddImGuiBackend<TBackend>(
            this IServiceCollection services,
            Func<IServiceProvider, TBackend> factory)
            where TBackend : class, IImGuiBackend
        {
            services.AddTransient<TBackend>(factory);
            services.AddTransient<IImGuiBackend>(serviceProvider => serviceProvider.GetRequiredService<TBackend>());

            return services;
        }
    }
}
