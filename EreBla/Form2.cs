using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EreBla
{
    public partial class GameMain : Form
    {
        System.IO.Stream playBGM;               // BGM用IOストリーム
        System.Media.SoundPlayer BGMplayer;     // BGM再生オブジェクト
        public int chara = 0;                   // キャラ番号(1=エレ,2=むい)

        public GameMain()
        {
            InitializeComponent();

            // スプラッシュスクリーンの表示
            SplashScreen ss = new SplashScreen();
            ss.Show();
            ss.Refresh();
            Thread.Sleep(3200);
            ss.Close();
            ss.Dispose();

            // キャラクタ選択画面の表示
            CharacterSelect cs = new CharacterSelect() {
                mm = this
            };
            cs.ShowDialog();
            cs.Dispose();
        }

        private void GameMain_Load(object sender, EventArgs e)
        {
            PlayGame();
        }

        // ゲームメインルーチン
        private void PlayGame()
        {
            // ループBGMの再生開始
            if (chara == 1)  {
                playBGM = Properties.Resources.BGMere1;
            } else {
                playBGM = Properties.Resources.BGMmui1;
            }
            // リソースを取得して音声再生
            BGMplayer = new System.Media.SoundPlayer(playBGM);
            BGMplayer.PlayLooping();

            // テスト表示（あとで消すよー）
            switch (chara) {
                case 0:
                    this.Close();
                    break;
                case 1:
                    TestLabel.Text = "エレ子とゲーム";
                    break;
                case 2:
                    TestLabel.Text = "むいとゲーム";
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        // もういっかいやるボタン
        private void RestartButton_Click(object sender, EventArgs e)
        {
            // 自分自身を隠して
            this.Hide();

            // キャラクタ選択画面の表示
            CharacterSelect cs = new CharacterSelect() {
                mm = this
            };
            cs.ShowDialog();
            cs.Dispose();

            // ゲーム開始
            this.Show();
            PlayGame();
        }

        private void GameMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 閉じたらループBGM終了
            BGMplayer.Stop();
            BGMplayer.Dispose();
        }
    }
}
