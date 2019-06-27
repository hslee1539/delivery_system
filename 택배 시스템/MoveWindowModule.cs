using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Heesu.Windows.Forms
{
    public class MoveWindowModule
    {
        private Form form;
        private Control target;
        private bool clickState = false;
        private Point clickPoint = new Point();
        private Point tmpPoint = new Point();

        public MoveWindowModule(Form form, Control clickTarget)
        {
            this.form = form;
            this.target = clickTarget;
            this.target.MouseDown += Target_MouseDown;
            this.target.MouseMove += Target_MouseMove;
            this.target.MouseUp += Target_MouseUp;
            this.form.Disposed += Form_Disposed;
        }

        private void Form_Disposed(object sender, EventArgs e)
        {
            this.target.MouseDown -= Target_MouseDown;
            this.target.MouseMove -= Target_MouseMove;
            this.target.MouseUp -= Target_MouseUp;
            this.form.Disposed -= Form_Disposed;
        }

        private void Target_MouseUp(object sender, MouseEventArgs e)
        {
            this.tmpPoint.X = Cursor.Position.X - this.clickPoint.X;
            this.tmpPoint.Y = Cursor.Position.Y - this.clickPoint.Y;
            this.form.Location = this.tmpPoint;
            this.clickState = false;
        }

        private void Target_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.clickState)
            {
                this.tmpPoint.X = Cursor.Position.X - this.clickPoint.X;
                this.tmpPoint.Y = Cursor.Position.Y - this.clickPoint.Y;
                this.form.Location = this.tmpPoint;
            }
        }

        private void Target_MouseDown(object sender, MouseEventArgs e)
        {
            this.clickState = true;
            this.clickPoint = e.Location;
            this.clickPoint.X += target.Location.X;
            this.clickPoint.Y += target.Location.Y;
        }
    }
}
