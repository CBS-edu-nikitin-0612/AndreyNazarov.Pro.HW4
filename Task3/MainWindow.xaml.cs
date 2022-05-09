using System;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;

namespace Task3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            string text = OpenFile(TextBoxPath.Text);
            text = ChengeText(text);
            SaveFile(text, TextBoxPath.Text);
        }
        private string OpenFile(string path)
        {
            StreamReader reader = null;
            try
            {
                FileInfo file = new(path);
                reader = new(file.Open(FileMode.OpenOrCreate));
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                TextBoxStatus.Text = ex.Message;
            }
            finally
            {
                if (reader != null) reader.Close();

            }
            return null;
        }
        private void SaveFile(string text, string path)
        {
            StreamWriter writer = null;
            try
            {
                FileInfo file = new(path);
                writer = new(file.Open(FileMode.Open));
                writer.Write(text);
                TextBoxStatus.Text = "done";
            }
            catch (Exception ex)
            {
                TextBoxStatus.Text = ex.Message;
            }
            finally
            {
                writer.Close();
            }
        }
        private string ChengeText(string text)
        {
            string pattern = @" (?:(?i)без|в|вне|для|до|за|из|к|кроме|между|на|над|о|от|перед|по|под|при|про|ради|сс|сквозь|среди|у|через) ";
            Regex regex = new(pattern);
            return regex.Replace(text, " ГАВ ");
        }
    }
}
