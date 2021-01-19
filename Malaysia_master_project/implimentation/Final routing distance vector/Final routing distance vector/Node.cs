using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Final_routing_distance_vector
{
    class Node
    {
        public int id;
        public Point p;
        public int[] cost ;
        public int[] nexthop;
        public string neighbour="";
        Queue q;
        public Node()
        {
        }
        public Node(int id,Point p1)
        {
            this.id = id;
            p = p1;
            q = new Queue();
            cost=new int[8];
            nexthop=new int[8];
        }

        public Node(int ID, int[] hops, string neigh, string neighcost)
        {
            id = ID;
            string[] array1 = neigh.Split(',');
            string[] array2 = neighcost.Split(',');
            for (int i = 0; i < array1.Length; i++)
            {
                int x = int.Parse(array1[i]);
                int y = int.Parse(array2[i]);
                cost[x] = y;
            }
            for (int j = 0; j < cost.Length; j++)
                cost[j] = 999;
            
        }
    }
}
