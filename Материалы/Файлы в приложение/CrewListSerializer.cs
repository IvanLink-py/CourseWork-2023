using System;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using NfModels.ViewModels.Base;

namespace NfModels.Services.Serializers;

/// <summary>
/// Сериалайзер списка работников объекта фабрики
/// </summary>
public class CrewListSerializer : IBsonSerializer<CrewList>, IBsonArraySerializer
{
    public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var result = new CrewList();
        var reader = context.Reader;

        reader.ReadStartArray();

        while (true)
        {
            reader.ReadBsonType();
            if (reader.State == BsonReaderState.EndOfArray) break;

            reader.ReadStartDocument();

            var crew = new Crew();

            var person = PersonManager.LoadPerson(reader.ReadObjectId("Person"));
            
            if (person is null)
            {
                reader.ReadInt32("WorkType");
                reader.ReadEndDocument();
                continue;
            }

            crew.Person = person;
            crew.WorkType = (Work)reader.ReadInt32("WorkType");
            reader.ReadEndDocument();
            
            result.Add(crew);
        }

        reader.ReadEndArray();

        return result;
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, CrewList value)
    {
        Serialize(context, args, (object)value);
    }

    CrewList IBsonSerializer<CrewList>.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return (CrewList)Deserialize(context, args);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        var l = (CrewList)value;

        var writer = context.Writer;

        writer.WriteStartArray();

        foreach (var crew in l)
        {
            writer.WriteStartDocument();
            writer.WriteObjectId("Person", crew.Person.Id);
            writer.WriteInt32("WorkType", (int)crew.WorkType);
            writer.WriteEndDocument();
        }

        writer.WriteEndArray();
    }

    public Type ValueType { get; } = typeof(CrewList);

    public bool TryGetItemSerializationInfo(out BsonSerializationInfo serializationInfo)
    {
        serializationInfo = new BsonSerializationInfo("MyCrew", new CrewListSerializer(), ValueType);
        return true;
    }
}