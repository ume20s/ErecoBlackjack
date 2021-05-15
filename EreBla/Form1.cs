using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EreBla
{
    public partial class CharacterSelect : Form
    {
        public GameMain mm;

        public CharacterSelect()
        {
            InitializeComponent();
        }

        private void PictEleBig_MouseEnter(object sender, EventArgs e)
        {
            PictMuiBig.Visible = true;
        }

        private void PictEleBig_MouseHover(object sender, EventArgs e)
        {
            PictMuiBig.Visible = true;
        }

        private void PictEleBig_MouseMove(object sender, MouseEventArgs e)
        {
            PictMuiBig.Visible = true;
        }

        private void PictEleBig_MouseLeave(object sender, EventArgs e)
        {
            PictMuiBig.Visible = false;
        }

        private void PictEleSmall_MouseEnter(object sender, EventArgs e)
        {
            PictMuiBig.Visible = true;
        }

        private void PictEreBig_MouseEnter(object sender, EventArgs e)
        {
            PictEreBig.Visible = true;
        }

        private void PictEreBig_MouseHover(object sender, EventArgs e)
        {
            PictEreBig.Visible = true;
        }

        private void PictEreBig_MouseMove(object sender, MouseEventArgs e)
        {
            PictEreBig.Visible = true;
        }

        private void PictEreBig_MouseLeave(object sender, EventArgs e)
        {
            PictEreBig.Visible = false;
        }

        private void PictEreSmall_MouseEnter(object sender, EventArgs e)
        {
            PictEreBig.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // カーソルをオリジナルのものに変更
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            Cursor myCursor = new Cursor(asm.GetManifestResourceStream(asm.GetName().Name + ".Resources.cursor.cur"));
            this.Cursor = myCursor;
        }

        private async void PictEreBig_Click(object sender, EventArgs e)
        {
            mm.chara = 1;
            await EreSelected();
            this.Close();
        }

        private async Task EreSelected()
        {
            PictEreBig.Image = Properties.Resources.ere_select1;
            PictEreSmall.Image = Properties.Resources.ere_select1;
            await Task.Delay(500);
            PictEreBig.Image = Properties.Resources.ere_select2;
            PictEreSmall.Image = Properties.Resources.ere_select2;
            await Task.Delay(500);
            PictEreBig.Image = Properties.Resources.ere_select3;
            PictEreSmall.Image = Properties.Resources.ere_select3;
            await Task.Delay(2000);
        }

        private async void PictMuiBig_Click(object sender, EventArgs e)
        {
            mm.chara = 2;
            await MuiSelected();
            this.Close();
        }

        private async Task MuiSelected()
        {
            PictMuiBig.Image = Properties.Resources.mui_select1;
            PictMuiSmall.Image = Properties.Resources.mui_select1;
            await Task.Delay(500);
            PictMuiBig.Image = Properties.Resources.mui_select2;
            PictMuiSmall.Image = Properties.Resources.mui_select2;
            await Task.Delay(500);
            PictMuiBig.Image = Properties.Resources.mui_select3;
            PictMuiSmall.Image = Properties.Resources.mui_select3;
            await Task.Delay(2000);
        }
    }
}
