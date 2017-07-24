using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YoutubeTheme.Skin
{
    [DefaultEvent("TextChanged")]
    public class YoutubeTextbox : Control
    {

        #region Declarations

        private TextBox _t = new TextBox();
        private TextBox T
        {
            get { return _t; }
            set
            {
                if (_t != null)
                {
                    _t.MouseLeave -= T_MouseLeave;
                    _t.MouseEnter -= T_MouseEnter;
                    _t.MouseDown -= T_MouseEnter;
                    _t.MouseHover -= T_MouseEnter;
                    _t.TextChanged -= T_TextChanged;
                    _t.KeyDown -= T_KeyDown;
                }
                _t = value;
                if (_t != null)
                {
                    _t.MouseLeave += T_MouseLeave;
                    _t.MouseEnter += T_MouseEnter;
                    _t.MouseDown += T_MouseEnter;
                    _t.MouseHover += T_MouseEnter;
                    _t.TextChanged += T_TextChanged;
                    _t.KeyDown += T_KeyDown;
                }
            }
        }
        private static readonly HelperMethods H = new HelperMethods();
        private HorizontalAlignment _textAlign;
        private int _maxLength;
        private bool _readOnly;
        private bool _useSystemPasswordChar;
        private string _watermarkText;
        private Image _image;
        private HelperMethods.MouseMode _state;
        private AutoCompleteSource _autoCompleteSource;
        private AutoCompleteMode _autoCompleteMode;
        private AutoCompleteStringCollection _autoCompleteCustomSource;
        private bool _multiline;
        private string[] _lines;


        #endregion

        #region Native Methods

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        #endregion

        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle
        {
            get
            {
                return BorderStyle.None;
            }
        }

        [Category("Custom"), Description("Gets or sets how text is aligned in TextBox control.")]
        public HorizontalAlignment TextAlign
        {
            get { return _textAlign; }
            set
            {
                _textAlign = value;
                if (T != null)
                {
                    T.TextAlign = value;
                }
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets how text is aligned in TextBox control.")]
        public int MaxLength
        {
            get { return _maxLength; }
            set
            {
                _maxLength = value;
                if (T != null)
                {
                    T.MaxLength = value;
                }
                Invalidate();
            }
        }

        [Browsable(false), ReadOnly(true)]
        public override Color BackColor
        {
            get { return Color.Transparent; }
        }

        [Browsable(false), ReadOnly(true)]
        public override Color ForeColor
        {
            get { return Color.Transparent; }        
        }

        [Category("Custom"), Description("Gets or sets a value indicating whether text in the text box is read-only.")]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                if (T != null)
                {
                    T.ReadOnly = value;
                }
            }
        }

        [Category("Custom"), Description("Gets or sets a value indicating whether the text in  TextBox control should appear as the default password character.")]
        public bool UseSystemPasswordChar
        {
            get { return _useSystemPasswordChar; }
            set
            {
                _useSystemPasswordChar = value;
                if (T != null)
                {
                    T.UseSystemPasswordChar = value;
                }
            }
        }

        [Category("Custom"), Description("Gets or sets a value indicating whether this is a multiline System.Windows.Forms.TextBox control.")]
        public bool Multiline
        {
            get { return _multiline; }
            set
            {
                _multiline = value;
                if (T == null)
                    return;
                T.Multiline = value;
                if (value)
                {
                    T.Height = Height - 10;
                }
                else
                {
                    Height = T.Height + 10;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image BackgroundImage
        {
            get { return null; }
        }

        [Category("Custom"), Description("Gets or sets the current text in  TextBox.")]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (T != null)
                {
                    T.Text = value;
                }
            }
        }

        [Category("Custom"), Description("Gets or sets the text in the System.Windows.Forms.TextBox while being empty.")]
        public string WatermarkText
        {
            get { return _watermarkText; }
            set
            {
                _watermarkText = value;
                SendMessage(T.Handle, 5377, 0, value);
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the image of the control.")]
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
        public AutoCompleteSource AutoCompleteSource
        {
            get { return _autoCompleteSource; }
            set
            {
                _autoCompleteSource = value;
                if (T != null)
                {
                    T.AutoCompleteSource = value;
                }
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return _autoCompleteCustomSource; }
            set
            {
                _autoCompleteCustomSource = value;
                if (T != null)
                {
                    T.AutoCompleteCustomSource = value;
                }
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets an option that controls how automatic completion works for the TextBox.")]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return _autoCompleteMode; }
            set
            {
                _autoCompleteMode = value;
                if (T != null)
                {
                    T.AutoCompleteMode = value;
                }
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the font of the text displayed by the control.")]
        public new Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                if (T == null)
                    return;
                T.Font = value;
                T.Location = new Point(5, 5);
                T.Width = Width - 8;
                if (!Multiline)
                    Height = T.Height + 11;
            }
        }

        [Category("Custom"), Description("Gets or sets the lines of text in the control.")]
        public string[] Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                if (T == null)
                    return;
                T.Lines = value;
                Invalidate();
            }
        }

        [Category("Custom"), Description("Gets or sets the ContextMenuStrip associated with this control.")]
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return base.ContextMenuStrip; }
            set
            {
                base.ContextMenuStrip = value;
                if (T == null)
                    return;
                T.ContextMenuStrip = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructors

        public YoutubeTextbox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            _textAlign = HorizontalAlignment.Left;
            _maxLength = 32767;
            _readOnly = false;
            _useSystemPasswordChar = false;
            _watermarkText = string.Empty;
            _image = null;
            _state = HelperMethods.MouseMode.Normal;
            _autoCompleteSource = AutoCompleteSource.None;
            _autoCompleteMode = AutoCompleteMode.None;
            _multiline = false;
            _lines = null;
            Font = new Font("Segoe UI", 10);
            T.Multiline = false;
            T.Cursor = Cursors.IBeam;
            T.BackColor = Colors.White;
            T.ForeColor = Colors.Silver;
            T.BorderStyle = BorderStyle.None;
            T.Location = new Point(7, 8);
            T.Font = Font;
            T.UseSystemPasswordChar = UseSystemPasswordChar;
            Size = new Size(135, 30);
            if (Multiline)
            {
                T.Height = Height - 11;
            }
            else
            {
                Height = T.Height + 11;
            }
        }

        #endregion

        #region Events

        public new event TextChangedEventHandler TextChanged;
        public delegate void TextChangedEventHandler(object sender);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(T))
                Controls.Add(T);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            T.Size = new Size(Width - 10, Height - 10);
        }

        #region TextBox MouseEvents

        private void T_MouseLeave(object sender, EventArgs e)
        {
            _state = HelperMethods.MouseMode.Normal;
            Invalidate();
        }

        private void T_MouseEnter(object sender, EventArgs e)
        {
            _state = HelperMethods.MouseMode.Pushed;
            Invalidate();
        }

        private void T_TextChanged(object sender, EventArgs e)
        {
            Text = T.Text;
            if (TextChanged != null) TextChanged.Invoke(this);
            Invalidate();
        }

        private void T_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                e.SuppressKeyPress = true;
            if (e.Control && e.KeyCode == Keys.C)
            {
                T.Copy();
                e.SuppressKeyPress = true;
            }
            _state = HelperMethods.MouseMode.Pushed;
            Invalidate();
        }

        #endregion

        #endregion

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            GraphicsPath gp = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            gp.AddRectangle(rect);

            gp.CloseFigure();
            using (PathGradientBrush b = new PathGradientBrush(gp) { CenterColor = Colors.White, SurroundColors = new Color[] { Colors.LightSilver }, FocusScales = new PointF(0.98f, 0.75f) })
            using (Pen p = new Pen(Colors.Silver))
            using (Pen p2 = new Pen(Colors.Blue))
            {
                switch (_state)
                {
                    case HelperMethods.MouseMode.Normal:
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(p, rect);
                        break;
                    case HelperMethods.MouseMode.Pushed:
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(p2, rect);
                        break;
                }

            }
        

            if (Image != null)
            {
                T.Location = new Point(31, 5);
                T.Width = Width - 60;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(Image, new Rectangle(8, 6, 16, 16));
            }
            else
            {
           
                T.Location = new Point(7, 5);
                T.Width = Width - 10;
            
            }

            gp.Dispose();

        }

        #endregion

    }
}