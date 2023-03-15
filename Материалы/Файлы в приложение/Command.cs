using System;
using System.Windows.Input;

namespace NfModels.Infrastructure.Commands.Base;

/// <summary>
/// Абстрактный класс команды, реализующий логику интерфейса ICommand
/// </summary>
public abstract class Command : ICommand
{
    /// <summary>
    /// Событие, вызываемое при изменения статуса возможности исполнения команды
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Возможность исполнения команды
    /// </summary>
    /// <param name="parameter">Параметр команды</param>
    /// <returns></returns>
    public abstract bool CanExecute(object parameter);

    /// <summary>
    /// Исполнение команды
    /// </summary>
    /// <param name="parameter">Параметр команды</param>
    public abstract void Execute(object parameter);
}