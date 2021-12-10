using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3
{
    internal class CPB
    {
        public List<Object> objects;
        public int supportCount;
        public Object itemPointer;
        public CPB()
        {
            objects = new List<Object>();
        }
        public CPB(int supCount)
        {
            objects = new List<Object>();
            this.supportCount = supCount;
        }

        public CPB combineWith(CPB other)
        {
            CPB temp = new CPB();
            temp.supportCount = other.supportCount < supportCount ? other.supportCount : supportCount;

            foreach(Object obj in objects)
                temp.objects.Add(obj);

            foreach(Object obj in other.objects)
                temp.objects.Add(obj);

            return temp;
        }

        public bool compareTo(CPB other)
        {
            if(other.objects.Count != objects.Count)
                return false;

            for(int count = 0; count < other.objects.Count; count++)
            {
                if(other.objects[count].ToString().CompareTo(objects[count]) != 0)
                    return false;
            }

            return true;
        }

        public string generatePattern()
        {
            string result = "{";

            for(int count = 0; count < objects.Count; count++)
            {
                if(count < objects.Count - 1)
                    result += objects[count].ToString() + ", ";
                else
                    result += objects[count].ToString();
            }

            return result + "}";
        }

        public override string ToString()
        {
            string result = "{";
            foreach (Object o in objects)
            {
                result += o.ToString() + ",";
            }
            result = result.Substring(0, result.Length - 1) + ": " + supportCount + "}";

            return result;
        }
    }
}
