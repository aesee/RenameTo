/*
 * Author: Sarapulov
 *
 */
using System;
using System.Collections.Generic;
namespace RenameTo
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("RenamedTo. Utility for rename groups of files.");
			Console.WriteLine("----------------------------------------------");
	
			Segments[] list = Segments.ConfigIni();
			
			foreach (var segment in list)
			{
                if (segment != null)
                {
                    Console.WriteLine("Searching {0}.", segment.Modul);
                    Console.WriteLine("The old name: {0}, new name: {1}", segment.Name, segment.NewName);
                    Segments.Rename(segment);
                }
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}