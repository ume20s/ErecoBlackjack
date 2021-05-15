using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace EreBla
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        // スプラッシュスクリーンの表示
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;     // 日付時刻取得用構造体
            System.IO.Stream strm;          // 音声再生用IOストリーム

            // 取得時刻の秒が偶数だったらエレ子そうじゃなかったらむいちゃん
            if (dt.Second % 2 == 0) { 
                strm = Properties.Resources.titleE;
            } else {
                strm = Properties.Resources.titleM;
            }

            // リソースを取得して音声再生
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(strm);
            player.Play();
            player.Dispose();
        }
    }
}
