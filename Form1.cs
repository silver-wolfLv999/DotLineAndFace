namespace DotLineAndFace
{
    public partial class Form1 : Form
    {
        PaintEventHandler hold_paint_event = null;
        Point start = new Point();
        Point end = new Point();
        Brush A_Brush = new SolidBrush(Color.Black);
        Pen A_Pen = new Pen(Color.Black, 20);
        bool isMouseDown = false;
        int size = 20;
        public Form1()
        {
            InitializeComponent();
            hold_paint_event = Draw_Dot;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (hold_paint_event != null)
            {
                hold_paint_event(sender, e);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            this.ActiveControl = null;
            if (int.TryParse(textBox1.Text, out int result))
            {
                size = result;
                A_Pen = new Pen(A_Pen.Color, size);
            }
            else
            {
                textBox1.Text = size.ToString();
            }
            if (hold_paint_event != Draw_Dot)
            {
                start = new Point(e.X, e.Y);
                end = start;
            }
            else
            {
                start = new Point(e.X - size / 2, e.Y - size / 2);
                this.Invalidate();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hold_paint_event == Draw_Line)
            {
                end = new Point(e.X, e.Y);
                if (isMouseDown)
                    this.Invalidate();
            }
            if (hold_paint_event == Draw_Face)
            {
                end = new Point(e.X, e.Y);
                if (isMouseDown)
                    this.Invalidate();
            
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            if (hold_paint_event != Draw_Dot)
            {
                end = new Point(e.X, e.Y);
                this.Invalidate();
            }
        }

        private void button_check(object sender, EventArgs e)
        {
            RadioButton get_radious_button = (RadioButton)sender;
            string get_text = get_radious_button.Text;
            switch (get_text)
            {
                case "dot":
                    hold_paint_event = Draw_Dot;
                    break;
                case "line":
                    hold_paint_event = Draw_Line;
                    break;
                case "face":
                    hold_paint_event = Draw_Face;
                    break;
            }
        }

        private void Draw_Dot(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(A_Brush, start.X, start.Y, size, size);
        }

        private void Draw_Line(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(A_Pen, start, end);
        }

        private void Draw_Face(object sender, PaintEventArgs e)
        {
            int x = start.X < end.X ? start.X : end.X;
            int y = start.Y < end.Y ? start.Y : end.Y;
            int height = Math.Abs(start.Y - end.Y);
            int width = Math.Abs(start.X - end.X);       
            e.Graphics.FillEllipse(A_Brush, x, y, width, height);
            if (isMouseDown)
            {
                using (Pen pen = new Pen(Color.LightGray, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, x, y, width, height);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button1.BackColor = colorDialog1.Color;
            A_Brush = new SolidBrush(button1.BackColor);
            A_Pen = new Pen(button1.BackColor, size);
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
