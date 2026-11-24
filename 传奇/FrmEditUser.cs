using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 传奇.core;

namespace 传奇
{
    public partial class FrmEditUser : Form
    {
        public string UserQQ { get; set; }
        UserInfo user { get; set; }

        public FrmEditUser()
        {
            InitializeComponent();
        }

        private void FrmEditUser_Load(object sender, EventArgs e)
        {
            user = UserInfoApi.GetInfoByQQ(UserQQ);
            this.Text = $"修改QQ({user.UserQQ})[{user.Name}]用户信息";
        }


    }
}
