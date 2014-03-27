// ***********************************************************************************
//  Created by zbw911 
//  �����ڣ�2013��05��13�� 18:15
//  
//  �޸��ڣ�2013��05��13�� 18:20
//  �ļ�����FrameworkOnly/Dev.Comm.Web.Mvc/WatchAttribute.cs
//  
//  ����и��õĽ����������ʼ��� zbw911#gmail.com
// ***********************************************************************************

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Dev.Comm.Web.Mvc.Filter
{

    #region TraceRunAttribute
    //ToDo:��ʵ�������ʹ�� StopWatch Ҫ��DateTime�ã�But...

    /// <summary>
    /// ���ڴ洢������Ϣ������㣬
    /// ���� child Ӧ���� CollectBase���Щ
    /// </summary>
    internal class WatchNode
    {
        private List<WatchNode> _child = new List<WatchNode>();
        public string Name { get; set; }
        public long OnActionExecuting { get; set; }
        public long OnActionExecuted { get; set; }
        public long OnResultExecuted { get; set; }
        public long OnResultExecuting { get; set; }

        public WatchNode Parent { get; set; }
        public List<WatchNode> Child
        {
            get { return this._child; }
            set { this._child = value; }
        }


        public long All
        {
            get { return (OnResultExecuted - OnActionExecuting) / 10000; }
        }

        public long Action
        {
            get { return (this.OnActionExecuted - this.OnActionExecuting) / 10000; }
        }

        public long Result
        {
            get { return (this.OnResultExecuted - this.OnResultExecuting) / 10000; }
        }

        public bool IsChild { get; set; }

        public string ParentName
        {
            get
            {
                if (Parent == null) return null;
                return Parent.Name;
            }
        }
    }

    /// <summary>
    /// ����ʱ����
    /// </summary>
    public class NameTime
    {
        public string Name { get; set; }
        public long Time { get; set; }
        public string Do { get; set; }

        public ViewContext Parent
        {
            get;
            set;
        }

        public string ParentName
        {
            get
            {
                if (Parent == null)
                    return null;

                return Parent.RouteData.Values["action"].ToString();
            }
        }
    }
    /// <summary>
    /// ����MVC Action Result ��������ʱ��
    /// </summary>
    public class TraceRunAttribute : ActionFilterAttribute
    {
        private string _newLine = "<br/>\r\n";
        private const string __List__ = "______List__________";
        internal const string ViewResultTypeKey = "______________________ViewType________";
        public TraceRunAttribute()
        {
        }

        public bool IsShow
        {
            get;
            set;
        }

        /// <summary>
        /// ����
        /// </summary>

        public string NewLine
        {
            get { return this._newLine; }
            set { this._newLine = value; }
        }


        /// <summary>
        /// ����ģ��
        /// </summary>
        public string TitleTemple { get; set; }
        /// <summary>
        /// ��ģ��
        /// </summary>
        public string ItemTemple { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string name = filterContext.ActionDescriptor.ActionName;
            var context = filterContext.HttpContext;

            var parent = filterContext.ParentActionViewContext;

            this.CheckItem(context, name, "OnActionExecuted", parent);

            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            string name = filterContext.ActionDescriptor.ActionName;
            var parent = filterContext.ParentActionViewContext;
            var context = filterContext.HttpContext;

            this.CheckItem(context, name, "OnActionExecuting", parent);


            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var parent = filterContext.ParentActionViewContext;

            string name = filterContext.RouteData.Values["action"].ToString();
            var context = filterContext.HttpContext;

            this.CheckItem(context, name, "OnResultExecuted", parent);

            if (!filterContext.IsChildAction && filterContext.Result is ViewResult)
            {
                //���Key�����ڸ��� ResponeRequestָʾ��
                context.Items.Add(ViewResultTypeKey, "ViewResult");
                WriteReport(context);
            }


            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            string name = filterContext.RouteData.Values["action"].ToString();
            var context = filterContext.HttpContext;
            var parent = filterContext.ParentActionViewContext;
            this.CheckItem(context, name, "OnResultExecuting", parent);

            base.OnResultExecuting(filterContext);
        }

        private void WriteReport(HttpContextBase context)
        {

            if (!IsShow)
                context.Response.Write("<!--");
            context.Response.Write("<b>������Ϣ</b>" + NewLine);

            var list = context.Items[__List__] as List<NameTime>;



            foreach (var nameTime in list)
            {
                context.Response.Write("" + nameTime.Name + "->" + nameTime.Do + "->" + nameTime.Time + "->" + (nameTime.Parent != null ? nameTime.Parent.RouteData.Values["action"].ToString() : "NONE") + NewLine);
            }
            context.Response.Write("<br/>-------------------------------------------------------------" + NewLine);

            var node = Parse(list);

            var str = PrintNode(node);

            context.Response.Write(str);

            if (!IsShow)
                context.Response.Write("-->");


            // CS html Trace

            var cshtmlstr = CshtmlTrance.Print(IsShow, NewLine);

            context.Response.Write(cshtmlstr);


            context.Items.Remove(__List__);
        }


        private string PrintNode(WatchNode node)
        {

            var s = "name:" + node.Name + " = " + node.All + "  action->" + node.Action + " Result->" + node.Result + " parent->" + node.ParentName + NewLine;

            if (node.Child != null && node.Child.Count > 0)
            {
                s += "c___:" + node.Name + "=" + node.Child.Sum(x => x.All) + "  action->" +
                     node.Child.Sum(x => x.Action) + " Result->" + node.Child.Sum(x => x.Result) + NewLine;
            }

            foreach (var watchData in node.Child)
            {
                var temp = watchData;
                var c = 0;
                while (temp.Parent != null)
                {
                    temp = temp.Parent;
                    c++;
                }
                s += "".PadLeft(c * 2, '-') + PrintNode(watchData);
            }

            return s;

        }



        private WatchNode Parse(List<NameTime> list)
        {
            WatchNode TopNode = new WatchNode();
            var current = TopNode;
            foreach (var nameTime in list)
            {

                switch (nameTime.Do)
                {
                    case "OnActionExecuting":
                        if (nameTime.Parent == null)
                        {
                            TopNode.IsChild = false;
                            TopNode.OnActionExecuting = nameTime.Time;
                        }
                        else
                        {
                            var newnode = new WatchNode();
                            newnode.Parent = current;
                            current.Child.Add(newnode);
                            current = newnode;
                            current.IsChild = true;
                            current.OnActionExecuting = nameTime.Time;
                        }

                        current.Name = nameTime.Name;

                        break;
                    case "OnActionExecuted":
                        current.OnActionExecuted = nameTime.Time;

                        break;
                    case "OnResultExecuting":
                        current.OnResultExecuting = nameTime.Time;
                        break;
                    case "OnResultExecuted":
                        current.OnResultExecuted = nameTime.Time;

                        if (current.Parent != null && current.Parent.Name == nameTime.ParentName)
                        {
                            current = current.Parent;
                        }

                        break;

                }

            }

            return TopNode;
        }

        private void CheckItem(HttpContextBase httpcontext, string name, string Do, ViewContext parent)
        {

            if (!httpcontext.Items.Contains(__List__))
            {
                httpcontext.Items.Add(__List__, new List<NameTime>());
            }

            var list = httpcontext.Items[__List__] as List<NameTime>;

            list.Add(new NameTime
            {
                Name = name,
                Time = System.DateTime.Now.Ticks,
                Do = Do,
                Parent = parent
            });

        }



    }

    #endregion

    #region CSHTML TRACE

    internal class CshtmlTraceModel
    {
        public string Name { get; set; }
        public long TimeTicks { get; set; }
    }

    /// <summary>
    /// CSHtmlҳ��TRance
    /// </summary>
    public class CshtmlTrance
    {
        private const string CshtmlTrancekey = "_________CshtmlTrancekey_____________";
        public static void Add(string name, long timeTicket)
        {
            var httpcontext = HttpContext.Current;

            if (!httpcontext.Items.Contains(CshtmlTrancekey))
            {
                httpcontext.Items.Add(CshtmlTrancekey, new List<CshtmlTraceModel>());
            }

            var list = httpcontext.Items[CshtmlTrancekey] as List<CshtmlTraceModel>;

            list.Add(new CshtmlTraceModel
            {
                Name = name,
                TimeTicks = timeTicket
            });
        }


        public static string Print(bool isshow = false, string newLine = "<br/>\r\n")
        {
            var httpcontext = HttpContext.Current;

            var list = new List<CshtmlTraceModel>();

            if (httpcontext.Items.Contains(CshtmlTrancekey))
            {
                list = httpcontext.Items[CshtmlTrancekey] as List<CshtmlTraceModel>;
            }


            StringBuilder sb = new StringBuilder();




            if (!isshow)
                sb.Append("<!-- ").Append(newLine);

            sb.Append("cshtml ���ٱ���").Append(newLine);

            foreach (var cshtmlTraceModel in list)
            {
                sb.Append("" + cshtmlTraceModel.Name + "->" + cshtmlTraceModel.TimeTicks / 10000).Append(newLine);
            }

            if (!isshow)
                sb.Append("-->");

            httpcontext.Items.Remove(CshtmlTrancekey);
            return sb.ToString();
        }

    }

    #endregion

    #region RequestTrace
    /// <summary>
    /// ��Requestһ�ε�ʱ����м��
    /// </summary>
    public class BeginEndRequestTrace
    {

        private const string Key = "_____________BeginEndRequest___________";

        private const string BeginRequest = Key + "BeginRequest";

        private const string EndRequest = Key + "EndRequest";

        /// <summary>
        /// http://stackoverflow.com/questions/1123741/why-cant-my-host-softsyshosting-com-support-beginrequest-and-endrequest-event
        /// <![CDATA[public class MySmartApp: HttpApplication{
        /// public override void Init(){
        ///     this.BeginRequest += new EventHandler(MvcApplication_BeginRequest);
        ///    this.EndRequest += new EventHandler(MvcApplication_EndRequest);
        ///  }
        ///  protected void Application_Start(){
        ///      RegisterRoutes(RouteTable.Routes);
        ///  } 
        ///}
        ///or like this: 
        ///public class MySmartApp: HttpApplication{
        /// public MySmartApp(){
        ///  this.BeginRequest += new EventHandler(MvcApplication_BeginRequest);
        /// this.EndRequest += new EventHandler(MvcApplication_EndRequest);
        /// }
        ///protected void Application_Start(){
        /// RegisterRoutes(RouteTable.Routes);
        ///} 
        ///}
        ///]]>
        /// </summary>
        /// <param name="app"></param>
        ///<param name="isshow">�Ƿ���ҳ������ʾ </param>
        ///<param name="newLine">ʹ��ʲô���� </param>
        public static void Init(HttpApplication app, bool isshow = false, string newLine = "<br/>\r\n")
        {

            app.BeginRequest += (sender, e) =>
            {
                app.Context.Items[BeginRequest] = System.DateTime.Now.Ticks;
                //app.Response.Write(System.DateTime.Now.Ticks);
            };

            app.EndRequest += (sender, e) =>
            {
                app.Context.Items[EndRequest] = System.DateTime.Now.Ticks;
                //app.Response.Write(System.DateTime.Now.Ticks);

                var strPrint = Print(isshow, newLine);

                app.Response.Write(strPrint);
            };
        }

        private static string Print(bool isshow = false, string newLine = "<br/>\r\n")
        {

            var httpcontext = HttpContext.Current;


            //����ajax���󣬲����ӽ��
            var xmlhttp = httpcontext.Request.Headers["X-Requested-With"];
            if (xmlhttp == "XMLHttpRequest")
            {
                return string.Empty;
            }

            //���ݷ��صĵ����ͣ�ֻ�з���text/htmlʱ���ŷ��ر��� 
            var responetype = httpcontext.Response.ContentType;
            if (responetype != "text/html")
            {
                return string.Empty;
            }

            //ֻ��ӡ ViewResult���͵ģ����������̫ͨ�ã��������������� TraceRunAttribute ��������ʱû�и��õİ취����������
            if (!httpcontext.Items.Contains(TraceRunAttribute.ViewResultTypeKey) || (string)httpcontext.Items[TraceRunAttribute.ViewResultTypeKey] != "ViewResult")
            {
                httpcontext.Items.Remove(TraceRunAttribute.ViewResultTypeKey);
                return string.Empty;
            }

            long tbegin = 0;
            long tend = 0;

            if (httpcontext.Items.Contains(BeginRequest))
            {
                tbegin = (long)httpcontext.Items[BeginRequest];
                httpcontext.Items.Remove(BeginRequest);
            }


            if (httpcontext.Items.Contains(EndRequest))
            {
                tend = (long)httpcontext.Items[EndRequest];
                httpcontext.Items.Remove(EndRequest);
            }


            StringBuilder sb = new StringBuilder();



            if (!isshow)
                sb.Append("<!-- ").Append(newLine);
            sb.Append(newLine);
            sb.Append("Request ���ٱ���").Append(newLine);


            if (tbegin == 0 || tend == 0)
            {
                sb.Append("Request->Not Setup").Append(newLine);
            }
            else
            {
                sb.Append("BeginRequest->" + tbegin).Append(newLine);
                sb.Append("EndRequest->" + tend).Append(newLine);
                sb.Append("Request->" + (tend - tbegin) / 10000).Append(newLine);

            }

            if (!isshow)
                sb.Append("-->");

            return sb.ToString();
        }


    }



    /// <summary>
    /// ʹ��IHttpModule ��ʽע������¼�
    /// </summary>
    public class BeginEndRequestTraceHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            BeginEndRequestTrace.Init(context);
            //throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }
    }


    /// <summary>
    /// Begin  End Request Trace
    /// </summary>
    public static class BeginEndRequestTraceHttpModuleRegister
    {
        /// <summary>
        /// Do it
        /// </summary>
        public static void Do()
        {
            DynamicModuleUtility.RegisterModule(typeof(BeginEndRequestTraceHttpModule));
        }
    }

    #endregion
}