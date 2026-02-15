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
    /// Interaktionslogik für Wissenschaft.xaml
    /// </summary>
    public partial class Wissenschaft : Page
    {
        MainWindow main;
        public Wissenschaft(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Button_Click(sender, e);
        }
        public void plus_Click(object sender, RoutedEventArgs e)
        { 
            main.plus_Click(sender, e);
        }
        public void minus_Click(object sender, RoutedEventArgs e)
        { 
            main.minus_Click(sender, e);
        }

        public void Button_Click_Modulo(object sender, RoutedEventArgs e)
        {
            main.Button_Click_Modulo(sender, e);
        }
        public void mal_Click(object sender, RoutedEventArgs e)
        {
            main.mal_Click(sender, e);
        }
        public void durch_Click(object sender, RoutedEventArgs e)
        {
            main.durch_Click(sender, e);
        }
        public void Button_Click_Gleish(object sender, RoutedEventArgs e)
        {
            main.Button_Click_Gleish(sender, e);
        }
        public void Button_Click_Plus_Minus(object sender, RoutedEventArgs e)
        {
            main.Button_Click_Plus_Minus(sender, e);
        }
        public void Button_Click_Quadrat(object sender, RoutedEventArgs e)
        {
            main.Button_Click_Quadrat(sender, e);
        }
        public void Button_Click_Root(object sender, RoutedEventArgs e)
        {
            main.Button_Click_Root(sender, e);
        }
        public void Button_Click_Kehrwert(object sender, RoutedEventArgs e)
        {
            main.Button_Click_Kehrwert(sender, e);
        }
        public void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            main.Button_Click_Back(sender, e);
        }
        public void Button_Click_EntF(object sender, RoutedEventArgs e)
        {
            main.Button_Click_EntF(sender, e);
        }
        public void Button_Click_Entfernen(object sender, RoutedEventArgs e)
        {
            main.Button_Click_Entfernen(sender, e);
        }
        public void log_Click(object sender, RoutedEventArgs e)
        {
            main.log_Click(sender, e);
        }
        public void zehnHoch_Click(object sender, RoutedEventArgs e)
        {
            main.zehnHoch_Click(sender,e);
        }
        public void eulerscheZahl_Click(object sender, RoutedEventArgs e)
        {
            main.eulerscheZahl_Click(sender, e);
        }
        public void ln_Click(object sender, RoutedEventArgs e)
        {  
            main.ln_Click(sender, e);
        }
        public void cot_Click(object sender, RoutedEventArgs e)
        { 
            main.cot_Click(sender, e);
        }
        public void tan_Click(object sender, RoutedEventArgs e)
        {  
            main.tan_Click(sender, e);
        }
        public void cos_Click(object sender, RoutedEventArgs e)
        {  
            main.cos_Click(sender, e);
        }
        public void sin_Click(object sender, RoutedEventArgs e)
        {  
            main.sin_Click(sender, e);
        }
    }
}
