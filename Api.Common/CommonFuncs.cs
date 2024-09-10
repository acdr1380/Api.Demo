using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Common
{
    public static class CommonFuncs
    {
        /// <summary>
        /// 生成唯一键
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        { 
         
            Guid guid = Guid.NewGuid();

            /**
             * 这里可以加其他的逻辑
             */

            return guid.ToString();
        }


        /// <summary>
        /// 判断泛型是否有指定属性，并且属性是否为空
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool IsPropertyNullOrEmpty<T>(T model, string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            // 判断是否存在属性
            if (property == null)
            {
                return true;
            }

            var value = property.GetValue(model);

            // 对于引用类型（包括可空引用类型）
            if (value == null)
            {
                return true;
            }

            // 对于值类型，需根据具体类型判断其是否为默认值
            Type valueType = property.PropertyType;
            if (valueType.IsValueType && value.Equals(Activator.CreateInstance(valueType)))
            {
                return true;
            }

            return false;
        }

    }
}
