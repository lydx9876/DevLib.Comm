//// ***********************************************************************************
//// Created by zbw911 
//// �����ڣ�2012��12��18�� 10:44
//// 
//// �޸��ڣ�2013��02��18�� 18:24
//// �ļ�����ControlSet.cs
//// 
//// ����и��õĽ����������ʼ���zbw911#gmail.com
//// ***********************************************************************************

//namespace Dev.Comm.Web.WebFrom
//{
//    using System;
//    using System.Collections;
//    using System.Data;
//    using System.Web;
//    using System.Web.UI.WebControls;

//    public class ControlSet
//    {
//        #region WEBҳ���ҳ���ƺ���

//        /// <summary>
//        ///  ҳ���ҳ���ƺ���
//        /// </summary>
//        /// <param name="TotalCount">���ܼ�¼��</param>
//        /// <param name="PerPageCount">ÿҳ��Ŀ</param>
//        /// <param name="FileName">���ӵ�ַ</param>
//        /// <param name="CurrentPage">��ǰҳ</param>
//        public static void WebCrossPage(int TotalCount, int PerPageCount, string FileName, int CurrentPage)
//        {
//            int TotalPage; //������ҳ��
//            if ((TotalCount%PerPageCount) == 0)
//                TotalPage = TotalCount/PerPageCount;
//            else
//                TotalPage = TotalCount/PerPageCount + 1;
//            if (TotalPage == 0) //�ܼ�¼Ϊ0ʱΪһҳ
//                TotalPage = 1;

//            HttpContext.Current.Response.Write(
//                "<table width=\"100%\" border=\"0\" height=\"100%\" cellspacing=\"0\" cellpadding=\"0\" valign=\"Top\">");
//            HttpContext.Current.Response.Write("<tr><td align=\"center\">");
//            HttpContext.Current.Response.Write("<font color=\"#000080\">");
//            if (CurrentPage < 2)
//                HttpContext.Current.Response.Write("��ҳ ��һҳ "); //��ǰҳ�ǵ�һҳʱ,��ҳ����һҳû������
//            else
//            {
//                HttpContext.Current.Response.Write("<a href=\"" + FileName +
//                                                   "?Page=1\"><font color=\"#000080\">��ҳ </font></a> "); //��ҳ������
//                HttpContext.Current.Response.Write("<a href=\"" + FileName + "?Page=" + (CurrentPage - 1) +
//                                                   "\"><font color=\"#000080\">��һҳ </font></a>");
//            }
//            if (TotalPage - CurrentPage < 1)
//                HttpContext.Current.Response.Write("��һҳ βҳ ");
//            else
//            {
//                HttpContext.Current.Response.Write("<a href=\"" + FileName + "?Page=" + (CurrentPage + 1) +
//                                                   "\"><font color=\"#000080\">��һҳ </font></a>");
//                HttpContext.Current.Response.Write("<a href=" + FileName + "?Page=" + TotalPage +
//                                                   "><font color=\"#000080\">βҳ </font></a>");
//            }
//            HttpContext.Current.Response.Write("ҳ�Σ�<strong><font color=red>" + CurrentPage + "</font>/" + TotalPage +
//                                               "</strong>ҳ ");
//            HttpContext.Current.Response.Write("��<b>" + TotalCount + "</b>����Ϣ��<b>" + PerPageCount + "</b>��/ҳ��");
//            HttpContext.Current.Response.Write(
//                "ת����<input type='text' name=\"Page\" size=\"4\" maxlength=\"10\" class=\"smallInput\" value=" +
//                CurrentPage + ">");
//            HttpContext.Current.Response.Write(
//                "<input class=\"buttonface\" type=\"submit\"  value=\"Goto\"  name=\"cndok\"></font></td></tr></table>");
//        }

//        #endregion

//        #region ȡĳ���ֶεĲ���������ʾ��DataGrid��

//        /// <summary>
//        /// ȡĳ���ֶεĲ���������ʾ��DataGrid��
//        /// </summary>
//        /// <param name="strCon">����</param>
//        /// <param name="intLength">����</param>
//        /// <returns>STRING</returns>
//        public static string ShowPartConToDg(Object strCon, int intLength)
//        {
//            string strReturn = strCon.ToString();
//            if (strReturn.Length <= intLength)
//            {
//                return strReturn;
//            }
//            else
//            {
//                strReturn = strReturn.Substring(0, intLength) + "...";
//                return strReturn;
//            }
//        }

//        #endregion

//        #region ��ʼ��DropDownList�����ݿ�󶨵�����

//        /// <summary>
//        /// ��ʼ��DropDownList�����ݿ�󶨵�����
//        /// </summary>
//        /// <param name="DdlName">DropDownList����</param>
//        /// <param name="strSql">DataSet</param>
//        /// <param name="strDataField">��ʾֵ</param>
//        /// <param name="strFieldId">��ֵ</param>
//        public static void DataBindDropdl(DropDownList DdlName, DataSet Ds, string TextField, string ValueField)
//        {
//            if (Ds == null)
//                return;
//            DdlName.DataSource = Ds;
//            DdlName.DataTextField = TextField;
//            DdlName.DataValueField = ValueField;
//            DdlName.DataBind();
//        }

//        #endregion

//        #region �����ʼ��DropDownList

//        /// <summary>
//        /// ��ʼ��DropDownList
//        /// </summary>
//        /// <param name="DdlName">DropDownList</param>
//        /// <param name="intValue">����</param>
//        /// <param name="strType">Year,Month,Day,Comm</param>
//        public static void DataBindDdlYear(DropDownList DdlName, int intValue)
//        {
//            string strTemp = "";
//            var ArrliMinute = new ArrayList();
//            for (int i = 0; i < intValue; i++)
//            {
//                strTemp = Convert.ToInt32(DateTime.Now.Year - i).ToString();
//                ArrliMinute.Add(strTemp);
//            }
//            DdlName.DataSource = ArrliMinute;
//            DdlName.DataBind();
//        }

//        #endregion

//        #region ���³�ʼ��DropDownList

//        /// <summary>
//        /// ��ʼ��DropDownList
//        /// </summary>
//        /// <param name="DdlName">DropDownList</param>
//        /// <param name="intValue">����</param>
//        /// <param name="strType">Year,Month,Day,Comm</param>
//        public static void DataBindDdlMonth(DropDownList DdlName)
//        {
//            string strTemp = "";
//            var ArrliMinute = new ArrayList();
//            for (int i = 1; i <= 12; i++)
//            {
//                if (i.ToString().Length == 1)
//                    strTemp = "0" + i.ToString();
//                else
//                    strTemp = i.ToString();
//                ArrliMinute.Add(strTemp);
//            }
//            DdlName.DataSource = ArrliMinute;
//            DdlName.DataBind();
//            int intCurr = Convert.ToInt32(DateTime.Now.Month);
//            DdlName.Items[intCurr - 1].Selected = true;
//        }

//        #endregion

//        #region �����ʼ��DropDownList

//        /// <summary>
//        /// ��ʼ��DropDownList
//        /// </summary>
//        /// <param name="DdlName">DropDownList</param>
//        /// <param name="intValue">����</param>
//        /// <param name="strType">Year,Month,Day,Comm</param>
//        public static void DataBindDdlDaye(DropDownList DdlName)
//        {
//            string strTemp = "";
//            var ArrliMinute = new ArrayList();
//            for (int i = 1; i <= 31; i++)
//            {
//                if (i.ToString().Length == 1)
//                    strTemp = "0" + i.ToString();
//                else
//                    strTemp = i.ToString();
//                ArrliMinute.Add(strTemp);
//            }
//            DdlName.DataSource = ArrliMinute;
//            DdlName.DataBind();
//            int intCurr = Convert.ToInt32(DateTime.Now.Day);
//            DdlName.Items[intCurr - 1].Selected = true;
//        }

//        #endregion

//        #region ��DropDownList�в���һ����¼

//        /// <summary>
//        /// ��DropDownList�в���һ����¼
//        /// </summary>
//        /// <param name="ddlname">DropDownList��</param>
//        /// <param name="strName">���������</param>
//        public static void AddDataToDdl(DropDownList ddlname, string strName)
//        {
//            var li = new ListItem();
//            li.Value = "0";
//            li.Text = strName;
//            ddlname.Items.Insert(0, li);
//            li.Selected = true;
//        }

//        #endregion

//        #region DataGrid���ݰ�

//        /// <summary>
//        /// DataGrid���ݰ�
//        /// </summary>
//        /// <param name="Dg">���󶨵�DataGrid</param>
//        /// <param name="strSql">DataSet</param>
//        public static void DataGridDataBind(DataGrid Dg, DataSet Ds)
//        {
//            Dg.DataSource = Ds;
//            Dg.DataBind();
//        }

//        #endregion

//        #region ��ʽ��DATAGRID�е�ĳ���е���

//        /// <summary>
//        /// ��ʽ��DATAGRID�е�ĳ���е���
//        /// </summary>
//        /// <param name="dgr">DataGrid��</param>
//        public static void UniteDataGrid(DataGrid Dg, int col)
//        {
//            int i;
//            String LastType;
//            int LastCell;
//            if (Dg.Items.Count > 0)
//            {
//                LastType = Dg.Items[0].Cells[col].Text;
//                Dg.Items[0].Cells[col].RowSpan = 1;
//                LastCell = 0;
//                for (i = 1; i < Dg.Items.Count; i++)
//                {
//                    if (Dg.Items[i].Cells[col].Text == LastType)
//                    {
//                        Dg.Items[i].Cells[col].Visible = false;
//                        Dg.Items[LastCell].Cells[col].RowSpan++;
//                    }
//                    else
//                    {
//                        LastType = Dg.Items[i].Cells[col].Text;
//                        LastCell = i;
//                        Dg.Items[i].Cells[col].RowSpan = 1;
//                    }
//                }
//            }
//        }

//        #endregion

//        #region ��ʽ��DATAGRID�е�����	lianyee

//        public static void UniteDataGridNew(DataGrid Dg, int col)
//        {
//            int i;
//            string LastType;
//            string strs = "";
//            string str = "";
//            int LastCell;
//            if (Dg.Items.Count > 0)
//            {
//                LastType = Dg.Items[0].Cells[col].Text;
//                strs = Dg.Items[0].Cells[col + 1].Text;
//                Dg.Items[0].Cells[col].RowSpan = 1;
//                LastCell = 0;
//                for (i = 1; i < Dg.Items.Count; i++)
//                {
//                    str = Dg.Items[i].Cells[col + 1].Text;
//                    if (Dg.Items[i].Cells[col].Text == LastType)
//                    {
//                        strs = strs + " " + str;
//                        Dg.Items[LastCell].Cells[col + 1].Text = strs;
//                        Dg.Items[i].Cells[col].Visible = false;
//                        Dg.Items[i].Visible = false;
//                    }
//                    else
//                    {
//                        LastType = Dg.Items[i].Cells[col].Text;
//                        LastCell = i;
//                        Dg.Items[LastCell].Cells[col + 1].Text = str;
//                        strs = Dg.Items[LastCell].Cells[col + 1].Text;
//                    }
//                }
//            }
//        }

//        #endregion

//        #region ��̬�ı�DataGrid�����ӵ�ַ

//        /// <summary>
//        ///  DataGrid�����ӵ�ַ
//        /// </summary>
//        /// <param name="strAddr">���ӵĵ�ַ</param>
//        /// <param name="pathname">���Ĳ���</param>
//        /// <returns></returns>
//        public static string LinkAddress(string strAddr, Object pathname)
//        {
//            return strAddr + "?ID=" + pathname;
//        }

//        #endregion

//        #region ȡDataGrid��ĳ�еĲ�������

//        /// <summary>
//        /// ȡDataGrid��ĳ�еĲ�������
//        /// </summary>
//        /// <param name="strConn"></param>
//        /// <param name="intNum"></param>
//        /// <returns></returns>
//        public static string GetDgPartStr(string strConn, int intNum)
//        {
//            return strConn = strConn.Substring(0, intNum);
//        }

//        #endregion

//        #region �õ���ǰ����

//        ///// <summary>
//        ///// �õ���ǰ����
//        ///// </summary>
//        ///// <returns>string</returns>
//        //public static string GetCurrDayWeek()
//        //{
//        //    var dt = new string[7] {"������", "����һ", "���ڶ�", "������", "������", "������", "������"};
//        //    string strDate = DateTime.Today.Year.ToString() + "��" + DateTime.Today.Month.ToString() + "��" +
//        //                     DateTime.Today.Day.ToString() + "��   " + dt[(int) DateTime.Today.DayOfWeek];
//        //    return strDate;
//        //}

//        #endregion

//        #region ��ȡ������

//        public static string GetTblName(string startTime, string tblName)
//        {
//            string inidate = startTime.Substring(0, 7).Replace("-", "");
//            string curdate = DateTime.Today.ToString().Substring(0, 7).Replace("-", "");
//            if (Convert.ToInt32(startTime.Substring(0, 4)) >= 2008)
//            {
//                if (inidate != curdate)
//                {
//                    tblName = tblName + inidate;
//                }
//            }
//            return tblName;
//        }

//        #endregion
//    }
//}