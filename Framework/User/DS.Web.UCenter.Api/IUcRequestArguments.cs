// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����IUcRequestArguments.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Specialized;

namespace DS.Web.UCenter.Api
{
    /// <summary>
    /// Requser����
    /// Dozer ��Ȩ����
    /// �����ơ��޸ģ����뱣���ҵ���ϵ��ʽ��
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    public interface IUcRequestArguments
    {
        /// <summary>
        /// Action
        /// </summary>
        string Action { get; }

        /// <summary>
        /// ʱ��
        /// </summary>
        long Time { get; }

        /// <summary>
        /// Query����
        /// </summary>
        NameValueCollection QueryString { get; }

        /// <summary>
        /// Form����
        /// </summary>
        string FormData { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsInvalidRequest { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAuthracationExpiried { get; }
    }
}