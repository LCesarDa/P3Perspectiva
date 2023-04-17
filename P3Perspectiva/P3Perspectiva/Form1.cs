using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace P3Perspectiva
{
    public partial class Form1 : Form
    {
        public Canvas canvas;
        PictureBox pictureBox;
        TextBox path;
        NumericUpDown r, g, b;
        Button fill, load, rx, ry, rz, visi;
        TrackBar cd, cr, tamano;
        Scene scene = new Scene();
        Model cube;
        List<Model> models;
        Transform c;
        TreeView list;
        Instance actual;
        int w, h, a = 0;
        public static float v_H = 1f, v_W, c_H, c_W;
        public static float distance = 1;
        Boolean fll = false, ex = true, ye = true, ze = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            canvas.FastClear();
            a++;
            scene.instance[2].transform.X = a;
            scene.instance[3].transform.Y = a;
            scene.instance[4].transform.Z = a;
            scene.instance[1].transform.Position = new Vertex((float)(Math.Cos(scene.instance[2].transform.X)), (float)(Math.Sin(scene.instance[2].transform.X)), 0);
            scene.instance[1].transform.Z = a;
            c.ZPosition = -cd.Value;
            c.Y = -cr.Value;
            rotate();
            canvas.RenderScene(scene, fll, c);
            pictureBox.Invalidate();
        }

        public Form1()
        {
            InitializeComponent();
            models = new List<Model>();
            AddCube();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            w = Form1.ActiveForm.Width - 200;
            h = Form1.ActiveForm.Height - 150;
            c_W = w;
            c_H = h;
            v_W = (float)w / (float)h;
            canvas = new Canvas(new Size(w, h));
            AddControls();
            scene.instance.Add(new Instance(cube, new Transform(0.2f, new Vertex(0, 0, 0), new Vertex(-4, -2, 0))));
            actual = scene.instance[scene.instance.Count - 1];
            TreeNode node = new TreeNode("Instance" + (list.Nodes.Count));
            node.Tag = actual;
            list.Nodes.Add(node);
            scene.instance.Add(new Instance(cube, new Transform(0.5f, new Vertex(0, 45, 0), new Vertex(1.5f, 0, 0))));
            actual = scene.instance[scene.instance.Count - 1];
            node = new TreeNode("Instance" + (list.Nodes.Count));
            node.Tag = actual;
            list.Nodes.Add(node);
            scene.instance.Add(new Instance(cube, new Transform(0.5f, new Vertex(0, 0, 0), new Vertex(-1.5f, -2.4f, 0))));
            actual = scene.instance[scene.instance.Count - 1];
            node = new TreeNode("Instance" + (list.Nodes.Count));
            node.Tag = actual;
            list.Nodes.Add(node);
            scene.instance.Add(new Instance(cube, new Transform(0.5f, new Vertex(0, 0, 0), new Vertex(0, -2.4f, 0))));
            actual = scene.instance[scene.instance.Count - 1];
            node = new TreeNode("Instance" + (list.Nodes.Count));
            node.Tag = actual;
            list.Nodes.Add(node);
            scene.instance.Add(new Instance(cube, new Transform(0.5f, new Vertex(0, 0, 0), new Vertex(1.5f, -2.4f, 0))));
            actual = scene.instance[scene.instance.Count - 1];
            node = new TreeNode("Instance" + (list.Nodes.Count));
            node.Tag = actual;
            list.Nodes.Add(node);
            
            this.Controls.Add(pictureBox);
            this.Controls.Add(fill);
            this.Controls.Add(cd);
            this.Controls.Add(cr);
            this.Controls.Add(visi);
            this.Controls.Add(path);
            this.Controls.Add(load);
            this.Controls.Add(rx);
            this.Controls.Add(ry);
            this.Controls.Add(rz);
            this.Controls.Add(fill);
            this.Controls.Add(r);
            this.Controls.Add(g);
            this.Controls.Add(b);
            this.Controls.Add(list);

            c = new Transform(1, new Vertex(0, 0, 0), new Vertex(0, 0, 0));
            c.FOV = 90;
        }
        private void AddCube()
        {
            List<Vertex> ver = new List<Vertex>();
            List<Triangle> tri = new List<Triangle>();
            ver.Add(new Vertex(1, 1, 1, Color.Red));
            ver.Add(new Vertex(-1, 1, 1, Color.Green));
            ver.Add(new Vertex(-1, -1, 1, Color.Blue));
            ver.Add(new Vertex(1, -1, 1, Color.Yellow));
            ver.Add(new Vertex(1, 1, -1, Color.Purple));
            ver.Add(new Vertex(-1, 1, -1, Color.Cyan));
            ver.Add(new Vertex(-1, -1, -1, Color.Orange));
            ver.Add(new Vertex(1, -1, -1, Color.Pink));
            tri.Add(new Triangle(0, 1, 2, Color.Red));
            tri.Add(new Triangle(0, 2, 3, Color.Red));
            tri.Add(new Triangle(4, 0, 3, Color.Green));
            tri.Add(new Triangle(4, 3, 7, Color.Green));
            tri.Add(new Triangle(5, 4, 7, Color.Blue));
            tri.Add(new Triangle(5, 7, 6, Color.Blue));
            tri.Add(new Triangle(1, 5, 6, Color.Yellow));
            tri.Add(new Triangle(1, 6, 2, Color.Yellow));
            tri.Add(new Triangle(4, 5, 1, Color.Purple));
            tri.Add(new Triangle(4, 1, 0, Color.Purple));
            tri.Add(new Triangle(2, 6, 7, Color.Cyan));
            tri.Add(new Triangle(2, 7, 3, Color.Cyan));
            cube = new Model(ver, tri);
        }
        private void AddControls()
        {
            load = new Button
            {
                Location = new Point(10, 15),
                Text = "Load"
            };
            path = new TextBox
            {
                Location = new Point(15 + load.Width, 15),
            };
            pictureBox = new PictureBox
            {
                Image = canvas.bitmap,
                Size = new Size(w, h),
                Location = new Point(0, 50),
                BackColor = Color.Gray
            };
            rx = new Button
            {
                Location = new Point(w + 50, 80),
                Text = "Stop X",
            };
            ry = new Button
            {
                Location = new Point(w + 50, 110),
                Text = "Stop Y",
            };
            rz = new Button
            {
                Location = new Point(w + 50, 140),
                Text = "Stop Z",
            };
            fill = new Button
            {
                Location = new Point(w + 50, 50),
                Text = "Texture",
            };
            visi = new Button
            {
                Location = new Point(w + 50, 20),
                Text = "Visible",
            };
            list = new TreeView
            {
                Location = new Point(w + 50, 210),
                Height = 400,
            };
            cd = new TrackBar
            {
                Location = new Point(w + 5, 50),
                Orientation = Orientation.Vertical,
                Height = h,
                LargeChange = 1,
                Maximum = 50,
                Minimum = -50,
                Value = -7,
            };
            tamano = new TrackBar
            {
                Location = new Point(w + 25, 50),
                Orientation = Orientation.Vertical,
                Height = h,
                LargeChange = 1,
                Maximum = 5,
                Minimum = 0,
                Value = 1,
            };
            cr = new TrackBar
            {
                Location = new Point(0, h + 50),
                Width = w,
                Maximum = 180,
                LargeChange = 1,
                Minimum = -180,
                Value = 0,
            };
            fill.Click += FILL_Click;
            load.Click += LOAD_Click;
            list.AfterSelect += LIST_AfterSelect;
            rx.Click += RX_Click;
            ry.Click += RY_Click;
            rz.Click += RZ_Click;
        }
        private void FILL_Click(object sender, EventArgs e)
        {
            fll = !fll;
            if (fll)
            {
                fill.Text = "WireFrame";
            }
            else
            {
                fill.Text = "Texture";
            }
        }
        private void LOAD_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(path.Text), tex, f;
            int[] tri = new int[3];
            List<Vertex> v = new List<Vertex>();
            List<Triangle> t = new List<Triangle>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    if (lines[i][0] == 'v')
                    {
                        if (lines[i][1] == ' ')
                        {
                            tex = lines[i].Split(' ');
                            v.Add(new Vertex(float.Parse(tex[1]), float.Parse(tex[2]), float.Parse(tex[3])));
                        }
                    }
                    if (lines[i][0] == 'f')
                    {
                        tex = lines[i].Split(' ');
                        for (int j = 1; j < 4; j++)
                        {
                            f = tex[j].Split('/');
                            tri[j - 1] = int.Parse(f[0]) - 1;
                        }
                        t.Add(new Triangle(tri[0], tri[1], tri[2], Color.Black));
                    }
                }
            }
            models.Add(new Model(v, t));
            scene.instance.Add(new Instance(models[models.Count - 1], new Transform(1, new Vertex(0, 0, 0), new Vertex(0, 0, 0))));
            actual = scene.instance[scene.instance.Count - 1];
            TreeNode node = new TreeNode("Instance" + (list.Nodes.Count));
            node.Tag = actual;
            list.Nodes.Add(node);
        }
        private void RX_Click(object sender, EventArgs e)
        {
            actual.transform.ex = !actual.transform.ex;
            if (actual.transform.ex) rx.Text = "Stop X";
            else rx.Text = "Run X";
        }
        private void RY_Click(object sender, EventArgs e)
        {
            actual.transform.ye = !actual.transform.ye;
            if (actual.transform.ye) ry.Text = "Stop Y";
            else ry.Text = "Run Y";
        }
        private void RZ_Click(object sender, EventArgs e)
        {
            actual.transform.ze = !actual.transform.ze;
            if (actual.transform.ze) rz.Text = "Stop Z";
            else rz.Text = "Run Z";
        }
        private void LIST_AfterSelect(object sender, TreeViewEventArgs e)
        {
            actual = (Instance)list.SelectedNode.Tag;
            if (actual.transform.ex) rx.Text = "Stop X";
            else rx.Text = "Run X";
            if (actual.transform.ye) ry.Text = "Stop Y";
            else ry.Text = "Run Y";
            if (actual.transform.ze) rz.Text = "Stop Z";
            else rz.Text = "Run Z";
        }
        private void rotate()
        {
            for (int i = 0; i < scene.instance.Count; i++)
            {
                if (scene.instance[i].transform.ex) scene.instance[i].transform.X = a;
                if (scene.instance[i].transform.ye) scene.instance[i].transform.Y = a;
                if (scene.instance[i].transform.ze) scene.instance[i].transform.Z = a;
            }
        }
    }
}
