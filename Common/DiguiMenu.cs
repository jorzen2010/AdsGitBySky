using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    class DiguiMenu
    {
        public void LoopToAppendChildren<T>(List<T> all, T curItem, string parentIdName = "ParentId", string idName = "Id", string childrenName = "children")
        {
            var subItems = all.Where(ee => ee.GetType().GetProperty(parentIdName).GetValue(ee, null).ToString() == curItem.GetType().GetProperty(idName).GetValue(curItem, null).ToString()).ToList(); //新闻1

            curItem.GetType().GetField(childrenName).SetValue(curItem, subItems);
            foreach (var subItem in subItems)
            {
                LoopToAppendChildren(all, subItem);//新闻1.1
            }
        }
    }
}
