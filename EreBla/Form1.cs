using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using NAudio.Wave;

namespace EreBla
{
    public partial class CharacterSelect : Form
    {
        public GameMain mm;                     // メインにキャラクタ番号を受け渡すためのオブジェクト
        System.IO.Stream selectBGM;             // BGM用IOストリーム
        System.Media.SoundPlayer BGMplayer;     // BGM再生オブジェクト

        // セリフ再生用NAudioオブジェクト
        private NAudio.Wave.WaveOut player = new NAudio.Wave.WaveOut();

        public CharacterSelect()
        {
            // もとからあった初期化
            InitializeComponent();

            // ループBGMの再生開始
            selectBGM = Properties.Resources.BGMselect11;
            BGMplayer = new System.Media.SoundPlayer(selectBGM);
            BGMplayer.PlayLooping();
        }

        // カーソル処理関連
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

        // キャラ選択画面
        private void Form1_Load(object sender, EventArgs e)
        {
            // カーソルをオリジナルのものに変更
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            Cursor myCursor = new Cursor(asm.GetManifestResourceStream(asm.GetName().Name + ".Resources.hand_cursor.cur"));
            this.Cursor = myCursor;

            // キャラクタの初期化
            mm.chara = 0;
        }

        // エレ子選択
        private async void PictEreBig_Click(object sender, EventArgs e)
        {
            // エレ子（１）を選択
            mm.chara = 1;

            // ループBGMをとめる
            BGMplayer.Stop();
            BGMplayer.Dispose();

            // リソースを取得して選択終了BGM再生
            selectBGM = Properties.Resources.BGMselect21;
            BGMplayer = new System.Media.SoundPlayer(selectBGM);
            BGMplayer.Play();

            // エレ子よろこんで選択画面終了
            Speach("selectE");
            await EreSelected();
            BGMplayer.Dispose();
            this.Close();
        }

        // エレ子よろこぶアニメ
        private async Task EreSelected()
        {
            PictEreBig.Image = Properties.Resources.ere_select1;
            PictEreSmall.Image = Properties.Resources.ere_select1;
            await Task.Delay(300);
            PictEreBig.Image = Properties.Resources.ere_select2;
            PictEreSmall.Image = Properties.Resources.ere_select2;
            await Task.Delay(300);
            PictEreBig.Image = Properties.Resources.ere_select3;
            PictEreSmall.Image = Properties.Resources.ere_select3;
            await Task.Delay(1500);
        }

        // むいちゃん選択
        private async void PictMuiBig_Click(object sender, EventArgs e)
        {
            // むいちゃん（２）を選択
            mm.chara = 2;

            //  ループBGMをとめる
            BGMplayer.Stop();
            BGMplayer.Dispose();

            // リソースを取得して選択終了BGM再生
            selectBGM = Properties.Resources.BGMselect21;
            BGMplayer = new System.Media.SoundPlayer(selectBGM);
            BGMplayer.Play();

            // むいちゃんとまどって選択画面終了
            Speach("selectM");
            await MuiSelected();
            BGMplayer.Dispose();
            this.Close();
        }

        // むいちゃんとまどいアニメ
        private async Task MuiSelected()
        {
            PictMuiBig.Image = Properties.Resources.mui_select1;
            PictMuiSmall.Image = Properties.Resources.mui_select1;
            await Task.Delay(300);
            PictMuiBig.Image = Properties.Resources.mui_select2;
            PictMuiSmall.Image = Properties.Resources.mui_select2;
            await Task.Delay(300);
            PictMuiBig.Image = Properties.Resources.mui_select3;
            PictMuiSmall.Image = Properties.Resources.mui_select3;
            await Task.Delay(1500);
        }

        // セリフ音声の再生
        private async void Speach(string s)
        {
            byte[] buffer = (byte[])Properties.Resources.ResourceManager.GetObject(s);
            using var stream = new MemoryStream(buffer);
            using WaveStream pcm = new Mp3FileReader(stream);
            player.Init(pcm);
            player.Play();
            while (player.PlaybackState == PlaybackState.Playing) {
                await Task.Delay(10);
            }
            player.Dispose();
        }
    }
}
