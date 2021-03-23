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
        private Parameters _parameters = new Parameters();
        private Kompas3DBuilder _kompasBuilder = new Kompas3DBuilder();
        private readonly Dictionary<TextBox, Action<Parameters, string>> _textBoxDictionary;

        private void LabelTextFillUp()
        {
            ShelfMinLength.Text = "Минимальная: " + _parameters.GetMinParameter("ShelfLength") + " мм";
            ShelfMaxLength.Text = "Максимальная: " + _parameters.GetMaxParameter("ShelfLength") + " мм";
            ShelfMinWidth.Text = "Минимальная: " + _parameters.GetMinParameter("ShelfWidth") + " мм";
            ShelfMaxWidth.Text = "Максимальная: " + _parameters.GetMaxParameter("ShelfWidth") + " мм";
            ShelfMinHeight.Text = "Минимальная: " + _parameters.GetMinParameter("ShelfHeight") + " мм";
            ShelfMaxHeight.Text = "Максимальная: " + _parameters.GetMaxParameter("ShelfHeight") + " мм";
            ShelfLegsMinHeight.Text ="Минимальная: " + _parameters.GetMinParameter("ShelfLegsHeight") + " мм";
            ShelfLegsMaxHeight.Text ="Максимальная: " + _parameters.GetMaxParameter("ShelfLegsHeight") + " мм";
            ShelfBindingMinHeight.Text ="Минимальная: " + _parameters.GetMinParameter("ShelfBindingHeight") + " мм";
            ShelfBindingMaxHeight.Text ="Максимальная: " + _parameters.GetMaxParameter("ShelfBindingHeight") + " мм";
        }

        private void TextBoxFillUp()
        {
            ShelfLengthTextBox.Text = _parameters["ShelfLength"].ToString();
            ShelfWidthTextBox.Text = _parameters["ShelfWidth"].ToString();
            ShelfHeightTextBox.Text = _parameters["ShelfHeight"].ToString();
            ShelfLegsHeightTextBox.Text = _parameters["ShelfLegsHeight"].ToString();
            ShelfBindingHeightTextBox.Text = _parameters["ShelfBindingHeight"].ToString();
        }

        private void VerifyParameters()
        {
            try
            {
                _parameters["ShelfLength"] = Double.Parse(ShelfLengthTextBox.Text);
                _parameters["ShelfWidth"] = Double.Parse(ShelfWidthTextBox.Text);
                _parameters["ShelfHeight"] = Double.Parse(ShelfHeightTextBox.Text);
                _parameters["ShelfLegsHeight"] = Double.Parse(ShelfLegsHeightTextBox.Text);
                _parameters["ShelfBindingHeight"] = Double.Parse(ShelfBindingHeightTextBox.Text);
                _parameters.ShelfBootsPlaceLength = _parameters["ShelfLength"];
                _parameters.ShelfBootsPlaceWidth = _parameters["ShelfWidth"];
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
            _textBoxDictionary = new Dictionary<TextBox, Action<Parameters, string>>()
            {
                {
                    ShelfLengthTextBox,
                    (tempList, text) => {tempList["ShelfLength"] = Double.Parse(text);}
                },
                {
                    ShelfWidthTextBox,
                    (tempList, text) => {tempList["ShelfWidth"] = Double.Parse(text);}
                },
                {
                    ShelfHeightTextBox,
                    (tempList, text) => {tempList["ShelfHeight"] = Double.Parse(text);}
                },
                {
                    ShelfLegsHeightTextBox,
                    (tempList, text) => {tempList["ShelfLegsHeight"] = Double.Parse(text);}
                },
                {
                    ShelfBindingHeightTextBox,
                    (tempList, text) => {tempList["ShelfBindingHeight"] = Double.Parse(text);}
                }
            };

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void SetMinButton_Click(object sender, EventArgs e)
        {
            ShelfLengthTextBox.Text = _parameters.GetMinParameter("ShelfLength").ToString();
            ShelfWidthTextBox.Text = _parameters.GetMinParameter("ShelfWidth").ToString();
            ShelfHeightTextBox.Text = _parameters.GetMinParameter("ShelfHeight").ToString();
            ShelfLegsHeightTextBox.Text = _parameters.GetMinParameter("ShelfLegsHeight").ToString();
            ShelfBindingHeightTextBox.Text = _parameters.GetMinParameter("ShelfBindingHeight").ToString();
            TextBoxSetColor();
        }

        private void SetMaxButton_Click(object sender, EventArgs e)
        {
            ShelfLengthTextBox.Text = _parameters.GetMaxParameter("ShelfLength").ToString();
            ShelfWidthTextBox.Text = _parameters.GetMaxParameter("ShelfWidth").ToString();
            ShelfHeightTextBox.Text = _parameters.GetMaxParameter("ShelfHeight").ToString();
            ShelfLegsHeightTextBox.Text = _parameters.GetMaxParameter("ShelfLegsHeight").ToString();
            ShelfBindingHeightTextBox.Text = _parameters.GetMaxParameter("ShelfBindingHeight").ToString();
            TextBoxSetColor();
        }

        private void ConstructButton_Click(object sender, EventArgs e)
        {
            VerifyParameters();
            _kompasBuilder.BuildObject(_parameters);
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
                    currentAction.Invoke(_parameters, currentTextBox.Text);
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
