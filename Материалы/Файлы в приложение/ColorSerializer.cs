using System;
using System.Windows.Media;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace NfModels.Services.Serializers;

/// <summary>
/// Сериалайзер объекта System.Windows.Media.Color
/// </summary>
public class ColorSerializer : IBsonSerializer
{
    public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var reader = context.Reader;

        reader.ReadStartDocument();

        if (reader.State == BsonReaderState.Type)
            if (reader.ReadBsonType() == BsonType.EndOfDocument)
            {
                reader.ReadEndDocument();
                return Colors.Black;
            }

        reader.ReadStartArray();

        var r = reader.ReadInt32();
        var g = reader.ReadInt32();
        var b = reader.ReadInt32();
        var a = reader.ReadInt32();

        reader.ReadEndArray();
        reader.ReadEndDocument();

        return Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        var c = (Color)value;

        var writer = context.Writer;

        writer.WriteStartDocument();
        writer.WriteStartArray("values");

        writer.WriteInt32(c.R);
        writer.WriteInt32(c.G);
        writer.WriteInt32(c.B);
        writer.WriteInt32(c.A);

        writer.WriteEndArray();

        writer.WriteEndDocument();
    }

    public Type ValueType { get; set; } = typeof(Color);
}