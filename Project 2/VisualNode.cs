using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    class VisualNode
    {
        private String visualIDString;
        private readonly Task task;
        public int key;
        public VisualNode(List<Row> rowList)
        {
            key = rowList[0].id;
            task = Task.Run(() =>
            {
                visualIDString = "";
                int max = rowList.Count;
                for (int count = 0; count < max; count++)
                {
                    if (count == max - 1)
                    {
                        visualIDString += rowList[count].id;
                    }
                    else
                        visualIDString += rowList[count].id + "|";
                }
                visualIDString = "[" + visualIDString + "]";
            });
        }

        public override string ToString()
        {
            if (!task.IsCompleted)
                task.Wait();
            
            return visualIDString;
        }
    }
}
