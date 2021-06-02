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
        Trials trial = Trials.MainTrial;
        Image playerImage = Resource1.playerImage;
        Image halfHealth = Resource1.HalfHealth_com;
        Image fullHealth = Resource1.FullHealth_com;
        Image glowingBallsImage = Resource1.GlowingBall;
        Image playerInSpaceSheep = Resource1.playerInSpaceShip;
        Player player;
        ScreenPoint screen;
        int[,] map;
        int[] decorationObject;
        List<GlowingBalls> glowingBalls = new List<GlowingBalls>();
        List<ICreature> creatures = new List<ICreature>();
        private List<Block> blocks = new List<Block>();
        int tickCount = 1000;

        public Form1()
        {
            var trialsInfo = new TrialsInfo();
            var maps = new Maps();
            screen = new ScreenPoint(trialsInfo.GetChangingScreen(trial));
            blocks = trialsInfo.GetStartBlocks(screen, tickCount);
            map = maps.GetMap(trial);
            decorationObject = maps.GetDecorationObjerct();
            player = new Player(new TrialsInfo().GetStartPoint(trial));
            creatures = trialsInfo.GetMonstersTrial(trial);
            FillBlocksImages();
            FillMonsterImages();
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
                if (new CreaturesChangeState().ChangeState(creatures, i, player))
                    continue;
                var position = creatures[i].GetChangePosition(map,player,decorationObject,screen,glowingBalls);
                graphics.DrawImage(imagesMonster[trial], new Rectangle(position.X -screen.GetPointX(),
                                     position.Y - screen.GetPointY(),creatures[i].GetSize(),creatures[i].GetSize()));
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
            glowingBalls.ChangeState(creatures,player,map,decorationObject);
            for (var i = 0; i <glowingBalls.Count; i++)
            {
                var position = glowingBalls[i].GetPosition();
                graphics.DrawImage(glowingBallsImage, new Rectangle(position.X -screen.GetPointX(),
                        position.Y -screen.GetPointY(),glowingBalls[i].GetSize(),glowingBalls[i].GetSize()));
            }
        }

        private void DrawFlyingUfoLevel(Graphics graphics)
        {
            graphics.DrawImage(playerInSpaceSheep, new Rectangle(player.GetPosition().X, player.GetPosition().Y, 40, 40));
            foreach (var block in blocks)
            {
                graphics.DrawImage(Resource1.pipeUp, new Rectangle(block.pointUp.X - screen.GetPointX(), block.pointUp.Y, block.width, block.lenghtUpPart));
                graphics.DrawImage(Resource1.pipeDown, new Rectangle(block.pointDown.X - screen.GetPointX(), block.pointDown.Y, block.width, block.lengthDownPart));
            }
        }

        private void DrawLevel(Graphics graphics)
        {
            PaintBlocks(graphics);
            PaintCreatures(graphics);
            creatures.AddBalls(player,tickCount,glowingBalls,map,decorationObject);
            DrawGlowingBalls(graphics);
            if (player.IsintoSpaceSheep() == true)
                graphics.DrawImage(playerInSpaceSheep, new Rectangle(player.GetPosition().X - screen.GetPointX(), player.GetPosition().Y - screen.GetPointY(), 40, 40));
            else
                graphics.DrawImage(playerImage, new Rectangle(player.GetPosition().X - screen.GetPointX(),player.GetPosition().Y -screen.GetPointY(), 40, 40));
            DrawHealth(graphics);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (trial== Trials.ArenaTrial&&player.IsintoSpaceSheep()==false)
                e.Graphics.DrawImage(playerInSpaceSheep,new Rectangle(700-screen.GetPointX(),455-screen.GetPointY(),150,150));
            if (trial == Trials.FlyingUFO)
                DrawFlyingUfoLevel(e.Graphics);
            else
                DrawLevel(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F)
                player.ChangeIsIntoSpaceSheep(true,creatures.Count());
            if (e.KeyCode == Keys.Right)
                player.ChangeDirectionPlayer(screen,map,decorationObject, Direction.Right, trial);
            else if (e.KeyCode == Keys.Left)
                player.ChangeDirectionPlayer(screen,map,decorationObject, Direction.Left, trial);
            else if (e.KeyCode == Keys.Up)
                player.ChangeDirectionPlayer(screen,map,decorationObject, Direction.Up, trial);
            else if (e.KeyCode == Keys.Down)
                player.ChangeDirectionPlayer(screen,map,decorationObject, Direction.Down, trial);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new ChangePlayerMonstersPerTick().ChangeStatesPerTick(ref player,ref screen,ref map, ref decorationObject, ref trial, ref creatures, 
                ref glowingBalls,ref tickCount,ref blocks);
            tickCount += 1;
            Invalidate();
            Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
             =>e.Location.DoAction(trial,creatures,screen,player,glowingBalls,tickCount,map,decorationObject);

        private void FillBlocksImages()
        {
            imagesBlock.Add(2, Resource1.caveTrialBackGround);
            imagesBlock.Add(3, Resource1.caveTrialBackGround);
            imagesBlock.Add(4, Resource1.Leaf_Block);
            imagesBlock.Add(5, Resource1.Living_Wood);
            imagesBlock.Add(7, Resource1.kisspng_sprite_tile_based_video_game_level_2d_computer_gra_unity_5ac7699ddb8e27_416025401523018141899322);
            imagesBlock.Add(9, Resource1.grass);
            imagesBlock.Add(1, Resource1.caveTrialBackGround);
        }

        private void FillMonsterImages()
        {
            imagesMonster.Add(Trials.CaveTrial, Resource1.pngegg__5_);
            imagesMonster.Add(Trials.MainTrial, Resource1.monster);
            imagesMonster.Add(Trials.ArenaTrial, Resource1.pngegg__6_);
        }
    }
}
