// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����Mail.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dev.Comm.Net
{
    /// <summary>
    ///ģ���ţ�
    ///����������ͨ��ģ��
    ///�� �� �ˣ�������
    ///�������ڣ�2009-04-18
    /// <summary>
    /// �����ʼ���
    /// </summary>
    public class Mail
    {
        #region ���� ���� �����

        /// <summary>
        /// �ʼ��������ȼ�
        /// </summary>
        public enum Priority
        {
            /// <summary>��߼���</summary>
            HIGH = 1,

            /// <summary>Ĭ�ϼ���</summary>
            NORMAL = 3,

            /// <summary>��ͼ���</summary>
            LOW = 5
        }

        private readonly string _Server = "";

        /// <summary>
        /// SMTP��������������Ϣ
        /// </summary>
        private readonly string strResponse;

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private string strErrMessage;

        /// <summary>
        /// ���캯��
        /// </summary>
        public Mail(string Server)
        {
            strErrMessage = "";

            strResponse = "";

            _Server = Server;
        }

        /// <summary>
        /// ȡ�ô�������Ϣ
        /// </summary>
        public string ErrorMessage
        {
            get { return strErrMessage; }
        }

        /// <summary>
        /// ȡ��SMTP��������������Ϣ
        /// </summary>
        public string ServerResponse
        {
            get { return strResponse; }
        }

        #endregion

        #region base64����

        /// <summary>
        /// ����BASE64����
        /// </summary>
        /// <param name="Data">����</param>
        /// <returns>�ַ���</returns>
        private string Encode(string Data)
        {
            byte[] bteData;

            bteData = Encoding.GetEncoding("gb2312").GetBytes(Data);

            //return Convert.ToBase64String(bteData);

            string w = Convert.ToBase64String(bteData);

            return w;
        }

        /// <summary>
        /// ����BASE64����
        /// </summary>
        /// <param name="Data">����</param>
        /// <returns>�ַ���</returns>
        private string Decode(string Data)
        {
            byte[] bteData;

            bteData = Convert.FromBase64String(Data);

            return Encoding.GetEncoding("gb2312").GetString(bteData);
        }

        #endregion

        #region ���ش�������

        /// <summary>
        /// ���ش�������
        /// </summary>
        /// <param name="str">���������ص���Ϣ</param>
        /// <returns>��������</returns>
        private string FormatString(string str)
        {
            var smtpcMail = new SMTPClient();

            string s = str.Substring(0, 3);

            str = str.Substring(0, str.IndexOf("\0"));

            switch (s)
            {
                case "500":

                    return "�����ַ����" + "��������ԭʼ������Ϣ��" + str;

                case "501":

                    return "������ʽ����" + "��������ԭʼ������Ϣ��" + str;

                case "502":

                    return "�����ʵ��" + "��������ԭʼ������Ϣ��" + str;

                case "503":

                    return "��������ҪSMTP��֤" + "��������ԭʼ������Ϣ��" + str;

                case "504":

                    return "�����������ʵ��" + "��������ԭʼ������Ϣ��" + str;

                case "421":

                    return "����δ�������رմ����ŵ�" + "��������ԭʼ������Ϣ��" + str;

                case "450":

                    return "Ҫ����ʼ�����δ��ɣ����䲻���ã����磬����æ��" + "��������ԭʼ������Ϣ��" + str;

                case "550":

                    return "Ҫ����ʼ�����δ��ɣ����䲻���ã����磬����δ�ҵ����򲻿ɷ��ʣ�" + "��������ԭʼ������Ϣ��" + str;

                case "451":

                    return "����Ҫ��Ĳ�������������г���" + "��������ԭʼ������Ϣ��" + str;

                case "551":

                    return "�û��Ǳ��أ��볢��<forward-path>" + "��������ԭʼ������Ϣ��" + str;

                case "452":

                    return "ϵͳ�洢���㣬Ҫ��Ĳ���δִ��" + "��������ԭʼ������Ϣ��" + str;

                case "552":

                    return "�����Ĵ洢���䣬Ҫ��Ĳ���δִ��" + "��������ԭʼ������Ϣ��" + str;

                case "553":

                    return "�����������ã�Ҫ��Ĳ���δִ�У����������ʽ����" + "��������ԭʼ������Ϣ��" + str;

                case "432":

                    return "��Ҫһ������ת��" + "��������ԭʼ������Ϣ��" + str;

                case "534":

                    return "��֤���ƹ��ڼ�" + "��������ԭʼ������Ϣ��" + str;

                case "538":

                    return "��ǰ�������֤������Ҫ����" + "��������ԭʼ������Ϣ��" + str;

                case "454":

                    return "��ʱ��֤ʧ��" + "��������ԭʼ������Ϣ��" + str;

                case "530":

                    return "��Ҫ��֤" + "��������ԭʼ������Ϣ��" + str;

                default:

                    return "û��ƥ��Ĵ��������������ز�����" + str;
            }
        }

        #endregion

        #region �����ʼ� ��Ҫ

        /// <summary>
        /// ���ʼ���web.config�ж�ȡ���ýڵ㣬����Ժ���÷��ʼ�����
        /// </summary>
        /// <param name="configMessage"></param>
        /// <param name="mail"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="emailMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public int ToSendMail(string configMessage, string mail, string name, string subject, string emailMessage,
                              out string errorMessage)
        {
            return ToSendMail("", configMessage, mail, name, subject, emailMessage, out errorMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendUsername"></param>
        /// <param name="configMessage"></param>
        /// <param name="mail"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="emailMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public int ToSendMail(string sendUsername, string configMessage, string mail, string name, string subject, string emailMessage,
                                     out string errorMessage)
        {

            string cMessage = "";
            //��ȡ������Ϣ
            string[] strMail = configMessage.Split(";".ToCharArray());

            int result_mail = SendMail(strMail[0], Convert.ToInt32(strMail[1]), strMail[2], sendUsername, true,
                                       strMail[2], strMail[3], mail, name, Priority.HIGH, true, "", subject,
                                       emailMessage, out cMessage);

            //�����ʼ����ؽ������0�Ķ��Ǵ���
            errorMessage = cMessage;
            return result_mail;
        }


        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="smtpHost">SMTP������</param>
        /// <param name="port">SMTP�������˿�</param>
        /// <param name="from">�ʼ�������</param>
        /// <param name="displayFromName">��ʾ�ķ���������</param>
        /// <param name="authentication">�Ƿ������֤</param>
        /// <param name="userName">��֤�û���</param>
        /// <param name="password">��֤����</param>
        /// <param name="To">�ʼ�������</param>
        /// <param name="displayToName">��ʾ�Ľ���������</param>
        /// <param name="priority">���ȼ�</param>
        /// <param name="html">�Ƿ�ΪHTML</param>
        /// <param name="Base">URL</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="message">�ʼ�����</param>
        /// <param name="errmsg">������Ϣ </param>
        public int SendMail(string smtpHost, int port, string @from, string displayFromName, bool authentication,
                            string userName, string password, string To, string displayToName, Priority priority,
                            bool html, string Base, string subject, string message, out string errmsg)
        {
            try
            {
                @from = "<" + @from + ">";
                To = "<" + To + ">";
                ////if (To.IndexOf(";") > -1)
                ////    To = "<" + To.Replace(";", ">,<") + ">";
                ////else
                ////    To = "<" + To + ">";


                var Name = new[]
                               {
                                   "��������ַ", "�˿�", "��������", "����������", "�Ƿ���֤(Smtp��¼)", "��֤�û���", "��֤����", "�ʼ�������", "����������",
                                   "���ȼ�"
                                   , "�Ƿ�HTML", "������ӵ�ַ(����ͼƬ������ʱ)", "����", "����"
                               };

                var NameParameter = new[]
                                        {
                                            smtpHost, port.ToString(), @from, displayFromName, authentication.ToString(),
                                            userName, password, To, displayToName, priority.ToString(), html.ToString(),
                                            Base, subject, message
                                        };

                //����Ϊ�����ʼ�ʧ��ʱ��¼����վ��Ŀ¼���ı��ļ���

                string strResponseNumber; //smtp���������ص���Ϣ

                var smtpcMail = new SMTPClient(); //ʵ����������

                smtpcMail.Connect(smtpHost, port); //����SmtpHost������,�˿�Port

                bool boolConnect = smtpcMail.isConnected(); //���������Ƿ�ɹ�

                //�ж��Ƿ����������

                if (!boolConnect)
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name,  string.Join(",", NameParameter), "δ��ȡ������", "");


                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>δ��ȡ������");

                    errmsg = "smtpcMail.Connect error:δ��ȡ������";
                    return -1;
                }

                strResponseNumber = smtpcMail.GetServerResponse(); //�õ����������ص���Ϣ

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "220"))) //����220��ʾ�ɹ�����
                {
                    smtpcMail.Close();

                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>��������ʧ��");
                   
                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "��������ʧ��",
                    //                 FormatString(strResponseNumber).Trim());
                    errmsg = "smtpcMail.GetServerResponse error:" + FormatString(strResponseNumber).Trim();
                    return -1;
                }

                smtpcMail.SendCommandToServer("HELO kaidongmei\r\n");
                Thread.Sleep(50);

                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "250")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "����������ӳ��ڣ����к�����",
                    //                 FormatString(strResponseNumber).Trim());

                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>����������ӳ��ڣ����к�����");
                  
                    errmsg = FormatString(strResponseNumber).Trim();
                    return 1;
                }

                //��һ������

                if (authentication) //�Ƿ���֤
                {
                    //�������ͨѶ�ڶ����������¼

                    smtpcMail.SendCommandToServer("AUTH LOGIN\r\n");
                    Thread.Sleep(50);
                    strResponseNumber = smtpcMail.GetServerResponse();

                    if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "334")))
                    {
                        smtpcMail.Close();

                        //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "�����¼���ִ���",
                        //                 FormatString(strResponseNumber).Trim());

                        Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>�����¼���ִ���");
                  
                        errmsg = FormatString(strResponseNumber).Trim();
                        return 2;
                    }

                    //�������ͨѶ�������������û��ʺ�

                    smtpcMail.SendCommandToServer(Encode(userName) + "\r\n");
                    Thread.Sleep(50);
                    strResponseNumber = smtpcMail.GetServerResponse();

                    if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "334")))
                    {
                        smtpcMail.Close();

                        //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "�����û����Ƴ���",
                        //                 FormatString(strResponseNumber).Trim());


                        Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>�����û����Ƴ���");

                        errmsg = FormatString(strResponseNumber).Trim();
                        return 3;
                    }

                    //����������

                    //�������ͨѶ���Ĳ��������û�����

                    smtpcMail.SendCommandToServer(Encode(password) + "\r\n");
                    Thread.Sleep(50);
                    strResponseNumber = smtpcMail.GetServerResponse();

                    if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "235")))
                    {
                        smtpcMail.Close();

                        //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "�����û��������",
                        //                 FormatString(strResponseNumber).Trim());

                        Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>�����û��������");

                        errmsg = FormatString(strResponseNumber).Trim();
                        return 4;
                    }

                    //���Ĳ�����
                }

                //�������ͨѶ���岽�����뷢���ʼ��������ַ

                smtpcMail.SendCommandToServer("MAIL FROM: " + @from + /* +  " AUTH = " + From*/ "\r\n");
                Thread.Sleep(50);
                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "250")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "���뷢���������",
                    //                 FormatString(strResponseNumber).Trim());
                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>���뷢���������");


                    errmsg = FormatString(strResponseNumber).Trim();
                    return 5;
                }

                //���岽����

                //�������ͨѶ����������������ʼ��������ַ

                smtpcMail.SendCommandToServer("RCPT TO: " + To + "\r\n");
                Thread.Sleep(50);
                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "250")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "��������������",
                    //                 FormatString(strResponseNumber).Trim());
                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>��������������");

                    errmsg = FormatString(strResponseNumber).Trim();
                    return 6;
                }

                //����������

                //�������ͨѶ���߲�������������������				

                smtpcMail.SendCommandToServer("DATA\r\n");
                Thread.Sleep(50);
                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "354")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "�������ʼ������ݳ���",
                    //                 FormatString(strResponseNumber).Trim());


                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>�������ʼ������ݳ���");
                    errmsg = "smtpcMail.SendCommandToServer error:" + FormatString(strResponseNumber).Trim();
                    return 7;
                }

                /*�ʼ���Ҫ���ݿ�ʼ			               ��زο�	 
				
                FROM:<����><�ʼ���ַ>                      ��ʽ��FROM:����Ա 
				  
                TO:  <����><�ʼ���ַ>                      ��ʽ��TO:Name<1234@sina.com>  
				
                SUBJECT:<����>                             ��ʽ��SUBJECT:����������ܲ��� 
				
                DATE:<ʱ��>                                ��ʽ��DATE: Thu, 29 Aug 2002 09:52:47 +0800 (CST)
				
                REPLY-TO:<���ʼ���ַ>                      ��ʽ��REPLY-TO:webmaster@sina.com 
				
                Content-Type:<�ʼ�����>                    ��ʽ��Content-Type: multipart/mixed; boundary=unique-boundary-1 
				
                X-Priority:<�ʼ����ȼ�>                    ��ʽ��X-Priority:3 
				
                MIME-Version��<�汾>                       ��ʽ��MIME-Version:1.0 
				
                Content-Transfer-Encoding:<���ݴ������>   ��ʽ��Content-Transfer-Encoding:Base64 
				
                X-Mailer:<�ʼ�������>                      ��ʽ��X-Mailer:FoxMail 4.0 beta 1 [cn]  */

                string strData = "";

                strData = string.Concat("From: ", displayFromName + @from);

                strData = string.Concat(strData, "\r\n");

                strData = string.Concat(strData, "To: ");

                strData = string.Concat(strData, displayToName + To);

                strData = string.Concat(strData, "\r\n");

                strData = string.Concat(strData, "Subject: ");

                strData = string.Concat(strData, subject);

                strData = string.Concat(strData, "\r\n");

                if (html)
                {
                    strData = string.Concat(strData, "Content-Type: text/html;charset=\"gb2312\"");
                }
                else
                {
                    strData = string.Concat(strData, "Content-Type: text/plain;charset=\"gb2312\"");
                }

                strData = string.Concat(strData, "\r\n");

                //strData = string.Concat(strData,"Content-Transfer-Encoding: base64;");

                //strData = string.Concat(strData,"\r\n");

                strData = string.Concat(strData, "X-Priority: " + priority);

                strData = string.Concat(strData, "\r\n");

                strData = string.Concat(strData, "MIME-Version: 1.0");

                strData = string.Concat(strData, "\r\n");

                //strData = string.Concat(strData,"Content-Type: text/html;" );			

                //strData = string.Concat(strData,"\r\n");							

                strData = string.Concat(strData, "X-Mailer: Email�Զ����ͳ��� 1.0 ");

                strData = string.Concat(strData, "\r\n" + "\r\n");

                strData = string.Concat(strData, message);

                //ִ��.�����������

                strData = string.Concat(strData, "\r\n.\r\n");

                smtpcMail.SendCommandToServer(strData);
                Thread.Sleep(50);
                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "250")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "���-��������ʼ����ݳ���",
                    //                 FormatString(strResponseNumber).Trim());

                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>���-��������ʼ����ݳ���");
                    errmsg = "smtpcMail.SendCommandToServer(strData) error:" + FormatString(strResponseNumber).Trim();
                    return 7;
                }

                //���߲����

                //���ִ��.QUIT����Ͽ�����

                smtpcMail.SendCommandToServer("QUIT\r\n");
                Thread.Sleep(50);
                smtpcMail.Close();
            }
            catch (SocketException err)
            {
                strErrMessage += err.Message + " " + err.StackTrace;
                errmsg = "SocketException err:" + strErrMessage;
                return -1;
            }
            catch (Exception e)
            {
                strErrMessage += e.Message + " " + e.StackTrace;
                errmsg = "Exception e:" + strErrMessage;
                return -1;
            }
            errmsg = "�ʼ����ͳɹ�";
            return 0;
        }

        /// <summary>
        /// Ⱥ���ʼ�
        /// </summary>
        /// <param name="SmtpHost"></param>
        /// <param name="Port"></param>
        /// <param name="From"></param>
        /// <param name="DisplayFromName"></param>
        /// <param name="Authentication"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="To">���ʱ��;�ָ�</param>
        /// <param name="DisplayToName"></param>
        /// <param name="Priority"></param>
        /// <param name="Html"></param>
        /// <param name="Base"></param>
        /// <param name="Subject"></param>
        /// <param name="Message"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public int SendMoreMail(string SmtpHost, int Port, string From, string DisplayFromName, bool Authentication,
                                string UserName, string Password, string To, string DisplayToName, Priority Priority,
                                bool Html, string Base, string Subject, string Message)
        {
            var from = new MailAddress(From, DisplayToName); //�ʼ��ķ�����,����Ϊ��ʾ������
            var mail = new MailMessage();
            mail.Subject = Subject; //�����ʼ��ı���
            mail.From = from; //�����ʼ��ķ�����

            //���˷���
            string address = "";
            string displayName = "";
            string[] mailNames = (To + ";").Split(';');
            foreach (var name in mailNames)
            {
                if (name != string.Empty)
                {
                    if (name.IndexOf('<') > 0)
                    {
                        displayName = name.Substring(0, name.IndexOf('<'));
                        address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                    }
                    else
                    {
                        displayName = string.Empty;
                        address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                    }
                    mail.CC.Add(new MailAddress(address, displayName)); //�ռ��˵�ַ�ļ���
                }
            }

            mail.Body = Message; //�����ʼ�������            
            mail.BodyEncoding = Encoding.UTF8; //�����ʼ��ĸ�ʽ
            mail.IsBodyHtml = true; //���������Ƿ�ΪHTML��ʽ
            mail.Priority = MailPriority.Normal; //�����ʼ��ķ��ͼ���
            ////if (txtMailTo.Text != "")
            ////{
            ////    string fileName = txtUpFile.Text.Trim();                                   //�����ʼ��ĸ���
            ////    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);             //ȡ�ļ���
            ////    mail.Attachments.Add(new Attachment(fileName));                            //��Ӹ������ʼ�����
            ////}
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            var client = new SmtpClient();
            client.Host = SmtpHost; //����SMTP�ĵ�ַ��ע�⣺��ʲô�����Ӧ�������Ӧ�ĵ�ַ          
            client.Port = Port; //�������� SMTP ����Ķ˿ڣ�Ĭ�ϵ��� 25
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(UserName, Password); //�ҵ�����ĵ�¼�������롣���Ƿ��ͷ����û��������룬Ҫ��Ӧ�����Host��ַ
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(mail);
            return 1;
        }

        #endregion

        #region ���ݱ����ļ�·���������

        /// <summary>
        /// ���� �����ļ� �������ģ��
        /// </summary>
        /// <param name="path">�����ļ�ģ��·��</param>
        /// <param name="replaceContent">�ļ��еĲ������Ͳ���ֵ�Լ�ֵ�Ե���ʽ��Ӧ</param>
        /// <returns></returns>
        public string GetHtmlBody(string path, Dictionary<string, string> replaceContent)
        {
            FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var f = new byte[fs.Length];
            fs.Read(f, 0, (int)fs.Length);
            fs.Close();
            string result = Encoding.GetEncoding("GB2312").GetString(f);

            if (replaceContent != null)
            {
                foreach (var str in replaceContent.Keys)
                {
                    result = result.Replace(str, replaceContent[str]);
                }
            }

            return result;
        }

        #endregion

        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format. 
            return Regex.IsMatch(strIn,
                                 @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }

    /// <summary>
    /// TcpClient�����࣬��������SMTP�����������ӹ���
    /// </summary>
    public class SMTPClient : TcpClient
    {
        #region ����������� ���캯�� ��ǰ״̬ ��������ķ��� ��ȡ���������� �����ַ���

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        /// <returns>����ΪTrue��������ΪFalse</returns>
        public bool isConnected()
        {
            return Active;
        }

        /// <summary>
        /// ���������������
        /// </summary>
        /// <param name="Command">����</param>
        public void SendCommandToServer(string Command)
        {
            NetworkStream ns = GetStream();

            byte[] WriteBuffer;

            WriteBuffer = new byte[1024];

            WriteBuffer = Encoding.Default.GetBytes(Command);

            ns.Write(WriteBuffer, 0, WriteBuffer.Length);

            return;
        }

        /// <summary>
        /// ȡ�÷�����������Ϣ
        /// </summary>
        /// <returns>�ַ���</returns>
        public string GetServerResponse()
        {
            int StreamSize;

            string ReturnValue = "";

            byte[] ReadBuffer;

            NetworkStream ns = GetStream();

            ReadBuffer = new byte[1024];

            StreamSize = ns.Read(ReadBuffer, 0, ReadBuffer.Length);

            if (StreamSize == 0)
            {
                return ReturnValue;
            }
            else
            {
                ReturnValue = Encoding.Default.GetString(ReadBuffer);

                return ReturnValue;
            }
        }

        /// <summary>
        /// �жϷ��ص���Ϣ���Ƿ���ָ����SMTP�������
        /// </summary>
        /// <param name="Message">��Ϣ</param>
        /// <param name="SMTPCode">SMTP����</param>
        /// <returns>���ڷ���False�������ڷ���True</returns>
        public bool DoesStringContainSMTPCode(string Message, string SMTPCode)
        {
            return (Message.IndexOf(SMTPCode, 0, 10) == -1) ? false : true;
        }

        #endregion
    }

    ///// <summary>
    ///// ��¼��־���ı�
    ///// </summary>
    //public class TextLog
    //{
    //    #region �ʼ�����ʧ�ܵ�ʱ�� ��¼��־���ı�

    //    /// <summary>
    //    ///  ��¼��־��Text�ļ�
    //    /// </summary>
    //    /// <param name="Server">  ������·��</param>
    //    /// <param name="TextName">Text����_����·����</param>
    //    /// <param name="Name">    ��������</param>
    //    /// <param name="NameParameter">����ֵ</param>
    //    public static void ErrorLog(string Server, string TextName, string[] Name, string[] NameParameter, string Error,
    //                                string ErrorBody)
    //    {
    //        try
    //        {
    //            StreamWriter sw = null;

    //            if (!(File.Exists(Server + @"\" + TextName)))
    //            {
    //                sw = File.CreateText(Server + @"\" + TextName);
    //            }
    //            else
    //            {
    //                sw = File.AppendText(Server + @"\" + TextName);
    //            }

    //            sw.WriteLine("\n");

    //            sw.WriteLine("������ʱ�䣺" + DateTime.Now.ToString() + "\n");

    //            sw.WriteLine("���󱾵�������" + Error + "   ���������ͷ�����������Ϣ��" + ErrorBody + "\n");

    //            for (int i = 0; i < Name.Length; i++)
    //            {
    //                sw.WriteLine("�������ƣ�" + Name[i] + "  ֵ��" + NameParameter[i]);

    //                sw.WriteLine("\n");
    //            }

    //            sw.Flush();

    //            sw.Close();
    //        }
    //        catch
    //        {
    //            //���Դ���
    //        }
    //    }

    //    /// <summary>
    //    ///  ��¼��־��Text�ļ�
    //    /// </summary>
    //    /// <param name="Server">  ������·��</param>
    //    /// <param name="TextName">Text����_����·����</param>
    //    /// <param name="Name">    ��������</param>
    //    /// <param name="NameParameter">����ֵ</param>
    //    public static void ErrorLog(string Server, string TextName, string Name, string NameParameter, string Error,
    //                                string ErrorBody)
    //    {
    //        string[] s, s1;

    //        s = new[] { Name };

    //        s1 = new[] { NameParameter };

    //        ErrorLog(Server, TextName, s, s1, Error, ErrorBody);
    //    }

    //    #endregion
    //}
}