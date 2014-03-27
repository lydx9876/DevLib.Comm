// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����IUcClient.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Generic;

namespace DS.Web.UCenter.Client
{
    ///<summary>
    /// UcApi Client
    /// Dozer ��Ȩ����
    /// �����ơ��޸ģ����뱣���ҵ���ϵ��ʽ��
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    ///</summary>
    public interface IUcClient
    {
        /// <summary>
        /// �û�ע��
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="passWord">����</param>
        /// <param name="email">Email</param>
        /// <param name="questionId">��½����</param>
        /// <param name="answer">��</param>
        /// <returns></returns>
        UcUserRegister UserRegister(string userName, string passWord, string email, int questionId = 0,
                                    string answer = "");

        /// <summary>
        /// �û���½
        /// </summary>
        /// <param name="userName">�û���/Uid/Email</param>
        /// <param name="passWord">����</param>
        /// <param name="loginMethod">��¼��ʽ</param>
        /// <param name="checkques">��Ҫ��½����</param>
        /// <param name="questionId">����ID</param>
        /// <param name="answer">��</param>
        /// <returns></returns>
        UcUserLogin UserLogin(string userName, string passWord, LoginMethod loginMethod = LoginMethod.UserName,
                              bool checkques = false, int questionId = 0, string answer = "");

        /// <summary>
        /// �õ��û���Ϣ
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        UcUserInfo UserInfo(string userName);

        /// <summary>
        /// �õ��û���Ϣ
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <returns></returns>
        UcUserInfo UserInfo(decimal uid);

        /// <summary>
        /// �����û���Ϣ
        /// ������������֤�û���ԭ�����Ƿ���ȷ������ָ�� ignoreoldpw Ϊ 1��
        /// ���ֻ�޸� Email ���޸����룬���� newpw Ϊ�գ�
        /// ͬ�����ֻ�޸����벻�޸� Email������ email Ϊ�ա�
        /// </summary>
        /// <returns></returns>
        UcUserEdit UserEdit(string userName, string oldPw, string newPw, string email, bool ignoreOldPw = false,
                            int questionId = 0, string answer = "");

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <returns></returns>
        bool UserDelete(params int[] uid);

        /// <summary>
        /// ɾ���û�ͷ��
        /// </summary>
        /// <param name="uid">Uid</param>
        void UserDeleteAvatar(params int[] uid);

        /// <summary>
        /// ͬ����½
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <returns>ͬ����½�� Html ����</returns>
        string UserSynlogin(decimal uid);

        /// <summary>
        /// ͬ���ǳ�
        /// </summary>
        /// <returns>ͬ���ǳ��� Html ����</returns>
        string UserSynLogout();

        /// <summary>
        /// ��� Email ��ʽ
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        UcUserCheckEmail UserCheckEmail(string email);

        /// <summary>
        /// �����ܱ����û�
        /// </summary>
        /// <param name="admin">��������Ա</param>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        bool UserAddProtected(string admin, params string[] userName);

        /// <summary>
        /// ɾ���ܱ����û�
        /// </summary>
        /// <param name="admin">��������Ա</param>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        bool UserDeleteProtected(string admin, params string[] userName);

        /// <summary>
        /// �õ��ܱ����û�
        /// </summary>
        /// <returns></returns>
        UcUserProtecteds UserGetProtected();

        /// <summary>
        /// �ϲ��û�
        /// </summary>
        /// <param name="oldUserName">���û���</param>
        /// <param name="newUserName">���û���</param>
        /// <param name="uid">Uid</param>
        /// <param name="passWord">����</param>
        /// <param name="email">Email</param>
        /// <returns></returns>
        UcUserMerge UserMerge(string oldUserName, string newUserName, decimal uid, string passWord, string email);

        /// <summary>
        /// �Ƴ������û���¼
        /// </summary>
        /// <param name="userName">�û���</param>
        void UserMergeRemove(string userName);

        /// <summary>
        /// �õ��û�����
        /// </summary>
        /// <param name="appId">Ӧ�ó���Id</param>
        /// <param name="uid">Uid</param>
        /// <param name="credit">���ֱ��</param>
        /// <returns></returns>
        int UserGetCredit(int appId, decimal uid, int credit);

        /// <summary>
        /// �������Ϣ
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <returns></returns>
        UcPmCheckNew PmCheckNew(decimal uid);

        /// <summary>
        /// ���Ͷ���Ϣ
        /// </summary>
        /// <param name="fromUid">�������û� ID��0 Ϊϵͳ��Ϣ</param>
        /// <param name="replyPmId">�ظ�����Ϣ ID��0:�����µĶ���Ϣ������ 0:�ظ�ָ���Ķ���Ϣ</param>
        /// <param name="subject">��Ϣ����</param>
        /// <param name="message">��Ϣ����</param>
        /// <param name="msgTo">�ռ���ID</param>
        /// <returns></returns>
        UcPmSend PmSend(int fromUid, int replyPmId, string subject, string message, params int[] msgTo);

        /// <summary>
        /// ���Ͷ���Ϣ
        /// </summary>
        /// <param name="fromUid">�������û� ID��0 Ϊϵͳ��Ϣ</param>
        /// <param name="replyPmId">�ظ�����Ϣ ID��0:�����µĶ���Ϣ������ 0:�ظ�ָ���Ķ���Ϣ</param>
        /// <param name="subject">��Ϣ����</param>
        /// <param name="message">��Ϣ����</param>
        /// <param name="msgTo">�ռ����û���</param>
        /// <returns></returns>
        UcPmSend PmSend(int fromUid, int replyPmId, string subject, string message, params string[] msgTo);

        /// <summary>
        /// ɾ������Ϣ
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="folder">�ļ���</param>
        /// <param name="pmIds">����ϢID</param>
        /// <returns>ɾ��������</returns>
        int PmDelete(decimal uid, PmDeleteFolder folder, params int[] pmIds);

        /// <summary>
        /// ɾ���Ự
        /// </summary>
        /// <param name="uid">������</param>
        /// <param name="toUids">�ռ���</param>
        /// <returns>ɾ��������</returns>
        int PmDelete(decimal uid, params int[] toUids);

        /// <summary>
        /// �޸��Ķ�״̬
        /// </summary>
        /// <param name="uid">������</param>
        /// <param name="toUids">�ռ���</param>
        /// <param name="pmIds">����ϢID</param>
        /// <param name="readStatus">�Ķ�״̬</param>
        void PmReadStatus(decimal uid, int toUids, int pmIds = 0, ReadStatus readStatus = ReadStatus.Readed);

        /// <summary>
        /// �޸��Ķ�״̬
        /// </summary>
        /// <param name="uid">������</param>
        /// <param name="toUids">�ռ�������</param>
        /// <param name="pmIds">����ϢID����</param>
        /// <param name="readStatus">�Ķ�״̬</param>
        void PmReadStatus(decimal uid, IEnumerable<int> toUids, IEnumerable<int> pmIds,
                          ReadStatus readStatus = ReadStatus.Readed);

        /// <summary>
        /// ��ȡ����Ϣ�б�
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="page">��ǰҳ��ţ�Ĭ��ֵ 1</param>
        /// <param name="pageSize">ÿҳ�����Ŀ����Ĭ��ֵ 10</param>
        /// <param name="folder">����Ϣ���ڵ��ļ���</param>
        /// <param name="filter">���˷�ʽ</param>
        /// <param name="msgLen">��ȡ����Ϣ�������ֵĳ��ȣ�0 Ϊ����ȡ��Ĭ��ֵ 0</param>
        /// <returns></returns>
        UcPmList PmList(decimal uid, int page = 1, int pageSize = 10, PmReadFolder folder = PmReadFolder.NewBox,
                        PmReadFilter filter = PmReadFilter.NewPm, int msgLen = 0);

        /// <summary>
        /// ��ȡ����Ϣ����
        /// ���ӿں������ڷ���ָ���û���ָ����Ϣ ID ����Ϣ�����ص������а�����������Ϣ�Ļظ���
        /// ���ָ�� touid ��������ô����Ϣ���г����� uid �� touid ֮��Ķ���Ϣ��daterange ����ָ��������Ϣ�����ڷ�Χ��
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="pmId">����ϢID</param>
        /// <param name="toUid">�ռ���ID</param>
        /// <param name="dateRange">���ڷ�Χ</param>
        /// <returns></returns>
        UcPmView PmView(decimal uid, int pmId, int toUid = 0, DateRange dateRange = DateRange.Today);

        /// <summary>
        /// ��ȡ��������Ϣ����
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="type">����</param>
        /// <param name="pmId">����ϢID</param>
        /// <returns></returns>
        UcPm PmViewNode(decimal uid, ViewType type = ViewType.Specified, int pmId = 0);

        /// <summary>
        /// ����δ����Ϣ��ʾ
        /// </summary>
        /// <param name="uid">Uid</param>
        void PmIgnore(decimal uid);

        /// <summary>
        /// �õ�������
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <returns></returns>
        UcPmBlacklsGet PmBlacklsGet(decimal uid);

        /// <summary>
        /// ���ú�����Ϊ��ֹ�����ˣ����ԭ���ݣ�
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <returns></returns>
        bool PmBlacklsSetAll(decimal uid);

        /// <summary>
        /// ���ú����������ԭ���ݣ�
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="userName">�������û���</param>
        /// <returns></returns>
        bool PmBlacklsSet(decimal uid, params string[] userName);

        /// <summary>
        /// ��Ӻ�����Ϊ��ֹ������
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <returns></returns>
        bool PmBlacklsAddAll(decimal uid);

        /// <summary>
        /// ���Ӻ�����
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="userName">�������û���</param>
        /// <returns></returns>
        bool PmBlacklsAdd(decimal uid, params string[] userName);

        /// <summary>
        /// ɾ���������еĽ�ֹ������
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <returns></returns>
        void PmBlacklsDeleteAll(decimal uid);

        /// <summary>
        /// ɾ��������
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="userName">�������û���</param>
        void PmBlacklsDelete(decimal uid, params string[] userName);

        /// <summary>
        /// ���Ӻ���
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="friendId">����ID</param>
        /// <param name="comment">��ע</param>
        /// <returns></returns>
        bool UcFriendAdd(decimal uid, int friendId, string comment = "");

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="friendIds">����ID</param>
        /// <returns></returns>
        bool UcFriendDelete(decimal uid, params int[] friendIds);

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="direction">����</param>
        /// <returns>������Ŀ</returns>
        int UcFriendTotalNum(decimal uid, FriendDirection direction = FriendDirection.All);

        /// <summary>
        /// �����б�
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="page">��ǰҳ���</param>
        /// <param name="pageSize">ÿҳ�����Ŀ��</param>
        /// <param name="totalNum">��������</param>
        /// <param name="direction">����</param>
        /// <returns></returns>
        UcFriends UcFriendList(decimal uid, int page = 1, int pageSize = 10, int totalNum = 10,
                               FriendDirection direction = FriendDirection.All);

        /// <summary>
        /// ���ֶһ�����
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="from">ԭ����</param>
        /// <param name="to">Ŀ�����</param>
        /// <param name="toAppId">Ŀ��Ӧ��ID</param>
        /// <param name="amount">��������</param>
        /// <returns></returns>
        bool UcCreditExchangeRequest(decimal uid, int from, int to, int toAppId, int amount);

        ///<summary>
        /// �޸�ͷ��
        ///</summary>
        ///<param name="uid">Uid</param>
        ///<param name="type"></param>
        ///<returns></returns>
        string Avatar(decimal uid, AvatarType type = AvatarType.Virtual);

        /// <summary>
        /// �õ�ͷ���ַ
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="size">��С</param>
        /// <param name="type">����</param>
        /// <returns></returns>
        string AvatarUrl(decimal uid, AvatarSize size, AvatarType type = AvatarType.Virtual);

        /// <summary>
        /// ���ͷ���Ƿ����
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="size"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool AvatarCheck(decimal uid, AvatarSize size = AvatarSize.Middle, AvatarType type = AvatarType.Virtual);

        /// <summary>
        /// ��ȡ��ǩ����
        /// </summary>
        /// <param name="tagName">��ǩ��</param>
        /// <param name="number">Ӧ�ó���ID��Ӧ������</param>
        /// <returns></returns>
        UcTags TagGet(string tagName, IEnumerable<KeyValuePair<string, string>> number);

        /// <summary>
        /// ����¼�
        /// </summary>
        /// <param name="icon">ͼ�����ͣ��磺thread��post��video��goods��reward��debate��blog��album��comment��wall��friend</param>
        /// <param name="uid">Uid</param>
        /// <param name="userName">�û���</param>
        /// <param name="titleTemplate">����ģ��</param>
        /// <param name="titleData">������������</param>
        /// <param name="bodyTemplate">����ģ��</param>
        /// <param name="bodyData">ģ������</param>
        /// <param name="bodyGeneral">��ͬ�¼��ϲ�ʱ�õ������ݣ��ض������飬ֻ�����name��link������</param>
        /// <param name="targetIds">����</param>
        /// <param name="images">���ͼƬ�� URL �����ӵ�ַ��һ��ͼƬ��ַ��һ�����ӵ�ַ</param>
        /// <returns></returns>
        int FeedAdd(FeedIcon icon, decimal uid, string userName, string titleTemplate, string titleData,
                    string bodyTemplate, string bodyData, string bodyGeneral, string targetIds, params string[] images);

        /// <summary>
        /// �õ�Feed
        /// </summary>
        /// <param name="limit">��������</param>
        /// <returns></returns>
        UcFeeds FeedGet(int limit);

        /// <summary>
        /// �õ�Ӧ���б�
        /// </summary>
        /// <returns></returns>
        UcApps AppList();

        /// <summary>
        /// ����ʼ�������
        /// </summary>
        /// <param name="subject">����</param>
        /// <param name="message">����</param>
        /// <param name="uids">Uid</param>
        /// <returns></returns>
        UcMailQueue MailQueue(string subject, string message, params int[] uids);

        /// <summary>
        /// ����ʼ�������
        /// </summary>
        /// <param name="subject">����</param>
        /// <param name="message">����</param>
        /// <param name="emails">Ŀ��Email</param>
        /// <returns></returns>
        UcMailQueue MailQueue(string subject, string message, params string[] emails);

        /// <summary>
        /// ����ʼ�������
        /// </summary>
        /// <param name="subject">����</param>
        /// <param name="message">����</param>
        /// <param name="uids">Uid</param>
        /// <param name="emails">Ŀ��email</param>
        /// <returns></returns>
        UcMailQueue MailQueue(string subject, string message, int[] uids, string[] emails);

        /// <summary>
        /// ����ʼ�������
        /// </summary>
        /// <param name="subject">����</param>
        /// <param name="message">����</param>
        /// <param name="fromMail">�����ˣ���ѡ������Ĭ��Ϊ�գ�uc��̨���õ��ʼ���Դ��Ϊ�����˵�ַ</param>
        /// <param name="charset">�ʼ��ַ�������ѡ������Ĭ��Ϊgbk</param>
        /// <param name="htmlOn">�Ƿ���html��ʽ���ʼ�����ѡ������Ĭ��ΪFALSE�����ı��ʼ�</param>
        /// <param name="level">�ʼ����𣬿�ѡ������Ĭ��Ϊ1�����ִ�����ȷ��ͣ�ȡֵΪ0��ʱ���������ͣ��ʼ��������</param>
        /// <param name="uids">Uid</param>
        /// <param name="emails">Ŀ��email</param>
        /// <returns></returns>
        UcMailQueue MailQueue(string subject, string message, string fromMail, string charset, bool htmlOn, int level,
                              int[] uids, string[] emails);
    }
}