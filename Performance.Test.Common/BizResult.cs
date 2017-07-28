using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Performance.Test.Common
{
    /// <summary>
    /// 业务返回类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class BizResult<T>
    {
        public BizResult(T returnObj, int businessCode)
        {
            this.ReturnObj = returnObj;
            this.BusinessCode = businessCode;
        }

        public BizResult(T returnObj, int businessCode, Pager pager)
        {
            this.ReturnObj = returnObj;
            this.BusinessCode = businessCode;
            this.Pager = pager;
        }

        public BizResult(T returnObj, int businessCode, string businessMessage)
        {
            this.ReturnObj = returnObj;
            this.BusinessCode = businessCode;
            this.BusinessMessage = businessMessage;
        }

        public BizResult(T returnObj, int businessCode, string businessMessage, Pager pager)
        {
            this.ReturnObj = returnObj;
            this.BusinessCode = businessCode;
            this.BusinessMessage = businessMessage;
            this.Pager = pager;
        }

        public BizResult(T returnObj)
        {
            this.ReturnObj = returnObj;
        }

        public BizResult(T returnObj, Pager pager)
        {
            this.ReturnObj = returnObj;
            this.Pager = pager;
        }

        public BizResult(EnumSystemCode sysCode)
        {
            this.SysCode = sysCode;
            this.SysMessage = this.SysCode.ToDescription();
        }

        public BizResult(EnumSystemCode sysCode, string sysMessage)
        {
            this.SysCode = sysCode;
            this.SysMessage = sysMessage;
        }

        public BizResult()
        {
        }

        /// <summary>
        /// 系统消息
        /// </summary>
        [DataMember]
        public string SysMessage { get; set; } = EnumSystemCode.Success.ToDescription();

        /// <summary>
        /// 业务代码
        /// </summary>
        public EnumSystemCode SysCode { get; set; } = EnumSystemCode.Success;

        /// <summary>
        /// 系统调用是否成功，如失败则参考 syscode 以及 sysmessage 查看失败信息
        /// </summary>
        public bool IsSuccess => this.SysCode == EnumSystemCode.Success;

        /// <summary>
        /// 业务消息
        /// </summary>
        [DataMember]
        public string BusinessMessage { get; set; }

        /// <summary>
        /// 业务代码
        /// </summary>
        [DataMember]
        public int BusinessCode { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        [DataMember]
        public T ReturnObj { get; set; }

        [DataMember]
        public Pager Pager { get; set; }

        public override string ToString()
        {
            return $"SysCode:{SysCode},SysMessage:{SysMessage},BusinessCode:{BusinessCode},BusinessMessage{BusinessMessage},ReturnObj:{ReturnObj}";
        }
    }

    /// <summary>
    /// 业务CODE
    /// </summary>
    public enum EnumSystemCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1000,

        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        Failed,

        /// <summary>
        /// 程序出错
        /// </summary>
        [Description("程序出错")]
        Exception,

        /// <summary>
        /// 服务器通信错误
        /// </summary>
        [Description("服务器通信错误")]
        CommunicationError,

        /// <summary>
        /// 服务器通信超时
        /// </summary>
        [Description("服务器通信超时")]
        Timeout,
    }

    public class Pager
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        public int Skip => (PageIndex - 1) * PageSize;

        [DataMember]
        public int TotalCount { get; set; }

        public int TotalPage
        {
            get
            {
                if (PageSize == 0)
                {
                    PageSize = 10;
                }
                var totalPage = TotalCount / PageSize;
                if (TotalCount % PageSize != 0) totalPage++;

                return totalPage;
            }
        }

        public Pager()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        public Pager(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public Pager(int pageIndex, int pageSize, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }

    public static class EnumExtensions
    {
        public static string ToDescription(this System.Enum enumeration)
        {
            var enumType = enumeration.GetType();
            string name = System.Enum.GetName(enumType, enumeration);
            if (name == null) return null;

            var fieldInfo = enumType.GetField(name);
            if (fieldInfo == null) return null;

            var attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
            return attr?.Description;
        }

        public static int ToInt(this System.Enum enumeration)
        {
            return Convert.ToInt32(enumeration);
        }

        public static T ToEnum<T>(this string val)
        {
            return (T)Enum.Parse(typeof(T), val);
        }
    }

    public static class JsonSerializerExtenstions
    {
        /// <summary>
        /// 采用Newtonsoft.Json封装
        /// </summary>
        public static string ToJson(this object item)
        {
            return JsonConvert.SerializeObject(item);
        }

        /// <summary>
        /// 采用Newtonsoft.Json封装
        /// </summary>
        public static T FromJsonTo<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
