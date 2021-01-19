using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Final_routing_distance_vector
{
    class Topology
    {
        public ArrayList topologynode;
        public int numberOfNodes = 0;
        public Topology()
        {
            topologynode = new ArrayList();
        }
       
        public void AddNode(Node newnode)
        {
            topologynode.Add(newnode);
            numberOfNodes++;
        }
        public void RemoveNode(Node newnode)
        {
            topologynode.Remove(newnode);
            numberOfNodes--;
        }
        public bool ValidPath(ArrayList array)
        {
            bool flag = true;
            ArrayList x = new ArrayList();
            for (int j = 0; j < array.Count; j = j + 3)
            {
                int a = (int)array[j];
                int b = (int)array[j + 1];
                int c = (int)array[j + 2];
               
                int result =  a * 4 + b * 2 + c* 1;
                x.Add(result);

            }
                for (int i = 1; i < x.Count; i++)
                {
                    bool b = Neighbours((int)x[i - 1], (int)x[i]);
                    if (b==false)
                    {
                        flag = false;
                        break;
                    }

                }
                if (flag == true)
                {
                    for (int k = 0; k < x.Count; k++)
                    {
                        int num = (int)x[k];
                        for (int m = k + 1; m < x.Count; m++)
                        {
                            if ((int)x[m] == num)
                            {
                                flag = false;
                                break;
                            }
                            
                        }
                        if (flag == false)
                            break;
                    }
                }
            return flag;
        }
        public bool Neighbours(int nodeid1,int nodeid2)
        {
         
            Node newnode = new Node();
            if (nodeid1 == nodeid2) return false;
            else
            {
                for (int j = 0; j < numberOfNodes; j++)
                {
                    newnode = (Node)topologynode[j];
                    if (newnode.id == nodeid1)
                        break;
                }
                string[] str = newnode.neighbour.Split(',');
                bool flag = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if ((int.Parse(str[i])) == nodeid2)
                    {
                        flag = true;
                        break;
                    }
                }
                return flag;
            }
        }
        public int Calculate2Cost(int nodeid1,int nodeid2)
        {
            Node newnode=new Node();
            int cost=0;
            for (int i = 0; i < topologynode.Count; i++)
            {
                newnode =(Node) topologynode[i];
                if (newnode.id == nodeid1)
                {
                    cost = newnode.cost[nodeid2];
                    break;
                }
            }
           
            return cost;
        }
        public int CalculateTotalCost(ArrayList array)
        {
            int totalcost = 0;
            ArrayList x = new ArrayList();
            for (int j = 0; j < array.Count; j = j + 3)
            {
                int a = (int)array[j];
                int b = (int)array[j + 1];
                int c = (int)array[j + 2];

                int result = a * 4 + b * 2 + c * 1;
                x.Add(result);

            }
            for (int i = 1; i < x.Count; i++)
            {
                totalcost += Calculate2Cost((int)x[i-1],(int)x[i]);
            }
            return totalcost;
        }
        public int MaxNodeNum()
        {
            return topologynode.Count - 1;
        }
    }
}
