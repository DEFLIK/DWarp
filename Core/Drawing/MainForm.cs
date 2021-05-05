using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Core;

namespace DWarp
{
    public partial class MainForm : Form
    {
        private static Bitmap floorImage = Properties.Resources.Floor; // ToRefactor...
        public MainForm(DirectoryInfo imagesDirectory = null)
        {
            var controlsInfo = new Label // ToUpgrade...
            {
                Location = new Point(0, 30),
                Size = new Size(150, 100),
                Text = "Move = WASD\nResize = MouseWheel\nUndo = Q\n Redo = E\nTake - Place Cube = F\nReset = R\nWarpPlayer = Space\nSelectWarpedPlayer = V"
            };
            Controls.Add(controlsInfo);

            Game.Player.Sprite.Image.MakeTransparent(Color.White);

            Paint += (sender, args) =>
            {
                controlsInfo.Update(); // Optimize...
                var g = args.Graphics;
                g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
                g.ScaleTransform(2, 2);
                DrawGameCreatures(Game.Map, g);
                g.ResetTransform();
                UpdateGameInfo(g);
                Invalidate();
            };

            KeyDown += (sender, args) => InputControl.ApplyKey(args.KeyCode);

            MouseWheel += (sender, args) => InputControl.ApllyMouseScroll(args.Delta);

            InitializeComponent();
        }

        public void UpdateGameInfo(Graphics g)
        {
            g.DrawString($"{Game.CommandsStack.Stack.Count} / {Game.CommandsStack.Limit}", new Font("Arial", 15), Brushes.Red, new Point(0, 0));
            if(Game.CurrentLevel.TimeLimit > 0)
                g.DrawString($"Time: {Game.Time}", new Font("Arial", 15), Brushes.Red, new Point(100, 0));
        }

        public void DrawGameCreatures(Creature[,] map, Graphics g) // ToRefactor...
        {
            foreach (var creature in map)
            {
                creature.Sprite.Image.MakeTransparent(Color.White);
                UpdateSprite(creature);
                if (creature.Type == CreatureType.Button || creature.Type == CreatureType.Door)
                    g.DrawImage(floorImage, creature.Sprite.Rectangle);
                g.DrawImage(creature.Sprite.Image, creature.Sprite.Rectangle);
                creature.Sprite.Image.SetResolution(1, 1);
            }
            foreach(var cube in Game.Cubes)
            {
                if (cube is null)
                    continue;
                cube.Sprite.Image.MakeTransparent(Color.White);
                UpdateSprite(cube);
                g.DrawImage(cube.Sprite.Image, cube.Sprite.Rectangle);
            }

            UpdateSprite(Game.Player);
            g.DrawImage(Game.Player.Sprite.Image, Game.Player.Sprite.Rectangle);
            if (Game.IsWarped)
            {
                UpdateSprite(Game.WarpedPlayer);
                g.DrawImage(Game.WarpedPlayer.Sprite.Image, Game.WarpedPlayer.Sprite.Rectangle);
            }
        }

        public void UpdateSprite(Creature creature)
        {
            creature.Sprite.Rectangle.X = Game.SpritesSize * creature.Location.X - Game.SpritesSize * Game.Map.GetLength(0) / 2 + creature.Sprite.Offset.X;
            creature.Sprite.Rectangle.Y = Game.SpritesSize * creature.Location.Y - Game.SpritesSize * Game.Map.GetLength(0) / 2 + creature.Sprite.Offset.Y;
            creature.Sprite.Rectangle.Width = Game.SpritesSize + creature.Sprite.SizeOffset.Width;
            creature.Sprite.Rectangle.Height = Game.SpritesSize + creature.Sprite.SizeOffset.Height;
        }
    }
}
