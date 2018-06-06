using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Dewritwo;
using System.Net;
using System.Windows.Media.Animation;
using System.Reflection;
using System.IO;
using System.Globalization;

namespace HaloOnlineArmourEditor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class CustomizeWindow : MetroWindow
	{
		private bool ignoreSelectionChange = true;

		private Customization.Race selectedRace = Customization.DEFAULT_RACE;

		//0 = None
		//1 = primary
		//2 = secondary
		//3 = detail
		int editingColor = 0;

		Customization.Color primaryColor;
		Customization.Color secondaryColor;
		Customization.Color detailColor;

		public CustomizeWindow()
		{
			Config.Initial();
			InitializeComponent();

			selectedRace = Customization.GetRaceFromPrefs(Config.GetConfigVariable("Player.Representation", Customization.GetPrefsFromRace(Customization.DEFAULT_RACE)));

			UpdateDropdownValues();

			//primaryColorBox.ItemsSource = Enum.GetValues(typeof(Customization.Color));
			//secondaryColorBox.ItemsSource = Enum.GetValues(typeof(Customization.Color));
			//detailColorBox.ItemsSource = Enum.GetValues(typeof(Customization.Color));

			if (selectedRace == Customization.Race.Elite)
			{

				helmetBox.SelectedItem = Customization.Elite.GetHelmetFromPrefs(Config.GetConfigVariable("Player.Armor.Helmet", Customization.Elite.GetPrefsFromHelmet(Customization.Elite.DEFAULT_HELMET)));
				leftShoulderBox.SelectedItem = Customization.Elite.GetShoulderFromPrefs(Config.GetConfigVariable("Player.Armor.Shoulders", Customization.Elite.GetPrefsFromShoulder(Customization.Elite.DEFAULT_LEFT_SHOULDER)));
				rightShoulderBox.SelectedItem = Customization.Elite.GetShoulderFromPrefs(Config.GetConfigVariable("Player.Armor.Arms", Customization.Elite.GetPrefsFromShoulder(Customization.Elite.DEFAULT_RIGHT_SHOULDER)));
				chestBox.SelectedItem = Customization.Elite.GetChestFromPrefs(Config.GetConfigVariable("Player.Armor.Chest", Customization.Elite.GetPrefsFromChest(Customization.Elite.DEFAULT_CHEST)));
			}
			else
			{
				helmetBox.SelectedItem = Customization.Spartan.GetHelmetFromPrefs(Config.GetConfigVariable("Player.Armor.Helmet", Customization.Spartan.GetPrefsFromHelmet(Customization.Spartan.DEFAULT_HELMET)));
				leftShoulderBox.SelectedItem = Customization.Spartan.GetShoulderFromPrefs(Config.GetConfigVariable("Player.Armor.Shoulders", Customization.Spartan.GetPrefsFromShoulder(Customization.Spartan.DEFAULT_LEFT_SHOULDER)));
				rightShoulderBox.SelectedItem = Customization.Spartan.GetShoulderFromPrefs(Config.GetConfigVariable("Player.Armor.Arms", Customization.Spartan.GetPrefsFromShoulder(Customization.Spartan.DEFAULT_RIGHT_SHOULDER)));
				chestBox.SelectedItem = Customization.Spartan.GetChestFromPrefs(Config.GetConfigVariable("Player.Armor.Chest", Customization.Spartan.GetPrefsFromChest(Customization.Spartan.DEFAULT_CHEST)));
			}

			primaryColor = Customization.GetColorFromPrefs(Config.GetConfigVariable("Player.Colors.Primary", Customization.GetPrefsFromColor(Customization.DEFAULT_PRIMARY_COLOR)));
			secondaryColor = Customization.GetColorFromPrefs(Config.GetConfigVariable("Player.Colors.Secondary", Customization.GetPrefsFromColor(Customization.DEFAULT_SECONDARY_COLOR)));
			detailColor = Customization.GetColorFromPrefs(Config.GetConfigVariable("Player.Colors.Lights", Customization.GetPrefsFromColor(Customization.DEFAULT_DETAIL_COLOR)));

			ignoreSelectionChange = false;

			UpdateConfigAndPreview();

			//DownloadImages();
		}

		private void RaceButton_Clicked(object sender, RoutedEventArgs args)
		{
			Button button = sender as Button;
			selectedRace = (Customization.Race)Enum.Parse(typeof(Customization.Race), button.Name);
			UpdateDropdownValues();
		}

		private void UpdateDropdownValues()
		{
			ignoreSelectionChange = true;

			if (selectedRace == Customization.Race.Elite)
			{
				helmetBox.ItemsSource = Enum.GetValues(typeof(Customization.Elite.Helmet));
				leftShoulderBox.ItemsSource = Enum.GetValues(typeof(Customization.Elite.Shoulder));
				rightShoulderBox.ItemsSource = Enum.GetValues(typeof(Customization.Elite.Shoulder));
				chestBox.ItemsSource = Enum.GetValues(typeof(Customization.Elite.Chest));

				helmetBox.SelectedItem = Customization.Elite.DEFAULT_HELMET;
				leftShoulderBox.SelectedItem = Customization.Elite.DEFAULT_LEFT_SHOULDER;
				rightShoulderBox.SelectedItem = Customization.Elite.DEFAULT_RIGHT_SHOULDER;
				chestBox.SelectedItem = Customization.Elite.DEFAULT_CHEST;
			}
			else
			{
				helmetBox.ItemsSource = Enum.GetValues(typeof(Customization.Spartan.Helmet));
				leftShoulderBox.ItemsSource = Enum.GetValues(typeof(Customization.Spartan.Shoulder));
				rightShoulderBox.ItemsSource = Enum.GetValues(typeof(Customization.Spartan.Shoulder));
				chestBox.ItemsSource = Enum.GetValues(typeof(Customization.Spartan.Chest));

				helmetBox.SelectedItem = Customization.Spartan.DEFAULT_HELMET;
				leftShoulderBox.SelectedItem = Customization.Spartan.DEFAULT_LEFT_SHOULDER;
				rightShoulderBox.SelectedItem = Customization.Spartan.DEFAULT_RIGHT_SHOULDER;
				chestBox.SelectedItem = Customization.Spartan.DEFAULT_CHEST;
			}

			primaryColor = Customization.DEFAULT_PRIMARY_COLOR;
			secondaryColor = Customization.DEFAULT_SECONDARY_COLOR;
			detailColor = Customization.DEFAULT_DETAIL_COLOR;

			ignoreSelectionChange = false;

			UpdateConfigAndPreview();
		}

		public void UpdatePreviewImage()
		{
			BitmapImage previewBitmap = new BitmapImage();
			previewBitmap.BeginInit();
				previewBitmap.UriSource = new Uri(String.Format(@"https://halo.bungie.net/stats/Halo3/PlayerModel.ashx?p1={0}&p2={1}&p3={2}&p4={3}&p5={4}&p6={5}&p7={6}&p8={7}",
				(int)selectedRace, (int)helmetBox.SelectedItem, (int)rightShoulderBox.SelectedItem, (int)leftShoulderBox.SelectedItem, (int)chestBox.SelectedItem, (int)primaryColor, (int)secondaryColor, (int)detailColor));

			previewBitmap.EndInit();

			previewImage.Source = previewBitmap;
		}

		public void UpdateColorBoxes()
		{
			primaryColorRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(Customization.GetPrefsFromColor(primaryColor)));
			secondaryColorRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(Customization.GetPrefsFromColor(secondaryColor)));
			detailColorRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(Customization.GetPrefsFromColor(detailColor)));
		}

		public void UpdateConfigAndPreview()
		{
			if (ignoreSelectionChange)
				return;

			if (selectedRace == Customization.Race.Elite)
			{
				Config.SetVariable("Player.Armor.Helmet", Customization.Elite.GetPrefsFromHelmet((Customization.Elite.Helmet)helmetBox.SelectedItem), ref Config.ConfigFile, true);
				Config.SetVariable("Player.Armor.Shoulders", Customization.Elite.GetPrefsFromShoulder((Customization.Elite.Shoulder)leftShoulderBox.SelectedItem), ref Config.ConfigFile, true);
				Config.SetVariable("Player.Armor.Arms", Customization.Elite.GetPrefsFromShoulder((Customization.Elite.Shoulder)rightShoulderBox.SelectedItem), ref Config.ConfigFile, true);
				Config.SetVariable("Player.Armor.Chest", Customization.Elite.GetPrefsFromChest((Customization.Elite.Chest)chestBox.SelectedItem), ref Config.ConfigFile, true);
			}
			else
			{
				Config.SetVariable("Player.Armor.Helmet", Customization.Spartan.GetPrefsFromHelmet((Customization.Spartan.Helmet)helmetBox.SelectedItem), ref Config.ConfigFile, true);
				Config.SetVariable("Player.Armor.Shoulders", Customization.Spartan.GetPrefsFromShoulder((Customization.Spartan.Shoulder)leftShoulderBox.SelectedItem), ref Config.ConfigFile, true);
				Config.SetVariable("Player.Armor.Arms", Customization.Spartan.GetPrefsFromShoulder((Customization.Spartan.Shoulder)rightShoulderBox.SelectedItem), ref Config.ConfigFile, true);
				Config.SetVariable("Player.Armor.Chest", Customization.Spartan.GetPrefsFromChest((Customization.Spartan.Chest)chestBox.SelectedItem), ref Config.ConfigFile, true);
			}

			Config.SetVariable("Player.Colors.Primary", Customization.GetPrefsFromColor(primaryColor), ref Config.ConfigFile, true);
			Config.SetVariable("Player.Colors.Secondary", Customization.GetPrefsFromColor(secondaryColor), ref Config.ConfigFile, true);
			Config.SetVariable("Player.Colors.Lights", Customization.GetPrefsFromColor(detailColor), ref Config.ConfigFile, true);

			Config.SaveConfigFile("dewrito_prefs.cfg", Config.ConfigFile);

			primaryColorRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(Customization.GetPrefsFromColor(primaryColor)));

			UpdateColorBoxes();

			UpdatePreviewImage();
		}

		private void UpdateConfigAndPreview(object sender, SelectionChangedEventArgs e)
		{
			UpdateConfigAndPreview();
		}

		private void RamdomizeClicked(object sender, RoutedEventArgs e)
		{
			ignoreSelectionChange = true;

			if (selectedRace == Customization.Race.Elite)
			{
				helmetBox.SelectedItem = Customization.Elite.GetRandomHelmet();
				leftShoulderBox.SelectedItem = Customization.Elite.GetRandomShoulder();
				rightShoulderBox.SelectedItem = Customization.Elite.GetRandomShoulder();
				chestBox.SelectedItem = Customization.Elite.GetRandomChest();
			}
			else
			{
				helmetBox.SelectedItem = Customization.Spartan.GetRandomHelmet();
				leftShoulderBox.SelectedItem = Customization.Spartan.GetRandomShoulder();
				rightShoulderBox.SelectedItem = Customization.Spartan.GetRandomShoulder();
				chestBox.SelectedItem = Customization.Spartan.GetRandomChest();
			}

			primaryColor = Customization.GetRandomColor();
			secondaryColor = Customization.GetRandomColor();
			detailColor = Customization.GetRandomColor();

			ignoreSelectionChange = false;

			UpdateConfigAndPreview();
		}

		private void Color_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Rectangle rectangle = sender as Rectangle;
			Customization.Color color = (Customization.Color)Enum.Parse(typeof(Customization.Color), rectangle.Name);

			SolidColorBrush brush = new SolidColorBrush();

			switch(editingColor)
			{
				case 1:
					primaryColor = color;
					break;
				case 2:
					secondaryColor = color;
					break;
				case 3:
					detailColor = color;
					break;
			}

			UpdateConfigAndPreview();

			Storyboard sb = Resources["hideColors"] as Storyboard;
			sb.Begin(colorsMenu);
			editingColor = 0;
		}

		private void primaryColorButton_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (editingColor != 0)
				return;

			editingColor = 1;
			Storyboard sb = Resources["showColors"] as Storyboard;
			sb.Begin(colorsMenu);
		}

		private void secondaryColorButton_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (editingColor != 0)
				return;

			editingColor = 2;
			Storyboard sb = Resources["showColors"] as Storyboard;
			sb.Begin(colorsMenu);
		}

		private void detailColorButton_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (editingColor != 0)
				return;

			editingColor = 3;
			Storyboard sb = Resources["showColors"] as Storyboard;
			sb.Begin(colorsMenu);
		}

		//private void DownloadImages()
		//{
		//	using (WebClient client = new WebClient())
		//	{
		//		foreach (Customization.Spartan.Helmet helmet in Enum.GetValues(typeof(Customization.Spartan.Helmet)))
		//		{
		//			foreach (Customization.Spartan.Shoulder rightShoulder in Enum.GetValues(typeof(Customization.Spartan.Shoulder)))
		//			{
		//				foreach (Customization.Spartan.Shoulder leftShoulder in Enum.GetValues(typeof(Customization.Spartan.Shoulder)))
		//				{
		//					foreach (Customization.Spartan.Chest chest in Enum.GetValues(typeof(Customization.Spartan.Chest)))
		//					{
		//						foreach (Customization.Color primaryColor in Enum.GetValues(typeof(Customization.Color)))
		//						{
		//							foreach (Customization.Color secondaryColor in Enum.GetValues(typeof(Customization.Color)))
		//							{
		//								foreach (Customization.Color detailColor in Enum.GetValues(typeof(Customization.Color)))
		//								{
		//									System.IO.Directory.CreateDirectory(String.Format(@"C:\Users\AlexN\Desktop\temp\0\{0}\{1}\{2}\{3}\{4}\{5}\",
		//		(int)helmet, (int)rightShoulder, (int)leftShoulder, (int)chest, (int)primaryColor, (int)secondaryColor));

		//									client.DownloadFile(new Uri(String.Format(@"https://halo.bungie.net/stats/Halo3/PlayerModel.ashx?p1={0}&p2={1}&p3={2}&p4={3}&p5={4}&p6={5}&p7={6}&p8={7}",
		//		0, (int)helmet, (int)rightShoulder, (int)leftShoulder, (int)chest, (int)primaryColor, (int)secondaryColor, (int)detailColor)),
		//		String.Format(@"C:\Users\AlexN\Desktop\temp\0\{0}\{1}\{2}\{3}\{4}\{5}\{6}.png",
		//		(int)helmet, (int)rightShoulder, (int)leftShoulder, (int)chest, (int)primaryColor, (int)secondaryColor, (int)detailColor));
		//								}
		//							}
		//						}
		//					}
		//				}
		//			}
		//		}
		//	}
		//}
	}
}
