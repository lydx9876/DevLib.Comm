// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2013��02��18�� 17:37
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����DBForbidden.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.Forbidden
{
    /// <summary>
    ///     ���������������ݿ��ϣ��ݲ�ʵ��
    /// </summary>
    public class DBForbidden : IForbidden
    {
        #region Public Methods and Operators

        public void ClearForbiddenList(decimal uid)
        {
        }

        /// <summary>
        ///     ���б����ų�
        /// </summary>
        /// <param name="UserForbiddenModel">�Ӷ������ų�</param>
        public void DeList(UserForbiddenModel UserForbiddenModel)
        {
        }

        public virtual void Dispose()
        {
        }

        /// <summary>
        ///     �����б�
        /// </summary>
        /// <param name="UserForbiddenModel"></param>
        public void EnList(UserForbiddenModel UserForbiddenModel)
        {
        }

        /// <param name="Uid"></param>
        public bool IsForbidden(decimal Uid)
        {
            return false;
        }

        #endregion
    }

    //end DBForbidden
}

//end namespace Forbidden