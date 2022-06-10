using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyLib.Json.Serializer
{
    public static class SerializerService
    {

        private static JsonSerializerOptions _serializerOptions { get; set; }

        public static JsonSerializerOptions SerializerOptions
        {
            get
            {
                if (_serializerOptions == null)
                {
                    _serializerOptions = new JsonSerializerOptions()
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        PropertyNameCaseInsensitive = true,
                        ReadCommentHandling = JsonCommentHandling.Skip,
                        WriteIndented = true,
                    };
                }
                return _serializerOptions;
            }
        }

        /// <summary>
        /// Serializa un modelo en un fichero de configuración json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonFilePath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task SerializeIntoJson<T>(string jsonFilePath, T model)
        {
            string json = JsonSerializer.Serialize(model, SerializerOptions);
            await File.WriteAllTextAsync(jsonFilePath, json);
        }
        /// <summary>
        /// Deserializa el contenido de un fichero json en un modelo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonFIlePath"></param>
        /// <returns></returns>
        public static async Task<T> DeserializeFromJson<T>(string jsonFIlePath)
        {
            string json = await File.ReadAllTextAsync(jsonFIlePath);
            T model = JsonSerializer.Deserialize<T>(json, SerializerOptions);
            return model;
        }
    }
}
