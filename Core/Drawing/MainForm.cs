using System.Drawing;
using System.Windows.Forms;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Controls;
using DWarp.Core.Models;
using System.Reflection;

namespace DWarp.Core.Drawing
{
    public partial class MainForm : Form
    {
        public State CurrentState;
        public CreaturesSprites CurrentSprites;
        public readonly Menu MenuPanel;
        private static readonly Bitmap floorImage = Properties.Resources.Floor;
        private static readonly int threadsCount = 2;
        private (int X, int Y) centerOffset = (0, 0);

        public void ChangeState(State newState)
        {
            CurrentSprites = new CreaturesSprites(newState);
            CurrentState = newState;
        }

        public MainForm(State firstState)
        {
            InitializeComponent();
            ChangeState(firstState);
            for (var thread = 0; thread < threadsCount; thread++)
                InitializeInvalidater();

            MenuPanel = new Menu(this);
            var drawWires = false;

            var controlTable = new TableLayoutPanel();
            var wireLabel = new Label() { Text = "Show Wires", Font = new Font("Arial", 12, FontStyle.Underline), BackColor = Color.Black, ForeColor = Color.White };
            controlTable.Size = new Size(106, 25);
            wireLabel.Click += (sender, args) => drawWires = drawWires ? false : true;
            controlTable.Controls.Add(wireLabel);
            Controls.Add(controlTable);

            Paint += (sender, args) =>
            {
                args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                args.Graphics.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
                DrawStaticCreatures(args.Graphics);
                DrawDynamicCreatures(args.Graphics);
                args.Graphics.TranslateTransform(
                    -CurrentState.Map.GetLength(0) * CurrentState.SpritesSize / 2 + CurrentState.SpritesSize / 2,
                    -CurrentState.Map.GetLength(1) * CurrentState.SpritesSize / 2 + CurrentState.SpritesSize / 2);
                args.Graphics.TranslateTransform(centerOffset.X, centerOffset.Y);
                if (drawWires)
                    DrawWires(args);
                args.Graphics.ResetTransform();
                args.Graphics.DrawImage(UISprites.Vignette.Image, UISprites.Vignette.Rectangle);
                if (UISprites.WarpVignette.Visible)
                {
                    args.Graphics.DrawImage(UISprites.WarpVignette.Image, UISprites.WarpVignette.Rectangle);
                    UpdateUISprite(UISprites.WarpVignette);
                }
                UpdateGameInfo(args.Graphics);
                FpsCounter.Elapsed();
            };

            KeyDown += (sender, args) => InputControl.ApplyKey(CurrentState, args.KeyCode);

            Load += (sender, args) => OnSizeChanged(args);

            SizeChanged += (sender, args) =>
            {
                controlTable.Location = new Point(ClientSize.Width - controlTable.Width, 0);
                MenuPanel.MainTable.Size = new Size(450, ClientSize.Height);
                MenuPanel.MainTable.Location = new Point(ClientSize.Width / 2 - MenuPanel.MainTable.Width / 2, ClientSize.Height / 2 - MenuPanel.MainTable.Height / 2);
                MenuPanel.LevelsTable.Size = new Size(ClientSize.Width, ClientSize.Height / 2);
                MenuPanel.LevelsTable.Location = new Point(ClientSize.Width / 2 - MenuPanel.LevelsTable.Width / 2, ClientSize.Height / 2 - MenuPanel.LevelsTable.Height / 2);
                MenuPanel.InstructionImage.Size = ClientSize;
                UpdateUISprite(UISprites.Vignette);
                UpdateUISprite(UISprites.WarpVignette);
            };

            MouseWheel += (sender, args) => InputControl.ApllyMouseScroll(CurrentState, args.Delta);
        }

        private void InitializeInvalidater()
        {
            var InvalidatingTimer = new Timer();
            InvalidatingTimer.Interval = 1;
            InvalidatingTimer.Tick += (sender, args) => Invalidate();
            InvalidatingTimer.Start();
        }

        private void DrawWires(PaintEventArgs args)
        {
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
        }

        private void UpdateGameInfo(Graphics g)
        {
            g.DrawString($"{CurrentState.CommandsStack.Stack.Count} / {CurrentState.CommandsStack.Limit}", 
                new Font("Arial", 15, FontStyle.Bold), 
                CurrentState.CommandsStack.Stack.Count == CurrentState.CommandsStack.Limit ? Brushes.Red : Brushes.AntiqueWhite, 
                new Point(ClientSize.Width - 70, ClientSize.Height - 30));
            if (CurrentState.CurrentLevel.TimeLimit > 0)
                g.DrawString($"TIME LEFT: {CurrentState.Time}", 
                    new Font("Arial", 15, FontStyle.Bold), 
                    CurrentState.Time == CurrentState.CurrentLevel.TimeLimit ? Brushes.DarkRed : Brushes.Red, 
                    new Point(0, ClientSize.Height - 30));
            g.DrawString($"FPS: {FpsCounter.GetFps()}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, Point.Empty);
        }

        private void DrawDynamicCreatures(Graphics g)
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

        private void DrawStaticCreatures(Graphics g)
        {
            foreach (var staticCreature in CurrentSprites.Static)
                if (staticCreature.Sprite.Visible)
                {
                    UpdateCreatureSprite(staticCreature);
                    g.DrawImage(staticCreature.Sprite.Image, staticCreature.Sprite.Rectangle);
                }
        }

        private void UpdateCreatureSprite(Creature creature)
        {
            creature.Sprite.Rectangle.X = CurrentState.SpritesSize * creature.Location.X - CurrentState.SpritesSize * CurrentState.Map.GetLength(0) / 2 + creature.Sprite.Offset.X;
            creature.Sprite.Rectangle.Y = CurrentState.SpritesSize * creature.Location.Y - CurrentState.SpritesSize * CurrentState.Map.GetLength(0) / 2 + creature.Sprite.Offset.Y;
            creature.Sprite.Rectangle.Width = CurrentState.SpritesSize * creature.Sprite.SizePercent.Width / 100;
            creature.Sprite.Rectangle.Height = CurrentState.SpritesSize * creature.Sprite.SizePercent.Height / 100;
        }

        private void UpdateUISprite(Sprite sprite)
        {
            sprite.Rectangle.Width = (ClientSize.Width + 5) * sprite.SizePercent.Width / 100;
            sprite.Rectangle.Height = (ClientSize.Height + 5) * sprite.SizePercent.Height / 100;
        }

        public void SetDoubleBuffered(Control control)
        {
            if (SystemInformation.TerminalServerSession)
                return;
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(control, true, null);
        }

        public void ShowWinInfo() => 
            MessageBox.Show("-Ты и вправду так сильно хочешь выбраться? Прости, но ты навсегда в моем цикле испытаний...\n\nИгра пройдена!", "???", MessageBoxButtons.OK);
    }
}
