using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using appointment_management_system.ViewModels;
using appointment_management_system.Views;

namespace appointment_management_system;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            Database_access.get_instance();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindow_view_model(desktop),
            };
            desktop.MainWindow.Closed += (sender,e) => Shutdown(desktop);
            
        }

        base.OnFrameworkInitializationCompleted();
    }
    private void Shutdown(IClassicDesktopStyleApplicationLifetime desktop)
    {
        Database_access.close_database();
        desktop.Shutdown();
    }
    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}