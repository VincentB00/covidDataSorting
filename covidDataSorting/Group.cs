using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidDataSorting
{
    public class Group
    {
        public String name;
        public List<int> rowOrderList;
        public List<Group> childGroupList = null;
        public Group(String name, List<int> rowOrderList)
        {
            this.name = name;
            copyOrderList(rowOrderList);
        }
        public Group(String name)
        {
            this.name = name;
            this.rowOrderList = new List<int>();
        }
        public void addChildGroup(String name)
        {
            if(childGroupList == null)
            {
                childGroupList = new List<Group>();
                childGroupList.Add(new Group(name));
            }
            else
                childGroupList.Add(new Group(name));
        }
        public void copyOrderList(List<int> orderList)
        {
            this.rowOrderList = new List<int>();
            foreach (int index in orderList)
                this.rowOrderList.Add(index);
        }
        public Group search(String name)
        {
            if (this.name.CompareTo(name) == 0)
                return this;
            else if (childGroupList != null)
            {
                Group resultGroup = null;
                foreach(Group group in childGroupList)
                {
                    resultGroup = group.search(name);
                    if (resultGroup != null)
                        return resultGroup;
                }
                return resultGroup;
            }
            else
                return null;
        }

    }
}
