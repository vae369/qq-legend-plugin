using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 传奇.core
{
    public class FormattedText
    {
        private string text;
        private Font font;
        private Brush defaultBrush;
        private TextAlignment alignment;
        private TextTrimming trimming;
        public Dictionary<int, Brush> colorMap = new Dictionary<int, Brush>();

        public FormattedText(string text, Font font, Brush defaultBrush, Brush firstBrush, TextAlignment alignment, TextTrimming trimming)
        {
            this.text = text;
            this.font = font;
            this.defaultBrush = defaultBrush;
            this.alignment = alignment;
            this.trimming = trimming;
            colorMap[0] = firstBrush; // 默认起始颜色
        }

        public void SetSpanForegroundBrush(int startIndex, int length, Brush brush)
        {
            for (int i = startIndex; i < startIndex + length; i++)
            {
                colorMap[i] = brush; // 设置指定范围的文字颜色
            }
        }

        public string ToGdiRichText()
        {
            return text; // 这里简化处理,实际使用时可能需要更复杂的逻辑来处理不同颜色的绘制,例如通过多次调用DrawString或使用GraphicsPath等.
        }
    }
}
