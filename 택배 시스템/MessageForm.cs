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
    public partial class MessageForm : Form
    {
        private Heesu.Windows.Forms.MoveWindowModule moveWindow;
        private Heesu.Windows.Forms.AnimationWindowModule animationWindow;
        private string initMainText;
        private string initSubText;
        private string initButton1Text;
        private string initButton2Text;
        private int timer = 0;

        public MessageForm(string mainText, string subText, string button1, string button2)
        {
            this.initMainText = mainText;
            this.initSubText = subText;
            this.initButton1Text = button1;
            this.initButton2Text = button2;
            this.InitializeComponent();
            this.moveWindow = new Heesu.Windows.Forms.MoveWindowModule(this, this.label1);
            this.animationWindow = new Heesu.Windows.Forms.AnimationWindowModule(this, this.button1, this.button2);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            this.label1.Text = this.initMainText;
            this.label2.Text = this.initSubText;
            this.button1.Text = this.initButton1Text;
            this.button2.Text = this.initButton2Text;
            this.timer = 0;
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer++;
            if(timer > 10)
            {
                this.timer1.Enabled = false;
                this.animationWindow.DoCloseAnimation();
            }
        }
    }
}
