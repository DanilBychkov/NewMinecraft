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
        private readonly object locker = new object();//

        Dictionary<int, Image> imagesBlock = new Dictionary<int, Image>();
        Dictionary<Trials, Image> imagesMonster = new Dictionary<Trials, Image>();
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
        int tickCount = 0;

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
            imagesMonster.Add(Trials.MainTrial, Resource1.Skeletron_Head);
            imagesMonster.Add(Trials.ArenaTrial, Resource1.Shooter);
        }

        public Form1()
        {
            var trialInfo = new TrialsInfo();
            FillBlocksImages();
            FillMonsterImages();
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

        private void PaintF(Graphics graphics)
        {
            for (var i = 0; i < map.GetLength(0); i++)
                for (var v = 0; v < map.GetLength(1); v++)
                    if (imagesBlock.ContainsKey(map[i, v]))
                        graphics.DrawImage(imagesBlock[map[i,v]], new Rectangle(i*60-screen.GetPointX(), v*60-screen.GetPointY(), 60, 60));
        }

        Image halfHealth = Resource1.HalfHealth_com;
        Image fullHealth = Resource1.FullHealth_com;

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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            PaintF(e.Graphics);
            Action[] actions = new Action[creatures.Count];
            for (var i = 0; i < creatures.Count; i++)
            {
                creatures[i].ChangeSleep(player.GetPosition());
                if (creatures[i].IsSleep())
                    continue;
                if (creatures[i].IsDiedInConflit())
                {
                    creatures.RemoveAt(i);
                   continue;
                }
                var position = creatures[i].GetChangePosition(map, player, decorationObject, screen);
                e.Graphics.DrawImage(imagesMonster[trial], new Rectangle(position.X - screen.GetPointX(),
                                                         position.Y - screen.GetPointY(), creatures[i].GetSize(), creatures[i].GetSize()));
            }
            Action[] method = creatures.Select(i => new Action(() => i.MakeNewICreature(player.GetPosition(), "monster", tickCount, glowingBalls))).ToArray();
            Parallel.Invoke(method);
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
                   e.Graphics.DrawImage(smallSkeletonImage, new Rectangle(position.X - screen.GetPointX(), position.Y - screen.GetPointY(), glowingBalls[i].GetSize(), glowingBalls[i].GetSize()));
              else if (type == "monster" && trial == Trials.ArenaTrial)
                   e.Graphics.DrawImage(redBall, new Rectangle(position.X - screen.GetPointX(), position.Y - screen.GetPointY(), glowingBalls[i].GetSize(), glowingBalls[i].GetSize()));
              else if (type == "player")
                   e.Graphics.DrawImage(glowingBallsImage, new Rectangle(position.X - screen.GetPointX(), position.Y - screen.GetPointY(), glowingBalls[i].GetSize(), glowingBalls[i].GetSize()));
            }
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
                new ChangePosition().GetChangePosition(player, screen, PointToClient(Cursor.Position),
                    this.ClientSize.Width, this.ClientSize.Height, map, decorationObject, trial, creatures);
                var trialInfo = new TrialsInfo();
                tickCount += 1;
            if (trialInfo.IsPointToNextLevel(trial, new Point() { X = player.GetPosition().X, Y = player.GetPosition().Y }))
            {
                trial = trialInfo.GetNextTrial(trial);
                map = new Maps().GetMap(trial);
                creatures = trialInfo.GetMonstersTrial(trial);
                glowingBalls = new List<FlyingObject>();
                player.ChangePoisition(new Point() { X = trialInfo.GetStartPoint(trial).X, Y = trialInfo.GetStartPoint(trial).Y });
                screen = new ScreenPoint();
            }
            else if (player.GetHealth() <= 0 || creatures.Count == 0 && trial == Trials.ArenaTrial)
            {
                trial = Trials.CaveTrial;
                map = new Maps().GetMap(trial);
                creatures = trialInfo.GetMonstersTrial(trial);
                glowingBalls = new List<FlyingObject>();
                player = new Player(new TrialsInfo().GetStartPoint(trial));
                screen = new ScreenPoint();
            }
            Invalidate();
            Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (trial == Trials.CaveTrial)
                new CreatureNear().IsCreature(creatures, new Point() { X = e.Location.X + screen.GetPointX(), Y = e.Location.Y + screen.GetPointY() }, player.GetPosition());
            else 
            {
                var g = new Point() { X = e.Location.X + screen.GetPointX(), Y = e.Location.Y + screen.GetPointY() };
                player.MakeNewICreature(g, "player",tickCount,glowingBalls); 
            }
        }
    }
}
