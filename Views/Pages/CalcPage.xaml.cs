using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;

namespace Praktika.Views.Pages
{
    /// <summary>
    ///     Логика взаимодействия для CalcPage.xaml
    /// </summary>
    public partial class CalcPage : UserControl
    {
        string leftop = "";
        string operation = "";
        string rightop = "";
        public CalcPage()
        {
            InitializeComponent();
            foreach (UIElement el in nav_pnll.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
            leftop = "0";
        }
        string lastoperation = "0";
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string input = (string)((Button)e.OriginalSource).Content;

            if (string.IsNullOrEmpty(LabelCalc.Content.ToString()))
            {
                LabelCalc.Content = "0";
            }
            string text = LabelCalc.Content.ToString();
            input = input == "." ? "." : input;
            if (Arefm(text[text.Length - 1].ToString()) && Arefm(input))
                LabelCalc.Content = text.Remove(text.Length - 1);
            text = LabelCalc.Content.ToString();
            string leftop, rightop = "";

            double[] op = Regex.Matches(text + input, @"(?<!\d)-?\d*[.]?\d+")

            .OfType<Match>()
            .Select(m => double.Parse(m.Value.Replace(".", ",")))
            .ToArray();

            double number = 0;
            string operation = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsDigit(text[i]) && text[i] != '.')
                {
                    operation = text[i].ToString();
                }
            }
            switch (input)
            {
                case "CE":
                    {
                        int index = 0;
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (!char.IsDigit(text[i]) && text[i] != '.')
                            {
                                index = i;
                            }
                        }
                        if (Arefm(text[text.Length - 1].ToString()))
                        {
                            LabelCalc.Content = "0";
                        }
                        else if (index != 0) LabelCalc.Content = text.Remove(index + 1);
                        break;
                    }
                case "=":
                    {

                        LabelCalc.Content = Math.Round(Convert.ToDouble(new DataTable().Compute(LabelCalc.Content.ToString().Replace(",", "."), null)), 7);
                        break;
                    }
                case "C":
                    {
                        LabelCalc.Content = "0";
                        break;
                    }
                case "x²":
                    {

                        //Double num = Convert.ToDouble(new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString());


                        if (op.Length == 2)
                        {
                            LabelCalc.Content = (op[0].ToString() + operation + Math.Round(Math.Pow(op[1], 2), 6).ToString());
                            //ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            //invokeProv.Invoke();
                        }
                        else
                        {
                            number = op[0];
                            LabelCalc.Content = Math.Pow(number, 2);
                        }
                        break;
                    }
                case "x³":
                    {
                        if (op.Length == 2)
                        {
                            LabelCalc.Content = (op[0].ToString() + operation + Math.Round(Math.Pow(op[1], 3), 6).ToString());
                            //ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            //invokeProv.Invoke();
                        }
                        else
                        {
                            number = op[0];
                            LabelCalc.Content = Math.Pow(number, 3);
                        }
                        break;
                    }
                case "10^x":
                    {
                        if (op.Length == 2)
                        {
                            LabelCalc.Content = (op[0].ToString() + operation + Math.Round(Math.Pow(10, op[1]), 6).ToString());
                            //ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            //invokeProv.Invoke();
                        }
                        else
                        {
                            number = op[0];
                            LabelCalc.Content = Math.Pow(10, number);
                        }

                        break;
                    }
                case "n!":
                    {

                        double n = Convert.ToDouble(new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString());

                        int factorial = 1;   // значение факториала


                        if (op.Length == 2)
                        {
                            for (int i = 2; i <= op[1]; i++) // цикл начинаем с 2, т.к. нет смысла начинать с 1
                            {
                                factorial = factorial * i;
                            }
                            LabelCalc.Content = (op[0].ToString() + operation + factorial.ToString());
                            //ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            //invokeProv.Invoke();
                        }
                        else
                        {
                            number = op[0];
                            LabelCalc.Content = factorial;
                        }

                        break;
                    }
                case "±":
                    {


                        if (op.Length == 2)
                        {
                            double plus_minus = op[1];
                            plus_minus = -plus_minus;
                            LabelCalc.Content = (op[0].ToString() + operation + plus_minus.ToString());
                            // ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            // invokeProv.Invoke();
                        }
                        else
                        {
                            double plus_minus = op[0];
                            plus_minus = -plus_minus;
                            LabelCalc.Content = plus_minus;
                        }

                        break;
                    }
                case ".":
                    {

                        if (LabelCalc.Content.ToString().EndsWith(".")) return;
                        LabelCalc.Content = LabelCalc.Content.ToString() + ".";
                        break;
                    }
                case "√":
                    {
                        double koren = Convert.ToDouble(new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString());
                        LabelCalc.Content = Math.Sqrt(koren);
                        break;
                    }
                case "1/x":
                    {

                        double x = Convert.ToDouble(new DataTable().Compute(LabelCalc.Content.ToString(), null).ToString());
                        if (x == 0)
                        {
                            LabelCalc.Content = "";
                            return;
                        }
                        x = 1 / x;
                        LabelCalc.Content =Math.Round( x,7).ToString().Replace(",",".");
                        break;
                    }
                case "%":
                    {
                        string chislo = LabelCalc.Content.ToString();

                        double[] numbers = Regex.Matches(text, @"(\d+(?:\.\d+)?)")
                        .OfType<Match>()
                        .Select(m => double.Parse(m.Groups[1].Value) * (m.Groups[1].Value.StartsWith("0.0") ? 10 : 1))
                        .ToArray();


                        string str = "";
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (!char.IsDigit(text[i]) && text[i] != '.')
                            {
                                str += text[i];
                            }
                        }

                        double p = (numbers[0] - numbers[1] * numbers[0]) / 100;
                        LabelCalc.Content = (numbers[0].ToString() + str + (numbers[1] * numbers[0]) / 100).ToString();
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                        break;
                    }
                case "⌫":
                    {
                        LabelCalc.Content = text.Remove(text.Length - 1);
                        if (LabelCalc.Content.ToString().Length == 0) LabelCalc.Content = "0";

                        break;
                    }
                case "log":
                    {
                        if (op.Length == 2)
                        {
                            double num = op[1];

                            LabelCalc.Content = (op[0].ToString() + operation + Math.Round(Math.Log(op[1]), 7).ToString());
                            // ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            // invokeProv.Invoke();
                        }
                        else
                        {
                            LabelCalc.Content = Math.Round(Math.Log(op[0]), 7);
                        }



                        break;
                    }
                case "ln":
                    {
                        if (op.Length == 2)
                        {
                            double num = op[1];

                            LabelCalc.Content = (op[0].ToString() + operation + Math.Round(Math.Log(op[1]), 7).ToString());
                            // ButtonAutomationPeer peer = new ButtonAutomationPeer(Ravno);
                            //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            // invokeProv.Invoke();
                        }
                        else
                        {
                            LabelCalc.Content = Math.Round(Math.Log10(op[0]), 7);
                        }
                        break;
                    }

                default:
                    {
                        if (op.Length == 2)
                        {
                            if (op[1] == 0)
                            {
                                LabelCalc.Content = "";
                                return;
                            }
                        }

                        if (text[0].ToString() == "0" && char.IsDigit(Convert.ToChar(input)) && text.Length == 1)
                        {
                            LabelCalc.Content = text.Remove(0);
                        }

                        string textLocal = LabelCalc.Content.ToString() + input;
                        int index = 0;
                        for (int i = 0; i < textLocal.Length; i++)
                        {
                            if (!char.IsDigit(textLocal[i]) && textLocal[i] != '.' && textLocal[i] != ',' && char.IsDigit(textLocal[0]))
                            {
                                index++;
                            }
                        }
                        if (index > 1)
                        {
                            LabelCalc.Content = Math.Round(Convert.ToDouble(new DataTable().Compute(text.Replace(",", "."), null).ToString()), 7);
                            LabelCalc.Content += input;
                        }
                        else LabelCalc.Content += input;
                        break;

                    }
            }
            try
            {
                LabelCalc.Content = Math.Round(Convert.ToDouble(LabelCalc.Content), 7);
            }

            catch (System.FormatException) { }


        }
        private bool Arefm(string c)
        {
            if (c == "+" || c == "-" || c == "/" || c == "*") return true;
            else return false;
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
            butt[0] = (ButtonAdd("n!", 2, 1));
            butt[1] = (ButtonAdd("±", 3, 1));
            butt[2] = (ButtonAdd("10^x", 4, 1));
            butt[3] = (ButtonAdd("ln", 5, 1));
            butt[4] = (ButtonAdd("log", 6, 1));
            butt[5] = (ButtonAdd("x³", 7, 1));

            for (int q = 0; q < 6; q++)
            {
                nav_pnll.Children.Add(butt[q]);
                butt[q].Click += Button_Click;
            }
            Flag = true;
            nav_pnll.Margin = new Thickness(40, 10, 70, 80);
            nav_pnl.Margin = new Thickness(42, 74, 0, 80);
            Tg_Btn.Margin = new Thickness(50, 24, 0, 520);
            Tg_Btn.IsChecked = false;




        }
        public static Button ButtonAdd(string _content, int row = 0, int column = 0)
        {
            Button btn = new Button { Content = _content, Name = "Button" + row };
            Grid.SetColumn(btn, column);
            Grid.SetRow(btn, row);
            return btn;
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (!Flag) return;
            for (int i = 0; i < 6; i++)
            {
                butt[i].Visibility = Visibility.Collapsed;
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