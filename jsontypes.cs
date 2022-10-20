using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace tagfinder
{
    public partial class Jsontypes
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        //public Name Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("messages")]
        public Message[] Messages { get; set; }
    }

    public partial class Message
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public MessageType Type { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("date_unixtime")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DateUnixtime { get; set; }

        [JsonProperty("actor", NullValueHandling = NullValueHandling.Ignore)]
        public string? Actor { get; set; }
        //public Name? Actor { get; set; }

        [JsonProperty("actor_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ActorId { get; set; }
        //public Id? ActorId { get; set; }

        [JsonProperty("action", NullValueHandling = NullValueHandling.Ignore)]
        public string Action { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        //public Name? Title { get; set; }

        /*[JsonProperty("text")]
        public MessageTextUnion Text { get; set; }*/

        [JsonProperty("text")]
        public object Text { get; set; }

        [JsonProperty("text_entities")]
        public TextEntityElement[] TextEntities { get; set; }

        [JsonProperty("photo", NullValueHandling = NullValueHandling.Ignore)]
        public object? Photo { get; set; }
        //public File? Photo { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public string? From { get; set; }
        //public Name? From { get; set; }

        [JsonProperty("from_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? FromId { get; set; }
        //public Id? FromId { get; set; }

        [JsonProperty("file", NullValueHandling = NullValueHandling.Ignore)]
        public object? File { get; set; }

        [JsonProperty("thumbnail", NullValueHandling = NullValueHandling.Ignore)]
        public object? Thumbnail { get; set; }

        [JsonProperty("media_type", NullValueHandling = NullValueHandling.Ignore)]
        public MediaType? MediaType { get; set; }

        [JsonProperty("mime_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? MimeType { get; set; }
        //public MimeType? MimeType { get; set; }

        [JsonProperty("duration_seconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? DurationSeconds { get; set; }

        [JsonProperty("forwarded_from", NullValueHandling = NullValueHandling.Ignore)]
        public string ForwardedFrom { get; set; }

        [JsonProperty("edited", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Edited { get; set; }

        [JsonProperty("edited_unixtime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? EditedUnixtime { get; set; }

        [JsonProperty("reply_to_message_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReplyToMessageId { get; set; }

        [JsonProperty("message_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? MessageId { get; set; }
    }

    public partial class TextEntityElement
    {
        //[JsonProperty("")]
        public string whatever {get; set;}

        [JsonProperty("type")]
        public TextEntityType Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public enum Name { Гифкиのザウトラチャト };

    public enum Id { Channel1813100041 };

    public enum File { FileNotIncludedChangeDataExportingSettingsToDownload };

    public enum MediaType { Animation };

    public enum MimeType { ImageGif, VideoMp4 };

    public enum TextEntityType { Hashtag, Link, Plain, text_link };
    //public enum TextEntityType { Hashtag};

    public enum MessageType { Message, Service };

    public partial struct TextTextUnion
    {
        public string String;
        public TextEntityElement TextEntityElement;

        public static implicit operator TextTextUnion(string String) => new TextTextUnion { String = String };
        public static implicit operator TextTextUnion(TextEntityElement TextEntityElement) => new TextTextUnion { TextEntityElement = TextEntityElement };
    }

    public partial struct MessageTextUnion
    {
        public TextTextUnion[] AnythingArray;
        public string String;

        public static implicit operator MessageTextUnion(TextTextUnion[] AnythingArray) => new MessageTextUnion { AnythingArray = AnythingArray };
        public static implicit operator MessageTextUnion(string String) => new MessageTextUnion { String = String };
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                NameConverter.Singleton,
                IdConverter.Singleton,
                FileConverter.Singleton,
                MediaTypeConverter.Singleton,
                MimeTypeConverter.Singleton,
                MessageTextUnionConverter.Singleton,
                TextTextUnionConverter.Singleton,
                TextEntityTypeConverter.Singleton,
                MessageTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class NameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Name) || t == typeof(Name?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "гифкиのザウトラチャト")
            {
                return Name.Гифкиのザウトラチャト;
            }
            throw new Exception("Cannot unmarshal type Name");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Name)untypedValue;
            if (value == Name.Гифкиのザウトラチャト)
            {
                serializer.Serialize(writer, "гифкиのザウトラチャト");
                return;
            }
            throw new Exception("Cannot marshal type Name");
        }

        public static readonly NameConverter Singleton = new NameConverter();
    }

    internal class IdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Id) || t == typeof(Id?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "channel1813100041")
            {
                return Id.Channel1813100041;
            }
            throw new Exception("Cannot unmarshal type Id");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Id)untypedValue;
            if (value == Id.Channel1813100041)
            {
                serializer.Serialize(writer, "channel1813100041");
                return;
            }
            throw new Exception("Cannot marshal type Id");
        }

        public static readonly IdConverter Singleton = new IdConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class FileConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(File) || t == typeof(File?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "")
            {
                return null;
                //return File.FileNotIncludedChangeDataExportingSettingsToDownload;
            }
            throw new Exception("Cannot unmarshal type File");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (File)untypedValue;
            if (value == File.FileNotIncludedChangeDataExportingSettingsToDownload)
            {
                serializer.Serialize(writer, "");
                return;
            }
            throw new Exception("Cannot marshal type File");
        }

        public static readonly FileConverter Singleton = new FileConverter();
    }

    internal class MediaTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MediaType) || t == typeof(MediaType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "animation")
            {
                return MediaType.Animation;
            }
            throw new Exception("Cannot unmarshal type MediaType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MediaType)untypedValue;
            if (value == MediaType.Animation)
            {
                serializer.Serialize(writer, "animation");
                return;
            }
            throw new Exception("Cannot marshal type MediaType");
        }

        public static readonly MediaTypeConverter Singleton = new MediaTypeConverter();
    }

    internal class MimeTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MimeType) || t == typeof(MimeType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "image/gif":
                    return MimeType.ImageGif;
                case "video/mp4":
                    return MimeType.VideoMp4;
            }
            throw new Exception("Cannot unmarshal type MimeType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MimeType)untypedValue;
            switch (value)
            {
                case MimeType.ImageGif:
                    serializer.Serialize(writer, "image/gif");
                    return;
                case MimeType.VideoMp4:
                    serializer.Serialize(writer, "video/mp4");
                    return;
            }
            throw new Exception("Cannot marshal type MimeType");
        }

        public static readonly MimeTypeConverter Singleton = new MimeTypeConverter();
    }

    internal class MessageTextUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MessageTextUnion) || t == typeof(MessageTextUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new MessageTextUnion { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<TextTextUnion[]>(reader);
                    return new MessageTextUnion { AnythingArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type MessageTextUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (MessageTextUnion)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.AnythingArray != null)
            {
                serializer.Serialize(writer, value.AnythingArray);
                return;
            }
            throw new Exception("Cannot marshal type MessageTextUnion");
        }

        public static readonly MessageTextUnionConverter Singleton = new MessageTextUnionConverter();
    }

    internal class TextTextUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TextTextUnion) || t == typeof(TextTextUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new TextTextUnion { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<TextEntityElement>(reader);
                    return new TextTextUnion { TextEntityElement = objectValue };
            }
            throw new Exception("Cannot unmarshal type TextTextUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (TextTextUnion)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.TextEntityElement != null)
            {
                serializer.Serialize(writer, value.TextEntityElement);
                return;
            }
            throw new Exception("Cannot marshal type TextTextUnion");
        }

        public static readonly TextTextUnionConverter Singleton = new TextTextUnionConverter();
    }

    internal class TextEntityTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TextEntityType) || t == typeof(TextEntityType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "hashtag":
                    return TextEntityType.Hashtag;
                case "link":
                    return TextEntityType.Link;
                    
                case "plain":
                    return TextEntityType.Plain;
            }
            throw new Exception("Cannot unmarshal type TextEntityType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TextEntityType)untypedValue;
            switch (value)
            {
                case TextEntityType.Hashtag:
                    serializer.Serialize(writer, "hashtag");
                    return;
                case TextEntityType.Link:
                    serializer.Serialize(writer, "link");
                    return;
                case TextEntityType.Plain:
                    serializer.Serialize(writer, "plain");
                    return;
            }
            throw new Exception("Cannot marshal type TextEntityType");
        }

        public static readonly TextEntityTypeConverter Singleton = new TextEntityTypeConverter();
    }

    internal class MessageTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MessageType) || t == typeof(MessageType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "message":
                    return MessageType.Message;
                case "service":
                    return MessageType.Service;
            }
            throw new Exception("Cannot unmarshal type MessageType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MessageType)untypedValue;
            switch (value)
            {
                case MessageType.Message:
                    serializer.Serialize(writer, "message");
                    return;
                case MessageType.Service:
                    serializer.Serialize(writer, "service");
                    return;
            }
            throw new Exception("Cannot marshal type MessageType");
        }
        public static readonly MessageTypeConverter Singleton = new MessageTypeConverter();
        /*internal class Jsontypes
        {
            public string name { get; set; }
            public string type { get; set; }
            public int id { get; set; }
            public messagesArray[] messages { get; set; }
        }

        internal class messagesArray
        {
            public int id { get; set; }
            public DateTime date { get; set; }
            public int date_unixtime { get; set; }
            public string actor { get; set; }
            public string actor_id { get; set; }
            public string action { get; set; }
            public string title { get; set; }
            public string? text { get; set; }
            public string[]? text_entities { get; set; }
        }*/
    }
}
