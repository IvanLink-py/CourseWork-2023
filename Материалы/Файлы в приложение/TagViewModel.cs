using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NfModels.Infrastructure.Commands;
using NfModels.Services;
using NfModels.ViewModels.Base;
using NfModels.Views.Dialogs;
using NfModels.Views.Dialogs.PickDialogs;

namespace NfModels.ViewModels.NewsFactoryObjects;

/// <summary>
/// Модель представления тега
/// </summary>
public class TagViewModel : TitledObjectViewModel
{
    private Color _myColor;

    public TagViewModel()
    {
        ChangeParentCommand = new LambdaCommand(OnChangeParentCommandExecute, CanChangeParentCommandExecute);
    }

    /// <summary>
    /// Основной цвет тега
    /// </summary>
    public Color MyColor
    {
        get => _myColor;
        set
        {
            Set(ref _myColor, value);
            OnPropertyChanged(nameof(TextColor));
            OnPropertyChanged(nameof(BgColor));
            OnPropertyChanged(nameof(BorderColor));
        }
    }

    /// <summary>
    /// Вторичный цвет тега
    /// </summary>
    public Color MySecondColor
    {
        get
        {
            var c = Color.Multiply(MyColor, 0.4f);
            c.A = byte.MaxValue;
            return c;
        }
    }

    /// <summary>
    /// Цвет текста тега
    /// </summary>
    public Color TextColor => MySecondColor.R + MySecondColor.G + MySecondColor.B > 350 ? Colors.Black : Colors.White;

    /// <summary>
    /// Цвет фона тега
    /// </summary>
    public Color BgColor => MySecondColor;

    /// <summary>
    /// Цвет границы тега
    /// </summary>
    public Color BorderColor => MyColor;

    #region ParentTag

    /// <summary>
    /// Родительский тег
    /// </summary>
    [BsonIgnore]
    public TagViewModel? ParentTag
    {
        get => TagManager.LoadTag(ParentTagId);
        set
        {
            if (value is not null) ParentTagId = value.Id;
        }
    }

    #endregion

    public override string TypeName => "Тег";

    protected override void Reload(NewsFactoryObjectViewModel other)
    {
        base.Reload(other);
        var viewModel = (TagViewModel)other;

        MyColor = viewModel.MyColor;
        ParentTagId = viewModel.ParentTagId;
    }

    /// <summary>
    /// Получение коллекции тегов родителей
    /// </summary>
    /// <param name="addSelf">Добавление в коллекцию себя</param>
    /// <returns>Коллекция тегов родителей</returns>
    public IEnumerable<TagViewModel> Parents(bool addSelf = false)
    {
        var tag = addSelf ? this : ParentTag;
        while (tag is not null)
        {
            yield return tag;
            tag = tag.ParentTag;
            if (tag == this) yield break;
        }
    }

    #region ParentTagId

    /// <summary>
    /// Идентификатор родительского тега
    /// </summary>
    public ObjectId? ParentTagId
    {
        get => _parentTagId;
        set
        {
            Set(ref _parentTagId, value);
            OnPropertyChanged(nameof(ParentTag));
        }
    }

    private ObjectId? _parentTagId;

    #endregion

    #region ChangeParentCommand

    /// <summary>
    /// Команда изменения родительского тега
    /// </summary>
    public ICommand ChangeParentCommand { get; }

    private bool CanChangeParentCommandExecute(object p)
    {
        return true;
    }

    private void OnChangeParentCommandExecute(object p)
    {
        if (PickTagDialog.PickTag() is not { } tag) return;
        ParentTag = tag;
    }

    #endregion
}