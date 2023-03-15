using System.Windows.Controls;
using System.Windows.Input;
using NfModels.Infrastructure.Commands;
using NfModels.ViewModels.Base;
using NfModels.ViewModels.Windows;
using NfModels.Views.Pages;

namespace ProjectManager.ViewModels.Windows;

/// <summary>
/// Модель представления главного окна
/// </summary>
internal class MainWindowViewModel : ViewModel
{
    public MainWindowViewModel()
    {
        GoToProjectPageCommand = new LambdaCommand(GoToProjectPageCommandExecute, CanGoToProjectPageCommandExecute);
        GoToMainPageCommand = new LambdaCommand(GoToMainPageCommandExecute, CanGoToMainPageCommandExecute);
        GoToMaterialsPageCommand =
            new LambdaCommand(GoToMaterialsPageCommandExecute, CanGoToMaterialsPageCommandExecute);
        GoToFilmingPageCommand = new LambdaCommand(GoToFilmingPageCommandExecute, CanGoToFilmingPageCommandExecute);
    }


    public ICommand GoToProjectPageCommand { get; }
    public ICommand GoToMainPageCommand { get; }

    public ICommand GoToMaterialsPageCommand { get; }

    public ICommand GoToFilmingPageCommand { get; }

    private bool CanGoToProjectPageCommandExecute(object p)
    {
        return true;
    }

    private void GoToProjectPageCommandExecute(object p)
    {
        CurrentPage = new ExplorerPage(ExplorerPreset.Projects);
    }
    
    private bool CanGoToMainPageCommandExecute(object p)
    {
        return true;
    }

    private void GoToMainPageCommandExecute(object p)
    {
        CurrentPage = new ExplorerPage();
    }

    private bool CanGoToMaterialsPageCommandExecute(object p)
    {
        return true;
    }

    private void GoToMaterialsPageCommandExecute(object p)
    {
        CurrentPage = new ExplorerPage(ExplorerPreset.Materials);
    }

    private bool CanGoToFilmingPageCommandExecute(object p)
    {
        return true;
    }

    private void GoToFilmingPageCommandExecute(object p)
    {
        CurrentPage = new ExplorerPage(ExplorerPreset.Filmings);
    }


    #region CurrentPage - Текущая страница

    private Page _currentPage = new ExplorerPage();

    public Page CurrentPage
    {
        get => _currentPage;
        set => Set(ref _currentPage, value);
    }

    #endregion
}