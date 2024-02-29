using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemPract9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += new EventHandler(Logger.HandleClick);
            textBox1.TextChanged += new EventHandler(Logger.HandleTextChanges);
            textBox2.TextChanged += new EventHandler(Logger.HandleTextChanges);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox1.Text + textBox2.Text;
            ResultSaver.SaveResult(textBox3.Text);
        }
    }
}
