using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject
{
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();

            //Khoi tao gia tri cho combobox.
            cbFuct.Items.Add("Chọn thuật toán");
            cbFuct.Items.Add("Dijkstra");
            cbFuct.Items.Add("A_Sao");
            cbFuct.Text = "Chọn thuật toán";
            lbThongBao.Text = "Xin vui lòng chọn vị trí bắt đầu cho quân mã.";
        }
      
    }
}
