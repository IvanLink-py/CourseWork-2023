using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MongoDB.Bson;
using NfModels.ViewModels.NewsFactoryObjects;

namespace NfModels.Services;

public static class TagManager
{
    /// <summary>
    /// Все теги
    /// </summary>
    public static ObservableCollection<TagViewModel> AllTags { get; private set; } =
        new(DBProvider.LoadTags().OrderBy(t => t.Title));

    /// <summary>
    /// Обновление коллекции тегов
    /// </summary>
    public static void Refresh()
    {
        AllTags = new ObservableCollection<TagViewModel>(DBProvider.LoadTags().OrderBy(t => t.Title));
    }
    
    /// <summary>
    /// Загрузка тега по его ID
    /// </summary>
    /// <param name="id">ID тега</param>
    /// <returns>Объект тега</returns>
    public static TagViewModel? LoadTag(ObjectId? id)
    {
        return id is null ? null : AllTags.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Загрузка тега по его названию
    /// </summary>
    /// <param name="id">Название тега</param>
    /// <returns>Объект тега</returns>
    public static TagViewModel? LoadTag(string? name)
    {
        return name is null
            ? null
            : AllTags.FirstOrDefault(t => string.Equals(t.Title, name, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    /// Получение списка всех дочерних тегов данному
    /// </summary>
    /// <param name="targetTag">Тег-родитель</param>
    /// <param name="addSelf">Добавлять тег-родитель в коллекцию</param>
    /// <returns>Список всех дочерних тегов</returns>
    public static IList<TagViewModel> GetAllChild(TagViewModel? targetTag, bool addSelf = true)
    {
        if (targetTag is null) return AllTags;
        var child = new List<TagViewModel> { targetTag };

        var stop = false;

        while (!stop)
        {
            stop = true;
            foreach (var tag in AllTags)
            {
                if (tag.ParentTag is null || child.Contains(tag) || !child.Contains(tag.ParentTag)) continue;
                child.Add(tag);
                stop = false;
            }
        }

        if (!addSelf) child.RemoveAt(0);
        return child;
    }
}