namespace MyPageViewer.Controls
{
    partial class AttachmentsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listAttachments = new CheckedListBox();
            btDelete = new Button();
            btAdd = new Button();
            btRefresh = new Button();
            btSaveAll = new Button();
            SuspendLayout();
            // 
            // listAttachments
            // 
            listAttachments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listAttachments.FormattingEnabled = true;
            listAttachments.Location = new Point(20, 14);
            listAttachments.Name = "listAttachments";
            listAttachments.Size = new Size(256, 418);
            listAttachments.TabIndex = 3;
            // 
            // btDelete
            // 
            btDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btDelete.Image = Properties.Resources.delete24;
            btDelete.Location = new Point(240, 440);
            btDelete.Name = "btDelete";
            btDelete.Size = new Size(35, 36);
            btDelete.TabIndex = 4;
            btDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            btDelete.UseVisualStyleBackColor = true;
            // 
            // btAdd
            // 
            btAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btAdd.Image = Properties.Resources.plus24;
            btAdd.Location = new Point(199, 440);
            btAdd.Name = "btAdd";
            btAdd.Size = new Size(35, 36);
            btAdd.TabIndex = 5;
            btAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btAdd.UseVisualStyleBackColor = true;
            // 
            // btRefresh
            // 
            btRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btRefresh.Image = Properties.Resources.refresh24;
            btRefresh.Location = new Point(20, 438);
            btRefresh.Name = "btRefresh";
            btRefresh.Size = new Size(35, 36);
            btRefresh.TabIndex = 5;
            btRefresh.TextImageRelation = TextImageRelation.ImageBeforeText;
            btRefresh.UseVisualStyleBackColor = true;
            // 
            // btSaveAll
            // 
            btSaveAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btSaveAll.Image = Properties.Resources.saveall24;
            btSaveAll.Location = new Point(61, 438);
            btSaveAll.Name = "btSaveAll";
            btSaveAll.Size = new Size(35, 36);
            btSaveAll.TabIndex = 5;
            btSaveAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            btSaveAll.UseVisualStyleBackColor = true;
            // 
            // AttachmentsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btDelete);
            Controls.Add(btSaveAll);
            Controls.Add(btRefresh);
            Controls.Add(btAdd);
            Controls.Add(listAttachments);
            Name = "AttachmentsControl";
            Size = new Size(292, 486);
            Load += AttachmentsControl_Load;
            ResumeLayout(false);
        }

        #endregion
        private CheckedListBox listAttachments;
        private Button btDelete;
        private Button btAdd;
        private Button btRefresh;
        private Button btSaveAll;
    }
}
