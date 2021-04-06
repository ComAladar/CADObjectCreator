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
        /// <summary>
        /// Поле хранит экземпляр класса параметров.
        /// </summary>
        private Parameters _parameters = new Parameters();

        /// <summary>
        /// Поле хранит экземпляр класса builder.
        /// </summary>
        private Kompas3DBuilder _kompasBuilder = new Kompas3DBuilder();

        /// <summary>
        /// Поле хранит словарь для заполнения TextBox.
        /// </summary>
        private readonly Dictionary<TextBox, Action<Parameters, string>> _textBoxDictionary;

        /// <summary>
        /// Поле хранит словарь для заполнения Label.
        /// </summary>
        private readonly Dictionary<Label, Func<Parameters, string>> _labelDictionary;

        /// <summary>
        /// Метод заполняет TextBox начальными параметрами этажерки.
        /// </summary>
        private void TextBoxFillUp()
        {
            ShelfLengthTextBox.Text = 
                _parameters[ParametersName.ShelfLength].Value.ToString();
            ShelfWidthTextBox.Text = 
                _parameters[ParametersName.ShelfWidth].Value.ToString();
            ShelfHeightTextBox.Text = 
                _parameters[ParametersName.ShelfHeight].Value.ToString();
            ShelfLegsHeightTextBox.Text = 
                _parameters[ParametersName.ShelfLegsHeight].Value.ToString();
            ShelfBindingHeightTextBox.Text = 
                _parameters[ParametersName.ShelfBindingHeight].Value.ToString();
        }

        /// <summary>
        /// Метод проверяет и передает параметры для построения этажерки.
        /// </summary>
        private void VerifyParameters()
        {
            try
            {
                _parameters[ParametersName.ShelfLength].Value = 
                    Double.Parse(ShelfLengthTextBox.Text);
                _parameters[ParametersName.ShelfWidth].Value = 
                    Double.Parse(ShelfWidthTextBox.Text);
                _parameters[ParametersName.ShelfHeight].Value = 
                    Double.Parse(ShelfHeightTextBox.Text);
                _parameters[ParametersName.ShelfLegsHeight].Value = 
                    Double.Parse(ShelfLegsHeightTextBox.Text);
                _parameters[ParametersName.ShelfBindingHeight].Value = 
                    Double.Parse(ShelfBindingHeightTextBox.Text);
                _parameters.ShelfBootsPlaceLength = 
                    _parameters[ParametersName.ShelfLength].Value;
                _parameters.ShelfBootsPlaceWidth = 
                    _parameters[ParametersName.ShelfWidth].Value;
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Проверьте введенные параметры!",
                    "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Метод меняет цвет всех TextBox на белый.
        /// </summary>
        private void TextBoxSetColor()
        {
            ShelfLengthTextBox.BackColor = Color.White;
            ShelfWidthTextBox.BackColor = Color.White;
            ShelfHeightTextBox.BackColor = Color.White;
            ShelfLegsHeightTextBox.BackColor = Color.White;
            ShelfBindingHeightTextBox.BackColor = Color.White;
        }

        /// <summary>
        /// Метод проверяет правильность ввода данных типа double.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
            TextBoxFillUp();
            _textBoxDictionary = new Dictionary<TextBox, Action<Parameters, string>>()
            {
                {
                    ShelfLengthTextBox,
                    (tempList, text) =>
                    {
                        tempList[ParametersName.ShelfLength].Value = 
                            Double.Parse(text);
                    }
                },
                {
                    ShelfWidthTextBox,
                    (tempList, text) =>
                    {
                        tempList[ParametersName.ShelfWidth].Value = 
                            Double.Parse(text);
                    }
                },
                {
                    ShelfHeightTextBox,
                    (tempList, text) =>
                    {
                        tempList[ParametersName.ShelfHeight].Value = 
                            Double.Parse(text);
                    }
                },
                {
                    ShelfLegsHeightTextBox,
                    (tempList, text) =>
                    {
                        tempList[ParametersName.ShelfLegsHeight].Value = 
                            Double.Parse(text);
                    }
                },
                {
                    ShelfBindingHeightTextBox,
                    (tempList, text) =>
                    {
                        tempList[ParametersName.ShelfBindingHeight].Value = 
                            Double.Parse(text);
                    }
                }
            };

            _labelDictionary = new Dictionary<Label, Func<Parameters, string>>()
            {
                {
                    ShelfMinLength,
                    (tempList) => "Минимальная: " + tempList[ParametersName.ShelfLength].Min + "мм"
                },
                {
                    ShelfMinWidth,
                    (tempList) =>
                    {
                        
                        return    "Минимальная: " + tempList[ParametersName.ShelfWidth].Min + "мм";
                    }
                },
                {
                    ShelfMinHeight,
                    (tempList) => "Минимальная: " + tempList[ParametersName.ShelfHeight].Min + "мм"
                },
                {
                    ShelfLegsMinHeight,
                    (tempList) => "Минимальная: " + tempList[ParametersName.ShelfLegsHeight].Min + "мм"
                },
                {
                    ShelfBindingMinHeight,
                    (tempList) => "Минимальная: " + tempList[ParametersName.ShelfBindingHeight].Min + "мм"
                },
                {
                    ShelfMaxLength,
                    (tempList) => "Максимальная: " + tempList[ParametersName.ShelfLength].Max + "мм"
                },
                {
                    ShelfMaxWidth,
                    (tempList) => "Максимальная: " + tempList[ParametersName.ShelfWidth].Max + "мм"
                },
                {
                    ShelfMaxHeight,
                    (tempList) => "Максимальная: " + tempList[ParametersName.ShelfHeight].Max + "мм"
                },
                {
                    ShelfLegsMaxHeight,
                    (tempList) => "Максимальная: " + tempList[ParametersName.ShelfLegsHeight].Max + "мм"
                },
                {
                    ShelfBindingMaxHeight,
                    (tempList) => "Максимальная: " + tempList[ParametersName.ShelfBindingHeight].Max + "мм"
                }
            };
            foreach (Label tempLabel in this.Controls.OfType<Label>())
            {
                LabelTextFillUp(tempLabel);
            }
        }

        /// <summary>
        /// Метод задающий минимальный значения этажерки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetMinButton_Click(object sender, EventArgs e)
        {
            var dictionary = new Dictionary<TextBox, ParametersName>()
            {
                {ShelfLengthTextBox, ParametersName.ShelfLength},
                {ShelfWidthTextBox, ParametersName.ShelfWidth}
            };

            foreach (var parametersName in dictionary)
            {
                parametersName.Key.Text = _parameters[parametersName.Value].Min.ToString();
            }

            ShelfLengthTextBox.Text = 
                _parameters[ParametersName.ShelfLength].Min.ToString();
            ShelfWidthTextBox.Text = 
                _parameters[ParametersName.ShelfWidth].Min.ToString();
            ShelfHeightTextBox.Text = 
                _parameters[ParametersName.ShelfHeight].Min.ToString();
            ShelfLegsHeightTextBox.Text = 
                _parameters[ParametersName.ShelfLegsHeight].Min.ToString();
            ShelfBindingHeightTextBox.Text = 
                _parameters[ParametersName.ShelfBindingHeight].Min.ToString();
            TextBoxSetColor();
        }

        /// <summary>
        /// Метод задающий максимальный значения этажерки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetMaxButton_Click(object sender, EventArgs e)
        {
            ShelfLengthTextBox.Text = 
                _parameters[ParametersName.ShelfLength].Max.ToString();
            ShelfWidthTextBox.Text = 
                _parameters[ParametersName.ShelfWidth].Max.ToString();
            ShelfHeightTextBox.Text = 
                _parameters[ParametersName.ShelfHeight].Max.ToString();
            ShelfLegsHeightTextBox.Text = 
                _parameters[ParametersName.ShelfLegsHeight].Max.ToString();
            ShelfBindingHeightTextBox.Text = 
                _parameters[ParametersName.ShelfBindingHeight].Max.ToString();
            TextBoxSetColor();
        }

        private void ConstructButton_Click(object sender, EventArgs e)
        {
            VerifyParameters();
            _kompasBuilder.BuildObject(_parameters);
        }

        /// <summary>
        /// Метод проверки правильности ввода значений в TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLeaveVerify(object sender, EventArgs e)
        {
            var currentTextBox = (TextBox)sender;
            currentTextBox.Text = DoubleTypeCheck(currentTextBox.Text);
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

        /// <summary>
        /// Метод для заполнения всех Label с помощью словаря.
        /// </summary>
        /// <param name="tempLabel"></param>
        private void LabelTextFillUp(Label tempLabel)
        {
            var currentAction = _labelDictionary[tempLabel];
            tempLabel.Text = currentAction.Invoke(_parameters);
        }

        /// <summary>
        /// Обработчик возможных символов в TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxOnlyDouble(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) 
                && number != 8 && number !=',') // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
}
