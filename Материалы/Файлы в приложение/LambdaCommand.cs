using System;
using NfModels.Infrastructure.Commands.Base;

namespace NfModels.Infrastructure.Commands;

/// <summary>
/// Лямбда-команда, принимающая в конструкторе функции для исполнения
/// </summary>
public class LambdaCommand : Command
{
    private readonly Func<object, bool> _CanExecute;
    private readonly Action<object> _Execute;

    public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
    {
        _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
        _CanExecute = CanExecute;
    }

    public override bool CanExecute(object parameter)
    {
        return _CanExecute?.Invoke(parameter) ?? true;
    }

    public override void Execute(object parameter)
    {
        _Execute(parameter);
    }
}