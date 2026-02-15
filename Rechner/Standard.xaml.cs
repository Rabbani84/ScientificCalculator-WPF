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

namespace Rechner
{
    /// <summary>
    /// Interaktionslogik für Standard.xaml
    /// </summary>
    public partial class Standard : Page
    {
        MainWindow mw;
        public Standard(MainWindow mw)
        {
            this.mw = mw;   
            InitializeComponent();
            
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            mw.Button_Click(sender, e);
        }
        public void plus_Click(object sender, RoutedEventArgs e)
        {
            mw.plus_Click(sender, e);
        }
        public void minus_Click(object sender, RoutedEventArgs e)
        {
            mw.minus_Click(sender, e);
        }
        public void mal_Click(object sender, RoutedEventArgs e)
        {
            mw.mal_Click(sender, e);
        }
        public void durch_Click(object sender, RoutedEventArgs e)
        {
            mw.durch_Click(sender, e);
        }
        public void Button_Click_Gleish(object sender, RoutedEventArgs e)
        {
            mw.Button_Click_Gleish(sender, e);
        }
        public void Button_Click_Plus_Minus(object sender, RoutedEventArgs e)
        {
            mw.Button_Click_Plus_Minus(sender, e);
        }
        public void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            mw.Button_Click_Back(sender, e);
        }
        public void Button_Click_EntF(object sender, RoutedEventArgs e)
        {
            mw.Button_Click_EntF(sender, e);
        }
        public void Button_Click_Entfernen(object sender, RoutedEventArgs e)
        {
            mw.Button_Click_Entfernen(sender, e);
        }
        private void Button_Click_Modulo(object sender, RoutedEventArgs e)
        {
            mw.Button_Click_Modulo(sender, e);
        }
    }
}
