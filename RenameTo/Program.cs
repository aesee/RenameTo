/*
 * Author: Sarapulov
 *
 */
using System;
using System.IO;

namespace RenameTo
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Программа переименования штатных сегментов.");
			Console.WriteLine("-------------------------------------------");
	
			Segments[] list = {
				new Segments("МХКИ", "bt_tam_s", "bt_tamh"), 
				new Segments("СМ", "bt_tas_s", "bt_tas"),
				new Segments("программы прошивки", "bt_texn_s", "bt_texn")};
			
			foreach (var segment in list)
			{
				Console.WriteLine("Поиск сегментов {0}.", segment.Modul);
				Segments.Rename(segment);
			}
			
			Console.Write("Нажмите любую кнопку для завершения . . . ");
			Console.ReadKey(true);
		}
		
		public class Segments
		{
			private string modul;
			private string name;
			private string newName;
			
			public string Modul{ get {return modul;} set {modul = value;}}
			public string Name{ get {return name;} set {name = value;}}
			public string NewName{ get {return newName;} set {newName = value;}}
			
			public Segments(string modul, string name, string newName)
			{
				Modul = modul;
				Name = name;
				NewName = newName;
			}
			
			public static void Rename(Segments file)
			{
				try
				{
					string pattern = file.Name + ".*";
					string[] dirs = Directory.GetFiles(".", pattern, SearchOption.AllDirectories);
					
					int length = dirs.Length;
					if (file.Modul != "СМ")	length--;
					
					Console.WriteLine("Найдено штатных файлов прошивки {0}: {1}.", file.Modul, length);
					foreach (string dir in dirs)
					{
						if (dir.Contains(file.Name) &&
						    !dir.Contains("description") &&
						    !dir.Contains("report"))
						{
							var newDir = Segments.DirFix(file, dir);
							File.Move(dir, newDir);
							File.Delete(dir);
						}
					}
					//Console.WriteLine("Сегменты переименованы. \n");
					Console.WriteLine();
				}
				catch (Exception e)
				{
					Console.WriteLine("Ошибка: {0}", e.ToString());
				}
			}
			
			public static string DirFix(Segments file, string dir)
			{
				string[] path = dir.Split('\\');
				path[path.Length - 1] = path[path.Length - 1].Replace(file.Name, file.NewName);
				return String.Join("\\", path);
			}
			
		}
	}
}