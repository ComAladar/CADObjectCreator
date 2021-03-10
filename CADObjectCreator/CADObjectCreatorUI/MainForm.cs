using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CADObjectCreatorParameters;

namespace CADObjectCreatorUI
{
    public partial class MainForm : Form
    {
        private Parameters Parameters = new Parameters();

        private void LabelTextFillUp()
        {
            ShelfMinLength.Text = "Минимальная длина: " + Parameters.ParametersList["ShelfLength", false] + " мм";
            ShelfMaxLength.Text = "Максимальная длина: " + Parameters.ParametersList["ShelfLength", true] + " мм";
            ShelfMinWidth.Text = "Минимальная ширина: " + Parameters.ParametersList["ShelfWidth", false] + " мм";
            ShelfMaxWidth.Text = "Максимальная ширина: " + Parameters.ParametersList["ShelfWidth", true] + " мм";
            ShelfMinHeight.Text = "Минимальная высота: " + Parameters.ParametersList["ShelfHeight", false] + " мм";
            ShelfMaxHeight.Text = "Максимальная высота: " + Parameters.ParametersList["ShelfHeight", true] + " мм";
            ShelfLegsMinHeight.Text ="Минимальная: " + Parameters.ParametersList["ShelfLegsHeight",false] + " мм";
            ShelfLegsMaxHeight.Text ="Максимальная: " + Parameters.ParametersList["ShelfLegsHeight",true] + " мм";
            ShelfBindingMinHeight.Text ="Минимальная: " + Parameters.ParametersList["ShelfBindingHeight",false] + " мм";
            ShelfBindingMaxHeight.Text ="Максимальная: " + Parameters.ParametersList["ShelfBindingHeight", true] + " мм";
        }

        private void TextBoxFillUp()
        {
            ShelfLengthTextBox.Text =Parameters.ParametersList["ShelfLength"].ToString();
            ShelfWidthTextBox.Text =Parameters.ParametersList["ShelfWidth"].ToString();
            ShelfHeightTextBox.Text =Parameters.ParametersList["ShelfHeight"].ToString();
            ShelfLegsHeightTextBox.Text = Parameters.ParametersList["ShelfLegsHeight"].ToString();
            ShelfBindingHeightTextBox.Text =Parameters.ParametersList["ShelfBindingHeight"].ToString();
        }

        private void VerifyParameters()
        {
            try
            {
                Parameters.ParametersList["ShelfLength"] = Double.Parse(ShelfLengthTextBox.Text);
                Parameters.ParametersList["ShelfWidth"] = Double.Parse(ShelfWidthTextBox.Text);
                Parameters.ParametersList["ShelfHeight"] = Double.Parse(ShelfHeightTextBox.Text);
                Parameters.ParametersList["ShelfLegsHeight"] = Double.Parse(ShelfLegsHeightTextBox.Text);
                Parameters.ParametersList["ShelfBindingHeight"] = Double.Parse(ShelfBindingHeightTextBox.Text);
                Parameters.DependentParameters.ShelfBootsPlaceLength = Parameters.ParametersList["ShelfLength"];
                Parameters.DependentParameters.ShelfBootsPlaceWidth = Parameters.ParametersList["ShelfWidth"];
            }
            catch(ArgumentException exception)
            {
                MessageBox.Show(exception.Message.ToString());
            }
        }

 

        public MainForm()
        {
            InitializeComponent();
            LabelTextFillUp();
            TextBoxFillUp();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void SetMinButton_Click(object sender, EventArgs e)
        {
            ShelfLengthTextBox.Text = Parameters.ParametersList["ShelfLength",false].ToString();
            ShelfWidthTextBox.Text = Parameters.ParametersList["ShelfWidth",false].ToString();
            ShelfHeightTextBox.Text = Parameters.ParametersList["ShelfHeight",false].ToString();
            ShelfLegsHeightTextBox.Text = Parameters.ParametersList["ShelfLegsHeight",false].ToString();
            ShelfBindingHeightTextBox.Text = Parameters.ParametersList["ShelfBindingHeight",false].ToString();
        }

        private void SetMaxButton_Click(object sender, EventArgs e)
        {
            ShelfLengthTextBox.Text = Parameters.ParametersList["ShelfLength",true].ToString();
            ShelfWidthTextBox.Text = Parameters.ParametersList["ShelfWidth",true].ToString();
            ShelfHeightTextBox.Text = Parameters.ParametersList["ShelfHeight",true].ToString();
            ShelfLegsHeightTextBox.Text = Parameters.ParametersList["ShelfLegsHeight",true].ToString();
            ShelfBindingHeightTextBox.Text = Parameters.ParametersList["ShelfBindingHeight",true].ToString();
        }

        private void ConstructButton_Click(object sender, EventArgs e)
        {
            VerifyParameters();

        }
    }
}
