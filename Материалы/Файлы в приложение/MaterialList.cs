using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NfModels.Infrastructure.Commands;
using NfModels.ViewModels.NewsFactoryObjects;
using NfModels.ViewModels.Windows;
using NfModels.Views.Dialogs;
using NfModels.Views.Dialogs.PickDialogs;

namespace NfModels.ViewModels.Base;

/// <summary>
/// Класс представляющий коллекцию материалов проекта
/// </summary>
public class MaterialList : ObservableCollection<MaterialViewModel>
{
    public MaterialList()
    {
        AddMaterialCommand = new LambdaCommand(OnAddMaterialCommandExecute, CanAddMaterialCommandExecute);
        RemoveMaterialCommand = new LambdaCommand(OnRemoveMaterialCommandExecute, CanRemoveMaterialCommandExecute);
    }

    #region AddMaterialCommand

    /// <summary>
    /// Команда добавления материала в коллекцию
    /// </summary>
    public ICommand AddMaterialCommand { get; }

    private bool CanAddMaterialCommandExecute(object p)
    {
        return true;
    }

    private void OnAddMaterialCommandExecute(object p)
    {
        var result = PickObjectDialog.GetObject(ExplorerPreset.Materials);
        if (result is not MaterialViewModel material) return;
        if (Contains(material))
        {
            MessageBox.Show("Этот материал уже есть в списке", "Глупо с твоей стороны", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }
        Add(material);
    }

    #endregion

    #region RemoveMaterialCommand

    /// <summary>
    /// Команда удаления материала в коллекцию
    /// </summary>
    public ICommand RemoveMaterialCommand { get; }

    private bool CanRemoveMaterialCommandExecute(object p)
    {
        return p is MaterialViewModel materialViewModel && Contains(materialViewModel);
    }

    private void OnRemoveMaterialCommandExecute(object p)
    {
        Remove((MaterialViewModel)p);
    }

    #endregion
}