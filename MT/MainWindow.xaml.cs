using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.ComponentModel;

namespace MT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        public MainWindow()
        {
            InitializeComponent();
            CalculateAllCommand = new DelegateCommand(CalculateAll);
            RandomNumber();
            this.DataContext = this;
        }

        public bool Cheker()
        {
            if (tabControl.SelectedIndex == 0 && textBox.Text!="")
            {
                return true;
            }
            else if (tabControl.SelectedIndex == 1 && textBox2.Text != "" )
            {
                return true;
            }
            if (tabControl.SelectedIndex == 2)
            {
                if (textBox1.Text != "" && textBox3.Text!="")
                {
                    return true; 
                }
            }
            return false;
        }

        int gameType = new int();
            int sp = new int();

        public void RandomNumber()
        {
            Random rnd = new Random();
            if (gameType == 1)
            {
                sp = rnd.Next(10, 99);
                answer = rnd.Next(10, 99);
            }
            else if(gameType == 2)
            {
                sp = rnd.Next(100, 999);
                answer = rnd.Next(10, 99);
            }
            else if(gameType == 3)
            {
                sp = rnd.Next(10, 99);
                answer = rnd.Next(100, 999);
            }
            LabelPrinter();
        }

        public void LabelPrinter()
        {
            if (tabControl.SelectedIndex == 0)
            {
                label.Content = (sp + "+" + answer + "=");
                answer = answer + sp;
            }else if(tabControl.SelectedIndex == 1)
            {
                label2.Content = (sp + "-" + answer + "=");
                answer = sp - answer;
            }if (tabControl.SelectedIndex == 2)
            {
                if(textBox1.Text != "")
                {
                    answer = sp * int.Parse(textBox1.Text);
                    label4.Content = (sp + "$ * "+ int.Parse(textBox1.Text));
                }
            }
        }


        private int answer = new int();

        public event PropertyChangedEventHandler PropertyChanged;

        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //        if (tabControl.SelectedIndex == 0 && answer == int.Parse(textBox.Text))
        //        {
        //            label1.Content = "Правильно";
        //        }
        //        else
        //        {
        //            label1.Content = "Не правильно";
        //        }
        //        if (tabControl.SelectedIndex == 1 && answer == int.Parse(textBox2.Text))
        //        {
        //            label3.Content = "Правильно";
        //        }
        //        else
        //        {
        //            label3.Content = "Не правильно";
        //        }
        //        if (tabControl.SelectedIndex == 2 && answer == int.Parse(textBox3.Text))
        //        {
        //            label5.Content = "Правильно";
        //        }
        //        else
        //        {
        //            label5.Content = "Не правильно";
        //        }
        //    RandomNumber();
        //    textBox.Text = "";
        //    textBox2.Text = "";
        //    textBox3.Text = "";
        //}


        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            gameType = 1;
            RandomNumber();
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            
            gameType = 2;
            RandomNumber();
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            gameType = 3;
            RandomNumber();
        }


        public ICommand CalculateAllCommand { get; }

        public int Answer
        {
            get
            {
                return _answer;
            }

            set
            {
                if (answer != value)
                {
                    _answer = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer)));
                }
            }   
        }

        private void CalculateAll()
        {
            if (tabControl.SelectedIndex == 0 && answer == Answer)
            {
                label1.Content = "Правильно";
                Answer = 0;
                textBox.SelectAll();
            }
            else
            {
                label1.Content = "Не правильно";
                Answer = 0;
                textBox.SelectAll();
            }
            if (tabControl.SelectedIndex == 1 && answer == int.Parse(textBox2.Text))
            {
                label3.Content = "Правильно";
                Answer = 0;
                textBox2.SelectAll();
            }
            else
            {
                label3.Content = "Не правильно";
                Answer = 0;
                textBox2.SelectAll();
            }
            if (tabControl.SelectedIndex == 2 && answer == int.Parse(textBox3.Text))
            {
                label5.Content = "Правильно";
                Answer = 0;
                textBox3.SelectAll();
            }
            else
            {
                label5.Content = "Не правильно";
                Answer = 0;
                textBox3.SelectAll();
            }
            RandomNumber();
            
        }
        private int _answer;

        private void ViewModel_Loaded(object sender, RoutedEventArgs e)
        {
            RandomNumber();
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            RandomNumber();
        }

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            RandomNumber();
        }

        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            RandomNumber();
        }
    }
}
