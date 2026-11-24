using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;

namespace 传奇.core
{
    /// <summary>
    /// 文本类,文字靠左,居中,描边字体
    /// </summary>
    public class TextHelper
    {
        // 默认值4,属性（范围0-12）控制文字与背景的对比度,值越高边缘越锐利：
        static int textContrast = 3;

        /// <summary>
        /// 文字居中显示
        /// </summary>
        public static void DrawCenteredString(Graphics g, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center; // 设置水平居中
            format.LineAlignment = StringAlignment.Center; // 设置垂直居中

            g.SmoothingMode = SmoothingMode.HighQuality;//设置字体质量
            g.TextContrast = textContrast;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawString(text, font, brush, layoutRectangle, format);
        }

        public static void DrawLeftString(Graphics g, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;

            g.SmoothingMode = SmoothingMode.HighQuality;//设置字体质量
            g.TextContrast = textContrast;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.TextRenderingHint = TextRenderingHint.SystemDefault;

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawString(text, font, brush, layoutRectangle, format);
        }

        public static void DrawRightString(Graphics g, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            format.LineAlignment = StringAlignment.Far;

            g.SmoothingMode = SmoothingMode.HighQuality;//设置字体质量
            g.TextContrast = textContrast;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawString(text, font, brush, layoutRectangle, format);
        }
        public static void DrawLeftStrokeString(Graphics g, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            int strokeThickness = 2;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            // 构建路径
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(layoutRectangle.X, layoutRectangle.Y, layoutRectangle.Width, layoutRectangle.Height);
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, layoutRectangle, sf);

            // 绘制描边
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen strokePen = new Pen(Color.Black, strokeThickness))
            {
                g.DrawPath(strokePen, path);
            }

            // 填充文本
            g.FillPath(brush, path);
            g.TextContrast = textContrast;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }

        public static void DrawRightStrokeString(Graphics g, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            int strokeThickness = 2;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            // 构建路径
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(layoutRectangle.X, layoutRectangle.Y, layoutRectangle.Width, layoutRectangle.Height);
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, layoutRectangle, sf);

            // 绘制描边
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen strokePen = new Pen(Color.Black, strokeThickness))
            {
                g.DrawPath(strokePen, path);
            }

            // 填充文本
            g.FillPath(brush, path);
            g.TextContrast = textContrast;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }

        public static void DrawCenterStrokeString(Graphics g, string text, Font font, Brush brush, Rectangle layoutRectangle)
        {
            int strokeThickness = 2;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center; // 设置水平居中
            sf.LineAlignment = StringAlignment.Center; // 设置垂直居中

            // 构建路径
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(layoutRectangle.X, layoutRectangle.Y, layoutRectangle.Width, layoutRectangle.Height);
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, layoutRectangle, sf);

            // 绘制描边
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen strokePen = new Pen(Color.Black, strokeThickness))
            {
                g.DrawPath(strokePen, path);
            }

            // 填充文本
            g.FillPath(brush, path);
            g.TextContrast = textContrast;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }


        /// <summary>
        /// 文字渐变色
        /// </summary>
        public static void DrawLinearGradientString(Graphics g, string text, Font font, Rectangle rect, Rectangle ract, Color color1, Color color2)
        {
            LinearGradientMode[] arrMode = { LinearGradientMode.ForwardDiagonal, LinearGradientMode.Horizontal, LinearGradientMode.BackwardDiagonal, LinearGradientMode.Vertical };
            Random rnd = new Random();

            LinearGradientBrush brush = new LinearGradientBrush(ract, color1, color2, arrMode[rnd.Next(0, arrMode.Length)]);

            // 绘制渐变文字
            g.DrawString(text, font, brush, rect);
        }
    }
}
