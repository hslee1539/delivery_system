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
    public partial class SendForm : Form
    {
        private Heesu.Windows.Forms.AnimationWindowModule animationWindow;
        private Heesu.Windows.Forms.MoveWindowModule moveWindow;
        private 천지인Form 천지인 = new 천지인Form();
        private Color button_size_color;
        public SendForm()
        {
            InitializeComponent();
            this.animationWindow = new Heesu.Windows.Forms.AnimationWindowModule(this, this.button1);
            this.moveWindow = new Heesu.Windows.Forms.MoveWindowModule(this, this.label1);
            this.button_size_color = button2.BackColor;
        }

        private void SendForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.천지인.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.button2.BackColor = this.button3.BackColor = this.button4.BackColor = this.button_size_color;
            var button = sender as Button;
            button.BackColor = Color.CadetBlue;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button2.BackColor == button_size_color && button3.BackColor == button_size_color && button4.BackColor == button_size_color)
            {
                new MessageForm("알림", "크기를 선택하세요", "확인", "확인").ShowDialog();
            }
            else
            {

            }
        }
    }
}
