using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace 传奇
{
    public static class Api
    {
        #region 动态引用API接口
        private const string MQ = "MingQQAPI.DLL";
        [DllImport(MQ)]
        private static extern IntPtr Api_GetQQLevel(string RobotQQ); //获取等级 活跃天数 升级剩余活跃天数
        [DllImport(MQ)]
        private static extern IntPtr Api_Logout(string RobotQQ);//令指定QQ下线,应确保QQ号码已在列表中且在线
        [DllImport(MQ)]
        private static extern IntPtr Api_GNGetGid(string GroupNum);//群号转群ID
        [DllImport(MQ)]
        private static extern IntPtr Api_GidGetGN(string GroupID);//群ID转群号
        [DllImport(MQ)]
        private static extern IntPtr Api_SetDisGroupName(string RobotQQ, string GroupNID, string Name);//修改讨论组名称

        [DllImport(MQ)]
        private static extern IntPtr Api_SetFriendsRemark(string RobotQQ, string ObjQQ, string Name);//修改好友备注姓名
        [DllImport(MQ)]
        private static extern IntPtr Api_GetFriendsRemark(string RobotQQ, string ObjQQ);// 取好友备注姓名（成功返回备注,失败或无备注返回空）

        [DllImport(MQ)]
        private static extern IntPtr Api_SendPack(string RobotQQ, string PcakText);//向服务器发送原始封包（成功返回服务器返回的包 失败返回空）
        [DllImport(MQ)]
        private static extern IntPtr Api_GetOnlineQQlist();//取框架在线可用的QQ列表 换行符分割

        [DllImport(MQ)]
        private static extern IntPtr Api_SetGender(string RobotQQ, string x);//设置机器人性别“1”为男 “2”为女

        [DllImport(MQ)]
        private static extern IntPtr Api_SetNick(string RobotQQ, string x);//设置机器人昵称
        [DllImport(MQ)]
        private static extern IntPtr Api_GetExpertDays(string RobotQQ, string ObjQQ);//查询对象或自身QQ达人天数（返回实际天数 失败返回-1）
        [DllImport(MQ)]
        private static extern IntPtr Api_TakeGift(string RobotQQ);//抽取群礼物（返回结果Json,需群聊等级LV5后）
        [DllImport(MQ)]
        private static extern IntPtr Api_SetSign(string RobotQQ, string msg);//设置个人签名
        [DllImport(MQ)]
        private static extern IntPtr Api_GetAge(string RobotQQ, string ObjQQ);//取年龄 成功返回年龄 失败返回-1

        [DllImport(MQ)]
        private static extern IntPtr Api_CheckGift(string RobotQQ);//查询我的群礼物（返回Json格式） QQMini Pro才可用

        [DllImport(MQ)]
        private static extern IntPtr Api_GiveGift(string RobotQQ, string GroupNum, string ObjQQ, string pid);//送群礼物（返回Json格式
        [DllImport(MQ)]
        private static extern IntPtr Api_GetGroupChatLv(string RobotQQ, string GroupNum, string ObjQQ);//查询对象或自身群聊等级（返回Json格式）
        [DllImport(MQ)]
        private static extern IntPtr Api_DelQQ(string RobotQQ);//删除框架帐号列表内指定QQ,不可在执行登录过程中删除QQ否则有几率引起错误   QQMini Pro才可用

        [DllImport(MQ)]
        private static extern IntPtr Api_BeInput(string RobotQQ, string ObjQQ);//置正在输入状态（发送消息后会打断状态）
        [DllImport(MQ)]
        private static extern IntPtr Api_GetQQAddMode(string RobotQQ, string ObjQQ);//取对象好友添加验证方式 （00 允许任何人  01 需要身份验证  03 需回答正确问题  04 需回答问题  99 已经是好友）
        [DllImport(MQ)]
        private static extern IntPtr Api_IsOnline(string RobotQQ, string ObjQQ);//查询对象是否在线
        [DllImport(MQ)]
        private static extern IntPtr Api_GetOnlineState(string RobotQQ, string ObjQQ);//查询对象在线状态   返回 1、在线 2、Q我 3、离开 4、忙碌 5、勿扰 6、隐身或离线

        [DllImport(MQ)]
        private static extern IntPtr Api_GetGroupMemberNum(string RobotQQ, string GroupNum);//查询对象群当前人数和上限人数
        [DllImport(MQ)]
        private static extern IntPtr Api_GetWpa(string RobotQQ, string ObjQQ);//查询对方是否允许在线状态临时会话消息（非讨论组和群临时）
        [DllImport(MQ)]
        private static extern IntPtr Api_GetDisGroupName(string RobotQQ, string DisGroupID);//取讨论组名称,需执行的讨论组ID
        [DllImport(MQ)]
        private static extern IntPtr Api_SetDisGroup(string RobotQQ, string url);//通过连接加入讨论组
        [DllImport(MQ)]
        private static extern IntPtr Api_GetDisGroup(string RobotQQ, string DisGroupID);//通过讨论组ID获取加群连接
        [DllImport(MQ)]
        private static extern IntPtr Api_UpLoad(string RobotQQ, int hInstance);//上传头像（通过读入字节集方式）成功真 失败假
        [DllImport(MQ)]
        private static extern IntPtr Api_PhotoWallUpload(string RobotQQ, int hInstance);//上传封面（通过读入字节集方式）成功真 失败假
        [DllImport(MQ)]
        private static extern IntPtr Api_GetGroupList_A(string RobotQQ);//取得群列表,#换行符分割 不受最高获取500群限制（如需获取群名称请对应调用 Api_GetGroupName 理论群名获取不会频繁）
        [DllImport(MQ)]
        private static extern IntPtr Api_GetGroupList_B(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetGroupMemberList(string RobotQQ, string GroupM);//取得群成员列表,返回获取到的原始JSON格式信息,需自行解析（有群员昵称）
        [DllImport(MQ)]
        private static extern IntPtr Api_GetAdminList(string RobotQQ, string GroupM);//取包括群主在内的群管列表
        [DllImport(MQ)]
        private static extern IntPtr Api_GetDisGroupMemberList(string RobotQQ, string GroupNum);//取出讨论组成员列表 （返回格式为 换行符分割开的）
        [DllImport(MQ)]
        private static extern IntPtr Api_SetStatus(string RobotQQ, int i, string ms); //设置机器人在线状态+附加信息,1~6 分别对应我在线上, Q我吧, 离开, 忙碌, 请勿打扰, 隐身
        [DllImport(MQ)]
        private static extern IntPtr Api_GetStatus(string RobotQQ);//获取机器人状态信息,成功返回：昵称、帐号、在线状态、速度、收信、发信、在线时间,失败返回空
        [DllImport(MQ)]
        private static extern IntPtr Api_Like(string RobotQQ, string QQ);//调用一次点赞一次,成功返回空,失败返回理由如：每天最多给他点十个赞哦,调用此Api时应注意频率,每人每日可被赞10次,每日每Q最多可赞50人
        [DllImport(MQ)]
        private static extern IntPtr Api_AddFriend(string RobotQQ, string ObjQQ, string msg);//.版本 2主动加好友 成功返回真 失败返回假 当对象设置需要正确回答问题或不允许任何人添加时无条件失败 QQMini Pro才可用

        [DllImport(MQ)]
        private static extern IntPtr Api_GetVer();
        [DllImport(MQ)]
        private static extern IntPtr Api_GetTimeStamp();
        [DllImport(MQ)]
        private static extern IntPtr Api_GetLog();
        [DllImport(MQ)]
        private static extern IntPtr Api_IsEnable();
        [DllImport(MQ)]
        private static extern IntPtr Api_DisabledPlugin();
        [DllImport(MQ)]
        private static extern IntPtr Api_OutPut(string Log);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetNick(string RobotQQ, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_SendMsg(string RobotQQ, int MsgType, string MsgTo, string ObjQQ, string Msg, int ID);
        [DllImport(MQ)]
        private static extern IntPtr Api_SendXML(string RobotQQ, int SendType, int MsgFrom, string MsgTo, string ObjQQ, string ObjectMsg, int ObjeCType);
        [DllImport(MQ)]
        private static extern IntPtr Api_SendJSON(string RobotQQ, int SendType, int MsgType, string MsgTo, string ObjQQ, string Json);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetFriendList(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetGroupList(string RobotQQ);//取得群列表,返回获取到的原始JSON格式信息,需自行解析

        [DllImport(MQ)]
        private static extern IntPtr Api_GetCookies(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetBlogPsKey(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetZonePsKey(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetGroupPsKey(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetClassRoomPsKey(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetBkn(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetBkn32(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetClientkey(string RobotQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_SessionKey(string RobotQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_GetNotice(string RobotQQ, string GroupNum);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetPicLink(string RobotQQ, int PicType, string ReferenceObj, string PicGUID);

        [DllImport(MQ)]
        private static extern IntPtr Api_UpLoadVoice(string RobotQQ, int type, string GroupNum, int pAmr);
        [DllImport(MQ)]
        private static extern IntPtr Api_UpLoadPic(string RobotQQ, int UpLoadType, string UpTo, int hInstance);
        [DllImport(MQ)]
        private static extern IntPtr Api_JoinGroup(string RobotQQ, string GroupNUm, string Reason);
        [DllImport(MQ)]
        private static extern IntPtr Api_QuitGroup(string RobotQQ, string GroupNum);
        [DllImport(MQ)]
        private static extern IntPtr Api_SendVoice(string RobotQQ, string ObjQQ, int pAmr);
        [DllImport(MQ)]
        private static extern IntPtr Api_AddBkList(string RobotQQ, string ObjQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_DelBkList(string RobotQQ, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_ShutUP(string RobotQQ, string GroupNum, string ObjQQ, int Time);
        [DllImport(MQ)]
        private static extern IntPtr Api_IsShutUp(string RobotQQ, string GroupNum, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_SetAnon(string RobotQQ, string GroupNum, bool swit);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetDisGroupList(string RobotQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_ShakeWindow(string RobotQQ, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetObjInfo(string RobotQQ, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_DelFriend(string RobotQQ, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_SetShieldedGroup(string RobotQQ, string GroupNum, bool type);
        [DllImport(MQ)]
        private static extern IntPtr Api_SetAdmin(string RobotQQ, string GroupNum, string ObjQQ, bool SetWay);
        [DllImport(MQ)]
        private static extern IntPtr Api_WithdrawMsg(string RobotQQ, string GroupNum, string MsgNum, string MsgID);

        [DllImport(MQ)]
        private static extern IntPtr Api_SignIn(string RobotQQ, string GroupNum);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetGroupCard(string RobotQQ, string GroupNum, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_AddQQ(string RobotQQ, string PassWord, bool Auto);

        [DllImport(MQ)]
        private static extern IntPtr Api_LoginQQ(string RobotQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_IfFriend(string RobotQQ, string ObjQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_UpVote(string RobotQQ, string ObjQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_TeaEncrypt(string Text, string SessionKey);//Tea加密
        [DllImport(MQ)]
        private static extern IntPtr Api_TeaDecrypt(string Text, string SessionKey);//Tea解密


        [DllImport(MQ)]
        private static extern IntPtr Api_SetGroupCard(string RobotQQ, string GroupNum, string ObjQQ, string NewCard);
        [DllImport(MQ)]
        private static extern IntPtr Api_KickGroupMBR(string RobotQQ, string GroupNum, string ObjQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_QuitDisGroup(string RobotQQ, string DisGroupID);
        [DllImport(MQ)]
        private static extern IntPtr Api_KickDisGroupMBR(string RobotQQ, string DisGroupID, string ObjQQ);


        [DllImport(MQ)]
        private static extern IntPtr Api_GetQQList();



        [DllImport(MQ)]
        private static extern IntPtr Api_GetOffLineList();

        [DllImport(MQ)]
        private static extern IntPtr Api_PBGroupNotic(string RobotQQ, string GroupNum, string Title, string Content);

        [DllImport(MQ)]
        private static extern IntPtr Api_PBHomeWork(string RobotQQ, string GroupNum, string HomeWorkName, string HomeWorkTitle, string Text);
        [DllImport(MQ)]
        private static extern IntPtr Api_AdminInviteGroup(string RobotQQ, string GroupNum, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_NoAdminInviteGroup(string RobotQQ, string ObjQQ, string GroupNum);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetGender(string RobotQQ, string ObjQQ);
        [DllImport(MQ)]
        private static extern IntPtr Api_GetQQAge(string RobotQQ, string ObjQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_GetPerExp(string RobotQQ, string ObjQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_GetSign(string RobotQQ, string ObjQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_GetEmail(string RobotQQ, string ObjQQ);

        [DllImport(MQ)]
        private static extern IntPtr Api_CreateDisGroup(string RobotQQ, string DisGroupName);
        [DllImport(MQ)]
        private static extern IntPtr Api_InviteDisGroup(string RobotQQ, string DisGroupID, string ObjQQ);


        #endregion
        #region 根据API接口内存指针转成指定返回类型解决c#框架一些未知BUG
        ///<summary>
        ///将好友拉入黑名单,成功返回真,失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲拉黑的QQ</param>
        public static bool ApiAddBkList(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_AddBkList(RobotQQ, ObjQQ);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));
        }
        /// <summary>
        /// 撤回群消息
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="GroupNum">群号</param>
        /// <param name="MsgNum">MsgNum消息序号</param>
        /// <param name="MsgID">消息ID</param>
        /// <returns></returns>
        public static string ApiWithdrawMsg(string RobotQQ, string GroupNum, string MsgNum, string MsgID)
        {

            IntPtr intPtr = Api_WithdrawMsg(RobotQQ, GroupNum, MsgNum, MsgID);

            return Marshal.PtrToStringAnsi(intPtr);

        }

        ///<summary>
        ///向框架帐号列表增加一个登录QQ,成功返回真（CleverQQ可用）
        ///</summary>
        ///<param name="RobotQQ">帐号</param>
        ///<param name="PassWord">密码</param>
        ///<param name="Auto">自动登录</param>
        public static string ApiAddQQ(string RobotQQ, string PassWord, bool Auto)
        {
            IntPtr intPtr = Api_AddQQ(RobotQQ, PassWord, Auto);

            return Marshal.PtrToStringAnsi(intPtr);

        }

        ///<summary>
        ///管理员邀请对象入群,每次只能邀请一个对象,频率过快会失败
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">被邀请对象QQ</param>
        ///<param name="GroupNum">欲邀请加入的群号</param>
        public static void ApiAdminInviteGroup(string RobotQQ, string ObjQQ, string GroupNum)
        {
            Api_AdminInviteGroup(RobotQQ, GroupNum, ObjQQ);

        }


        ///<summary>
        ///创建一个讨论组,成功返回讨论组ID,失败返回空
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupName">讨论组名称</param>
        public static string ApiCreateDisGroup(string RobotQQ, string DisGroupName)
        {
            IntPtr intPtr = Api_CreateDisGroup(RobotQQ, DisGroupName);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// 删除好友,成功返回真,失败返回假
        /// </summary>
        /// <param name="RobotQQ">响应的QQ</param>
        /// <param name="ObjQQ">对象QQ</param>

        public static void ApiDelBkList(string RobotQQ, string ObjQQ)
        {
            Api_DelBkList(RobotQQ, ObjQQ);
        }

        ///<summary>
        ///删除好友,成功返回真,失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲删除对象QQ</param>
        public static bool ApiDelFriend(string RobotQQ, string ObjQQ)
        {

            IntPtr intPtr = Api_DelFriend(RobotQQ, ObjQQ);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));
        }

        ///<summary>
        ///请求禁用插件自身
        ///</summary>
        public static void ApiDisabledPlugin()
        {
            Api_DisabledPlugin();
        }

        ///<summary>
        ///取得机器人网页操作用参数Bkn或G_tk
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetBkn(string RobotQQ)
        {
            IntPtr intPtr = Api_GetBkn(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        ///<summary>
        ///取得机器人网页操作用参数长Bkn或长G_tk
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetBkn32(string RobotQQ)
        {
            IntPtr intPtr = Api_GetBkn32(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        ///<summary>
        ///取得腾讯微博页面操作用参数P_skey
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetBlogPsKey(string RobotQQ)
        {
            IntPtr intPtr = Api_GetBlogPsKey(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);

        }


        ///<summary>
        ///取得腾讯课堂页面操作用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetClassRoomPsKey(string RobotQQ)
        {
            IntPtr intPtr = Api_GetClassRoomPsKey(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        ///<summary>
        ///取得机器人网页操作用的Clientkey
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetClientkey(string RobotQQ)
        {
            IntPtr intPtr = Api_GetClientkey(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }


        ///<summary>
        ///取得机器人网页操作用的Cookies
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetCookies(string RobotQQ)
        {
            IntPtr intPtr = Api_GetCookies(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }



        ///<summary>
        ///取得讨论组列表
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetDisGroupList(string RobotQQ)
        {

            IntPtr intPtr = Api_GetDisGroupList(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);

        }


        ///<summary>
        ///取邮箱,当对象QQ不为10000@qq.com时,可用于获取正确邮箱
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static string ApiGetEmail(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_GetEmail(RobotQQ, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);

        }


        ///<summary>
        ///取得好友列表,返回获取到的原始JSON格式信息,需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetFriendList(string RobotQQ)
        {
            IntPtr intPtr = Api_GetFriendList(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);

        }


        ///<summary>
        ///取对象性别 1男 2女 未知或失败返回-1
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static int ApiGetGender(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_GetGender(RobotQQ, ObjQQ);
            return (int)intPtr;
        }

        ///<summary>
        ///取对象群名片
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">欲取得群名片的QQ号码</param>
        public static string ApiGetGroupCard(string RobotQQ, string GroupNum, string ObjQQ)
        {

            IntPtr intPtr = Api_GetGroupCard(RobotQQ, GroupNum, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }



        ///<summary>
        ///取得群列表,返回获取到的原始JSON格式信息,需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetGroupList(string RobotQQ)
        {

            IntPtr intPtr = Api_GetGroupList(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }



        ///<summary>
        ///取得群列表,返回获取到的原始JSON格式信息,需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetGroupList_A(string RobotQQ)
        {

            IntPtr intPtr = Api_GetGroupList_A(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        ///<summary>
        ///取得群列表,返回获取到的原始JSON格式信息,需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetGroupList_B(string RobotQQ)
        {

            IntPtr intPtr = Api_GetGroupList_B(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }



        ///<summary>
        ///取得QQ群页面操作用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetGroupPsKey(string RobotQQ)
        {
            IntPtr intPtr = Api_GetGroupPsKey(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        ///<summary>
        ///取框架日志
        ///</summary>
        public static string ApiGetLog()
        {
            IntPtr intPtr = Api_GetLog();
            return Marshal.PtrToStringAnsi(intPtr);
        }




        ///<summary>
        ///取对象昵称
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲取得的QQ号码</param>
        public static string ApiGetNick(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_GetNick(RobotQQ, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        ///<summary>
        ///取群公告,返回该群所有公告,JSON格式,需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取得公告的群号</param>
        public static string ApiGetNotice(string RobotQQ, string GroupNum)
        {
            IntPtr intPtr = Api_GetNotice(RobotQQ, GroupNum);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        ///<summary>
        ///获取对象资料,此方式为http,调用时应自行注意控制频率（成功返回JSON格式需自行解析）
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static string ApiGetObjInfo(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_GetObjInfo(RobotQQ, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);

        }







        ///<summary>
        ///取框架离线QQ号（多Q版可用）
        ///</summary>
        public static string ApiGetOffLineList()
        {

            IntPtr intPtr = Api_GetOffLineList();
            return Marshal.PtrToStringAnsi(intPtr);

        }


        ///<summary>
        ///取个人说明
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static string ApiGetPerExp(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_GetPerExp(RobotQQ, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }


        ///<summary>
        ///根据图片GUID取得图片下载链接
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="PicType">图片类型</param>
        ///<param name="ReferenceObj">参考对象</param>
        ///<param name="PicGUID">图片GUID</param>
        public static string ApiGetPicLink(string RobotQQ, int PicType, string ReferenceObj, string PicGUID)
        {

            IntPtr intPtr = Api_GetPicLink(RobotQQ, PicType, ReferenceObj, PicGUID);
            return Marshal.PtrToStringAnsi(intPtr);
        }


        ///<summary>
        ///取Q龄,成功返回Q龄,失败返回-1
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static int ApiGetQQAge(string RobotQQ, string ObjQQ)
        {

            IntPtr intPtr = Api_GetQQAge(RobotQQ, ObjQQ);
            return (int)intPtr;
        }


        ///<summary>
        ///取框架所有QQ号
        ///</summary>
        public static string ApiGetQQList()
        {
            IntPtr intPtr = Api_GetQQList();
            return Marshal.PtrToStringAnsi(intPtr);
        }



        /// <summary>
        /// 取个性签名
        /// </summary>
        /// <param name="RobotQQ">机器人</param>
        /// <param name="ObjQQ">对象QQ</param>
        /// <returns></returns>
        public static string ApiGetSign(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_GetSign(RobotQQ, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        ///<summary>
        ///获取当前框架内部时间戳
        ///</summary>
        public static long ApiGetTimeStamp()
        {
            IntPtr intPtr = Api_GetTimeStamp();
            return (long)intPtr;
        }

        ///<summary>
        ///取框架版本号
        ///</summary>
        public static string ApiGetVer()
        {
            IntPtr intPtr = Api_GetVer();
            return Marshal.PtrToStringAnsi(intPtr);
        }


        ///<summary>
        ///取得QQ空间页面操作有用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiGetZonePsKey(string RobotQQ)
        {

            IntPtr intPtr = Api_GetZonePsKey(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }




        ///<summary>
        ///是否QQ好友,好友返回真,非好友或获取失败返回假
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="OBjQQ">对象QQ</param>
        public static bool ApiIfFriend(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_IfFriend(RobotQQ, ObjQQ);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));
        }

        /// <summary>
        /// 邀请对象加入讨论组,成功返回空,失败返回理由
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="DisGroupID">讨论组ID</param>
        /// <param name="ObjQQ">被邀请对象QQ：多个用 换行符 分割</param>
        /// <returns></returns>

        public static string ApiInviteDisGroup(string RobotQQ, string DisGroupID, string ObjQQ)
        {
            IntPtr intPtr = Api_InviteDisGroup(RobotQQ, DisGroupID, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }



        ///<summary>
        ///取得插件自身启用状态,启用真,禁用假
        ///</summary>
        public static bool ApiIsEnable()
        {
            IntPtr intPtr = Api_IsEnable();

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));

        }


        ///<summary>
        ///查询对象或自己是否被禁言,禁言返回真,失败或未禁言返回假
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static bool ApiIsShutUp(string RobotQQ, string GroupNum, string ObjQQ)
        {

            IntPtr intPtr = Api_IsShutUp(RobotQQ, GroupNum, ObjQQ);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));

        }

        ///<summary>
        ///申请加群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="Reason">附加理由,可留空</param>
        public static void ApiJoinGroup(string RobotQQ, string GroupNUm, string Reason)
        {

            Api_JoinGroup(RobotQQ, GroupNUm, Reason);

        }

        /// <summary>
        /// 将对象移除讨论组,成功返回空,失败返回理由
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="DisGroupID">需要执行的讨论组ID</param>
        /// <param name="ObjQQ">被执行对象</param>
        /// <returns></returns>

        public static string ApiKickDisGroupMBR(string RobotQQ, string DisGroupID, string ObjQQ)
        {
            IntPtr intPtr = Api_KickDisGroupMBR(RobotQQ, DisGroupID, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);
        }


        ///<summary>
        ///将对象移出群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">被执行对象</param>
        public static void ApiKickGroupMBR(string RobotQQ, string GroupNum, string ObjQQ)
        {
            Api_KickGroupMBR(RobotQQ, GroupNum, ObjQQ);

        }



        ///<summary>
        ///登录指定QQ,应确保QQ号码在列表中已经存在
        ///</summary>
        ///<param name="RobotQQ">欲登录的QQ</param>
        public static void ApiLoginQQ(string RobotQQ)
        {
            Api_LoginQQ(RobotQQ);
        }

        ///<summary>
        ///非管理员邀请对象入群,每次只能邀请一个对象,频率过快会失败
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">被邀请人QQ号码</param>
        ///<param name="GroupNum">群号</param>
        public static void ApiNoAdminInviteGroup(string RobotQQ, string ObjQQ, string GroupNum)
        {

            Api_NoAdminInviteGroup(RobotQQ, ObjQQ, GroupNum);
        }





        ///<summary>
        ///向IRQQ日志窗口发送一条本插件的日志,可用于调试输出需要的内容,或定位插件错误与运行状态
        ///</summary>
        ///<param name="Log">日志信息</param>
        public static void ApiOutPutLog(string Log)
        {
            Api_OutPut(Log);
        }

        /// <summary>
        /// 发布群公告（成功返回真,失败返回假）,调用此API应保证响应QQ为管理员
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="GroupNum">欲发布公告的群号</param>
        /// <param name="Title">公告标题</param>
        /// <param name="Content">内容</param>
        /// <returns></returns>

        public static bool ApiPBGroupNotic(string RobotQQ, string GroupNum, string Title, string Content)
        {

            IntPtr intPtr = Api_PBGroupNotic(RobotQQ, GroupNum, Title, Content);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));
        }


        ///<summary>
        ///发布QQ群作业
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="HomeWorkName">作业名</param>
        ///<param name="HomdWorkTitle">作业标题</param>
        ///<param name="Text">作业内容</param>
        public static string ApiPBHomeWork(string RobotQQ, string GroupNum, string HomeWorkName, string HomeWorkTitle, string Text)
        {

            IntPtr intPtr = Api_PBHomeWork(RobotQQ, GroupNum, HomeWorkName, HomeWorkTitle, Text);
            return Marshal.PtrToStringAnsi(intPtr);

        }





        ///<summary>
        ///退出讨论组
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupID">需要退出的讨论组ID</param>
        public static void ApiQuitDisGroup(string RobotQQ, string DisGroupID)
        {
            Api_QuitDisGroup(RobotQQ, DisGroupID);

        }


        ///<summary>
        ///退群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲退出的群号</param>
        public static void ApiQuitGroup(string RobotQQ, string GroupNum)
        {
            Api_QuitGroup(RobotQQ, GroupNum);

        }



        ///<summary>
        ///发送JSON结构消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="SendType">发送方式：1普通 2匿名（匿名需要群开启）</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象所属群_讨论组（消息来源）,发送群、讨论组、临时会话填写、如发送对象为好友可留空</param>
        ///<param name="ObjQQ">收信对象QQ</param>
        ///<param name="Json">Json结构内容</param>
        public static void ApiSendJSON(string RobotQQ, int SendType, int MsgType, string MsgTo, string ObjQQ, string Json)
        {
            Api_SendJSON(RobotQQ, SendType, MsgType, MsgTo, ObjQQ, Json);

        }



        ///<summary>
        ///发送普通文本消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象群_讨论组：发送群、讨论组、临时会话时填写</param>
        ///<param name="ObjQQ">收信QQ</param>
        ///<param name="Msg">内容</param>
        ///<param name="ID">气泡ID</param>
        public static void ApiSendMsg(string RobotQQ, int MsgType, string MsgTo, string ObjQQ, string Msg, int ID)
        {
            //string c = Marshal.PtrToStringAnsi(Api_SendMsg(RobotQQ, MsgType, MsgTo, String.Empty, Msg + $"[气泡{ID}] \\bq144", ID));

            Api_SendMsg(RobotQQ, MsgType, MsgTo, ObjQQ, Msg, ID);
        }





        /// <summary>
        /// 好友语音上传并发送（成功返回真,失败返回假）
        /// </summary>
        /// <param name="RobotQQ">响应的QQ</param>
        /// <param name="ObjQQ">接收QQ</param>
        /// <param name="pAmr">语音数据的指针</param>
        /// <returns></returns>

        public static bool ApiSendVoice(string RobotQQ, string ObjQQ, int pAmr)
        {

            IntPtr intPtr = Api_SendVoice(RobotQQ, ObjQQ, pAmr);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));

        }



        ///<summary>
        ///发送XML消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="SendType">发送方式：1普通 2匿名（匿名需要群开启）</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象群、讨论组：发送群、讨论组、临时时填写,如MsgType为好友可空</param>
        ///<param name="ObjQQ">收信对象QQ</param>
        ///<param name="ObjectMsg">XML代码</param>
        ///<param name="ObjCType">结构子类型：00基本 02点歌</param>
        public static void ApiSendXML(string RobotQQ, int SendType, int MsgFrom, string MsgTo, string ObjQQ, string ObjectMsg, int ObjeCType)
        {

            Api_SendXML(RobotQQ, SendType, MsgFrom, MsgTo, ObjQQ, ObjectMsg, ObjeCType);

        }



        ///<summary>
        ///获取会话SessionKey密匙
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static string ApiSessionKey(string RobotQQ)
        {

            IntPtr intPtr = Api_SessionKey(RobotQQ);
            return Marshal.PtrToStringAnsi(intPtr);

        }


        ///<summary>
        ///设置或取消管理员,成功返回空,失败返回理由
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ</param>
        ///<param name="SetWay">操作方式,真为设置管理,假为取消管理</param>
        public static string ApiSetAdmin(string RobotQQ, string GroupNum, string ObjQQ, bool SetWay)
        {
            IntPtr intPtr = Api_SetAdmin(RobotQQ, GroupNum, ObjQQ, SetWay);
            return Marshal.PtrToStringAnsi(intPtr);

        }


        ///<summary>
        ///开关群匿名消息发送功能,成功返回真,失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="Swit">开关：真开 假关</param>
        public static bool ApiSetAnon(string RobotQQ, string GroupNum, bool swit)
        {

            IntPtr intPtr = Api_SetAnon(RobotQQ, GroupNum, swit);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));

        }


        ///<summary>
        ///修改对象群名片,成功返回真,失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ：被修改名片人QQ</param>
        ///<param name="NewCard">需要修改的群名片</param>
        public static bool ApiSetGroupCard(string RobotQQ, string GroupNum, string ObjQQ, string NewCard)
        {
            IntPtr intPtr = Api_SetGroupCard(RobotQQ, GroupNum, ObjQQ, NewCard);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));

        }





        ///<summary>
        ///屏蔽或接收某群消息
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="type">真为屏蔽接收,假为接收拼不提醒</param>
        public static void ApiSetShieldedGroup(string RobotQQ, string GroupNum, bool type)
        {

            Api_SetShieldedGroup(RobotQQ, GroupNum, type);
        }


        ///<summary>
        ///向好友发起窗口抖动,调用此Api腾讯会限制频率
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">接收抖动消息的QQ</param>
        public static bool ApiShakeWindow(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_ShakeWindow(RobotQQ, ObjQQ);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));

        }

        ///<summary>
        ///禁言群内某人
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲操作的群号</param>
        ///<param name="ObjQQ">欲禁言对象,如留空且机器人QQ为管理员,将设置该群为全群禁言</param>
        ///<param name="Time">禁言时间：0解除（秒）,如为全群禁言,参数为非0,解除全群禁言为0</param>
        public static void ApiShutUP(string RobotQQ, string GroupNum, string ObjQQ, int Time)
        {
            Api_ShutUP(RobotQQ, GroupNum, ObjQQ, Time);
        }


        ///<summary>
        ///QQ群签到,成功返回真失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        public static bool ApiSignIn(string RobotQQ, string GroupNum)
        {
            IntPtr intPtr = Api_SignIn(RobotQQ, GroupNum);

            return Convert.ToBoolean(Marshal.PtrToStringAnsi(intPtr));


        }


        ///<summary>
        ///腾讯Tea加密
        ///</summary>
        ///<param name="Text">需要加密的内容</param>
        ///<param name="SessionKey">会话Key,从Api_SessionKey获得</param>
        public static string ApiTeaEncrypt(string Text, string SessionKey)
        {
            IntPtr intPtr = Api_TeaEncrypt(Text, SessionKey);
            return Marshal.PtrToStringAnsi(intPtr);
        }


        ///<summary>
        ///腾讯Tea解密
        ///</summary>
        ///<param name="Text">需解密的内容</param>
        ///<param name="SessionKey">会话Key,从Api_SessionKey获得</param>
        public static string ApiTeaDecrypt(string Text, string SessionKey)
        {
            IntPtr intPtr = Api_TeaDecrypt(Text, SessionKey);
            return Marshal.PtrToStringAnsi(intPtr);


        }


        /// <summary>
        /// 上传图片（通过读入字节集方式）,可使用网页链接或本地读入,成功返回该图片GUID,失败返回空
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="UpLoadType">上传类型：1好友2群PS:好友临时用1,群讨论组用2；当填写错误时,图片GUID发送不会成功</param>
        /// <param name="UpTo">参考对象,上传该图片所属群号或QQ</param>
        /// <param name="hInstance">图片字节集数据</param>
        /// <returns></returns>
        public static string ApiUpLoadPic(string RobotQQ, int UpLoadType, string UpTo, int hInstance)
        {
            IntPtr intPtr = Api_UpLoadPic(RobotQQ, UpLoadType, UpTo, hInstance);
            return Marshal.PtrToStringAnsi(intPtr);
        }


        ///<summary>
        ///上传QQ语音,成功返回语音GUID,失败返回空
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="type">上传类型 2 QQ群</param>
        ///<param name="GroupNum">接收的群号</param>
        ///<param name="pAmr">语音数据指针</param>
        public static string ApiUpLoadVoice(string RobotQQ, int type, string GroupNum, int pAmr)
        {

            IntPtr intPtr = Api_UpLoadVoice(RobotQQ, type, GroupNum, pAmr);
            return Marshal.PtrToStringAnsi(intPtr);

        }

        /// <summary>
        /// 调用一次点一下,成功返回空,失败返回理由如：每天最多给他点十个赞哦,调用此Api时应注意频率,每人每日10次,至多50人
        /// </summary>
        /// <param name="RobotQQ">机器人QQ</param>
        /// <param name="ObjQQ">被赞人QQ</param>
        /// <returns></returns>

        public static string ApiUpVote(string RobotQQ, string ObjQQ)
        {
            IntPtr intPtr = Api_UpVote(RobotQQ, ObjQQ);
            return Marshal.PtrToStringAnsi(intPtr);

        }
        #endregion

        public static T JsonDeserialize<T>(string JsonStr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            T objs = jss.Deserialize<T>(JsonStr);
            return objs;
        }

        public static string JsonSerialize<T>(T t)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            string objs = jss.Serialize(t);
            return objs;
        }
         
    }
}
