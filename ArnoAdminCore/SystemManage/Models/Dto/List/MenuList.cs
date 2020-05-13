using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto.List
{
    public class MenuList : Menu, ITreeData<MenuList>
    {
        public string Label { get => this.MenuName; }
        public List<MenuList> Children { get; set; } = new List<MenuList>();
    }
}
