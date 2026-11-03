using System.Text;using System.Text.Json;using System.Xml.Serialization;
namespace DAL;
public interface IDataProvider<T>{void Save(List<T> data,string path);List<T> Load(string path);}
public class JsonProvider<T>:IDataProvider<T>{public void Save(List<T>d,string p)=>File.WriteAllText(p,JsonSerializer.Serialize(d,new JsonSerializerOptions{WriteIndented=true}));public List<T> Load(string p)=>JsonSerializer.Deserialize<List<T>>(File.ReadAllText(p))??new();}
public class XmlProvider<T>:IDataProvider<T>{public void Save(List<T>d,string p){using var f=File.Create(p);new XmlSerializer(typeof(List<T>)).Serialize(f,d);}public List<T> Load(string p){using var f=File.OpenRead(p);return (List<T>)(new XmlSerializer(typeof(List<T>)).Deserialize(f)??new List<T>());}}
// Generic binary provider uses a length-prefixed JSON payload stored as bytes.
public class BinaryProvider<T>:IDataProvider<T>{public void Save(List<T>d,string p){var bytes=Encoding.UTF8.GetBytes(JsonSerializer.Serialize(d));using var w=new BinaryWriter(File.Create(p));w.Write(bytes.Length);w.Write(bytes);}public List<T> Load(string p){using var r=new BinaryReader(File.OpenRead(p));int n=r.ReadInt32();if(n<0||n>100_000_000)throw new InvalidDataException();return JsonSerializer.Deserialize<List<T>>(r.ReadBytes(n))??new();}}
// Custom text provider is a distinct, human-readable JSON-lines format.
public class CustomProvider<T>:IDataProvider<T>{public void Save(List<T>d,string p){using var w=new StreamWriter(p);foreach(var x in d)w.WriteLine(JsonSerializer.Serialize(x));}public List<T> Load(string p)=>File.ReadLines(p).Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x=>JsonSerializer.Deserialize<T>(x)!).ToList();}
