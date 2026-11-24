using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 传奇
{
    public partial class FrmEditMap : Form
    {
        public LengendMapInfo MapInfo { get; set; }

        public FrmEditMap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            MapInfo.LMI_MapName = txtMapName.Text.Trim();
            MessageBox.Show("操作成功!");
            this.Close();
        }

        private void FrmEditMap_Load(object sender, EventArgs e)
        {
            txtMapName.Text = MapInfo.LMI_MapName;
        }
    }
}
