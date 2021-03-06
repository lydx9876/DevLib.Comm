﻿// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月17日 11:22
//  
//  修改于：2013年06月17日 12:28
//  文件名：Dev.Libs/Dev.Framework.FileServer.Impl/LocalUploadFile.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.IO;
using Dev.Comm;
using Dev.Comm.Utils;

namespace Dev.Framework.FileServer.LocalUploaderFileImpl
{
    /// <summary>
    ///   本地文件方式
    /// </summary>
    public class LocalUploadFile : IUploadFile
    {
        #region Fields

        private IKey _currentKey;

        #endregion

        #region C'tors

        /// <summary>
        ///   Ctor
        /// </summary>
        /// <param name="key"> </param>
        public LocalUploadFile(IKey key)
        {
            this._currentKey = key;
        }

        #endregion

        #region Instance Methods

        private string GetFilePath(FileSaveInfo fileInfo, string newFilename = null)
        {

            var startdirname = fileInfo.FileServer.startdirname;

            var apppath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\', '/'); ;

            startdirname = startdirname.Replace("|APP_BASE|", apppath);



            //var filepath = "" + startdirname + "/" + fileInfo.Dirname + "/" + fileInfo.Savefilename;

            var filepath = Path.Combine(startdirname, fileInfo.Dirname, newFilename ?? fileInfo.Savefilename);


            return filepath;
        }

        #endregion

        #region IUploadFile Members

        /// <summary>
        ///   生成KEY方案
        /// </summary>
        /// <param name="key"> </param>
        public void SetCurrentKey(IKey key)
        {
            this._currentKey = key;
        }

        /// <summary>
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="fileKey"> </param>
        /// <param name="param"> </param>
        /// <returns> </returns>
        public string SaveFile(byte[] bytefile, string fileKey, params object[] param)
        {
            return UpdateFile(bytefile, fileKey, param);
        }


        /// <summary>
        /// </summary>
        /// <param name="stream"> </param>
        /// <param name="fileKey"> </param>
        /// <returns> </returns>
        public string SaveFile(Stream stream, string fileKey, params object[] param)
        {
            return UpdateFile(stream, fileKey, param);
        }


        /// <summary>
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="fileKey"> </param>
        /// <param name="param"> </param>
        /// <returns> </returns>
        public string UpdateFile(byte[] bytefile, string fileKey, params object[] param)
        {
            return this.UpdateFile(FileUtil.BytesToStream(bytefile), fileKey, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="fileKey"></param>
        /// <param name="specifyName"></param>
        /// <returns></returns>

        public string SaveFileBySpecifyName(byte[] bytefile, string fileKey, string specifyName)
        {
            return SaveFileBySpecifyName(FileUtil.BytesToStream(bytefile), fileKey, specifyName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileKey"></param>
        /// <param name="specifyName"></param>
        /// <returns></returns>
        public string SaveFileBySpecifyName(Stream stream, string fileKey, string specifyName)
        {
            stream.Seek(0, SeekOrigin.Begin);

            var fileInfo = this._currentKey.GetFileSavePath(fileKey);

            var filepath = this.GetFilePath(fileInfo, specifyName);

            FileUtil.StreamToFile(stream, filepath);

            return fileKey;
        }
        /// <summary>
        /// 根据文件Key删除文件 
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public void DeleteFile(string fileKey, params object[] param)
        {
            var fileInfo = this._currentKey.GetFileSavePath(fileKey, param);

            var filepath = this.GetFilePath(fileInfo);

            FileUtil.DeleteFile(filepath);
        }

        /// <summary>
        /// 删除File所在的Path
        /// </summary>
        /// <param name="fileKey"></param>
        public void DeltePath(string fileKey)
        {
            var fileInfo = this._currentKey.GetFileSavePath(fileKey);

            var filepath = this.GetFilePath(fileInfo);

            var pathname = Path.GetDirectoryName(filepath);

            if (Directory.Exists(pathname))
                Directory.Delete(pathname, true);
        }

        public bool ExistFile(string fileKey, params object[] param)
        {
            var fileInfo = this._currentKey.GetFileSavePath(fileKey, param);

            var filepath = this.GetFilePath(fileInfo);

            return File.Exists(filepath);
        }

        /// <summary>
        /// 取得文件的物理存储位置
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetFileStorePath(string fileKey, params object[] param)
        {
            var fileInfo = this._currentKey.GetFileSavePath(fileKey, param);

            var filepath = this.GetFilePath(fileInfo);

            return filepath;
        }


        public string UpdateFile(Stream stream, string fileKey, params object[] param)
        {
            stream.Seek(0, SeekOrigin.Begin);

            var fileInfo = this._currentKey.GetFileSavePath(fileKey, param);

            var filepath = this.GetFilePath(fileInfo);

            FileUtil.StreamToFile(stream, filepath);

            return fileKey;
        }

        #endregion
    }
}