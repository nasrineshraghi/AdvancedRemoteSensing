using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_routing_distance_vector
{
    class Queue
    {
        double[] queue;
        int maxsize;
        public int qsize = 0;
        public Queue()
        {
            maxsize = 100;
            queue = new double[100];
        }
        public void Add(double time)
        {
            if (!Full())
            {
                queue[qsize + 1] = time;
                qsize++;
            }
            else
            {
                changeqsize();
                queue[qsize + 1] = time;
                qsize++;
            }

        }
        public double Remove()
        {
            double x = queue[0];
            for (int i = 0; i < queue.Length - 1; i++)
            {
                queue[i] = queue[i + 1];
            }
            qsize--;
            return x;
        }
        public bool Empty()
        {
            if (qsize == 0)
                return true;
            else
                return false;
        }
        public void changeqsize()
        {
            double[] a = queue;
            queue = new double[maxsize + 100];
            for (int i = 0; i < a.Length; i++)
                queue[i] = a[i];
            maxsize += 100;
        }
        public bool Full()
        {
            if (qsize + 1 == maxsize)
                return true;
            else
                return false;
        }
    }
}
