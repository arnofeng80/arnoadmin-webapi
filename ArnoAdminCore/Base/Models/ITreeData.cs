using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Dto
{
    public interface ITreeData<T>
    {
        public long Id { get; }
        public long ParentId { get; }
        public String Label { get; }
        List<T> Children { get; set; }
    }
}
