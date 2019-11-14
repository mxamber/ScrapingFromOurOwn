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
		
		public static String addColumn(String input, String label, String value) {
			if(String.IsNullOrEmpty(input.Trim()) == false) {
				input += Environment.NewLine;
			}
			input += label;
			input += "\t\t";
			input += value;
			
			return input;
		}
		
		// optional two-parameter variant
		public static String addColumn(String label, String value) {
			return addColumn("", label, value);
		}
		
		
		public static void writeFile(String path, String content) {
			
			try {
				StreamWriter writer = new StreamWriter(path);
				writer.Write(content);
				writer.Close();
			} catch (Exception e) {
				Console.WriteLine("Execution failed: {0}", e.ToString());
			}
			
		}
	}
}
