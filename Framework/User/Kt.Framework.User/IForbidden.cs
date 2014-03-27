// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����IForbidden.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
using Dev.Framework.User.Forbidden;

namespace Dev.Framework.User
{
    public interface IForbidden
    {
        /// <summary>
        ///     ���б����ų�
        /// </summary>
        /// <param name="UserForbiddenModel">�Ӷ������ų�</param>
        void DeList(UserForbiddenModel UserForbiddenModel);

        /// <summary>
        ///     �����б�
        /// </summary>
        /// <param name="UserForbiddenModel"></param>
        void EnList(UserForbiddenModel UserForbiddenModel);

        /// <summary>
        ///     �Ƿ񱻾ܾ�
        /// </summary>
        /// <param name="Uid"></param>
        bool IsForbidden(decimal Uid);

        void ClearForbiddenList(decimal uid);
    }

//end IForbidden
}

//end namespace Forbidden