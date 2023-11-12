using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    internal class Fields
    {
        public PictureBox Cell { get; set; }
        public Image CellImage { get; set; }
        public int X { get; set; }
        public int Y { get; set; } 
        public Size SizeOfCell { get; set; }
    }
}
