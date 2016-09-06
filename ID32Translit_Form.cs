﻿using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using NickBuhro.Translit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;


namespace ID32Translit
{
    struct ID32TranslitStruct
    {
        public string fileName;
        public string oldTitle;
        public string newTitle;
        public string oldArtist;
        public string newArtist;
        public string oldAlbum;
        public string newAlbum;

    }

    public partial class Form_ID2Translit : Form
    {
        private Dictionary<int, ID32TranslitStruct> _filesToRename = new Dictionary<int, ID32TranslitStruct>() { };

        public Form_ID2Translit()
        {
            InitializeComponent();
            label_SelectedFolder.Text = "";
            progressBar.Visible = false;
            textBox_Result.ScrollBars = ScrollBars.Vertical; 
            // TODO
            // Add localization
            

        }

        private void button_SelectFolder_Click(object sender, EventArgs e)
        {
            _filesToRename.Clear();

            dialog_SelectFolder.SelectedPath = "E:\\Music\\VK"; // TODO Remove this string

            dialog_SelectFolder.ShowDialog();

            if (dialog_SelectFolder.SelectedPath.Length >= 3)
            {
                label_SelectedFolder.Text = dialog_SelectFolder.SelectedPath;
                logData("Выбрали папку: '" + label_SelectedFolder.Text+"'"); // TODO - Localization;
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


        private void logData(String s)
        {
            if (s != null)
            {
                textBox_Result.AppendText(s);
                textBox_Result.AppendText("\r\n");
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
                logData("----");
                TagLib.File ID3 = TagLib.File.Create(file);
                if (!checkIfID3AreInEnglish(ref ID3))
                {
                    logData("Нашли MP3 с неанглоязычными тегами " + file); // TODO - Localization;
                    ID32TranslitStruct ID3Data = new ID32TranslitStruct() { };
                    ID3Data.fileName = file;
                    ID3Data.oldTitle = ID3.Tag.Title;
                    ID3Data.oldAlbum = ID3.Tag.Album;
                    ID3Data.oldArtist = ID3.Tag.FirstAlbumArtist;

                    logData("Текущий заголовок: '"+ID3.Tag.Title+"'");
                    if (isCyrillic(ID3.Tag.Title)){
                        ID3Data.newTitle = Transliteration.CyrillicToLatin(ID3.Tag.Title);
                        logData("Новый заголовок: '" + ID3Data.newTitle + "'"); // TODO - Localization;
                    }
                    else
                    {
                        logData("Нет кириллики - не переделываем заголовок."); // TODO - Localization;
                    }


                    logData("Текущий артист: '" + ID3.Tag.FirstPerformer + "'"); // TODO - Localization;
                    if (isCyrillic(ID3.Tag.FirstPerformer)){
                        ID3Data.newArtist = Transliteration.CyrillicToLatin(ID3.Tag.FirstPerformer);
                        logData("Новый артист: '" + ID3Data.newArtist + "'"); // TODO - Localization;
                    }
                    else
                    {
                        logData("Нет кириллики - не переделываем артиста."); // TODO - Localization;
                    }

                    logData("Текущее название альбома: '" + ID3.Tag.Album + "'"); // TODO - Localization;
                    if (isCyrillic(ID3.Tag.Album)){
                        ID3Data.newAlbum = Transliteration.CyrillicToLatin(ID3.Tag.Album);
                        logData("Новое название альбома: '"+ ID3Data.newAlbum + "'"); // TODO - Localization;
                    }
                    else
                    {
                        logData("Нет кириллики - не переделываем альбом."); // TODO - Localization;
                    }

                    _filesToRename[i] = ID3Data;
                    i++;
                    // TODO Remove this block
                    logData("Допсвойства для дебага");
                    logData("AlbumArtists: "+String.Join(" ", ID3.Tag.AlbumArtists));
                    logData("AlbumSort: " + ID3.Tag.AlbumSort);
                    logData("Comment: " + ID3.Tag.Comment);
                    logData("Composers: " + String.Join(" ", ID3.Tag.Composers));
                    logData("Conductor: " + ID3.Tag.Conductor);
                    logData("Copyright: " + ID3.Tag.Copyright);
                    logData("FirstAlbumArtist: " + ID3.Tag.FirstAlbumArtist);
                    logData("FirstComposer: " + ID3.Tag.FirstComposer);
                    logData("FirstGenre: " + ID3.Tag.FirstGenre);
                    logData("FirstPerformer: " + ID3.Tag.FirstAlbumArtist);
                    logData("Genres: " + String.Join(" ", ID3.Tag.Genres));
                    logData("Grouping: " + ID3.Tag.Grouping);
                    logData("IsEmpty: " + ID3.Tag.IsEmpty.ToString());
                    logData("JoinedAlbumArtists: " + ID3.Tag.JoinedAlbumArtists);
                    logData("JoinedComposers: " + ID3.Tag.JoinedComposers);
                    logData("JoinedGenres: " + ID3.Tag.JoinedGenres);
                    logData("JoinedPerformers: " + ID3.Tag.JoinedPerformers);
                    logData("Performers: " + String.Join(" ", ID3.Tag.Performers));
                    logData("TagTypes: " + String.Join(" ", ID3.Tag.TagTypes));
                    logData("Tag: " + ID3.Tag.TitleSort);
                    
                }
                else
                {
                    logData("Пропускаем: " + file); // TODO - Localization;
                }
                
                progressBar.PerformStep();
            }

            progressBar.Visible = false;

            logData("Завершили сканирование. Посмотрите - если всё устраивает - нажимайте Применить."); // TODO - Localization;
            button_ApplyResult.Enabled = true;
            button_EditResult.Enabled = true;
            
        }

        private Boolean checkIfID3AreInEnglish (ref TagLib.File ID3)
        {
            if (!IsEnglish(ID3.Tag.Title) || !IsEnglish(ID3.Tag.FirstPerformer) || !IsEnglish(ID3.Tag.Album))
            {
                return false;
            }
            return true;   
        }

        private void renameID3(int id)
        {
            logData("---");
            logData("Начали работу с файлом "+_filesToRename[id].fileName);

            TagLib.File ID3 = TagLib.File.Create(_filesToRename[id].fileName);

            if (!IsEnglish(ID3.Tag.Title) && !isCyrillic(ID3.Tag.Title) && checkBox_clearBadTags.Checked == true)
            {
                logData("Очищаем тайтл "+ID3.Tag.Title);
                ID3.Tag.Title = null;
            }
            if (!IsEnglish(ID3.Tag.FirstPerformer) && !isCyrillic(ID3.Tag.FirstPerformer) && checkBox_clearBadTags.Checked == true)
            {
                logData("Очищаем артиста " + ID3.Tag.FirstPerformer);
                ID3.Tag.Performers = null;
            }
            if (!IsEnglish(ID3.Tag.Album) && !isCyrillic(ID3.Tag.Album) && checkBox_clearBadTags.Checked == true)
            {
                logData("Очищаем альбом " + ID3.Tag.Album);
                ID3.Tag.Album = null;
            }

            if (ID3.Tag.Title == null && checkBox_SetFileNameToTitle.Checked == true)
            {
                string filename = Path.GetFileNameWithoutExtension(_filesToRename[id].fileName);
                if (!IsEnglish(filename))
                {
                    if (isCyrillic(filename))
                    {
                        ID3.Tag.Title = Transliteration.CyrillicToLatin(filename);
                        logData("Перекодируем файл "+filename);
                        logData("Ставим название из файла : "+ID3.Tag.Title);
                    }
                    else
                    {
                        logData("Название файла плохое: "+filename);
                    }
                }
                else
                {
                    logData("Ставим название из файла: " + filename);
                    ID3.Tag.Title = filename;
                }
                // Не забываем, что название файла тоже надо перекодировать в транслит
            }

            else
            {
                logData(ID3.Tag.Title+" => "+_filesToRename[id].newTitle);
                ID3.Tag.Title = _filesToRename[id].newTitle;
                logData(ID3.Tag.FirstPerformer + " => " + _filesToRename[id].newArtist);
                ID3.Tag.Performers = null;
                ID3.Tag.Performers = new[] { _filesToRename[id].newArtist };
                logData(ID3.Tag.Album + " => " + _filesToRename[id].newAlbum);
                ID3.Tag.Album = _filesToRename[id].newAlbum;
            }

            ID3.Save();
        }



        private Boolean isCyrillic(String s)
        {
            if (s != null)
            {
                return Regex.IsMatch(s, @"\p{IsCyrillic}") ;
                // TODO - менять кодировку и пробовать ещё раз, много тегов в другой кириллике
            }
            return false;
        }
        Boolean IsEnglish(string inputstring)
        {
            if (inputstring != null)
            {
                Regex regex = new Regex(@"[A-Za-z0-9 .,-=+(){}\[\]\\\?\|\}\{_@#\$\%\^\&\*\t]");
                MatchCollection matches = regex.Matches(inputstring);
                if (matches.Count == inputstring.Length)
                {
                    return true;
                }
            }
            return false;
        }

        private void button_ApplyResult_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены? Обратного пути не будет.", "Подтверждение выбора", MessageBoxButtons.YesNo); // TODO Localization
            if (dialogResult == DialogResult.Yes)
            {
                int c = _filesToRename.Count()-1;

                progressBar.Maximum = c;
                progressBar.Value = 1;
                progressBar.Step = 1;

                for (int i = 0; i <= c; i++)
                {
                    renameID3(i);
                    progressBar.PerformStep();
                }

                progressBar.Visible = false;
                logData("Всё закончилось. Нажмите правой кнопкой на текстовом поле чтобы очистить его или сохранить результат в файл."); // TODO Localization
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
    }
}
