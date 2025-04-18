using System;
using System.Collections;
using System.Collections.Generic;

namespace WalkingTec.Mvvm.Core.Extensions
{
    /// <summary>
    /// 枚举扩展函数
    /// </summary>
    public static class EnumExtension
    {
        #region 将枚举类型转化为下拉列表

        /// <summary>
        /// 将枚举类型转化为下拉列表
        /// </summary>
        /// <param name="self">枚举类型</param>
        /// <param name="value">value</param>
        /// <param name="pleaseSelect">pleaseSelect</param>
        /// <returns>下拉菜单数据列表</returns>
        public static List<ComboSelectListItem> ToListItems(this Type self, object value = null, bool pleaseSelect = false)
        {
            string[] names = null;
            Array values = null;
            //如果是枚举
            if (self.IsEnum)
            {
                names = Enum.GetNames(self);
                values = Enum.GetValues(self);
            }
            //如果是nullable的枚举
            if (self.IsGenericType && self.GenericTypeArguments[0].IsEnum)
            {
                names = Enum.GetNames(self.GenericTypeArguments[0]);
                values = Enum.GetValues(self.GenericTypeArguments[0]);
            }
            //生成下拉菜单数据
            List<ComboSelectListItem> rv = new List<ComboSelectListItem>();
            if (names != null)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    var name = names[i];
                    var newitem = new ComboSelectListItem { Text = PropertyHelper.GetEnumDisplayName(self, name), Value = values.GetValue(i).ToString() };
                    if (value is string)
                    {
                        if (value != null)
                        {
                            string v = value.ToString();
                            var vs = v.Split(',');
                            foreach (var item in vs)
                            {
                                if (item != null && (newitem.Value.ToString() == item.ToString()) || name == item.ToString())
                                {
                                    newitem.Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                    else if (value is IEnumerable it)
                    {
                        foreach (var item in it)
                        {
                            if (item != null && name == item.ToString())
                            {
                                newitem.Selected = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (value != null && name == value.ToString())
                        {
                            newitem.Selected = true;
                        }
                    }
                    rv.Add(newitem);
                }
            }
            if (pleaseSelect == true)
            {
                rv.Insert(0, new ComboSelectListItem { Text = CoreProgram._localizer?["Sys.PleaseSelect"], Value = "" });
            }
            return rv;
        }

        #endregion 将枚举类型转化为下拉列表
    }
}