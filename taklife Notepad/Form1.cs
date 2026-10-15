using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace taklife_Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = "";
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "text files(*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 2;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    path = sfd.FileName;
                }
            }
            if (path.Contains(".txt") == false)
            {
                path += ".txt";
            }
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, textBox1.Text);
            }
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filetext = string.Empty;
            using (var openfileDialog = new OpenFileDialog())
            {
                openfileDialog.InitialDirectory = "c:\\";
                openfileDialog.Filter = "text files(*.txt)|*.txt|All files (*.*)|*.*";
                openfileDialog.FilterIndex = 1;

                if (openfileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filestream = openfileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(filestream))
                    {
                        filetext = reader.ReadToEnd();
                    }
                }
            }
            textBox1.Text = filetext;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchText = textBox2.Text;
            string text = textBox1.Text;

            int index = text.IndexOf(searchText);
               if(index !=-1)
            {
                textBox1.Focus();
                textBox1.Select(index,searchText.Length);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        bool MoveForm = false;
        Point LocationStart;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveForm = true;
            LocationStart = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveForm)
            {
                Location = new Point(Location.X+(e.Location.X - LocationStart.X),
                                   Location.Y + (e.Location.Y - LocationStart.Y));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            MoveForm = false;
        }
    }
}
