using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minecraft.Models;
using Minecraft.Control;

namespace Minecraft
{
    public partial class Form1 : Form
    {
        Dictionary<int, Image> Images = new Dictionary<int, Image>();
        int[,] map;
        int[] decorationObject;
        Player player;
        Zombi zombi;
        Image zombiImage = Resource1.Armed_Zombie;
        Image playerImage = Resource1.Player;
        ScreenPoint screen = new ScreenPoint();
        EnumMaps numberLevel = EnumMaps.CaveMap;
        public void FillImages()
        { 
            Images.Add(1, Resource1.Dirt_Block);
            Images.Add(2, Resource1.Obsidian);
            Images.Add(3, Resource1.G_Stone);
            Images.Add(4, Resource1.Leaf_Block);
            Images.Add(5, Resource1.Living_Wood);
            Images.Add(6, Resource1.Gray_Stucco);
        }

        public Form1()
        {
            FillImages();
            player = new Player(new Point() { X = 61, Y = 361 });
            zombi = new Zombi(new Point() { X = 130, Y = 70 });
            map = new Maps().GetMap(EnumMaps.CaveMap);
            decorationObject = new Maps().GetDecorationObject();
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
                    if (Images.ContainsKey(map[x, y]))
                        graphics.DrawImage(Images[map[x,y]], new Rectangle(x * 60 - screen.GetPointX(), y * 60 - screen.GetPointY(), 60, 60));
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)//должна только отрисовывать мир и игроков
        {
            PaintF(e.Graphics);
            e.Graphics.DrawImage(playerImage, new Rectangle(player.GetPosition().X,player.GetPosition().Y, 40, 40));
            e.Graphics.DrawImage(zombiImage, new Rectangle(zombi.GetPosition().X, zombi.GetPosition().Y, 40, 40));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            var direction = Direction.None;
            if (e.KeyCode == Keys.Right)
                direction = Direction.Right;
            else if (e.KeyCode == Keys.Left)
                direction = Direction.Left;
            else if (e.KeyCode == Keys.Up)
                direction = Direction.Up;
            else if (e.KeyCode == Keys.Down)
                direction = Direction.Down;
            new ChangeDirection().ChangeDirectionPlayer(direction, player, screen, map, decorationObject, numberLevel);
        }

        private void Form1_Click(object sender, EventArgs e)//убрать метод потом
        {
            Point PointClick = PointToClient(Cursor.Position);
            var x = (PointClick.X + screen.GetPointX()) / 60;
            var y = (PointClick.Y + screen.GetPointY()) / 60;
            if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1))
                return;
            map[x, y] = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new ChangePosition().GetChangePosition(player,screen, PointToClient(Cursor.Position), 
                this.ClientSize.Width, this.ClientSize.Height,map,decorationObject,numberLevel,zombi);
            Invalidate();
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
