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
    public partial class 천지인Form : Form
    {
        private Heesu.Windows.Forms.AnimationWindowModule animationWindow;
        private Heesu.Windows.Forms.MoveWindowModule moveWindow;
        Korean korean = new Korean();
        string number = "412E";
        public 천지인Form()
        {
            InitializeComponent();
            this.animationWindow = new Heesu.Windows.Forms.AnimationWindowModule(this, this.button1);
            this.moveWindow = new Heesu.Windows.Forms.MoveWindowModule(this, this.label1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            number = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            this.number = this.number.Remove(this.number.Length - 1);
            this.number += button.Tag + "E";
            label2.Text = korean.Parse(this.number);
        }

        private void 천지인Form_Load(object sender, EventArgs e)
        {

        }
    }
}
