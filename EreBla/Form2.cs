using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;


namespace EreBla
{
    public partial class GameMain : Form
    {
        public int chara = 0;                   // キャラ番号(1=エレ,2=むい)
        private System.IO.Stream playBGM;       // BGM用IOストリーム
        private System.Media.SoundPlayer playBGMplayer;   // BGM再生オブジェクト
        private NAudio.Wave.WaveOut pl;         // セリフ再生オブジェクト
        int[] CharaCard = new int[2];           // キャラの持ち手
        int[] PlayerCard = new int[2];          // プレイヤーの持ち手
        Random r = new System.Random();         // 乱数発生用
        int[,] result = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; // 勝敗分回数(0:ダミー, 1:エレ子, 2:むい)

        // トランプのリソース名
        private string[,] card = {
            {"s01","s02","s03","s04","s05","s06","s07","s08","s09","s10","s11","s12","s13"},
            {"c01","c02","c03","c04","c05","c06","c07","c08","c09","c10","c11","c12","c13"},
            {"h01","h02","h03","h04","h05","h06","h07","h08","h09","h10","h11","h12","h13"},
            {"d01","d02","d03","d04","d05","d06","d07","d08","d09","d10","d11","d12","d13"}
        };

        // キャラ名
        private string[] charaname = { "", "エレ子", "むいちゃん" };

        // キャラのイメージリソース名
        private string[,] ImgWin = { { "ere_win1", "ere_win2", "ere_win3" }, { "mui_win1", "mui_win2", "mui_win3" } };
        private string[,] ImgDraw = { { "ere_draw1", "ere_draw2", "ere_draw3" }, { "mui_draw1", "mui_draw2", "mui_draw3" } };
        private string[,] ImgLose = { { "ere_lose1", "ere_lose2", "ere_lose3" }, { "mui_lose1", "mui_lose2", "mui_lose3" } };

        // キャラのセリフリソース名
        private string[] TalkPlay = { "playE", "playM" };
        private string[,] TalkWin = { { "winE1", "winE2" }, { "winM1", "winM2" } };
        private string[] TalkDraw = { "drawE", "drawM" };
        private string[,] TalkLose = { { "loseE1", "loseE2" }, { "loseM1", "loseM2" } };

        public GameMain()
        {
            // もとからあった初期化
            InitializeComponent();

            // セリフ用オブジェクト生成
            pl = new NAudio.Wave.WaveOut();

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
        private async void PlayGame()
        {
            // ボタンを非表示
            ButtonChange.Visible = false;
            ButtonChallenge.Visible = false;
            RestartButton.Visible = false;
            OneMoreButton.Visible = false;

            // ラベル表示
            LabelDealer.Text = charaname[chara];
            LabelPlayer.Text = "あなた";
            DispLabelResult();

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

            // 1/52の確率でプレイヤーにジョーカー
            if (r.Next(0, 52) == 0) {
                if(r.Next(0, 2) == 0) {
                    PlayerCard[0] = -1;
                    CardPictPlayer1.Image = (System.Drawing.Image)Properties.Resources.joker;
                } else {
                    PlayerCard[1] = -1;
                    CardPictPlayer2.Image = (System.Drawing.Image)Properties.Resources.joker;
                }
            }
            // キャラセリフ
            await Speak(TalkPlay[chara-1]);

            // ボタンの表示（マルチスレッド対応メイン）
            if (this.InvokeRequired) {
                this.Invoke(new Action(this.DispButtonSub));
            } else {
                ButtonChange.Visible = true;
                ButtonChallenge.Visible = true;
            }
        }

        // ボタンの表示（マルチスレッド対応サブ）
        private void DispButtonSub()
        {
            ButtonChange.Visible = true;
            ButtonChallenge.Visible = true;
        }

        // もういっかいボタン
        private void OneMoreButton_Click(object sender, EventArgs e)
        {
            PlayGame();
        }

        // メニューに戻るボタン
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

            // セリフ用オブジェクトの破棄
            pl.Dispose();
        }

        // 交換ボタンが押されたら
        private void ButtonChange_Click(object sender, EventArgs e)
        {
            // 配る効果音
            Speak("cardDistribute");

            // プレイヤーの２枚
            PlayerCard[0] = r.Next(0, 25);
            PlayerCard[1] = (PlayerCard[0] + r.Next(1, 23)) % 26;

            // プレイヤーカードの表示
            CardPictPlayer1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(PlayerCard[0] / 13), (int)(PlayerCard[0] % 13)]);
            CardPictPlayer2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(PlayerCard[1] / 13), (int)(PlayerCard[1] % 13)]);

            // 1/50の確率でプレイヤーにジョーカー
            if (r.Next(0, 50) == 0) {
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

            // カードを開ける効果音
            Speak("cardOpen");

            // キャラカードをターン
            for (i = 0; i <= 10; i++) {
                CardPictChara2.Width = (int)(78 * (10 - i) / 10);
                await Task.Delay(50);
            }
            CardPictChara2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(card[(int)(CharaCard[1] / 13) + 2, (int)(CharaCard[1] % 13)]);
            for (i = 0; i <= 10; i++) {
                CardPictChara2.Width = (int)(78 * i / 10);
                await Task.Delay(50);
            }

            // 得点計算
            chara_pt = CalcPoint(CharaCard[0] % 13 + 1, CharaCard[1] % 13 + 1);
            player_pt = CalcPoint(PlayerCard[0] % 13 + 1, PlayerCard[1] % 13 + 1);
            LabelDealer.Text = charaname[chara] + "：" + chara_pt.ToString();
            LabelPlayer.Text = "あなた：" + player_pt.ToString();

            // ループBGMをとめる
            playBGMplayer.Stop();
            playBGMplayer.Dispose();

            // リソースを取得してゲーム終了BGM再生
            if(chara == 1) {
                playBGM = Properties.Resources.BGMere2;
            }
            else {
                playBGM = Properties.Resources.BGMmui2;
            }
            playBGMplayer = new System.Media.SoundPlayer(playBGM);
            playBGMplayer.Play();

            // 勝敗判定
            if (PlayerCard[0]==-1 || PlayerCard[1] == -1){
                result[chara, 0]++;
                DispLabelResult();
                LabelPlayer.Text = "あなた：Joker";
                await YouWin();
            } else if(chara_pt < player_pt) {
                result[chara, 0]++;
                DispLabelResult();
                await YouWin();
            } else if(chara_pt == player_pt) {
                result[chara, 2]++;
                DispLabelResult();
                await YouDraw();
            } else {
                result[chara, 1]++;
                DispLabelResult();
                await YouLose();
            }
            OneMoreButton.Visible = true;
            RestartButton.Visible = true;
        }

        // 対キャラ成績の表示（マルチスレッド対応メイン）
        private void DispLabelResult()
        {
            if (this.InvokeRequired) {
                this.Invoke(new Action(this.DispLabelResultSub));
            } else {
                LabelResult.Text = "対" + charaname[chara] + "成績：" + result[chara, 0] + "勝" + result[chara, 1] + "敗" + result[chara, 2] + "分";
            }
        }

        // 対キャラ成績の表示（マルチスレッド対応サブ）
        private void DispLabelResultSub()
        {
            LabelResult.Text = "対" + charaname[chara] + "成績：" + result[chara, 0] + "勝" + result[chara, 1] + "敗" + result[chara, 2] + "分";
        }

        // カードの得点計算
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

        // プレイヤー勝利
        private async Task YouWin()
        {
            
            CharaPict.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(ImgLose[chara - 1, 0]);
            await Task.Delay(200);
            CharaPict.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(ImgLose[chara - 1, 1]);
            await Task.Delay(800);
            if (result[1,0] == 108) {   // エレ子に108回勝ったら別画像
                CharaPict.Image = (System.Drawing.Image)Properties.Resources.ele_lose102;
            } else {
                CharaPict.Image = (System.Drawing.Image)(System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(ImgLose[chara - 1, 2]);
            }
            await Speak(TalkLose[chara - 1, r.Next(2)]);
        }

        // プレイヤー引き分け
        private async Task YouDraw()
        {
            CharaPict.Image = (Image)Properties.Resources.ResourceManager.GetObject(ImgDraw[chara - 1, 0]);
            await Task.Delay(200);
            CharaPict.Image = (Image)Properties.Resources.ResourceManager.GetObject(ImgDraw[chara - 1, 1]);
            await Task.Delay(800);
            CharaPict.Image = (Image)Properties.Resources.ResourceManager.GetObject(ImgDraw[chara - 1, 2]);
            await Speak(TalkDraw[chara - 1]);
        }
        // プレイヤー敗北
        private async Task YouLose()
        {
            CharaPict.Image = (Image)Properties.Resources.ResourceManager.GetObject(ImgWin[chara - 1, 0]);
            await Task.Delay(200);
            CharaPict.Image = (Image)Properties.Resources.ResourceManager.GetObject(ImgWin[chara - 1, 1]);
            await Task.Delay(800);
            CharaPict.Image = (Image)Properties.Resources.ResourceManager.GetObject(ImgWin[chara - 1, 2]);
            await Speak(TalkWin[chara - 1, r.Next(2)]);
        }

        // セリフ音声の再生
        private async Task Speak(string s)
        {
            byte[] buffer = (byte[])Properties.Resources.ResourceManager.GetObject(s);
            using var stream = new MemoryStream(buffer);
            using WaveStream pcm = new Mp3FileReader(stream);
            pl.Init(pcm);
            pl.Play();
            while (pl.PlaybackState == PlaybackState.Playing) {
                await Task.Delay(10);
            }
        }
    }
}
