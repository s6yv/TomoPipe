namespace PlatomMonitor.Windows
{
    partial class ValidatorToolWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidatorToolWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSchema = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.editor_schema = new ScintillaNET.Scintilla();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_download_schemas = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.combo_schemas = new System.Windows.Forms.ComboBox();
            this.btnValidateSchema = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.editor_message = new ScintillaNET.Scintilla();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.combo_messages = new System.Windows.Forms.ComboBox();
            this.btnValidateMessage = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblSchema, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 575);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblSchema
            // 
            this.lblSchema.AutoSize = true;
            this.lblSchema.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSchema.Location = new System.Drawing.Point(3, 0);
            this.lblSchema.Name = "lblSchema";
            this.lblSchema.Size = new System.Drawing.Size(778, 13);
            this.lblSchema.TabIndex = 0;
            this.lblSchema.Text = "Schemat walidacyjny:";
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.editor_schema);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 233);
            this.panel1.TabIndex = 1;
            // 
            // editor_schema
            // 
            this.editor_schema.AutomaticFold = ((ScintillaNET.AutomaticFold)(((ScintillaNET.AutomaticFold.Show | ScintillaNET.AutomaticFold.Click) 
            | ScintillaNET.AutomaticFold.Change)));
            this.editor_schema.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editor_schema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor_schema.Location = new System.Drawing.Point(0, 0);
            this.editor_schema.Name = "editor_schema";
            this.editor_schema.Size = new System.Drawing.Size(778, 233);
            this.editor_schema.TabIndex = 0;
            this.editor_schema.Text = "editor_schema";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btn_download_schemas, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_save, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.combo_schemas, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnValidateSchema, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 255);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(778, 29);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btn_download_schemas
            // 
            this.btn_download_schemas.Location = new System.Drawing.Point(3, 3);
            this.btn_download_schemas.Name = "btn_download_schemas";
            this.btn_download_schemas.Size = new System.Drawing.Size(75, 23);
            this.btn_download_schemas.TabIndex = 2;
            this.btn_download_schemas.Text = "&Pobierz...";
            this.btn_download_schemas.UseVisualStyleBackColor = true;
            this.btn_download_schemas.Click += new System.EventHandler(this.btn_download_schemas_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(84, 3);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 1;
            this.button_save.Text = "Zapisz";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // combo_schemas
            // 
            this.combo_schemas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combo_schemas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_schemas.FormattingEnabled = true;
            this.combo_schemas.ItemHeight = 13;
            this.combo_schemas.Location = new System.Drawing.Point(165, 3);
            this.combo_schemas.Name = "combo_schemas";
            this.combo_schemas.Size = new System.Drawing.Size(509, 21);
            this.combo_schemas.TabIndex = 2;
            this.combo_schemas.SelectedIndexChanged += new System.EventHandler(this.combo_schemas_SelectedIndexChanged);
            // 
            // btnValidateSchema
            // 
            this.btnValidateSchema.AutoSize = true;
            this.btnValidateSchema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnValidateSchema.Location = new System.Drawing.Point(680, 3);
            this.btnValidateSchema.Name = "btnValidateSchema";
            this.btnValidateSchema.Size = new System.Drawing.Size(95, 23);
            this.btnValidateSchema.TabIndex = 0;
            this.btnValidateSchema.Text = "Waliduj schemat";
            this.btnValidateSchema.UseVisualStyleBackColor = true;
            this.btnValidateSchema.Click += new System.EventHandler(this.btnValidateSchema_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMessage.Location = new System.Drawing.Point(3, 287);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(778, 13);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "Komunikat:";
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.editor_message);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 303);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(778, 233);
            this.panel2.TabIndex = 2;
            // 
            // editor_message
            // 
            this.editor_message.AutomaticFold = ((ScintillaNET.AutomaticFold)(((ScintillaNET.AutomaticFold.Show | ScintillaNET.AutomaticFold.Click) 
            | ScintillaNET.AutomaticFold.Change)));
            this.editor_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editor_message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor_message.Location = new System.Drawing.Point(0, 0);
            this.editor_message.Name = "editor_message";
            this.editor_message.Size = new System.Drawing.Size(778, 233);
            this.editor_message.TabIndex = 1;
            this.editor_message.Text = "editor_message";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.combo_messages, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnValidateMessage, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 542);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(778, 29);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // combo_messages
            // 
            this.combo_messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combo_messages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_messages.FormattingEnabled = true;
            this.combo_messages.ItemHeight = 13;
            this.combo_messages.Location = new System.Drawing.Point(3, 3);
            this.combo_messages.Name = "combo_messages";
            this.combo_messages.Size = new System.Drawing.Size(659, 21);
            this.combo_messages.TabIndex = 3;
            this.combo_messages.SelectedIndexChanged += new System.EventHandler(this.combo_messages_SelectedIndexChanged);
            // 
            // btnValidateMessage
            // 
            this.btnValidateMessage.AutoSize = true;
            this.btnValidateMessage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnValidateMessage.Location = new System.Drawing.Point(668, 3);
            this.btnValidateMessage.Name = "btnValidateMessage";
            this.btnValidateMessage.Size = new System.Drawing.Size(107, 23);
            this.btnValidateMessage.TabIndex = 1;
            this.btnValidateMessage.Text = "Waliduj komunikat ";
            this.btnValidateMessage.UseVisualStyleBackColor = true;
            this.btnValidateMessage.Click += new System.EventHandler(this.btnValidateMessage_Click);
            // 
            // ValidatorToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 607);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ValidatorToolWindow";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Walidacja schematów walidacyjnych i komunikatów:";
            this.Shown += new System.EventHandler(this.ValidatorToolWindow_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnValidateMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ScintillaNET.Scintilla editor_schema;
        private ScintillaNET.Scintilla editor_message;
        private System.Windows.Forms.Label lblSchema;
        private System.Windows.Forms.ComboBox combo_schemas;
        private System.Windows.Forms.ComboBox combo_messages;
        private System.Windows.Forms.Button btnValidateSchema;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_download_schemas;
        private System.Windows.Forms.Button button_save;
    }
}