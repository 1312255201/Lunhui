using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Lunhui
{
	class MangGetOS
	{
		public string disk;
		public string Line;
		public bool islinux;
		public string config;
		public string configpath;
		public string configplayer;
	}
	class MangConfig
	{
		public static string GetConfig(string name)
		{
			string value = "";
			MangGetOS getos = GetOSConfig();
			if (!Directory.Exists(getos.configpath))
				Directory.CreateDirectory(getos.configpath);
			if (!File.Exists(getos.config))
				File.WriteAllText(getos.config, "=-Config-=");
			string[] text = System.IO.File.ReadAllLines(getos.config);
			foreach (string cat in text)
			{
				if (cat.Length > name.Length)
					if (cat.Substring(0, name.Length + 1) == name + ":")
					{
						value = cat.Replace(name + ": ", "");
					}
			}
			return value;
		}

		public static void SetConfig(string item, string content)
		{
			MangGetOS getos = GetOSConfig();
			if (!Directory.Exists(getos.configpath))
				Directory.CreateDirectory(getos.configpath);
			string[] text = System.IO.File.ReadAllLines(getos.config);
			List<string> pz = new List<string>();
			bool writing = false;
			foreach (string cat in text)
			{
				if (cat.Length > item.Length)
				{
					if (cat.Substring(0, item.Length + 1) == item + ":")
					{
						writing = true;
						pz.Add(item + ": " + content);
					}
					else
					{
						pz.Add(cat);
					}
				}
				else
				{
					pz.Add(cat);
				}
			}
			if (!writing)
			{
				pz.Add(item + ": " + content);
			}
			File.WriteAllLines(getos.config, pz);
		}
		public static MangGetOS GetOSConfig()
		{
			string disk;
			string Line;
			string configplayer;
			bool islinux;
			string configtxt;
			string configpath;
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				disk = @"/";
				Line = "\n";
				islinux = true;
				configpath = @"/manghui";
				configtxt = @"/manghui/config.txt";
				configplayer = @"/manghui/player.txt";
			}
			else
			{
				disk = @"C:\";
				Line = "\r\n";
				islinux = false;
				configpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SCP Secret Laboratory\manghui";
				configtxt = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SCP Secret Laboratory\manghui\config.txt";
				configplayer = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SCP Secret Laboratory\manghui\player.txt";
			}
			return new MangGetOS
			{
				disk = disk,
				Line = Line,
				islinux = islinux,
				configpath = configpath,
				config = configtxt,
				configplayer = configplayer
			};
		}
	}
}
