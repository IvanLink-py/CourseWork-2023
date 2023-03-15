using System;
using System.ComponentModel;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NfModels.Infrastructure.Commands;
using NfModels.Services;
using NfModels.Views.Dialogs;
using NfModels.Views.Dialogs.EditDialogs;

namespace NfModels.ViewModels.Base;

/// <summary>
/// Базовый класс объекта фабрики
/// </summary>
public abstract class NewsFactoryObjectViewModel : ViewModel
{
    protected NewsFactoryObjectViewModel()
    {
        Save = new LambdaCommand(OnSaveExecute, CanSaveExecute);
        CancelCommand = new LambdaCommand(OnCancelCommandExecute, CanCancelCommandExecute);
    }

    /// <summary>
    /// Идентификатор объекта фабрики
    /// </summary>
    [BsonElement("_id")] public ObjectId Id { get; set; }

    /// <summary>
    /// Словесное название типа объекта фабрики 
    /// </summary>
    public virtual string TypeName => GetType().ToString();

    
    protected override void OnPropertyChanged(string PropertyName = null)
    {
        base.OnPropertyChanged(PropertyName);
        if (PropertyName == nameof(IsNotSaved)) return;
        IsNotSaved = true;
    }

    /// <summary>
    /// Метод перезаписи всех полей данного объекта по примеру другого
    /// </summary>
    /// <param name="other">Другой объект фабрики</param>
    /// <exception cref="ArgumentException">Тип данного объекта не совпадает с типом аргумента</exception>
    protected virtual void Reload(NewsFactoryObjectViewModel other)
    {
        if (other.GetType() != GetType()) throw new ArgumentException();

        IsNotSaved = false;
    }

    #region CreationTime

    /// <summary>
    /// Дата и время создания объекта
    /// </summary>
    public DateTime CreationTime
    {
        get => _creationTime;
        set => Set(ref _creationTime, value);
    }

    private DateTime _creationTime = DateTime.Now;

    #endregion

    #region Save

    /// <summary>
    /// Команда сохранения объекта
    /// </summary>
    public ICommand Save { get; }

    private bool CanSaveExecute(object p)
    {
        return true;
    }

    private void OnSaveExecute(object p)
    {
        DBProvider.Save(this);
        if (p is EditDialogWindow window) window.SilentClose();
    }

    #endregion

    #region CancelCommand

    /// <summary>
    /// Команда отмены несохранённых изменений
    /// </summary>
    public ICommand CancelCommand { get; }

    private bool CanCancelCommandExecute(object? p)
    {
        return true;
    }

    private void OnCancelCommandExecute(object? p)
    {
        var savedData = DBProvider.LoadSavedData(this);
        if (savedData is not null)Reload(savedData);
        if (p is EditDialogWindow window) window.SilentClose();
    }

    #endregion

    #region IsSaved

    /// <summary>
    /// Сохранён ли данный объект
    /// </summary>
    [BsonIgnore]
    public bool IsNotSaved
    {
        get => _isNotSaved;
        set => Set(ref _isNotSaved, value);
    }

    private bool _isNotSaved;

    #endregion

    protected bool Equals(NewsFactoryObjectViewModel other)
    {
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((NewsFactoryObjectViewModel)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

public enum ActionStatus
{
    [Description("Запланировано")] Planed,
    [Description("В работе")] InWork,
    [Description("Готово")] Done,
    [Description("Отменено")] Aborted
}