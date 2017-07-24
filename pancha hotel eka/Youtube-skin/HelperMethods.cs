#region NameSpaces

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

#pragma warning disable 1570
#endregion

namespace YoutubeTheme.Skin
{



    /// <summary>
    /// Youtube Theme
    /// Author : THE LORD
    /// Release Date : Monday, June 19, 2017
    /// Updated : Wednesday, June 21, 2017
    /// HF Account : https://hackforums.net/member.php?action=profile&uid=3304362
    /// PM Me for any bug.
    /// </summary>

    #region Helper

    public sealed class HelperMethods
    {

        #region MouseStates

        /// <summary>
        /// The helper enumerator to get mouse states.
        /// </summary>
        public enum MouseMode : byte
        {
            Normal,
            Hovered,
            Pushed,
            Disabled
        }

        #endregion

        #region Draw Methods

        /// <summary>
        /// The Method to draw the image from encoded base64 string.
        /// </summary>
        /// <param name="g">The Graphics to draw the image.</param>
        /// <param name="base64Image">The Encoded base64 image.</param>
        /// <param name="rect">The Rectangle area for the image.</param>
        public void DrawImageFromBase64(Graphics g, string base64Image, Rectangle rect)
        {
            Image im = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(base64Image)))
            {
                im = Image.FromStream(ms);
                ms.Close();
            }
            g.DrawImage(im, rect);
        }

        /// <summary>
        /// The Method to fill rounded rectangle.
        /// </summary>
        /// <param name="g">The Graphics to draw the image.</param>
        /// <param name="c">The Color to the rectangle area.</param>
        /// <param name="rect">The Rectangle area to be filled.</param>
        /// <param name="curve">The Rounding border radius.</param>
        /// <param name="topLeft">Wether the top left of rectangle be round or not.</param>
        /// <param name="topRight">Wether the top right of rectangle be round or not.</param>
        /// <param name="bottomLeft">Wether the bottom left of rectangle be round or not.</param>
        /// <param name="bottomRight">Wether the bottom right of rectangle be round or not.</param>
        public void FillRoundedPath(Graphics g, Color c, Rectangle rect, int curve, bool topLeft = true,
            bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            g.FillPath(new SolidBrush(c), RoundRec(rect, curve, topLeft, topRight, bottomLeft, bottomRight));
        }

        /// <summary>
        /// The Method to fill the rounded rectangle.
        /// </summary>
        /// <param name="g">The Graphics to fill the rectangle.</param>
        /// <param name="b">The brush to the rectangle area.</param>
        /// <param name="rect">The Rectangle area to be filled.</param>
        /// <param name="curve">The Rounding border radius.</param>
        /// <param name="topLeft">Wether the top left of rectangle be round or not.</param>
        /// <param name="topRight">Wether the top right of rectangle be round or not.</param>
        /// <param name="bottomLeft">Wether the bottom left of rectangle be round or not.</param>
        /// <param name="bottomRight">Wether the bottom right of rectangle be round or not.</param>
        public void FillRoundedPath(Graphics g, Brush b, Rectangle rect, int curve, bool topLeft = true,
            bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            g.FillPath(b, RoundRec(rect, curve, topLeft, topRight, bottomLeft, bottomRight));
        }

        /// <summary>
        /// The Method to fill the rectangle the base color and surrounding with another color(Rectangle with shadow).
        /// </summary>
        /// <param name="g">The Graphics to fill the rectangle.</param>
        /// <param name="centerColor">The Center color of the rectangle area.</param>
        /// <param name="surroundColor">The Inner Surround color of the rectangle area.</param>
        /// <param name="p">The Point of the surrounding color.</param>
        /// <param name="rect">The Rectangle area to be filled.</param>
        /// <param name="curve">The Rounding border radius.</param>
        /// <param name="topLeft">Wether the top left of rectangle be round or not.</param>
        /// <param name="topRight">Wether the top right of rectangle be round or not.</param>
        /// <param name="bottomLeft">Wether the bottom left of rectangle be round or not.</param>
        /// <param name="bottomRight">Wether the bottom right of rectangle be round or not.</param>
        public void FillWithInnerRectangle(Graphics g, Color centerColor, Color surroundColor, Point p, Rectangle rect,
            int curve, bool topLeft = true, bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            using (
                PathGradientBrush pgb =
                    new PathGradientBrush(RoundRec(rect, curve, topLeft, topRight, bottomLeft, bottomRight)))
            {

                pgb.CenterColor = centerColor;
                pgb.SurroundColors = new Color[] { surroundColor };
                pgb.FocusScales = p;
                GraphicsPath gp = new GraphicsPath { FillMode = FillMode.Winding };
                gp.AddRectangle(rect);
                g.FillPath(pgb, gp);
                gp.Dispose();
            }
        }

        /// <summary>
        /// The Method to fill the circle the base color and surrounding with another color(Rectangle with shadow).
        /// </summary>
        /// <param name="g">The Graphics to fill the circle.</param>
        /// <param name="centerColor">The Center color of the rectangle area.</param>
        /// <param name="surroundColor">The Inner Surround color of the rectangle area.</param>
        /// <param name="p">The Point of the surrounding color.</param>
        /// <param name="rect">The circle area to be filled.</param>
        public void FillWithInnerEllipse(Graphics g, Color centerColor, Color surroundColor, Point p, Rectangle rect)
        {
            GraphicsPath gp = new GraphicsPath { FillMode = FillMode.Winding };
            gp.AddEllipse(rect);
            using (PathGradientBrush pgb = new PathGradientBrush(gp))
            {
                pgb.CenterColor = centerColor;
                pgb.SurroundColors = new Color[] { surroundColor };
                pgb.FocusScales = p;
                g.FillPath(pgb, gp);
                gp.Dispose();
            }
        }

        /// <summary>
        /// The Method to fill the rounded rectangle the base color and surrounding with another color(Rectangle with shadow).
        /// </summary>
        /// <param name="g">The Graphics to fill rounded the rectangle.</param>
        /// <param name="centerColor">The Center color of the rectangle area.</param>
        /// <param name="surroundColor">The Inner Surround color of the rectangle area.</param>
        /// <param name="p">The Point of the surrounding color.</param>
        /// <param name="rect">The Rectangle area to be filled.</param>
        /// <param name="curve">The Rounding border radius.</param>
        /// <param name="topLeft">Wether the top left of rectangle be round or not.</param>
        /// <param name="topRight">Wether the top right of rectangle be round or not.</param>
        /// <param name="bottomLeft">Wether the bottom left of rectangle be round or not.</param>
        /// <param name="bottomRight">Wether the bottom right of rectangle be round or not.</param>
        public void FillWithInnerRoundedPath(Graphics g, Color centerColor, Color surroundColor, Point p, Rectangle rect,
            int curve, bool topLeft = true, bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            using (
                PathGradientBrush pgb =
                    new PathGradientBrush(RoundRec(rect, curve, topLeft, topRight, bottomLeft, bottomRight)))
            {
                pgb.CenterColor = centerColor;
                pgb.SurroundColors = new Color[] { surroundColor };
                pgb.FocusScales = p;
                g.FillPath(pgb, RoundRec(rect, curve, topLeft, topRight, bottomLeft, bottomRight));
            }
        }

        /// <summary>
        /// The Method to draw the rounded rectangle area.
        /// </summary>
        /// <param name="g">The Graphics to draw rounded the rectangle.</param>
        /// <param name="c">Border Color</param>
        /// <param name="size">Border thickness</param>
        /// <param name="rect">The Rectangle area to be drawn.</param>
        /// <param name="curve">The Rounding border radius.</param>
        /// <param name="topLeft">Wether the top left of rectangle be round or not.</param>
        /// <param name="topRight">Wether the top right of rectangle be round or not.</param>
        /// <param name="bottomLeft">Wether the bottom left of rectangle be round or not.</param>
        /// <param name="bottomRight">Wether the bottom right of rectangle be round or not.</param>
        public void DrawRoundedPath(Graphics g, Color c, float size, Rectangle rect, int curve, bool topLeft = true,
            bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
            g.DrawPath(new Pen(c, size), RoundRec(rect, curve, topLeft, topRight, bottomLeft, bottomRight));
        }

        /// <summary>
        /// The method to draw the triangle.
        /// </summary>
        /// <param name="g">The Graphics to draw triangle.</param>
        /// <param name="c">The Triangle Color.</param>
        /// <param name="size">The Triangle thickness</param>
        /// <param name="P1">Point 1</param>
        /// <param name="P2">Point 2</param>
        /// <param name="P3">Point 3</param>
        /// <param name="P4">Point 4</param>
        /// <param name="P5">Point 5</param>
        /// <param name="P6">Point 6</param>
        public void DrawTriangle(Graphics g, Color c, int size, Point p10, Point p11, Point p20, Point p21, Point p30,
            Point p31)
        {
            g.DrawLine(new Pen(c, size), p10, p11);
            g.DrawLine(new Pen(c, size), p20, p21);
            g.DrawLine(new Pen(c, size), p30, p31);
        }

        /// <summary>
        /// The Method to fill the rectangle with border.
        /// </summary>
        /// <param name="g">The Graphics to fill the the rectangle.</param>
        /// <param name="rect">The Rectangle to fill.</param>
        /// <param name="rectColor">The Rectangle color.</param>
        /// <param name="StrokeColor">The Stroke(Border) color.</param>
        /// <param name="strokeSize">The Stroke thickness.</param>
        public void FillStrokedRectangle(Graphics g, Rectangle rect, Color rectColor, Color stroke, int strokeSize = 1)
        {
            using (SolidBrush b = new SolidBrush(rectColor))
            using (Pen s = new Pen(stroke, strokeSize))
            {
                g.FillRectangle(b, rect);
                g.DrawRectangle(s, rect);
            }

        }

        /// <summary>
        /// The Method to fill rounded rectangle with border.
        /// </summary>
        /// <param name="g">The Graphics to fill rounded the rectangle.</param>
        /// <param name="rect">The Rectangle to fill.</param>
        /// <param name="rectColor">The Rectangle color.</param>
        /// <param name="StrokeColor">The Stroke(Border) color.</param>
        /// <param name="strokeSize">The Stroke thickness.</param>
        /// <param name="Curve">The Rounding border radius.</param>
        /// <param name="topLeft">Wether the top left of rectangle be round or not.</param>
        /// <param name="topRight">Wether the top right of rectangle be round or not.</param>
        /// <param name="bottomLeft">Wether the bottom left of rectangle be round or not.</param>
        /// <param name="bottomRight">Wether the bottom right of rectangle be round or not.</param>
        public void FillRoundedStrokedRectangle(Graphics g, Rectangle rect, Color rectColor, Color stroke,
            int strokeSize = 1, int curve = 1, bool topLeft = true, bool topRight = true, bool bottomLeft = true,
            bool bottomRight = true)
        {
            using (SolidBrush b = new SolidBrush(rectColor))
            {
                using (Pen s = new Pen(stroke, strokeSize))
                {
                    FillRoundedPath(g, b, rect, curve, topLeft, topRight, bottomLeft, bottomRight);
                    DrawRoundedPath(g, stroke, strokeSize, rect, curve, topLeft, topRight, bottomLeft, bottomRight);
                }
            }
        }

        /// <summary>
        /// The Method to draw the image with custom color.
        /// </summary>
        /// <param name="g"> The Graphic to draw the image.</param>
        /// <param name="r"> The Rectangle area of image.</param>
        /// <param name="image"> The image that the custom color applies on it.</param>
        /// <param name="c">The Color that be applied to the image.</param>
        /// <remarks></remarks>
        public void DrawImageWithColor(Graphics g, Rectangle r, Image image, Color c)
        {
            float[][] ptsArray = new float[][]
            {
                new float[] {Convert.ToSingle(c.R/255.0), 0f, 0f, 0f, 0f},
                new float[] {0f, Convert.ToSingle(c.G/255.0), 0f, 0f, 0f},
                new float[] {0f, 0f, Convert.ToSingle(c.B/255.0), 0f, 0f},
                new float[] {0f, 0f, 0f, Convert.ToSingle(c.A/255.0), 0f},
                new float[]
                {
                    Convert.ToSingle(c.R/255.0),
                    Convert.ToSingle(c.G/255.0),
                    Convert.ToSingle(c.B/255.0), 0f,
                    Convert.ToSingle(c.A/255.0)
                }
            };
            ImageAttributes imgAttribs = new ImageAttributes();
            imgAttribs.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
            g.DrawImage(image, r, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imgAttribs);
            image.Dispose();
        }


        /// <summary>
        /// The Method to draw the image with custom color.
        /// </summary>
        /// <param name="g"> The Graphic to draw the image.</param>
        /// <param name="r"> The Rectangle area of image.</param>
        /// <param name="image"> The Encoded base64 image that the custom color applies on it.</param>
        /// <param name="c">The Color that be applied to the image.</param>
        /// <remarks></remarks>
        public void DrawImageWithColor(Graphics g, Rectangle r, string image, Color c)
        {
            Image im = ImageFromBase64(image);
            float[][] ptsArray = new float[][]
            {
                new float[] {Convert.ToSingle(c.R/255.0), 0f, 0f, 0f, 0f},
                new float[] {0f, Convert.ToSingle(c.G/255.0), 0f, 0f, 0f},
                new float[] {0f, 0f, Convert.ToSingle(c.B/255.0), 0f, 0f},
                new float[] {0f, 0f, 0f, Convert.ToSingle(c.A/255.0), 0f},
                new float[]
                {
                    Convert.ToSingle(c.R/255.0),
                    Convert.ToSingle(c.G/255.0),
                    Convert.ToSingle(c.B/255.0), 0f,
                    Convert.ToSingle(c.A/255.0)
                }
            };
            ImageAttributes imgAttribs = new ImageAttributes();
            imgAttribs.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
            g.DrawImage(im, r, 0, 0, im.Width, im.Height, GraphicsUnit.Pixel, imgAttribs);
        }

        #endregion

        #region Shapes

        /// <summary>
        /// The Triangle that joins 3 points to the triangle shape.
        /// </summary>
        /// <param name="p1">Point 1.</param>
        /// <param name="p2">Point 2.</param>
        /// <param name="p3">Point 3.</param>
        /// <returns>The Trangle shape based on given points.</returns>
        public Point[] Triangle(Point p1, Point p2, Point p3)
        {
            return new Point[]
            {
                p1,
                p2,
                p3
            };
        }

        #endregion

        #region Brushes

        /// <summary>
        /// The Brush with two colors one center another surounding the center based on the given rectangle area. 
        /// </summary>
        /// <param name="centerColor">The Center color of the rectangle.</param>
        /// <param name="surroundColor">The Surrounding color of the rectangle.</param>
        /// <param name="p">The Point of surrounding.</param>
        /// <param name="rect">The Rectangle of the brush.</param>
        /// <returns>The Brush with two colors one center another surounding the center.</returns>
        public static PathGradientBrush GlowBrush(Color centerColor, Color surroundColor, Point p, Rectangle rect)
        {
            GraphicsPath gp = new GraphicsPath { FillMode = FillMode.Winding };
            gp.AddRectangle(rect);
            return new PathGradientBrush(gp)
            {
                CenterColor = centerColor,
                SurroundColors = new Color[] { surroundColor },
                FocusScales = p
            };
        }

        /// <summary>
        /// The Brush from RGBA color.
        /// </summary>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        /// <param name="a">Alpha.</param>
        /// <returns>The Brush from given RGBA color.</returns>
        public SolidBrush SolidBrushRgbColor(int r, int g, int b, int a = 0)
        {
            return new SolidBrush(Color.FromArgb(a, r, g, b));
        }

        /// <summary>
        /// The Brush from HEX color.
        /// </summary>
        /// <param name="cWithoutHash">HEX Color without hash.</param>
        /// <returns>The Brush from given HEX color.</returns>
        public SolidBrush SolidBrushHtMlColor(string cWithoutHash)
        {
            return new SolidBrush(GetHtmlColor(cWithoutHash));
        }

        #endregion

        #region Pens

        /// <summary>
        /// The Pen from RGBA color.
        /// </summary>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        /// <param name="a">Alpha.</param>
        /// <returns>The Pen from given RGBA color.</returns>
        public Pen PenRgbColor(int r, int g, int b, int a, float size)
        {
            return new Pen(Color.FromArgb(a, r, g, b), size);
        }

        /// <summary>
        /// The Pen from HEX color.
        /// </summary>
        /// <param name="cWithoutHash">HEX Color without hash.</param>
        /// <param name="size">The Size of the pen.</param>
        /// <returns></returns>
        public Pen PenHtMlColor(string cWithoutHash, float size = 1)
        {
            return new Pen(GetHtmlColor(cWithoutHash), size);
        }

        #endregion

        #region Colors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cWithoutHash"></param>
        /// <returns></returns>
        public Color GetHtmlColor(string cWithoutHash)
        {
            return ColorTranslator.FromHtml("#" + cWithoutHash);
        }

        /// <summary>
        /// The Color from HEX by alpha property.
        /// </summary>
        /// <param name="alpha">Alpha.</param>
        /// <param name="cWithoutHash">HEX Color without hash.</param>
        /// <returns>The Color from HEX with given ammount of transparency</returns>
        public Color GetAlphaHtmlColor(int alpha, string cWithoutHash)
        {
            return Color.FromArgb(alpha, ColorTranslator.FromHtml("#" + cWithoutHash));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The String format to provide the alignment.
        /// </summary>
        /// <param name="horizontal">Horizontal alignment.</param>
        /// <param name="vertical">Horizontal alignment. alignment.</param>
        /// <returns>The String format.</returns>
        public StringFormat SetPosition(StringAlignment horizontal = StringAlignment.Center,
            StringAlignment vertical = StringAlignment.Center)
        {
            return new StringFormat
            {
                Alignment = horizontal,
                LineAlignment = vertical
            };
        }

        /// <summary>
        /// The Matrix array of single from color.
        /// </summary>
        /// <param name="c">The Color.</param>
        /// <returns>The Matrix array of single from the given color</returns>
        public float[][] ColorToMatrix(Color c)
        {
            return new float[][]
            {
                new float[]
                {
                    Convert.ToSingle(c.R/255),
                    0,
                    0,
                    0,
                    0
                },
                new float[]
                {
                    0,
                    Convert.ToSingle(c.G/255),
                    0,
                    0,
                    0
                },
                new float[]
                {
                    0,
                    0,
                    Convert.ToSingle(c.B/255),
                    0,
                    0
                },
                new float[]
                {
                    0,
                    0,
                    0,
                    Convert.ToSingle(c.A/255),
                    0
                },
                new float[]
                {
                    Convert.ToSingle(c.R/255),
                    Convert.ToSingle(c.G/255),
                    Convert.ToSingle(c.B/255),
                    0f,
                    Convert.ToSingle(c.A/255)
                }
            };
        }

        /// <summary>
        /// The Image from encoded base64 image.
        /// </summary>
        /// <param name="base64Image">The Encoded base64 image</param>
        /// <returns>The Image from encoded base64.</returns>
        public Image ImageFromBase64(string base64Image)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(base64Image)))
            {
                return Image.FromStream(ms);
            }
        }


        #endregion

        #region Round Border

        /// <summary>
        /// Credits : AeonHack
        /// </summary>

        public GraphicsPath RoundRec(Rectangle r, int curve, bool topLeft = true, bool topRight = true,
            bool bottomLeft = true, bool bottomRight = true)
        {
            GraphicsPath createRoundPath = new GraphicsPath(FillMode.Winding);
            if (topLeft)
            {
                createRoundPath.AddArc(r.X, r.Y, curve, curve, 180f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
            }
            if (topRight)
            {
                createRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
            }
            if (bottomRight)
            {
                createRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);

            }
            if (bottomLeft)
            {
                createRoundPath.AddArc(r.X, (r.Bottom + r.Y) - curve, curve, curve, 90f, 90f);
            }
            else
            {
                createRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
            }
            createRoundPath.CloseFigure();
            return createRoundPath;
        }

        #endregion

    }

    #endregion
}
