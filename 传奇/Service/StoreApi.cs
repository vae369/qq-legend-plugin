using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 传奇.core;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace 传奇
{

    public class StoreApi
    {
        static string prgusePath = LegendFilePath.LegendIMGDataPath + "Prguse\\";
        static string prguse3Path = LegendFilePath.LegendIMGDataPath + "Prguse3\\";
        static string newopUIPath = LegendFilePath.LegendIMGDataPath + "NewopUI\\";
        static string itemsPath = LegendFilePath.LegendIMGDataPath + "Items\\";// 装备在背包的样子 


        static Font font14Bold = new Font("黑体", 14, FontStyle.Bold);
        static Font font10 = new Font("黑体", 10);
        static Font fontUnderline10 = new Font("黑体", 9, FontStyle.Underline);
        static Font font12 = new Font("黑体", 12);
        static Font font13 = new Font("黑体", 13);
        static Font font14 = new Font("黑体", 14);
        static Font font15 = new Font("黑体", 15);
        static Font font16 = new Font("黑体", 16);
        static Font font28 = new Font("黑体", 28);
        static Font fontUnderline16 = new Font("黑体", 16, FontStyle.Underline);
        static Font fontUnderline12 = new Font("黑体", 12, FontStyle.Underline);
        static Rectangle rect = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// 装备回收-查看面板
        /// </summary>
        public static string GetEquRecycle(UserInfo user, string equDesc, int x = 0, int y = 0, int w = 0, int h = 0)
        {
            try
            {
                string savePath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper config = new IniFileHelper(savePath);

                UserInfoImg userInfoImg = new UserInfoImg(user);
                string path = userInfoImg.GamgeMainForm(user, false, true, true, true, x, y, w, h);

                using (Image imgMian = Image.FromFile(path))
                {
                    using (Bitmap bitmap = new Bitmap(imgMian.Width, imgMian.Height))
                    {
                        using (Graphics gh = Graphics.FromImage(bitmap))
                        {
                            gh.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                            gh.DrawImage(imgMian, 0, 0, bitmap.Width, bitmap.Height);//添加主窗体背景

                            using (Image img = Image.FromFile(newopUIPath + "001911.png"))// 回收框
                            {
                                gh.DrawImage(img, 0, 12, img.Width, img.Height);
                            }
                            using (Image img = Image.FromFile(newopUIPath + "000280.png"))
                            {
                                gh.DrawImage(img, 433, 15, img.Width, img.Height);
                            }
                            string[] recArray = { "垃圾系列", "沃玛系列", "祖玛系列", "赤月系列", "永恒首饰", "永恒剑甲", "毁灭首饰", "毁灭剑甲", "刑天首饰", "焚天首饰", "沧澜首饰", "刑焚沧甲", "刑焚沧剑" };
                            int boxX = 30, boxY = 15;
                            for (int i = 0; i < recArray.Length; i++)
                            {
                                boxY += 20;

                                if (config.Read("装备回收属性", recArray[i]) == "是")
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "000229.png"))
                                    {
                                        gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                    }
                                }
                                else
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "000228.png"))// 选择框
                                    {
                                        gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                    }
                                }
                                rect = new Rectangle(boxX + 15, boxY, 0, 0);
                                TextHelper.DrawLeftString(gh, recArray[i], font12, Brushes.Yellow, rect);
                            }

                            recArray = new[] { "灭世首饰", "灭世剑甲", "轮回首饰", "轮回剑甲", "起源首饰", "起源剑甲", "回忆首饰", "回忆剑甲", "命运首饰", "命运剑甲", "边界首饰", "边界剑甲" };
                            boxX = 130;
                            boxY = 15;
                            for (int i = 0; i < recArray.Length; i++)
                            {
                                boxY += 20;
                                if (config.Read("装备回收属性", recArray[i]) == "是")
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "000229.png"))
                                    {
                                        gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                    }
                                }
                                else
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "000228.png"))
                                    {
                                        gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                    }
                                }
                                rect = new Rectangle(boxX + 15, boxY, 0, 0);
                                TextHelper.DrawLeftString(gh, recArray[i], font12, Brushes.Yellow, rect);
                            }


                            recArray = new[] { "毒蛇首饰", "毒蛇剑甲", "沙漠首饰", "沙漠剑甲", "古城首饰", "古城剑甲", "孤岛首饰", "孤岛剑甲", "仙门首饰", "仙门剑甲", "战神首饰", "战神剑甲" };
                            boxX = 230;
                            boxY = 15;
                            for (int i = 0; i < recArray.Length; i++)
                            {
                                boxY += 20;
                                if (config.Read("装备回收属性", recArray[i]) == "是")
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "000229.png"))
                                    {
                                        gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                    }
                                }
                                else
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "000228.png"))
                                    {
                                        gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                    }
                                }
                                rect = new Rectangle(boxX + 15, boxY, 0, 0);
                                TextHelper.DrawLeftString(gh, recArray[i], font12, Brushes.Yellow, rect);
                            }

                            recArray = new[] { "复古首饰", "复古剑甲", "一级神技", "二级神技", "三级神技", "四级神技", "五级神技", "六级神技", "初级时装", "复古时装", "血牛时装", "特殊物品" };
                            boxX = 330;
                            boxY = 15;
                            for (int i = 0; i < recArray.Length; i++)
                            {
                                boxY += 20;
                                if (config.Read("装备回收属性", recArray[i]) == "是")
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "000229.png"))
                                    {
                                        gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                    }
                                }
                                else
                                {
                                    using (Image img = Image.FromFile(newopUIPath + "000228.png"))
                                    {
                                        gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                    }
                                }
                                rect = new Rectangle(boxX + 15, boxY, 0, 0);
                                TextHelper.DrawLeftString(gh, recArray[i], font12, Brushes.Yellow, rect);
                            }
                            boxX = 30;
                            boxY = 310;
                            if (user.User_EquRecycleAttr.AutoRecycl == "是")
                            {
                                using (Image img = Image.FromFile(newopUIPath + "000229.png"))
                                {
                                    gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                }
                            }
                            else
                            {
                                using (Image img = Image.FromFile(newopUIPath + "000228.png"))// 选择框
                                {
                                    gh.DrawImage(img, boxX, boxY, img.Width, img.Height);
                                }
                            }
                            rect = new Rectangle(boxX + 15, boxY, 0, 0);
                            TextHelper.DrawLeftString(gh, "自动回收", font12, Brushes.Yellow, rect);

                            rect = new Rectangle(130, 308, 0, 0);
                            TextHelper.DrawLeftString(gh, "一键回收", fontUnderline12, Brushes.Yellow, rect);

                            rect = new Rectangle(228, 308, 0, 0);
                            TextHelper.DrawLeftString(gh, "全部勾选", fontUnderline12, Brushes.Yellow, rect);

                            rect = new Rectangle(328, 308, 0, 0);
                            TextHelper.DrawLeftString(gh, "取消全部勾选", fontUnderline12, Brushes.Yellow, rect);

                            rect = new Rectangle(30, 333, 0, 0);
                            TextHelper.DrawLeftString(gh, "仓库服务", fontUnderline12, Brushes.Yellow, rect);

                            rect = new Rectangle(110, 333, 0, 0);
                            TextHelper.DrawLeftString(gh, "拍卖市场", fontUnderline12, Brushes.Yellow, rect);

                            rect = new Rectangle(190, 333, 0, 0);
                            TextHelper.DrawLeftString(gh, "实物兑换", fontUnderline12, Brushes.Yellow, rect);

                            rect = new Rectangle(30, 357, 0, 0);
                            TextHelper.DrawLeftString(gh, "分解结晶", font12, Brushes.Aqua, rect);

                            rect = new Rectangle(110, 357, 0, 0);
                            TextHelper.DrawLeftString(gh, "查看分解结晶", fontUnderline12, Brushes.Yellow, rect);

                            int itemX = 0, itemY = 0;
                            if (equDesc.StartsWith("查看"))
                            {
                                // 设置矩形的填充颜色为半透明（Alpha = 100）
                                Color transparentColor = Color.FromArgb(200, 0, 0, 0); // 透明度为100
                                SolidBrush transparentBrush = new SolidBrush(transparentColor);
                                int boxWidth = 0;
                                int boxHeight = 0;
                                int jb = 0, yb = 0, exp = 0;
                                StringBuilder sb = new StringBuilder();
                                switch (equDesc)
                                {
                                    case "查看垃圾系列":
                                        itemX = 48;
                                        itemY = 50;
                                        boxWidth = 500;
                                        boxHeight = 320;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("基本剑术,攻杀剑术,刺杀剑术,半月弯刀,野蛮冲撞,烈火剑法");
                                        sb.AppendLine("火球术,抗拒火环,诱惑之光,地狱火,雷电术,瞬间移动,大火球");
                                        sb.AppendLine("爆裂火焰,火墙,疾光电影,地狱雷光,魔法盾,圣言术,冰咆哮");
                                        sb.AppendLine("治愈术,精神力战法,施毒术,灵魂火符,召唤骷髅,隐身术,集体隐身术");
                                        sb.AppendLine("幽灵盾,神圣战甲术,心灵启示,困魔咒,群体治疗术,召唤神兽");
                                        sb.AppendLine("木剑,匕首,乌木剑,青铜剑,短剑,铁剑,鹤嘴锄,青铜斧,罗刹,海魂,八荒,半月,凌风");
                                        sb.AppendLine("破魂,偃月,降魔,斩马刀,修罗,凝霜,布衣(男),轻型盔甲(男),中型盔甲(男)");
                                        sb.AppendLine("重盔甲(男),魔法长袍(男),灵魂战衣(男),战神盔甲(男),恶魔长袍(男)");
                                        sb.AppendLine("幽灵战衣(男),布衣(女),轻型盔甲(女),中型盔甲(女),重盔甲(女),魔法长袍(女)");
                                        sb.AppendLine("灵魂战衣(女),战神盔甲(女),恶魔长袍(女),幽灵战衣(女),青铜头盔,魔法头盔");
                                        sb.AppendLine("道士头盔,骷髅头盔,神秘头盔,记忆头盔,祈祷头盔,白色虎齿项链,灯笼项链");
                                        sb.AppendLine("记忆项链,金项链,传统项链,白金项链,黄色水晶项链,黑檀项链,黑色水晶项链");
                                        sb.AppendLine("琥珀项链,魔鬼项链,凤凰明珠,蓝翡翠项链,竹笛,放大镜,狂风项链,祈祷项链");
                                        sb.AppendLine("古铜戒指,六角戒指,玻璃戒指,牛角戒指,生铁戒指,蓝色水晶戒指,黑色水晶戒指");
                                        sb.AppendLine("蛇眼戒指,珍珠戒指,金戒指,骷髅戒指,降妖除魔戒指,珊瑚戒指,魅力戒指,道德戒指");
                                        sb.AppendLine("狂风戒指,祈祷戒指,神秘戒指,记忆戒指,铁手镯,银手镯,夏普儿手镯,避邪手镯");
                                        sb.AppendLine("记忆手镯,小手镯,大手镯,钢手镯,皮制手套,金手镯,坚固手套,魔法手镯,黑檀手镯");
                                        sb.AppendLine("道士手镯死神手套,铂金手镯,神秘腰带,魔血手镯,虹魔手镯,魔力手镯,祈祷手镯");
                                        sb.AppendLine("布鞋,鹿皮靴,紫绸靴,避魂靴,兽皮腰带,青铜腰带,钢铁腰带");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: sb.AppendLine("100金币1经验"); break;
                                            case 2: sb.AppendLine("200金币2经验"); break;
                                            case 3: sb.AppendLine("300金币3经验"); break;
                                            case 4: sb.AppendLine("400金币4经验"); break;
                                            case 5: sb.AppendLine("500金币5经验"); break;
                                            default: sb.AppendLine("100金币1经验"); break;
                                        }
                                        break;
                                    case "查看沃玛系列":
                                        itemX = 48;
                                        itemY = 70;
                                        boxWidth = 200;
                                        boxHeight = 95;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("炼狱,银蛇,井中月,无极棍,魔杖");
                                        sb.AppendLine("幽灵项链,生命项链,天珠项链");
                                        sb.AppendLine("龙之戒指,红宝石戒指,铂金戒指");
                                        sb.AppendLine("幽灵手套,思贝儿手镯,心灵手镯");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 2000; jb = 500; break;
                                            case 2: exp = 4000; jb = 1000; break;
                                            case 3: exp = 6000; jb = 5000; break;
                                            case 4: exp = 8000; jb = 3000; break;
                                            case 5: exp = 10000; jb = 4000; break;
                                            default: exp = 2000; jb = 500; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                        }
                                        sb.AppendLine($"{jb}金币{exp}经验");
                                        break;
                                    case "查看祖玛系列":
                                        itemX = 48;
                                        itemY = 90;
                                        boxWidth = 305;
                                        boxHeight = 95;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("裁决之杖,龙纹剑,血饮,骨玉权杖");
                                        sb.AppendLine("绿色项链,恶魔铃铛,灵魂项链");
                                        sb.AppendLine("力量戒指,紫碧螺,泰坦戒指");
                                        sb.AppendLine("骑士手镯,龙之手镯,三眼手镯,阎罗手套,黑铁手套");

                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 5000; jb = 2000; break;
                                            case 2: exp = 10000; jb = 4000; break;
                                            case 3: exp = 20000; jb = 6000; break;
                                            case 4: exp = 30000; jb = 8000; break;
                                            case 5: exp = 40000; jb = 10000; break;
                                            default: exp = 5000; jb = 2000; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                        }
                                        sb.AppendLine($"{jb}金币{exp}经验");
                                        break;
                                    case "查看赤月系列":
                                        itemX = 48;
                                        itemY = 110;
                                        boxWidth = 365;
                                        boxHeight = 110;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("屠龙,命运之刃,霸者之刃,龙牙,怒斩,逍遥扇,嗜魂法杖");
                                        sb.AppendLine("天魔神甲,法神披风,天尊道袍,圣战宝甲,霓裳羽衣,天师长袍");
                                        sb.AppendLine("圣战项链,法神项链,天尊项链");
                                        sb.AppendLine("圣战戒指,法神戒指,天尊戒指");
                                        sb.AppendLine("圣战手镯,法神手镯,天尊手镯");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 50000; jb = 5000; break;
                                            case 2: exp = 100000; jb = 10000; break;
                                            case 3: exp = 200000; jb = 20000; break;
                                            case 4: exp = 300000; jb = 30000; break;
                                            case 5: exp = 400000; jb = 40000; break;
                                            default: exp = 50000; jb = 5000; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                        }
                                        sb.AppendLine($"{jb}金币{exp}经验");
                                        break;
                                    case "查看永恒首饰":
                                        itemX = 48;
                                        itemY = 130;
                                        boxWidth = 185;
                                        boxHeight = 95;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("宙神头盔,朱雀头盔,道域头盔");
                                        sb.AppendLine("永恒圣链,永恒圣戒,永恒圣镯");
                                        sb.AppendLine("永恒法链,永恒法戒,永恒法镯");
                                        sb.AppendLine("永恒道链,永恒道戒,永恒道镯");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 100000; yb = 10; break;
                                            case 2: exp = 300000; yb = 20; break;
                                            case 3: exp = 400000; yb = 30; break;
                                            case 4: exp = 500000; yb = 40; break;
                                            case 5: exp = 600000; yb = 60; break;
                                            default: exp = 100000; yb = 10; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看永恒剑甲":
                                        itemX = 48;
                                        itemY = 150;
                                        boxWidth = 345;
                                        boxHeight = 80;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("永恒魔剑,永恒圣刃,黄金屠龙,黄金逍遥扇,黄金噬魂法杖");
                                        sb.AppendLine("白虎战甲,青龙道袍,朱雀神袍");
                                        sb.AppendLine("白虎战衣,青龙道衣,朱雀神衣");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 400000; yb = 40; break;
                                            case 2: exp = 600000; yb = 60; break;
                                            case 3: exp = 800000; yb = 80; break;
                                            case 4: exp = 1000000; yb = 100; break;
                                            case 5: exp = 1200000; yb = 120; break;
                                            default: exp = 400000; yb = 40; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看毁灭首饰":
                                        itemX = 48;
                                        itemY = 170;
                                        boxWidth = 185;
                                        boxHeight = 140;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("毁灭战链,毁灭法链,毁灭道链");
                                        sb.AppendLine("毁灭战戒,毁灭法戒,毁灭道戒");
                                        sb.AppendLine("毁灭战镯,毁灭法镯,毁灭道镯");
                                        sb.AppendLine("毁灭战章,毁灭法章,毁灭道章");
                                        sb.AppendLine("毁灭战盔,毁灭法盔,毁灭道盔");
                                        sb.AppendLine("毁灭战靴,毁灭法靴,毁灭道靴");
                                        sb.AppendLine("毁灭战带,毁灭法带,毁灭道带");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 800000; yb = 60; break;
                                            case 2: exp = 1000000; yb = 100; break;
                                            case 3: exp = 1200000; yb = 200; break;
                                            case 4: exp = 1400000; yb = 300; break;
                                            case 5: exp = 1600000; yb = 400; break;
                                            default: exp = 800000; yb = 60; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看毁灭剑甲":
                                        itemX = 48;
                                        itemY = 190;
                                        boxWidth = 185;
                                        boxHeight = 95;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("毁灭战刃,毁灭法杖,毁灭羽扇");
                                        sb.AppendLine("毁灭战甲,毁灭魔甲,毁灭道甲");
                                        sb.AppendLine("毁灭战衣,毁灭魔衣,毁灭道衣");
                                        sb.AppendLine("毁灭神甲,毁灭神袍");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 1000000; yb = 500; break;
                                            case 2: exp = 1500000; yb = 600; break;
                                            case 3: exp = 2000000; yb = 800; break;
                                            case 4: exp = 2500000; yb = 1000; break;
                                            case 5: exp = 3000000; yb = 1200; break;
                                            default: exp = 1000000; yb = 500; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看刑天首饰":
                                        itemX = 48;
                                        itemY = 210;
                                        boxWidth = 185;
                                        boxHeight = 80;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("刑天战盔,刑天战链,刑天护腕");
                                        sb.AppendLine("刑天战戒,刑天腰带,刑天战靴");
                                        sb.AppendLine("刑天勋章");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 1; yb = 400; break;
                                            case 2: exp = 2; yb = 500; break;
                                            case 3: exp = 3; yb = 600; break;
                                            case 4: exp = 4; yb = 700; break;
                                            case 5: exp = 5; yb = 800; break;
                                            default: exp = 1; yb = 400; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看焚天首饰":
                                        itemX = 48;
                                        itemY = 230;
                                        boxWidth = 185;
                                        boxHeight = 80;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("焚天道盔,焚天道链,焚天手镯");
                                        sb.AppendLine("焚天腰带,焚天道靴,焚天道戒");
                                        sb.AppendLine("焚天勋章");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 1; yb = 400; break;
                                            case 2: exp = 2; yb = 500; break;
                                            case 3: exp = 3; yb = 600; break;
                                            case 4: exp = 4; yb = 700; break;
                                            case 5: exp = 5; yb = 800; break;
                                            default: exp = 1; yb = 400; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看沧澜首饰":
                                        itemX = 48;
                                        itemY = 250;
                                        boxWidth = 185;
                                        boxHeight = 80;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("沧澜魔盔,沧澜魔链,沧澜手镯");
                                        sb.AppendLine("沧澜魔戒,沧澜腰带,沧澜魔靴");
                                        sb.AppendLine("沧澜勋章");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 1; yb = 400; break;
                                            case 2: exp = 2; yb = 500; break;
                                            case 3: exp = 3; yb = 600; break;
                                            case 4: exp = 4; yb = 700; break;
                                            case 5: exp = 5; yb = 800; break;
                                            default: exp = 1; yb = 400; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看刑焚沧甲":
                                        itemX = 48;
                                        itemY = 270;
                                        boxWidth = 185;
                                        boxHeight = 65;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("刑天战甲,沧澜魔甲,焚天道甲");
                                        sb.AppendLine("刑天战袍,沧澜魔袍,焚天道袍");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 1; yb = 800; break;
                                            case 2: exp = 2; yb = 1000; break;
                                            case 3: exp = 3; yb = 2000; break;
                                            case 4: exp = 4; yb = 3000; break;
                                            case 5: exp = 5; yb = 4000; break;
                                            default: exp = 1; yb = 800; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看刑焚沧剑":
                                        itemX = 48;
                                        itemY = 290;
                                        boxWidth = 185;
                                        boxHeight = 50;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("刑天之怒,焚天之吼,沧澜之啸");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 1; yb = 3000; break;
                                            case 2: exp = 2; yb = 5000; break;
                                            case 3: exp = 3; yb = 6000; break;
                                            case 4: exp = 4; yb = 7000; break;
                                            case 5: exp = 5; yb = 8000; break;
                                            default: exp = 1; yb = 3000; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                    case "查看特殊物品":
                                        itemX = 347;
                                        itemY = 270;
                                        boxWidth = 260;
                                        boxHeight = 66;
                                        sb.AppendLine("可回收");
                                        sb.AppendLine("麻痹戒指,复活戒指,传送戒指,火焰戒指");
                                        sb.AppendLine("护身戒指,超负载戒指,防御戒指,赤月斗笠");
                                        switch (user.User_General.VipLv)
                                        {
                                            case 1: exp = 10; yb = 300; break;
                                            case 2: exp = 20; yb = 500; break;
                                            case 3: exp = 30; yb = 600; break;
                                            case 4: exp = 40; yb = 700; break;
                                            case 5: exp = 50; yb = 800; break;
                                            default: exp = 1; yb = 300; break;
                                        }
                                        if (user.User_VIPAttribute.MonExpInc > 0)
                                        {
                                            exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                            yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                        }
                                        sb.AppendLine($"{yb}元宝{exp}经验");
                                        break;
                                }

                                string newpath = newopUIPath + "000044.png";
                                // 绘制一个矩形
                                gh.FillRectangle(transparentBrush, itemX, itemY, boxWidth, boxHeight);

                                using (Image img = Image.FromFile(newpath))// 线-上
                                {
                                    gh.DrawImage(img, itemX, itemY, boxWidth, img.Height);
                                }
                                using (Image img = Image.FromFile(newpath))// 线-下
                                {
                                    gh.DrawImage(img, itemX, itemY + boxHeight - (img.Height / 2), boxWidth, img.Height);
                                }

                                newpath = newopUIPath + "000045.png";
                                using (Image img = Image.FromFile(newpath))// 线-左
                                {
                                    gh.DrawImage(img, itemX, itemY, img.Width, boxHeight);
                                }
                                using (Image img = Image.FromFile(newpath))// 线-右
                                {
                                    gh.DrawImage(img, itemX + boxWidth, itemY, img.Width, boxHeight);
                                }

                                newpath = newopUIPath + "000042.png";
                                using (Image img = Image.FromFile(newpath))// 左上角框框
                                {
                                    gh.DrawImage(img, itemX, itemY, img.Width, img.Height);
                                }
                                newpath = newopUIPath + "000043.png";
                                using (Image img = Image.FromFile(newpath))// 左下角框框
                                {
                                    gh.DrawImage(img, itemX, itemY + boxHeight - img.Width, img.Width, img.Height);
                                }
                                newpath = newopUIPath + "000040.png";
                                using (Image img = Image.FromFile(newpath))// 右上角框框
                                {
                                    gh.DrawImage(img, itemX + boxWidth - (img.Height / 2) - 1, itemY, img.Width, img.Height);
                                }
                                newpath = newopUIPath + "000041.png";
                                using (Image img = Image.FromFile(newpath))// 右下角框框
                                {
                                    gh.DrawImage(img, itemX + boxWidth - (img.Height / 2) - 1, itemY + boxHeight - img.Width + 1, img.Width, img.Height);
                                }

                                itemX += 5;
                                itemY += 5;
                                rect = new Rectangle(itemX, itemY, 0, 0);
                                TextHelper.DrawLeftStrokeString(gh, sb.ToString(), font13, Brushes.White, rect);
                            }

                        }
                        path = $"{LegendFilePath.imgPath + user.UserQQ}.png";
                        bitmap.Save(path, ImageFormat.Png);
                    }
                }

                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 勾选装备回收
        /// </summary>
        public static void CheckedEquRecycle(UserInfo user, string equDesc)
        {
            try
            {
                equDesc = equDesc.Replace("勾选", "");
                bool isCancel = false;
                if (equDesc.StartsWith("取消"))
                {
                    equDesc = equDesc.Replace("取消", "");
                    isCancel = true;
                }
                if (equDesc == "自动回收")
                {
                    if (user.User_General.VipLv < 3) throw new Exception("对不起，只有黄金以上的会员才能使用自动回收哦,详情请发送 会员特权 查看。");
                    if (!isCancel)
                        user.User_EquRecycleAttr.AutoRecycl = "是";
                    else
                        user.User_EquRecycleAttr.AutoRecycl = "否";
                }

                string savePath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper config = new IniFileHelper(savePath);

                if (!isCancel)
                {
                    if (equDesc != "全部")
                    {
                        config.Write("装备回收属性", equDesc, "是");
                    }
                    else
                    {
                        var list = config.GetAllKeys("装备回收属性");
                        foreach (string key in list)
                        {
                            config.Write("装备回收属性", key, "是");
                        }
                    }
                }
                else
                {
                    if (equDesc != "全部")
                    {
                        config.Write("装备回收属性", equDesc, "否");
                    }
                    else
                    {
                        var list = config.GetAllKeys("装备回收属性");
                        foreach (string key in list)
                        {
                            config.Write("装备回收属性", key, "否");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 一键回收背包里的装备
        /// </summary>
        public static bool EquRecycleByPb(UserInfo user)
        {
            try
            {
                bool flag = false;
                string savePath = LegendFilePath.UserPath + user.UserQQ + ".ini";
                IniFileHelper userInI = new IniFileHelper(savePath);
                string[] arrRecyclStd;
                string strRecyclStd = "";
                var pbList = userInI.GetAllKeys("背包物品");
                int exp = 0, yb = 0, jb = 0;
                if (user.User_EquRecycleAttr.AutoRecyclType1 == "是")
                {
                    // 取回收的垃圾装备
                    string stdPath = LegendFilePath.MonQuestPath + "垃圾装备.txt";
                    var ljqeulist = FileHelper.ReadTxtReturnList(stdPath);
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            if (ljqeulist.Contains(std.StdName))
                            {
                                exp = 1;
                                jb = 100;
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.JB += jb;
                                userInI.Write("普通数据", "金币", user.User_General.JB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType2 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "炼狱,银蛇,井中月,无极棍,魔杖,幽灵项链,生命项链,天珠项链,龙之戒指,红宝石戒指,铂金戒指,幽灵手套,思贝儿手镯,心灵手镯";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                exp = 10000;
                                switch (user.User_General.VipLv)
                                {
                                    case 1: jb = 500; break;
                                    case 2: jb = 1000; break;
                                    case 3: jb = 5000; break;
                                    case 4: jb = 3000; break;
                                    case 5: jb = 4000; break;
                                    default: jb = 500; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.JB += jb;
                                userInI.Write("普通数据", "金币", user.User_General.JB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType3 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "裁决之杖,龙纹剑,血饮,骨玉权杖,黑铁头盔,绿色项链,恶魔铃铛,灵魂项链,力量戒指,紫碧螺,泰坦戒指,骑士手镯,龙之手镯,三眼手镯,阎罗手套,黑铁手套";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                exp = 40000;
                                switch (user.User_General.VipLv)
                                {
                                    case 1: jb = 2000; break;
                                    case 2: jb = 4000; break;
                                    case 3: jb = 6000; break;
                                    case 4: jb = 8000; break;
                                    case 5: break;
                                    default: jb = 2000; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "金币", user.User_General.JB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType4 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "屠龙,命运之刃,霸者之刃,龙牙,怒斩,逍遥扇,嗜魂法杖,天魔神甲,法神披风,天尊道袍,圣战宝甲,霓裳羽衣,天师长袍,圣战头盔,圣战项链,圣战戒指,圣战手镯,法神头盔,法神项链,法神戒指,法神手镯,天尊头盔,天尊项链,天尊戒指,天尊手镯,赤月腰带,赤月靴子";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                exp = 400000;
                                switch (user.User_General.VipLv)
                                {
                                    case 1: jb = 5000; break;
                                    case 2: jb = 10000; break;
                                    case 3: jb = 20000; break;
                                    case 4: jb = 30000; break;
                                    case 5: jb = 40000; break;
                                    default: jb = 5000; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.JB += jb;
                                userInI.Write("普通数据", "金币", user.User_General.JB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType5 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "宙神头盔,朱雀头盔,道域头盔,永恒圣链,永恒法链,永恒道链,永恒圣戒,永恒法戒,永恒道戒,永恒圣镯,永恒法镯,永恒道镯";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                exp = 600000;
                                switch (user.User_General.VipLv)
                                {
                                    case 1: yb = 10; break;
                                    case 2: yb = 20; break;
                                    case 3: yb = 30; break;
                                    case 4: yb = 40; break;
                                    case 5: yb = 60; break;
                                    default: yb = 10; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType6 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "永恒魔剑,永恒圣刃,黄金屠龙,黄金逍遥扇,黄金噬魂法杖,白虎战甲,青龙道袍,朱雀神袍,白虎战衣,青龙道衣,朱雀神衣";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                exp = 1200000;
                                switch (user.User_General.VipLv)
                                {
                                    case 1: yb = 10; break;
                                    case 2: yb = 20; break;
                                    case 3: yb = 30; break;
                                    case 4: yb = 40; break;
                                    case 5: yb = 60; break;
                                    default: yb = 10; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType7 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "毁灭战链,毁灭战戒,毁灭战镯,毁灭战章,毁灭战盔,毁灭战靴,毁灭战带,毁灭法链,毁灭法戒,毁灭法镯,毁灭法章,毁灭法盔,毁灭法靴,毁灭法带,毁灭道链,毁灭道戒,毁灭道镯,毁灭道章,毁灭道盔,毁灭道靴,毁灭道带";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                exp = 1600000;
                                switch (user.User_General.VipLv)
                                {
                                    case 1: yb = 60; break;
                                    case 2: yb = 100; break;
                                    case 3: yb = 200; break;
                                    case 4: yb = 300; break;
                                    case 5: yb = 400; break;
                                    default: yb = 60; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    jb = user.User_VIPAttribute.MonExpInc / 100 * jb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType8 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "毁灭战刃,毁灭法杖,毁灭羽扇,毁灭战甲,毁灭魔甲,毁灭道甲,毁灭战衣,毁灭魔衣,毁灭道衣,毁灭神甲,毁灭神袍";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                exp = 3000000;
                                switch (user.User_General.VipLv)
                                {
                                    case 1: yb = 500; break;
                                    case 2: yb = 600; break;
                                    case 3: yb = 800; break;
                                    case 4: yb = 1000; break;
                                    case 5: yb = 1200; break;
                                    default: yb = 500; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType9 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "刑天战盔,刑天战链,刑天护腕,刑天战戒,刑天腰带,刑天战靴,刑天勋章";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                switch (user.User_General.VipLv)
                                {
                                    case 1: exp = 1; yb = 400; break;
                                    case 2: exp = 2; yb = 500; break;
                                    case 3: exp = 3; yb = 600; break;
                                    case 4: exp = 4; yb = 700; break;
                                    case 5: exp = 5; yb = 800; break;
                                    default: exp = 1; yb = 400; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType10 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "焚天道盔,焚天道链,焚天手镯,焚天腰带,焚天道靴,焚天道戒,焚天勋章";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                switch (user.User_General.VipLv)
                                {
                                    case 1: exp = 1; yb = 400; break;
                                    case 2: exp = 2; yb = 500; break;
                                    case 3: exp = 3; yb = 600; break;
                                    case 4: exp = 4; yb = 700; break;
                                    case 5: exp = 5; yb = 800; break;
                                    default: exp = 1; yb = 400; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType11 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "沧澜魔盔,沧澜魔链,沧澜手镯,沧澜魔戒,沧澜腰带,沧澜魔靴,沧澜勋章";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                switch (user.User_General.VipLv)
                                {
                                    case 1: exp = 1; yb = 400; break;
                                    case 2: exp = 2; yb = 500; break;
                                    case 3: exp = 3; yb = 600; break;
                                    case 4: exp = 4; yb = 700; break;
                                    case 5: exp = 5; yb = 800; break;
                                    default: exp = 1; yb = 400; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType12 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "沧刑天战甲,沧澜魔甲,焚天道甲,刑天战袍,沧澜魔袍,焚天道袍";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                switch (user.User_General.VipLv)
                                {
                                    case 1: exp = 1; yb = 800; break;
                                    case 2: exp = 2; yb = 1000; break;
                                    case 3: exp = 3; yb = 2000; break;
                                    case 4: exp = 4; yb = 3000; break;
                                    case 5: exp = 5; yb = 4000; break;
                                    default: exp = 1; yb = 800; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }
                if (user.User_EquRecycleAttr.AutoRecyclType13 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "刑天之怒,焚天之吼,沧澜之啸";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                switch (user.User_General.VipLv)
                                {
                                    case 1: exp = 1; yb = 3000; break;
                                    case 2: exp = 2; yb = 5000; break;
                                    case 3: exp = 3; yb = 6000; break;
                                    case 4: exp = 4; yb = 7000; break;
                                    case 5: exp = 5; yb = 8000; break;
                                    default: exp = 1; yb = 3000; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }

                if (user.User_EquRecycleAttr.AutoRecyclType49 == "是")
                {
                    foreach (string key in pbList)
                    {
                        StditemDB std = Api.JsonDeserialize<StditemDB>(userInI.ReadValue("背包物品", key));
                        if (std != null)
                        {
                            strRecyclStd = "麻痹戒指,复活戒指,传送戒指,火焰戒指,护身戒指,超负载戒指,防御戒指,赤月斗笠";
                            arrRecyclStd = strRecyclStd.Split(',');
                            if (arrRecyclStd.Contains(std.StdName))
                            {
                                switch (user.User_General.VipLv)
                                {
                                    case 1: exp = 10; yb = 300; break;
                                    case 2: exp = 20; yb = 500; break;
                                    case 3: exp = 30; yb = 600; break;
                                    case 4: exp = 40; yb = 700; break;
                                    case 5: exp = 50; yb = 800; break;
                                    default: exp = 10; yb = 300; break;
                                }
                                if (user.User_VIPAttribute.MonExpInc > 0)
                                {
                                    exp = user.User_VIPAttribute.MonExpInc / 100 * exp;
                                    yb = user.User_VIPAttribute.MonExpInc / 100 * yb;
                                }
                                user.User_General.SumExp += exp;
                                user.User_General.YB += yb;
                                userInI.Write("普通数据", "元宝", user.User_General.YB + "");
                                userInI.Write("普通数据", "总经验", user.User_General.SumExp + "");
                                userInI.Delete("背包物品", key);
                                flag = true;
                            }
                        }
                    }
                }

                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
