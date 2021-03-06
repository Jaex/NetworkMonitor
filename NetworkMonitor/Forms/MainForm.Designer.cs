﻿namespace NetworkMonitor
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.lvMain = new System.Windows.Forms.ListView();
            this.chDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.niMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenMainWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenLogFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tscMain = new System.Windows.Forms.ToolStripContainer();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbCopyAll = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenLogFile = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenGitHub = new System.Windows.Forms.ToolStripButton();
            this.cmsTray.SuspendLayout();
            this.tscMain.BottomToolStripPanel.SuspendLayout();
            this.tscMain.ContentPanel.SuspendLayout();
            this.tscMain.TopToolStripPanel.SuspendLayout();
            this.tscMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate,
            this.chStatus});
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMain.FullRowSelect = true;
            this.lvMain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMain.Location = new System.Drawing.Point(0, 0);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(534, 414);
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            // 
            // chDate
            // 
            this.chDate.Text = "Date";
            this.chDate.Width = 162;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 339;
            // 
            // niMain
            // 
            this.niMain.ContextMenuStrip = this.cmsTray;
            this.niMain.Text = "Network Monitor";
            this.niMain.Visible = true;
            this.niMain.BalloonTipClicked += new System.EventHandler(this.niMain_BalloonTipClicked);
            this.niMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niMain_MouseDoubleClick);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenMainWindow,
            this.tsmiOpenLogFile,
            this.tsmiSettings,
            this.tsmiExit});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.ShowImageMargin = false;
            this.cmsTray.Size = new System.Drawing.Size(154, 92);
            // 
            // tsmiOpenMainWindow
            // 
            this.tsmiOpenMainWindow.Name = "tsmiOpenMainWindow";
            this.tsmiOpenMainWindow.Size = new System.Drawing.Size(153, 22);
            this.tsmiOpenMainWindow.Text = "Open main window";
            this.tsmiOpenMainWindow.Click += new System.EventHandler(this.tsmiOpenMainWindow_Click);
            // 
            // tsmiOpenLogFile
            // 
            this.tsmiOpenLogFile.Name = "tsmiOpenLogFile";
            this.tsmiOpenLogFile.Size = new System.Drawing.Size(153, 22);
            this.tsmiOpenLogFile.Text = "Open log file...";
            this.tsmiOpenLogFile.Click += new System.EventHandler(this.tsbOpenLogFile_Click);
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(153, 22);
            this.tsmiSettings.Text = "Settings...";
            this.tsmiSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(153, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tscMain
            // 
            // 
            // tscMain.BottomToolStripPanel
            // 
            this.tscMain.BottomToolStripPanel.Controls.Add(this.ssMain);
            // 
            // tscMain.ContentPanel
            // 
            this.tscMain.ContentPanel.Controls.Add(this.lvMain);
            this.tscMain.ContentPanel.Size = new System.Drawing.Size(534, 414);
            this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscMain.Location = new System.Drawing.Point(0, 0);
            this.tscMain.Name = "tscMain";
            this.tscMain.Size = new System.Drawing.Size(534, 461);
            this.tscMain.TabIndex = 1;
            this.tscMain.Text = "toolStripContainer1";
            // 
            // tscMain.TopToolStripPanel
            // 
            this.tscMain.TopToolStripPanel.Controls.Add(this.tsMain);
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.None;
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.ssMain.Location = new System.Drawing.Point(0, 0);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(534, 22);
            this.ssMain.SizingGrip = false;
            this.ssMain.TabIndex = 0;
            // 
            // tsslStatus
            // 
            this.tsslStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslStatus.Margin = new System.Windows.Forms.Padding(3, 4, 0, 5);
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(0, 13);
            // 
            // tsMain
            // 
            this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCopyAll,
            this.tsbOpenLogFile,
            this.tsbSettings,
            this.tsbOpenGitHub});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(534, 25);
            this.tsMain.Stretch = true;
            this.tsMain.TabIndex = 0;
            // 
            // tsbCopyAll
            // 
            this.tsbCopyAll.AutoToolTip = false;
            this.tsbCopyAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCopyAll.Enabled = false;
            this.tsbCopyAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyAll.Name = "tsbCopyAll";
            this.tsbCopyAll.Size = new System.Drawing.Size(54, 22);
            this.tsbCopyAll.Text = "Copy all";
            this.tsbCopyAll.Click += new System.EventHandler(this.tsbCopyAll_Click);
            // 
            // tsbOpenLogFile
            // 
            this.tsbOpenLogFile.AutoToolTip = false;
            this.tsbOpenLogFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOpenLogFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenLogFile.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tsbOpenLogFile.Name = "tsbOpenLogFile";
            this.tsbOpenLogFile.Size = new System.Drawing.Size(88, 22);
            this.tsbOpenLogFile.Text = "Open log file...";
            this.tsbOpenLogFile.Click += new System.EventHandler(this.tsbOpenLogFile_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.AutoToolTip = false;
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(62, 22);
            this.tsbSettings.Text = "Settings...";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tsbOpenGitHub
            // 
            this.tsbOpenGitHub.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbOpenGitHub.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenGitHub.Image = global::NetworkMonitor.Properties.Resources.GitHub;
            this.tsbOpenGitHub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenGitHub.Name = "tsbOpenGitHub";
            this.tsbOpenGitHub.Size = new System.Drawing.Size(23, 22);
            this.tsbOpenGitHub.Text = "Open GitHub page...";
            this.tsbOpenGitHub.Click += new System.EventHandler(this.tsbOpenGitHub_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.tscMain);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.cmsTray.ResumeLayout(false);
            this.tscMain.BottomToolStripPanel.ResumeLayout(false);
            this.tscMain.BottomToolStripPanel.PerformLayout();
            this.tscMain.ContentPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.PerformLayout();
            this.tscMain.ResumeLayout(false);
            this.tscMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.NotifyIcon niMain;
        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenMainWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripContainer tscMain;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbCopyAll;
        private System.Windows.Forms.ToolStripButton tsbOpenLogFile;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenLogFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolStripButton tsbOpenGitHub;
    }
}

