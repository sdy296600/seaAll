using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Core
{
    public partial class xTableLayoutPanel : TableLayoutPanel
    {
        [Browsable(true)]

        public Color GraColorA { get; set; }



        [Browsable(true)]

        public Color GraColorB { get; set; }

        //[Browsable(true)]

        //public Color GraColorC { get; set; }

        [Browsable(true)]

        public LinearGradientMode GradientFillStyle { get; set; }



        private Brush gradientBrush;



        public xTableLayoutPanel()

        {

            handlerGradientChanged = new EventHandler(GradientChanged);

            ResizeRedraw = true;

        }



        private EventHandler handlerGradientChanged;



        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)

        {
            gradientBrush =new LinearGradientBrush(ClientRectangle, GraColorA, GraColorB, GradientFillStyle);

    
            

            e.Graphics.FillRectangle(gradientBrush, ClientRectangle);

            //LinearGradientBrush gradientBrush = new LinearGradientBrush(
            //   this.ClientRectangle
            //   , GraColorA
            //   , GraColorC
            //   , 0
            //   , false);

            //ColorBlend cb = new ColorBlend();
            //cb.Positions = new[] { 0, 1 / 2f, 1 };
            //cb.Colors = new[] { GraColorA, GraColorB, GraColorC };
            //gradientBrush.InterpolationColors = cb;
            //gradientBrush.RotateTransform(45);

            //e.Graphics.FillRectangle(gradientBrush, ClientRectangle);
        }



        protected override void Dispose(bool disposing)
        {

            if (disposing)

            {

                if (gradientBrush != null) gradientBrush.Dispose();

            }

            base.Dispose(disposing);

        }



        protected override void OnScroll(ScrollEventArgs se)
        {

            Invalidate();

        }



        private void GradientChanged(object sender, EventArgs e)
        {

            if (gradientBrush != null) gradientBrush.Dispose();

            gradientBrush = null;

            Invalidate();

        }
    }
}
