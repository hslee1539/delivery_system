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
    public partial class BarcodeForm : Form, IShowDialog
    {
        private Heesu.Windows.Forms.AnimationWindowModule animationWindow;
        private Heesu.Windows.Forms.MoveWindowModule moveWindow;
        public BarcodeForm()
        {
            InitializeComponent();
            this.animationWindow = new Heesu.Windows.Forms.AnimationWindowModule(this, this.button1);
            this.moveWindow = new Heesu.Windows.Forms.MoveWindowModule(this, this.label1);
        }

        public DialogResult ShowGetDialog()
        {
            return this.ShowDialog();
        }

        public DialogResult ShowSendDialog()
        {
            return this.ShowDialog();
        }
    }
}
