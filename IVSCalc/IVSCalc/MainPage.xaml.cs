using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.System;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Collections.Generic;
using System.Collections;
using Windows.System.Profile;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IVSCalc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int DecimalCounter = 0;

        List<OperationType> SemiOpera = new List<OperationType>();
        List<double> SemiNumbers = new List<double>();
        List<string> Example = new List<string>();
        OperationType LastOpera;

        double currentNumber = 0;

        public double CurrentNumber
        {
            get => currentNumber;
            set
            {
                currentNumber = value;
                CalcRow.Text = CurrentNumber.ToString();
            }
        }

        public bool DotAdded { get; private set; } = false;
        public bool DotPrepared { get; private set; } = false;
        public bool NewInput { get; private set; } = false;
        public bool NumberAdded { get; private set; }
        public double Answer { get; private set; }
        public double LastNumber { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 350, Height = 430 });

            //LostFocus += MainPage_LostFocus;
            Focuser.Focus(FocusState.Programmatic);

            DocControl.KeyDown += DocControl_KeyDown;

            Focuser.Focus(FocusState.Programmatic);

            SetKeyDown(ControlsGrid);

            if (!AnalyticsInfo.VersionInfo.DeviceFamily.Contains("Desktop"))
            {
                Focuser.Visibility = Visibility.Collapsed;
            }

            DataContext = this;
        }

        private void MainPage_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DocControl.Visibility != Visibility.Visible)
                Focus(FocusState.Programmatic);
            else
                DocControl.Focus(FocusState.Programmatic);
        }

        private void DocControl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            Debug.WriteLine("Byl jsem tu");
        }

        private void SetKeyDown(Panel ParentObject)
        {
            foreach (var item in ParentObject.Children)
            {
                if (item is Panel)
                {
                    SetKeyDown((Panel)item);
                }
                else
                {
                    if (item is UIElement)
                    {
                        (item as UIElement).KeyDown += KeyInput;
                    }
                    else
                    {
                        Debug.WriteLine(item.GetType());
                    }
                }
            }
        }

        private void KeyInput(object sender, KeyRoutedEventArgs e)
        {
            Debug.WriteLine(e.Key);
            Focuser.Focus(FocusState.Programmatic);

            switch (e.Key)
            {
                case VirtualKey.Number0:
                case VirtualKey.Number1:
                case VirtualKey.Number2:
                case VirtualKey.Number3:
                case VirtualKey.Number4:
                case VirtualKey.Number5:
                case VirtualKey.Number6:
                case VirtualKey.Number7:
                case VirtualKey.Number8:
                case VirtualKey.Number9:
                case VirtualKey.NumberPad0:
                case VirtualKey.NumberPad1:
                case VirtualKey.NumberPad2:
                case VirtualKey.NumberPad3:
                case VirtualKey.NumberPad4:
                case VirtualKey.NumberPad5:
                case VirtualKey.NumberPad6:
                case VirtualKey.NumberPad7:
                case VirtualKey.NumberPad8:
                case VirtualKey.NumberPad9:
                    AddNumber(e.Key);
                    break;

                case VirtualKey.C:
                    ClearAll(null, null);
                    break;

                case VirtualKey.P:
                case VirtualKey.O:
                case VirtualKey.Add:
                case VirtualKey.Subtract:
                case VirtualKey.Divide:
                case VirtualKey.Multiply:
                    PrepareOperation(e.Key);
                    break;

                case VirtualKey.Decimal:
                    AddDot(null, null);
                    break;

                case VirtualKey.Enter:
                case VirtualKey.R:
                    Execute();
                    e.Handled = true;
                    break;

                case VirtualKey.L:
                case VirtualKey.F:
                case VirtualKey.S:
                    SolveOperation(e.Key);
                    break;

                case VirtualKey.Delete:
                    ClearCurrentNumber(null, null);
                    break;

                case VirtualKey.Escape:

                    if (DocGrid.Visibility == Visibility.Visible)
                        DocControl.CloseHelp();
                    else
                        ClearAll(null, null);

                    break;

                case VirtualKey.F1:
                    Help_Click(null, null);
                    break;

                case VirtualKey.Back:
                    BackNum(null, null);
                    break;

                case VirtualKey.X:
                    DeviationProf(10);
                    break;

                case VirtualKey.Y:
                    DeviationProf(100);
                    break;

                case VirtualKey.Z:
                    DeviationProf(1000);
                    break;

                default:
                    break;
            }
        }

        private void DeviationProf(int Count)
        {
            double[] Numbs = new double[Count];

            Random Rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                Numbs[i] = Rand.Next(0, 12498754);
            }

            Debug.WriteLine(Math.Deviation(Numbs).ToString());
        }

        private void Execute()
        {
            if (!ExRow.Text.Contains("="))
            {
                if (!NumberAdded)
                {
                    Example.Add(CurrentNumber.ToString());
                }

                if (SemiNumbers.Count != SemiOpera.Count + 1)
                {
                    SemiNumbers.Add(CurrentNumber);
                }
                else
                { }
            }
            else
            {
                char Opera = 'x';
                switch (LastOpera)
                {
                    case OperationType.Add:
                        Opera = '+';
                        break;
                    case OperationType.Sub:
                        Opera = '-';
                        break;
                    case OperationType.Div:
                        Opera = '/';
                        break;
                    case OperationType.Mul:
                        Opera = '*';
                        break;
                }

                Example = new List<string>
                {
                    SemiNumbers[0].ToString(),
                    Opera.ToString(),
                    CurrentNumber.ToString()
                };

                SemiNumbers.Add(CurrentNumber);
                SemiOpera.Add(LastOpera);
            }

            WriteEx();
            ExRow.Text += " =";

            while (SemiOpera.Contains(OperationType.Power) || SemiOpera.Contains(OperationType.Sqrt))
            {
                var Index = SemiOpera.IndexOf(OperationType.Power);
                if (SemiOpera.IndexOf(OperationType.Sqrt) < Index && SemiOpera.Contains(OperationType.Sqrt) || Index == -1)
                    Index = SemiOpera.IndexOf(OperationType.Sqrt);

                int PowerInt = 0;

                switch (SemiOpera[Index])
                {
                    case OperationType.Power:

                        if (int.TryParse(SemiNumbers[Index + 1].ToString(), out PowerInt))
                            SemiNumbers[Index] = Math.Pow(SemiNumbers[Index], PowerInt);
                        else
                            SetError();

                        break;

                    case OperationType.Sqrt:

                        if (int.TryParse(SemiNumbers[Index + 1].ToString(), out PowerInt))
                            SemiNumbers[Index] = Math.Root(SemiNumbers[Index], PowerInt);
                        else
                            SetError();

                        break;

                    default:
                        break;
                }

                SemiNumbers.RemoveAt(Index + 1);
                SemiOpera.RemoveAt(Index);
            }

            foreach (var Operation in new List<OperationType>()
            #region OperationsSequence
            {
                OperationType.Power,
                OperationType.Div,
                OperationType.Mul,
                OperationType.Sub,
                OperationType.Add

            })
            #endregion
            {
                while (SemiOpera.Contains(Operation))
                {
                    int Index = SemiOpera.IndexOf(Operation);

                    switch (Operation)
                    {
                        case OperationType.Add:
                            SemiNumbers[Index] = Math.Addition(SemiNumbers[Index], SemiNumbers[Index + 1]);
                            break;

                        case OperationType.Sub:
                            SemiNumbers[Index] = Math.Substraction(SemiNumbers[Index], SemiNumbers[Index + 1]);
                            break;

                        case OperationType.Div:
                            if (SemiNumbers[Index + 1] != 0)
                            {
                                SemiNumbers[Index] = Math.Division(SemiNumbers[Index], SemiNumbers[Index + 1]);
                            }
                            else
                            {
                                SetError();
                            }
                            break;

                        case OperationType.Mul:
                            SemiNumbers[Index] = Math.Multiplication(SemiNumbers[Index], SemiNumbers[Index + 1]);
                            break;

                        case OperationType.Power:
                            if (int.TryParse(SemiNumbers[Index + 1].ToString(), out int PowerInt))
                            {
                                SemiNumbers[Index] = Math.Pow(SemiNumbers[Index], PowerInt);
                            }
                            else
                            {
                                SetError();
                            }
                            break;
                    }

                    SemiNumbers.RemoveAt(Index + 1);
                    SemiOpera.RemoveAt(Index);
                }
            }


            CalcRow.Text = SemiNumbers[0].ToString();
            Answer = SemiNumbers[0];
            NewInput = true;
            NumberAdded = false;
        }

        private async void SetError()
        {
            Windows.UI.Popups.MessageDialog Messa = new Windows.UI.Popups.MessageDialog("Invalid operation\nCalculator doesn't work with infinity and undefined values.", "Error");

            await Messa.ShowAsync();

            ClearAll(null, null);
            CalcRow.Text = "Invalid operation";
        }

        public double GetResult(int Index)
        {
            if (Index + 1 < SemiOpera.Count)
            {
                if (SemiOpera[Index + 1] == OperationType.Mul || SemiOpera[Index + 1] == OperationType.Div)
                {
                    GetResult(Index + 1);
                }
            }

            switch (SemiOpera[Index])
            {
                case OperationType.Add:
                    SemiNumbers[Index] = Math.Addition(SemiNumbers[Index], SemiNumbers[Index + 1]);
                    break;

                case OperationType.Sub:
                    SemiNumbers[Index] = Math.Substraction(SemiNumbers[Index], SemiNumbers[Index + 1]);
                    break;

                case OperationType.Div:
                    if (SemiNumbers[Index + 1] != 0)
                    {
                        SemiNumbers[Index] = Math.Division(SemiNumbers[Index], SemiNumbers[Index + 1]);
                    }
                    else
                    {
                        SetError();
                    }
                    break;

                case OperationType.Mul:
                    SemiNumbers[Index] = Math.Multiplication(SemiNumbers[Index], SemiNumbers[Index + 1]);
                    break;

                default:

                    Debug.WriteLine("Power opera");
                    break;
            }

            SemiNumbers.RemoveAt(Index + 1);
            SemiOpera.RemoveAt(Index);

            return 0;
        }

        private void ClearCurrentNumber(object sender, RoutedEventArgs e)
        {
            CurrentNumber = 0;
            DecimalCounter = 0;
            DotAdded = false;
            DotPrepared = false;
            Dot.Background = new SolidColorBrush(Color.FromArgb(51, 255, 255, 255));
            NewInput = true;
        }

        private void AddNumber(VirtualKey KeyNumber)
        {
            var NumberStr = KeyNumber.ToString();
            int.TryParse(NumberStr[NumberStr.Length - 1].ToString(), out int Number);

            AddNumber(Number);
        }

        private void AddNumber(object sender, RoutedEventArgs e)
        {
            var NumberStr = (sender as Button).Content.ToString();
            int.TryParse(NumberStr, out int Number);

            AddNumber(Number);
        }

        private void AddNumber(double Number)
        {
            if (SemiOpera.Count == 0)
            {
                ExRow.Text = "";
                Example = new List<string>();

                if (SemiNumbers.Count != 0)
                {
                    Answer = SemiNumbers[0];
                }

                SemiNumbers = new List<double>();
            }

            if (NewInput)
            {
                ClearCurrentNumber(null, null);
                NewInput = false;
            }

            string AddAnotherNumber = CurrentNumber.ToString();
            if (CurrentNumber.ToString().Contains("."))
                AddAnotherNumber = AddAnotherNumber.Remove(CurrentNumber.ToString().IndexOf('.'), 1);

            if (DotAdded && AddAnotherNumber.Length < 15)
            {
                DecimalCounter++;
                CurrentNumber /= 10;
            }
            else
            {
                if (DotPrepared)
                {
                    Dot.Background = new SolidColorBrush(Color.FromArgb(51, 255, 255, 255));
                    DotPrepared = !DotPrepared;
                    DotAdded = !DotAdded;
                    DecimalCounter++;
                    CurrentNumber /= 10;
                }
            }

            for (int i = 0; i < DecimalCounter; i++)
            {
                Number /= 10;
            }

            if (AddAnotherNumber.Length < 15)
            {
                CurrentNumber = CurrentNumber * 10 + Number;
            }
        }

        private void AddDot(object sender, RoutedEventArgs e)
        {
            if (!DotAdded && CurrentNumber.ToString().Length < 15)
            {
                if (!DotPrepared)
                {
                    Dot.Background = new SolidColorBrush(Colors.ForestGreen);
                    DotPrepared = !DotPrepared;
                }
                else
                {
                    Dot.Background = new SolidColorBrush(Color.FromArgb(51, 255, 255, 255));
                    DotPrepared = !DotPrepared;
                }
            }
        }

        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            DocControl.SetClose(async (x, y) =>
            {
                DocGrid.Visibility = Visibility.Collapsed;
                await ControlsGrid.Blur(0, 1000).StartAsync();
            });

            MainPage_LostFocus(null, null);

            DocControl.Focus(FocusState.Programmatic);

            DocGrid.Visibility = Visibility.Visible;
            await ControlsGrid.Blur(10, duration: 1000).StartAsync();
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            ClearCurrentNumber(null, null);
            SemiNumbers = new List<double>();
            SemiOpera = new List<OperationType>();
            Example = new List<string>();
            NumberAdded = false;
            NewInput = false;

            ExRow.Text = "";
        }

        private void ButtonOperation(object sender, RoutedEventArgs e)
        {
            var OperationStr = (sender as Button).Content.ToString();

            switch (OperationStr)
            {
                case "+":
                    PrepareOperation(VirtualKey.Add);
                    break;

                case "-":
                    PrepareOperation(VirtualKey.Subtract);
                    break;

                case "÷":
                case "/":
                    PrepareOperation(VirtualKey.Divide);
                    break;

                case "*":
                    PrepareOperation(VirtualKey.Multiply);
                    break;

                case "xⁿ":
                    PrepareOperation(VirtualKey.P);
                    break;

                case "ⁿ√":
                    PrepareOperation(VirtualKey.O);
                    break;

                case "Log":
                    SolveOperation(VirtualKey.L);
                    break;

                case "√":
                    SolveOperation(VirtualKey.S);
                    break;

                case "x!":
                    SolveOperation(VirtualKey.F);
                    break;

                case "=":
                    Execute();
                    break;

                default:
                    break;
            }
        }

        private void PrepareOperation(VirtualKey Key)
        {
            string Operation = "";
            OperationType Opera = OperationType.Add;

            switch (Key)
            {
                case VirtualKey.P:
                    Opera = OperationType.Power;
                    Operation = "xⁿ";
                    break;

                case VirtualKey.O:
                    Opera = OperationType.Sqrt;
                    Operation = "ⁿ√";
                    break;

                case VirtualKey.Multiply:
                    Opera = OperationType.Mul;
                    Operation = "*";
                    break;

                case VirtualKey.Add:
                    Opera = OperationType.Add;
                    Operation = "+";
                    break;

                case VirtualKey.Subtract:
                    Opera = OperationType.Sub;
                    Operation = "-";
                    break;

                case VirtualKey.Divide:
                    Opera = OperationType.Div;
                    Operation = "/";
                    break;

                default:
                    break;
            }

            if ((NumberAdded && NewInput) || !(NumberAdded && NewInput))
            {
                if (!NumberAdded)
                {
                    if (ExRow.Text.Contains("="))
                    {
                        Example = new List<string>();

                        if (CalcRow.Text == SemiNumbers[0].ToString())
                            Example.Add(SemiNumbers[0].ToString());
                        else
                            Example.Add(CurrentNumber.ToString());

                    }
                    else
                    {
                        SemiNumbers.Add(CurrentNumber);
                        Example.Add(CurrentNumber.ToString());
                    }
                }

                Example.Add(Operation);
                SemiOpera.Add(Opera);
            }
            else
            {
                Example[Example.Count - 1] = Operation;
                SemiOpera[SemiOpera.Count - 1] = Opera;
            }

            WriteEx();

            LastOpera = Opera;

            NumberAdded = false;
            NewInput = true;
        }

        private void SolveOperation(VirtualKey Key)
        {
            double SolvedNum = 0;
            string SemiEx = "";
            string CurrentNumStr = "";


            if (ExRow.Text.Contains("="))
            {
                ExRow.Text = "";
                Example = new List<string>();

                if (SemiNumbers.Count != 0)
                {
                    Answer = SemiNumbers[0];
                }

                SemiNumbers = new List<double>();
                CurrentNumStr = Answer.ToString();
                CurrentNumber = Answer;
            }
            else
            {
                if (NewInput)
                {
                    CurrentNumStr = Example[Example.Count - 1];
                }
                else
                {
                    CurrentNumStr = CurrentNumber.ToString();
                }
            }

            switch (Key)
            {
                case VirtualKey.L:
                    SolvedNum = Math.Log(CurrentNumber);

                    if (double.IsNaN(SolvedNum))
                    {
                        SetError();
                    }
                    else
                    {
                        SemiEx = $"Log₁₀({CurrentNumStr})";
                    }
                    break;

                case VirtualKey.F:

                    if (CurrentNumber > 171)
                        SolvedNum = double.PositiveInfinity;
                    else
                        SolvedNum = Math.Factorial(CurrentNumber);

                    if (!double.IsInfinity(SolvedNum) && !double.IsNegativeInfinity(SolvedNum) && !double.IsNaN(SolvedNum))
                    {
                        SemiEx = $"({CurrentNumStr})!";
                    }
                    else
                    {
                        SetError();
                    }
                    break;

                case VirtualKey.S:
                    SolvedNum = Math.Root(CurrentNumber, 2);

                    if (double.IsNaN(SolvedNum))
                    {
                        SetError();
                    }
                    else
                    {
                        SemiEx = $"√({CurrentNumStr})";
                    }
                    break;

                default:
                    break;
            }

            if ((NewInput || (!NewInput && NumberAdded)) && Example.Count != 0)
            {
                Example[Example.Count - 1] = SemiEx;
                SemiNumbers[SemiNumbers.Count - 1] = SolvedNum;
            }
            else
            {
                Example.Add(SemiEx);
                SemiNumbers.Add(SolvedNum);
            }

            WriteEx();

            CurrentNumber = SolvedNum;
            NewInput = true;
            NumberAdded = true;
        }

        private void NumNegation(object sender, RoutedEventArgs e)
        {
            CurrentNumber *= -1;
        }

        private void BackNum(object sender, RoutedEventArgs e)
        {
            if (CurrentNumber != 0)
            {
                var CurNumStr = CurrentNumber.ToString();
                CurNumStr = CurNumStr.Substring(0, CurNumStr.Length - 1);

                if (DotAdded)
                {
                    DecimalCounter--;
                }

                if (CurNumStr == "")
                {
                    CurrentNumber = 0;
                    return;
                }

                if (CurNumStr[CurNumStr.Length - 1] == '.')
                {
                    CurNumStr = CurNumStr.Substring(0, CurNumStr.Length - 1);
                    DotAdded = false;
                }

                double.TryParse(CurNumStr, out double NewCurNum);

                CurrentNumber = NewCurNum;
            }
        }

        public void WriteEx()
        {
            ExRow.Text = "";

            foreach (var SemiEx in Example)
            {
                ExRow.Text += $" {SemiEx}";
            }
        }

        private void GetFocus(object sender, TappedRoutedEventArgs e)
        {
            Focuser.Focus(FocusState.Pointer);
        }
    }
}
