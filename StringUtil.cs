using UnityEngine/*<TextAsset>*/;
using System/*<String, StringSplitOptions>*/;

namespace Finegamedesign.Utils
{
	/**
	 * Bridge between portable game and platform-specific filesystem, game engine or toolkit.
	 * Animation, sound, UI, or other view are in ViewUtils instead.
	 */
	public sealed class StringUtil
	{
		public static string lineDelimiter = "\n";

		public static int ParseInt(string digits)
		{
			return int.Parse(digits);
		}

		// Copied from:
		// http://stackoverflow.com/questions/5026689/how-to-check-whether-a-string-in-net-is-a-number-or-not
		public static bool IsDigit(string text)
		{
			foreach (char c in text)
			{
				if (!Char.IsDigit(c)) {
					return false;
				}
			}
			return (0 < text.Length);
		}

		// Return suffix after underscore as digits or -1.
		// "letter_2" returns 2.
		// More Examples: Editor/Tests/TestStringUtil.cs
		public static int ParseIndex(string tileName)
		{
			int tileIndex = -1;
			string[] parts = tileName.Split('_');
			if (2 <= parts.Length && IsDigit(parts[1])) {
				tileIndex = int.Parse(parts[1]);
			}
			return tileIndex;
		}

		public static string NormalizeLines(string text)
		{
			return text.Replace("\r\n", "\n").Replace("\r", "\n");
		}

		//
		// @param	path	Unconventionally, Unity expects the file extension is omitted.  This utility will try again to remove file extension if it can't load the first time.
		// Normalize line endings and trim whitespace.
		// Expects path is relative to "Assets/Resources/" folder.
		// Unity automatically embeds resource files.  Does not dynamicly load file, because file system is incompatible on mobile device or HTML5.
		// 
		public static string Read(string path)
		{
			TextAsset asset = (TextAsset) Resources.Load(path);
			if (null == asset) {
				string basename = System.IO.Path.ChangeExtension(path, null);
				asset = (TextAsset) Resources.Load(basename);
				if (null == asset) {
					DebugUtil.Log("Did you omit the file extension?  Did you place the file in the Assets/Resources/ folder?  Path was " + path + " and without extension was " + basename);
				}
			}
			string text = asset.text;
			text = NormalizeLines(text);
			text = text.Trim();
			// DebugUtil.Log("StringUtil.Read: " + text);
			return text;
		}

		//
		// I wish C# API were as simple as JavaScript and Python:
		// http://stackoverflow.com/questions/1126915/how-do-i-split-a-string-by-a-multi-character-delimiter-in-c
		// 
		public static string[] Split(string text, string delimiter)
		{
			string[] delimiters = new string[] {delimiter};
			string[] parts = text.Split(delimiters, StringSplitOptions.None);
			return parts;
		}

		public static string Remove(string text, int index)
		{
			return text.Remove(index);
		}

		//
		// Trim whitespace.  
		// Test case:  Expect 5 rows.  Got 6.  Last row is empty, from final line delimiter at the end of the file's text.
		// 
		// Would be nice when there's more time to generate hashes.
		// 
		public static string[][] ParseCsv(string text, string fieldDelimiter = ",")
		{
			text = text.Trim();
			string[] lines = Split(text, lineDelimiter);
			string[][] table = new string[lines.Length][];
			for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
			{
				string line = lines[lineIndex];
				string[] row = Split(line, fieldDelimiter);
				table[lineIndex] = row;
			}
			// DebugUtil.Log("StringUtil.ParseCsv: lines " + table.Length);
			return table;
		}
	}
}
