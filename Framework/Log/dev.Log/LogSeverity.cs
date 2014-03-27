// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����LogSeverity.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Log
{
    /// <summary>
    /// Enumeration of log severity levels.
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        /// Severity level of "Debug"
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Severity level of "Info"
        /// </summary>
        Info = 2,

        /// <summary>
        /// Severity level of "Warning"
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Severity level of "Error"
        /// </summary>
        Error = 4,

        /// <summary>
        /// Severity level of "Fatal"
        /// </summary>
        Fatal = 5
    }
}