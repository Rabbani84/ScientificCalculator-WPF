using Rechner.Core;
using System.Diagnostics;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string message = "!!!";
        string op;
        double op1;
        double op2;
        int iOp = 0;
        string ausgsbe;
        private bool TryGetCurrentNumber(out double x)
        {
            x = 0;

            // Prefer current input (ergebnis) then tb
            var text = !string.IsNullOrWhiteSpace(ergebnis.Text) && ergebnis.Text != message
                ? ergebnis.Text
                : tb.Text;

            return CalculatorEngine.TryParseNumber(text, out x);
        }

        private void AddToHistory(string expr, string valueText)
        {
            lb.Items.Add(expr + "\n" + valueText);
        }


        public MainWindow()
        {
            InitializeComponent();
            
            // HauptFenster.NavigationService.Navigate(new Wissenschaft(this));

            HauptFenster.NavigationService.Navigate(new Standard(this));
            woBinIch.Text = "\"Standrad\"";
            
        }
        public void stand_Click(object sender, RoutedEventArgs e)
        {
            HauptFenster.NavigationService.Navigate(new Standard(this));
            woBinIch.Text = "\"Standrad\"";
        }

        public void wissen_Click(object sender, RoutedEventArgs e)
        {
            HauptFenster.NavigationService.Navigate(new Wissenschaft(this));
            woBinIch.Text = "\"Wissenschaft\"";
        }

        private void gleichung_Click(object sender, RoutedEventArgs e)
        {
            HauptFenster.NavigationService.Navigate(new Gleichung(this));
            woBinIch.Text = "\"Gleichung\"";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            HauptFenster.NavigationService.Navigate(new Gardanische_Gleichung(this));
            woBinIch.Text = "\"Gleichung\"";
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            
            if (tb.Text.Contains("=") || ergebnis.Text.Contains("!!!"))
            {
                tb.Text = string.Empty;
                ergebnis.Text = string.Empty;    
            }
            ergebnis.Text += button.Content.ToString();
            
        }

        public void plus_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = ergebnis.Text + "+";
            ergebnis.Text = string.Empty;
        }

        public void minus_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = ergebnis.Text + "-";
            ergebnis.Text = string.Empty;
        }

        public void mal_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = ergebnis.Text + "x";
            ergebnis.Text = string.Empty;
        }

        public void durch_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = ergebnis.Text + "÷";
            ergebnis.Text = string.Empty;
        }

        public void Button_Click_Gleish(object sender, RoutedEventArgs e)
        {
            if (tb.Text.Contains("="))
            {
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                
            }
            else
            {
                tb.Text += ergebnis.Text;
                ergebnis.Text = string.Empty;
            }


            try
            {
                // build full expression text
                if (!tb.Text.Contains("="))
                    tb.Text += ergebnis.Text;

                // detect operator position (same logic but cleaner later)
                if (tb.Text.Contains("+")) iOp = tb.Text.IndexOf("+", 1);
                else if (tb.Text.Contains("-")) iOp = tb.Text.IndexOf("-", 1);
                else if (tb.Text.Contains("x")) iOp = tb.Text.IndexOf("x", 1);
                else if (tb.Text.Contains("÷")) iOp = tb.Text.IndexOf("÷", 1);
                else { message = "Error"; ergebnis.Text = message; return; }

                op = tb.Text.Substring(iOp, 1);

                var leftText = tb.Text.Substring(0, iOp);
                var rightText = tb.Text.Substring(iOp + 1);

                if (!CalculatorEngine.TryParseNumber(leftText, out op1) ||
                    !CalculatorEngine.TryParseNumber(rightText, out op2))
                {
                    message = "Error";
                    ergebnis.Text = message;
                    return;
                }

                BinaryOp bop = op switch
                {
                    "+" => BinaryOp.Add,
                    "-" => BinaryOp.Subtract,
                    "x" => BinaryOp.Multiply,
                    "÷" => BinaryOp.Divide,
                    _ => BinaryOp.Add
                };

                var result = CalculatorEngine.Evaluate(bop, op1, op2);

                if (!result.Ok)
                {
                    message = result.Error ?? "Error";
                    ergebnis.Text = message;
                    tb.Text = result.Expression;
                    AddToHistory(tb.Text, ergebnis.Text);
                    return;
                }

                tb.Text = result.Expression + " ";
                ergebnis.Text = CalculatorEngine.FormatResult(result.Value);

                AddToHistory(tb.Text, ergebnis.Text);
            }
            catch
            {
                message = "Error";
                ergebnis.Text = message;
            }


        }

        

        public void Button_Click_Plus_Minus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(ergebnis.Text) > 0)
                {
                    ergebnis.Text = $"{Convert.ToDouble(ergebnis.Text) * -1}";
                }
                else 
                {
                    ergebnis.Text = $"{Math.Abs(Convert.ToDouble(ergebnis.Text))}";
                }
                if(ergebnis.Text == string.Empty)
                {
                    ergebnis.Text = "-";
                }
            }
            catch 
            {
                message = "Error !!!";
                ergebnis.Text = message;
            }
        }

        public void Button_Click_Quadrat(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                ergebnis.Text = $"{Math.Pow(Convert.ToDouble(tb.Text),2)}";
                tb.Text = $"sqr( {Convert.ToDouble(tb.Text)} ) = ";
                lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
            }
            catch  
            {
                message = "Error !!!";
                ergebnis.Text = message;
            }

        }

        public void Button_Click_Root(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!TryGetCurrentNumber(out var x)) { ergebnis.Text = "Error"; return; }

                var r = CalculatorEngine.Evaluate(UnaryOp.Sqrt, x);

                tb.Text = r.Expression + " ";
                if (!r.Ok)
                {
                    ergebnis.Text = r.Error ?? "Error";
                }
                else
                {
                    // if negative: we show imaginary indicator like before
                    ergebnis.Text = (x < 0)
                        ? $"{CalculatorEngine.FormatResult(r.Value)}i"
                        : CalculatorEngine.FormatResult(r.Value);
                }

                AddToHistory(tb.Text, ergebnis.Text);
            }
            catch { ergebnis.Text = "Error"; }
        }

        static Complex ComplexSquareRoot(double num)
        {
            double realPart = Math.Sqrt(Math.Abs(num));
            double imaginaryPart = Math.Sqrt(Math.Abs(num)) * Math.Sign(num);
            
            return new Complex(realPart, imaginaryPart);

        }

        public struct Complex
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }

            public Complex(double real,double imaginary)
            {
                Real = real;
                Imaginary = imaginary;
            }
        }

        public void Button_Click_Kehrwert(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text; 
                }
                if (Convert.ToDouble(tb.Text) != 0)
                        {
                    ergebnis.Text = $"{1 / Convert.ToDouble(tb.Text)}";
                    tb.Text = $"1/({tb.Text}) =";
                    lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
                }
                else
                {
                    message = "Teilen durch 0 nicht möglich !!!";
                    ergebnis.Text = message;
                }
                
            }
            catch  
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        public void Button_Click_Back(object sender, RoutedEventArgs e)
        {
           
            if(ergebnis.Text.Length > 0) 
            {
                ergebnis.Text = ergebnis.Text.Substring(0, ergebnis.Text.Length - 1);
            }
        }

        public void Button_Click_EntF(object sender, RoutedEventArgs e)
        {
            tb.Clear();
            ergebnis.Text = null;
        }

        public void Button_Click_Entfernen(object sender, RoutedEventArgs e)
        {
            tb.Clear();
            ergebnis.Text = null;
        }

        public void Button_Click_Modulo(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                
                ergebnis.Text = $"{Convert.ToDouble(tb.Text) / 100}";
                tb.Text = $"{tb.Text} % = ";
                lb.Items.Add(tb.Text + "\n" + ergebnis.Text);


            }
            catch 
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        
        

        public void log_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                if (Convert.ToDouble(tb.Text) > 0)
                {
                    ergebnis.Text = $"{Math.Log10(Convert.ToDouble(tb.Text))}";
                    tb.Text = $"log( {tb.Text} ) = ";
                    lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
                }
                else
                {
                    message = "Ungültige Eingabe !!!";
                    ergebnis.Text = message;
                }

            }
            catch 
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        public void zehnHoch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                 ergebnis.Text = $"{Math.Pow(10,Convert.ToDouble(tb.Text))}";
                tb.Text = $"10^( {Convert.ToDouble(tb.Text)} ) = ";
                lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
            }
            catch 
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        public void eulerscheZahl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                ergebnis.Text = $"{Math.Pow(2.7182818, Convert.ToDouble(tb.Text))}";
                tb.Text = $"e^( {Convert.ToDouble(tb.Text)} ) = ";
                lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
            }
            catch 
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        public void ln_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                if (Convert.ToDouble(tb.Text) > 0)
                {
                    ergebnis.Text = $"{Math.Log(Convert.ToDouble(tb.Text))}";
                    tb.Text = $"ln( {tb.Text} ) = ";
                    lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
                }
                else
                {
                    message = "Ungültige Eingabe !!! ";
                    ergebnis.Text = message;
                }

            }
            catch 
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        public void cot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                double degrees = Math.Abs(Convert.ToDouble(tb.Text));
                double radians = degrees * (Math.PI / 180);
                ergebnis.Text = $"{Math.Round(1/(Math.Tan(radians)),1)}";
                tb.Text = $"cot( {Convert.ToDouble(tb.Text)} ) = ";
                lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
            }
            catch 
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        public void tan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                double degrees = Math.Abs(Convert.ToDouble(tb.Text));
                double radians = degrees * (Math.PI / 180);
                ergebnis.Text = $"{Math.Round(Math.Tan(radians), 1)}";
                tb.Text = $"tan( {Convert.ToDouble(tb.Text)} ) = ";
                lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
            }
            catch 
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        public void cos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ergebnis.Text != string.Empty && ergebnis.Text != message)
                {
                    tb.Text = ergebnis.Text;
                }
                double degrees = Math.Abs(Convert.ToDouble(tb.Text));
                double radians = degrees * (Math.PI / 180);
                ergebnis.Text = $"{Math.Round(Math.Cos(radians),1)}";
                tb.Text = $"cos( {Convert.ToDouble(tb.Text)} ) = ";
                lb.Items.Add(tb.Text + "\n" + ergebnis.Text);
            }
            catch 
            {
                message = "Error  !!!";
                ergebnis.Text = message;
            }
        }

        public void sin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!TryGetCurrentNumber(out var x)) { ergebnis.Text = "Error"; return; }

                var r = CalculatorEngine.Evaluate(UnaryOp.SinDeg, Math.Abs(x));
                if (!r.Ok) { ergebnis.Text = r.Error ?? "Error"; return; }

                tb.Text = r.Expression + " ";
                ergebnis.Text = CalculatorEngine.FormatResult(r.Value);
                AddToHistory(tb.Text, ergebnis.Text);
            }
            catch { ergebnis.Text = "Error"; }
        }

        public void lb_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        
    }
}