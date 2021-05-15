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
        public int chara = 0;

        public GameMain()
        {
            InitializeComponent();

            // スプラッシュスクリーンの表示
            SplashScreen ss = new SplashScreen();
            ss.Show();
            ss.Refresh();
            Thread.Sleep(3500);
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

        private void PlayGame()
        {
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

    }
}
