// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2013��02��18�� 17:37
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����UserForbiddenModel.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.Forbidden
{
    using System;

    public class UserForbiddenModel
    {
        #region Public Properties

        public int ErrorCount { get; set; }

        /// <summary>
        ///     ���һ�ε�¼����ʱ��
        /// </summary>
        public DateTime LastTime { get; set; }

        public decimal Uid { get; set; }

        #endregion
    }

    //end UserForbiddenModel
}

//end namespace Forbidden