using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Final_routing_distance_vector
{
    public partial class thenewform : Form
    {
       static public  ArrayList nodes;
       static public int num_nodes;
       static public int nodeindex;
       static public int [,]cost_matrix=new int[25,25];
       static public TabControl dynamicTabControl = new TabControl();
       public TabPage id0 = null;
       public TabPage id1 = null;
       public TabPage id2 = null;
       public TabPage id3 = null;
       public TabPage id4 = null;
       public TabPage id5 = null;
       public TabPage id6 = null;
       public TabPage id7 = null;
        
       public ArrayList IntefaceForNodes
       {
           get
           {
               return nodes;
           }
           set
           {
               nodes= value;
           }
       }
       public int IntefaceForNum_Node
       {
           get
           {
               return num_nodes;
           }
           set
           {
               num_nodes = value;
           }
       }
       
       public int IntefaceForNode_Index
       {
           get
           {
               return nodeindex;
           }
           set
           {
               nodeindex = value;
           }
       }
        public thenewform()
        {
            InitializeComponent();
            
            
        }
        

        private void thenewform_Load(object sender, EventArgs e)
        {
            if (num_nodes != 0)
            {
                    dynamicTabControl.Name = "DynamicTabControl";
                    dynamicTabControl.BackColor = Color.White;
                    dynamicTabControl.ForeColor = Color.Black;
                    dynamicTabControl.Font = new Font("Georgia", 16);
                    dynamicTabControl.Width = 600;
                    dynamicTabControl.Height = 404;
                    Controls.Add(dynamicTabControl);
                    

                    for (int i = 0; i < num_nodes; i++)
                    {
                        if (i == 0)
                        {
                            id0 = new TabPage();
                            id0.Name = "Node" + i; ;
                            id0.Text = "Node " + i;
                            id0.BackColor = Color.MistyRose;
                            id0.ForeColor = Color.White;
                            id0.Font = new Font("Verdana", 9);
                            id0.Width = 663;
                            id0.Height = 404;
                            dynamicTabControl.TabPages.Add(id0);
                        }
                        if (i == 1)
                        {
                            id1 = new TabPage();
                            id1.Name = "Node" + i; ;
                            id1.Text = "Node " + i;
                            id1.BackColor = Color.MistyRose;
                            id1.ForeColor = Color.White;
                            id1.Font = new Font("Verdana", 9);
                            id1.Width = 663;
                            id1.Height = 404;
                            dynamicTabControl.TabPages.Add(id1);
                        }
                        if (i == 2)
                        {
                            id2 = new TabPage();
                            id2.Name = "Node" + i; ;
                            id2.Text = "Node " + i;
                            id2.BackColor = Color.MistyRose;
                            id2.ForeColor = Color.White;
                            id2.Font = new Font("Verdana", 9);
                            id2.Width = 663;
                            id2.Height = 404;
                            dynamicTabControl.TabPages.Add(id2);
                        }
                        if (i == 3)
                        {
                            id3 = new TabPage();
                            id3.Name = "Node" + i; ;
                            id3.Text = "Node " + i;
                            id3.BackColor = Color.MistyRose;
                            id3.ForeColor = Color.White;
                            id3.Font = new Font("Verdana", 9);
                            id3.Width = 663;
                            id3.Height = 404;
                            dynamicTabControl.TabPages.Add(id3);
                        }
                        if (i == 4)
                        {
                            id4 = new TabPage();
                            id4.Name = "Node" + i; ;
                            id4.Text = "Node " + i;
                            id4.BackColor = Color.MistyRose;
                            id4.ForeColor = Color.White;
                            id4.Font = new Font("Verdana", 9);
                            id4.Width = 663;
                            id4.Height = 404;
                            dynamicTabControl.TabPages.Add(id4);
                        }
                        if (i == 5)
                        {
                            id5 = new TabPage();
                            id5.Name = "Node" + i; ;
                            id5.Text = "Node " + i;
                            id5.BackColor = Color.MistyRose;
                            id5.ForeColor = Color.White;
                            id5.Font = new Font("Verdana", 9);
                            id5.Width = 663;
                            id5.Height = 404;
                            dynamicTabControl.TabPages.Add(id5);
                        }
                        if (i == 6)
                        {
                            id6 = new TabPage();
                            id6.Name = "Node" + i; ;
                            id6.Text = "Node " + i;
                            id6.BackColor = Color.MistyRose;
                            id6.ForeColor = Color.White;
                            id6.Font = new Font("Verdana", 9);
                            id6.Width = 663;
                            id6.Height = 404;
                            dynamicTabControl.TabPages.Add(id6);
                        }
                        if (i == 7)
                        {
                            id7 = new TabPage();
                            id7.Name = "Node" + i; ;
                            id7.Text = "Node " + i;
                            id7.BackColor = Color.MistyRose;
                            id7.ForeColor = Color.White;
                            id7.Font = new Font("Verdana", 9);
                            id7.Width = 663;
                            id7.Height = 404;
                            dynamicTabControl.TabPages.Add(id7);
                        }

                    }

                        for (int k = 0; k < num_nodes; k++)
                        {

                            Label x, y;
                            int y1 = 40;
                            int x2 = 120;
                            y = new Label();
                            y.Location = new Point(x2, 10);
                            y.Name = "Labely";
                            y.Text = "Cost";
                            y.Font = new Font("Verdana", 12);
                            y.ForeColor = Color.RosyBrown;
                            if (k == 0)
                                id0.Controls.Add(y);
                            //dynamicTabControl.Controls.Add(y);
                            if (k == 1)
                                id1.Controls.Add(y);
                            if (k == 2)
                                id2.Controls.Add(y);
                            if (k == 3)
                                id3.Controls.Add(y);
                            if (k == 4)
                                id4.Controls.Add(y);
                            if (k == 5)
                                id5.Controls.Add(y);
                            if (k == 6)
                                id6.Controls.Add(y);
                            if (k == 7)
                                id5.Controls.Add(y);
                            y.Visible = true;
                            y = new Label();
                            y.Location = new Point(x2 + 100, 10);
                            y.Name = "Label12";
                            y.Text = "Next Hop";
                            y.Font = new Font("Verdana",12);
                            y.ForeColor = Color.RosyBrown;
                            if (k == 0)
                                id0.Controls.Add(y);
                            
                            if (k == 1)
                                id1.Controls.Add(y);
                            if (k == 2)
                                id2.Controls.Add(y);
                            if (k == 3)
                                id3.Controls.Add(y);
                            if (k == 4)
                                id4.Controls.Add(y);
                            if (k == 5)
                                id5.Controls.Add(y);
                            if (k == 6)
                                id6.Controls.Add(y);
                            if (k == 7)
                                id7.Controls.Add(y);
                            y.Visible = true;

                            for (int i = 0; i < num_nodes; i++)
                            {
                                x = new Label();
                                x.Location = new Point(20, y1);
                                x.Name = "Label" + i.ToString();
                                x.Text = i.ToString();
                                x.Font = new Font("Verdana", 12);
                                x.ForeColor = Color.RosyBrown;
                                if (k == 0)
                                    id0.Controls.Add(x);
                                //dynamicTabControl.Controls.Add(y);
                                if (k == 1)
                                    id1.Controls.Add(x);
                                if (k == 2)
                                    id2.Controls.Add(x);
                                if (k == 3)
                                    id3.Controls.Add(x);
                                if (k == 4)
                                    id4.Controls.Add(x);
                                if (k == 5)
                                    id5.Controls.Add(x);
                                if (k == 6)
                                    id6.Controls.Add(x);
                                if (k == 7)
                                    id7.Controls.Add(x);
                                x.Visible = true;

                                y1 += 50;
                            }

                            y1 = 40;
                            x2 = 120;

                            for (int j = 0; j < num_nodes; j++)
                            {
                                int[] cost = new int[8];
                                int[] nexthop = new int[8];

                                Node n = (Node)nodes[k];
                                cost = n.cost;
                                nexthop = n.nexthop;
                                Label z = new Label();
                                z.Font = new Font("Verdana", 12);
                                z.ForeColor = Color.Black;
                                z.Location = new Point(x2, y1);
                                z.Text = cost[j].ToString();
                                Label nh = new Label();
                                nh.Font = new Font("Verdana", 12);
                                nh.ForeColor = Color.Black;
                                nh.Location = new Point(x2+100, y1);
                                nh.Text =nexthop[j].ToString();
                                if (k == 0)
                                {
                                    id0.Controls.Add(z);
                                    id0.Controls.Add(nh);
                                }
                                //dynamicTabControl.Controls.Add(y);
                                if (k == 1)
                                {
                                    id1.Controls.Add(z);
                                    id1.Controls.Add(nh);
                                }
                                if (k == 2)
                                {
                                    id2.Controls.Add(z);
                                    id2.Controls.Add(nh);
                                }
                                if (k == 3)
                                {
                                    id3.Controls.Add(z);
                                    id3.Controls.Add(nh);
                                }
                                if (k == 4)
                                {
                                    id4.Controls.Add(z);
                                    id4.Controls.Add(nh);
                                }
                                if (k == 5)
                                {
                                    id5.Controls.Add(z);
                                    id5.Controls.Add(nh);
                                }
                                if (k == 6)
                                {
                                    id6.Controls.Add(z);
                                    id6.Controls.Add(nh);
                                }
                                if (k == 7)
                                {
                                    id7.Controls.Add(z);
                                    id7.Controls.Add(nh);
                                }
                                z.Visible = true;
                                y1 += 50;

                            }
                        }
                        
                
                    }
            
            
            
            
            
        }

        
    }
}
