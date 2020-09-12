using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Platom;
using Platom.Protocol.Schema;
using Platom.Protocol.Schema.Validators;
using PlatomMonitor.Windows.Models;
using ScintillaNET;

namespace PlatomMonitor.Windows
{
    public partial class ValidatorToolWindow : Form
    {
        private List<SchemaEntry> examples;

        public ValidatorToolWindow()
        {
            InitializeComponent();

            InitializeScintillaEditor(this.editor_message);
            InitializeScintillaEditor(this.editor_schema);

            this.editor_message.Text = editor_schema.Text = "{\r\n    // tutaj można pisać...\r\n}\r\n";
            this.editor_message.SetSavePoint();
            this.editor_schema.SetSavePoint();

        }


        void InitializeScintillaEditor(Scintilla editor)
        {
            editor.Styles[Style.Default].Font = "Consolas";
            editor.Styles[Style.Default].Size = 10;
            editor.Styles[Style.Default].ForeColor = Color.FromArgb(0x21, 0x21, 0x21);
            editor.Styles[Style.Default].BackColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
            editor.Styles[Style.Json.Keyword].ForeColor = Color.Blue;


            editor.Styles[Style.Json.Default].ForeColor = Color.Silver;
            editor.Styles[Style.Json.BlockComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            editor.Styles[Style.Json.LineComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            editor.Styles[Style.Json.Number].ForeColor = Color.Olive;
            editor.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
            editor.Styles[Style.Json.PropertyName].BackColor = Color.WhiteSmoke;
            editor.Styles[Style.Json.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            editor.Styles[Style.Json.StringEol].BackColor = Color.Pink;
            editor.Styles[Style.Json.Operator].ForeColor = Color.Purple;
            editor.Lexer = Lexer.Json;

            editor.SetKeywords(0, "true false null");
            
            editor.TabWidth = 2;
            editor.UseTabs = false;

            

            editor.SetProperty("fold", "1");
            editor.SetProperty("fold.compact", "1");

            editor.Margins[0].Width = 16;
            editor.Margins[0].Type = MarginType.Number;

            editor.Margins[3].Type = MarginType.Symbol;
            editor.Margins[3].Mask = Marker.MaskFolders;
            editor.Margins[3].Sensitive = true;
            editor.Margins[3].Width = 20;

            for (int i = 25; i <= 31; i++)
            {
                editor.Markers[i].SetBackColor(Color.FromArgb(20,20,20)); 
                editor.Markers[i].SetForeColor(Color.FromArgb(240,240,240));
            }

            editor.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            editor.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            editor.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            editor.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            editor.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            editor.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            editor.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            editor.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;

        }

        private void LoadExampleButtonClicked(object sender, EventArgs e)
        {
            //if (sender == this.button_schema1)
            //    this.editor_schema.Text = Platom.Protocol.Properties.Resources.schema_ect_potentials;
            //if (sender == this.button_schema2)
            //    this.editor_schema.Text = Platom.Protocol.Properties.Resources.schema_status;

            //if (sender == this.button_message1)
            //    this.editor_message.Text = Platom.Protocol.Properties.Resources.message_et3_measurements;
            //if (sender == this.button_message2)
            //    this.editor_message.Text = Platom.Protocol.Properties.Resources.message_status;
        }

        private void btnValidateSchema_Click(object sender, EventArgs e)
        {
            try
            {
                SchemaValidator sv = new SchemaValidator(this.editor_schema.Text);
                MessageBox.Show("Walidacja schematu walidacyjnego przebiegła pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidatorException ve)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Wyjątek podczas walidacji schematu walidacyjnego.\r\n");
                sb.AppendLine(ve.Message);
                MessageBox.Show(sb.ToString(), "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnValidateMessage_Click(object sender, EventArgs e)
        {
            SchemaValidator sv = null;
            try
            {
                 sv = new SchemaValidator(this.editor_schema.Text);
            }
            catch (ValidatorException ve)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Wyjątek podczas walidacji schematu walidacyjnego.\r\n");
                sb.AppendLine(ve.Message);
                MessageBox.Show(sb.ToString(), "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //

            try
            {
                sv.ValidateMessage(this.editor_message.Text);
                MessageBox.Show("Walidacja komunikatu przebiegła pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidatorException ve)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Wyjątek podczas walidacji komunikatu.\r\n");
                sb.AppendLine(ve.Message);
                MessageBox.Show(sb.ToString(), "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }


        private void ValidatorToolWindow_Shown(object sender, EventArgs e)
        {
            using (RepositoryUpdater updater = new RepositoryUpdater())
            {
                updater.StartUpdate(true, true);
                this.examples = updater.Examples;
            }

            this.combo_schemas.Items.Clear();
            this.combo_schemas.Items.AddRange(this.examples.ToArray());
            UpdateDropDownListWidth(this.combo_schemas);
        }


        private void btn_download_schemas_Click(object sender, EventArgs e)
        {
            using (RepositoryUpdater updater = new RepositoryUpdater())
            {
                updater.StartUpdate(false, false);
                this.examples = updater.Examples;
            }

            this.combo_schemas.Items.Clear();
            this.combo_schemas.Items.AddRange(this.examples.ToArray());
            UpdateDropDownListWidth(this.combo_schemas);
        }

        private void UpdateDropDownListWidth(ComboBox combo)
        {
            if (combo.Items.Count == 0)
                return;
            object[] items = new object[combo.Items.Count];
            combo.Items.CopyTo(items, 0);
            combo.DropDownWidth = items.Select(obj => TextRenderer.MeasureText(combo.GetItemText(obj), combo.Font).Width).Max();
        }

        private void combo_schemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Models.SchemaEntry se = this.combo_schemas.SelectedItem as Models.SchemaEntry;
            
            this.combo_messages.Items.Clear();
            this.combo_messages.Items.AddRange(se.Messages.ToArray());
            this.combo_messages.Text = $"Wybierz przykładowy komunikat dla schematu {se.SchemaName}...";
            UpdateDropDownListWidth(this.combo_messages);


            if (this.editor_schema.Modified)
            {
                if (MessageBox.Show("Treść schematu została zmieniona. Czy chcesz załadować nowy schemat i utracić zmiany w istniejącym?", "Pytanie",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            this.editor_schema.Text = se.Text;
            this.editor_schema.SetSavePoint();
        }

        private void combo_messages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Models.MessageEntry me = this.combo_messages.SelectedItem as Models.MessageEntry;

            if (this.editor_message.Modified)
            {
                if (MessageBox.Show("Treść komunikatu została zmieniona. Czy chcesz załadować nowy komunikat i utracić zmiany w istniejącym?", "Pytanie",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            this.editor_message.Text = me.Text;
            this.editor_message.SetSavePoint();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitlabplatom.cti.p.lodz.pl/tjaworski/validation_schemas#dodawanie-schemat%C3%B3w-i-komunikat%C3%B3w");
        }
    }
}
