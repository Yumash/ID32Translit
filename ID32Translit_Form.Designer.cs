namespace ID32Translit
{
    partial class Form_ID2Translit
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ID2Translit));
            this.dialog_SelectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.button_SelectFolder = new System.Windows.Forms.Button();
            this.label_SelectedFolder = new System.Windows.Forms.LinkLabel();
            this.button_Scan = new System.Windows.Forms.Button();
            this.checkBox_ScanRecursively = new System.Windows.Forms.CheckBox();
            this.textBox_Result = new System.Windows.Forms.TextBox();
            this.contextMenuStrip_TextBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_ClearTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.button_EditResult = new System.Windows.Forms.Button();
            this.button_ApplyResult = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.checkBox_SetFileNameToTitle = new System.Windows.Forms.CheckBox();
            this.checkBox_clearBadTags = new System.Windows.Forms.CheckBox();
            this.ToolStripMenuItem_SaveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_TextBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_SelectFolder
            // 
            resources.ApplyResources(this.button_SelectFolder, "button_SelectFolder");
            this.button_SelectFolder.Name = "button_SelectFolder";
            this.button_SelectFolder.UseVisualStyleBackColor = true;
            this.button_SelectFolder.Click += new System.EventHandler(this.button_SelectFolder_Click);
            // 
            // label_SelectedFolder
            // 
            resources.ApplyResources(this.label_SelectedFolder, "label_SelectedFolder");
            this.label_SelectedFolder.Name = "label_SelectedFolder";
            this.label_SelectedFolder.TabStop = true;
            this.label_SelectedFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_SelectedFolder_LinkClicked);
            // 
            // button_Scan
            // 
            resources.ApplyResources(this.button_Scan, "button_Scan");
            this.button_Scan.Name = "button_Scan";
            this.button_Scan.UseVisualStyleBackColor = true;
            this.button_Scan.Click += new System.EventHandler(this.button_Scan_Click);
            // 
            // checkBox_ScanRecursively
            // 
            resources.ApplyResources(this.checkBox_ScanRecursively, "checkBox_ScanRecursively");
            this.checkBox_ScanRecursively.Checked = true;
            this.checkBox_ScanRecursively.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ScanRecursively.Name = "checkBox_ScanRecursively";
            this.checkBox_ScanRecursively.UseVisualStyleBackColor = true;
            // 
            // textBox_Result
            // 
            resources.ApplyResources(this.textBox_Result, "textBox_Result");
            this.textBox_Result.ContextMenuStrip = this.contextMenuStrip_TextBox;
            this.textBox_Result.Name = "textBox_Result";
            // 
            // contextMenuStrip_TextBox
            // 
            this.contextMenuStrip_TextBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ClearTextBox,
            this.ToolStripMenuItem_SaveToFile});
            this.contextMenuStrip_TextBox.Name = "contextMenuStrip_TextBox";
            resources.ApplyResources(this.contextMenuStrip_TextBox, "contextMenuStrip_TextBox");
            // 
            // toolStripMenuItem_ClearTextBox
            // 
            this.toolStripMenuItem_ClearTextBox.Name = "toolStripMenuItem_ClearTextBox";
            resources.ApplyResources(this.toolStripMenuItem_ClearTextBox, "toolStripMenuItem_ClearTextBox");
            this.toolStripMenuItem_ClearTextBox.Click += new System.EventHandler(this.toolStripMenu_ClearTextBox_Click);
            // 
            // button_EditResult
            // 
            resources.ApplyResources(this.button_EditResult, "button_EditResult");
            this.button_EditResult.Name = "button_EditResult";
            this.button_EditResult.UseVisualStyleBackColor = true;
            this.button_EditResult.Click += new System.EventHandler(this.button_EditResult_Click);
            // 
            // button_ApplyResult
            // 
            resources.ApplyResources(this.button_ApplyResult, "button_ApplyResult");
            this.button_ApplyResult.Name = "button_ApplyResult";
            this.button_ApplyResult.UseVisualStyleBackColor = true;
            this.button_ApplyResult.Click += new System.EventHandler(this.button_ApplyResult_Click);
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // checkBox_SetFileNameToTitle
            // 
            resources.ApplyResources(this.checkBox_SetFileNameToTitle, "checkBox_SetFileNameToTitle");
            this.checkBox_SetFileNameToTitle.Name = "checkBox_SetFileNameToTitle";
            this.checkBox_SetFileNameToTitle.UseVisualStyleBackColor = true;
            this.checkBox_SetFileNameToTitle.CheckedChanged += new System.EventHandler(this.checkBox_SetFileNameToTitle_CheckedChanged);
            // 
            // checkBox_clearBadTags
            // 
            resources.ApplyResources(this.checkBox_clearBadTags, "checkBox_clearBadTags");
            this.checkBox_clearBadTags.Name = "checkBox_clearBadTags";
            this.checkBox_clearBadTags.UseVisualStyleBackColor = true;
            // 
            // ToolStripMenuItem_SaveToFile
            // 
            this.ToolStripMenuItem_SaveToFile.Name = "ToolStripMenuItem_SaveToFile";
            resources.ApplyResources(this.ToolStripMenuItem_SaveToFile, "ToolStripMenuItem_SaveToFile");
            this.ToolStripMenuItem_SaveToFile.Click += new System.EventHandler(this.ToolStripMenuItem_SaveToFile_Click);
            // 
            // Form_ID2Translit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_clearBadTags);
            this.Controls.Add(this.checkBox_SetFileNameToTitle);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button_ApplyResult);
            this.Controls.Add(this.button_EditResult);
            this.Controls.Add(this.textBox_Result);
            this.Controls.Add(this.checkBox_ScanRecursively);
            this.Controls.Add(this.button_Scan);
            this.Controls.Add(this.label_SelectedFolder);
            this.Controls.Add(this.button_SelectFolder);
            this.MaximizeBox = false;
            this.Name = "Form_ID2Translit";
            this.ShowIcon = false;
            this.contextMenuStrip_TextBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog dialog_SelectFolder;
        private System.Windows.Forms.Button button_SelectFolder;
        private System.Windows.Forms.LinkLabel label_SelectedFolder;
        private System.Windows.Forms.Button button_Scan;
        private System.Windows.Forms.CheckBox checkBox_ScanRecursively;
        private System.Windows.Forms.TextBox textBox_Result;
        private System.Windows.Forms.Button button_EditResult;
        private System.Windows.Forms.Button button_ApplyResult;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox checkBox_SetFileNameToTitle;
        private System.Windows.Forms.CheckBox checkBox_clearBadTags;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_TextBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ClearTextBox;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SaveToFile;
    }
}

