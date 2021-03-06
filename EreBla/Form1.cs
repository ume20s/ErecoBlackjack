using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace EreBla
{
    public partial class CharacterSelect : Form
    {
        public GameMain mm;                     // メインにキャラクタ番号を受け渡すためのオブジェクト
        private SpStream selectBGM;                 // BGM用IOストリーム
        private System.Media.SoundPlayer selectBGMplayer;   // BGM再生オブジェクト

        public CharacterSelect()
        {
            // もとからあった初期化
            InitializeComponent();

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
            // ループBGMの再生開始
            selectBGM = new SpStream(Properties.Resources.BGMselect1);
            selectBGM.Volume = mm.Volume;
            sliderVolume.Value = mm.Volume;
            selectBGMplayer = new System.Media.SoundPlayer(selectBGM);
            selectBGMplayer.PlayLooping();

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
            selectBGMplayer.Stop();
            selectBGMplayer.Dispose();

            // リソースを取得して選択終了BGM再生
            selectBGM.Dispose();
            selectBGM = new SpStream(Properties.Resources.BGMselect2);
            selectBGM.Volume = sliderVolume.Value;
            selectBGMplayer = new System.Media.SoundPlayer(selectBGM);
            selectBGMplayer.Play();

            // エレ子よろこんで選択画面終了
            Speach("selectE");
            await EreSelected();
            selectBGMplayer.Dispose();
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
            selectBGMplayer.Stop();
            selectBGMplayer.Dispose();

            // リソースを取得して選択終了BGM再生
            selectBGM.Dispose();
            selectBGM = new SpStream(Properties.Resources.BGMselect2);
            selectBGM.Volume = sliderVolume.Value;
            selectBGMplayer = new System.Media.SoundPlayer(selectBGM);
            selectBGMplayer.Play();

            // むいちゃんとまどって選択画面終了
            Speach("selectM");
            await MuiSelected();
            selectBGMplayer.Dispose();
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
            NAudio.Wave.WaveOut player;

            player = new NAudio.Wave.WaveOut(); 
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

        // マウスが乗っかっただけで音量調節スライダーを表示する
        private void LabelVolumeCtrl_MouseEnter(object sender, EventArgs e)
        {
            sliderVolume.Visible = true;
        }

        // マウスが範囲を外れると音量調整スライダーを非表示にする
        private void sliderVolume_MouseLeave(object sender, EventArgs e)
        {
            sliderVolume.Visible = false;
        }

        // マウスupで音量を再設定
        private void sliderVolume_MouseUp(object sender, MouseEventArgs e)
        {
            mm.Volume = sliderVolume.Value;
            selectBGM.Dispose();
            selectBGMplayer.Stop();
            selectBGMplayer.Dispose();
            selectBGM = new SpStream(Properties.Resources.BGMselect1);
            selectBGM.Volume = sliderVolume.Value;
            selectBGMplayer = new System.Media.SoundPlayer(selectBGM);
            selectBGMplayer.PlayLooping();
        }
    }
}
