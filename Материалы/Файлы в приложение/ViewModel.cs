using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NfModels.ViewModels.Base;

/// <summary>
/// Класс модели представления, реализующий интерфейс INotifyPropertyChanged
/// </summary>
[Serializable]
public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }

    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertytName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(PropertytName);
        return true;
    }
}