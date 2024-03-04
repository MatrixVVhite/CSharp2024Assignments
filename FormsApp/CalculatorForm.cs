using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsApp
{
	public partial class CalculatorForm : Form
	{
		private MathOperation _currentOperation;
		private readonly Dictionary<string, MathOperation> _operations;

		private MathOperation CurrentOperation { get => _currentOperation; set => ChangeCurrentOperation(value); }

		public CalculatorForm()
		{
			InitializeComponent();
			_operations = new Dictionary<string, MathOperation>()
			{
				{ "Add",		new MathOperation("+", (n1, n2) => n1 + n2) },
				{ "Subtract",	new MathOperation("-", (n1, n2) => n1 - n2) },
				{ "Multiply",	new MathOperation("x", (n1, n2) => n1 * n2) },
				{ "Divide",		new MathOperation("/", (n1, n2) => n1 / n2) },
				{ "Power",		new MathOperation("^", Math.Pow) },
				{ "Modulo",		new MathOperation("%", (n1, n2) => n1 % n2) }
			};
			CurrentOperation = _operations.First().Value;
		}

		private void FormLoad(object sender, EventArgs e)
		{
			foreach (string key in _operations.Keys)
				OperationSelection.Items.Add(key);
			OperationSelection.SelectedIndex = 0;
		}

		private void OperationSelectedIndexChanged(object sender, EventArgs e)
		{
			string operationText = (sender as ComboBox).SelectedItem.ToString();
			try
			{
				CurrentOperation = _operations[operationText];
			}
			catch (KeyNotFoundException) { }
		}

		private void CalculateClick(object sender, EventArgs e)
		{
			string resultText;
			if (TryGetNumbers(out double num1, out double num2))
			{
				resultText = CurrentOperation.Calculate(num1, num2).ToString();
				Result.ForeColor = Color.Black;
			}
			else
			{
				resultText = "Invalid Input";
				Result.ForeColor = Color.Red;
			}
			Result.Text = $"= {resultText}";
		}

		private void ChangeCurrentOperation(MathOperation operation)
		{
			_currentOperation = operation;
			OperationLabel.Text = operation.ToString();
		}

		private bool TryGetNumbers(out double num1, out double num2)
		{
			return double.TryParse(this.num1.Text, out num1) & double.TryParse(this.num2.Text, out num2);
		}

		private struct MathOperation
		{
			public string label;
			public Func<double, double, double> operation;

			public MathOperation(string label, Func<double, double, double> operation)
			{
				this.label = label;
				this.operation = operation;
			}

			public double Calculate(double num1, double num2) => operation(num1, num2);

			public override string ToString() => label;
		}
	}
}
