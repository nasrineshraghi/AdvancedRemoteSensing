using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace Final_routing_distance_vector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        
        static int infinity=999;
        public static Topology t;
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            t = new Topology();
            Application.Run(new Form1());
        }
        public class packet
    {
        int sourceid;
        int destid;
        int sendtime;
        int destime;
        public packet(int x,int y)
        {
            sourceid=x;
            destid=y;
        }
    }
            
        }
        
    }

