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
        public int chara = 0;                   // キャラ番号(1=エレ,2=むい)
        private System.IO.Stream playBGM;       // BGM用IOストリーム
        private System.Media.SoundPlayer playBGMplayer;   // BGM再生オブジェクト
        int[] CharaCard = new int[2];           // キャラの持ち手
        int[] PlayerCard = new int[2];          // プレイヤーの持ち手
        Random r = new System.Random();         // 乱数発生用

        // トランプのリソース名
        private string[,] card = {
            {"s01","s02","s03","s04","s05","s06","s07","s08","s09","s10","s11","s12","s13"},
            {"c01","c02","c03","c04","c05","c06","c07","c08","c09","c10","c11","c12","c13"},
            {"h01","h02","h03","h04","h05","h06","h07","h08","h09","h10","h11","h12","h13"},
            {"d01","d02","d03","d04","d05","d06","d07","d08","d09","d10","d11","d12","d13"}
        };

        // キャラ名
        private string[] charaname = { "","エレ子", "むいちゃん" };

        public GameMain()
        {
            // もとからあった初期化
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
            // カーソルをオリジナルのものに変更
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            Cursor myCursor = new Cursor(asm.GetManifestResourceStream(asm.GetName().Name + ".Resources.hand_cursor.cur"));
            this.Cursor = myCursor;

            // キャラクタが選ばれていたらゲーム開始
            if (chara ==1 || chara == 2) {
                PlayGame();
            } else {  // そうじゃなきゃ終了
                this.Close();
                this.Dispose();
            }
        }

        // ゲームメインルーチン
        private void PlayGame()
        {
            // 消えてるかもしれないのでボタン表示
            ButtonChange.Visible = true;
            ButtonChallenge.Visible = true;

            // ラベル表示
            LabelDealer.Text = charaname[chara];
            LabelPlayer.Text = "あなた";

            // キャラによるループBGMとイメージを設定
            switch (chara) {
                case 0:
                    this.Close();   // 誰も選んでいない
                    break;
                case 1:             // エレ子とゲーム
                    playBGM = Properties.Resources.BGMere1;
                    CharaPict.Image = Properties.Resources.ere_play;
                    break;
                case 2:             // むいとゲーム
                    playBGM = Properties.Resources.BGMmui1;
                    CharaPict.Image = Properties.Resources.mui_play;
                    break;
                default:            // ここには来ないはずだけど
                    this.Close();
                    break;
            }

            // リソースを取得してループBGM再生
            playBGMplayer = new System.Media.SoundPlayer(playBGM);
            playBGMplayer.PlayLooping();

            // キャラの２枚（乱数）
            CharaCard[0] = r.Next(0,25);
            CharaCard[1] = (CharaCard[0] + r.Next(1,24)) % 26;

            // キャラカードの表示（１枚は表、１枚は裏）
            CardPictChara1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(CharaCard[0] / 13) + 2, (int)(CharaCard[0] % 13)]);
            CardPictChara2.Image = (System.Drawing.Image)Properties.Resources.card_ura;

            // プレイヤーの２枚（乱数）
            PlayerCard[0] = r.Next(0, 25);
            PlayerCard[1] = (PlayerCard[0] + r.Next(1,24)) % 26;

            // プレイヤーカードの表示（２枚とも表）
            CardPictPlayer1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(PlayerCard[0] / 13), (int)(PlayerCard[0] % 13)]);
            CardPictPlayer2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(PlayerCard[1] / 13), (int)(PlayerCard[1] % 13)]);

            // 1/15の確率でプレイヤーにジョーカー
            if (r.Next(0, 15) >= 0) {
                if(r.Next(0,2) == 0) {
                    PlayerCard[0] = -1;
                    CardPictPlayer1.Image = (System.Drawing.Image)Properties.Resources.joker;
                } else {
                    PlayerCard[1] = -1;
                    CardPictPlayer2.Image = (System.Drawing.Image)Properties.Resources.joker;
                }
            }
        }

        // もういっかいやるボタン（動作確認用）
        private void RestartButton_Click(object sender, EventArgs e)
        {
            // 自分自身を隠して
            this.Hide();

            // キャラ選択画面の表示
            CharacterSelect cs = new CharacterSelect() {
                mm = this
            };
            cs.ShowDialog();
            cs.Dispose();

            // キャラ選択されていたらゲーム開始
            if (chara == 1 || chara == 2) {
                this.Show();
                PlayGame();
            } else {
                this.Close();
                this.Dispose();
            }
        }

        private void GameMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 閉じたらループBGM終了
            if(playBGMplayer != null) {
                playBGMplayer.Stop();
                playBGMplayer.Dispose();
            }
        }

        // 交換ボタンが押されたら
        private void ButtonChange_Click(object sender, EventArgs e)
        {
            // プレイヤーの２枚
            PlayerCard[0] = r.Next(0, 25);
            PlayerCard[1] = (CharaCard[0] + r.Next(1, 24)) % 26;

            // プレイヤーカードの表示
            CardPictPlayer1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(PlayerCard[0] / 13), (int)(PlayerCard[0] % 13)]);
            CardPictPlayer2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(PlayerCard[1] / 13), (int)(PlayerCard[1] % 13)]);

            // 1/13の確率でプレイヤーにジョーカー
            if (r.Next(0, 13) == 0) {
                if (r.Next(0, 2) == 0) {
                    PlayerCard[0] = -1;
                    CardPictPlayer1.Image = (System.Drawing.Image)Properties.Resources.joker;
                } else {
                    PlayerCard[1] = -1;
                    CardPictPlayer2.Image = (System.Drawing.Image)Properties.Resources.joker;
                }
            }

            // 交換は１回だけなので交換ボタンを非表示にする
            ButtonChange.Visible = false;
        }

        // 勝負ボタンが押されたら
        private async void ButtonChallenge_Click(object sender, EventArgs e)
        {
            int i;
            int chara_pt;           // キャラの値
            int player_pt;          // プレイヤーの値

            // ボタン関係を非表示
            ButtonChange.Visible = false;
            ButtonChallenge.Visible = false;

            // キャラカードをターン
            for (i = 0; i <= 10; i++) {
                CardPictChara2.Width = (int)(112 * (10 - i) / 10);
                await Task.Delay(50);
            }
            CardPictChara2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(CharaCard[1] / 13) + 2, (int)(CharaCard[1] % 13)]);
            for (i = 0; i <= 10; i++) {
                CardPictChara2.Width = (int)(112 * i / 10);
                await Task.Delay(50);
            }

            // 勝敗判定
            chara_pt = CalcPoint(CharaCard[0] % 13 + 1, CharaCard[1] % 13 + 1);
            player_pt = CalcPoint(PlayerCard[0] % 13 + 1, PlayerCard[1] % 13 + 1);
            LabelDealer.Text = charaname[chara] + "：" + chara_pt.ToString();
            LabelPlayer.Text = "あなた：" + player_pt.ToString();
        }

        private int CalcPoint(int a,int b)
        {
            int pt;

            // 絵札処理
            if (a > 10) a = 10;
            if (b > 10) b = 10;

            // エース処理（とりあえず11で考える）
            if (a == 1) a = 11;
            if (b == 1) b = 11;

            // ジョーカー処理（なるべく21に近づける）
            if (a == 0) {
                a = 11;
                if (a + b > 21) a = 21 - b;
            }
            if (b == 0) {
                b = 11;
                if (a + b > 21) b = 21 - a;
            }

            // 合計する
            pt = a + b;

            // 21を超えたとき（エースが11以外の理由はない）
            if (pt > 21) pt -= 10;

            return pt;
        }
    }
}
