using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto.List
{
    public class DepartmentList: Department, ITreeData<DepartmentList>
    {
        public string Label { get => this.DeptName; }
        public List<DepartmentList> Children { get; set; } = new List<DepartmentList>();
    }
}
