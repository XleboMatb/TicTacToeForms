using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToeForm : Form
    {
        int slotsLeft = 8; //количество свободных клеток (8, потому что счёт с 0 начинается)
        Random randY = new Random(); //глобальный рандом создан, чтобы избежать 
        PictureBox[,] gridArray = new PictureBox[3, 3];
        Bitmap Xmark = new Bitmap(TicTacToe.Properties.Resources.Red_X_svg);
        Bitmap Omark = new Bitmap(TicTacToe.Properties.Resources.O_Jolle_insigna);
        public TicTacToeForm()
        {
            InitializeComponent();
            CreatePlayField();
        }

        private void ClickOnCell_Click(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridArray.GetLength(1); j++)
                {
                    if ((sender as PictureBox) == gridArray[i, j] & gridArray[i, j].Image == null) //это я нарандомил >3<
                    {
                        gridArray[i, j].Image = Xmark;
                        gridArray[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                        RandomTurn();
                        WinCondition();
                        break;
                    }
                    else if (((sender as PictureBox) == gridArray[i, j] & gridArray[i, j].Image != null)) //при нажатии на Picturebox с картинкой внутри ничего не делает 
                    {
                        return;
                    }

                }

            }
            slotsLeft--;
        }

        /// <summary>
        /// создаётся массив 3 на 3 (поле в крестиках-ноликах)
        /// массив заполняется PictureBox'ами, которым задаётся значение, цвет внутри и цвет границ. 
        /// то, где размещены PictureBox'ы определяется по индексам в массиве * высоту/длину PictureBox'а
        /// после чего PictureBox размещается на поле по заданным координатам.
        /// к размещённому PictureBox добавляется Event - MouseClick по классу MouseEventHandler, который отслеживает нажатия по созданному PictureBox'у. 
        /// </summary>
        private void CreatePlayField()
        {
            for (int i = 0; i < gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridArray.GetLength(1); j++)
                {
                    gridArray[i, j] = new PictureBox();
                    gridArray[i, j].Size = new Size(100, 100);
                    gridArray[i, j].BackColor = Color.White;
                    gridArray[i, j].BorderStyle = BorderStyle.FixedSingle;
                    gridArray[i, j].Location = new Point(i * gridArray[i, j].Size.Width + 10, j * gridArray[i, j].Size.Height + 10);
                    this.Controls.Add(gridArray[i, j]);
                    gridArray[i, j].MouseClick += new MouseEventHandler(ClickOnCell_Click);
                }
            }

        }
        /// <summary>
        ///метод, генерирующий рандомоное число от 0 до максимального значения массива.
        ///После чего делает "ход", вставляя по сгенерированным индексам картинку кружка.
        ///Если же данное сгенерированное значение имеет картинку внутри, то процесс повторяется до того момента, пока рандом не "нароллит" свободный индекс в массиве.
        ///после чего, если значение нароленно, то вызывает метод WinCondition, который смотрит, является ли данный ход выигрышным или нет. 
        /// </summary>
        public void RandomTurn()
        {
            while (true)
            {
                Random randX = new Random();
                int randomIndexX = randX.Next(0, gridArray.GetLength(0));
                int randomIndexY = randY.Next(0, gridArray.GetLength(1));
                if (gridArray[randomIndexX, randomIndexY].Image == null)
                {
                    gridArray[randomIndexX, randomIndexY].Image = Omark;
                    gridArray[randomIndexX, randomIndexY].SizeMode = PictureBoxSizeMode.StretchImage;
                    slotsLeft--;
                    break;
                }
                else
                {
                    if (gridArray[randomIndexX, randomIndexY].Image != null & slotsLeft > 0)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            WinCondition();

        }
        /// <summary>
        /// Я не знаю как это можно написать по-другому и правильно, но данный метод проверяет соответствует ли одна из комбинаций в клетках выигрышной или проигрышной позиции.
        /// Переменная slotsLeft нужна здесь, ибо если не указывать сколько свободных клеток осталось на поле - программа крашистя и/или выдаёт неправильный результат. 
        /// Также переменная была сделана потому, что я не знаю как вывести по другому оставшиеся свободные клетки в массиве. 
        /// 
        /// todo: разобраться как найти пустые элементы в массиве и вернуть их. 
        /// </summary>
        private void WinCondition()
        {
            if (gridArray[0, 0].Image == Xmark & gridArray[1, 1].Image == Xmark & gridArray[2, 2].Image == Xmark & slotsLeft >= -1 |
                gridArray[0, 2].Image == Xmark & gridArray[1, 1].Image == Xmark & gridArray[2, 0].Image == Xmark & slotsLeft >= -1 |
                gridArray[0, 0].Image == Xmark & gridArray[0, 1].Image == Xmark & gridArray[0, 2].Image == Xmark & slotsLeft >= -1 |
                gridArray[1, 0].Image == Xmark & gridArray[1, 1].Image == Xmark & gridArray[1, 2].Image == Xmark & slotsLeft >= -1 |
                gridArray[2, 0].Image == Xmark & gridArray[2, 1].Image == Xmark & gridArray[2, 2].Image == Xmark & slotsLeft >= -1 |
                gridArray[0, 0].Image == Xmark & gridArray[1, 0].Image == Xmark & gridArray[2, 0].Image == Xmark & slotsLeft >= -1 |
                gridArray[0, 1].Image == Xmark & gridArray[1, 1].Image == Xmark & gridArray[2, 1].Image == Xmark & slotsLeft >= -1 |
                gridArray[0, 2].Image == Xmark & gridArray[1, 2].Image == Xmark & gridArray[2, 2].Image == Xmark & slotsLeft >= -1)
            {
                MessageBox.Show("you win!");
                ResetTheField();
                return;
            }
            else if (gridArray[0, 0].Image == Omark & gridArray[1, 1].Image == Omark & gridArray[2, 2].Image == Omark & slotsLeft >= -1 |
                gridArray[0, 2].Image == Omark & gridArray[1, 1].Image == Omark & gridArray[2, 0].Image == Omark & slotsLeft >= -1 |
                gridArray[0, 0].Image == Omark & gridArray[0, 1].Image == Omark & gridArray[0, 2].Image == Omark & slotsLeft >= -1 |
                gridArray[1, 0].Image == Omark & gridArray[1, 1].Image == Omark & gridArray[1, 2].Image == Omark & slotsLeft >= -1 |
                gridArray[2, 0].Image == Omark & gridArray[2, 1].Image == Omark & gridArray[2, 2].Image == Omark & slotsLeft >= -1 |
                gridArray[0, 0].Image == Omark & gridArray[1, 0].Image == Omark & gridArray[2, 0].Image == Omark & slotsLeft >= -1 |
                gridArray[0, 1].Image == Omark & gridArray[1, 1].Image == Omark & gridArray[2, 1].Image == Omark & slotsLeft >= -1 |
                gridArray[0, 2].Image == Omark & gridArray[1, 2].Image == Omark & gridArray[2, 2].Image == Omark & slotsLeft >= -1)
            {
                MessageBox.Show("you lost!");
                ResetTheField();
                return;
            }
            else if (slotsLeft < 0)
            {
                MessageBox.Show("tie");
                ResetTheField();
            }
        }
        /// <summary>
        /// метод, который даёт всем зачениям Image в массиве PictureBox с названием gridArray значение null, 
        /// чтобы поля стали пустыми. 
        /// </summary>

        private void ResetTheField()
        {

            for (int i = 0; i < gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridArray.GetLength(1); j++)
                {
                    gridArray[i, j].Image = null;
                }
            }
            slotsLeft = 8;
            MessageBox.Show("the field has been resetted!");
            
        }
    }
}
