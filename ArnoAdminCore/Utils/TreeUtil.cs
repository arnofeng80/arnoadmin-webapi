using ArnoAdminCore.SystemManage.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArnoAdminCore.Utils
{
    public class TreeUtil
    {
        public static void BuildTree<T>(IEnumerable<T> list, IEnumerable<T> parentList) where T : ITreeData<T>
        {
            foreach(T item in parentList)
            {
                item.Children = list.Where(x => x.ParentId == item.Id).ToList();
                if (item.Children.Count > 0)
                {
                    BuildTree(list, item.Children);
                }
            }
        }
    }
}
