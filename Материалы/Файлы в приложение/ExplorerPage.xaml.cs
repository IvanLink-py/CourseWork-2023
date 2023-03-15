using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NfModels.Infrastructure.Commands;
using NfModels.ViewModels.Base;
using NfModels.ViewModels.Windows;
using NfModels.Views.Dialogs;
using NfModels.Views.Dialogs.PickDialogs;

namespace NfModels.Views.Pages;

/// <summary>
/// Страница браузера объектов фабрики
/// </summary>
public partial class ExplorerPage : Page
{
    private readonly bool _dialogMode;
    internal NewsFactoryObjectViewModel? DialogResult;
    private readonly PickObjectDialog? _dialog;

    public ExplorerPage()
    {
        InitializeComponent();
    }

    public ExplorerPage(ExplorerPreset preset)
    {
        InitializeComponent();
        ((ExplorerViewModel)DataContext)?.LoadPreset(preset);
    }
    
    internal ExplorerPage(ExplorerPreset preset, PickObjectDialog dialog)
    {
        InitializeComponent();
        ((ExplorerViewModel)DataContext)?.LoadPreset(preset, true);
        
        _dialogMode = true;
        _dialog = dialog;
        dialog.SetPage(this);
    }

    private void OnItemDoubleClick(object sender, MouseButtonEventArgs e)
    {
        e.Handled = true;
        var viewModel = (NewsFactoryObjectViewModel)((FrameworkElement)sender).DataContext;

        if (viewModel is null) return;
        if (_dialogMode && _dialog is not null)
        {
            DialogResult = viewModel;
            _dialog.Close();
        }
        else OpenNewsFactoryObjectCommand.OpenObject(viewModel);
    }
}