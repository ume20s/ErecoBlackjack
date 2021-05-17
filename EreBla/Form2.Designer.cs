
namespace EreBla
{
    partial class GameMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RestartButton = new System.Windows.Forms.Button();
            this.CharaPict = new System.Windows.Forms.PictureBox();
            this.CardPictPlayer1 = new System.Windows.Forms.PictureBox();
            this.CardPictPlayer2 = new System.Windows.Forms.PictureBox();
            this.ButtonChange = new System.Windows.Forms.PictureBox();
            this.ButtonChallenge = new System.Windows.Forms.PictureBox();
            this.CardPictChara1 = new System.Windows.Forms.PictureBox();
            this.CardPictChara2 = new System.Windows.Forms.PictureBox();
            this.LabelPlayer = new System.Windows.Forms.Label();
            this.LabelDealer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CharaPict)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardPictPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardPictPlayer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonChallenge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardPictChara1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardPictChara2)).BeginInit();
            this.SuspendLayout();
            // 
            // RestartButton
            // 
            this.RestartButton.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RestartButton.Location = new System.Drawing.Point(624, 12);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(118, 41);
            this.RestartButton.TabIndex = 1;
            this.RestartButton.Text = "動作確認";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // CharaPict
            // 
            this.CharaPict.Location = new System.Drawing.Point(260, 12);
            this.CharaPict.Name = "CharaPict";
            this.CharaPict.Size = new System.Drawing.Size(225, 270);
            this.CharaPict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CharaPict.TabIndex = 2;
            this.CharaPict.TabStop = false;
            // 
            // CardPictPlayer1
            // 
            this.CardPictPlayer1.Location = new System.Drawing.Point(282, 304);
            this.CardPictPlayer1.Name = "CardPictPlayer1";
            this.CardPictPlayer1.Size = new System.Drawing.Size(144, 207);
            this.CardPictPlayer1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CardPictPlayer1.TabIndex = 3;
            this.CardPictPlayer1.TabStop = false;
            // 
            // CardPictPlayer2
            // 
            this.CardPictPlayer2.Location = new System.Drawing.Point(456, 304);
            this.CardPictPlayer2.Name = "CardPictPlayer2";
            this.CardPictPlayer2.Size = new System.Drawing.Size(144, 207);
            this.CardPictPlayer2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CardPictPlayer2.TabIndex = 4;
            this.CardPictPlayer2.TabStop = false;
            // 
            // ButtonChange
            // 
            this.ButtonChange.Image = global::EreBla.Properties.Resources.BtnChange;
            this.ButtonChange.Location = new System.Drawing.Point(142, 544);
            this.ButtonChange.Name = "ButtonChange";
            this.ButtonChange.Size = new System.Drawing.Size(157, 57);
            this.ButtonChange.TabIndex = 5;
            this.ButtonChange.TabStop = false;
            this.ButtonChange.Click += new System.EventHandler(this.ButtonChange_Click);
            // 
            // ButtonChallenge
            // 
            this.ButtonChallenge.Image = global::EreBla.Properties.Resources.BtnChallenge;
            this.ButtonChallenge.Location = new System.Drawing.Point(479, 544);
            this.ButtonChallenge.Name = "ButtonChallenge";
            this.ButtonChallenge.Size = new System.Drawing.Size(148, 57);
            this.ButtonChallenge.TabIndex = 6;
            this.ButtonChallenge.TabStop = false;
            this.ButtonChallenge.Click += new System.EventHandler(this.ButtonChallenge_Click);
            // 
            // CardPictChara1
            // 
            this.CardPictChara1.Location = new System.Drawing.Point(41, 137);
            this.CardPictChara1.Name = "CardPictChara1";
            this.CardPictChara1.Size = new System.Drawing.Size(112, 161);
            this.CardPictChara1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CardPictChara1.TabIndex = 7;
            this.CardPictChara1.TabStop = false;
            // 
            // CardPictChara2
            // 
            this.CardPictChara2.Location = new System.Drawing.Point(172, 137);
            this.CardPictChara2.Name = "CardPictChara2";
            this.CardPictChara2.Size = new System.Drawing.Size(112, 161);
            this.CardPictChara2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CardPictChara2.TabIndex = 8;
            this.CardPictChara2.TabStop = false;
            // 
            // LabelPlayer
            // 
            this.LabelPlayer.AutoSize = true;
            this.LabelPlayer.Location = new System.Drawing.Point(624, 304);
            this.LabelPlayer.Name = "LabelPlayer";
            this.LabelPlayer.Size = new System.Drawing.Size(39, 15);
            this.LabelPlayer.TabIndex = 9;
            this.LabelPlayer.Text = "Player";
            // 
            // LabelDealer
            // 
            this.LabelDealer.AutoSize = true;
            this.LabelDealer.Location = new System.Drawing.Point(41, 101);
            this.LabelDealer.Name = "LabelDealer";
            this.LabelDealer.Size = new System.Drawing.Size(40, 15);
            this.LabelDealer.TabIndex = 10;
            this.LabelDealer.Text = "Dealer";
            // 
            // GameMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(255)))), ((int)(((byte)(195)))));
            this.ClientSize = new System.Drawing.Size(754, 648);
            this.Controls.Add(this.LabelDealer);
            this.Controls.Add(this.LabelPlayer);
            this.Controls.Add(this.CardPictChara2);
            this.Controls.Add(this.CardPictChara1);
            this.Controls.Add(this.ButtonChallenge);
            this.Controls.Add(this.ButtonChange);
            this.Controls.Add(this.CardPictPlayer2);
            this.Controls.Add(this.CardPictPlayer1);
            this.Controls.Add(this.CharaPict);
            this.Controls.Add(this.RestartButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameMain";
            this.Text = "エレ子のブラックジャック";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameMain_FormClosed);
            this.Load += new System.EventHandler(this.GameMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CharaPict)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardPictPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardPictPlayer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonChallenge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardPictChara1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardPictChara2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.PictureBox CharaPict;
        private System.Windows.Forms.PictureBox CardPictPlayer1;
        private System.Windows.Forms.PictureBox CardPictPlayer2;
        private System.Windows.Forms.PictureBox ButtonChange;
        private System.Windows.Forms.PictureBox ButtonChallenge;
        private System.Windows.Forms.PictureBox CardPictChara1;
        private System.Windows.Forms.PictureBox CardPictChara2;
        private System.Windows.Forms.Label LabelPlayer;
        private System.Windows.Forms.Label LabelDealer;
    }
}