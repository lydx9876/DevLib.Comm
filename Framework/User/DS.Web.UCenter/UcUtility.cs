// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcUtility.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DS.Web.UCenter
{
    /// <summary>
    /// Dozer 版权所有
    /// 允许复制、修改，但请保留我的联系方式！
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    public static class UcUtility
    {
        private static Encoding _encode;

        private static Encoding Encode
        {
            get { return _encode ?? (_encode = Encoding.GetEncoding(UcConfig.UcCharset)); }
        }

        #region AuthCode

        /// <summary>
        /// AuthCode
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="operation">操作</param>
        /// <param name="keyStr">KEY</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        private static string authCode(string sourceStr, AuthCodeMethod operation, string keyStr, int expiry = 0)
        {
            const int ckeyLength = 4;
            byte[] source = Encode.GetBytes(sourceStr);
            byte[] key = Encode.GetBytes(keyStr);

            key = Md5(key);

            byte[] keya = Md5(SubBytes(key, 0, 0x10));
            byte[] keyb = Md5(SubBytes(key, 0x10, 0x10));
            byte[] keyc = getKeyc(ckeyLength, operation, source);

            byte[] cryptkey = AddBytes(keya, Md5(AddBytes(keya, keyc)));
            int keyLength = cryptkey.Length;

            source = getCKey(expiry, ckeyLength, operation, source, keyb);

            int sourceLength = source.Length;

            int[] box = getBox();

            int[] rndkey = getRndkey(cryptkey, keyLength);

            rndBox(box, rndkey);

            byte[] result = getResult(source, box, sourceLength);


            return operation == AuthCodeMethod.Decode
                       ? check(keyb, result)
                       : BytesToString(keyc) + Convert.ToBase64String(result).Replace("=", "");
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <param name="keyb">Keyb</param>
        /// <param name="result">结果</param>
        /// <returns></returns>
        private static string check(byte[] keyb, byte[] result)
        {
            long time = long.Parse(BytesToString(result).Substring(0, 10));
            if ((time == 0 ||
                 time - PhpTimeNow() > 0) &&
                BytesToString(SubBytes(result, 10, 16)) ==
                BytesToString(SubBytes(Md5(AddBytes(SubBytes(result, 26), keyb)), 0, 16)))
            {
                return BytesToString(SubBytes(result, 26));
            }
            return "";
        }

        /// <summary>
        /// 计算结果
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="box">Box</param>
        /// <param name="sourceLength">数据源长度</param>
        /// <returns></returns>
        private static byte[] getResult(IList<byte> source, IList<int> box, int sourceLength)
        {
            var result = new byte[sourceLength];
            for (int a = 0, j = 0, i = 0; i < sourceLength; i++)
            {
                a = (a + 1)%256;
                j = (j + box[a])%256;
                int tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;

                result[i] = (byte) (source[i] ^ (box[(box[a] + box[j])%256]));
            }
            return result;
        }

        /// <summary>
        /// Box乱序
        /// </summary>
        /// <param name="box"></param>
        /// <param name="rndkey"></param>
        private static void rndBox(IList<int> box, IList<int> rndkey)
        {
            for (int j = 0, i = 0; i < 256; i++)
            {
                j = (j + box[i] + rndkey[i])%256;
                int tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }
        }

        /// <summary>
        /// 得到RandomKey
        /// </summary>
        /// <param name="cryptkey">cryptkey</param>
        /// <param name="keyLength">keyLength</param>
        /// <returns></returns>
        private static int[] getRndkey(byte[] cryptkey, int keyLength)
        {
            var rndkey = new int[256];
            for (int i = 0; i < 256; i++)
            {
                rndkey[i] = cryptkey[i%keyLength];
            }
            return rndkey;
        }

        /// <summary>
        /// 得到Box
        /// </summary>
        /// <returns></returns>
        private static int[] getBox()
        {
            var box = new int[256];
            for (int k = 0; k < 256; k++)
            {
                box[k] = k;
            }
            return box;
        }

        /// <summary>
        /// 得到CKey
        /// </summary>
        /// <param name="expiry">过期时间</param>
        /// <param name="ckeyLength">CKey长度</param>
        /// <param name="operation">操作</param>
        /// <param name="source">数据源</param>
        /// <param name="keyb">Keyb</param>
        /// <returns></returns>
        private static byte[] getCKey(int expiry, int ckeyLength, AuthCodeMethod operation, byte[] source, byte[] keyb)
        {
            if (operation == AuthCodeMethod.Decode)
            {
                while (source.Length%4 != 0)
                {
                    source = AddBytes(source, Encode.GetBytes("="));
                }
                source = Convert.FromBase64String(BytesToString(SubBytes(source, ckeyLength)));
            }
            else
            {
                source =
                    AddBytes(
                        (expiry != 0
                             ? Encode.GetBytes((expiry + PhpTimeNow()).ToString())
                             : Encode.GetBytes("0000000000")),
                        SubBytes(Md5(AddBytes(source, keyb)), 0, 0x10), source);
            }
            return source;
        }

        /// <summary>
        /// 得到keyc
        /// </summary>
        /// <param name="ckeyLength">ckey长度</param>
        /// <param name="operation">操作</param>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        private static byte[] getKeyc(int ckeyLength, AuthCodeMethod operation, byte[] source)
        {
            return (ckeyLength > 0)
                       ? ((operation == AuthCodeMethod.Decode)
                              ? SubBytes(source, 0, ckeyLength)
                              : RandomBytes(ckeyLength))
                       : new byte[0];
        }


        /// <summary>
        /// UCenter编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string AuthCodeDecode(string str)
        {
            return authCode(str, AuthCodeMethod.Decode, UcConfig.UcKey);
        }

        /// <summary>
        /// UCenter编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="key">UC KEY</param>
        /// <returns></returns>
        public static string AuthCodeDecode(string str, string key)
        {
            return authCode(str, AuthCodeMethod.Decode, key);
        }

        /// <summary>
        /// UCenter编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="key">UC KEY</param>
        /// <param name="expiry">过期时间 0代表永不过期</param>
        /// <returns></returns>
        public static string AuthCodeDecode(string str, string key, int expiry)
        {
            return authCode(str, AuthCodeMethod.Decode, key, expiry);
        }


        /// <summary>
        /// UCenter解码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string AuthCodeEncode(string str)
        {
            return authCode(str, AuthCodeMethod.Encode, UcConfig.UcKey);
        }

        /// <summary>
        /// UCenter解码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="key">UC KEY</param>
        /// <returns></returns>
        public static string AuthCodeEncode(string str, string key)
        {
            return authCode(str, AuthCodeMethod.Encode, key);
        }

        /// <summary>
        /// UCenter解码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="key">UC KEY</param>
        /// <param name="expiry">过期时间 0代表永不过期</param>
        /// <returns></returns>
        public static string AuthCodeEncode(string str, string key, int expiry)
        {
            return authCode(str, AuthCodeMethod.Encode, key, expiry);
        }

        #endregion

        #region 常用函数

        /// <summary>
        /// 得到 UserAgent 字符串
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent()
        {
            return "Mozilla/4.0(compatible;MSIE6.0;)";
            //var a = HttpContext.Current.Request.ServerVariables["Http_User_Agent"];
            //var b = HttpContext.Current.Request.UserAgent;
            //return (HttpContext.Current != null) ? HttpContext.Current.Request.UserAgent : "Mozilla/4.0(compatible;MSIE6.0;)";
        }

        /// <summary>
        /// Byte数组转字符串
        /// </summary>
        /// <param name="b">数组</param>
        /// <returns></returns>
        public static string BytesToString(byte[] b)
        {
            return new string(Encode.GetChars(b));
        }

        /// <summary>
        /// 计算Md5
        /// </summary>
        /// <param name="b">byte数组</param>
        /// <returns>计算好的字符串</returns>
        public static byte[] Md5(byte[] b)
        {
            var cryptHandler = new MD5CryptoServiceProvider();
            byte[] hash = cryptHandler.ComputeHash(b);
            string ret = "";
            foreach (var a in hash)
            {
                if (a < 16)
                {
                    ret += "0" + a.ToString("x");
                }
                else
                {
                    ret += a.ToString("x");
                }
            }
            return Encode.GetBytes(ret);
        }

        /// <summary>
        /// 计算Md5
        /// </summary>
        /// <param name="str">byte数组</param>
        /// <returns>计算好的字符串</returns>
        public static string Md5(string str)
        {
            var cryptHandler = new MD5CryptoServiceProvider();
            byte[] hash = cryptHandler.ComputeHash(Encode.GetBytes(str));
            string ret = "";
            foreach (var a in hash)
            {
                if (a < 16)
                {
                    ret += "0" + a.ToString("x");
                }
                else
                {
                    ret += a.ToString("x");
                }
            }
            return ret;
        }

        /// <summary>
        /// Byte数组相加
        /// </summary>
        /// <param name="bytes">数组</param>
        /// <returns></returns>
        public static byte[] AddBytes(params byte[][] bytes)
        {
            int index = 0;
            int length = bytes.Sum(b => b.Length);
            var result = new byte[length];

            foreach (var bs in bytes)
            {
                foreach (var b in bs)
                {
                    result[index++] = b;
                }
            }
            return result;
        }

        /// <summary>
        /// Byte数组分割
        /// </summary>
        /// <param name="b">数组</param>
        /// <param name="start">开始</param>
        /// <param name="length">结束</param>
        /// <returns></returns>
        public static byte[] SubBytes(byte[] b, int start, int length = int.MaxValue)
        {
            if (start >= b.Length) return new byte[0];
            if (start < 0) start = 0;
            if (length < 0) length = 0;
            if (length > b.Length || start + length > b.Length) length = b.Length - start;
            var result = new byte[length];
            int index = 0;
            for (int k = start; k < start + length; k++)
            {
                result[index++] = b[k];
            }
            return result;
        }


        /// <summary>
        /// 计算Php格式的当前时间
        /// </summary>
        /// <returns>Php格式的时间</returns>
        public static long PhpTimeNow()
        {
            return DateTimeToPhpTime(DateTime.UtcNow);
        }

        /// <summary>
        /// PhpTime转DataTime
        /// </summary>
        /// <returns></returns>
        public static DateTime PhpTimeToDateTime(long time)
        {
            var timeStamp = new DateTime(1970, 1, 1);
            long t = (time + 8*60*60)*10000000 + timeStamp.Ticks;
            return new DateTime(t);
        }

        /// <summary>
        /// DataTime转PhpTime
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns></returns>
        public static long DateTimeToPhpTime(DateTime datetime)
        {
            var timeStamp = new DateTime(1970, 1, 1);
            return (datetime.Ticks - timeStamp.Ticks)/10000000;
        }


        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="lens">长度</param>
        /// <returns></returns>
        public static byte[] RandomBytes(int lens)
        {
            var chArray = new[]
                              {
                                  'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
                                  'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
                                  'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                                  'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
                              };
            int length = chArray.Length;
            var result = new byte[lens];
            var random = new Random();
            for (int i = 0; i < lens; i++)
            {
                result[i] = (byte) chArray[random.Next(length)];
            }
            return result;
        }


        /// <summary>
        /// PhpUrl编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string PhpUrlEncode(string str)
        {
            string result = string.Empty;
            const string keys = "_-.1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < str.Length; i++)
            {
                string str4 = str.Substring(i, 1);
                if (keys.Contains(str4))
                {
                    result = result + str4;
                }
                else
                {
                    result = Encode.GetBytes(str4).Aggregate(result, (current, n) => current + "%" + n.ToString("X"));
                }
            }
            return result;
        }

        #endregion
    }


    /// <summary>
    /// 操作类型
    /// </summary>
    internal enum AuthCodeMethod
    {
        Encode,
        Decode,
    }
}