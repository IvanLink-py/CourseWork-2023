namespace NfModels.ViewModels.Base;


/// <summary>
/// Класс, представляющий абстрактный объект фабрики, с информацией о работающих над ним работниках
/// </summary>
public abstract class CrewedObjectViewModel : TaggedObjectViewModel
{
    protected override void Reload(NewsFactoryObjectViewModel other)
    {
        base.Reload(other);
        var crewedOther = (CrewedObjectViewModel)other;
        MyCrew = crewedOther._myCrew;
    }

    #region MyCrew

    /// <summary>
    /// Коллекция, 
    /// </summary>
    public CrewList MyCrew
    {
        get => _myCrew;
        set
        {
            Set(ref _myCrew, value);
            MyCrew.CollectionChanged += (_, _) => { IsNotSaved = true; };
        }
    }

    private CrewList _myCrew = new();

    #endregion
}