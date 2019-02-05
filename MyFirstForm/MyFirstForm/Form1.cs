using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstForm
{
    public partial class calculatorForm : Form
    {
        List<string> numbers = new List<string>();
        List<string> operators = new List<string>();
        int index = 0, dOperator, mOperator;
        double calculator = 0, clearZeros;
        bool calculationDone = false;
        string temp;

        #region Constructor
        public calculatorForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Methods (Buttons)

        #region Special Operators (empty)
        private void percentageButton_Click(object sender, EventArgs e)
        {

        }

        private void powerButton_Click(object sender, EventArgs e)
        {

        }

        private void squareButton_Click(object sender, EventArgs e)
        {

        }

        private void oneXButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Clearing Buttons
        private void ceButton_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void cButton_Click(object sender, EventArgs e)
        {
            ClearDisplay();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (this.displayBox.Text.Length > 0)
            {
                StringBuilder sb = new StringBuilder(this.displayBox.Text);
                sb.Remove(this.displayBox.Text.Length-1, 1);
                this.displayBox.Text = sb.ToString();
            }
            CursorToEndOfDisplay();
        }
        #endregion

        #region Operators
        private void divideButton_Click(object sender, EventArgs e)
        {
            AddOperation("/");
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            AddOperation("*");
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            AddOperation("-");
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddOperation("+");
        }
        #endregion

        #region Numbers
        private void oneButton_Click(object sender, EventArgs e)
        {
            StringInserter("1");
        }

        private void twoButton_Click(object sender, EventArgs e)
        {
            StringInserter("2");
        }

        private void threeButton_Click(object sender, EventArgs e)
        {
            StringInserter("3");
        }

        private void fourButton_Click(object sender, EventArgs e)
        {
            StringInserter("4");
        }

        private void fiveButton_Click(object sender, EventArgs e)
        {
            StringInserter("5");
        }

        private void sixButton_Click(object sender, EventArgs e)
        {
            StringInserter("6");
        }

        private void sevenButton_Click(object sender, EventArgs e)
        {
            StringInserter("7");
        }

        private void eightButton_Click(object sender, EventArgs e)
        {
            StringInserter("8");
        }

        private void nineButton_Click(object sender, EventArgs e)
        {
            StringInserter("9");
        }

        private void zeroButton_Click(object sender, EventArgs e)
        {
            StringInserter("0");
        }
        #endregion

        #region Other Buttons
        private void plusMinusButton_Click(object sender, EventArgs e)
        {
            EmptyDisplayCheck("0");

            if (this.displayBox.Text[0] == '-')
            {
                AddCharacterInfront('+');
            }
            else if (this.displayBox.Text[0] == '+')
            {
                AddCharacterInfront('-');
            }
            else if(this.displayBox.Text[0] != '0')
            {
                this.displayBox.Text = "-" + this.displayBox.Text;
            }
            CursorToEndOfDisplay();
        }

        private void pointButton_Click(object sender, EventArgs e)
        {
            if (!displayBox.Text.Contains('.'))
            StringInserter(".");
        }

        private void equalsButton_Click(object sender, EventArgs e)
        {
            AddOperation("");
            if (operators.Contains("*") || operators.Contains("/"))
            {
                do
                {
                    dOperator = operators.IndexOf("/");
                    mOperator = operators.IndexOf("*");

                if (mOperator < dOperator && mOperator != -1 || dOperator == -1)
                    {
                        calculator = Convert.ToDouble(numbers[mOperator]) * Convert.ToDouble(numbers[mOperator + 1]);
                        numbers[mOperator] = Convert.ToString(calculator);
                        numbers.RemoveAt(mOperator + 1);
                        operators.RemoveAt(mOperator);
                    }
                else if (mOperator > dOperator && dOperator != -1 || mOperator == -1)
                    {
                        calculator = Convert.ToDouble(numbers[dOperator]) / Convert.ToDouble(numbers[dOperator + 1]);
                        numbers[dOperator] = Convert.ToString(calculator);
                        numbers.RemoveAt(dOperator + 1);
                        operators.RemoveAt(dOperator);
                    }

                } while (operators.Contains("*") || operators.Contains("/"));
            }

            if(operators.Contains("+") || operators.Contains("-"))
            {
                calculator = Convert.ToDouble(numbers[0]);
                for (int i = 0; i < numbers.Count; i++)
                {
                    switch (operators[i])
                    {
                        case "+":
                            calculator = calculator + Convert.ToDouble(numbers[i + 1]);
                            break;
                        case "-":
                            calculator = calculator - Convert.ToDouble(numbers[i + 1]);
                            break;
                        default:
                            break;
                    }
                }
            }
            ClearDisplay();
            displayBox.Text = Convert.ToString(calculator);
            numbers[0] = displayBox.Text;
            calculationDone = true;
            index = 0;
        }
        #endregion

        #endregion

        #region Helper Methods & Misc. Methods

        #region Helper Functions

        private void ClearDisplay()
        {
            this.displayBox.Text = string.Empty;
            CursorToEndOfDisplay();
        }

        private void FocusDisplay()
        {
            this.displayBox.Focus();
        }

        private void StringInserter(string s)
        {
            if (calculationDone)
            {
                ClearAll();
            }
            this.displayBox.Text = this.displayBox.Text.Insert(this.displayBox.SelectionStart, s);
            //ClearZeroBeforeNumbers();
            CursorToEndOfDisplay();
        }

        private void CursorToEndOfDisplay()
        {
            this.displayBox.SelectionStart = this.displayBox.Text.Length;
        }

        private void AddCharacterInfront(char s)
        {
            char[] ch = this.displayBox.Text.ToCharArray();

            if (ch[0] != '0')
            {
                ch[0] = s;
                this.displayBox.Text = new string(ch);
            }
        }

        private void EmptyDisplayCheck(string e)
        {
            if (displayBox.Text == string.Empty) displayBox.Text = e;
        }

        private void AddOperation(string op)
        {
            EmptyDisplayCheck("0");
            ClearZeroBeforeNumbers();
            temp = this.displayBox.Text;
            if (calculationDone)
            {
                ClearAll();
            }
            numbers.Add(temp);
            operators.Add(op);
            outputLabel.Text = outputLabel.Text + numbers[index] + operators[index];
            ClearDisplay();
            index++;
        }

        private void ClearAll()
        {
            ClearDisplay();
            outputLabel.Text = string.Empty;
            numbers.Clear();
            operators.Clear();
            index = 0;
            calculationDone = false;
        }

        private void ClearZeroBeforeNumbers()
        {
            /*if(displayBox.Text != "0")
            {*/
                clearZeros = Convert.ToDouble(displayBox.Text);
                clearZeros = clearZeros * 1;
                ClearDisplay();
                displayBox.Text = clearZeros.ToString();
            //}
            CursorToEndOfDisplay();
        }
        #endregion

        #region Keypresses
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.NumPad0:
                    zeroButton.PerformClick();
                    break;
                case Keys.NumPad1:
                    oneButton.PerformClick();
                    break;
                case Keys.NumPad2:
                    twoButton.PerformClick();
                    break;
                case Keys.NumPad3:
                    threeButton.PerformClick();
                    break;
                case Keys.NumPad4:
                    fourButton.PerformClick();
                    break;
                case Keys.NumPad5:
                    fiveButton.PerformClick();
                    break;
                case Keys.NumPad6:
                    sixButton.PerformClick();
                    break;
                case Keys.NumPad7:
                    sevenButton.PerformClick();
                    break;
                case Keys.NumPad8:
                    eightButton.PerformClick();
                    break;
                case Keys.NumPad9:
                    nineButton.PerformClick();
                    break;
                case Keys.Add:
                    addButton.PerformClick();
                    break;
                case Keys.Subtract:
                    subtractButton.PerformClick();
                    break;
                case Keys.Multiply:
                    multiplyButton.PerformClick();
                    break;
                case Keys.Divide:
                    divideButton.PerformClick();
                    break;
                case Keys.OemPeriod:
                    pointButton.PerformClick();
                    break;
                case Keys.Enter:
                    equalsButton.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #endregion
    }
}
