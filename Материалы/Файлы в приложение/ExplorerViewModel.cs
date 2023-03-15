using System;
using System.Collections.Generic;
using NfModels.Services;
using NfModels.ViewModels.Base;

namespace NfModels.ViewModels.Windows;

/// <summary>
/// Модель представления браузера 
/// </summary>
public class ExplorerViewModel : ViewModel
{
    private string _filter;

    /// <summary>
    /// Текущий запрос
    /// </summary>
    public string Filter
    {
        get => _filter;
        set
        {
            Set(ref _filter, value);
            OnPropertyChanged(nameof(Objects));
        }
    }

    #region HiddenFilter

    /// <summary>
    /// Скрытый запрос
    /// </summary>
    public string HiddenFilter
    {
        get => _hiddenFilter;
        set
        {
            Set(ref _hiddenFilter, value);
            OnPropertyChanged(nameof(Objects));
        }
    }

    private string _hiddenFilter = "";

    #endregion

    /// <summary>
    /// Полный фильтр
    /// </summary>
    public string FullFilter => _hiddenFilter + " " + _filter;

    /// <summary>
    /// Объекты для отображения
    /// </summary>
    public IEnumerable<NewsFactoryObjectViewModel> Objects => DBProvider.LoadObjects(FilterParser.Parse(FullFilter));

    /// <summary>
    /// Загрузка предустановленной запроса
    /// </summary>
    /// <param name="preset">Предустановленный запрос</param>
    /// <param name="hidden">Загрузить в скрытом режиме</param>
    /// <exception cref="ArgumentOutOfRangeException">Неизвестная предустановка</exception>
    public void LoadPreset(ExplorerPreset preset, bool hidden = false)
    {
        if (!hidden)
            Filter = preset switch
            {
                ExplorerPreset.Projects => "Тип:Проект",
                ExplorerPreset.Materials => "Тип:Материал",
                ExplorerPreset.Filmings => "Тип:Съёмка",
                _ => throw new ArgumentOutOfRangeException(nameof(preset), preset, null)
            };
        else
            HiddenFilter = preset switch
            {
                ExplorerPreset.Projects => "Тип:Проект",
                ExplorerPreset.Materials => "Тип:Материал",
                ExplorerPreset.Filmings => "Тип:Съёмка",
                _ => throw new ArgumentOutOfRangeException(nameof(preset), preset, null)
            };
    }
}

/// <summary>
/// Предустановленные запросы
/// </summary>
public enum ExplorerPreset
{
    Projects,
    Materials,
    Filmings
}