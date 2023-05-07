﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQA
{
    public partial class flogin : Form
    {
        public flogin()
        {
            InitializeComponent();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Tạo database và bảng users
            if(MainClass.IsValidUser(txtUser.Text, txtPass.Text) == false )
            {
                guna2MessageDialog1.Show("Sai tài khoản hoặc mật khẩu");
                return;
            }
            else
            {
                this.Hide();
                fMain frm = new fMain();
                frm.Show();
            }

            // Thêm 1 user


        }

        private void guna2PictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            txtPass.UseSystemPasswordChar = false;
        }

        private void guna2PictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }


    }
}
