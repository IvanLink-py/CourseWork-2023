using System;
using FuzzySharp;
using System.Collections.ObjectModel;
using System.Linq;
using MongoDB.Bson;
using NfModels.ViewModels.NewsFactoryObjects;

namespace NfModels.Services;

/// <summary>
/// Менеджер работников
/// </summary>
public class PersonManager
{
    /// <summary>
    /// Все работники
    /// </summary>
    public static ObservableCollection<PersonViewModel> AllPersons { get; private set; } = new(DBProvider.LoadPersons());
    
    /// <summary>
    /// Обновление коллекции работников
    /// </summary>
    public static void Refresh()
    {
        AllPersons = new ObservableCollection<PersonViewModel>(DBProvider.LoadPersons().AsEnumerable().OrderBy(t => t.FullName));
    }

    /// <summary>
    /// Загрузка работника по его ID
    /// </summary>
    /// <param name="id">ID работника</param>
    /// <returns>Объект работника</returns>
    public static PersonViewModel? LoadPerson(ObjectId? id)
    {
        return id is null ? null : AllPersons.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Нечёткий поиск работника
    /// </summary>
    /// <param name="search">Приблизительные данные работника</param>
    /// <returns>Наиболее подходящий работник</returns>
    public static PersonViewModel FuzzyFind(string search)
    {
        return AllPersons.OrderByDescending(p => Fuzz.PartialRatio(p.FullName, search)).First();
    }
}