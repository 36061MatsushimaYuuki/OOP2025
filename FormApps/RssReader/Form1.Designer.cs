namespace RssReader {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            btRssGet = new Button();
            lbTitles = new ListBox();
            wvRssLink = new Microsoft.Web.WebView2.WinForms.WebView2();
            btBack = new Button();
            btForward = new Button();
            tbFavorite = new TextBox();
            label1 = new Label();
            label2 = new Label();
            cbUrl = new ComboBox();
            btFavoriteAdd = new Button();
            btReload = new Button();
            btFavoriteDelete = new Button();
            tbSiteUrlText = new TextBox();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            SuspendLayout();
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(943, 37);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(100, 33);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lbTitles.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lbTitles.FormattingEnabled = true;
            lbTitles.HorizontalScrollbar = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(25, 166);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(425, 550);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.BackColor = SystemColors.Control;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = Color.WhiteSmoke;
            wvRssLink.Location = new Point(473, 206);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(570, 510);
            wvRssLink.TabIndex = 3;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.ContentLoading += wvRssLink_ContentLoading;
            // 
            // btBack
            // 
            btBack.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btBack.Location = new Point(473, 166);
            btBack.Name = "btBack";
            btBack.Size = new Size(38, 33);
            btBack.TabIndex = 1;
            btBack.Text = "←";
            btBack.UseVisualStyleBackColor = true;
            btBack.Click += btBack_Click;
            // 
            // btForward
            // 
            btForward.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btForward.Location = new Point(517, 164);
            btForward.Name = "btForward";
            btForward.Size = new Size(37, 33);
            btForward.TabIndex = 1;
            btForward.Text = "→";
            btForward.UseVisualStyleBackColor = true;
            btForward.Click += btForward_Click;
            // 
            // tbFavorite
            // 
            tbFavorite.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbFavorite.Location = new Point(212, 112);
            tbFavorite.Name = "tbFavorite";
            tbFavorite.Size = new Size(553, 33);
            tbFavorite.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(25, 44);
            label1.Name = "label1";
            label1.Size = new Size(181, 21);
            label1.TabIndex = 4;
            label1.Text = "URLまたはお気に入り名称:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(93, 118);
            label2.Name = "label2";
            label2.Size = new Size(113, 21);
            label2.TabIndex = 4;
            label2.Text = "お気に入り名称:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // cbUrl
            // 
            cbUrl.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cbUrl.FormattingEnabled = true;
            cbUrl.Location = new Point(212, 38);
            cbUrl.Name = "cbUrl";
            cbUrl.Size = new Size(711, 33);
            cbUrl.TabIndex = 5;
            // 
            // btFavoriteAdd
            // 
            btFavoriteAdd.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btFavoriteAdd.Location = new Point(780, 112);
            btFavoriteAdd.Name = "btFavoriteAdd";
            btFavoriteAdd.Size = new Size(100, 33);
            btFavoriteAdd.TabIndex = 1;
            btFavoriteAdd.Text = "登録";
            btFavoriteAdd.UseVisualStyleBackColor = true;
            btFavoriteAdd.Click += btFavoriteAdd_Click;
            // 
            // btReload
            // 
            btReload.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btReload.Location = new Point(560, 165);
            btReload.Name = "btReload";
            btReload.Size = new Size(37, 33);
            btReload.TabIndex = 1;
            btReload.Text = "↻";
            btReload.UseVisualStyleBackColor = true;
            btReload.Click += btReload_Click;
            // 
            // btFavoriteDelete
            // 
            btFavoriteDelete.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btFavoriteDelete.Location = new Point(886, 111);
            btFavoriteDelete.Name = "btFavoriteDelete";
            btFavoriteDelete.Size = new Size(100, 33);
            btFavoriteDelete.TabIndex = 1;
            btFavoriteDelete.Text = "削除";
            btFavoriteDelete.UseVisualStyleBackColor = true;
            btFavoriteDelete.Click += btFavoriteDelete_Click;
            // 
            // tbSiteUrlText
            // 
            tbSiteUrlText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSiteUrlText.Enabled = false;
            tbSiteUrlText.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbSiteUrlText.ImeMode = ImeMode.NoControl;
            tbSiteUrlText.Location = new Point(603, 165);
            tbSiteUrlText.Name = "tbSiteUrlText";
            tbSiteUrlText.Size = new Size(440, 33);
            tbSiteUrlText.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1072, 742);
            Controls.Add(tbSiteUrlText);
            Controls.Add(cbUrl);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(wvRssLink);
            Controls.Add(lbTitles);
            Controls.Add(btReload);
            Controls.Add(btForward);
            Controls.Add(btBack);
            Controls.Add(btFavoriteDelete);
            Controls.Add(btFavoriteAdd);
            Controls.Add(btRssGet);
            Controls.Add(tbFavorite);
            Name = "Form1";
            Text = "RSSリーダー";
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvRssLink;
        private Button btBack;
        private Button btForward;
        private TextBox tbFavorite;
        private Label label1;
        private Label label2;
        private ComboBox cbUrl;
        private Button btFavoriteAdd;
        private Button btReload;
        private Button btFavoriteDelete;
        private TextBox tbSiteUrlText;
    }
}
