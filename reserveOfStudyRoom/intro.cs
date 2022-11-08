using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace reserveOfStudyRoom
{
    public partial class intro : Form
    {
        public intro()
        {
            InitializeComponent();

            PrivateFontCollection privateFont = new PrivateFontCollection();
            privateFont.AddFontFile("font/NanumSquareNeo-bRg.ttf");
            Font font = new Font(privateFont.Families[0], 24f);
            Font font2 = new Font(privateFont.Families[0], 18f);
            studentCertification.Font = readingRoom.Font = studyRoom.Font = font;
            label1.Font = font2;
        }

        public void fontBorderSetting (ButtonBase btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
        }

        private void intro_Load(object sender, EventArgs e)
        {
            studentCertification.Left = (this.ClientSize.Width - studentCertification.Width) / 2;
            studentCertification.Top = (this.ClientSize.Height - studentCertification.Height) / 2;

            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2;

            fontBorderSetting(studentCertification);
            fontBorderSetting(readingRoom);
            fontBorderSetting(studyRoom);
        }

        private void studentCertification_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void readingRoom_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void studyRoom_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
