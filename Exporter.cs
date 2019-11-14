using System;
using System.IO;

namespace ScrapingFromOurOwn
{
	public class Exporter
	{
		public Exporter()
		{
			
		}
		
		public static String exeDirectory() {
			return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
		}
		
		public static String addColumn(String input, String label, String value, String value2 = "") {
			if(String.IsNullOrEmpty(input.Trim()) == false) {
				input += Environment.NewLine;
			}
			input += label;
			input += "\t\t" + value;
			
			if(String.IsNullOrEmpty(value2) == false) {
			   	input += "\t\t" + value2;
			}
			
			return input;
		}
		
		// optional two-parameter variant
		public static String addColumn(String label, String value) {
			return addColumn("", label, value);
		}
		
		public static String addColumn(String label, String value, String value2) {
			return addColumn("", label, value, value2);
		}
		
		
		public static void writeFile(String path, String content) {
			
			try {
				StreamWriter writer = new StreamWriter(path);
				writer.Write(content);
				writer.Close();
			} catch (Exception e) {
				Console.WriteLine("Execution failed. Invalid file name?\n\n{0}", e.ToString());
			}
			
		}
	}
}
