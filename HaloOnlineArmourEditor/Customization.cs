using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaloOnlineArmourEditor
{
	public static class Customization
	{
		public const string HALO3_VISOR_COLOR = "#FFAB00";

		public const Race DEFAULT_RACE = Race.Spartan;
		public const Color DEFAULT_PRIMARY_COLOR = Color.Olive;
		public const Color DEFAULT_SECONDARY_COLOR = Color.Olive;
		public const Color DEFAULT_DETAIL_COLOR = Color.Olive; //lights

		public enum Race
		{
			Spartan = 0,
			Elite = 1
			//Anything else renders a Halo 3 Logo.
		}
		public enum Color
		{
			Steel = 0,
			Silver = 1,
			White = 2,
			Red = 3,
			Mauve = 4,
			Salmon = 5,
			Orange = 6,
			Coral = 7,
			Peach = 8,
			Gold = 9,
			Yellow = 10,
			Pale = 11,
			Sage = 12,
			Green = 13,
			Olive = 14,
			Teal = 15,
			Aqua = 16,
			Cyan = 17,
			Blue = 18,
			Cobalt = 19,
			Sapphire = 20,
			Violet = 21,
			Orchid = 22,
			Lavender = 23,
			Crimson = 24,
			RudyWine = 25,
			Pink = 26,
			Brown = 27,
			Tan = 28,
			Khaki = 29
		}

		private static Tuple<string, Race>[] h3_ho_Races = new Tuple<string, Race>[]
		{
				new Tuple<string, Race>("spartan", Race.Spartan),
				new Tuple<string, Race>("elite", Race.Elite ),
		};
		private static Tuple<string, Color>[] h3_ho_Colors = new Tuple<string, Color>[]
		{
				new Tuple<string, Color>("#282828", Color.Steel),
				new Tuple<string, Color>("#7C7C7C", Color.Silver ),
				new Tuple<string, Color>("#C3C3C3", Color.White),
				new Tuple<string, Color>("#620B0B", Color.Red),
				new Tuple<string, Color>("#BD2B2B", Color.Mauve),
				new Tuple<string, Color>("#E24444", Color.Salmon),
				new Tuple<string, Color>("#BC4D00", Color.Orange),
				new Tuple<string, Color>("#F4791F", Color.Coral),
				new Tuple<string, Color>("#FFA362", Color.Peach),
				new Tuple<string, Color>("#A77708", Color.Gold),
				new Tuple<string, Color>("#DD9A08", Color.Yellow),
				new Tuple<string, Color>("#FFBC3B", Color.Pale),
				new Tuple<string, Color>("#1F3602", Color.Sage),
				new Tuple<string, Color>("#546E26", Color.Green),
				new Tuple<string, Color>("#B3E164", Color.Olive),
				new Tuple<string, Color>("#0A3B3F", Color.Teal),
				new Tuple<string, Color>("#178C95", Color.Aqua),
				new Tuple<string, Color>("#54DDDB", Color.Cyan),
				new Tuple<string, Color>("#0B2156", Color.Blue),
				new Tuple<string, Color>("#1D4BBC", Color.Cobalt),
				new Tuple<string, Color>("#5D85EB", Color.Sapphire),
				new Tuple<string, Color>("#1D1052", Color.Violet),
				new Tuple<string, Color>("#5438CF", Color.Orchid),
				new Tuple<string, Color>("#A18CFF", Color.Lavender),
				new Tuple<string, Color>("#460014", Color.Crimson),
				new Tuple<string, Color>("#AF0E46", Color.RudyWine),
				new Tuple<string, Color>("#FF4D8A", Color.Pink),
				new Tuple<string, Color>("#1C0D02", Color.Brown),
				new Tuple<string, Color>("#774D31", Color.Tan),
				new Tuple<string, Color>("#C69069", Color.Khaki)
		};

		public static Race GetRaceFromPrefs(string prefsKey)
		{
			foreach (Tuple<string, Race> race in h3_ho_Races)
			{
				if (race.Item1 == prefsKey)
					return race.Item2;
			}
			return Race.Spartan;
		}
		public static Color GetColorFromPrefs(string prefsKey)
		{
			foreach (Tuple<string, Color> color in h3_ho_Colors)
			{
				if (color.Item1 == prefsKey)
					return color.Item2;
			}
			return Color.Olive;
		}

		public static string GetPrefsFromRace(Race _race)
		{
			foreach (Tuple<string, Race> race in h3_ho_Races)
			{
				if (race.Item2 == _race)
					return race.Item1;
			}

			return "spartan";
		}
		public static string GetPrefsFromColor(Color _color)
		{
			foreach (Tuple<string, Color> color in h3_ho_Colors)
			{
				if (color.Item2 == _color)
					return color.Item1;
			}

			return "#B3E164";
		}

		static Random random = new Random();
		public static Color GetRandomColor()
		{
			Color[] colors = (Color[])Enum.GetValues(typeof(Color));
			return colors[random.Next(0, colors.Length - 1)];
		}

		public static class Spartan
		{
			public const Helmet DEFAULT_HELMET = Helmet.MarkVI;
			public const Chest DEFAULT_CHEST = Chest.MarkVI;
			public const Shoulder DEFAULT_LEFT_SHOULDER = Shoulder.MarkVI;
			public const Shoulder DEFAULT_RIGHT_SHOULDER = Shoulder.MarkVI;

			public enum Helmet
			{
				MarkVI = 0,
				CQB = 1,
				EVA = 2,
				Recon = 3,
				EOD = 4,
				Hayabusa = 5,
				Security = 6,
				Scout = 7,
				ODST = 8, //Helmet Only.
				MarkV = 9, //Helmet Only.
				Rogue = 10 //Helmet Only
			}
			public enum Chest
			{
				MarkVI = 0,
				CQB = 1,
				EVA = 2,
				Recon = 3,
				Hayabusa = 4,
				EOD = 5,
				Scout = 6,
				Katana = 7, //Chest Only
				Bungie = 8 //Chest Only
			}
			public enum Shoulder
			{
				MarkVI = 0,
				CQB = 1,
				EVA = 2,
				Recon = 3,
				EOD = 4,
				Hayabusa = 5,
				Security = 6,
				Scout = 7
			}

			private static Tuple<string, Helmet>[] h3_ho_Helmets = new Tuple<string, Helmet>[]
			{
				new Tuple<string, Helmet>("air_assault", Helmet.MarkVI),
				new Tuple<string, Helmet>("stealth", Helmet.CQB ),
				new Tuple<string, Helmet>("renegade", Helmet.EVA),
				new Tuple<string, Helmet>("nihard", Helmet.Recon ),
				new Tuple<string, Helmet>("gladiator", Helmet.EOD ),
				new Tuple<string, Helmet>("mac", Helmet.Hayabusa ),
				new Tuple<string, Helmet>("shark", Helmet.EVA ),
				new Tuple<string, Helmet>("juggernaut", Helmet.Scout ),
				new Tuple<string, Helmet>("dutch", Helmet.MarkV ),
				new Tuple<string, Helmet>("chameleon", Helmet.Rogue ),
				new Tuple<string, Helmet>("halberd", Helmet.ODST ),
				new Tuple<string, Helmet>("cyclops", Helmet.MarkVI ),
				new Tuple<string, Helmet>("scanner", Helmet.MarkVI ),
				new Tuple<string, Helmet>("mercenary", Helmet.CQB ),
				new Tuple<string, Helmet>("hoplite", Helmet.EVA ),
				new Tuple<string, Helmet>("ballista", Helmet.Recon ),
				new Tuple<string, Helmet>("strider", Helmet.EOD),
				new Tuple<string, Helmet>("demo", Helmet.Hayabusa ),
				new Tuple<string, Helmet>("orbital", Helmet.Scout ),
				new Tuple<string, Helmet>("spectrum", Helmet.MarkV ),
				new Tuple<string, Helmet>("gungnir", Helmet.Rogue ),
				new Tuple<string, Helmet>("hammerhead", Helmet.ODST ),
				new Tuple<string, Helmet>("omni", Helmet.MarkVI ),
				new Tuple<string, Helmet>("oracle", Helmet.CQB),
				new Tuple<string, Helmet>("silverback", Helmet.EVA ),
				new Tuple<string, Helmet>("widowmaker", Helmet.Recon ),
			};
			private static Tuple<string,Chest>[] h3_ho_Chests = new Tuple<string, Chest>[]
			{
				new Tuple<string, Chest>("air_assault", Chest.MarkVI),
				new Tuple<string, Chest>("stealth", Chest.CQB ),
				new Tuple<string, Chest>("renegade", Chest.EVA),
				new Tuple<string, Chest>("nihard", Chest.Recon ),
				new Tuple<string, Chest>("gladiator", Chest.EOD ),
				new Tuple<string, Chest>("mac", Chest.Hayabusa ),
				new Tuple<string, Chest>("shark", Chest.Katana ),
				new Tuple<string, Chest>("juggernaut", Chest.Scout ),
				new Tuple<string, Chest>("dutch", Chest.MarkVI ),
				new Tuple<string, Chest>("chameleon", Chest.MarkVI ),
				new Tuple<string, Chest>("halberd", Chest.MarkVI ),
				new Tuple<string, Chest>("cyclops", Chest.MarkVI ),
				new Tuple<string, Chest>("scanner", Chest.MarkVI ),
				new Tuple<string, Chest>("mercenary", Chest.CQB ),
				new Tuple<string, Chest>("hoplite", Chest.EVA ),
				new Tuple<string, Chest>("ballista", Chest.Recon ),
				new Tuple<string, Chest>("strider", Chest.Bungie),
				new Tuple<string, Chest>("demo", Chest.Hayabusa ),
				new Tuple<string, Chest>("orbital", Chest.Scout ),
				new Tuple<string, Chest>("spectrum", Chest.MarkVI ),
				new Tuple<string, Chest>("gungnir", Chest.MarkVI ),
				new Tuple<string, Chest>("hammerhead", Chest.MarkVI ),
				new Tuple<string, Chest>("omni", Chest.MarkVI ),
				new Tuple<string, Chest>("oracle", Chest.CQB),
				new Tuple<string, Chest>("silverback", Chest.EVA ),
				new Tuple<string, Chest>("widowmaker", Chest.Recon ),
			};
			private static Tuple<string, Shoulder>[] h3_ho_Shoulders = new Tuple<string, Shoulder>[]
			{
				new Tuple<string, Shoulder>("air_assault", Shoulder.MarkVI),
				new Tuple<string, Shoulder>("stealth", Shoulder.CQB ),
				new Tuple<string, Shoulder>("renegade", Shoulder.EVA),
				new Tuple<string, Shoulder>("nihard", Shoulder.Recon ),
				new Tuple<string, Shoulder>("gladiator", Shoulder.EOD ),
				new Tuple<string, Shoulder>("mac", Shoulder.Hayabusa ),
				new Tuple<string, Shoulder>("shark", Shoulder.Security ),
				new Tuple<string, Shoulder>("juggernaut", Shoulder.Scout ),
				new Tuple<string, Shoulder>("dutch", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("chameleon", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("halberd", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("cyclops", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("scanner", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("mercenary", Shoulder.CQB ),
				new Tuple<string, Shoulder>("hoplite", Shoulder.EVA ),
				new Tuple<string, Shoulder>("ballista", Shoulder.Recon ),
				new Tuple<string, Shoulder>("strider", Shoulder.EOD),
				new Tuple<string, Shoulder>("demo", Shoulder.Hayabusa ),
				new Tuple<string, Shoulder>("orbital", Shoulder.Scout ),
				new Tuple<string, Shoulder>("spectrum", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("gungnir", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("hammerhead", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("omni", Shoulder.MarkVI ),
				new Tuple<string, Shoulder>("oracle", Shoulder.CQB),
				new Tuple<string, Shoulder>("silverback", Shoulder.EVA ),
				new Tuple<string, Shoulder>("widowmaker", Shoulder.Recon ),
			};

			public static Helmet GetHelmetFromPrefs(string prefsKey)
			{
				foreach(Tuple<string,Helmet> helmet in h3_ho_Helmets)
				{
					if (helmet.Item1 == prefsKey)
						return helmet.Item2;
				}

				return 0;
			}
			public static Chest GetChestFromPrefs(string prefsKey)
			{
				foreach (Tuple<string, Chest> chest in h3_ho_Chests)
				{
					if (chest.Item1 == prefsKey)
						return chest.Item2;
				}

				return 0;
			}
			public static Shoulder GetShoulderFromPrefs(string prefsKey)
			{
				foreach (Tuple<string, Shoulder> shoulder in h3_ho_Shoulders)
				{
					if (shoulder.Item1 == prefsKey)
						return shoulder.Item2;
				}

				return 0;
			}

			public static string GetPrefsFromHelmet(Helmet _helmet)
			{
				foreach (Tuple<string, Helmet> helmet in h3_ho_Helmets)
				{
					if (helmet.Item2 == _helmet)
						return helmet.Item1;
				}

				return "air_assault";
			}
			public static string GetPrefsFromChest(Chest _chest)
			{
				foreach (Tuple<string, Chest> chest in h3_ho_Chests)
				{
					if (chest.Item2 == _chest)
						return chest.Item1;
				}

				return "air_assault";
			}
			public static string GetPrefsFromShoulder(Shoulder _shoulder)
			{
				foreach (Tuple<string, Shoulder> shoulder in h3_ho_Shoulders)
				{
					if (shoulder.Item2 == _shoulder)
						return shoulder.Item1;
				}

				return "air_assault";
			}

			public static Helmet GetRandomHelmet()
			{
				Helmet[] helmets = (Helmet[])Enum.GetValues(typeof(Helmet));
				return helmets[random.Next(0, helmets.Length - 1)];
			}
			public static Chest GetRandomChest()
			{
				Chest[] chests = (Chest[])Enum.GetValues(typeof(Chest));
				return chests[random.Next(0, chests.Length - 1)];
			}
			public static Shoulder GetRandomShoulder()
			{
				Shoulder[] shoulders = (Shoulder[])Enum.GetValues(typeof(Shoulder));
				return shoulders[random.Next(0, shoulders.Length - 1)];
			}
		}

		public static class Elite
		{
			public const Helmet DEFAULT_HELMET = 0;
			public const Chest DEFAULT_CHEST = 0;
			public const Shoulder DEFAULT_LEFT_SHOULDER = 0;
			public const Shoulder DEFAULT_RIGHT_SHOULDER = 0;

			public enum Helmet
			{
				Combat = 0,
				Assault = 1,
				Flight = 2,
				Commando = 3,
				Ascetic = 4
			}
			public enum Chest
			{
				Combat = 0,
				Assault = 1,
				Flight = 2,
				Commando = 3,
				Ascetic = 4
			}
			public enum Shoulder
			{
				Combat = 0,
				Assault = 1,
				Flight = 2,
				Commando = 3,
				Ascetic = 4
			}

			private static Tuple<string, Helmet>[] h3_ho_Helmets = new Tuple<string, Helmet>[]
			{
			};
			private static Tuple<string, Chest>[] h3_ho_Chests = new Tuple<string, Chest>[]
			{
			};
			private static Tuple<string, Shoulder>[] h3_ho_Shoulders = new Tuple<string, Shoulder>[]
			{
			};

			public static Helmet GetHelmetFromPrefs(string prefsKey)
			{
				foreach (Tuple<string, Helmet> helmet in h3_ho_Helmets)
				{
					if (helmet.Item1 == prefsKey)
						return helmet.Item2;
				}

				return 0;
			}
			public static Chest GetChestFromPrefs(string prefsKey)
			{
				foreach (Tuple<string, Chest> chest in h3_ho_Chests)
				{
					if (chest.Item1 == prefsKey)
						return chest.Item2;
				}

				return 0;
			}
			public static Shoulder GetShoulderFromPrefs(string prefsKey)
			{
				foreach (Tuple<string, Shoulder> shoulder in h3_ho_Shoulders)
				{
					if (shoulder.Item1 == prefsKey)
						return shoulder.Item2;
				}

				return 0;
			}

			public static string GetPrefsFromHelmet(Helmet _helmet)
			{
				foreach (Tuple<string, Helmet> helmet in h3_ho_Helmets)
				{
					if (helmet.Item2 == _helmet)
						return helmet.Item1;
				}

				return "air_assault";
			}
			public static string GetPrefsFromChest(Chest _chest)
			{
				foreach (Tuple<string, Chest> chest in h3_ho_Chests)
				{
					if (chest.Item2 == _chest)
						return chest.Item1;
				}

				return "air_assault";
			}
			public static string GetPrefsFromShoulder(Shoulder _shoulder)
			{
				foreach (Tuple<string, Shoulder> shoulder in h3_ho_Shoulders)
				{
					if (shoulder.Item2 == _shoulder)
						return shoulder.Item1;
				}

				return "air_assault";
			}

			public static Helmet GetRandomHelmet()
			{
				Helmet[] helmets = (Helmet[])Enum.GetValues(typeof(Helmet));
				return helmets[random.Next(0, helmets.Length - 1)];
			}
			public static Chest GetRandomChest()
			{
				Chest[] chests = (Chest[])Enum.GetValues(typeof(Chest));
				return chests[random.Next(0, chests.Length - 1)];
			}
			public static Shoulder GetRandomShoulder()
			{
				Shoulder[] shoulders = (Shoulder[])Enum.GetValues(typeof(Shoulder));
				return shoulders[random.Next(0, shoulders.Length - 1)];
			}
		}
	}
}
