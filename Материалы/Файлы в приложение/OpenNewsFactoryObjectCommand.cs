using NfModels.Infrastructure.Commands.Base;
using NfModels.ViewModels.Base;
using NfModels.ViewModels.NewsFactoryObjects;
using NfModels.Views.Dialogs;
using NfModels.Views.Dialogs.EditDialogs;

namespace NfModels.Infrastructure.Commands;

/// <summary>
/// Команда открытия окна объекта фабрики
/// </summary>
public class OpenNewsFactoryObjectCommand : Command
{
    public override bool CanExecute(object parameter)
    {
        return true;
    }

    public override void Execute(object parameter)
    {
        OpenObject(parameter as NewsFactoryObjectViewModel);
    }

    /// <summary>
    /// Метод, открывающий окно редактирования объекта 
    /// </summary>
    /// <param name="parameter">Объект для открытия</param>
    public static void OpenObject(NewsFactoryObjectViewModel? parameter)
    {
        if (parameter is null) return;
        parameter.IsNotSaved = false;
        switch (parameter)
        {
            case ProjectViewModel project:
                new EditProjectDialog(project).Show();
                break;
            case PersonViewModel person:
                new EditPersonDialog(person).Show();
                break;
            case TagViewModel tag:
                new EditTagDialog(tag).Show();
                break;
            case FilmingViewModel filming:
                new EditFilmingDialog(filming).Show();
                break;
            case MaterialViewModel material:
                new EditMaterialDialog(material).Show();
                break;
        }
    }
}