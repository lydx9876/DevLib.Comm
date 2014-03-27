// ***********************************************************************************
//  Created by zbw911 
//  �����ڣ�2013��04��16�� 18:19
//  
//  �޸��ڣ�2013��05��13�� 18:20
//  �ļ�����FrameworkOnly/Dev.Comm.Web.Mvc/UrlHelperExtension.cs
//  
//  ����и��õĽ����������ʼ��� zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Web;
using System.Web.Mvc;

namespace Dev.Comm.Web
{
    /// <summary>
    /// </summary>
    public static class UrlHelperExtension
    {
        public static string Absolute(this UrlHelper url, string relativeOrAbsolute)
        {
            var uri = new Uri(relativeOrAbsolute, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
            {
                return relativeOrAbsolute;
            }


            // At this point, we know the url is relative.
            return VirtualPathUtility.ToAbsolute(relativeOrAbsolute);
        }
    }
}