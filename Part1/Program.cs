namespace Part1;
class Program
{
 static void Main()
 {
  var products=new List<Product>{new(101,"Ноутбук","Lenovo",28500,3),new(102,"Миша","Logitech",750,10),new(103,"Клавіатура","HyperX",2200,5),new(104,"Монітор","Samsung",8900,4),new(105,"Навушники","Sony",3100,6)};
  Console.WriteLine("Початковий масив товарів:");products.ForEach(Console.WriteLine);
  var dataDirectory=Path.Combine(AppContext.BaseDirectory,"data");
  Directory.CreateDirectory(dataDirectory);
  string DataPath(string file)=>Path.Combine(dataDirectory,file);
  Test("binary",new BinaryProductSerializer(),products,DataPath);Test("xml",new XmlProductSerializer(),products,DataPath);Test("json",new JsonProductSerializer(),products,DataPath);Test("custom",new CustomProductSerializer(),products,DataPath);
  var collection=new List<Product>(products);new JsonProductSerializer().Save(collection,DataPath("products-collection.json"));var restored=new JsonProductSerializer().Load(DataPath("products-collection.json"));
  Console.WriteLine($"\nКолекція List<T>: збережено та відновлено {restored.Count} об'єктів.");
  Console.WriteLine("\nЗбільшення вартості першого товару на 10%:");products[0].IncreasePrice(10);Console.WriteLine(products[0]);
 }
 static void Test(string name,IDataSerializer<List<Product>> serializer,List<Product> products,Func<string,string> dataPath){var path=dataPath($"products.{name}");serializer.Save(products,path);var restored=serializer.Load(path);Console.WriteLine($"\n{name}: відновлено {restored.Count} об'єктів; перший: {restored[0]}");}
}
