// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����JScript.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************

namespace Dev.Comm.Web.WebFrom
{
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public static class JScript
    {
        public const string START_SCRIPT = "<script language=\"javascript\" type=\"text/javascript\">";
        public const string END_SCRIPT = "</script>";


        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void Show(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg + "');</script>");
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowAlert(Page page, string msg)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "message",
                                                        "<script language='javascript' type='text/javascript'>alert('" +
                                                        msg + "');</script>");
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowAndCloseCeng(Page page, string msg, string rtn)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg +
                                                    "');window.top.hidePopWin(" + rtn + ")</script>");
        }


        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ExecJs(Page page, string js)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>" + js + "</script>");
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���,ˢ�´򿪴��ڣ��رձ�����
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowAndCloseAndRefrush(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg + "');"
                                                    + "opener.location.reload();"
                                                    + "window.close();"
                                                    + "</script>");
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���,ˢ�´򿪴��ڣ��رձ�����
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowAndClose(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg + "');"
                                                    + "window.close();"
                                                    + "</script>");
        }

        /// <summary>
        /// �ؼ���� ��Ϣȷ����ʾ��
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowConfirm(WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
        public static void ShowAndRedirect(Page page, string msg, string url)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("self.location.href='{0}'", url);
            Builder.Append("</script>");
            //page.RegisterStartupScript();
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }


        public static void ShowAndRefresh(Page page, string msg)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("self.location.href=self.location.href;");
            Builder.Append("</script>");

            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }


        public static void ShowAndRedirectTop(Page page, string msg, string url)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }

        public static void RedirectTop(Page page, string url)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            //			Builder.AppendFormat("alert('{0}');",msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }

        /// <summary>
        /// ����Զ���ű���Ϣ
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="script">����ű�</param>
        public static void ResponseScript(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>" + script + "</script>");
        }

        public static void ShowAndCloseSubModal(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg +
                                                    "');window.top.hidePopWin();</script>");
        }

        public static void Redirect(Page page, string url)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("self.location.href='{0}'", url);
            Builder.Append("</script>");
            //page.RegisterStartupScript();
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }

        public static string GetScriptBlock(string AScriptText)
        {
            var sb = new StringBuilder();
            sb.Append(START_SCRIPT);
            sb.Append(AScriptText);
            sb.Append(END_SCRIPT);

            return sb.ToString();
        }

        /*�ƽ��� 2009.06.18*/

        public static void SetHtmlElementValue(string formName, string elementName, string elementValue)
        {
            string js = @"<Script language='JavaScript'>if(document." + formName + "." + elementName +
                        "!=null){document." + formName + "." + elementName + ".value =" + elementValue + ";}</Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void MainRedirect(string url)
        {
            string js = "<script language=javascript>if (parent.opener != null) {parent.opener.top.location.href = '" +
                        url + "';window.close();} else {window.top.location.href='" + url + "';}</script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void MainRedirect(string message, string url)
        {
            string js = "<script language=javascript>alert('" + message +
                        "');if (parent.opener != null) {parent.opener.location.href = '" + url +
                        "';window.close();} else {window.top.location.href='" + url + "';}</script>";
            HttpContext.Current.Response.Write(js);
        }

        #region ��ʾ��ϢShowMessageBox

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="strMsg">��Ϣ����</param>
        public static void ShowMessageBox(string strMsg)
        {
            HttpContext.Current.Response.Write("<script language='javascript'>window.alert('" + strMsg + "');</script>");
        }

        #endregion

        #region ��ʾ��ϢShowMessageBox

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="message">�������͵ı���</param>
        public static void ShowMessageBox(object message)
        {
            string js = @"<Script language='JavaScript'>
                    alert('{0}');  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, message));
        }

        #endregion

        #region ָ��ָ����ĳ��ҳ��

        /// <summary>
        /// ָ��ָ����ĳ��ҳ��
        /// </summary>
        /// <param name="strUrl">ҳ���ַ</param>
        public static void RedirectPage(string strUrl)
        {
            HttpContext.Current.Response.Write("<script language='javascript'>window.parent.location.href('" + strUrl +
                                               "');</script>");
        }

        #endregion

        #region ȷ���Ƿ�ɾ����ʾ ImageButton

        /// <summary>
        /// ȷ���Ƿ�ɾ����ʾ ImageButton
        /// </summary>
        /// <param name="IB">ImageButton�ؼ�</param>
        public static void ShowMessageBox(ImageButton IB, string strMesg)
        {
            IB.Attributes.Add("onclick", "return confirm('" + strMesg + "');");
        }

        #endregion

        #region ȷ���Ƿ�ɾ����ʾ Button

        /// <summary>
        /// ȷ���Ƿ�ɾ����ʾ Button
        /// </summary>
        /// <param name="IB">Button�ؼ�</param>
        public static void ShowMessageBox(Button BN, string strMesg)
        {
            BN.Attributes.Add("onclick", "return confirm('" + strMesg + "');");
        }

        #endregion

        #region ȷ��Ҫ�༭��ʾ LinkButton

        /// <summary>
        /// ȷ���Ƿ�ɾ����ʾ LinkButton
        /// </summary>
        /// <param name="lb">LinkButton�ؼ�</param>
        public static void ShowMessageBox(LinkButton LB, string strMesg)
        {
            LB.Attributes.Add("onclick", "return confirm('" + strMesg + "');");
        }

        #endregion

        #region ������ʾ��Ϣ��ͬʱ����Ч��ҳ��

        /// <summary>
        /// ������ʾ��Ϣ��ͬʱ����Ч��ҳ��
        /// </summary>
        /// <param name="message">��ʾ��Ϣ����</param>
        /// <param name="toURL">ҳ���ַ</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }

        public static void AlertAndRefresh(string message)
        {
            string js = "<script language=javascript>alert('{0}');window.location.href = window.location.href;</script>";
            HttpContext.Current.Response.Write(string.Format(js, message));
        }

        #endregion

        #region �ص���ʷҳ��

        /// <summary>
        /// �ص���ʷҳ��
        /// </summary>
        /// <param name="value">-1/1,1��ʾǰ������1��ʾ����</param>
        public static void GoHistory(int value)
        {
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            HttpContext.Current.Response.End();
        }

        #endregion

        #region �رյ�ǰ����

        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        #endregion

        #region ˢ�¸�����

        /// <summary>
        /// ˢ�¸�����
        /// </summary>
        public static void RefreshParent()
        {
            string js = @"<Script language='JavaScript'>
                    parent.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region ��ʽ��ΪJS�ɽ��͵��ַ���

        /// <summary>
        /// ��ʽ��ΪJS�ɽ��͵��ַ���
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string JSStringFormat(string s)
        {
            return s.Replace("\r", "\\r").Replace("\n", "\\n").Replace("'", "\\'").Replace("\"", "\\\"");
        }

        #endregion

        #region ˢ�´򿪴���

        /// <summary>
        /// ˢ�´򿪴���
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �¿�ҳ��ȥ��ie�Ĳ˵�

        /// <summary>
        /// �¿�ҳ��ȥ��ie�Ĳ˵�
        /// </summary>
        /// <param name="url"></param>
        public static void OpenWebForm(string url)
        {
            /*��������������������������������������������������������������������*/
            /*�޸���Ա:		yxd						*/
            /*�޸�ʱ��:	2005-12-1	*/
            /*�޸�Ŀ��:	�¿�ҳ��ȥ��ie�Ĳ˵�������						*/
            /*ע������:								*/
            /*��ʼ*/
            string js = @"<Script language='JavaScript'>
			//window.open('" + url + @"');
			window.open('" + url +
                        @"','','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');
			</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �򿪴���

        /// <summary>
        /// �򿪴���
        /// </summary>
        /// <param name="url">��ַ</param>
        /// <param name="name"></param>
        /// <param name="future"></param>
        public static void OpenWebForm(string url, string name, string future)
        {
            string js = @"<Script language='JavaScript'>
                     window.open('" + url + @"','" + name + @"','" + future + @"')
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �򿪴���

        public static void OpenOneWebForm(string url)
        {
            /*��������������������������������������������������������������������*/
            /*�޸���Ա:		yxd						*/
            /*�޸�ʱ��:	2003-4-9	*/
            /*�޸�Ŀ��:	�¿�ҳ��ȥ��ie�Ĳ˵�������						*/
            /*ע������:								*/
            /*��ʼ*/
            string js = @"<Script language='JavaScript'>
			window.open('" + url + @"','main');
			</Script>";
            /*����*/
            /*��������������������������������������������������������������������*/

            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �򿪴���

        /// <summary>
        /// �򿪴���
        /// </summary>
        /// <param name="url">��ַ</param>
        /// <param name="formName">��������</param>
        public static void OpenWebForm(string url, string formName)
        {
            /*��������������������������������������������������������������������*/
            /*�޸���Ա:		yxd						*/
            /*�޸�ʱ��:	2003-4-9	*/
            /*�޸�Ŀ��:	�¿�ҳ��ȥ��ie�Ĳ˵�������						*/
            /*ע������:								*/
            /*��ʼ*/
            string js = @"<Script language='JavaScript'>
			//window.open('" + url + @"','" + formName + @"');
			window.open('" + url + @"','" + formName +
                        @"','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no','main');
			</Script>";
            /*����*/
            /*��������������������������������������������������������������������*/

            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region ��WEB����

        /// <summary>		
        /// ������:OpenWebForm	
        /// ��������:��WEB����	
        /// ��������:
        /// �㷨����:
        /// �� ��: ���㶫
        /// �� ��: 2005-12-2 17:00
        /// �� ��:
        /// �� ��:
        /// �� ��:
        /// </summary>
        /// <param name="url">WEB����</param>
        /// <param name="isFullScreen">�Ƿ�ȫ��Ļ</param>
        public static void OpenWebForm(string url, bool isFullScreen, int intHeight, int intWidth, int intTop,
                                       int intLeft)
        {
            string js = @"<Script language='JavaScript'>";
            if (isFullScreen)
            {
                js += "var iWidth = 0;";
                js += "var iHeight = 0;";
                js += "iWidth=window.screen.availWidth-10;";
                js += "iHeight=window.screen.availHeight-50;";
                js +=
                    "var szFeatures ='width=' + iWidth + ',height=' + iHeight + ',top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no';";
                js += "window.open('" + url + @"','',szFeatures);";
            }
            else
            {
                js += "window.open('" + url + @"','','height=" + intHeight + ",width=" + intWidth + ",top=" + intTop +
                      ",left=" + intLeft +
                      ",location=no,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,directories=no');";
            }
            js += "</Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void OpenWebForm(Page page, string url, string win, string width, string height)
        {
            var sb = new StringBuilder();
            sb.Append("var name='");
            sb.Append(win);
            sb.Append("'; var url='");
            sb.Append(url);
            sb.Append("'; var iWidth;");
            sb.Append("iWidth=");
            sb.Append(width);
            sb.Append("; var iHeight;");
            sb.Append("iHeight=");
            sb.Append(height);
            sb.Append("; var iTop = (window.screen.availHeight-30-iHeight)/2;");
            sb.Append("var iLeft = (window.screen.availWidth-10-iWidth)/2;");
            sb.Append(
                "window.open(url,name,'height='+iHeight+',innerHeight='+iHeight+',width='+iWidth+',innerWidth='+iWidth+',top='+iTop+',left='+iLeft+',toolbar=no,menubar=no,scrollbars=auto,resizeable=no,location=no,status=no');");
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebForm_9u4984", GetScriptBlock(sb.ToString()));
        }

        public static void OpenWebForm(Page page, string url, string winName, string width, string height,
                                       string szFeatures)
        {
            var sb = new StringBuilder();
            sb.Append("var name='");
            sb.Append(winName);
            sb.Append("'; var url='");
            sb.Append(url);
            sb.Append("'; var iWidth;");
            sb.Append("iWidth=");
            sb.Append(width);
            sb.Append("; var iHeight;");
            sb.Append("iHeight=");
            sb.Append(height);
            sb.Append("; var iTop = (window.screen.availHeight-30-iHeight)/2;");
            sb.Append("var iLeft = (window.screen.availWidth-10-iWidth)/2;");
            sb.Append(
                "window.open(url,name,'height='+iHeight+',innerHeight='+iHeight+',width='+iWidth+',innerWidth='+iWidth+',top='+iTop+',left='+iLeft+'," +
                szFeatures + "');");
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebForm_9u4984", GetScriptBlock(sb.ToString()));
        }

        #endregion

        #region ת��Url�ƶ���ҳ��

        /// <summary>
        /// ת��Url�ƶ���ҳ��
        /// </summary>
        /// <param name="url">Url</param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region ָ���Ŀ��ҳ��ת��

        /// <summary>
        /// ָ���Ŀ��ҳ��ת��
        /// </summary>
        /// <param name="FrameName">�������</param>
        /// <param name="url">URL</param>
        public static void JavaScriptFrameHref(string FrameName, string url)
        {
            string js = @"<Script language='JavaScript'>
					
                    @obj.location.replace(""{0}"");
                  </Script>";
            js = js.Replace("@obj", FrameName);
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region ���ÿ��ҳ��

        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <param name="strRows"></param>
        public static void JavaScriptResetPage(string strRows)
        {
            string js = @"<Script language='JavaScript'>
                    window.parent.CenterFrame.rows='" + strRows + "';</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �ͻ��˷�������Cookie

        /// <summary>
        /// ������:JavaScriptSetCookie
        /// ��������:�ͻ��˷�������Cookie
        /// ����:yxd
        /// ���ڣ�2003-4-9
        /// �汾��1.0
        /// </summary>
        /// <param name="strName">Cookie��</param>
        /// <param name="strValue">Cookieֵ</param>
        public static void JavaScriptSetCookie(string strName, string strValue)
        {
            string js = @"<script language=Javascript>
			var the_cookie = '" + strName + "=" + strValue + @"'
			var dateexpire = 'Tuesday, 01-Dec-2020 12:00:00 GMT';
			//document.cookie = the_cookie;//д��Cookie<BR>} <BR>
			document.cookie = the_cookie + '; expires='+dateexpire;			
			</script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region ���ظ�����

        /// <summary>		
        /// ������:GotoParentWindow	
        /// ��������:���ظ�����	
        /// ��������:
        /// �㷨����:
        /// �� ��: ���㶫
        /// �� ��: 2003-04-30 10:00
        /// �� ��:
        /// �� ��:
        /// �� ��:
        /// </summary>
        /// <param name="parentWindowUrl">������</param>		
        public static void GotoParentWindow(string parentWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    this.parent.location.replace('" + parentWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �滻������

        /// <summary>		
        /// ������:ReplaceParentWindow	
        /// ��������:�滻������	
        /// ��������:
        /// �㷨����:
        /// �� ��: ���㶫
        /// �� ��: 2003-04-30 10:00
        /// �� ��:
        /// �� ��:
        /// �� ��:
        /// </summary>
        /// <param name="parentWindowUrl">������</param>
        /// <param name="caption">������ʾ</param>
        /// <param name="future">������������</param>
        public static void ReplaceParentWindow(string parentWindowUrl, string caption, string future)
        {
            string js = "";
            if (future != null && future.Trim() != "")
            {
                js = @"<script language=javascript>this.parent.location.replace('" + parentWindowUrl + "','" + caption +
                     "','" + future + "');</script>";
            }
            else
            {
                js =
                    @"<script language=javascript>var iWidth = 0 ;var iHeight = 0 ;iWidth=window.screen.availWidth-10;iHeight=window.screen.availHeight-50;
							var szFeatures = 'dialogWidth:'+iWidth+';dialogHeight:'+iHeight+';dialogLeft:0px;dialogTop:0px;center:yes;help=no;resizable:on;status:on;scroll=yes';this.parent.location.replace('" +
                    parentWindowUrl + "','" + caption + "',szFeatures);</script>";
            }

            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �滻��ǰ����Ĵ򿪴���

        /// <summary>		
        /// ������:ReplaceOpenerWindow	
        /// ��������:�滻��ǰ����Ĵ򿪴���	
        /// ��������:
        /// �㷨����:
        /// �� ��: ����
        /// �� ��: 2003-04-30 16:00
        /// �� ��:
        /// �� ��:
        /// �� ��:
        /// </summary>
        /// <param name="openerWindowUrl">��ǰ����Ĵ򿪴���</param>		
        public static void ReplaceOpenerWindow(string openerWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.location.replace('" + openerWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �滻��ǰ����Ĵ򿪴��ڵĸ�����

        /// <summary>		
        /// ������:ReplaceOpenerParentWindow	
        /// ��������:�滻��ǰ����Ĵ򿪴��ڵĸ�����	
        /// ��������:
        /// �㷨����:
        /// �� ��: ����
        /// �� ��: 2003-07-03 19:00
        /// �� ��:
        /// �� ��:
        /// �� ��:
        /// </summary>
        /// <param name="openerWindowUrl">��ǰ����Ĵ򿪴��ڵĸ�����</param>		
        public static void ReplaceOpenerParentFrame(string frameName, string frameWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.parent." + frameName + ".location.replace('" + frameWindowUrl +
                        "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �滻��ǰ����Ĵ򿪴��ڵĸ�����

        /// <summary>		
        /// ������:ReplaceOpenerParentWindow	
        /// ��������:�滻��ǰ����Ĵ򿪴��ڵĸ�����	
        /// ��������:
        /// �㷨����:
        /// �� ��: ����
        /// �� ��: 2003-07-03 19:00
        /// �� ��:
        /// �� ��:
        /// �� ��:
        /// </summary>
        /// <param name="openerWindowUrl">��ǰ����Ĵ򿪴��ڵĸ�����</param>		
        public static void ReplaceOpenerParentWindow(string openerParentWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.parent.location.replace('" + openerParentWindowUrl +
                        "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �رմ���

        /// <summary>		
        /// ������:CloseParentWindow	
        /// ��������:�رմ���	
        /// ��������:
        /// �㷨����:
        /// �� ��: ���㶫
        /// �� ��: 2003-04-30 16:00
        /// �� ��:
        /// �� ��:
        /// �� ��:
        /// </summary>
        public static void CloseParentWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.parent.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region �رմ���

        /// <summary>
        /// �رմ���
        /// </summary>
        public static void CloseOpenerWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region ���ش�ģʽ���ڵĽű�

        /// <summary>
        /// ������:ShowModalDialogJavascript	
        /// ��������:���ش�ģʽ���ڵĽű�	
        /// ��������:
        /// �㷨����:
        /// �� ��: ����
        /// �� ��: 2003-04-30 15:00
        /// �� ��:
        /// �� ��:
        /// �� ��:
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <returns></returns>
        public static void ShowModalDialogJavascript(string webFormUrl)
        {
            string js = @"<script language=javascript>
							var iWidth = 0 ;
							var iHeight = 0 ;
							iWidth=window.screen.availWidth-10;
							iHeight=window.screen.availHeight-50;
							var szFeatures = 'dialogWidth:'+iWidth+';dialogHeight:'+iHeight+';dialogLeft:0px;dialogTop:0px;center:yes;help=no;resizable:on;status:on;scroll=yes';
							showModalDialog('" + webFormUrl + "','',szFeatures);</script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region ��ֵ����������

        /// <summary>
        /// ��ֵ����������
        /// </summary>
        /// <param name="RedirectUrl"></param>
        /// <param name="ProductName"></param>
        /// <param name="PlayerName"></param>
        /// <param name="Num"></param>
        /// <param name="SaveMsg"></param>
        /// <param name="SaveResult"></param>
        /// <param name="OtherMsg"></param>
        /// <param name="FromUrl"></param>
        /// <param name="ProductId"></param>
        /// <param name="ProductType"></param>
        /// <param name="SpUserId"></param>
        public static void ShowSaveResult(string RedirectUrl, string ProductName, string PlayerName, string Num,
                                          string SaveMsg, string SaveResult, string OtherMsg, string FromUrl,
                                          string ProductId, string ProductType, string SpUserId)
        {
            try
            {
                HttpContext.Current.Response.Redirect(RedirectUrl + "?ProductName=" + ProductName.Replace("\r\n", "") +
                                                      "&PlayerName=" + PlayerName + "&Num=" + Num + "&SaveMsg=" +
                                                      SaveMsg + "&SaveResult=" +
                                                      SaveResult.Replace("��ֵʧ��",
                                                                         "<font color=\"red\"><b>��ֵʧ��</b></font>") +
                                                      "&OtherMsg=" + OtherMsg + "&FromUrl=" + FromUrl + "&ProductId=" +
                                                      ProductId + "&ProductType=" + ProductType + "&SpUserId=" +
                                                      SpUserId);
            }
            catch
            {
                HttpContext.Current.Response.Write("��������<BR>��������г��ִ����뼰ʱ�鿴�������ۼ�¼��");
                HttpContext.Current.Response.End();
                return;
            }
        }

        #endregion
    }
}