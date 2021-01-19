using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Final_routing_distance_vector.Properties;
using System.Collections;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;

namespace Final_routing_distance_vector
{
    public partial class Form1 : Form
    {
        ArrayList nodes = new ArrayList();
        ArrayList points = new ArrayList();
        bool butAddclick = false;
        bool butAddlink = false;
        bool butenter = false;
        bool click = false;
        int count = 0;
        ArrayList neighbours = new ArrayList();
        bool costbut = false;
        ArrayList b = new ArrayList(2);
        int[,] cost = new int[25, 25];
        static int weight = -1;
        static Label l1 = new Label();
        static Topology t = new Topology();
        static double smallest;
        static int event_type;
        static double flooding;
        static int flodding_iat = 30;
        static double clocktime = 0;
        static ArrayList winform = new ArrayList();
        static bool sendbutt = false;
        static thenewform thenewform1 = new thenewform();
        static string weightOutput = "";
        static int x, y;

        public Form1()
        {
            InitializeComponent();


        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = this.panel2.CreateGraphics();
            if (butAddclick)
            {
                Pen p = new Pen(Color.Yellow, 2);
                Rectangle r = new Rectangle(e.X, e.Y, 30, 30);

                g.DrawRectangle(p, r);
                Label l = new Label();
                nodes.Add(r);
                // g.DrawRectangle(p, e.X, e.Y, 50, 50);
                l.Location = new Point(e.X + 10, e.Y - 25);
                l.Text = count.ToString();
                l.Visible = true;
                l.Parent = panel2;
                Node newnode = new Node(count, new Point(e.X, e.Y));
                count++;
                t.AddNode(newnode);
                outputtxt.Text += "A node has been created with id " + (count - 1) + "\r\n";

                butAddclick = false;
            }

            if (butAddlink)
            {

                points.Add(new Point(e.X, e.Y));
                if (points.Count == 2)
                {
                    Pen pen = new Pen(Color.Red, 1);
                    g.DrawLine(pen, (Point)points[0], (Point)points[1]);
                    neighbours.Add((Point)points[0]);
                    neighbours.Add((Point)points[1]);
                        x = FindeNode((Point)points[0]);
                    y = FindeNode((Point)points[1]);
                        
                    Update(x, y);
                    outputtxt.Text += "A link between nodes " + x + " - " + y + " has been created \r\n";
                    butAddlink = false;
                    l1.Location = new Point(32, 10);
                    l1.Size = new System.Drawing.Size(180, 20);
                    l1.Text = "Enter the cost of " + x + " - " + y + " :";
                    textBox1.Visible = true;
                    textBox1.Enabled = true;
                    l1.Visible = true;
                    panel3.Controls.Add(l1);
                    panel3.Controls.Add(textBox1);

                    costbut = false;
                    points = new ArrayList(2);

                }

            }

        }

        public int FindeNode(Point p1)
        {
            ArrayList a = t.topologynode;
            Node node = new Node();
            for (int i = 0; i < a.Count; i++)
            {
                node = (Node)a[i];
                if ((p1.X >= node.p.X && p1.X <= node.p.X + 30) && (p1.Y >= node.p.Y && p1.Y <= node.p.Y + 30))
                    return node.id;
            }
            return -1;

        }
        public void AllocateWeight(int node1, int node2, int cost)
        {
            textBox2.Font = new Font("Verdana", 10);

            textBox2.Text += "Cost between " + node1.ToString() + "  &  " + node2.ToString() + " is : " + cost.ToString() + "\r\n";

        }
        public void Update(int x, int y)
        {
            ArrayList a = t.topologynode;
            for (int i = 0; i < a.Count; i++)
            {
                Node newnode = new Node();
                newnode = (Node)a[i];
                if (newnode.id == x)
                {
                    newnode.nexthop[y] = x;
                    if (newnode.neighbour == "")
                        newnode.neighbour = y.ToString();
                    else
                    {
                        //string[]b=new string[20];
                        string[] b = newnode.neighbour.Split(',');
                        bool c = false;
                        for (int j = 0; j < b.Length; j++)
                        {
                            if (b[j] == y.ToString())
                                c = true;
                        }
                        if (!c)
                            newnode.neighbour = newnode.neighbour + "," + y.ToString();
                    }

                }
                if (newnode.id == y)
                {
                    newnode.nexthop[x] = y;
                    if (newnode.neighbour == "")
                        newnode.neighbour = x.ToString();
                    else
                    {
                        string[] b = new string[20];
                        b = newnode.neighbour.Split(',');
                        bool c = false;
                        for (int j = 0; j < b.Length; j++)
                        {
                            if (b[j] == y.ToString())
                                c = true;
                        }
                        if (!c)
                            newnode.neighbour = newnode.neighbour + "," + x.ToString();
                    }

                }
            }
        }

        private void addnode_Click(object sender, EventArgs e)
        {
            butAddclick = true;
        }

        private void addlink_Click(object sender, EventArgs e)
        {
            butAddlink = true;
        }


        private void button9_Click(object sender, EventArgs e)
        {
            costbut = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            butenter = true;
            if (e.KeyCode == Keys.Enter)
            {
                weight = Int32.Parse(textBox1.Text);
                ArrayList a = t.topologynode;
                Node node = new Node();
                for (int i = 0; i < a.Count; i++)
                {
                    node = (Node)a[i];
                    if (node.id == x && node.cost[i] < weight)
                    {
                        node.cost[y] = weight;
                        node.nexthop[y] = y;
                    }

                    if (node.id == y && node.cost[i] < weight)
                    {
                        node.cost[x] = weight;
                        node.nexthop[x] = x;
                    }
                }

                AllocateWeight(x, y, weight);
                outputtxt.Text += "The cost between nodes " + x + " and " + y + " is " + weight + "\r\n";
                textBox1.Clear();
                l1.Visible = false;
                textBox1.Visible = false;
                b = new ArrayList(2);

            }

        }


        public static void Cost()
        {
            Node newnode = new Node();
            for (int i = 0; i < t.numberOfNodes; i++)
            {
                newnode = (Node)t.topologynode[i];
                for (int j = 0; j < t.numberOfNodes; j++)
                {
                    if (i == j)
                        newnode.cost[j] = 0;
                    else
                        if (i != j)
                        {
                            if (newnode.cost[j] == 0)
                                newnode.cost[j] = 999;
                        }
                }
            }
        }

        public static void Flood()
        {
            for (int i = 0; i < t.numberOfNodes; i++)
                ForwardTable(i);
            flooding = clocktime + flodding_iat;
        }
        public static void ForwardTable(int identification)
        {
            Node newnode = new Node();
            ArrayList a = t.topologynode;
            string[] x;
            for (int i = 0; i < a.Count; i++)
            {
                if (((Node)a[i]).id == identification)
                {
                    newnode = (Node)a[i];

                }
            }
            x = newnode.neighbour.Split(',');
            for (int j = 0; j < x.Length; j++)
            {
                Receieve(newnode.id, int.Parse(x[j]), newnode.cost);
            }

        }
        public static void Receieve(int sourceid, int destid, int[] costsource)
        {
            ArrayList a = t.topologynode;
            Node newnode = new Node();
            for (int j = 0; j < a.Count; j++)
                if (((Node)a[j]).id == destid)
                    newnode = (Node)a[j];

            for (int i = 0; i < costsource.Length; i++)
            {
                if ((i != newnode.id))
                {
                    if (costsource[i] + newnode.cost[sourceid] < newnode.cost[i])
                    {
                        newnode.cost[i] = costsource[i] + newnode.cost[sourceid];
                        newnode.nexthop[i] = sourceid;
                    }

                }
            }


            thenewform1.IntefaceForNode_Index = destid;
            thenewform1.IntefaceForNodes = t.topologynode;
            thenewform1.IntefaceForNum_Node = t.numberOfNodes;

            thenewform1.Invalidate();



        }
        private void button3_Click(object sender, EventArgs e)
        {
            int x = t.numberOfNodes;
            Cost();
            sendbutt = true;
            thenewform1.IntefaceForNodes = t.topologynode;
            thenewform1.IntefaceForNum_Node = t.numberOfNodes;


            winform.Add(thenewform1);

            while (clocktime <= 300)
            {
                Scheduler();
                Clocktime_Update();
                Event_Update();
            }
            thenewform1.Show();

        }
        public static void Scheduler()
        {
            smallest = 1.0e+30;
            if (flooding < smallest)
            {
                smallest = flooding;
                event_type = 0;
            }

        }
        public void Clocktime_Update()
        {
            clocktime = smallest;
        }
        public static void Event_Update()
        {
            if (event_type == 0)
                Flood();
        }
        private void MoveRect(Graphics G, Pen P, Rectangle R, Point source, Point destination)
        {
            //Erase rectangle
            Pen eraser = new Pen(this.BackColor);
            G.DrawRectangle(eraser, R);
            //Create a matrix and translate it.
            Matrix myMatrix = new Matrix();
            int Xdist = (int)Math.Round(Math.Sqrt((Math.Pow((destination.X - source.X), 2)) + (Math.Pow((destination.Y - source.Y), 2))));
            for (int i = 0; i < Xdist; i++)
            {
                System.Threading.Thread.Sleep(10 + i); //slow down
                Xdist = i;
                if (destination.X - source.X > 0)
                    myMatrix.Translate(Xdist, 0);
                if (destination.X - source.X < 0)
                    myMatrix.Translate(-Xdist, 0);
                if (destination.Y - source.Y > 0)
                    myMatrix.Translate(0, Xdist);
                if (destination.Y - destination.X < 0)
                    myMatrix.Translate(0, -Xdist);
                //Draw the Points to the screen again after applying the
                //transform.
                G.DrawRectangle(eraser, R); //first erase the existing rectangle
                G.Transform = myMatrix;
                G.DrawRectangle(P, R);
            }

        }
        private void MovePoint()
        {

            //Move along the right side
            if (mPoint.X == mRect.Right)
            {

                //Only do it if we aren't at the bottom of the right edge
                if (mPoint.Y != mRect.Bottom)
                {
                    mPoint.Offset(0, 1);

                    return;
                }
            }
            //Move along the bottom side
            if (mPoint.Y == mRect.Bottom)
            {

                //Only do it if we aren't at the left side of the bottom edge
                if (mPoint.X != mRect.Left)
                {
                    mPoint.Offset(-1, 0);

                    return;
                }
            }


            //Move along the left side
            if (mPoint.X == mRect.Left)
            {

                //Only do it if we aren't at the top side of the left edge
                if (mPoint.Y != mRect.Top)
                {
                    mPoint.Offset(0, -1);
                }
            }


            //Move along the top side
            if (mPoint.Y == mRect.Top)
            {
                //Only do it if we aren't at the right side of the top edge
                if (mPoint.X != mRect.Right)
                {
                    mPoint.Offset(1, 0);
                }
            }
        }
        protected void TimerCB(object o)
        {

            //Move the rectangle
            MovePoint();

            //Redraw the form
            Invalidate();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Population TestPopulation = new Population(0, t.MaxNodeNum(), t.numberOfNodes * 3, t);
            string textouptut = TestPopulation.WriteNextGeneration();

            for (int i = 0; i < 10; i++)
            {
                TestPopulation.NextGeneration();
                textouptut += TestPopulation.WriteNextGeneration();
            }
            Form2 form = new Form2();
            form.IntefaceForOutput = textouptut;
            form.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }



    }
        
    }

