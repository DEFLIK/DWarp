using System.Drawing;
using System.Windows.Forms;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using DWarp.Resources.Levels;
using System.Collections.Generic;
using System.Reflection;

namespace DWarp.Core.Drawing
{
    public partial class MainForm : Form // Форма будет почти полностью переписана при реализации интерфейса, сейчас тут поле экспериментов и беспорядка
    {
        private static Bitmap floorImage = Properties.Resources.Floor; // ToRefactor...
        public State CurrentState;
        public CreaturesSprites CurrentSprites;
        public Timer InvalidatingTimer;
        //public TableLayoutPanel MenuTable;
        public readonly Menu MenuPanel;
        private (int X, int Y) centerOffset = (0, 0);

        public void ChangeState(State newState)
        {
            CurrentSprites = new CreaturesSprites(newState);
            CurrentState = newState;
        }

        public void SetDoubleBuffered(Control control)
        {
            if (SystemInformation.TerminalServerSession)
                return;
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(control, true, null);
        }

        public MainForm(State firstState)
        {
            InitializeComponent();
            ChangeState(firstState);
            InvalidatingTimer = new Timer();
            InvalidatingTimer.Interval = 1;
            InvalidatingTimer.Tick += (sender, args) => Invalidate();
            InvalidatingTimer.Start();
            var InvalidatingTimer1 = new Timer();
            InvalidatingTimer1.Interval = 1;
            InvalidatingTimer1.Tick += (sender, args) => Invalidate();
            InvalidatingTimer1.Start();

            MenuPanel = new Menu(this);

            //MenuTable = new TableLayoutPanel();
            //MenuTable.SuspendLayout();
            //MenuTable.Size = new Size(ClientSize.Width / 2, ClientSize.Height);
            //MenuTable.RowStyles.Add(new RowStyle());
            //MenuTable.RowStyles.Add(new RowStyle());
            //MenuTable.RowStyles.Add(new RowStyle());
            //MenuTable.RowStyles.Add(new RowStyle());
            ////menuTable.ColumnStyles.Add(new ColumnStyle());

            //var resumeButton = new PictureBox();
            //resumeButton.Size = new Size(MenuTable.Width, 100);
            //resumeButton.SizeMode = PictureBoxSizeMode.StretchImage;
            //resumeButton.Image = Properties.Resources.Cube;
            ////resumeButton.Location = new Point(ClientSize.Width / 2 - resumeButton.Width / 2, ClientSize.Height / 2 - resumeButton.Height / 2);
            //resumeButton.Click += (sender, args) => resumeButton.Visible = resumeButton.Visible ? false : true;
            //MenuButtons.Add(resumeButton);
            ////Controls.Add(resumeButton);
            //var settingsButton = new PictureBox();
            //settingsButton.Size = new Size(MenuTable.Width - 5, 100);
            //settingsButton.SizeMode = PictureBoxSizeMode.StretchImage;
            //settingsButton.Image = Properties.Resources.Player;
            ////settingsButton.Location = new Point(ClientSize.Width / 2 - settingsButton.Width / 2, resumeButton.Location.Y + settingsButton.Height + 5);
            //settingsButton.Click += (sender, args) => settingsButton.Visible = settingsButton.Visible ? false : true;
            //MenuButtons.Add(settingsButton);
            ////Controls.Add(settingsButton);

            //MenuTable.Controls.Add(resumeButton);
            //MenuTable.Controls.Add(settingsButton);
            //MenuTable.BackColor = Color.FromArgb(100, 0, 0, 0);
            //MenuTable.Location = new Point(ClientSize.Width / 2 - MenuTable.Width / 2, ClientSize.Height / 2 - MenuTable.Height / 2);
            //MenuTable.ResumeLayout();
            //SetDoubleBuffered(MenuTable);
            //Controls.Add(MenuTable);

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
                levelLabel.Click += (sender, args) => Game.ChangeLevel(level.Value);
                levelsTable.Controls.Add(levelLabel);
            }
            Controls.Add(levelsTable);

            Paint += (sender, args) =>
            {
                //if (CurrentState.Map == null)
                //    return;
                args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                args.Graphics.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
                DrawStaticCreatures(args.Graphics);
                DrawDynamicCreatures(args.Graphics);
                //DrawGameCreatures(CurrentState.Map, g);
                args.Graphics.TranslateTransform(
                    -CurrentState.Map.GetLength(0) * CurrentState.SpritesSize / 2 + CurrentState.SpritesSize / 2,
                    -CurrentState.Map.GetLength(1) * CurrentState.SpritesSize / 2 + CurrentState.SpritesSize / 2);
                args.Graphics.TranslateTransform(centerOffset.X, centerOffset.Y);
                if (drawWires)
                    foreach (var wire in CurrentState.CurrentLevel.Wires)
                        args.Graphics.DrawLine(
                            new Pen(new SolidBrush(
                                Color.FromArgb(wire.Button.X * 255 / CurrentState.Map.GetLength(0), 
                                wire.Door.X * 255 / CurrentState.Map.GetLength(0),
                                wire.Button.Y * 255 / CurrentState.Map.GetLength(1)))) 
                                { Width = CurrentState.SpritesSize / 10 }, 
                            wire.Button.X * CurrentState.SpritesSize, 
                            wire.Button.Y * CurrentState.SpritesSize, 
                            wire.Door.X * CurrentState.SpritesSize, 
                            wire.Door.Y * CurrentState.SpritesSize);
                args.Graphics.ResetTransform();
                args.Graphics.DrawImage(vignette.Image, vignette.Rectangle);
                UpdateGameInfo(args.Graphics);
                FpsCounter.Elapsed();
            };

            KeyDown += (sender, args) => InputControl.ApplyKey(CurrentState, args.KeyCode);

            Load += (sender, args) => OnSizeChanged(args);

            SizeChanged += (sender, args) =>
            {
                levelsTable.Location = new Point(0, ClientSize.Height - levelsTable.Height);
                controlTable.Location = new Point(ClientSize.Width - controlTable.Width, 0);
                MenuPanel.Table.Location = new Point(ClientSize.Width / 2 - MenuPanel.Table.Width / 2, ClientSize.Height / 2 - MenuPanel.Table.Height / 2);
                UpdateGUISprite(vignette);
            };

            MouseWheel += (sender, args) => InputControl.ApllyMouseScroll(CurrentState, args.Delta);


            //var menuTable = new TableLayoutPanel();
            //menuTable.Size = ClientSize;
            //menuTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            //menuTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //for (var i = 0; i < 10; i++)
            //    for (var j = 0; j < 10; j++)
            //    {
            //        var menuLabel = new Label() { Text = $"MENU {i}, {j}", Font = new Font("Arial", 10, FontStyle.Underline) };
            //        menuTable.Controls.Add(menuLabel);
            //    }
            //Controls.Add(menuTable);
        }

        public void UpdateGameInfo(Graphics g)
        {
            g.DrawString($"{CurrentState.CommandsStack.Stack.Count} / {CurrentState.CommandsStack.Limit}", new Font("Arial", 15), Brushes.Red, new Point(0, 0));
            if(CurrentState.CurrentLevel.TimeLimit > 0)
                g.DrawString($"Time: {CurrentState.Time}", new Font("Arial", 15), Brushes.Red, new Point(100, 0));
            g.DrawString($"FPS: {FpsCounter.GetFps()}", new Font("Arial", 15), Brushes.Red, new Point(220, 0));
        }

        public void DrawDynamicCreatures(Graphics g) //Todo...
        {
            foreach (var dynamicCreature in CurrentSprites.Dynamic)
                if(dynamicCreature.Sprite.Visible)
                {
                    UpdateCreatureSprite(dynamicCreature);
                    if (dynamicCreature.Type == CreatureType.Button)
                        g.DrawImage(floorImage, dynamicCreature.Sprite.Rectangle);
                    g.DrawImage(dynamicCreature.Sprite.Image, dynamicCreature.Sprite.Rectangle);
                }
        }

        public void DrawStaticCreatures(Graphics g) //Todo...
        {
            foreach (var staticCreature in CurrentSprites.Static)
                if (staticCreature.Sprite.Visible)
                {
                    UpdateCreatureSprite(staticCreature);
                    g.DrawImage(staticCreature.Sprite.Image, staticCreature.Sprite.Rectangle);
                }
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
            }
            foreach(var cube in CurrentState.Cubes)
            {
                if (cube is null)
                    continue;
                cube.Sprite.Image.MakeTransparent(Color.White);
                UpdateCreatureSprite(cube);
                g.DrawImage(cube.Sprite.Image, cube.Sprite.Rectangle);
            }

            UpdateCreatureSprite(CurrentState.Player);
            CurrentState.Player.Sprite.Image.MakeTransparent(Color.White);//
            g.DrawImage(CurrentState.Player.Sprite.Image, CurrentState.Player.Sprite.Rectangle);
            UpdateCreatureSprite(CurrentState.Dummy);
            g.DrawImage(CurrentState.Dummy.Sprite.Image, CurrentState.Dummy.Sprite.Rectangle);
            if (CurrentState.IsWarped)
            {
                UpdateCreatureSprite(CurrentState.WarpedPlayer);
                CurrentState.WarpedPlayer.Sprite.Image.MakeTransparent(Color.White);//
                g.DrawImage(CurrentState.WarpedPlayer.Sprite.Image, CurrentState.WarpedPlayer.Sprite.Rectangle);
            }
        }

        public void UpdateCreatureSprite(Creature creature)
        {
            creature.Sprite.Rectangle.X = CurrentState.SpritesSize * creature.Location.X - CurrentState.SpritesSize * CurrentState.Map.GetLength(0) / 2 + creature.Sprite.Offset.X;
            creature.Sprite.Rectangle.Y = CurrentState.SpritesSize * creature.Location.Y - CurrentState.SpritesSize * CurrentState.Map.GetLength(0) / 2 + creature.Sprite.Offset.Y;
            creature.Sprite.Rectangle.Width = CurrentState.SpritesSize * creature.Sprite.SizePercent.Width / 100;
            creature.Sprite.Rectangle.Height = CurrentState.SpritesSize * creature.Sprite.SizePercent.Height / 100;
        }

        public void UpdateGUISprite(Sprite sprite)
        {
            sprite.Rectangle.Width = ClientSize.Width + 5;
            sprite.Rectangle.Height = ClientSize.Height + 5;
        }
    }
}
