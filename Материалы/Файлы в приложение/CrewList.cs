using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NfModels.Infrastructure.Commands;
using NfModels.ViewModels.NewsFactoryObjects;
using NfModels.Views.Dialogs;
using NfModels.Views.Dialogs.EditDialogs;
using NfModels.Views.Dialogs.PickDialogs;

namespace NfModels.ViewModels.Base;

/// <summary>
/// Класс, представляющий коллекцию работников и выполняемых ими обязанностей  
/// </summary>
public class CrewList : ObservableCollection<Crew>
{
    public CrewList()
    {
        RemoveCrewCommand = new LambdaCommand(OnRemoveCrewCommandExecute, CanRemoveCrewCommandExecute);
        AddCrewCommand = new LambdaCommand(OnAddCrewCommandExecute, CanAddCrewCommandExecute);
        EditCrewCommand = new LambdaCommand(OnEditCrewCommandExecute, CanEditCrewCommandExecute);
    }

    #region AddCrewCommand

    /// <summary>
    /// Команда добавления нового работника с обязанностью в коллекцию
    /// </summary>
    public ICommand AddCrewCommand { get; }

    private bool CanAddCrewCommandExecute(object? p)
    {
        return true;
    }

    private void OnAddCrewCommandExecute(object? p)
    {
        p ??= PickPersonDialog.PickPerson();
        if (p is PersonViewModel person) Add(new Crew {Person = person, WorkType = Work.Other});
    }

    #endregion
    

    #region RemoveCrewCommand

    /// <summary>
    /// Команда удаления работника с обязанностью из коллекции
    /// </summary>
    public ICommand RemoveCrewCommand { get; }

    private bool CanRemoveCrewCommandExecute(object p)
    {
        return p is Crew;
    }

    private void OnRemoveCrewCommandExecute(object p)
    {
        Remove((Crew)p);
    }

    #endregion
    
    #region EditCrewCommand

    /// <summary>
    /// Команда изменения работника с обязанностью в коллекции
    /// </summary>
    public ICommand EditCrewCommand { get; }

    private bool CanEditCrewCommandExecute(object p)
    {
        return true;
    }

    private void OnEditCrewCommandExecute(object p)
    {
        new EditPersonDialog(((Crew)p).Person).Show();
    }

    #endregion
}

/// <summary>
/// Класс, представляющий информацию о работнике и выполняемой им обязанности
/// </summary>
public class Crew : ViewModel
{
    #region Persone

    public PersonViewModel Person
    {
        get => _person;
        set => Set(ref _person, value);
    }

    private PersonViewModel _person;

    #endregion

    #region WorkType

    public Work WorkType
    {
        get => _workType;
        set => Set(ref _workType, value);
    }

    private Work _workType;

    #endregion
}

/// <summary>
/// Перечисление возможных типов обязанностей у работника
/// </summary>
public enum Work
{
    [Description("Монтажёр")] VideoEngineer,
    [Description("Редактор")] Editor,
    [Description("Оператор")] Operator,
    [Description("Водитель")] Driver,
    [Description("Корреспондент")] Reporter,
    [Description("Другое")] Other
}