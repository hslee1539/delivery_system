using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 택배_시스템
{
    public partial class KeypadForm : Form, IShowDialog
    {
        private Heesu.Windows.Forms.AnimationWindowModule animationWindow;
        private Heesu.Windows.Forms.MoveWindowModule moveWindow;
        public ulong ID_result { get; private set; } = 0;
        public DialogResult result;

        public KeypadForm()
        {
            InitializeComponent();
            this.animationWindow = new Heesu.Windows.Forms.AnimationWindowModule(this, this.button1, this.button13);
            this.moveWindow = new Heesu.Windows.Forms.MoveWindowModule(this, this.label1);
        }

        public DialogResult ShowGetDialog()
        {
            this.result = DialogResult.None;
            this.ID_result = 0;
            this.ShowDialog();
            return this.result;
        }

        public DialogResult ShowSendDialog()
        {
            this.result = DialogResult.None;
            this.ID_result = 0;
            this.ShowDialog();
            return this.result;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            label2.Text += button.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.result = DialogResult.OK;
            this.ID_result = ulong.Parse(label2.Text);
            label2.Text = "";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int len = label2.Text.Length;
            if(len > 0)
            {
                label2.Text = label2.Text.Remove(len - 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.result = DialogResult.Cancel;
            label2.Text = "";
            this.ID_result = 0;
        }
    }
}
