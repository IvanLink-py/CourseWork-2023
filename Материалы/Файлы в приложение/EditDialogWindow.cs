using System;
using System.ComponentModel;
using System.Windows;
using NfModels.ViewModels.Base;

namespace NfModels.Views.Dialogs.EditDialogs;

/// <summary>
/// Абстрактный класс окон редактирования объектов фабрики
/// </summary>
public abstract class EditDialogWindow : Window
{
    /// <summary>
    /// Не показывать диалог о не сохранённых изменениях
    /// </summary>
    private bool _isSilentClose;

    /// <summary>
    /// Обработка события закрытия окна
    /// </summary>
    /// <param name="e">Параметры события закрытия окна</param>
    /// <exception cref="ArgumentOutOfRangeException">Неизвестный результат диалога</exception>
    protected override void OnClosing(CancelEventArgs e)
    {
        if (!_isSilentClose && DataContext is NewsFactoryObjectViewModel { IsNotSaved: true } vm)
        {
            var result = MessageBox.Show("Имеются не сохранённые изменения. Сохранить?", "Подтвердите",
                MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
                case MessageBoxResult.Yes:
                    vm.Save.Execute(null);
                    break;
                case MessageBoxResult.No:
                    vm.CancelCommand.Execute(null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        base.OnClosing(e);
    }

    /// <summary>
    /// Закрытие без предупреждения не сохранённых изменений
    /// </summary>
    public void SilentClose()
    {
        _isSilentClose = true;
        Close();
    }
}