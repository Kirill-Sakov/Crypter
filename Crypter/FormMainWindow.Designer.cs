namespace Crypter
{
    partial class FormMainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainWindow));
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.groupBoxFiles = new System.Windows.Forms.GroupBox();
            this.groupBoxKey = new System.Windows.Forms.GroupBox();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.buttonAddFiles = new System.Windows.Forms.Button();
            this.groupBoxControlPanel = new System.Windows.Forms.GroupBox();
            this.checkBoxTwoKeys = new System.Windows.Forms.CheckBox();
            this.groupBoxDecrypt = new System.Windows.Forms.GroupBox();
            this.buttonDecrypt = new System.Windows.Forms.Button();
            this.buttonOpenKey = new System.Windows.Forms.Button();
            this.buttonSaveKey = new System.Windows.Forms.Button();
            this.buttonDeleteFiles = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.buttonCancel = new System.Windows.Forms.ToolStripSplitButton();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.logBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundCrypter = new System.ComponentModel.BackgroundWorker();
            this.groupBoxCrypt = new System.Windows.Forms.GroupBox();
            this.buttonCryptro = new System.Windows.Forms.Button();
            this.backgroundDecrypter = new System.ComponentModel.BackgroundWorker();
            this.textBoxRsaKey = new System.Windows.Forms.TextBox();
            this.groupBoxRsaKey = new System.Windows.Forms.GroupBox();
            this.buttonCopyRsaKey = new System.Windows.Forms.Button();
            this.buttonOpenRsaKey = new System.Windows.Forms.Button();
            this.buttonSaveRsaKey = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxTree = new System.Windows.Forms.GroupBox();
            this.treeView = new Crypter.NoClickTree();
            this.groupBoxFiles.SuspendLayout();
            this.groupBoxKey.SuspendLayout();
            this.groupBoxControlPanel.SuspendLayout();
            this.groupBoxDecrypt.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBoxCrypt.SuspendLayout();
            this.groupBoxRsaKey.SuspendLayout();
            this.groupBoxTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFiles
            // 
            this.listViewFiles.AllowDrop = true;
            this.listViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(3, 16);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(761, 157);
            this.listViewFiles.TabIndex = 0;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.List;
            this.listViewFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFiles_DragDrop);
            this.listViewFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFiles_DragEnter);
            this.listViewFiles.DoubleClick += new System.EventHandler(this.listViewFiles_DoubleClick);
            this.listViewFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewFiles_KeyDown);
            // 
            // groupBoxFiles
            // 
            this.groupBoxFiles.Controls.Add(this.listViewFiles);
            this.groupBoxFiles.Location = new System.Drawing.Point(162, 373);
            this.groupBoxFiles.Name = "groupBoxFiles";
            this.groupBoxFiles.Size = new System.Drawing.Size(767, 176);
            this.groupBoxFiles.TabIndex = 1;
            this.groupBoxFiles.TabStop = false;
            this.groupBoxFiles.Text = "Зашифровать/Дешифровать файлы";
            // 
            // groupBoxKey
            // 
            this.groupBoxKey.Controls.Add(this.textBoxKey);
            this.groupBoxKey.Location = new System.Drawing.Point(165, 555);
            this.groupBoxKey.Name = "groupBoxKey";
            this.groupBoxKey.Size = new System.Drawing.Size(623, 149);
            this.groupBoxKey.TabIndex = 2;
            this.groupBoxKey.TabStop = false;
            this.groupBoxKey.Text = "Ключ";
            // 
            // textBoxKey
            // 
            this.textBoxKey.AllowDrop = true;
            this.textBoxKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxKey.Location = new System.Drawing.Point(3, 16);
            this.textBoxKey.Multiline = true;
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(617, 130);
            this.textBoxKey.TabIndex = 0;
            this.textBoxKey.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxKey_DragDrop);
            this.textBoxKey.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxKey_DragEnter);
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Location = new System.Drawing.Point(6, 19);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(132, 23);
            this.buttonAddFiles.TabIndex = 3;
            this.buttonAddFiles.Text = "Добавить файлы";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // groupBoxControlPanel
            // 
            this.groupBoxControlPanel.Controls.Add(this.checkBoxTwoKeys);
            this.groupBoxControlPanel.Controls.Add(this.groupBoxDecrypt);
            this.groupBoxControlPanel.Controls.Add(this.buttonOpenKey);
            this.groupBoxControlPanel.Controls.Add(this.buttonSaveKey);
            this.groupBoxControlPanel.Controls.Add(this.buttonDeleteFiles);
            this.groupBoxControlPanel.Controls.Add(this.buttonAddFiles);
            this.groupBoxControlPanel.Location = new System.Drawing.Point(12, 12);
            this.groupBoxControlPanel.Name = "groupBoxControlPanel";
            this.groupBoxControlPanel.Size = new System.Drawing.Size(144, 692);
            this.groupBoxControlPanel.TabIndex = 4;
            this.groupBoxControlPanel.TabStop = false;
            this.groupBoxControlPanel.Text = "Панель управления";
            // 
            // checkBoxTwoKeys
            // 
            this.checkBoxTwoKeys.Location = new System.Drawing.Point(9, 448);
            this.checkBoxTwoKeys.Name = "checkBoxTwoKeys";
            this.checkBoxTwoKeys.Size = new System.Drawing.Size(129, 31);
            this.checkBoxTwoKeys.TabIndex = 9;
            this.checkBoxTwoKeys.Text = "Дополнительный уровень защиты";
            this.checkBoxTwoKeys.UseVisualStyleBackColor = true;
            this.checkBoxTwoKeys.CheckedChanged += new System.EventHandler(this.checkBoxTwoKeys_CheckedChanged);
            // 
            // groupBoxDecrypt
            // 
            this.groupBoxDecrypt.Controls.Add(this.buttonDecrypt);
            this.groupBoxDecrypt.Location = new System.Drawing.Point(6, 543);
            this.groupBoxDecrypt.Name = "groupBoxDecrypt";
            this.groupBoxDecrypt.Size = new System.Drawing.Size(132, 143);
            this.groupBoxDecrypt.TabIndex = 8;
            this.groupBoxDecrypt.TabStop = false;
            this.groupBoxDecrypt.Text = "Дешифровать";
            // 
            // buttonDecrypt
            // 
            this.buttonDecrypt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDecrypt.Image = global::Crypter.Properties.Resources.bluekey_azu_12515;
            this.buttonDecrypt.Location = new System.Drawing.Point(3, 16);
            this.buttonDecrypt.Name = "buttonDecrypt";
            this.buttonDecrypt.Size = new System.Drawing.Size(126, 124);
            this.buttonDecrypt.TabIndex = 7;
            this.buttonDecrypt.UseVisualStyleBackColor = true;
            this.buttonDecrypt.Click += new System.EventHandler(this.buttonDecrypt_Click);
            // 
            // buttonOpenKey
            // 
            this.buttonOpenKey.Location = new System.Drawing.Point(6, 485);
            this.buttonOpenKey.Name = "buttonOpenKey";
            this.buttonOpenKey.Size = new System.Drawing.Size(132, 23);
            this.buttonOpenKey.TabIndex = 6;
            this.buttonOpenKey.Text = "Открыть ключ";
            this.buttonOpenKey.UseVisualStyleBackColor = true;
            this.buttonOpenKey.Click += new System.EventHandler(this.buttonOpenKey_Click);
            // 
            // buttonSaveKey
            // 
            this.buttonSaveKey.Location = new System.Drawing.Point(6, 514);
            this.buttonSaveKey.Name = "buttonSaveKey";
            this.buttonSaveKey.Size = new System.Drawing.Size(132, 23);
            this.buttonSaveKey.TabIndex = 5;
            this.buttonSaveKey.Text = "Сохранить ключ";
            this.buttonSaveKey.UseVisualStyleBackColor = true;
            this.buttonSaveKey.Click += new System.EventHandler(this.buttonSaveKey_Click);
            // 
            // buttonDeleteFiles
            // 
            this.buttonDeleteFiles.Location = new System.Drawing.Point(6, 48);
            this.buttonDeleteFiles.Name = "buttonDeleteFiles";
            this.buttonDeleteFiles.Size = new System.Drawing.Size(132, 23);
            this.buttonDeleteFiles.TabIndex = 4;
            this.buttonDeleteFiles.Text = "Исключить файлы";
            this.buttonDeleteFiles.UseVisualStyleBackColor = true;
            this.buttonDeleteFiles.Click += new System.EventHandler(this.buttonDeleteFiles_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonCancel,
            this.progressBar,
            this.logBar});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 707);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(934, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonCancel.AutoSize = false;
            this.buttonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(138, 20);
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.ButtonClick += new System.EventHandler(this.buttonCancel_ButtonClick);
            // 
            // progressBar
            // 
            this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(610, 16);
            // 
            // logBar
            // 
            this.logBar.Name = "logBar";
            this.logBar.Size = new System.Drawing.Size(30, 17);
            this.logBar.Text = "Log:";
            // 
            // backgroundCrypter
            // 
            this.backgroundCrypter.WorkerReportsProgress = true;
            this.backgroundCrypter.WorkerSupportsCancellation = true;
            this.backgroundCrypter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundCrypter.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundCrypter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // groupBoxCrypt
            // 
            this.groupBoxCrypt.Controls.Add(this.buttonCryptro);
            this.groupBoxCrypt.Location = new System.Drawing.Point(794, 555);
            this.groupBoxCrypt.Name = "groupBoxCrypt";
            this.groupBoxCrypt.Size = new System.Drawing.Size(132, 149);
            this.groupBoxCrypt.TabIndex = 8;
            this.groupBoxCrypt.TabStop = false;
            this.groupBoxCrypt.Text = "Зашифровать:";
            // 
            // buttonCryptro
            // 
            this.buttonCryptro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCryptro.Image = global::Crypter.Properties.Resources.blue_key_locked_12546;
            this.buttonCryptro.Location = new System.Drawing.Point(3, 16);
            this.buttonCryptro.Name = "buttonCryptro";
            this.buttonCryptro.Size = new System.Drawing.Size(126, 130);
            this.buttonCryptro.TabIndex = 5;
            this.buttonCryptro.UseVisualStyleBackColor = true;
            this.buttonCryptro.Click += new System.EventHandler(this.buttonCryptro_Click);
            // 
            // backgroundDecrypter
            // 
            this.backgroundDecrypter.WorkerReportsProgress = true;
            this.backgroundDecrypter.WorkerSupportsCancellation = true;
            this.backgroundDecrypter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundDecrypter_DoWork);
            this.backgroundDecrypter.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundDecrypter_ProgressChanged);
            this.backgroundDecrypter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundDecrypter_RunWorkerCompleted);
            // 
            // textBoxRsaKey
            // 
            this.textBoxRsaKey.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxRsaKey.Location = new System.Drawing.Point(3, 16);
            this.textBoxRsaKey.Multiline = true;
            this.textBoxRsaKey.Name = "textBoxRsaKey";
            this.textBoxRsaKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRsaKey.Size = new System.Drawing.Size(188, 580);
            this.textBoxRsaKey.TabIndex = 9;
            // 
            // groupBoxRsaKey
            // 
            this.groupBoxRsaKey.Controls.Add(this.buttonCopyRsaKey);
            this.groupBoxRsaKey.Controls.Add(this.buttonOpenRsaKey);
            this.groupBoxRsaKey.Controls.Add(this.buttonSaveRsaKey);
            this.groupBoxRsaKey.Controls.Add(this.textBoxRsaKey);
            this.groupBoxRsaKey.Location = new System.Drawing.Point(935, 12);
            this.groupBoxRsaKey.Name = "groupBoxRsaKey";
            this.groupBoxRsaKey.Size = new System.Drawing.Size(194, 692);
            this.groupBoxRsaKey.TabIndex = 10;
            this.groupBoxRsaKey.TabStop = false;
            this.groupBoxRsaKey.Text = "Ваш дополнительный ключ";
            this.groupBoxRsaKey.Visible = false;
            // 
            // buttonCopyRsaKey
            // 
            this.buttonCopyRsaKey.Location = new System.Drawing.Point(6, 602);
            this.buttonCopyRsaKey.Name = "buttonCopyRsaKey";
            this.buttonCopyRsaKey.Size = new System.Drawing.Size(182, 23);
            this.buttonCopyRsaKey.TabIndex = 13;
            this.buttonCopyRsaKey.Text = "Копировать в буфер";
            this.buttonCopyRsaKey.UseVisualStyleBackColor = true;
            this.buttonCopyRsaKey.Click += new System.EventHandler(this.buttonCopyRsaKey_Click);
            // 
            // buttonOpenRsaKey
            // 
            this.buttonOpenRsaKey.Location = new System.Drawing.Point(6, 631);
            this.buttonOpenRsaKey.Name = "buttonOpenRsaKey";
            this.buttonOpenRsaKey.Size = new System.Drawing.Size(182, 23);
            this.buttonOpenRsaKey.TabIndex = 12;
            this.buttonOpenRsaKey.Text = "Открыть из файла";
            this.buttonOpenRsaKey.UseVisualStyleBackColor = true;
            this.buttonOpenRsaKey.Click += new System.EventHandler(this.buttonOpenRsaKey_Click);
            // 
            // buttonSaveRsaKey
            // 
            this.buttonSaveRsaKey.Location = new System.Drawing.Point(6, 660);
            this.buttonSaveRsaKey.Name = "buttonSaveRsaKey";
            this.buttonSaveRsaKey.Size = new System.Drawing.Size(182, 23);
            this.buttonSaveRsaKey.TabIndex = 11;
            this.buttonSaveRsaKey.Text = "Сохранить в файл";
            this.buttonSaveRsaKey.UseVisualStyleBackColor = true;
            this.buttonSaveRsaKey.Click += new System.EventHandler(this.buttonSaveRsaKey_Click);
            // 
            // groupBoxTree
            // 
            this.groupBoxTree.Controls.Add(this.treeView);
            this.groupBoxTree.Location = new System.Drawing.Point(162, 12);
            this.groupBoxTree.Name = "groupBoxTree";
            this.groupBoxTree.Size = new System.Drawing.Size(767, 355);
            this.groupBoxTree.TabIndex = 11;
            this.groupBoxTree.TabStop = false;
            this.groupBoxTree.Text = "Зашифровать/Дешифровать папки";
            // 
            // treeView
            // 
            this.treeView.CheckBoxes = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(3, 16);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(761, 336);
            this.treeView.TabIndex = 10;
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            // 
            // FormMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 729);
            this.Controls.Add(this.groupBoxTree);
            this.Controls.Add(this.groupBoxRsaKey);
            this.Controls.Add(this.groupBoxCrypt);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBoxControlPanel);
            this.Controls.Add(this.groupBoxKey);
            this.Controls.Add(this.groupBoxFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crypter";
            this.Load += new System.EventHandler(this.FormMainWindow_Load);
            this.groupBoxFiles.ResumeLayout(false);
            this.groupBoxKey.ResumeLayout(false);
            this.groupBoxKey.PerformLayout();
            this.groupBoxControlPanel.ResumeLayout(false);
            this.groupBoxDecrypt.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBoxCrypt.ResumeLayout(false);
            this.groupBoxRsaKey.ResumeLayout(false);
            this.groupBoxRsaKey.PerformLayout();
            this.groupBoxTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.GroupBox groupBoxFiles;
        private System.Windows.Forms.GroupBox groupBoxKey;
        private System.Windows.Forms.Button buttonAddFiles;
        private System.Windows.Forms.GroupBox groupBoxControlPanel;
        private System.Windows.Forms.Button buttonDeleteFiles;
        private System.Windows.Forms.Button buttonCryptro;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel logBar;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Button buttonOpenKey;
        private System.Windows.Forms.Button buttonSaveKey;
        private System.ComponentModel.BackgroundWorker backgroundCrypter;
        private System.Windows.Forms.Button buttonDecrypt;
        private System.Windows.Forms.GroupBox groupBoxCrypt;
        private System.ComponentModel.BackgroundWorker backgroundDecrypter;
        private System.Windows.Forms.GroupBox groupBoxDecrypt;
        private System.Windows.Forms.ToolStripSplitButton buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxTwoKeys;
        private System.Windows.Forms.TextBox textBoxRsaKey;
        private System.Windows.Forms.GroupBox groupBoxRsaKey;
        private System.Windows.Forms.Button buttonCopyRsaKey;
        private System.Windows.Forms.Button buttonOpenRsaKey;
        private System.Windows.Forms.Button buttonSaveRsaKey;
        //private System.Windows.Forms.TreeView treeView;
        private NoClickTree treeView;
        private System.Windows.Forms.GroupBox groupBoxTree;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

