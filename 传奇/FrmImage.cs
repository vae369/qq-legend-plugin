using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 传奇
{
    public partial class FrmImage : Form
    {
        public LengendMapInfo lengedMapInfo { get; set; }
        public FrmImage()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindImage(string imgPath)
        {
            //让pictureBox显示图像,并且不被占用,能即时删除图片
            System.Drawing.Image imgModel = System.Drawing.Image.FromFile(imgPath);   //加载图片
            MemoryStream mstr = new MemoryStream(); //创建新的MemoryStream
            imgModel.Save(mstr, ImageFormat.Png);// 保存这个对象

            pictureBox1.Image = System.Drawing.Image.FromStream(mstr);

            imgModel.Dispose();//释放占用
        }

        private void FrmImage_Load(object sender, EventArgs e)
        {
            this.Text = lengedMapInfo.LMI_MapName;
            string imgPath = $"{LegendFilePath.LegendIMGDataPath + "地图\\" + lengedMapInfo.LMI_ID}_{lengedMapInfo.LMI_MapName}.png";

            if (File.Exists(imgPath))
            {
                bindImage(imgPath);
            }
        }
    }
}
