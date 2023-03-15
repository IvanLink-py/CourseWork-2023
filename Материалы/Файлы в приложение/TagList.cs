using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NfModels.Infrastructure.Commands;
using NfModels.ViewModels.NewsFactoryObjects;
using NfModels.Views.Dialogs;
using NfModels.Views.Dialogs.EditDialogs;
using NfModels.Views.Dialogs.PickDialogs;

namespace NfModels.ViewModels.Base;

/// <summary>
/// Класс, представляющий список тегов объекта фабрики
/// </summary>
public class TagList : ObservableCollection<TagViewModel>
{
    public TagList()
    {
        AddTagCommand = new LambdaCommand(OnAddTagCommandExecute, CanAddTagCommandExecute);
        RemoveTagCommand = new LambdaCommand(OnRemoveTagCommandExecute, CanRemoveTagCommandExecute);
        EditTagCommand = new LambdaCommand(OnEditTagCommandExecute, CanEditTagCommandExecute);
    }

    /// <summary>
    /// Получение списка всех тегов и тегов-родителей для поиска
    /// </summary>
    /// <returns>Список всех тегов и тегов-родителей для поиска</returns>
    public IEnumerable<TagViewModel> GetAllTagsToSearch()
    {
        var prev = 0;
        var tags = new List<TagViewModel>(Items);

        while (prev != tags.Count)
        {
            prev = tags.Count;
            foreach (var parentTag in tags.Select(tag => tag.ParentTag)
                         .Where(parentTag => parentTag is not null && !tags.Contains(parentTag)))
                tags.Add(parentTag!);
        }

        return tags;
    }

    #region AddTagCommand

    /// <summary>
    /// Команда добавления тега 
    /// </summary>
    public ICommand AddTagCommand { get; }

    private bool CanAddTagCommandExecute(object p)
    {
        return true;
    }

    private void OnAddTagCommandExecute(object? p)
    {
        p ??= PickTagDialog.PickTag();
        if (p is TagViewModel tag) AddTag(tag);
    }

    /// <summary>
    /// Метод добавления тега
    /// </summary>
    /// <param name="tag">Тег</param>
    private void AddTag(TagViewModel tag)
    {
        if (!Contains(tag))
        {
            if ((from oldTag in Items.ToList() where oldTag.Parents().Contains(tag) select MessageBox.Show(
                    $"Данный тег обобщает, уже присвоенный тег \"{oldTag.Title}\", добавление этого тега ничего не изменит.",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error)).Any())
            {
                return;
            }
            
            foreach (var newTagParent in tag.Parents())
            {
                if (!Contains(newTagParent)) continue;
                var messageBoxResult = MessageBox.Show(
                    $"Данный тег уточняет, уже присвоенный тег \"{newTagParent.Title}\". Старый тег будет удалён.",
                    "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (messageBoxResult == MessageBoxResult.Cancel) return;
                Remove(newTagParent);
            }
            
            Add(tag);
        }
        else
        {
            MessageBox.Show("Данный тег уже присвоен данном объекту", "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }

    #endregion

    #region RemoveTagCommand

    /// <summary>
    /// Команда исключения тега
    /// </summary>
    public ICommand RemoveTagCommand { get; }

    private bool CanRemoveTagCommandExecute(object p)
    {
        return p is TagViewModel tag && Items.Contains(tag);
    }

    private void OnRemoveTagCommandExecute(object p)
    {
        Remove((TagViewModel)p);
    }

    #endregion

    #region EditTagCommand

    /// <summary>
    /// Команда изменения тега 
    /// </summary>
    public ICommand EditTagCommand { get; }

    private bool CanEditTagCommandExecute(object p)
    {
        return true;
    }

    private void OnEditTagCommandExecute(object p)
    {
        new EditTagDialog((TagViewModel)p).Show();
    }

    #endregion
}