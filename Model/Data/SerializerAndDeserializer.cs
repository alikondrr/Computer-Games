using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Model.Data
{
  public abstract class Serializer
  {
    public abstract void Serialize<T>(T obj, string filePath);
    public abstract T Deserialize<T>(string filePath);
  }


  public class JsonSerializer : Serializer
  {
    public override void Serialize<T>(T obj, string filePath)
    {
      var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
      string json = JsonConvert.SerializeObject(obj, settings);
      File.WriteAllText(filePath, json);
    }
    
    public override T Deserialize<T>(string filePath)
    {
      if (!File.Exists(filePath))
        return default(T);

      var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

      string json = File.ReadAllText(filePath);
      return JsonConvert.DeserializeObject<T>(json, settings);
    }
  }
  

  public class XmlSerializer : Serializer
  {
    public override void Serialize<T>(T obj, string filePath)
    {
      var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
      using (var writer = new StreamWriter(filePath))
      {
        serializer.Serialize(writer, obj);
      }
    }

    public override T Deserialize<T>(string filePath)
    {
      if (!File.Exists(filePath))
        return default(T);

      var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
      using (var reader = new StreamReader(filePath))
      {
        return (T)serializer.Deserialize(reader);
      }
    }
  }
}