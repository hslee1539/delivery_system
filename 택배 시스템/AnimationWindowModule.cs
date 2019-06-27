using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Heesu.Windows.Forms
{
    public class AnimationWindowModule
    {
        delegate void SetLocation(Control control, Point point);
        delegate void SetFormVisible(Form control, bool visible);
        delegate void FormClose(Form control);
        static SetLocation setLocation = StaticSetLocation;
        static SetFormVisible setVisible = StaticFormVisible;
        static FormClose formClose = StaticClose;

        int MS = 20;
        int FRAME = 17;
        Form target;
        Control backGround;
        Point[] orignalLocation = new Point[0];
        //0번에는 width, 마지막에는 0
        int[] movement = new int[0];
        Thread thread;
        public AnimationWindowModule(Form target, params Button []exitButton)
        {
            target.Shown += Target_Shown;
            if(exitButton.Length == 0)
            {
                throw new Exception("반듯이 클릭 대상이 있어야 합니다. 코딩을 수정하세요.");
            }
            foreach (var item in exitButton)
            {
                item.Click += ExitButton_Click;
            }
            //별도의 background control 추가
            this.backGround = new Control(target, "test");
            target.Controls.Add(this.backGround);
            this.backGround.Text = "";
            this.backGround.Visible = true;
            this.backGround.BackColor = target.BackColor;
            this.backGround.Location = new Point(0, 0);
            this.backGround.Size = target.Size;
            this.backGround.SendToBack();
            //form의 background는 투명색
            target.BackColor = Color.Fuchsia;
            target.TransparencyKey = target.BackColor;
            //form 등록
            this.target = target;
            this.syncOrignalLocation();
            this.syncMovement();
        }
        public void DoCloseAnimation()
        {
            thread = new Thread(new ThreadStart(this.endAnimation));
            thread.Start();
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DoCloseAnimation();
        }

        private void Target_Shown(object sender, EventArgs e)
        {
            this.readyToStartAnimation();
            thread = new Thread(new ThreadStart(this.startAnimation));
            thread.Start();

        }
        /// <summary>
        /// 현 form의 컨트롤들의 위치로 최종 애니메이션 지점으로 설정합니다.
        /// </summary>
        public void syncOrignalLocation()
        {
            int max = target.Controls.Count;
            if(max != orignalLocation.Length)
            {
                orignalLocation = new Point[max];
            }
            for (int i = 0; i < max; i++)
            {
                orignalLocation[i] = target.Controls[i].Location;
            }
        }
        public void syncMovement()
        {
            if(movement.Length != FRAME)
            {
                int width = target.Size.Width;
                movement = new int[FRAME];
                movement[0] = width;
                for(int i = 1; i < FRAME; i++)
                {
                    movement[i] = width / (i * 4 + 1);
                }
                movement[FRAME - 1] = 0;
            }
        }
        public void readyToStartAnimation()
        {
            int max = target.Controls.Count;
            int width = target.Size.Width;
            for(int i = 0; i < max; i++)
            {
                var control = target.Controls[i];
                var location = control.Location;
                location.X -= width;
                control.Location = location;
            }
        }
        protected void startAnimation()
        {
            int index = 0;
            int maxIndex = this.target.Controls.Count;
            int frame = 0;
            int maxFrame = maxIndex + this.FRAME;
            this.target.Invoke(setVisible, target, false);
            while (frame < maxFrame)
            {
                index = index < maxIndex ? index + 1 : index;
                for (int i = 0; i < index; i++)
                {
                    var control = target.Controls[i];
                    var location = control.Location;
                    int movementIndex = (frame - i) < this.FRAME ? (frame - i) : (this.FRAME-1);
                    location.X = orignalLocation[i].X - movement[movementIndex];
                    target.Invoke(setLocation,control,location);
                }
                Thread.Sleep(MS);
                frame++;
            }
            this.target.Invoke(setVisible, target, true);
        }
        protected void endAnimation()
        {
            int index = 0;
            int maxIndex = this.target.Controls.Count;
            int frame = 0;
            int maxFrame = maxIndex + this.FRAME;
            this.target.Invoke(setVisible, target,false);
            while (frame < maxFrame)
            {
                index = index < maxIndex ? index + 1 : index;
                for (int i = 0; i < index; i++)
                {
                    var control = target.Controls[i];
                    var location = control.Location;
                    int movementIndex = (frame - i) < this.FRAME ? (frame - i) : (this.FRAME - 1);
                    location.X = movement[movementIndex] + orignalLocation[i].X - target.Width;
                    target.Invoke(setLocation, control, location);
                }
                Thread.Sleep(MS);
                frame++;
            }
            this.target.Invoke(setVisible, target, true);
            this.target.Invoke(formClose,target);
        }

        private static void StaticSetLocation(Control control, Point location)
        {
            control.Location = location;
        }
        private static void StaticClose(Form form)
        {
            form.Close();
        }
        private static void StaticFormVisible(Form control, bool visible)
        {
            control.Enabled = visible;
        }


    }
}
