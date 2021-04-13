using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft
{
    public partial class Form1 : Form
    {
        int[,] map;
        int[] decorationObject = new int[3] {0,4,5};
        Image earth = Resource1.Dirt_Block;//1
        Image player = Resource1.Player;
        Image obsidian = Resource1.Obsidian;//2
        Image stone = Resource1.G_Stone;//3
        Image leaf = Resource1.Leaf_Block;//4
        Image wood = Resource1.Living_Wood;//5
        Image gray_Srucco = Resource1.Gray_Stucco;//6

        Point ChangingScreen = new Point(0, 0);
        int block = 1;
        int PointPlayerX = 0;
        int PointPlayerY = 100;

        int Keyup = 0;

        int direction = 0;//-1-влево,1-вправо

        public Form1()
        {
            var maps = new Maps();
            map = maps.GetMainMap();
            PointPlayerX = this.ClientSize.Width / 2 - 20;
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            KeyPreview = true;
        }

        private void PaintF(Graphics graphics)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == 1)
                        graphics.DrawImage(earth, new Rectangle(x * 40 - ChangingScreen.X, y * 40 - ChangingScreen.Y, 40, 40));
                    if (map[x, y] == 2)
                        graphics.DrawImage(obsidian, new Rectangle(x * 40 - ChangingScreen.X, y * 40 - ChangingScreen.Y, 40, 40));
                    if (map[x, y] == 3)
                        graphics.DrawImage(stone, new Rectangle(x * 40 - ChangingScreen.X, y * 40 - ChangingScreen.Y, 40, 40));
                    if (map[x, y] == 4)
                        graphics.DrawImage(leaf, new Rectangle(x * 40 - ChangingScreen.X, y * 40 - ChangingScreen.Y, 40, 40));
                    if (map[x, y] == 5)
                        graphics.DrawImage(wood, new Rectangle(x * 40 - ChangingScreen.X, y * 40 - ChangingScreen.Y, 40, 40));
                    if (map[x, y] == 6)
                        graphics.DrawImage(gray_Srucco, new Rectangle(x * 40 - ChangingScreen.X, y * 40 - ChangingScreen.Y, 40, 40));

                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            PaintF(e.Graphics);
            if (EmptyBelow())
                PointPlayerY += 6;
            if (Keyup > 0)
            {
                Keyup -= 12;
                if(IsWay(0,12))
                    PointPlayerY -= 12;
                else 
                {
                    Keyup = 0;
                }
            }
            e.Graphics.DrawImage(player, new Rectangle((int)PointPlayerX, PointPlayerY, 40, 40));
        }

        private bool EmptyBelow()
        {
            var x = (PointPlayerX + ChangingScreen.X) / 40.0;
            var y = ((PointPlayerY + ChangingScreen.Y) + 40) / 40;
            if (direction == -1)
            {
                if (map[(int)x + 1, y] == 4 || map[(int)x, y] == 4) return true;
                if (map[(int)x + 1, y] == 5 || map[(int)x, y] == 5) return true;
                if (map[(int)x + 1, y] != 0 || map[(int)x, y] != 0) return false;
            }
            if (direction == 1)
            {
                if (map[(int)x, y] == 4 || map[(int)x + 1, y] == 4) return true;
                if (map[(int)x, y] == 5 || map[(int)x + 1, y] == 5) return true;
                if (map[(int)x, y] != 0 || map[(int)x + 1, y] != 0) return false;
            }
            if (direction == 0)
            {
                if (map[(int)x, y] == 4) return true;
                if (map[(int)x, y] == 5) return true;
                if (map[(int)x, y] != 0) return false;
            }
            return true;
        }

        private bool IsWay(int x, int y)
        {
            if (direction == 1&&y==0)
            {
                if (map[(PointPlayerX + ChangingScreen.X + 40) / 40, (PointPlayerY + ChangingScreen.Y ) / 40] == 5) return true;
                if (map[(PointPlayerX + ChangingScreen.X + 40) / 40, (PointPlayerY + ChangingScreen.Y ) / 40] == 4) return true;
                if (map[(PointPlayerX + ChangingScreen.X + 40) / 40, (PointPlayerY + ChangingScreen.Y ) / 40] == 0) return true;
            }
            if (direction == -1&&y==0)
            {
                if (map[(PointPlayerX + ChangingScreen.X) / 40, (PointPlayerY + ChangingScreen.Y ) / 40] == 5) return true;
                if (map[(PointPlayerX + ChangingScreen.X) / 40, (PointPlayerY + ChangingScreen.Y ) / 40] == 4) return true;
                if (map[(PointPlayerX + ChangingScreen.X) / 40, (PointPlayerY + ChangingScreen.Y ) / 40] == 0) return true;
            }
            if (y != 0)
                if (decorationObject.Contains(map[(PointPlayerX + ChangingScreen.X) / 40, (PointPlayerY + ChangingScreen.Y - y) / 40])&&
                    decorationObject.Contains(map[(PointPlayerX + ChangingScreen.X+40) / 40, (PointPlayerY + ChangingScreen.Y - y) / 40])) return true;
            return false;
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Right)
            {
                direction = 1;
                if (IsWay(3, 0))
                {
                    PointPlayerX += 4;
                    ChangingScreen.X += 8;
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                direction = -1;
                if (IsWay(-3, 0))
                {
                    PointPlayerX -= 4;
                    ChangingScreen.X -= 8;
                }

            }
            if (e.KeyCode == Keys.Up && Keyup <= 0 && !EmptyBelow())
            {

                Keyup = 130;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Point PointClick = PointToClient(Cursor.Position);
            var x = (PointClick.X + ChangingScreen.X) / 40;
            var y = (PointClick.Y + ChangingScreen.Y) / 40;
            if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1))
                return;
            map[x, y] = block;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point PointClick = PointToClient(Cursor.Position);
            var width = this.ClientSize.Width / 10;
            var height = this.ClientSize.Height / 10;
            if (PointClick.X > this.ClientSize.Width - width)
            {
                PointPlayerX -= 5;
                ChangingScreen.X += 5;
            }
            if (PointClick.X < width)
            {
                PointPlayerX +=5;
                ChangingScreen.X -= 5;
            }
            if (PointClick.Y > this.ClientSize.Height - height)
            {
                PointPlayerY -= 5;
                ChangingScreen.Y += 5;
            }
            if (PointClick.Y < height)
            {
                PointPlayerY += 5;
                ChangingScreen.Y -= 5;
            }
            Invalidate();
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
