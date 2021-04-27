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
        Dictionary<int, Image> imagesBlock = new Dictionary<int, Image>();
        Dictionary<Trials, Image> imagesMonster = new Dictionary<Trials, Image>();
        Trials trial = Trials.CaveTrial;
        Image playerImage = Resource1.Player;
        Player player;
        Object[] monsters=new object[3];
        ScreenPoint screen = new ScreenPoint();
        int[,] map;
        int[] decorationObject;
        public void FillBlocksImages()
        { 
            imagesBlock.Add(1, Resource1.Dirt_Block);
            imagesBlock.Add(2, Resource1.Obsidian);
            imagesBlock.Add(3, Resource1.G_Stone);
            imagesBlock.Add(4, Resource1.Leaf_Block);
            imagesBlock.Add(5, Resource1.Living_Wood);
            imagesBlock.Add(6, Resource1.Gray_Stucco);
        }

        public void FillMonsterImages()
        {
            imagesMonster.Add(Trials.CaveTrial,Resource1.Armed_Zombie);
        }

        public Form1()
        {
            monsters[0] = new Zombi(new Point() {X=540,Y=180});//убрать в описание уровня
            monsters[1] = new Zombi(new Point() { X = 241, Y = 421 });//
            monsters[2] = new Zombi(new Point() { X = 661,Y=480});//
            FillBlocksImages();
            FillMonsterImages();
            var maps = new Maps();
            map = maps.GetMap(trial);
            decorationObject = maps.GetDecorationObjerct();
            player= new Player(new Point() { X = 181, Y = 841 });// убрать в описание уровня
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            KeyPreview = true;
        }

        private void PaintF(Graphics graphics)
        {
            for (var i = 0; i < map.GetLength(0); i++)
                for (var v = 0; v < map.GetLength(1); v++)
                    if (imagesBlock.ContainsKey(map[i, v]))
                        graphics.DrawImage(imagesBlock[map[i,v]], new Rectangle(i*60-screen.GetPointX(), v*60-screen.GetPointY(), 60, 60));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            PaintF(e.Graphics);
            e.Graphics.DrawImage(playerImage, new Rectangle(player.GetPosition().X,player.GetPosition().Y, 40, 40));
            foreach (var monster in monsters)
            {
                var IcreatureMonster = (ICreature)monster;
                if (!IcreatureMonster.IsDeadInConflict() && !IcreatureMonster.IsSleep())
                    e.Graphics.DrawImage(imagesMonster[trial], new Rectangle(IcreatureMonster.GetPosition().X, IcreatureMonster.GetPosition().Y, 40, 40));
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
                new ChangeDirection().ChangeDirectionPlayer(player, screen, map, decorationObject, Direction.Right, trial);
            else if (e.KeyCode == Keys.Left)
                new ChangeDirection().ChangeDirectionPlayer(player, screen, map, decorationObject, Direction.Left, trial);
            else if (e.KeyCode == Keys.Up)
                new ChangeDirection().ChangeDirectionPlayer(player, screen, map, decorationObject, Direction.Up, trial);
            else if (e.KeyCode == Keys.Down)
                new ChangeDirection().ChangeDirectionPlayer(player, screen, map, decorationObject, Direction.Down, trial);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new ChangePosition().GetChangePosition(player, screen, PointToClient(Cursor.Position), 
                this.ClientSize.Width, this.ClientSize.Height,map,decorationObject,trial,monsters);
            Invalidate();
            Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            new CreatureNear().IsCreature(monsters,e.Location,player.GetPosition());
        }
    }
}
