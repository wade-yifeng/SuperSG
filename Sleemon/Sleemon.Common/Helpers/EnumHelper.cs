namespace Sleemon.Common
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public class EnumHelper
    {
        public static string GetEnumDescription(Enum enumItem)
        {
            FieldInfo fi = enumItem.GetType().GetField(enumItem.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return enumItem.ToString();
            }

        }
    }
}
