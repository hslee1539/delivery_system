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
    public interface IShowDialog
    {
        DialogResult ShowGetDialog();
        DialogResult ShowSendDialog();
    }
    public partial class IDInputForm : Form , IShowDialog
    {
        private Heesu.Windows.Forms.AnimationWindowModule animationWindow;
        private Heesu.Windows.Forms.MoveWindowModule moveWindow;
        /// <summary>
        /// 1 : get, 2: send
        /// </summary>
        private int state = 0;

        private BarcodeForm barcodeForm = new BarcodeForm();
        private KeypadForm keypadForm = new KeypadForm();

        public UserSchema result;

        public IDInputForm()
        {
            InitializeComponent();
            this.animationWindow = new Heesu.Windows.Forms.AnimationWindowModule(this, this.button1);
            this.moveWindow = new Heesu.Windows.Forms.MoveWindowModule(this, this.label1);
         }

        public DialogResult ShowGetDialog()
        {
            this.state = 1;
            return ShowDialog();
        }
        public DialogResult ShowSendDialog()
        {
            this.state = 2;
            return ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (this.state)
            {
                case 1:
                    if(this.keypadForm.ShowGetDialog() == DialogResult.OK)
                    {
                        UserSchema search = UserProcessor.GetProcessor().Get(this.keypadForm.ID_result);
                        if( search != null)
                        {
                            //다음 폼 이동
                        }
                    }
                    break;
                case 2:
                    if(this.keypadForm.ShowSendDialog() == DialogResult.OK)
                    {
                        result = UserProcessor.GetProcessor().Get(this.keypadForm.ID_result);
                        if (result == null)
                        {
                            new MessageForm("오류", "없는 사용자 입니다.", "확인", "확인").ShowDialog();
                        }
                        this.animationWindow.DoCloseAnimation();
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (this.state)
            {
                case 1:
                    this.barcodeForm.ShowGetDialog();
                    break;
                case 2:
                    this.barcodeForm.ShowSendDialog();
                    break;
            }
        }
    }
}
