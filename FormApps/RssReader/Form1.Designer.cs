﻿namespace RssReader {
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
            panel1 = new Panel();
            btSearch = new Button();
            tbSearch = new TextBox();
            lbTitles = new ListBox();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(943, 20);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(100, 33);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.BackColor = Color.WhiteSmoke;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = SystemColors.ControlLight;
            wvRssLink.Location = new Point(0, 0);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(570, 529);
            wvRssLink.TabIndex = 3;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.ContentLoading += wvRssLink_ContentLoading;
            // 
            // btBack
            // 
            btBack.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btBack.Location = new Point(473, 128);
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
            btForward.Location = new Point(517, 128);
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
            tbFavorite.Location = new Point(148, 71);
            tbFavorite.Name = "tbFavorite";
            tbFavorite.Size = new Size(449, 33);
            tbFavorite.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(25, 27);
            label1.Name = "label1";
            label1.Size = new Size(152, 21);
            label1.TabIndex = 4;
            label1.Text = "URL(お気に入り名称):";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(29, 77);
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
            cbUrl.Location = new Point(183, 21);
            cbUrl.Name = "cbUrl";
            cbUrl.Size = new Size(740, 33);
            cbUrl.TabIndex = 5;
            // 
            // btFavoriteAdd
            // 
            btFavoriteAdd.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btFavoriteAdd.Location = new Point(603, 71);
            btFavoriteAdd.Name = "btFavoriteAdd";
            btFavoriteAdd.Size = new Size(62, 33);
            btFavoriteAdd.TabIndex = 1;
            btFavoriteAdd.Text = "登録";
            btFavoriteAdd.UseVisualStyleBackColor = true;
            btFavoriteAdd.Click += btFavoriteAdd_Click;
            // 
            // btReload
            // 
            btReload.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btReload.Location = new Point(560, 128);
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
            btFavoriteDelete.Location = new Point(671, 70);
            btFavoriteDelete.Name = "btFavoriteDelete";
            btFavoriteDelete.Size = new Size(59, 33);
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
            tbSiteUrlText.Location = new Point(603, 128);
            tbSiteUrlText.Name = "tbSiteUrlText";
            tbSiteUrlText.Size = new Size(440, 33);
            tbSiteUrlText.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(wvRssLink);
            panel1.Location = new Point(473, 187);
            panel1.Name = "panel1";
            panel1.Size = new Size(570, 529);
            panel1.TabIndex = 8;
            // 
            // btSearch
            // 
            btSearch.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btSearch.Location = new Point(418, 127);
            btSearch.Name = "btSearch";
            btSearch.Size = new Size(36, 33);
            btSearch.TabIndex = 1;
            btSearch.Text = "🔎";
            btSearch.UseVisualStyleBackColor = true;
            btSearch.Click += btSearch_Click;
            // 
            // tbSearch
            // 
            tbSearch.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbSearch.Location = new Point(29, 127);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(380, 33);
            tbSearch.TabIndex = 0;
            tbSearch.TextChanged += tbSearch_TextChanged;
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lbTitles.DrawMode = DrawMode.OwnerDrawFixed;
            lbTitles.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lbTitles.FormattingEnabled = true;
            lbTitles.HorizontalExtent = 529;
            lbTitles.HorizontalScrollbar = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(29, 187);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(425, 529);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(188, 248, 230);
            ClientSize = new Size(1072, 742);
            Controls.Add(lbTitles);
            Controls.Add(tbSiteUrlText);
            Controls.Add(cbUrl);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btReload);
            Controls.Add(btForward);
            Controls.Add(btBack);
            Controls.Add(btFavoriteDelete);
            Controls.Add(btSearch);
            Controls.Add(btFavoriteAdd);
            Controls.Add(btRssGet);
            Controls.Add(tbSearch);
            Controls.Add(tbFavorite);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "RSSリーダー";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btRssGet;
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
        private Panel panel1;
        private Button btSearch;
        private TextBox tbSearch;
        private ListBox lbTitles;
    }
}
