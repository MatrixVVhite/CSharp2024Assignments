namespace FormsApp
{
	partial class CalculatorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.num1 = new System.Windows.Forms.TextBox();
			this.num2 = new System.Windows.Forms.TextBox();
			this.OperationLabel = new System.Windows.Forms.Label();
			this.Result = new System.Windows.Forms.Label();
			this.OperationSelection = new System.Windows.Forms.ComboBox();
			this.Calculate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// num1
			// 
			this.num1.Location = new System.Drawing.Point(12, 12);
			this.num1.Name = "num1";
			this.num1.Size = new System.Drawing.Size(40, 20);
			this.num1.TabIndex = 0;
			// 
			// num2
			// 
			this.num2.Location = new System.Drawing.Point(77, 12);
			this.num2.Name = "num2";
			this.num2.Size = new System.Drawing.Size(40, 20);
			this.num2.TabIndex = 1;
			// 
			// OperationLabel
			// 
			this.OperationLabel.AutoSize = true;
			this.OperationLabel.Location = new System.Drawing.Point(58, 15);
			this.OperationLabel.Name = "OperationLabel";
			this.OperationLabel.Size = new System.Drawing.Size(13, 13);
			this.OperationLabel.TabIndex = 2;
			this.OperationLabel.Text = "+";
			// 
			// Result
			// 
			this.Result.AutoSize = true;
			this.Result.Location = new System.Drawing.Point(123, 15);
			this.Result.Name = "Result";
			this.Result.Size = new System.Drawing.Size(13, 13);
			this.Result.TabIndex = 3;
			this.Result.Text = "=";
			// 
			// OperationSelection
			// 
			this.OperationSelection.FormattingEnabled = true;
			this.OperationSelection.Items.AddRange(new object[] {
            "Add",
            "Subtract",
            "Multiply"});
			this.OperationSelection.Location = new System.Drawing.Point(12, 38);
			this.OperationSelection.Name = "OperationSelection";
			this.OperationSelection.Size = new System.Drawing.Size(59, 21);
			this.OperationSelection.TabIndex = 4;
			this.OperationSelection.Text = "Add";
			this.OperationSelection.SelectedIndexChanged += new System.EventHandler(this.OperationSelectedIndexChanged);
			// 
			// Calculate
			// 
			this.Calculate.Location = new System.Drawing.Point(77, 38);
			this.Calculate.Name = "Calculate";
			this.Calculate.Size = new System.Drawing.Size(59, 23);
			this.Calculate.TabIndex = 5;
			this.Calculate.Text = "Calculate";
			this.Calculate.UseVisualStyleBackColor = true;
			this.Calculate.Click += new System.EventHandler(this.CalculateClick);
			// 
			// CalculatorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(264, 68);
			this.Controls.Add(this.Calculate);
			this.Controls.Add(this.OperationSelection);
			this.Controls.Add(this.Result);
			this.Controls.Add(this.OperationLabel);
			this.Controls.Add(this.num2);
			this.Controls.Add(this.num1);
			this.Name = "CalculatorForm";
			this.Text = "Exercise B - Forms";
			this.Load += new System.EventHandler(this.FormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox num1;
		private System.Windows.Forms.TextBox num2;
		private System.Windows.Forms.Label OperationLabel;
		private System.Windows.Forms.Label Result;
		private System.Windows.Forms.ComboBox OperationSelection;
		private System.Windows.Forms.Button Calculate;
	}
}

