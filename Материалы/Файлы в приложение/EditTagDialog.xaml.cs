using System.Windows;
using System.Windows.Media;
using NfModels.ViewModels.NewsFactoryObjects;
using Xceed.Wpf.Toolkit;

namespace NfModels.Views.Dialogs.EditDialogs;

/// <summary>
/// Окно редактирования тега
/// </summary>
public partial class EditTagDialog : EditDialogWindow
{
    public EditTagDialog()
    {
        DataContext = new TagViewModel();
        InitializeComponent();
    }

    public EditTagDialog(TagViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    private void ColorPicker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        var newColor = ((ColorPicker)sender).SelectedColor;
        if (newColor is not null) ((TagViewModel)DataContext).MyColor = newColor.Value;
    }
}