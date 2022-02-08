using System.Collections.Generic;
using System.IO;
using BepInEx;

namespace AsgardLegacy
{
	public class ConfigSync
	{
		public static string ConfigPath = Path.GetDirectoryName(Paths.BepInExConfigPath) + Path.DirectorySeparatorChar.ToString() + "TribesOfValheim.cfg";

		public static void RPC_ConfigSync(long sender, ZPackage configPkg)
		{
			if (ZNet.instance.IsServer())
			{
				var zpackage = new ZPackage();
				var array = File.ReadAllLines(ConfigPath);
				var list = new List<string>();
				for (var i = 0; i < array.Length; i++)
				{
					if (array[i].Trim().StartsWith("al_svr_"))
						list.Add(array[i]);
				}
				list.Add("al_svr_version = 0.0.1");
				zpackage.Write(list.Count);
				foreach (var text in list)
				{
					zpackage.Write(text);
				}
				ZRoutedRpc.instance.InvokeRoutedRPC(sender, "ConfigSync", new object[] { zpackage });
				ZLog.Log("Tribes of Valheim server configurations synced to peer #" + sender);
			}
			else
			{
				if (configPkg == null || configPkg.Size() <= 0 || sender != AsgardLegacy.ServerID)
					return;

				var lineNumber = configPkg.ReadInt();
				if (lineNumber == 0)
				{
					ZLog.LogWarning("Got zero line config file from server. Cannot load.");
					return;
				}

				var trimChars = new char[] { ' ' , '=' };
				var versionMismatch = false;
				for (var j = 0; j < lineNumber; j++)
				{
					var text2 = configPkg.ReadString();
					var text3 = text2.Substring(0, text2.IndexOf('=') + 1);
					text3 = text3.Trim(trimChars);
					if (text3 == "al_svr_version")
					{
						var text4 = text2.Substring(text2.IndexOf('=') + 1);
						text4 = text4.Trim(trimChars);
						if (text4 == "0.0.1")
							continue;

						ZLog.Log("VL CLIENT -------------- version failure: server had version [" + text4 + "] and client had version [0.0.1]");
						versionMismatch = true;
						break;
					}
					else
					{
						if (GlobalConfigs.ConfigStrings.ContainsKey(text3))
						{
							var text8 = text2.Substring(text2.IndexOf('=') + 1).Trim(trimChars);
							text8 = text3 == "al_svr_enforceConfigClass"
								? (text8.ToLower().ToString() == "true") ? "1" : "0"
								: text3 == "al_svr_aoeRequiresLoS"
									? (text8.ToLower().ToString() == "true") ? "1" : "0"
									: text3 == "al_svr_allowAltarClassChange"
										? (text8.ToLower().ToString() == "true") ? "1" : "0"
										: text8;

							var value = 1;
							try
							{
								value = int.Parse(text8);
							}
							catch
							{
								text8 = text8.Replace(",", ".");
							}
							try
							{
								value = int.Parse(text8);
							}
							catch
							{
								text8 = text8.Replace(".", ",");
							}
							try
							{
								value = int.Parse(text8);
							}
							catch
							{
								ZLog.Log("Tribes of Valheim : unable to sync modifiers - setting to default");
								value = 1;
							}
							GlobalConfigs.ConfigStrings[text3] = value;
						}
					}
				}

				if (versionMismatch)
				{
					ZLog.LogWarning("Tribes of Valheim version mismatch; disabling.");
					AsgardLegacy.playerEnabled = false;
				}
				else
					ZLog.Log("Tribes of Valheim configurations synced to server.");
			}
		}
	}
}
