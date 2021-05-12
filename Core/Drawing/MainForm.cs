using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Core;
using DWarp.Resources.Levels;
using System;

namespace DWarp.Core.Drawing
{
    public partial class MainForm : Form // Форма будет почти полностью переписана при реализации интерфейса
    {
        private static Bitmap floorImage = Properties.Resources.Floor; // ToRefactor...

        public MainForm(DirectoryInfo imagesDirectory = null)
        {
            InitializeComponent();

            var vignette = new Sprite(new Rectangle(0,0,ClientSize.Width,ClientSize.Height), Properties.Resources.Vignette, null);

            var controlsInfo = new Label // ToUpgrade...
            {
                Location = new Point(0, 30),
                Size = new Size(150, 100),
                Text = "Move = WASD\nResize = MouseWheel\nUndo = Q\nRedo = E\nTake/Place Cube = Space\nReset = R\nWarpPlayer = F\nSelectWarpedPlayer = V"
            };
            Controls.Add(controlsInfo);

            var drawWires = false;
            var controlTable = new TableLayoutPanel(); // ToUpgrade...
            var wireLabel = new Label() { Text = "Show Wires", Font = new Font("Arial", 12, FontStyle.Underline), BackColor = Color.Black, ForeColor = Color.White };
            controlTable.Size = new Size(106, 25);
            wireLabel.Click += (sender, args) => drawWires = drawWires ? false : true;
            controlTable.Controls.Add(wireLabel);
            Controls.Add(controlTable);

            var levelsTable = new TableLayoutPanel(); // ToUpgrade...
            levelsTable.Size = new Size(106, Presets.Levels.Count * 25);
            foreach (var level in Presets.Levels)
            {
                levelsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
                var levelLabel = new Label() { Text = level.Key, Font = new Font("Arial", 12, FontStyle.Underline), BackColor = Color.Black, ForeColor = Color.White};
                levelLabel.Click += (sender, args) => Game.Load(level.Value);
                levelsTable.Controls.Add(levelLabel);
            }
            Controls.Add(levelsTable);

            SetAlphaBending(Game.WarpedPlayer.Sprite.Image, 140);
            Game.WarpedPlayer.Sprite.Image.MakeTransparent(Color.White);
            Game.Player.Sprite.Image.MakeTransparent(Color.White);

            Paint += (sender, args) =>
            {
                var g = args.Graphics;
                g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
                DrawGameCreatures(Game.Map, g);
                g.TranslateTransform(
                    -Game.Map.GetLength(0) * Game.SpritesSize / 2 + Game.SpritesSize / 2,
                    -Game.Map.GetLength(1) * Game.SpritesSize / 2 + Game.SpritesSize / 2);
                if (drawWires)
                    foreach (var wire in Game.CurrentLevel.Wires)
                        g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(wire.Button.X * 255 / Game.Map.GetLength(0), wire.Door.X * 255 / Game.Map.GetLength(0), wire.Button.Y * 255 / Game.Map.GetLength(1)))) { Width = Game.SpritesSize / 10 }, 
                            wire.Button.X * Game.SpritesSize, 
                            wire.Button.Y * Game.SpritesSize, 
                            wire.Door.X * Game.SpritesSize, 
                            wire.Door.Y * Game.SpritesSize);
                g.ResetTransform();
                g.DrawImage(vignette.Image, vignette.Rectangle);
                UpdateGameInfo(g);
                FpsCounter.Elapsed();
            };

            KeyDown += (sender, args) =>
            {
                InputControl.ApplyKey(args.KeyCode);
            };

            Load += (sender, args) =>
            {
                OnSizeChanged(args);
            };

            SizeChanged += (sender, args) =>
            {
                levelsTable.Location = new Point(0, ClientSize.Height - levelsTable.Height);
                controlTable.Location = new Point(ClientSize.Width - controlTable.Width, 0);
                foreach (var creature in Game.Map)
                    UpdateCreatureSprite(creature);
                foreach(var cube in Game.Cubes)
                    if(cube != null)
                        UpdateCreatureSprite(cube);
                UpdateGUISprite(vignette);
            };

            MouseWheel += (sender, args) =>
            {
                InputControl.ApllyMouseScroll(args.Delta); 
            };

            var invalidatingTimer = new Timer();
            invalidatingTimer.Interval = 1;
            invalidatingTimer.Tick += (sender, args) => Invalidate();
            invalidatingTimer.Start();
        }

        public Bitmap SetAlphaBending(Bitmap image, int alpha)
        {
            for(var x = 0; x < image.Width; x++)
                for(var y = 0; y < image.Height; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    image.SetPixel(x, y, Color.FromArgb(alpha, pixel));
                }
            return image;
        }

        public void UpdateGameInfo(Graphics g)
        {
            g.DrawString($"{Game.CommandsStack.Stack.Count} / {Game.CommandsStack.Limit}", new Font("Arial", 15), Brushes.Red, new Point(0, 0));
            if(Game.CurrentLevel.TimeLimit > 0)
                g.DrawString($"Time: {Game.Time}", new Font("Arial", 15), Brushes.Red, new Point(100, 0));
            g.DrawString($"FPS: {FpsCounter.GetFps()}", new Font("Arial", 15), Brushes.Red, new Point(220, 0));
        }

        public void DrawGameCreatures(Creature[,] map, Graphics g) // ToRefactor...
        {
            foreach (var creature in map)
            {
                creature.Sprite.Image.MakeTransparent(Color.White);
                UpdateCreatureSprite(creature);
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
                UpdateCreatureSprite(cube);
                g.DrawImage(cube.Sprite.Image, cube.Sprite.Rectangle);
            }

            UpdateCreatureSprite(Game.Player);
            g.DrawImage(Game.Player.Sprite.Image, Game.Player.Sprite.Rectangle);
            if (Game.IsWarped)
            {
                UpdateCreatureSprite(Game.WarpedPlayer);
                g.DrawImage(Game.WarpedPlayer.Sprite.Image, Game.WarpedPlayer.Sprite.Rectangle);
            }
        }

        public void UpdateCreatureSprite(Creature creature)
        {
            creature.Sprite.Rectangle.X = Game.SpritesSize * creature.Location.X - Game.SpritesSize * Game.Map.GetLength(0) / 2 + creature.Sprite.Offset.X;
            creature.Sprite.Rectangle.Y = Game.SpritesSize * creature.Location.Y - Game.SpritesSize * Game.Map.GetLength(0) / 2 + creature.Sprite.Offset.Y;
            creature.Sprite.Rectangle.Width = Game.SpritesSize + creature.Sprite.SizeOffset.Width;
            creature.Sprite.Rectangle.Height = Game.SpritesSize + creature.Sprite.SizeOffset.Height;
        }

        public void UpdateGUISprite(Sprite sprite)
        {
            sprite.Rectangle.Width = ClientSize.Width;
            sprite.Rectangle.Height = ClientSize.Height;
        }
    }
}
