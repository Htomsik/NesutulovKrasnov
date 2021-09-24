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

           
            // this.nav_pnl.Children.Add(btn);

        }
        bool flag;
        private static void Check(object text, out string Text)
        {
            Text = "";
            
            Text = text.ToString().Replace(",", ".");
            Text = new DataTable().Compute(Text, null).ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string number = "";
            string input = (string)((Button)e.OriginalSource).Content;


            switch (input)
            {
                case "CE":
                    {



                        int index = 0;
                        for (int i = 0; i < number.Length; i++)
                        {
                            if (!char.IsDigit(number[i]) && number[i] != ',')
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
                case "=":
                    {
                        string chislo = new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString();
                        LabelCalc.Content = chislo;
                        break;
                    }
                case "C":
                    {
                        LabelCalc.Content = "";
                        break;
                    }
                case "x²":
                    {
                        string chislo = new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString();
                        var kvadrat = Convert.ToDouble(chislo);
                        kvadrat = Math.Pow(kvadrat, 2);
                        chislo = Convert.ToString(kvadrat);
                        LabelCalc.Content = chislo;
                        break;
                    }
                case "x³":
                    {
                        string chislo = new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString();
                        var kub = Convert.ToDouble(chislo);
                        kub = Math.Pow(kub, 3);
                        chislo = Convert.ToString(kub);
                        LabelCalc.Content = chislo;
                        break;
                    }
                case "±":
                    {
                        string chislo = new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString();
                        var plus_minus = Convert.ToDouble(chislo);
                        plus_minus = -plus_minus;
                        chislo = Convert.ToString(plus_minus);
                        LabelCalc.Content = chislo;
                        break;
                    }
                case ",":
                    {
                        string chislo = new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString();
                        chislo = LabelCalc.Content.ToString() + ',';
                        LabelCalc.Content = chislo;
                        break;
                    }
                case "√":
                    {
                        string chislo = new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString();
                        var koren = Convert.ToDouble(chislo);
                        koren = Math.Sqrt(koren);
                        chislo = Convert.ToString(koren);
                        LabelCalc.Content = chislo;
                        break;
                    }
                case "1/x":
                    {
                        string chislo = new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString();
                        var x = Convert.ToDouble(chislo);
                        x = 1 / x;
                        chislo = Convert.ToString(x);
                        LabelCalc.Content = chislo;
                        break;
                    }
                case "%":
                    {
                        string chislo = LabelCalc.Content.ToString();
                        string a = chislo;
                        double[] numbers = Regex.Matches(a, @"(\d+(?:\,\d+)?)")
                        .OfType<Match>()
                        .Select(m => double.Parse(m.Groups[1].Value, CultureInfo.GetCultureInfo("ru-RU")) * (m.Groups[1].Value.StartsWith("0,0") ? 10 : 1))
                        .ToArray();

                        string s = chislo;
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
                case "⌫":
                    {


                        number = number.Remove(number.Length - 1);
                        LabelCalc.Content = number;
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
        Button[] butt = new Button[6];
        bool Flag = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Flag) return;
            butt[0]=(ButtonAdd("n!", 2, 1));
            butt[1] = (ButtonAdd("±", 3, 1));
            butt[2] = (ButtonAdd("10x", 4, 1));
            butt[3] = (ButtonAdd("ln", 5, 1));
            butt[4] = (ButtonAdd("log", 6, 1));
            butt[5] = (ButtonAdd("log", 7, 1));
            
            for(int q=0;q<6;q++)
            {
                nav_pnll.Children.Add(butt[q]);
            }
            Flag = true;
            nav_pnll.Margin= new Thickness(27,10,83,80);
            nav_pnl.Margin = new Thickness(42, 74, 0, 78);
            Tg_Btn.Margin = new Thickness(42, 24, 0, 520);
            Tg_Btn.IsChecked = false;

        }
        public static Button ButtonAdd(string _content, int row = 0, int column = 0)
        {
            Button btn = new Button { Content = _content,Name="Button"+row};
            Grid.SetColumn(btn, column);
            Grid.SetRow(btn, row);
            return btn;
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (!Flag) return;
            for (int i = 0; i < 6; i++)
            {
                butt[i].Visibility=Visibility.Collapsed;
            }
            Flag = false;
            nav_pnll.Margin = new Thickness(100, 10, 83, 80);
            nav_pnl.Margin = new Thickness(104, 74, 0, 78);
            Tg_Btn.Margin = new Thickness(113, 30, 0, 514);
            Tg_Btn.IsChecked = false;
        }
        public static Button ButtonVis(string _content, int row = 0, int column = 0)
        {
            Button btn = new Button { Content = _content };
            Grid.SetColumn(btn, column);
            Grid.SetRow(btn, row);
            btn.Visibility = Visibility.Collapsed;
            return btn;
        }
    }

}