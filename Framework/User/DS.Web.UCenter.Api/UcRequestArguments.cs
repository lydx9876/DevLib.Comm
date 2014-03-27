// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����UcRequestArguments.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace DS.Web.UCenter.Api
{
    /// <summary>
    /// Requser����
    /// Dozer ��Ȩ����
    /// �����ơ��޸ģ����뱣���ҵ���ϵ��ʽ��
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    public class UcRequestArguments : IUcRequestArguments
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="request">Request</param>
        public UcRequestArguments(HttpRequest request)
        {
            Code = request.QueryString["code"];
            if (Code == null)
                throw new Exception("�����������");
            FormData = HttpUtility.UrlDecode(request.Form.ToString(), Encoding.GetEncoding(UcConfig.UcCharset));
            QueryString = HttpUtility.ParseQueryString(UcUtility.AuthCodeDecode(Code));
            Action = QueryString["action"];
            long time;
            if (long.TryParse(QueryString["time"], out time)) Time = time;
            IsInvalidRequest = request.QueryString.Count == 0 && UcActions.Contains(Action);
            IsAuthracationExpiried = (UcUtility.PhpTimeNow() - Time) > 0xe10;
        }

        private string Code { get; set; }

        #region IUcRequestArguments Members

        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// ʱ��
        /// </summary>
        public long Time { get; private set; }

        /// <summary>
        /// Query����
        /// </summary>
        public NameValueCollection QueryString { get; private set; }

        /// <summary>
        /// Form����
        /// </summary>
        public string FormData { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInvalidRequest { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthracationExpiried { get; private set; }

        #endregion
    }
}