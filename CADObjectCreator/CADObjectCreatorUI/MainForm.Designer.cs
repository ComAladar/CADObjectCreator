namespace CADObjectCreatorUI
{
    partial class MainForm
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
            this.ShelfLengthTextBox = new System.Windows.Forms.TextBox();
            this.ShelfHeightTextBox = new System.Windows.Forms.TextBox();
            this.ShelfBindingHeightTextBox = new System.Windows.Forms.TextBox();
            this.ShelfWidthTextBox = new System.Windows.Forms.TextBox();
            this.ShelfLegsHeightTextBox = new System.Windows.Forms.TextBox();
            this.ConstructButton = new System.Windows.Forms.Button();
            this.SetMinButton = new System.Windows.Forms.Button();
            this.SetMaxButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShelfMaxHeight = new System.Windows.Forms.Label();
            this.ShelfMinHeight = new System.Windows.Forms.Label();
            this.ShelfMaxWidth = new System.Windows.Forms.Label();
            this.ShelfMinWidth = new System.Windows.Forms.Label();
            this.ShelfMaxLength = new System.Windows.Forms.Label();
            this.ShelfMinLength = new System.Windows.Forms.Label();
            this.ShelfHeight = new System.Windows.Forms.Label();
            this.ShelfWidth = new System.Windows.Forms.Label();
            this.ShelfLength = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ShelfBindingMaxHeight = new System.Windows.Forms.Label();
            this.ShelfBindingMinHeight = new System.Windows.Forms.Label();
            this.ShelfLegsMaxHeight = new System.Windows.Forms.Label();
            this.ShelfLegsMinHeight = new System.Windows.Forms.Label();
            this.ShelfBindingHeight = new System.Windows.Forms.Label();
            this.ShelfLegsHeight = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MaxSizeLabel = new System.Windows.Forms.Label();
            this.MinSizeLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShelfLengthTextBox
            // 
            this.ShelfLengthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ShelfLengthTextBox.Location = new System.Drawing.Point(20, 45);
            this.ShelfLengthTextBox.Name = "ShelfLengthTextBox";
            this.ShelfLengthTextBox.Size = new System.Drawing.Size(100, 20);
            this.ShelfLengthTextBox.TabIndex = 0;
            this.ShelfLengthTextBox.Text = "420";
            this.ShelfLengthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxOnlyDouble);
            this.ShelfLengthTextBox.Leave += new System.EventHandler(this.TextBoxLeaveVerify);
            // 
            // ShelfHeightTextBox
            // 
            this.ShelfHeightTextBox.Location = new System.Drawing.Point(380, 45);
            this.ShelfHeightTextBox.Name = "ShelfHeightTextBox";
            this.ShelfHeightTextBox.Size = new System.Drawing.Size(100, 20);
            this.ShelfHeightTextBox.TabIndex = 2;
            this.ShelfHeightTextBox.Text = "20";
            this.ShelfHeightTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxOnlyDouble);
            this.ShelfHeightTextBox.Leave += new System.EventHandler(this.TextBoxLeaveVerify);
            // 
            // ShelfBindingHeightTextBox
            // 
            this.ShelfBindingHeightTextBox.Location = new System.Drawing.Point(147, 35);
            this.ShelfBindingHeightTextBox.Name = "ShelfBindingHeightTextBox";
            this.ShelfBindingHeightTextBox.Size = new System.Drawing.Size(100, 20);
            this.ShelfBindingHeightTextBox.TabIndex = 4;
            this.ShelfBindingHeightTextBox.Text = "160";
            this.ShelfBindingHeightTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxOnlyDouble);
            this.ShelfBindingHeightTextBox.Leave += new System.EventHandler(this.TextBoxLeaveVerify);
            // 
            // ShelfWidthTextBox
            // 
            this.ShelfWidthTextBox.Location = new System.Drawing.Point(200, 45);
            this.ShelfWidthTextBox.Name = "ShelfWidthTextBox";
            this.ShelfWidthTextBox.Size = new System.Drawing.Size(100, 20);
            this.ShelfWidthTextBox.TabIndex = 1;
            this.ShelfWidthTextBox.Text = "190";
            this.ShelfWidthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxOnlyDouble);
            this.ShelfWidthTextBox.Leave += new System.EventHandler(this.TextBoxLeaveVerify);
            // 
            // ShelfLegsHeightTextBox
            // 
            this.ShelfLegsHeightTextBox.Location = new System.Drawing.Point(6, 35);
            this.ShelfLegsHeightTextBox.Name = "ShelfLegsHeightTextBox";
            this.ShelfLegsHeightTextBox.Size = new System.Drawing.Size(100, 20);
            this.ShelfLegsHeightTextBox.TabIndex = 3;
            this.ShelfLegsHeightTextBox.Text = "40";
            this.ShelfLegsHeightTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxOnlyDouble);
            this.ShelfLegsHeightTextBox.Leave += new System.EventHandler(this.TextBoxLeaveVerify);
            // 
            // ConstructButton
            // 
            this.ConstructButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstructButton.Location = new System.Drawing.Point(12, 239);
            this.ConstructButton.Name = "ConstructButton";
            this.ConstructButton.Size = new System.Drawing.Size(546, 45);
            this.ConstructButton.TabIndex = 7;
            this.ConstructButton.Text = "Построить";
            this.ConstructButton.UseVisualStyleBackColor = true;
            this.ConstructButton.Click += new System.EventHandler(this.ConstructButton_Click);
            // 
            // SetMinButton
            // 
            this.SetMinButton.Location = new System.Drawing.Point(187, 19);
            this.SetMinButton.Name = "SetMinButton";
            this.SetMinButton.Size = new System.Drawing.Size(79, 33);
            this.SetMinButton.TabIndex = 5;
            this.SetMinButton.Text = "Min";
            this.SetMinButton.UseVisualStyleBackColor = true;
            this.SetMinButton.Click += new System.EventHandler(this.SetMinButton_Click);
            // 
            // SetMaxButton
            // 
            this.SetMaxButton.Location = new System.Drawing.Point(187, 61);
            this.SetMaxButton.Name = "SetMaxButton";
            this.SetMaxButton.Size = new System.Drawing.Size(79, 33);
            this.SetMaxButton.TabIndex = 6;
            this.SetMaxButton.Text = "Max";
            this.SetMaxButton.UseVisualStyleBackColor = true;
            this.SetMaxButton.Click += new System.EventHandler(this.SetMaxButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ShelfMaxHeight);
            this.groupBox1.Controls.Add(this.ShelfMinHeight);
            this.groupBox1.Controls.Add(this.ShelfMaxWidth);
            this.groupBox1.Controls.Add(this.ShelfMinWidth);
            this.groupBox1.Controls.Add(this.ShelfMaxLength);
            this.groupBox1.Controls.Add(this.ShelfMinLength);
            this.groupBox1.Controls.Add(this.ShelfHeight);
            this.groupBox1.Controls.Add(this.ShelfWidth);
            this.groupBox1.Controls.Add(this.ShelfLength);
            this.groupBox1.Controls.Add(this.ShelfLengthTextBox);
            this.groupBox1.Controls.Add(this.ShelfHeightTextBox);
            this.groupBox1.Controls.Add(this.ShelfWidthTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(546, 115);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Характеристики полок";
            // 
            // ShelfMaxHeight
            // 
            this.ShelfMaxHeight.AutoSize = true;
            this.ShelfMaxHeight.Location = new System.Drawing.Point(377, 91);
            this.ShelfMaxHeight.Name = "ShelfMaxHeight";
            this.ShelfMaxHeight.Size = new System.Drawing.Size(161, 13);
            this.ShelfMaxHeight.TabIndex = 12;
            this.ShelfMaxHeight.Text = "Максимальная высота: 40 мм";
            // 
            // ShelfMinHeight
            // 
            this.ShelfMinHeight.AutoSize = true;
            this.ShelfMinHeight.Location = new System.Drawing.Point(377, 68);
            this.ShelfMinHeight.Name = "ShelfMinHeight";
            this.ShelfMinHeight.Size = new System.Drawing.Size(155, 13);
            this.ShelfMinHeight.TabIndex = 11;
            this.ShelfMinHeight.Text = "Минимальная высота: 20 мм";
            // 
            // ShelfMaxWidth
            // 
            this.ShelfMaxWidth.AutoSize = true;
            this.ShelfMaxWidth.Location = new System.Drawing.Point(197, 91);
            this.ShelfMaxWidth.Name = "ShelfMaxWidth";
            this.ShelfMaxWidth.Size = new System.Drawing.Size(168, 13);
            this.ShelfMaxWidth.TabIndex = 10;
            this.ShelfMaxWidth.Text = "Максимальная ширина: 220 мм";
            // 
            // ShelfMinWidth
            // 
            this.ShelfMinWidth.AutoSize = true;
            this.ShelfMinWidth.Location = new System.Drawing.Point(197, 68);
            this.ShelfMinWidth.Name = "ShelfMinWidth";
            this.ShelfMinWidth.Size = new System.Drawing.Size(162, 13);
            this.ShelfMinWidth.TabIndex = 9;
            this.ShelfMinWidth.Text = "Минимальная ширина: 190 мм";
            // 
            // ShelfMaxLength
            // 
            this.ShelfMaxLength.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShelfMaxLength.AutoSize = true;
            this.ShelfMaxLength.Location = new System.Drawing.Point(17, 91);
            this.ShelfMaxLength.Name = "ShelfMaxLength";
            this.ShelfMaxLength.Size = new System.Drawing.Size(160, 13);
            this.ShelfMaxLength.TabIndex = 8;
            this.ShelfMaxLength.Text = "Максимальная длина: 480 мм";
            // 
            // ShelfMinLength
            // 
            this.ShelfMinLength.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShelfMinLength.AutoSize = true;
            this.ShelfMinLength.Location = new System.Drawing.Point(17, 68);
            this.ShelfMinLength.Name = "ShelfMinLength";
            this.ShelfMinLength.Size = new System.Drawing.Size(154, 13);
            this.ShelfMinLength.TabIndex = 7;
            this.ShelfMinLength.Text = "Минимальная длина: 420 мм";
            // 
            // ShelfHeight
            // 
            this.ShelfHeight.AutoSize = true;
            this.ShelfHeight.Location = new System.Drawing.Point(377, 29);
            this.ShelfHeight.Name = "ShelfHeight";
            this.ShelfHeight.Size = new System.Drawing.Size(78, 13);
            this.ShelfHeight.TabIndex = 6;
            this.ShelfHeight.Text = "Высота полки";
            // 
            // ShelfWidth
            // 
            this.ShelfWidth.AutoSize = true;
            this.ShelfWidth.Location = new System.Drawing.Point(197, 29);
            this.ShelfWidth.Name = "ShelfWidth";
            this.ShelfWidth.Size = new System.Drawing.Size(79, 13);
            this.ShelfWidth.TabIndex = 5;
            this.ShelfWidth.Text = "Ширина полки";
            // 
            // ShelfLength
            // 
            this.ShelfLength.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShelfLength.AutoSize = true;
            this.ShelfLength.Location = new System.Drawing.Point(17, 29);
            this.ShelfLength.Name = "ShelfLength";
            this.ShelfLength.Size = new System.Drawing.Size(73, 13);
            this.ShelfLength.TabIndex = 4;
            this.ShelfLength.Text = "Длина полки";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ShelfBindingMaxHeight);
            this.groupBox2.Controls.Add(this.ShelfBindingMinHeight);
            this.groupBox2.Controls.Add(this.ShelfLegsMaxHeight);
            this.groupBox2.Controls.Add(this.ShelfLegsMinHeight);
            this.groupBox2.Controls.Add(this.ShelfBindingHeight);
            this.groupBox2.Controls.Add(this.ShelfLegsHeight);
            this.groupBox2.Controls.Add(this.ShelfLegsHeightTextBox);
            this.groupBox2.Controls.Add(this.ShelfBindingHeightTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 96);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Характеристики ножек и креплений";
            // 
            // ShelfBindingMaxHeight
            // 
            this.ShelfBindingMaxHeight.AutoSize = true;
            this.ShelfBindingMaxHeight.Location = new System.Drawing.Point(144, 78);
            this.ShelfBindingMaxHeight.Name = "ShelfBindingMaxHeight";
            this.ShelfBindingMaxHeight.Size = new System.Drawing.Size(127, 13);
            this.ShelfBindingMaxHeight.TabIndex = 13;
            this.ShelfBindingMaxHeight.Text = "Максимальная: 180 мм";
            // 
            // ShelfBindingMinHeight
            // 
            this.ShelfBindingMinHeight.AutoSize = true;
            this.ShelfBindingMinHeight.Location = new System.Drawing.Point(144, 58);
            this.ShelfBindingMinHeight.Name = "ShelfBindingMinHeight";
            this.ShelfBindingMinHeight.Size = new System.Drawing.Size(121, 13);
            this.ShelfBindingMinHeight.TabIndex = 13;
            this.ShelfBindingMinHeight.Text = "Минимальная: 160 мм";
            // 
            // ShelfLegsMaxHeight
            // 
            this.ShelfLegsMaxHeight.AutoSize = true;
            this.ShelfLegsMaxHeight.Location = new System.Drawing.Point(6, 78);
            this.ShelfLegsMaxHeight.Name = "ShelfLegsMaxHeight";
            this.ShelfLegsMaxHeight.Size = new System.Drawing.Size(121, 13);
            this.ShelfLegsMaxHeight.TabIndex = 13;
            this.ShelfLegsMaxHeight.Text = "Максимальная: 70 мм";
            // 
            // ShelfLegsMinHeight
            // 
            this.ShelfLegsMinHeight.AutoSize = true;
            this.ShelfLegsMinHeight.Location = new System.Drawing.Point(6, 58);
            this.ShelfLegsMinHeight.Name = "ShelfLegsMinHeight";
            this.ShelfLegsMinHeight.Size = new System.Drawing.Size(115, 13);
            this.ShelfLegsMinHeight.TabIndex = 13;
            this.ShelfLegsMinHeight.Text = "Минимальная: 40 мм";
            // 
            // ShelfBindingHeight
            // 
            this.ShelfBindingHeight.AutoSize = true;
            this.ShelfBindingHeight.Location = new System.Drawing.Point(144, 19);
            this.ShelfBindingHeight.Name = "ShelfBindingHeight";
            this.ShelfBindingHeight.Size = new System.Drawing.Size(102, 13);
            this.ShelfBindingHeight.TabIndex = 14;
            this.ShelfBindingHeight.Text = "Высота креплений";
            // 
            // ShelfLegsHeight
            // 
            this.ShelfLegsHeight.AutoSize = true;
            this.ShelfLegsHeight.Location = new System.Drawing.Point(6, 19);
            this.ShelfLegsHeight.Name = "ShelfLegsHeight";
            this.ShelfLegsHeight.Size = new System.Drawing.Size(80, 13);
            this.ShelfLegsHeight.TabIndex = 13;
            this.ShelfLegsHeight.Text = "Высота ножек";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.MaxSizeLabel);
            this.groupBox3.Controls.Add(this.MinSizeLabel);
            this.groupBox3.Controls.Add(this.SetMinButton);
            this.groupBox3.Controls.Add(this.SetMaxButton);
            this.groupBox3.Location = new System.Drawing.Point(290, 137);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 96);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Сброс размеров";
            // 
            // MaxSizeLabel
            // 
            this.MaxSizeLabel.AutoSize = true;
            this.MaxSizeLabel.Location = new System.Drawing.Point(6, 71);
            this.MaxSizeLabel.Name = "MaxSizeLabel";
            this.MaxSizeLabel.Size = new System.Drawing.Size(135, 13);
            this.MaxSizeLabel.TabIndex = 9;
            this.MaxSizeLabel.Text = "Максимальные размеры";
            // 
            // MinSizeLabel
            // 
            this.MinSizeLabel.AutoSize = true;
            this.MinSizeLabel.Location = new System.Drawing.Point(6, 29);
            this.MinSizeLabel.Name = "MinSizeLabel";
            this.MinSizeLabel.Size = new System.Drawing.Size(129, 13);
            this.MinSizeLabel.TabIndex = 8;
            this.MinSizeLabel.Text = "Минимальные размеры";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 301);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ConstructButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(586, 340);
            this.MinimumSize = new System.Drawing.Size(586, 340);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Создание Этажерки";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ShelfLengthTextBox;
        private System.Windows.Forms.TextBox ShelfHeightTextBox;
        private System.Windows.Forms.TextBox ShelfBindingHeightTextBox;
        private System.Windows.Forms.TextBox ShelfWidthTextBox;
        private System.Windows.Forms.TextBox ShelfLegsHeightTextBox;
        private System.Windows.Forms.Button ConstructButton;
        private System.Windows.Forms.Button SetMinButton;
        private System.Windows.Forms.Button SetMaxButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label MinSizeLabel;
        private System.Windows.Forms.Label MaxSizeLabel;
        private System.Windows.Forms.Label ShelfMaxHeight;
        private System.Windows.Forms.Label ShelfMinHeight;
        private System.Windows.Forms.Label ShelfMaxWidth;
        private System.Windows.Forms.Label ShelfMinWidth;
        private System.Windows.Forms.Label ShelfMaxLength;
        private System.Windows.Forms.Label ShelfMinLength;
        private System.Windows.Forms.Label ShelfHeight;
        private System.Windows.Forms.Label ShelfWidth;
        private System.Windows.Forms.Label ShelfLength;
        private System.Windows.Forms.Label ShelfBindingMaxHeight;
        private System.Windows.Forms.Label ShelfBindingMinHeight;
        private System.Windows.Forms.Label ShelfLegsMaxHeight;
        private System.Windows.Forms.Label ShelfLegsMinHeight;
        private System.Windows.Forms.Label ShelfBindingHeight;
        private System.Windows.Forms.Label ShelfLegsHeight;
    }
}

