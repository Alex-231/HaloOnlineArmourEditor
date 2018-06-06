//Dewrito Updater Config Code
//Shamelessly stolen from Fish's repo
//Medsouz did the heavy lifting ;)
//https://github.com/FishPhd/DewritoUpdater/blob/master/DewritoUpdater/Cfg.cs
//Modified by Alex-231 for HaloOnlineArmourEditor.

using HaloOnlineArmourEditor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Dewritwo
{
	internal static class Config
	{
		public static Dictionary<string, string> ConfigFile = new Dictionary<string, string>();
		public static Dictionary<string, string> LauncherConfigFile = new Dictionary<string, string>();

		public static void SetVariable(string varName, string varValue, ref Dictionary<string, string> configDict,
		  bool rcon = true)
		{
			if (configDict.ContainsKey(varName))
				configDict[varName] = varValue;
			else
				configDict.Add(varName, varValue);

			if (CheckIfProcessIsRunning("eldorado") && rcon)
				AsyncRcon(varName, varValue);
		}

		public static bool CheckIfProcessIsRunning(string nameSubstring)
		{
			return Process.GetProcesses().Any(p => p.ProcessName.Contains(nameSubstring));
		}


		public static bool SaveConfigFile(string cfgFileName, Dictionary<string, string> configDict)
		{
			try
			{
				var lines = configDict.Select(kvp => kvp.Key + " \"" + kvp.Value + "\"").ToList();

				/* Debug I assume.
				if (File.Exists(cfgFileName))
				  File.Delete(cfgFileName);
				*/

				if (File.Exists(cfgFileName))
				{
					File.WriteAllLines(cfgFileName + ".temp", lines.ToArray());
					File.Replace(cfgFileName + ".temp", cfgFileName, cfgFileName + ".bak");
				}
				else
				{
					File.WriteAllLines(cfgFileName, lines.ToArray());
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		public static string GetConfigVariable(string key, string defaultValue)
		{
			if (ConfigFile.ContainsKey(key))
			{
				return ConfigFile[key];
			}
			ConfigFile[key] = defaultValue;
			return defaultValue;
		}

		private static string DewCmd(string cmd)
		{
			var data = new byte[1024];
			TcpClient server;
			try
			{
				server = new TcpClient("127.0.0.1", 2448);
			}
			catch (SocketException)
			{
				return "Is ElDorito Running?";
			}

			var ns = server.GetStream();

			var recv = ns.Read(data, 0, data.Length);
			var stringData = Encoding.ASCII.GetString(data, 0, recv);

			ns.Write(Encoding.ASCII.GetBytes(cmd), 0, cmd.Length);

			/*
			Console.WriteLine(cmd);
			Console.WriteLine(stringData);
			*/

			ns.Flush();

			ns.Close();
			server.Close();

			return stringData;
		}

		private static async void AsyncRcon(string varName, string varValue)
		{
			await Task.Run(() => DewCmd(varName + ' ' + varValue));
		}

		private static bool LoadConfigFile(string cfgFileName, ref Dictionary<string, string> returnDict)
		{
			if (returnDict == null) throw new ArgumentNullException(nameof(returnDict));

			if (!File.Exists(cfgFileName))
				return false;

			var lines = File.ReadAllLines(cfgFileName);
			foreach (var line in lines)
			{
				var splitIdx = line.IndexOf(" ", StringComparison.Ordinal);
				if (splitIdx < 0 || splitIdx + 1 >= line.Length)
					continue; // line isn't valid?
				var varName = line.Substring(0, splitIdx);
				var varValue = line.Substring(splitIdx + 1);

				// remove quotes
				if (varValue.StartsWith("\""))
					varValue = varValue.Substring(1);
				if (varValue.EndsWith("\""))
					varValue = varValue.Substring(0, varValue.Length - 1);

				SetVariable(varName, varValue, ref returnDict);
			}
			return true;
		}

		public static void Initial()
		{
			var cfgFileExists = LoadConfigFile("dewrito_prefs.cfg", ref ConfigFile);

			if (!cfgFileExists)
			{;
				SetVariable("Player.Representation", Customization.GetPrefsFromRace(Customization.DEFAULT_RACE), ref ConfigFile, false);
				SetVariable("Player.Armor.Accessory", "air_assault", ref ConfigFile, false);
				SetVariable("Player.Armor.Arms", Customization.Spartan.GetPrefsFromShoulder(Customization.Spartan.DEFAULT_RIGHT_SHOULDER), ref ConfigFile, false);
				SetVariable("Player.Armor.Chest", Customization.Spartan.GetPrefsFromChest(Customization.Spartan.DEFAULT_CHEST), ref ConfigFile, false);
				SetVariable("Player.Armor.Helmet", Customization.Spartan.GetPrefsFromHelmet(Customization.Spartan.DEFAULT_HELMET), ref ConfigFile, false);
				SetVariable("Player.Armor.Legs", "air_assault", ref ConfigFile, false);
				SetVariable("Player.Armor.Pelvis", "", ref ConfigFile, false);
				SetVariable("Player.Armor.Shoulders", Customization.Spartan.GetPrefsFromShoulder(Customization.Spartan.DEFAULT_LEFT_SHOULDER), ref ConfigFile, false);
				SetVariable("Player.Colors.Primary", Customization.GetPrefsFromColor(Customization.DEFAULT_PRIMARY_COLOR), ref ConfigFile, false);
				SetVariable("Player.Colors.Secondary", Customization.GetPrefsFromColor(Customization.DEFAULT_SECONDARY_COLOR), ref ConfigFile, false);
				SetVariable("Player.Colors.Visor", Customization.GetPrefsFromColor(Customization.DEFAULT_DETAIL_COLOR), ref ConfigFile, false);
				SetVariable("Player.Colors.Lights", "#000000", ref ConfigFile, false);
				SetVariable("Player.Colors.Holo", "#000000", ref ConfigFile, false);
			}
		}
	}
}