using System;
using System.Drawing;
using System.Windows.Forms;
using DWarp.Resources.Levels;

namespace DWarp.Core.Drawing
{
    public class Menu
    {
        public readonly TableLayoutPanel MainTable;
        public readonly TableLayoutPanel LevelsTable;

        public Menu(MainForm linkedForm)
        {
            MainTable = new TableLayoutPanel()
            {
                Size = new Size(linkedForm.ClientSize.Width / 2, linkedForm.ClientSize.Height),
                BackColor = Color.FromArgb(100, 0, 0, 0),
            };
            MainTable.Location = new Point(linkedForm.ClientSize.Width / 2 - MainTable.Width / 2, linkedForm.ClientSize.Height / 2 - MainTable.Height / 2);

            LevelsTable = InitializeLevelSelector(linkedForm);

            var tutorialImage = new PictureBox()
            {
                Visible = false,
                Image = Properties.Resources.Instructions,
                Size = linkedForm.ClientSize,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            tutorialImage.Click += (sender, args) => tutorialImage.Visible = false;

            var logo = InitalizeMenuOption(Properties.Resources.Logo);
            var resumeButton = InitalizeMenuOption(Properties.Resources.Resume, (sender, args) => MainTable.Visible = false);
            var levelsButton = InitalizeMenuOption(Properties.Resources.Select, (sender, args) => SwitchLevelSelector());
            var tutorialButton = InitalizeMenuOption(Properties.Resources.Tutorial, (sender, args) => 
            {
                tutorialImage.Visible = true;
                LevelsTable.Visible = false;
                MainTable.Visible = false;
            });
            var exitButton = InitalizeMenuOption(Properties.Resources.Close, (sender, args) =>
            {
                GC.Collect();
                linkedForm.Close();
            });

            MainTable.Controls.Add(logo);
            MainTable.Controls.Add(resumeButton);
            MainTable.Controls.Add(levelsButton);
            MainTable.Controls.Add(tutorialButton);
            MainTable.Controls.Add(exitButton);

            linkedForm.Controls.Add(LevelsTable);
            linkedForm.SetDoubleBuffered(MainTable);
            linkedForm.Controls.Add(MainTable);
            linkedForm.Controls.Add(tutorialImage);
        }

        public void SwitchVisibility() => MainTable.Visible = !MainTable.Visible;

        private PictureBox InitalizeMenuOption(Image image, EventHandler action = null)
        {
            var newButton = new PictureBox();
            newButton.Size = new Size(MainTable.Width - 5, 100);
            newButton.SizeMode = PictureBoxSizeMode.StretchImage;
            newButton.Image = image;
            if (action != null)
                newButton.Click += action;
            return newButton;
        }

        private TableLayoutPanel InitializeLevelSelector(MainForm linkedForm)
        {
            var resultTable = new TableLayoutPanel() 
            {
                Visible = false,
                BackColor = Color.FromArgb(200, 0, 0, 0),
                Size = new Size(linkedForm.ClientSize.Width, linkedForm.ClientSize.Height / 2),
                ColumnCount = 4,
                RowCount = 3
            };
            resultTable.Location = new Point(linkedForm.ClientSize.Width / 2 - resultTable.Width / 2, linkedForm.ClientSize.Height / 2 - resultTable.Height / 2);
            linkedForm.SetDoubleBuffered(resultTable);
            var exitButton = InitializeLevelLabel("CLOSE", Color.DarkRed);
            exitButton.Click += (sender, args) => LevelsTable.Visible = false;
            resultTable.Controls.Add(exitButton);

            foreach (var level in Presets.Levels)
            {
                var levelLabel = InitializeLevelLabel(level.Key, Color.White);
                levelLabel.Click += (sender, args) =>
                {
                    Game.ChangeLevel(level.Value);
                    LevelsTable.Visible = false;
                    MainTable.Visible = false;
                };
                resultTable.Controls.Add(levelLabel);
            }

            return resultTable;
        }

        private Label InitializeLevelLabel(string text, Color color) => 
            new Label()
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Bazaronite", 25, FontStyle.Bold),
                Size = new Size(MainTable.Width / 2, MainTable.Height / 6),
                ForeColor = color,
                BackColor = Color.Black,
            };

        private void SwitchLevelSelector() => LevelsTable.Visible = !LevelsTable.Visible;
    }
}
