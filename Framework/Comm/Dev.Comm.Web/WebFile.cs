// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����WebFile.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
using System;
using System.IO;
using System.Text;
using System.Web;

namespace Dev.Comm.Web
{
    /// <summary>
    /// �ļ�����
    /// </summary>
    public class WebFile
    {
        public WebFile()
        {
        }

        #region Fields/Attributes/Properties

        private HttpResponse Response;
        private FileInfo fileInfo;

        #endregion

        #region Constructor

        public WebFile(HttpResponse response)
        {
            Response = response;
        }

        #endregion

        #region Check Method

        /// <summary>
        /// ָ���ļ��Ƿ����
        /// </summary>
        /// <param name="fullName">ָ���ļ�����·��</param>
        /// <returns>���ڷ���true;���򷵻�false</returns>
        public bool IsFileExists(string fullName)
        {
            fileInfo = new FileInfo(fullName);
            if (fileInfo.Exists)
                return true;
            return false;
        }

        #endregion

        #region File Upload

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="postedFile">�ϴ����ļ�</param>
        /// <param name="saveAsFullName">�ļ����������·��������������</param>
        /// <returns>�ɹ�����true;���򷵻�false</returns>
        public bool UploadFile(HttpPostedFile postedFile, string saveAsFullName)
        {
            return UploadFile(postedFile, saveAsFullName, false);
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="postedFile">�ϴ����ļ�</param>
        /// <param name="saveAsFullName">�ļ����������·��</param>
        /// <param name="isReplace">�����ͬ���ļ����ڣ��Ƿ񸲸�</param>
        /// <returns>�ɹ�����true,���򷵻�false</returns>
        public bool UploadFile(HttpPostedFile postedFile, string saveAsFullName, bool isReplace)
        {
            try
            {
                if (!isReplace && IsFileExists(saveAsFullName))
                    return false;
                postedFile.SaveAs(saveAsFullName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region File Download

        /// <summary>
        /// �ļ�����
        /// </summary>
        /// <param name="fullName">�ļ�����·��</param>
        /// <returns>���سɹ�����true,���򷵻�false</returns>
        public bool DownloadFile(string fullName)
        {
            return DownloadFile(fullName, fullName.Substring(fullName.LastIndexOf(@"\") + 1));
        }

        /// <summary>
        /// �ļ�����
        /// </summary>
        /// <param name="fullName">�ļ�����·��</param>
        /// <param name="sendFileName">���͵��ͻ�����ʾ���ļ���</param>
        /// <returns>���سɹ�����true,���򷵻�false</returns>
        public bool DownloadFile(string fullName, string sendFileName)
        {
            if (!IsFileExists(fullName))
                return false;


            fileInfo = new FileInfo(fullName);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                                                   "attachment; filename=" +
                                                   HttpUtility.UrlEncode(sendFileName, Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(fileInfo.FullName);
            //HttpContext.Current.Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            return true;
        }

        #endregion

        #region File Delete

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="fullName">�ļ�����·��</param>
        /// <returns>ɾ���ɹ�����true,���򷵻�false</returns>
        public bool DeleteFile(string fullName)
        {
            if (!IsFileExists(fullName))
                return false;
            try
            {
                fileInfo = new FileInfo(fullName);
                fileInfo.Delete();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region File Move To

        /// <summary>
        /// �ƶ��ļ�
        /// </summary>
        /// <param name="fullName">�ļ�����·��</param>
        /// <param name="newFullName">�ļ��ƶ���������·����������������������������</param>
        /// <returns>�ƶ��ɹ�����true,���򷵻�false</returns>
        public bool MoveTo(string fullName, string newFullName)
        {
            return MoveTo(fullName, newFullName, false);
        }

        /// <summary>
        /// �ƶ��ļ�
        /// </summary>
        /// <param name="fullName">�ļ�����·��</param>
        /// <param name="newFullName">�ļ��ƶ���������·����������������</param>
        /// <param name="isReplace">�����ͬ���ļ����ڣ��Ƿ񸲸�</param>
        /// <returns>�ƶ��ɹ�����true,���򷵻�false</returns>
        public bool MoveTo(string fullName, string newFullName, bool isReplace)
        {
            if (!isReplace && IsFileExists(fullName))
                return false;
            try
            {
                fileInfo = new FileInfo(fullName);
                fileInfo.MoveTo(newFullName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region File Copy To

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="fullName">�ļ�����·��</param>
        /// <param name="newFullName">�ļ��ƶ���������·����������������</param>
        /// <returns>���Ƴɹ�����true,���򷵻�false</returns>
        public bool CopyTo(string fullName, string newFullName)
        {
            return CopyTo(fullName, newFullName, false);
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="fullName">�ļ�����·��</param>
        /// <param name="newFullName">�ļ��ƶ���������·����������������</param>
        /// <param name="isReplace">�����ͬ���ļ����ڣ��Ƿ񸲸�</param>
        /// <returns>���Ƴɹ�����true,���򷵻�false</returns>
        public bool CopyTo(string fullName, string newFullName, bool isReplace)
        {
            if (!isReplace && IsFileExists(fullName))
                return false;
            try
            {
                fileInfo = new FileInfo(fullName);
                fileInfo.CopyTo(newFullName, false);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}