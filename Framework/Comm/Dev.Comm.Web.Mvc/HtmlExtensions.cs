// ***********************************************************************************
//  Created by zbw911 
//  �����ڣ�2013��04��16�� 18:21
//  
//  �޸��ڣ�2013��05��13�� 18:20
//  �ļ�����FrameworkOnly/Dev.Comm.Web.Mvc/HtmlExtensions.cs
//  
//  ����и��õĽ����������ʼ��� zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Linq;

namespace Dev.Comm.Web
{
    /**
     * then in your _Layout.cshtml:
<body>
...
@Html.RenderScripts()
</body>

and somewhere in some template:
@Html.Script(
    @<script src="@Url.Content("~/Scripts/jquery-1.4.4.min.js")" type="text/javascript"></script>
)

     * 
     * OR
     
     adding JS/CSS:
@Html.Resource(@<script src="@Url.Content("~/Scripts/jquery-1.4.4.min.js")" type="text/javascript"></script>, "js")`
@Html.Resource(@<link rel="stylesheet" href="@Url.Content("~/CSS/style.css")" />, "css")`

@Html.RenderResources("js") and @Html.RenderResources("css") to render.

     * 
     * */

    public static class HtmlExtensions
    {
        private const string scriptkey = "_script_";
        private const string csskey = "_csskey_";

        #region Scripts

        public static MvcHtmlString Script(this HtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            return Resource(htmlHelper, template, scriptkey);
        }

        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            return RenderResources(htmlHelper, scriptkey);
        }

        #endregion

        #region CSS

        public static MvcHtmlString Css(this HtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            return Resource(htmlHelper, template, csskey);
        }

        public static IHtmlString RenderCsses(this HtmlHelper htmlHelper)
        {
            return RenderResources(htmlHelper, csskey);
        }

        #endregion

        #region Base

        public static MvcHtmlString Resource(this HtmlHelper htmlHelper, Func<object, HelperResult> template,
                                             string resourcekey)
        {
            htmlHelper.ViewContext.HttpContext.Items["_" + resourcekey + "_" + Guid.NewGuid()] = template;
            return MvcHtmlString.Empty;
        }


        public static IHtmlString RenderResources(this HtmlHelper htmlHelper, string customkey)
        {

            // ���������޸ģ������޷�ִ��ö�ٲ����� 
            //����������
            var keys = htmlHelper.ViewContext.HttpContext.Items.Keys;

            var keystrs = (from object key in keys select key.ToString()).ToList();

            foreach (string key in keystrs)
            {
                if (key.StartsWith("_" + customkey + "_"))
                {
                    var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        htmlHelper.ViewContext.Writer.Write(template(null));

                        htmlHelper.ViewContext.HttpContext.Items.Remove(key);
                    }
                }
            }
            return MvcHtmlString.Empty;
        }

        #endregion
    }
}