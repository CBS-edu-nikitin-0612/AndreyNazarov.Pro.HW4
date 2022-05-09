using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Task2
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

        private void ButtonParse_Click(object sender, RoutedEventArgs e)
        {
            string page = GetPage(TextBoxPath.Text);
            ParseLinks(page);
            ParsePhoneNumbers(page);
            ParseEmails(page);
        }

        private string GetPage(string url)
        {
            string result = "";
            StreamReader reader = null;
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                reader = new(stream);
                result = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                TextBoxStutus.Text = e.Message;
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            return result;
        }
        private void ParseLinks(string page)
        {
            const string pattern = @"https?://[\w\W]*?(?:net|ru|org)";
            Regex regex = new Regex(pattern);
            foreach (Match item in regex.Matches(page))
            {
                ListBoxLinks.Items.Add(item.Value);
            }
        }
        private void ParsePhoneNumbers(string page)
        {
            const string pattern = @"\+\d\(?\d{3}\)?\D?\d{3}\D?\d{2}\D?\d{2}";
            Regex regex = new Regex(pattern);
            foreach (Match item in regex.Matches(page))
            {
                ListBoxPhoneNumbers.Items.Add(item.Value);
            }
        }
        private void ParseEmails(string page)
        {
            const string pattern = @"(?:\w+)@\w+.\w+";
            Regex regex = new Regex(pattern);
            foreach (Match item in regex.Matches(page))
            {
                ListBoxEmails.Items.Add(item.Value);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = null;
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                FileInfo file = new(path + @"\data.txt");
                Stream stream = file.Open(FileMode.OpenOrCreate);
                streamWriter = new(stream);
                streamWriter.WriteLine("Ссылки:");
                streamWriter.Write(String.Join("\n", ListBoxLinks.Items.Cast<object>()));
                streamWriter.WriteLine("\nТелефоны:");
                streamWriter.Write(String.Join("\n", ListBoxPhoneNumbers.Items.Cast<object>()));
                streamWriter.WriteLine("\nАдреса:");
                streamWriter.Write(String.Join("\n", ListBoxEmails.Items.Cast<object>()));
                TextBoxStutus.Text = "Данные сохранены";
            }
            catch (Exception ex)
            {
                TextBoxStutus.Text = ex.Message;
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
            }
        }
    }
}
