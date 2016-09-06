using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using NickBuhro.Translit;
using System.Collections.Generic;


namespace ID32Translit
{

    public partial class Form_ID2Translit : Form
    {
        private Dictionary<int, ID32TranslitStruct> _filesToRename = new Dictionary<int, ID32TranslitStruct>() { };

        public Form_ID2Translit()
        {
            InitializeComponent();
            label_SelectedFolder.Text = "";
            progressBar.Visible = false;
            textBox_Result.ScrollBars = ScrollBars.Vertical;            
        }

        private void button_SelectFolder_Click(object sender, EventArgs e)
        {
            _filesToRename.Clear();

            dialog_SelectFolder.ShowDialog();

            if (dialog_SelectFolder.SelectedPath.Length >= 3)
            {
                label_SelectedFolder.Text = dialog_SelectFolder.SelectedPath;
                logData(label_SelectedFolder.Text);
                button_Scan.Enabled = true;
            }
            


        }

        private void label_SelectedFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label_SelectedFolder.Text.Count() != 0)
            {
                Process.Start(label_SelectedFolder.Text);
            }
        }




        private void button_Scan_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;

            String[] files;
            
            progressBar.Minimum = 1;
            
            if (checkBox_ScanRecursively.Checked)
            {
                files = Directory.GetFiles(label_SelectedFolder.Text, "*.mp3", SearchOption.AllDirectories);
            }
            else
            {
                files = Directory.GetFiles(label_SelectedFolder.Text, "*.mp3");
            }

            progressBar.Maximum = files.Length;
            progressBar.Value = 1;
            progressBar.Step = 1;

            int i = 0;

            foreach (string file in files) {
                TagLib.File ID3 = TagLib.File.Create(file);
                if (!ID32TranslitTools.checkIfID3AreInEnglish(ref ID3))
                {
                    ID32TranslitStruct ID3Data = new ID32TranslitStruct();
                    ID3Data.fileName = file;
                    ID3Data.oldTitle = ID3.Tag.Title;
                    ID3Data.oldAlbum = ID3.Tag.Album;
                    ID3Data.oldArtist = ID3.Tag.FirstPerformer;

                    if (ID32TranslitTools.isCyrillic(ID3.Tag.Title)){
                        ID3Data.newTitle = Transliteration.CyrillicToLatin(ID3.Tag.Title);
                    }
                    else if (ID32TranslitTools.isCyrillic(ID32TranslitTools.ConvertToUtf8(ID3.Tag.Title)))
                    {
                        ID3Data.newTitle = Transliteration.CyrillicToLatin(ID32TranslitTools.ConvertToUtf8(ID3.Tag.Title));
                    }
                    else if (ID32TranslitTools.IsEnglish(ID3.Tag.Title))
                    {
                        ID3Data.newTitle = ID3Data.oldTitle;
                    }
                    else
                    {
                        if(checkBox_SetFileNameToTitle.Checked == true)
                        {
                            string filename = Path.GetFileNameWithoutExtension(file);
                            if (ID32TranslitTools.isCyrillic(filename))
                            {
                                ID3Data.newTitle = Transliteration.CyrillicToLatin(filename);
                            }
                            else if (ID32TranslitTools.IsEnglish(filename))
                            {
                                ID3Data.newTitle = filename;
                            }
                            else
                            {
                                if (checkBox_clearBadTags.Checked == true)
                                {
                                    ID3Data.newTitle = null;
                                }
                                else
                                {
                                    ID3Data.newTitle = ID3Data.oldTitle;
                                }
                            }
                        }
                    }

                    if (ID32TranslitTools.isCyrillic(ID3.Tag.FirstPerformer)){
                        ID3Data.newArtist = Transliteration.CyrillicToLatin(ID3.Tag.FirstPerformer);
                    }
                    else if (ID32TranslitTools.isCyrillic(ID32TranslitTools.ConvertToUtf8(ID3.Tag.FirstPerformer)))
                    {
                        ID3Data.newArtist = Transliteration.CyrillicToLatin(ID32TranslitTools.ConvertToUtf8(ID3.Tag.FirstPerformer));
                    }
                    else if (ID32TranslitTools.IsEnglish(ID3.Tag.FirstPerformer))
                    {
                        ID3Data.newArtist = ID3Data.oldArtist;
                    }
                    else if(checkBox_clearBadTags.Checked == true)
                    {
                        ID3Data.newArtist = null;
                    }

                    if (ID32TranslitTools.isCyrillic(ID3.Tag.Album)){
                        ID3Data.newAlbum = Transliteration.CyrillicToLatin(ID3.Tag.Album);
                    }
                    else if (ID32TranslitTools.isCyrillic(ID32TranslitTools.ConvertToUtf8(ID3.Tag.Album)))
                    {
                        ID3Data.newAlbum = Transliteration.CyrillicToLatin(ID32TranslitTools.ConvertToUtf8(ID3.Tag.Album));
                    }
                    else if (ID32TranslitTools.IsEnglish(ID3.Tag.Album))
                    {
                        ID3Data.newAlbum = ID3Data.oldAlbum;
                    }
                    else if (checkBox_clearBadTags.Checked == true)
                    {
                        ID3Data.newAlbum = null;
                    }

                    _filesToRename[i] = ID3Data;
                    i++;  
                }
                progressBar.PerformStep();
            }

            progressBar.Visible = false;

            foreach (KeyValuePair<int, ID32TranslitStruct> entry in _filesToRename)
            {
                logData("Файл: "+entry.Value.fileName);
                logData(entry.Value.oldTitle+" -> "+entry.Value.newTitle);
                logData(entry.Value.oldArtist + " -> " + entry.Value.newArtist);
                logData(entry.Value.oldAlbum + " -> " + entry.Value.newAlbum);
                logData("---");
            }
            if (_filesToRename.Count() != 0)
            {
                logData("Завершили сканирование. Посмотрите - если всё устраивает - нажимайте Применить."); // TODO - Localization;
                button_ApplyResult.Enabled = true;
                button_EditResult.Enabled = true;
            }
            else
            {
                logData("Не нашли файлов, нуждающихся в обработке");
            }
            
            
        }


        private void button_ApplyResult_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены? Обратного пути не будет.", "Подтверждение выбора", MessageBoxButtons.YesNo); // TODO Localization
            if (dialogResult == DialogResult.Yes)
            {
                progressBar.Maximum = _filesToRename.Count() - 1;
                progressBar.Value = 1;
                progressBar.Step = 1;
                progressBar.Visible = true;
                foreach (KeyValuePair<int,ID32TranslitStruct> entry in _filesToRename)
                {
                    TagLib.File ID3 = TagLib.File.Create(entry.Value.fileName);
                    ID3.Tag.Title = entry.Value.newTitle;
                    ID3.Tag.Performers = null;
                    ID3.Tag.Performers = new[] { entry.Value.newArtist };
                    ID3.Tag.Album = entry.Value.newAlbum;
                    try
                    {
                        if (ID3.Writeable)
                        {
                            ID3.Save();
                        }
                        else
                        {
                            logData(entry.Value.fileName + " - не могу записать данные");
                        }
                    }
                    catch (System.UnauthorizedAccessException error)
                    {
                        logData(entry.Value.fileName + " - не могу записать данные: "+error.Message);
                    }
                    
                    progressBar.PerformStep();
                }
                

                progressBar.Visible = false;
                logData("Всё закончилось. Нажмите правой кнопкой на текстовом поле чтобы очистить его или сохранить результат в файл."); // TODO Localization
                _filesToRename.Clear();
                button_ApplyResult.Enabled = false;
                button_EditResult.Enabled = false;
            }
        }

        private void checkBox_SetFileNameToTitle_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_SetFileNameToTitle.Checked == true)
            {
                checkBox_clearBadTags.Enabled = true;
            }
            else
            {
                checkBox_clearBadTags.Enabled = false;
                checkBox_clearBadTags.Checked = false;
            }
        }

        private void button_EditResult_Click(object sender, EventArgs e)
        {
            // TODO
            DialogResult dialogResult = MessageBox.Show("В следующей версии", "Не готово", MessageBoxButtons.OK); // TODO Localization
        }

        private void toolStripMenu_ClearTextBox_Click(object sender, EventArgs e)
        {
            textBox_Result.Clear();
        }

        private void ToolStripMenuItem_SaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "ID32Translit_result.txt";
            save.Filter = "Text File | *.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.OpenFile());
                writer.WriteLine(textBox_Result.Text);
                writer.Dispose();
                writer.Close();
            }
        }

        private void logData(String s)
        {
            if (s != null)
            {
                textBox_Result.AppendText(s);
                textBox_Result.AppendText("\r\n");
            }
        }
    }
}
