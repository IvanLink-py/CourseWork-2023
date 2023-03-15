using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using NfModels.Infrastructure.Commands;
using NfModels.Services;
using NfModels.ViewModels.Base;

namespace NfModels.ViewModels.NewsFactoryObjects;

/// <summary>
/// Модель представления проекта
/// </summary>
public class ProjectViewModel : CrewedObjectViewModel
{
    public ProjectViewModel()
    {
        ChangePathCommand = new LambdaCommand(OnChangePathCommandExecute, CanChangePathCommandExecute);
    }

    public override string TypeName => "Проект";

    protected override void Reload(NewsFactoryObjectViewModel other)
    {
        base.Reload(other);
        var viewModel = (ProjectViewModel)other;

        Status = viewModel._status;
        Path = viewModel._path;
        DeadLine = viewModel._deadLine;
        MyType = viewModel._MyType;
        Materials = viewModel._materials;
    }

    #region Path

    private string _path = string.Empty;

    /// <summary>Путь к проекту</summary>
    public string Path
    {
        get => _path;
        set
        {
            if (Directory.Exists(value)) CreationTime = new DirectoryInfo(value).CreationTime;
            if (!string.IsNullOrWhiteSpace(value) &&
                (string.IsNullOrWhiteSpace(Title) || Title == System.IO.Path.GetFileName(_path)) &&
                System.IO.Path.GetFileName(value) is { } dn)
                Title = dn;
            Set(ref _path, value);
        }
    }

    #endregion

    #region MyType

    /// <summary>
    /// Тип проекта
    /// </summary>
    public ProjectType MyType
    {
        get => _MyType;
        set => Set(ref _MyType, value);
    }

    private ProjectType _MyType;

    #endregion

    #region DeadLine

    /// <summary>
    /// Срок сдачи проекта
    /// </summary>
    public DateTime DeadLine
    {
        get => _deadLine;
        set => Set(ref _deadLine, value);
    }

    private DateTime _deadLine = DateTime.Today + TimeSpan.FromHours(19);

    #endregion

    #region Status

    /// <summary>
    /// Статус проекта
    /// </summary>
    public ActionStatus Status
    {
        get => _status;
        set => Set(ref _status, value);
    }

    private ActionStatus _status = ActionStatus.Planed;

    #endregion

    #region ChangePathCommand

    /// <summary>
    /// Команда изменения пути проекта
    /// </summary>
    public ICommand ChangePathCommand { get; }

    private bool CanChangePathCommandExecute(object p)
    {
        return true;
    }

    private void OnChangePathCommandExecute(object p)
    {
        if (FileDialogs.GetDirectory(Path, "Путь к проекту") is { } pa) Path = pa;
    }

    #endregion
    
    #region Materials
    /// <summary>
    /// Список материалов проекта
    /// </summary>
    public MaterialList Materials
    {
        get => _materials;
        set
        {
            Set(ref _materials, value);
            _materials.CollectionChanged += (_, _) => { IsNotSaved = true; };
        }
    }

    private MaterialList _materials = new();

    #endregion
}

/// <summary>
/// Тип проекта
/// </summary>
public enum ProjectType
{
    [Description("Эфир")] Efir,
    [Description("Сюжет")] Report,
    [Description("Реклама")] Advertising,
    [Description("Фильм")] Film,
    [Description("Другое")] Other
}