using System;
using System.Windows.Forms;

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
            System.IO.Stream titlecall;     // タイトルコール用IOストリーム

            // 取得時刻の秒が偶数だったらエレ子そうじゃなかったらむいちゃん
            if (dt.Second % 2 == 0) {
                titlecall = Properties.Resources.titleE;
            } else {
                titlecall = Properties.Resources.titleM;
            }

            // リソースを取得して音声再生
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(titlecall);
            player.Play();
            player.Dispose();
        }
    }
}
