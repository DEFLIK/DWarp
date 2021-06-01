using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DWarp.Core.Drawing
{
    public class Menu
    {
        public readonly TableLayoutPanel Table;

        public Menu(MainForm linkedForm) // in progress...
        {
            Table = new TableLayoutPanel() { Visible = false };
            Table.SuspendLayout();
            Table.Size = new Size(linkedForm.ClientSize.Width / 2, linkedForm.ClientSize.Height);
            Table.RowStyles.Add(new RowStyle());
            Table.RowStyles.Add(new RowStyle());
            Table.RowStyles.Add(new RowStyle());
            Table.RowStyles.Add(new RowStyle());
            //menuTable.ColumnStyles.Add(new ColumnStyle());

            var logo = InitalizeMenuOption(Properties.Resources.Player);
            var resumeButton = InitalizeMenuOption(Properties.Resources.Player, (sender, args) => Table.Visible = false);
            var levelsButton = InitalizeMenuOption(Properties.Resources.Player, (sender, args) => Table.Visible = false); //todo
            var exitButton = InitalizeMenuOption(Properties.Resources.Dummy, (sender, args) => linkedForm.Close());

            Table.Controls.Add(logo);
            Table.Controls.Add(resumeButton);
            Table.Controls.Add(levelsButton);
            Table.Controls.Add(exitButton);
            //Table.Controls.Add(settingsButton);
            Table.BackColor = Color.FromArgb(100, 0, 0, 0);
            Table.Location = new Point(linkedForm.ClientSize.Width / 2 - Table.Width / 2, linkedForm.ClientSize.Height / 2 - Table.Height / 2);
            Table.ResumeLayout();
            linkedForm.SetDoubleBuffered(Table);
            linkedForm.Controls.Add(Table);
        }

        public void SwitchVisibility() => Table.Visible = Table.Visible ? false : true;

        private PictureBox InitalizeMenuOption(Image image, EventHandler action = null)
        {
            var newButton = new PictureBox();
            newButton.Size = new Size(Table.Width - 5, 100);
            newButton.SizeMode = PictureBoxSizeMode.StretchImage;
            newButton.Image = image;
            if(action != null)
                newButton.Click += action;
            return newButton;
        }
    }
}
