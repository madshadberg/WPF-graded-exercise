using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace WPF_graded_exercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public ObservableCollection<User> users { get; }

        public MainWindow()
        {
            InitializeComponent();
            users = new ObservableCollection<User>();
            //Testing
            users.Add(new User("123;Testing;2;3"));
            userBox.ItemsSource = users;
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\"; <- default
            // only text files or CSV files
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
            // text file by default
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == true)
            {
                try { 

                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    
                        using (myStream)
                        {
                            string[] text = File.ReadAllLines(openFileDialog1.FileName);
                            foreach (string str in text)
                            {
                                    users.Add(new User(str));
                                    userBox.ItemsSource = users;
                            }
                                int numberOfUsers = users.Count;
                                labelStatusBar.Content = "Number of lines in the listbox: " + numberOfUsers + ". Last time loaded " + DateTime.Now;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void userBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userBox.SelectedIndex == -1) return;
            idTextBlock.DataContext = userBox.SelectedItem;
            nameTextBlock.DataContext = userBox.SelectedItem;
            ageTextBlock.DataContext = userBox.SelectedItem;
            scoreTextBlock.DataContext = userBox.SelectedItem;
        }
    }

    public class User
    {
        public User(string data)
        {
            var str = data.Split(';');
            //id;name;age;score
            Id = int.Parse(str[0]);
            Name = str[1];
            Age = int.Parse(str[2]);
            Score = int.Parse(str[3]);

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
        public override string ToString()
        {
            return string.Format("Name: " + Name + ", Id: " + Id);
        }
    }
}
