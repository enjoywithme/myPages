namespace MyPageViewer.Controls
{
    partial class TagsControl
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
            listTags = new CheckedListBox();
            btAdd = new Button();
            btDelete = new Button();
            SuspendLayout();
            // 
            // listTags
            // 
            listTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listTags.FormattingEnabled = true;
            listTags.Location = new Point(20, 13);
            listTags.Name = "listTags";
            listTags.Size = new Size(262, 364);
            listTags.TabIndex = 0;
            // 
            // btAdd
            // 
            btAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btAdd.Image = Properties.Resources.plus24;
            btAdd.Location = new Point(206, 388);
            btAdd.Name = "btAdd";
            btAdd.Size = new Size(35, 36);
            btAdd.TabIndex = 1;
            btAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btAdd.UseVisualStyleBackColor = true;
            // 
            // btDelete
            // 
            btDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btDelete.Image = Properties.Resources.delete24;
            btDelete.Location = new Point(247, 388);
            btDelete.Name = "btDelete";
            btDelete.Size = new Size(35, 36);
            btDelete.TabIndex = 1;
            btDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            btDelete.UseVisualStyleBackColor = true;
            // 
            // TagsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btDelete);
            Controls.Add(btAdd);
            Controls.Add(listTags);
            Name = "TagsControl";
            Size = new Size(302, 439);
            Load += TagsControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox listTags;
        private Button btAdd;
        private Button btDelete;
    }
}
