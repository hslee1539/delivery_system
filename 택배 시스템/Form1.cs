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
    public partial class Form1 : Form
    {
        private Heesu.Windows.Forms.AnimationWindowModule animationWindow;
        private Heesu.Windows.Forms.MoveWindowModule moveWindow;
        private IDInputForm inputForm = new IDInputForm();
        private SendForm sendForm = new SendForm();
        public Form1()
        {
            InitializeComponent();
            this.animationWindow = new Heesu.Windows.Forms.AnimationWindowModule(this, this.button1);
            this.moveWindow = new Heesu.Windows.Forms.MoveWindowModule(this, this.label1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.inputForm.ShowSendDialog();
            if(this.inputForm.result != null)
            {
                this.sendForm.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inputForm.ShowGetDialog();
        }
    }
}
