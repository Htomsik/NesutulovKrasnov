using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktika.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl
    {
        public Calculator()
        {
            InitializeComponent();
            foreach (UIElement el in nav_pnll.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
            
        }
        bool flag;
        private static void Check(object text,out string Text)
        {
            Text = "";
            if (string.IsNullOrEmpty(text as string)) return;
            Text=text.ToString().Replace(",", ".");
            Text = new DataTable().Compute(Text, null).ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                LabelCalc.Content = String.Empty;
                flag = true;
            }
            string number="";
            string input = (string)((Button)e.OriginalSource).Content;

            switch (input)
            {
                case "CE":
                    {

                        Check(LabelCalc.Content,out number);

                        int index = 0;
                        for (int i = 0; i < number.Length; i++)
                        {
                            if (!char.IsDigit(number[i]) && number[i]!=',')
                            {
                                index = i;
                            }
                        }
                        if (index == 0) LabelCalc.Content = "";
                        else
                        {
                            number = number.Remove(index + 1);
                            LabelCalc.Content = number;
                        }
                       
                        break;
                    }
                case "C":
                    {
                        LabelCalc.Content = "";
                        break;
                    }
                case "=":
                    {
                        Check(LabelCalc.Content, out number);
                        LabelCalc.Content = number;
                        break;
                    }
                case "x²":
                    {
                        Check(LabelCalc.Content, out number);
                        var kvadrat = Convert.ToDouble(number);
                        kvadrat = Math.Pow(kvadrat, 2);
                        number = Convert.ToString(kvadrat);
                        LabelCalc.Content = number;
                        break;
                    }
                case "x³":
                    {
                        Check(LabelCalc.Content, out number);
                        var kub = Convert.ToDouble(number);
                        kub = Math.Pow(kub, 3);
                        number = Convert.ToString(kub);
                        LabelCalc.Content = number;
                        break;
                    }
                case "±":
                    {
                        Check(LabelCalc.Content, out number);
                        var plus_minus = Convert.ToDouble(number);
                        plus_minus = -plus_minus;
                        number = Convert.ToString(plus_minus);
                        LabelCalc.Content = number;
                        break;
                    }
                case ",":
                    {
                        Check(LabelCalc.Content, out number);
                        LabelCalc.Content = number + ',';
                       
                        break;
                    }
                case "√":
                    {
                        Check(LabelCalc.Content, out number);
                        var koren = Convert.ToDouble(number);
                        koren = Math.Sqrt(koren);
                        number = Convert.ToString(koren);
                        LabelCalc.Content = number;
                        break;
                    }
                case "1/x":
                    {
                        Check(LabelCalc.Content, out number);
                        var x = Convert.ToDouble(number);
                        x = 1 / x;
                        number = Convert.ToString(x);
                        LabelCalc.Content = number;
                        break;
                    }
                case "⌫":
                    {
                        Check(LabelCalc.Content, out number);
  
                        number=number.Remove(number.Length - 1);
                        LabelCalc.Content = number;
                        break;
                    }
                case "%":
                    {
                        
                        string s = LabelCalc.Content.ToString();
                        double[] numbers = Regex.Matches(s, @"(\d+(?:\,\d+)?)")
                        .OfType<Match>()
                        .Select(m => double.Parse(m.Groups[1].Value, CultureInfo.GetCultureInfo("ru-RU")) * (m.Groups[1].Value.StartsWith("0,0") ? 10 : 1))
                        .ToArray();

                        
                        string str = "";
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (!char.IsDigit(s[i]))
                            {
                                str += s[i];
                            }
                        }

                        double p = (numbers[0] - numbers[1] * numbers[0]) / 100;
                        LabelCalc.Content = (numbers[0].ToString() + str.Replace(",", "") + (numbers[1] * numbers[0]) / 100).ToString();
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                        break;
                    }
                default:
                    {
                        LabelCalc.Content += input;
                        break;
                    }
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Calc_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }
    }
}
