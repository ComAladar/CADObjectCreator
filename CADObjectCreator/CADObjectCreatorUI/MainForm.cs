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

        private readonly Dictionary<TextBox, ParametersName> _textBoxDictionaryValues;


        /// <summary>
        /// Поле хранит удачность верификации параметров.
        /// </summary>
        private bool _isParametersVefified;


        /// <summary>
        /// Метод заполняет TextBox начальными параметрами этажерки.
        /// </summary>
        private void TextBoxFillUp()
        {
            //TODO: Duplication
            foreach (var parametersName in _textBoxDictionaryValues)
            {
                parametersName.Key.Text = 
                    _parameters[parametersName.Value].Value.ToString();
            }
        }

        /// <summary>
        /// Метод проверяет и передает параметры для построения этажерки.
        /// </summary>
        private void VerifyParameters()
        {
            try
            {
                foreach (var parametersName in _textBoxDictionaryValues)
                {
                    _parameters[parametersName.Value].Value = 
                        Double.Parse(parametersName.Key.Text);
                }

                _parameters.ShelfBootsPlaceLength =
                    _parameters[ParametersName.ShelfLength].Value;
                _parameters.ShelfBootsPlaceWidth =
                    _parameters[ParametersName.ShelfWidth].Value;
                _isParametersVefified = true;
            }
            catch (ArgumentException)
            {
                _isParametersVefified = false;
                MessageBox.Show("Проверьте введенные параметры!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Метод меняет цвет всех TextBox на белый.
        /// </summary>
        private void TextBoxSetColor()
        {
            var groupBoxes = Controls.OfType<GroupBox>();
            for (int i = 0; i < groupBoxes.Count(); i++)
            {
                var groupBox = groupBoxes.ElementAt(i);
                foreach (var textBox in groupBox.Controls.OfType<TextBox>())
                {
                    textBox.BackColor = Color.White;
                }
            }
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

            _textBoxDictionaryValues = new Dictionary<TextBox, ParametersName>()
            {
                {ShelfLengthTextBox, ParametersName.ShelfLength},
                {ShelfWidthTextBox, ParametersName.ShelfWidth},
                {ShelfHeightTextBox,ParametersName.ShelfHeight},
                {ShelfLegsHeightTextBox,ParametersName.ShelfLegsHeight},
                {ShelfBindingHeightTextBox,ParametersName.ShelfBindingHeight}
            };

            TextBoxFillUp();
            LabelMinMaxFillUp();
        }

        /// <summary>
        /// Метод задающий минимальный значения этажерки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetMinButton_Click(object sender, EventArgs e)
        {
            //TODO: Duplication
            foreach (var parametersName in _textBoxDictionaryValues)
            {
                parametersName.Key.Text = _parameters[parametersName.Value].Min.ToString();
            }

            TextBoxSetColor();
        }

        /// <summary>
        /// Метод задающий максимальный значения этажерки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetMaxButton_Click(object sender, EventArgs e)
        {
            //TODO: Duplication
            foreach (var parametersName in _textBoxDictionaryValues)
            {
                parametersName.Key.Text = _parameters[parametersName.Value].Max.ToString();
            }

            TextBoxSetColor();
        }

        /// <summary>
        /// Обработчик нажатия кнопки. Отвечает за построение детали.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConstructButton_Click(object sender, EventArgs e)
        {
            VerifyParameters();
            if (_isParametersVefified)
            {
                _kompasBuilder.BuildObject(_parameters,checkBoxLeft.Checked,checkBoxRight.Checked);
            }
        }

        /// <summary>
        /// Метод проверки правильности ввода значений в TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLeaveVerify(object sender, EventArgs e)
        {
            var currentTextBox = (TextBox) sender;
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
        /// Заполняет минимальные и максимальные значения в Label.
        /// </summary>
        private void LabelMinMaxFillUp()
        {
            var dictionary = new Dictionary<Label, ParametersName>()
            {
                {ShelfMinLength, ParametersName.ShelfLength},
                {ShelfMinWidth, ParametersName.ShelfWidth},
                {ShelfMinHeight,ParametersName.ShelfHeight},
                {ShelfLegsMinHeight,ParametersName.ShelfLegsHeight},
                {ShelfBindingMinHeight,ParametersName.ShelfBindingHeight}
            };

            foreach (var parametersName in dictionary)
            {
                parametersName.Key.Text = "Минимальная: " 
                                          + _parameters[parametersName.Value].Min 
                                          + " мм";
            }

            dictionary = new Dictionary<Label, ParametersName>()
            {
                {ShelfMaxLength, ParametersName.ShelfLength},
                {ShelfMaxWidth, ParametersName.ShelfWidth},
                {ShelfMaxHeight,ParametersName.ShelfHeight},
                {ShelfLegsMaxHeight,ParametersName.ShelfLegsHeight},
                {ShelfBindingMaxHeight,ParametersName.ShelfBindingHeight}
            };

            foreach (var parametersName in dictionary)
            {
                parametersName.Key.Text = "Максимальная: " 
                                          + _parameters[parametersName.Value].Max 
                                          + " мм";
            }
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
                && number != 8 && number != ',') // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

    }
}
