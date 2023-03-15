using System;
using NfModels.ViewModels.Base;

namespace NfModels.ViewModels.NewsFactoryObjects;

/// <summary>
/// Модель представления съёмки
/// </summary>
public class FilmingViewModel : CrewedObjectViewModel
{
    private ActionStatus _status = ActionStatus.Planed;

    /// <summary>
    /// Статус съёмки
    /// </summary>
    public ActionStatus Status
    {
        get => _status;
        set => Set(ref _status, value);
    }

    public override string TypeName => "Съёмка";

    protected override void Reload(NewsFactoryObjectViewModel other)
    {
        base.Reload(other);
        var viewModel = (FilmingViewModel)other;

        Status = viewModel._status;
        MyTime = viewModel._myTime;
        Address = viewModel._address;
    }

    #region MyTime

    /// <summary>
    /// Дата и время съёмки
    /// </summary>
    public DateTime MyTime
    {
        get => _myTime;
        set => Set(ref _myTime, value);
    }

    private DateTime _myTime = DateTime.Today + TimeSpan.FromDays(1);

    #endregion

    #region Address

    /// <summary>
    /// Адрес съёмки
    /// </summary>
    public string? Address
    {
        get => _address;
        set => Set(ref _address, value);
    }

    private string? _address;

    #endregion
}