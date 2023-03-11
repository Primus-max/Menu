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
using System.Drawing;
using System.Drawing.Imaging;
using Brushes = System.Windows.Media.Brushes;
using System.IO;
using System.Globalization;
using Menu.ViewModels;
using System.Threading;
using Newtonsoft.Json;
using System.Configuration;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Image = System.Windows.Controls.Image;

namespace Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime dateTime = DateTime.Now;


        public MainWindow()
        {
            InitializeComponent();
            MenuDate.Text = dateTime.ToString();
            this.SizeChanged += ResizeMainWin_Resize;


            Dictionary<string, string[]> dictionaryWords = new Dictionary<string, string[]>
            {
                ["А"] = new[] { "Абломись", "Ant", "Action" },
                ["B"] = new[] { "Boy", "Book", "Ball" },
                ["C"] = new[] { "Cat", "Call", "City" }
            };


            // конвертирую в JSON
            string json = JsonConvert.SerializeObject(dictionaryWords);
            // записываю в файл
            File.WriteAllText(@"dictionary.json", json);

            //Считываем данные из файла dictionary.json
            string jsonString = File.ReadAllText(@"dictionary.json");
            // Десериализуем строку в тип Dictionary
            Dictionary<string, string[]>? deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonString);



        }


        public void Picture()
        {
            var window = Application.Current.MainWindow;


            RenderTargetBitmap bmp = new RenderTargetBitmap((int)window.ActualWidth, (int)window.ActualHeight, 96, 96, PixelFormats.Default);
            bmp.Render(window);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (FileStream stream = new FileStream("Z:\\image.jpg", FileMode.Create))
            {
                encoder.Save(stream);
            }

            //// Get the text from the text fields
            //string text1 = textField1.Text;
            //string text2 = textField2.Text;

            //// Create a Bitmap object with the desired width and height
            //Bitmap bmp = new Bitmap(500, 500);

            //// Create a Graphics object from the Bitmap
            //Graphics g = Graphics.FromImage(bmp);

            //Font font;


            //// Draw the text to the Bitmap
            //// g.DrawString(text1, font, Brushes.Black, 0, 20);
            //// g.DrawString(text2, font, Brushes.Black, 0, 20);

            //// Save the Bitmap as an image file
            //bmp.Save("Z:\\image.jpg", ImageFormat.Jpeg);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Достаю настройки
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            //применяю настройки
            ChiefTextBox.Text = settings["Chief"].Value;
            CookedBreakfast.Text = settings["CoockedBreakfast"].Value;
            CookedDinner.Text = settings["CoockedDinner"].Value;
            CookedSupper.Text = settings["CoockedSupper"].Value;

            MenuTextMinMaxBreakFast.Text = settings["MenuTextMinMaxBreakFast"].Value;
            MenuTextAddPortionBreakFast.Text = settings["MenuTextAddPortionBreakFast"].Value;
            MenuTextMinMaxDinner.Text = settings["MenuTextMinMaxDinner"].Value;
            MenuTextAddPortionDinner.Text = settings["MenuTextAddPortionDinner"].Value;
            MenuTextMinMaxSupper.Text = settings["MenuTextMinMaxSupper"].Value;
            MenuTextAddPortionSupper.Text = settings["MenuTextAddPortionSupper"].Value;
            CaloriesMinOfBreakfastText.Text = settings["CaloriesMinOfBreakfastText"].Value;
            CaloriesMaxOfBreakfastText.Text = settings["CaloriesMaxOfBreakfastText"].Value;
            CaloriesMinOfDinnerText.Text = settings["CaloriesMinOfDinnerText"].Value;
            CaloriesMaxOfDinnerText.Text = settings["CaloriesMaxOfDinnerText"].Value;
            CaloriesMinOfSupperText.Text = settings["CaloriesMinOfSupperText"].Value;
            CaloriesMaxOfSupperText.Text = settings["CaloriesMaxOfSupperText"].Value;
            WeightDishesMinOfBreakfastText.Text = settings["WeightDishesMinOfBreakfastText"].Value;
            WeightDishesMaxOfBreakfastText.Text = settings["WeightDishesMaxOfBreakfastText"].Value;
            WeightDishesMinOfDinnerText.Text = settings["WeightDishesMinOfDinnerText"].Value;
            WeightDishesMaxOfDinnerText.Text = settings["WeightDishesMaxOfDinnerText"].Value;
            WeightDishesMinOfSupperText.Text = settings["WeightDishesMinOfSupperText"].Value;
            WeightDishesMaxOfSupperText.Text = settings["WeightDishesMaxOfSupperText"].Value;
        }

        public void AddWordAtDictionary(List<string> strings)
        {
            /* Dictionary<string, string[]> dictionaryWords = new Dictionary<string, string[]>
             {
                 ["А"] = new[] { "Apple", "Ant", "Action" },
                 ["B"] = new[] { "Boy", "Book", "Ball" },
                 ["C"] = new[] { "Cat", "Call", "City" }
             };*/
            // конвертирую в JSON
            //string json = JsonConvert.SerializeObject(dictionaryWords);
            // записываю в файл
            //File.WriteAllText(@"dictionary.json", json);
        }

        public void SortingWords()
        {
            List<string> listWords = new List<string>();
            listWords.Add(MenuTextMinMaxBreakFast.Text +
                MenuTextAddPortionBreakFast.Text +
                MenuTextMinMaxDinner.Text +
                MenuTextAddPortionDinner.Text +
                MenuTextMinMaxSupper.Text +
                MenuTextAddPortionSupper.Text);


            foreach (string word in listWords)
            {

                //Считываем данные из файла dictionary.json
                string jsonString = File.ReadAllText(@"dictionary.json");
                // Десериализуем строку в тип Dictionary
                Dictionary<string, string[]>? deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonString);
                //AddWordAtDictionary(""[]);

                var splittedString = word.Split(" ");

                foreach (var item in splittedString)
                {
                    if (item == "") continue;

                    if (item.Contains("\r\n"))
                    {
                        var subSplittedStr = item.Replace("\r\n", " ").Split(" ");

                        foreach (var subWord in subSplittedStr)
                        {
                            if (item == "") continue;

                            string firstLetter_Key = subWord[0].ToString().ToUpper();
                            string[] strings = deserializedDictionary[firstLetter_Key];


                            foreach (var itemDic in strings)
                            {
                                if (itemDic.ToLower() != subWord.ToLower())
                                {
                                    //deserializedDictionary.TryAdd(firstLetter_Key, subWord);
                                    // здесь надо записать это слово в Dictionary
                                }
                            }
                        }
                    }

                }

            }
        }

        private void ResizeMainWin_Resize(object sender, SizeChangedEventArgs e)
        {
            GridParent.Height = Application.Current.MainWindow.ActualHeight - 150;
            var baseSize = Application.Current.MainWindow.ActualHeight;

            #region Заголовки колонок
            ColumnHeader1.FontSize = baseSize / 35;
            ColumnHeader2.FontSize = baseSize / 35;
            ColumnHeader3.FontSize = baseSize / 35;
            ColumnHeader4.FontSize = baseSize / 35;
            ColumnHeader5.FontSize = baseSize / 35;
            #endregion

            #region Время приема пищи
            Breakfast.FontSize = baseSize / 25;
            Dinner.FontSize = baseSize / 25;
            Supper.FontSize = baseSize / 25;
            #endregion

            #region Блюда

            #region Завтрак
            MinMaxPortionBreakfaste.FontSize = baseSize / 32;
            AddPortionBreakfaste.FontSize = baseSize / 32;
            MenuTextMinMaxBreakFast.FontSize = baseSize / 36;
            MenuTextAddPortionBreakFast.FontSize = baseSize / 36;
            #endregion

            #region Обед
            MinMaxPortionDinner.FontSize = baseSize / 32;
            AddPortionDinner.FontSize = baseSize / 32;
            MenuTextMinMaxDinner.FontSize = baseSize / 36;
            MenuTextAddPortionDinner.FontSize = baseSize / 36;
            #endregion

            #region  Ужин
            MinMaxPortionSupper.FontSize = baseSize / 32;
            AddPortionSupper.FontSize = baseSize / 32;
            MenuTextMinMaxSupper.FontSize = baseSize / 36;
            MenuTextAddPortionSupper.FontSize = baseSize / 36;
            #endregion


            #region Выход блюд завтрак
            CaloriesMinOfBreakfast.FontSize = baseSize / 32;
            CaloriesMaxOfBreakfast.FontSize = baseSize / 32;
            CaloriesMaxOfBreakfastText.FontSize = baseSize / 38;
            CaloriesMinOfBreakfastText.FontSize = baseSize / 38;
            #endregion


            #region Выход блюд обед
            CaloriesMinOfDinner.FontSize = baseSize / 32;
            CaloriesMaxOfDinner.FontSize = baseSize / 32;
            CaloriesMaxOfDinnerText.FontSize = baseSize / 38;
            CaloriesMinOfDinnerText.FontSize = baseSize / 38;
            #endregion


            #region Выход блюд ужин
            CaloriesMinOfSupper.FontSize = baseSize / 32;
            CaloriesMaxOfSupper.FontSize = baseSize / 32;
            CaloriesMaxOfSupperText.FontSize = baseSize / 38;
            CaloriesMinOfSupperText.FontSize = baseSize / 38;
            #endregion


            #region Масса порций завтрак
            WeightDishesMaxOfBreakfast.FontSize = baseSize / 32;
            WeightDishesMinOfBreakfast.FontSize = baseSize / 32;
            WeightDishesMaxOfBreakfastText.FontSize = baseSize / 38;
            WeightDishesMinOfBreakfastText.FontSize = baseSize / 38;
            #endregion

            #region Масса порций обед
            WeightDishesMaxOfDinner.FontSize = baseSize / 32;
            WeightDishesMinOfDinner.FontSize = baseSize / 32;
            WeightDishesMaxOfDinnerText.FontSize = baseSize / 38;
            WeightDishesMinOfDinnerText.FontSize = baseSize / 38;
            #endregion

            #region Масса порций ужин
            WeightDishesMaxOfSupper.FontSize = baseSize / 32;
            WeightDishesMinOfSupper.FontSize = baseSize / 32;
            WeightDishesMaxOfSupperText.FontSize = baseSize / 38;
            WeightDishesMinOfSupperText.FontSize = baseSize / 38;
            #endregion


            #region Кто готовил
            CookedBreakfast.FontSize = baseSize / 30;
            CookedDinner.FontSize = baseSize / 30;
            CookedSupper.FontSize = baseSize / 30;
            #endregion

            #endregion
        }

        public void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;

                if (settings[key] != null)
                {
                    settings.Remove(key);
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }


        public void SaveSettimgs()
        {
            AddUpdateAppSettings("CoockedBreakfast", CookedBreakfast.Text);
            AddUpdateAppSettings("CoockedDinner", CookedDinner.Text);
            AddUpdateAppSettings("CoockedSupper", CookedSupper.Text);
            AddUpdateAppSettings("Chief", ChiefTextBox.Text);

            AddUpdateAppSettings("MenuTextMinMaxBreakFast", MenuTextMinMaxBreakFast.Text);
            AddUpdateAppSettings("MenuTextAddPortionBreakFast", MenuTextAddPortionBreakFast.Text);
            AddUpdateAppSettings("MenuTextMinMaxDinner", MenuTextMinMaxDinner.Text);
            AddUpdateAppSettings("MenuTextAddPortionDinner", MenuTextAddPortionDinner.Text);
            AddUpdateAppSettings("MenuTextMinMaxSupper", MenuTextMinMaxSupper.Text);
            AddUpdateAppSettings("MenuTextAddPortionSupper", MenuTextAddPortionSupper.Text);
            AddUpdateAppSettings("CaloriesMinOfBreakfastText", CaloriesMinOfBreakfastText.Text);
            AddUpdateAppSettings("CaloriesMaxOfBreakfastText", CaloriesMaxOfBreakfastText.Text);
            AddUpdateAppSettings("CaloriesMinOfDinnerText", CaloriesMinOfDinnerText.Text);
            AddUpdateAppSettings("CaloriesMaxOfDinnerText", CaloriesMaxOfDinnerText.Text);
            AddUpdateAppSettings("CaloriesMinOfSupperText", CaloriesMinOfSupperText.Text);
            AddUpdateAppSettings("CaloriesMaxOfSupperText", CaloriesMaxOfSupperText.Text);
            AddUpdateAppSettings("WeightDishesMinOfBreakfastText", WeightDishesMinOfBreakfastText.Text);
            AddUpdateAppSettings("WeightDishesMaxOfBreakfastText", WeightDishesMaxOfBreakfastText.Text);
            AddUpdateAppSettings("WeightDishesMinOfDinnerText", WeightDishesMinOfDinnerText.Text);
            AddUpdateAppSettings("WeightDishesMaxOfDinnerText", WeightDishesMaxOfDinnerText.Text);
            AddUpdateAppSettings("WeightDishesMinOfSupperText", WeightDishesMinOfSupperText.Text);
            AddUpdateAppSettings("WeightDishesMaxOfSupperText", WeightDishesMaxOfSupperText.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var window = Application.Current.MainWindow;

            DateTime date = (DateTime)MenuDate.SelectedDate;
            string fileName = date.ToShortDateString();

            SaveMenuPanel.Visibility = Visibility.Collapsed;


            SaveSettimgs();
            Thread.Sleep(500);

            //int widthImg = (int)window.ActualWidth;
            //int heightImg = (int)window.ActualHeight;

            //RenderTargetBitmap bmp = new RenderTargetBitmap(widthImg, heightImg, 96, 96, PixelFormats.Default);
            //bmp.Render(window);


            //PngBitmapEncoder encoder = new PngBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bmp));


            //int screenWidth = 1920; // Set the width of the TV screen
            //int screenHeight = 1080; // Set the height of the TV screen
            //double dpi = 96; // Set the desired DPI of the image
            //RenderTargetBitmap bmp = new RenderTargetBitmap((int)window.ActualWidth, (int)window.ActualHeight, dpi, dpi, PixelFormats.Default);
            //bmp.Render(window);

            //double widthRatio = (double)screenWidth / bmp.PixelWidth;
            //double heightRatio = (double)screenHeight / bmp.PixelHeight;
            //double ratio = Math.Min(widthRatio, heightRatio);

            //int newWidth = (int)(bmp.PixelWidth * ratio);
            //int newHeight = (int)(bmp.PixelHeight * ratio);

            //BitmapSource resizedBmp = new TransformedBitmap(bmp, new ScaleTransform(ratio, ratio));
            //PngBitmapEncoder encoder = new PngBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(resizedBmp));


            int screenWidth = 1920;
            int screenHeight = 1080;

            double dpi = 300;
            double scale = dpi / 96;

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(RenderGrid.ActualWidth * scale), (int)(RenderGrid.ActualHeight * scale), dpi, dpi, PixelFormats.Default);
            bmp.Render(RenderGrid);

            double padding = 0.05; // Этот параметр можно крутить для увеличения размера изображения
            double widthRatio = (double)screenWidth / bmp.PixelWidth;
            double heightRatio = (double)screenHeight / bmp.PixelHeight;
            double ratio = Math.Max(widthRatio, heightRatio) * (1 + padding);

            Transform resizeTransform = new ScaleTransform(ratio, ratio);
            if (widthRatio > heightRatio)
            {
                double translate = (screenHeight - bmp.PixelHeight * ratio) / 2;
                resizeTransform = new TransformGroup()
                {
                    Children = new TransformCollection()
        {
            new ScaleTransform(ratio, ratio),
            new TranslateTransform(0, translate)
        }
                };
            }
            else if (heightRatio > widthRatio)
            {
                double translate = (screenWidth - bmp.PixelWidth * ratio) / 2;
                resizeTransform = new TransformGroup()
                {
                    Children = new TransformCollection()
        {
            new ScaleTransform(ratio, ratio),
            new TranslateTransform(translate, 0)
        }
                };
            }

            BitmapSource resizedBmp = new TransformedBitmap(bmp, resizeTransform);

            Canvas canvas = new Canvas();
            canvas.Width = screenWidth;
            canvas.Height = screenHeight;

            Image image = new Image();
            image.Source = resizedBmp;
            image.Width = screenWidth;
            image.Height = screenHeight;

            canvas.Children.Add(image);

            string basePath = "";

            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                basePath = commonOpenFileDialog.FileName;

            string fullPAth = new StringBuilder(basePath + "\\" + fileName + ".png").ToString();

            using (FileStream stream = new FileStream(fullPAth, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(resizedBmp));
                encoder.Save(stream);
            }


            SaveMenuPanel.Visibility = Visibility.Visible;
            MessageBox.Show("Нормуль, все сохранил!");
        }
    }
}
