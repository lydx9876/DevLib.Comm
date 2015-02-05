using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Messaging;

namespace Dev.Framework.MessageQuene
{
    /// <summary>
    /// ΢����Ϣ���е�Ĭ��ʵ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MsmqQueneImpl<T> : IMsgQuene<T>
    {
        private readonly string _queuePath;
        private MessageQueue _myQueue;
        private XmlMessageFormatter formatter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queuePath"></param>
        /// <param name="isLocalMachine"></param>
        public MsmqQueneImpl(string queuePath, bool isLocalMachine)
        {
            _queuePath = queuePath;

            if (isLocalMachine)
                CreateIfNotExist();

            this._myQueue = new MessageQueue(_queuePath);

            formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
        }



        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public void Createqueue()
        {
            this._myQueue = MessageQueue.Create(_queuePath);


            //this._myQueue.ReceiveCompleted += _myQueue_ReceiveCompleted;
            //throw new NotImplementedException();

            _myQueue.BeginReceive();
        }

        /// <summary>
        /// �����첽������ֻ����һ��
        /// </summary>
        /// <param name="func"></param>
        public void ConfigReceiveCompleted(Action<T> func)
        {

            this._myQueue.ReceiveCompleted += (sender, e) =>
            {
                e.Message.Formatter = formatter;
                func((T)e.Message.Body);
            };
        }
        /// <summary>
        /// �����첽������ֻ����һ��
        /// </summary>
        /// <param name="func"></param>
        public void ConfigReceiveCompleted(Action<T, IAsyncResult> func)
        {

            this._myQueue.ReceiveCompleted += (sender, e) =>
            {
                e.Message.Formatter = formatter;
                func((T)e.Message.Body, e.AsyncResult);
            };
        }




        /// <summary>
        /// ��ʼ�첽
        /// </summary>
        public IAsyncResult BeginReceive()
        {
            return this._myQueue.BeginReceive();
        }


        /// <summary>
        /// �����첽
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public T EndRecive(IAsyncResult asyncResult)
        {
            var message = this._myQueue.EndReceive(asyncResult);
            return (T)message.Body;
        }





        public void CreateIfNotExist()
        {
            if (!Exists())
                Createqueue();
        }

        /// <summary>
        /// �鿴ָ����Ϣ�����Ƿ���� 
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            return MessageQueue.Exists(_queuePath);
        }

        /// <summary>
        /// ɾ�����е���Ϣ����
        /// </summary>
        /// <returns></returns>
        public void Delete()
        {
            MessageQueue.Delete(_queuePath);



        }

        /// <summary>
        /// �õ������е�������Ϣ
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllMessages()
        {


            Message[] message = this._myQueue.GetAllMessages();


            List<T> list = new List<T>();


            foreach (Message msg in message)
            {
                msg.Formatter = formatter;

                //_myQueue.ReceiveById(msg.Id);
                list.Add((T)msg.Body);
            }



            return list;
        }

        /// <summary>
        /// �鿴ĳ���ض������е���Ϣ���У������Ӹö������Ƴ���Ϣ��
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {

            //try
            //{
            //�Ӷ����н�����Ϣ
            Message myMessage = this._myQueue.Peek();
            T context = (T)myMessage.Body; //��ȡ��Ϣ������
            return context;
            //}
            //catch (MessageQueueException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //catch (InvalidCastException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        /// <summary>
        /// ����ID Peek ��������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T PeekById(string id)
        {
            var myMessage = this._myQueue.PeekById(id);
            T context = (T)myMessage.Body; //��ȡ��Ϣ������
            return context;
        }

        /// <summary>
        /// ����ָ����Ϣ��������ǰ�����Ϣ������Ӹö������Ƴ���
        /// </summary>
        /// <returns></returns>
        public T Receive()
        {
            //this._myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            //try
            //{
            //�Ӷ����н�����Ϣ
            this._myQueue.Formatter = formatter;
            Message myMessage = this._myQueue.Receive();
            T context = (T)myMessage.Body; //��ȡ��Ϣ������
            return context;
        }

        /// <summary>
        /// ����ID ����ָ����Ϣ��������ǰ�����Ϣ������Ӹö������Ƴ���
        /// </summary>
        /// <returns></returns>
        public T ReceiveById(string id)
        {
            Message myMessage = this._myQueue.ReceiveById(id);
            T context = (T)myMessage.Body; //��ȡ��Ϣ������
            return context;
        }

        /// <summary>
        /// ������Ϣ��ָ������Ϣ����
        /// </summary>
        /// <param name="msg"></param>
        public void Send(T msg)
        {
            //try
            //{
            //���ӵ����صĶ���
            Message myMessage = new Message();
            myMessage.Formatter = formatter;
            myMessage.Body = msg;

            //������Ϣ��������
            this._myQueue.Send(myMessage);
            //}
            //catch (ArgumentException e)
            //{
            //    Console.WriteLine(e.Message);
            //}

        }


        protected Message PeekWithoutTimeout(MessageQueue q, Cursor cursor, PeekAction action)
        {
            Message ret = null;
            try
            {
                ret = q.Peek(new TimeSpan(1), cursor, action);
            }
            catch (MessageQueueException mqe)
            {
                if (!mqe.Message.ToLower().Contains("timeout"))
                {
                    throw;
                }
            }
            return ret;
        }

        /// <summary>
        /// ȡ��MessageQueue���еĴ�С
        /// </summary>
        /// <returns></returns>
        public int GetMessageCount()
        {
            return GetPowerShellCount();

        }


        /// <summary>
        /// ���ָ�����е���Ϣ
        /// </summary>
        public void Clear()
        {
            this._myQueue.Purge();
        }


        private int GetPowerShellCount()
        {
            return GetPowerShellCount(this._queuePath, Environment.MachineName, "", "");
        }
        private int GetPowerShellCount(string queuePath, string machine, string username, string password)
        {
            var path = string.Format(@"\\{0}\root\CIMv2", machine);
            ManagementScope scope;
            if (string.IsNullOrEmpty(username))
            {
                scope = new ManagementScope(path);
            }
            else
            {
                var options = new ConnectionOptions { Username = username, Password = password };
                scope = new ManagementScope(path, options);
            }
            scope.Connect();
            if (queuePath.StartsWith(".\\")) queuePath = queuePath.Replace(".\\", string.Format("{0}\\", machine));

            string queryString = String.Format("SELECT * FROM Win32_PerfFormattedData_msmq_MSMQQueue");
            var query = new ObjectQuery(queryString);
            var searcher = new ManagementObjectSearcher(scope, query);
            IEnumerable<int> messageCountEnumerable =
                from ManagementObject queue in searcher.Get()
                select (int)(UInt64)queue.GetPropertyValue("MessagesInQueue");
            //IEnumerable<string> messageCountEnumerable =
            //  from ManagementObject queue in searcher.Get()
            //  select (string)queue.GetPropertyValue("Name");
            var x = messageCountEnumerable.First();
            return x;
        }
    }
}