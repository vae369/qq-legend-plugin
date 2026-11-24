using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace 传奇.Models
{
    public class UserInfo_Manager
    {
        public int Id { get; set; }
        public string UserStatus { get; set; }
        public string UserQQ { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Job { get; set; }
        public int Lv { get; set; }
        public string Map { get; set; }
        public string RoleType { get; set; }
        public int KillMonNum { get; set; }
        public int KillCityMonNum { get; set; }
        public long JB { get; set; }
        public long YB { get; set; }
        public int JGS { get; set; }
        public int REP { get; set; }
        public int PkPoint { get; set; } = 0;
    }
}
