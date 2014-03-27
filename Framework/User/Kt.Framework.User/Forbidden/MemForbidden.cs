// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����MemForbidden.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Framework.User.Forbidden
{
    /// <summary>
    ///     �ڴ�ʵ��
    /// </summary>
    public class MemForbidden : IForbidden
    {
        /// <summary>
        ///     �޶��б�
        /// </summary>
        private static List<UserForbiddenModel> list = new List<UserForbiddenModel>();

        private static readonly object lockobj = new object();


        internal IEnumerable<UserForbiddenModel> UserList
        {
            get
            {
                lock (lockobj)
                {
                    return list;
                }
            }
        }

        #region IForbidden Members

        /// <summary>
        ///     ���б����ų�
        /// </summary>
        /// <param name="UserForbiddenModel">�Ӷ������ų�</param>
        public void DeList(UserForbiddenModel UserForbiddenModel)
        {
            list.Remove(UserForbiddenModel);
        }

        /// <summary>
        ///     �����б�
        /// </summary>
        /// <param name="UserForbiddenModel"></param>
        public void EnList(UserForbiddenModel UserForbiddenModel)
        {
            list.Add(UserForbiddenModel);
            SortList();
        }

        /// <summary>
        ///     ������ȷ������б��д��û��ļ�¼���������ʧЧ������
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public void ClearForbiddenList(decimal uid)
        {
            UserForbiddenModel user = list.Where(x => x.Uid == uid).FirstOrDefault();
            DeList(user);
            //���£��������һ�����ڵ��ô˷�����ѭ��ɾ����ʱ�򣬽�������˵ļ�¼ɾ���ˣ����������
            //foreach (var u in UserList)
            //{
            //    if (DateTime.Now >= u.LastTime.AddMinutes(ForbiddenConfig.KEEPTIME))
            //    {
            //        this.DeList(u);
            //    }
            //}
        }

        /// <summary>
        ///     ����uid�жϴ��û��Ƿ��ڽ����б���
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns>false����ʾ�û����ڽ����б��У� true����ʾ�û��ڽ����б���</returns>
        public bool IsForbidden(decimal Uid)
        {
            //�ж��ڽ����б����Ƿ�����������
            UserForbiddenModel user = UserList.FirstOrDefault(x => x.Uid == Uid);
            if (user != null)
            {
                if (DateTime.Now >= user.LastTime.AddMinutes(ForbiddenConfig.KEEPTIME)) //δ������Сʱ����
                {
                    FreshList(new UserForbiddenModel {Uid = Uid, LastTime = DateTime.Now, ErrorCount = 1});
                    return false;
                }
                else
                {
                    if (user.ErrorCount >= ForbiddenConfig.MAXERROR)
                    {
                        return true;
                    }
                    else
                    {
                        FreshList(new UserForbiddenModel
                                      {Uid = Uid, LastTime = DateTime.Now, ErrorCount = user.ErrorCount + 1});
                        return false;
                    }
                }
            }
            else
            {
                EnList(new UserForbiddenModel {Uid = Uid, LastTime = DateTime.Now, ErrorCount = 1});
                return false;
            }

            /*���ȸ��ݴ����Uid�ж��ڽ����б����Ƿ�����������
                ������ڣ��жϴ˶����_LastTime�͵�ǰʱ��Ƚ��Ƿ񳬹���KEEPTIME
             *      ����ǣ����´��û���_LastTime=��ǰʱ�䣬_ErrorCount=1������false
             *          ��
             *              �жϴ��û���_ErrorCount�Ƿ���ڵ���MAXERROR
             *                  ����ǣ���ֱ�ӷ���true����ʾ���û��Ѿ�������
             *                  ������ǣ����´��û���_LastTime=��ǰʱ�䣬_ErrorCount=_ErrorCount+1������false
             *                  
             *  �粻���ڣ�
             *          ���б��������˶���_LastTime=��ǰʱ�䣬_ErrorCount=1������false
             */
        }

        #endregion

        /// <summary>
        ///     ˢ���б�
        /// </summary>
        /// <param name="UserForbiddenModel"></param>
        private void FreshList(UserForbiddenModel UserForbiddenModel)
        {
            UserForbiddenModel user = list.Where(x => x.Uid == UserForbiddenModel.Uid).FirstOrDefault();
            if (list.Remove(user))
            {
                EnList(UserForbiddenModel);
            }
        }

        /// <summary>
        ///     ����
        /// </summary>
        private void SortList()
        {
            if (UserList.Count() != 0)
            {
                list = UserList.OrderByDescending(x => x.LastTime).ToList();
            }
        }
    }

//end MemForbidden
}

//end namespace Forbidden