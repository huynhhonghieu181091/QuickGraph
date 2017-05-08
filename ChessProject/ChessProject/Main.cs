using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject
{
    public partial class Main : Form
    {
        public int[,] vt = new int[3, 2501];
        private Label[,] labelSo;
        private PictureBox[,] banCo;
        private ControlPanel _group;
        public int kich_thuoc=8;//Kích thước bàn cờ
        public int toa_do_X, _X;
        public int toa_do_Y, _Y;
        public int kt_X, kt_Y;
        private int bd = 1;//Số bước đi của mã
        private bool tuyChon = false;//Tùy chọn Start hay Stop
        private int do_rong = 40;
        private int sobd;
        private int trangthai;  //-1: Trang thai bat dau, 0: Da chon vi tri dau tien, 1: da chon vi tri cuoi
        //private int thuattoan; //1: dijkstra
        public int thuattoan; //1: dijkstra
        private DuongDi dd;

        public Main()
        {
            InitializeComponent();
            khoiTao();
        }

        public void khoiTao()
        {
            thuattoan = 0;
            trangthai = -1;
            sobd = 0;
            timer1.Enabled = false;
            timer1.Interval = 500;
            timer2.Enabled = false;

            this.Controls.Clear();

            //Khai bao so thu tu o cua ban co
            labelSo = new Label[2, kich_thuoc + 1];
            for (int i = 1; i <= kich_thuoc; i++)
            {
                //Ngang
                labelSo[1, i] = new Label();
                labelSo[1, i].AutoSize = true;
                labelSo[1, i].Location = new System.Drawing.Point(do_rong * i - 5, 9);
                labelSo[1, i].Name = i.ToString();
                labelSo[1, i].Size = new System.Drawing.Size(19, 13);
                labelSo[1, i].TabIndex = 1;
                labelSo[1, i].Text = i.ToString();
                labelSo[1, i].ForeColor = Color.Black;
                this.Controls.Add(labelSo[1, i]);
                //Doc
                labelSo[0, i] = new Label();
                labelSo[0, i].AutoSize = true;
                labelSo[0, i].Location = new System.Drawing.Point(5, do_rong * i - 5);
                labelSo[0, i].Name = i.ToString();
                labelSo[0, i].Size = new System.Drawing.Size(19, 13);
                labelSo[0, i].TabIndex = 1;
                labelSo[0, i].Text = i.ToString();
                labelSo[0, i].ForeColor = Color.Black;
                this.Controls.Add(labelSo[0, i]);
            }

            //Khởi tạo mảng hai chiều picture hiển thị bàn cờ
            banCo = new PictureBox[kich_thuoc + 1, kich_thuoc + 1];
            for (int i = 1; i <= kich_thuoc; i++)
            {
                for (int j = 1; j <= kich_thuoc; j++)
                {
                    banCo[i, j] = new PictureBox();
                    if (i % 2 != 0)
                    {
                        if (j % 2 != 0) banCo[i, j].BackgroundImage = global::ChessProject.Properties.Resources.white_tile;
                        else banCo[i, j].BackgroundImage = global::ChessProject.Properties.Resources.green_tile;
                    }
                    else if (j % 2 != 0) banCo[i, j].BackgroundImage = global::ChessProject.Properties.Resources.green_tile;
                    else banCo[i, j].BackgroundImage = global::ChessProject.Properties.Resources.white_tile;


                    banCo[i, j].Location = new System.Drawing.Point(25 + (j - 1) * do_rong, 25 + (i - 1) * do_rong);
                    banCo[i, j].Name = "pictureBox1";
                    banCo[i, j].Size = new System.Drawing.Size(do_rong, do_rong);
                    banCo[i, j].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    banCo[i, j].TabIndex = 0;
                    banCo[i, j].TabStop = false;
                    banCo[i, j].Click += new EventHandler(tileClicked);
                    this.Controls.Add(banCo[i, j]);
                }
            }
            //banCo[toa_do_X, toa_do_Y].Image = global::ChessProject.Properties.Resources.black_knight;

            //Khoi tao thuat toan tim duong di
            dd = new DuongDi();
            //Khởi tạo control điều khiển chương trình chính
            _group = new ControlPanel();
            _group.gbCommand.Enabled = false;
            _group.Top = 0;
            _group.Left = 25 + 12 + do_rong * kich_thuoc;
            _group.btCommand.Click += new EventHandler(btCommand_Click);
            _group.btReset.Click += new EventHandler(btReset_Click);
            _group.cbFuct.SelectedIndexChanged += new EventHandler(cboTG_Cho_SelectedIndexChanged);
            this.Controls.Add(_group);
           
            //Khởi tạo tùy chọn
            bd = 1;
            tuyChon = false;
            //Điều chỉnh độ rộng thích hợp cho form
            Width = 50 + do_rong * kich_thuoc + _group.Width;
            this.WindowState = FormWindowState.Normal;
            if (kich_thuoc <= 14)
            {
                AutoScroll = false;
                FormBorderStyle = FormBorderStyle.FixedSingle;
                MaximizeBox = false;
                _group.Height = 30 + do_rong * 14;
                Height = 25 + 12 + do_rong * 14 + 34;//34 là chiều cao của thanh tiêu đề
            }
            else
            {
                AutoScroll = true;
                FormBorderStyle = FormBorderStyle.Sizable;
                MaximizeBox = true;
                _group.Height = 30 + do_rong * kich_thuoc;
                Width += 20;//20 độ rộng của thanh trượt
                Height = 25 + 12 + do_rong * kich_thuoc + 34 + 20;//34 là chiều cao của thanh tiêu đề, 20 là chiều cao than trượt
            }
        }

        void tileClicked(object sender, EventArgs e)
        {
            PictureBox control = (PictureBox)sender;
            var point = control.Location;
            int i = (point.Y - 25) / do_rong + 1;
            int j = (point.X - 25) / do_rong + 1;
            if (trangthai == -1)
            {
                banCo[i, j].Image = global::ChessProject.Properties.Resources.black_knight;
                toa_do_X = _X = i;
                toa_do_Y = _Y = j;
                trangthai = 0;
                _group.lbThongBao.Text = "Xin vui lòng chọn vị trí kết thúc của quân ngựa.";
                _group.lblTDBD.Text = "Tọa độ bắt đầu: (" + i.ToString() + "," + j.ToString() + ")";
                _group.gbCommand.Enabled = true;
            }
            else if (trangthai == 0)
            {
                banCo[i, j].Image = global::ChessProject.Properties.Resources.black_knight;
                kt_X = i; kt_Y = j;
                trangthai = 1;
                _group.lbThongBao.Text = "Đã chọn xong vị trí xin vui lòng chọn thuật toán để chạy";
                _group.lblTDKT.Text = "Tọa độ kết thúc: (" + i.ToString() + "," + j.ToString() + ")";
                _group.gbCommand.Enabled = true;
            }
        }

        //khoi tạo tuận toán
       
      //  chọn thuật toán kiểm tra
        void cboTG_Cho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_group.cbFuct.Text == "Dijkstra")
            {
                thuattoan = 1;
            }
            else if (_group.cbFuct.Text == "A_Sao")
            {
                thuattoan = 2;
            }
            else
            {
                thuattoan = 0;
            }

        }

        private void btCommand_Click(object sender, EventArgs e)
        {
            dd.Set(kich_thuoc, toa_do_X, toa_do_Y);
            if (tuyChon == false)//bắt đầu chạy tự động
            {
                if (trangthai == -1)
                {
                    MessageBox.Show("Xin vui lòng click chuột bàn cờ để chọn vị trí xuất phát.");
                }
                else if (thuattoan == 0)
                {
                    MessageBox.Show("Xin vui lòng chọn thuật toán ở bản điều khiển bên phải.");
                }
                else if (thuattoan == 1) //Da chon vi tri xuat phat va vi tri ket thuc chay thuat toan Dijkstra
                {
                    if (trangthai < 1)
                    {
                        MessageBox.Show("Xin vui lòng click chuột bàn cờ để chọn vị trí kết thúc.");
                        return;
                    }
                    if (dd.Dijkstra(kt_X, kt_Y))
                    {
                        vt = dd.vt;
                        sobd = dd.sobd;
                        _group.lblSBD.Text = "Số bước đi: " + (sobd - 2).ToString();
                        _group.lbThongBao.Text = "Đang chạy thuật toán";
                        tuyChon = true;
                        _group.btCommand.Text = "Ngưng";
                        timer1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Thuật toán Dijkstra không tìm thấy đường đi giữa {0} và {1}", "(" + toa_do_X.ToString() + "," + toa_do_Y.ToString() + ")", "(" + kt_X.ToString() + "," + kt_Y.ToString() + ")"));
                    }
                }
                else if (thuattoan == 2) //Da chon vi tri xuat phat va vi tri ket thuc chay thuat toan Dijkstra
                {
                    if (trangthai < 1)
                    {
                        MessageBox.Show("Xin vui lòng click chuột bàn cờ để chọn vị trí kết thúc.");
                        return;
                    }
                    if (dd.AStar(kt_X, kt_Y))
                    {
                        vt = dd.vt;
                        sobd = dd.sobd;
                        _group.lbThongBao.Text = "Đang chạy thuật toán";
                        _group.lblSBD.Text = "Số bước đi: " + (sobd - 2).ToString();
                        tuyChon = true;
                        _group.btCommand.Text = "Ngưng";
                        timer1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Thuật toán A* không tìm thấy đường đi giữa {0} và {1}", "(" + toa_do_X.ToString() + "," + toa_do_Y.ToString() + ")", "(" + kt_X.ToString() + "," + kt_Y.ToString() + ")"));
                    }
                }
               
            }
            else
            {
                tuyChon = false;
                _group.btCommand.Text = "Tiếp theo";
                timer1.Enabled = false;
            }
        }

        //Reset lại chương trình
        private void btReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        public void xoaNgua()
        {
            for (int i = 1; i <= kich_thuoc; i++)
            {
                for (int j = 1; j <= kich_thuoc; j++)
                {
                    banCo[i, j].Image = null;
                }
            }
        }

        public void Reset()
        {
            if (banCo != null)
            {
                xoaNgua();
            }
            _group.cbFuct.Text = "Chọn thuật toán";
            _group.lbThongBao.Text = "Xin vui lòng chọn vị trí bắt đầu cho quân mã.";
            _group.btCommand.Text = "Bắt đầu";
            _group.lblSBD.Text = "Số bước đi: ";
            _group.lblTDBD.Text = "Tọa độ bắt đầu: ";
            _group.lblTDKT.Text = "Tọa độ kết thúc: ";
            thuattoan = 0;
            trangthai = -1;
            bd = 0;
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bd == 0)
            {
                bd++;
                //xoaNgua();
                banCo[toa_do_X, toa_do_Y].Image = global::ChessProject.Properties.Resources.black_knight;
            }
            else
            {
                banCo[_X, _Y].Image = global::ChessProject.Properties.Resources.white_knight;
                _X = vt[1, bd];
                _Y = vt[2, bd];
                banCo[_X, _Y].Image = global::ChessProject.Properties.Resources.black_knight;
                bd++;
                if (bd == sobd)//đi hết bàn cờ
                {
                    //_X = toa_do_X;
                    //_Y = toa_do_Y;
                    bd = 0;
                    tuyChon = false;
                    timer1.Enabled = false;
                    //timer2.Enabled = false;
                    _group.btCommand.Text = "Bắt đầu";
                    _group.lbThongBao.Text = "Kết thúc thuật toán.";
                    _group.gbCommand.Enabled = false;
                }
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
