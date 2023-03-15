namespace NfModels.ViewModels.Base;



public abstract class TaggedObjectViewModel : TitledObjectViewModel
{
    protected override void Reload(NewsFactoryObjectViewModel other)
    {
        base.Reload(other);
        var taggedOther = (TaggedObjectViewModel)other;
        Tags = taggedOther._tags;
    }

    #region Tags

    public TagList Tags
    {
        get => _tags;
        set
        {
            Set(ref _tags, value);
            Tags.CollectionChanged += (sender, args) => { IsNotSaved = true; };
        }
    }

    private TagList _tags = new();

    #endregion
}