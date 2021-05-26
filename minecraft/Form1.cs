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
        private readonly object locker = new object();//обратить внимание

        Dictionary<int, Image> imagesBlock = new Dictionary<int, Image>();
        Dictionary<Trials, Image> imagesMonster = new Dictionary<Trials, Image>();
        Dictionary<string, Image> imagesHealth = new Dictionary<string, Image>();//обратить внимание

        Trials trial = Trials.CaveTrial;

        Image playerImage = Resource1.playerImageCaveTrial;
        Player player;
        ScreenPoint screen = new ScreenPoint();
        int[,] map;
        int[] decorationObject;
        Image playerImageMainTrial=Resource1.playerImageMainTrial;
        Image smallSkeletonImage = Resource1.Cursed_Skull;
        Image glowingBallsImage = Resource1.GlowingBall;
        Image redBall = Resource1.redBall;
        List<FlyingObject> glowingBalls = new List<FlyingObject>();
        List<ICreature> creatures = new List<ICreature>();

        Image halfHealth = Resource1.HalfHealth_com;//обратить внимание
        Image fullHealth = Resource1.FullHealth_com;//обратить внимание
        int tickCount = 0;

        public Form1()
        {
            var trialInfo = new TrialsInfo();
            FillBlocksImages();
            FillMonsterImages();
            FillHealthImage();

            var maps = new Maps();
            map = maps.GetMap(trial);
            decorationObject = maps.GetDecorationObjerct();
            player = new Player(new TrialsInfo().GetStartPoint(trial));
            creatures = trialInfo.GetMonstersTrial(trial);
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            KeyPreview = true;
        }

        private void PaintBlocks(Graphics graphics)
        {
            for (var i = 0; i < map.GetLength(0); i++)
                for (var v = 0; v < map.GetLength(1); v++)
                    if (imagesBlock.ContainsKey(map[i, v]))
                        graphics.DrawImage(imagesBlock[map[i,v]], new Rectangle(i*60-screen.GetPointX(), v*60-screen.GetPointY(), 60, 60));
        }

        private void PaintCreatures(Graphics graphics)
        {
            for (var i = 0; i < creatures.Count; i++)
            {
                if (new CreaturesChangeState().ChangeState(ref creatures, i, ref player))
                    continue;
                var position = creatures[i].GetChangePosition(map, player, decorationObject, screen);
                graphics.DrawImage(imagesMonster[trial], new Rectangle(position.X - screen.GetPointX(),
                                     position.Y - screen.GetPointY(), creatures[i].GetSize(), creatures[i].GetSize()));
            }
        }

        private void DrawHealth(Graphics graphics)
        {
            var startPointX = 0;
            var startPointY = 50;
            for (var i = 0; i < player.GetHealth() / 10; i++)
            {
                startPointX += 30;
                graphics.DrawImage(fullHealth, new Rectangle(startPointX, startPointY, 30, 30));
            }
            startPointX += 30;
            if (player.GetHealth() % 10 != 0)
                graphics.DrawImage(halfHealth, new Rectangle(startPointX, startPointY, 30, 30));
        }

        private void DrawGlowingBalls(Graphics graphics)
        {
            for (var i = 0; i < glowingBalls.Count; i++)
            {
                if (glowingBalls[i].IsDied(creatures, glowingBalls, player, map, decorationObject))
                {
                    glowingBalls.RemoveAt(i);
                    continue;
                }
                var position = glowingBalls[i].GetPosition();
                var type = glowingBalls[i].GetTypeObject();
                if (type == "monster" && trial == Trials.MainTrial)
                    graphics.DrawImage(smallSkeletonImage, new Rectangle(position.X - screen.GetPointX(), position.Y - screen.GetPointY(), glowingBalls[i].GetSize(), glowingBalls[i].GetSize()));
                else if (type == "monster" && trial == Trials.ArenaTrial)
                    graphics.DrawImage(redBall, new Rectangle(position.X - screen.GetPointX(), position.Y - screen.GetPointY(), glowingBalls[i].GetSize(), glowingBalls[i].GetSize()));
                else if (type == "player")
                    graphics.DrawImage(glowingBallsImage, new Rectangle(position.X - screen.GetPointX(), position.Y - screen.GetPointY(), glowingBalls[i].GetSize(), glowingBalls[i].GetSize()));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)//
        {
            PaintBlocks(e.Graphics);
            PaintCreatures(e.Graphics);
            Action[] actions = new Action[creatures.Count];
            Action[] method = creatures.Select(i => new Action(() => i.MakeNewICreature(player.GetPosition(), "monster", tickCount, glowingBalls))).ToArray();
            Parallel.Invoke(method);
            DrawGlowingBalls(e.Graphics);
            if (trial == Trials.CaveTrial)
               e.Graphics.DrawImage(playerImage, new Rectangle(player.GetPosition().X - screen.GetPointX(), player.GetPosition().Y - screen.GetPointY(), 40, 40));
            else
               e.Graphics.DrawImage(playerImageMainTrial, new Rectangle(player.GetPosition().X - screen.GetPointX(), player.GetPosition().Y - screen.GetPointY(), 40, 40));
            DrawHealth(e.Graphics);
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
            new ChangePlayerMonstersPerTick().ChangeStatesPerTick(ref player, ref screen, PointToClient(Cursor.Position),
               this.ClientSize.Width, this.ClientSize.Height,ref  map, decorationObject, ref trial, ref creatures, ref glowingBalls);
            tickCount += 1;
            Invalidate();
            Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            new MouseClick().DoAction(trial, creatures, e.Location, screen, player, glowingBalls, tickCount);
        }

        private void FillBlocksImages()
        {
            imagesBlock.Add(1, Resource1.Dirt_Block);
            imagesBlock.Add(2, Resource1.Obsidian);
            imagesBlock.Add(3, Resource1.G_Stone);
            imagesBlock.Add(4, Resource1.Leaf_Block);
            imagesBlock.Add(5, Resource1.Living_Wood);
            imagesBlock.Add(6, Resource1.Gray_Stucco);
        }

        private void FillMonsterImages()
        {
            imagesMonster.Add(Trials.CaveTrial, Resource1.Armed_Zombie);
            imagesMonster.Add(Trials.MainTrial, Resource1.Skeletron_Head);
            imagesMonster.Add(Trials.ArenaTrial, Resource1.Shooter);
        }

        private void FillHealthImage()
        {
            imagesHealth.Add("halfHealth", Resource1.Armed_Zombie);
            imagesHealth.Add("fullHealth", Resource1.Armed_Zombie);
        }
    }
}
