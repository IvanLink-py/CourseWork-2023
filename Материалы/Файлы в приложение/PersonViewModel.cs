using System;
using MongoDB.Bson.Serialization.Attributes;
using NfModels.ViewModels.Base;

namespace NfModels.ViewModels.NewsFactoryObjects;

/// <summary>
/// Модель представления работника
/// </summary>
public class PersonViewModel : NewsFactoryObjectViewModel
{

    public PersonViewModel()
    {
        VoiceReadTimeModel = new ReadTimeModel();
        DigestReadTimeModel = new ReadTimeModel();
        DictorReadTimeModel = new ReadTimeModel();
    }
    
    /// <summary>
    /// Полное имя работника
    /// </summary>
    [BsonIgnore] public string FullName => $"{FirstName} {LastName}";

    #region FirstName

    /// <summary>
    /// Имя работника
    /// </summary>
    public string FirstName
    {
        get => _firstName;
        set => Set(ref _firstName, value);
    }

    private string _firstName = string.Empty;

    #endregion

    #region LastName

    /// <summary>
    /// Фамилия работника
    /// </summary>
    public string LastName
    {
        get => _lastName;
        set => Set(ref _lastName, value);
    }

    private string _lastName = string.Empty;

    #endregion

    #region Post

    /// <summary>
    /// Должность работника
    /// </summary>
    public string Post
    {
        get => _post;
        set => Set(ref _post, value);
    }

    private string _post = string.Empty;

    #endregion

    #region VoiceReadTimeModel

    /// <summary>
    /// Модель предсказания длительности стандартной записи
    /// </summary>
    public ReadTimeModel VoiceReadTimeModel
    {
        get => _voiceReadTimeModel;
        set
        {
            value.Mode = ReadTimeModelMode.Voice;
            value.User = FullName;
            Set(ref _voiceReadTimeModel, value);
        }
    }

    private ReadTimeModel _voiceReadTimeModel = ReadTimeModel.DefaultReadTimeModel;

    #endregion

    #region DigestReadTimeModel

    /// <summary>
    /// Модель предсказания длительности записи дайджеста
    /// </summary>
    public ReadTimeModel DigestReadTimeModel
    {
        get => _digestReadTimeModel;
        set
        {
            value.Mode = ReadTimeModelMode.Digest;
            value.User = FullName;
            Set(ref _digestReadTimeModel, value);
        } 
    }

    private ReadTimeModel _digestReadTimeModel;

    #endregion

    #region DictorReadTimeModel

    /// <summary>
    /// Модель предсказания длительности записи диктора
    /// </summary>
    public ReadTimeModel DictorReadTimeModel
    {
        get => _dictorReadTimeModel;
        set
        {
            value.Mode = ReadTimeModelMode.Dictor;
            value.User = FullName;
            Set(ref _dictorReadTimeModel, value);
        }
    }

    private ReadTimeModel _dictorReadTimeModel = ReadTimeModel.DefaultReadTimeModel;

    #endregion
}