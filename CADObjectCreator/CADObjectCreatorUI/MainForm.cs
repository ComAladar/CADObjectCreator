using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CADObjectCreatorParameters;
using CADObjectCreatorBuilder;

namespace CADObjectCreatorUI
{
    public partial class MainForm : Form
    {
        private Parameters Parameters = new Parameters();
        private Kompas3DBuilder KompasBuilder = new Kompas3DBuilder();
        private readonly Dictionary<TextBox, Action<ParametersList, string>> _textBoxDictionary;

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
                MessageBox.Show("Проверьте введенные параметры!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void TextBoxSetColor()
        {
            ShelfLengthTextBox.BackColor = Color.White;
            ShelfWidthTextBox.BackColor = Color.White;
            ShelfHeightTextBox.BackColor = Color.White;
            ShelfLegsHeightTextBox.BackColor = Color.White;
            ShelfBindingHeightTextBox.BackColor = Color.White;
        }

        private string DoubleTypeCheck(string value)
        {
            var match = Regex.Match(value, @"[0-9]+\,[0-9]+");
            if (match.Success)
            {
                value = match.Value;
            }
            return value;
        }

        
        public MainForm()
        {
            InitializeComponent();
            LabelTextFillUp();
            TextBoxFillUp();
            _textBoxDictionary = new Dictionary<TextBox, Action<ParametersList, string>>()
            {
                {
                    ShelfLengthTextBox,
                    (ParametersList tempList, string text) => {tempList["ShelfLength"] = Double.Parse(text);}
                },
                {
                    ShelfWidthTextBox,
                    (ParametersList tempList, string text) => {tempList["ShelfWidth"] = Double.Parse(text);}
                },
                {
                    ShelfHeightTextBox,
                    (ParametersList tempList, string text) => {tempList["ShelfHeight"] = Double.Parse(text);}
                },
                {
                    ShelfLegsHeightTextBox,
                    (ParametersList tempList, string text) => {tempList["ShelfLegsHeight"] = Double.Parse(text);}
                },
                {
                    ShelfBindingHeightTextBox,
                    (ParametersList tempList, string text) => {tempList["ShelfBindingHeight"] = Double.Parse(text);}
                }
            };

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
            TextBoxSetColor();
        }

        private void SetMaxButton_Click(object sender, EventArgs e)
        {
            ShelfLengthTextBox.Text = Parameters.ParametersList["ShelfLength",true].ToString();
            ShelfWidthTextBox.Text = Parameters.ParametersList["ShelfWidth",true].ToString();
            ShelfHeightTextBox.Text = Parameters.ParametersList["ShelfHeight",true].ToString();
            ShelfLegsHeightTextBox.Text = Parameters.ParametersList["ShelfLegsHeight",true].ToString();
            ShelfBindingHeightTextBox.Text = Parameters.ParametersList["ShelfBindingHeight",true].ToString();
            TextBoxSetColor();
        }

        private void ConstructButton_Click(object sender, EventArgs e)
        {
            VerifyParameters();
            KompasBuilder.BuildObject(Parameters);
        }

        private void TextBoxLeaveVerify(object sender, EventArgs e)
        {
            var currentTextBox = (TextBox)sender;
            currentTextBox.Text=DoubleTypeCheck(currentTextBox.Text);
            var currentAction = _textBoxDictionary[currentTextBox];
            if (currentTextBox.Text != String.Empty)
            {
                try
                {
                    currentAction.Invoke(Parameters.ParametersList, currentTextBox.Text);
                    currentTextBox.BackColor = Color.White;
                }
                catch (ArgumentException exception)
                {
                    currentTextBox.BackColor = Color.DarkRed;
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void TextBoxOnlyDouble(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number !=',') // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
