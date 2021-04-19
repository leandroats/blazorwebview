using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDesktop
{
    public static class DependencySolver
    {

        private static readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private static IServiceProvider _serviceProvider;
        private static IServiceScope _serviceScope;

        private static void Adds()
        {
            _serviceCollection.AddScoped<FrmMain>();
        }

        private static void CreateScope()
        {
            _serviceProvider = _serviceCollection.BuildServiceProvider(validateScopes: true);
            _serviceScope = _serviceProvider.CreateScope();
        }

        public static IServiceProvider GetProvider() {
            return _serviceProvider;
        }

        public static void Start() {

            _serviceCollection.AddBlazorWebView();
            Adds();
            CreateScope();

        }

        public static T GetInstance<T>() where T : class
        {
            return _serviceScope.ServiceProvider.GetRequiredService<T>();
        }

    }
}
