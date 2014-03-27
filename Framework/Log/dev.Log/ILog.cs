// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����ILog.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Log
{
    /// <summary>
    /// Defines a single method to write requested log events to an output device.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Write a log request to a given output device.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        void Log(object sender, LogEventArgs e);
    }
}