// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����ObserverLogToFile.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************

using System.IO;

namespace Dev.Log.Impl
{
    /// <summary>
    /// Writes log events to a local file.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class ObserverLogToFile : ILog
    {
        private readonly string _fileName;

        /// <summary>
        /// Constructor of ObserverLogToFile.
        /// </summary>
        /// <param name="fileName">File log to.</param>
        public ObserverLogToFile(string fileName)
        {
            _fileName = fileName;
        }

        #region ILog Members

        /// <summary>
        /// Write a log request to a file.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            string message = "[" + e.Date.ToString() + "] " +
                             e.SeverityString + ": " + e.Message;

            FileStream fileStream;


            // Create directory, if necessary
            try
            {
                fileStream = new FileStream(_fileName, FileMode.Append);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory((new FileInfo(_fileName)).DirectoryName);
                fileStream = new FileStream(_fileName, FileMode.Append);
            }

            // NOTE: Be sure you have write privileges to folder
            var writer = new StreamWriter(fileStream);
            try
            {
                writer.WriteLine(message);
            }
            catch
            {
                /* do nothing */
            }
            finally
            {
                try
                {
                    writer.Close();
                }
                catch
                {
                    /* do nothing */
                }
            }
        }

        #endregion
    }
}