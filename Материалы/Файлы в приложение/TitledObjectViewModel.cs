namespace NfModels.ViewModels.Base;

/// <summary>
/// Абстрактный класс объекта фабрики с названием и описанием
/// </summary>
public abstract class TitledObjectViewModel : NewsFactoryObjectViewModel
{
    private string _title = string.Empty;

    /// <summary>Заголовок объекта</summary>
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }

    protected override void Reload(NewsFactoryObjectViewModel other)
    {
        base.Reload(other);
        var viewModel = (TitledObjectViewModel)other;

        Title = viewModel._title;
        Description = viewModel._Description;
    }

    #region Description

    private string _Description = string.Empty;

    /// <summary>Описание объекта</summary>
    public string Description
    {
        get => _Description;
        set => Set(ref _Description, value);
    }

    #endregion
}