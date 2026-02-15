using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
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
    /// Interaktionslogik für Gleichung.xaml
    /// </summary>
    public partial class Gleichung : Page
    {
        MainWindow mw1;

        //TextBox tb = Application.Current.MainWindow.FindName("tb") as TextBox;
        //TextBlock ergebnis = Application.Current.MainWindow.FindName("ergebnis") as TextBlock;
        //ListBox lb = Application.Current.MainWindow.FindName("lb") as ListBox;
        public Gleichung(MainWindow mw1)
        {
            InitializeComponent();
            this.mw1 = mw1;
        }

        public void eintragen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = ersteZ.Value ?? 0;
                int b = zweiZ.Value ?? 0;
                int c = dreiZ.Value ?? 0;

                // Validation 1: a darf nicht 0 sein
                if (a == 0)
                {
                    mw1.tb.Text = $"({a})X²+({b})X+({c})=0";
                    mw1.ergebnis.Text = "Ungültige Eingabe: a darf nicht 0 sein!";
                    mw1.lb.Items.Add(mw1.tb.Text + "\n" + mw1.ergebnis.Text);
                    return;
                }

                double d = Math.Pow(b, 2) - (4 * a * c);

                mw1.tb.Text = $"({a})X²+({b})X+({c})=0";

                double x1, x2;

                if (d > 0)
                {
                    x1 = (-b + Math.Sqrt(d)) / (2 * a);
                    x2 = (-b - Math.Sqrt(d)) / (2 * a);

                    mw1.ergebnis.Text = $"X1 = {x1:F4} , X2 = {x2:F4}";
                }
                else if (d == 0)
                {
                    x1 = -b / (2.0 * a);
                    mw1.ergebnis.Text = $"Doppelte Lösung: X = {x1:F4}";
                }
                else
                {
                    // komplexe Lösung
                    double realPart = -b / (2.0 * a);
                    double imagPart = Math.Sqrt(Math.Abs(d)) / (2.0 * a);

                    mw1.ergebnis.Text =
                        $"Komplexe Lösungen:\n" +
                        $"X1 = {realPart:F4} + {imagPart:F4}i\n" +
                        $"X2 = {realPart:F4} - {imagPart:F4}i";
                }

                mw1.lb.Items.Add(mw1.tb.Text + "\n" + mw1.ergebnis.Text);

                // -------- Plot --------
                var model = new PlotModel
                {
                    Title = $"y={a}x²+({b})x+({c})"
                };

                model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
                model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

                var series = new LineSeries();

                for (double x = -20; x <= 20; x += 0.1)
                {
                    double y = a * x * x + b * x + c;
                    series.Points.Add(new DataPoint(x, y));
                }

                model.Series.Add(series);
                plotView.Model = model;
            }
            catch
            {
                mw1.ergebnis.Text = "Error !!!";
            }
        }

    }
}
