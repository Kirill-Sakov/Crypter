using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Crypter
{

    public partial class FormMainWindow : Form
    {
        /// <summary>
        /// Путь до директории приложения
        /// </summary>
        string ExeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        
        /// <summary>
        /// Для подсчёта процента готовности
        /// </summary>
        private int highestPercentageReached = 0;
        
        /// <summary>
        /// Структура для передачи данных в ассинхронный поток
        /// </summary>
        private struct CrypterPack
        {
            public string key { get; set; }
            public string iv { get; set; }
            public string[] paths { get; set; }
        }

        public FormMainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Анимация вхождения DragEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFiles_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Обработка события DragDrop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var file in files)
            {
                var existingFile = listViewFiles.FindItemWithText(file);
                if (existingFile != null)
                {
                    // Если указанный файл уже добавлен
                    continue;
                }
                else
                    listViewFiles.Items.Add(file);
            }
        }

        /// <summary>
        /// Кнопка Добавить файлы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = new Uri(Path.Combine(ExeDirectory, "data\\")).LocalPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in openFileDialog.FileNames)
                    {
                        var existingFile = listViewFiles.FindItemWithText(file);
                        if (existingFile != null)
                        {
                            // Если указанный файл уже добавлен
                            continue;
                        }
                        else
                            listViewFiles.Items.Add(file);
                    }
                }
            }            
        }

        /// <summary>
        /// Кнопка удалить файлы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteFiles_Click(object sender, EventArgs e)
        {            
            try
            {
                DeleteSelectFiles();
            }
            catch (Exception ex)
            {
                var result = MessageBox.Show(ex.Message + "\rИсключить все файлы?", "Внимание", MessageBoxButtons.YesNoCancel);

                // If the no button was pressed ...
                if (result == DialogResult.No || result == DialogResult.Cancel)
                {
                    // cancel the closure of the form.
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    listViewFiles.Items.Clear();
                }
            }
        }

        /// <summary>
        /// Функция удаления выделенных в данный момент элементов
        /// </summary>
        private void DeleteSelectFiles()
        {
            if (listViewFiles.SelectedItems.Count <= 0)
            {
                throw new Exception("Не выделено ни одного файла.");
            }
            foreach (ListViewItem eachItem in listViewFiles.SelectedItems)
            {
                listViewFiles.Items.Remove(eachItem);
            }
        }

        /// <summary>
        /// Удаление с помощью backspace & delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFiles_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                    DeleteSelectFiles();
            }
            catch (Exception ex)
            {
                var result = MessageBox.Show(ex.Message + "\rИсключить все файлы?", "Внимание", MessageBoxButtons.YesNoCancel);

                // If the no button was pressed ...
                if (result == DialogResult.No || result == DialogResult.Cancel)
                {
                    // cancel the closure of the form.
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    listViewFiles.Items.Clear();
                }
            }
        }

        /// <summary>
        /// Выставление стандратного цвета для элемента listView
        /// </summary>
        private void UncolorItems()
        {
            foreach (ListViewItem item in listViewFiles.Items)
            {
                item.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Включение/отключение работоспособности элементов интерфейса
        /// </summary>
        /// <param name="start"></param>
        private void CrypterStarted(bool start)
        {
            buttonCancel.Enabled = start;

            buttonAddFiles.Enabled = !start;
            buttonDeleteFiles.Enabled = !start;
            buttonOpenKey.Enabled = !start;
            checkBoxTwoKeys.Enabled = !start;

            treeView.Enabled = !start;
            listViewFiles.Enabled = !start;
        }

        /// <summary>
        /// Возвращает все выбранные папки из TreeView
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static IEnumerable<TreeNode> Descendants(TreeNodeCollection c)
        {
            foreach (var node in c.OfType<TreeNode>())
            {
                yield return node;

                foreach (var child in Descendants(node.Nodes))
                {
                    yield return child;
                }
            }
        }

        private static IEnumerable<ListViewItem> GetAllItemsFrom(ListView.ListViewItemCollection c)
        {
            foreach (var item in c.OfType<ListViewItem>())
            {
                yield return item;
            }
        }

        private static IEnumerable<string> GetAllFilesPathFrom(string[] c)
        {
            foreach (var path in c.OfType<string>())
            {
                if (File.Exists(path))
                {
                    yield return path;
                }
                else if (Directory.Exists(path))
                {
                    foreach (var child in GetAllFilesPathFrom(Directory.GetFileSystemEntries(path)))
                    {
                        yield return child;
                    }
                }
            }
        }

        /// <summary>
        /// Кнопка Зашифровать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCryptro_Click(object sender, EventArgs e)
        {
            // Пытаемся считать установленный ключ
            string key = string.Empty;
            string iv = string.Empty;
            try
            {
                // Если ключ длиннее 48 байт (143 - потому что байты представлены в hex виде)
                if (textBoxKey.Text.Length > 143)
                {
                    throw new Exception();
                }
                key = textBoxKey.Text.Substring(0, 95);
                iv = textBoxKey.Text.Substring(96, 47);
            }
            catch (Exception)
            {
                var result = MessageBox.Show("Файлы зашифруются новым ключом, который в дальнейшем можно будет использовать", "Ключ отсутствует или задан неверно", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    key = string.Empty;
                    iv = string.Empty;
                }
                else if(result == DialogResult.Cancel)
                {
                    return;
                }
                
            }


            // Переносим все выбранные папки treeView в список
            var selectedFolders = GetAllFilesPathFrom(Descendants(treeView.Nodes)
                .Where(n => n.Checked && n.Tag != null)
                .Select(n => n.Tag.ToString())
                .ToArray()).ToArray();

            var listPaths = GetAllFilesPathFrom(GetAllItemsFrom(listViewFiles.Items)
                .Select(n => n.Text)
                .ToArray()).ToArray();
            
            string[] allPaths = selectedFolders.Union(listPaths).ToArray();

            listViewFiles.Clear();

            // добавляем файлы в listView
            foreach (var path in allPaths)
            {
                listViewFiles.Items.Add(path.ToString());
            }

            // Проверяем наличие файлов
            if (listViewFiles.Items.Count <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите файлы для шифрования", "Не выбраны файлы", MessageBoxButtons.OK);
                return;
            }


            // Удаляем все подсветы
            UncolorItems();

            // Начальные логи
            logBar.Text = "Logs: Crypt started...";
            progressBar.Value = 0;
            highestPercentageReached = 0;

            // Если процесс уже не запущен, запускаем
            if (backgroundCrypter.IsBusy == false && backgroundDecrypter.IsBusy == false)
            {
                CrypterPack cryptPack = new CrypterPack();

                // Если есть ключи запускаем с ключом
                if (key != String.Empty && iv != String.Empty)
                {
                    // Заносим информацию о ключах
                    cryptPack.iv = iv;
                    cryptPack.key = key;
                    cryptPack.paths = allPaths;

                    backgroundCrypter.RunWorkerAsync(cryptPack);
                }
                // Если ключа нет, запускаем только по путям
                else
                {
                    cryptPack.paths = allPaths;
                    // В отдельном потоке
                    backgroundCrypter.RunWorkerAsync(cryptPack);
                }

                CrypterStarted(true);
            }
            else
            {
                MessageBox.Show("Процесс уже запущен.", "Невозможно запустить второй раз", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Асинхронный поток шифрования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Принимаем bgWorker
            BackgroundWorker worker = sender as BackgroundWorker;

            // Принимаем пути всех файлов и ключи
            CrypterPack cryptPack = (CrypterPack)e.Argument;

            // Шифруем файлы
            e.Result = EncryptFilesAES(cryptPack, worker, e);
        }

        /// <summary>
        /// Функция шифрования файлов AES
        /// </summary>
        /// <param name="files"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private object EncryptFilesAES(CrypterPack cryptPack, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Количество готовых файлов
            int countDoneCrypt = 0;

            // Создаём объект aes
            using (Aes aes = Aes.Create())
            {
                CrypterPack packForKeys = new CrypterPack();

                if (cryptPack.iv != null && cryptPack.key != null)
                {
                    // Ставим другие ключи
                    aes.IV = FromHex(cryptPack.iv);
                    aes.Key = FromHex(cryptPack.key);

                    packForKeys.iv = cryptPack.iv;
                    packForKeys.key = cryptPack.key;
                }
                else
                {
                    // Считываем новые
                    packForKeys.iv = BitConverter.ToString(aes.IV);
                    packForKeys.key = BitConverter.ToString(aes.Key);
                } 
                

                // Если объект AES успешно создан, записываем ключи в Бокс
                worker.ReportProgress(highestPercentageReached, packForKeys);

                // Для каждого файла из файлов
                foreach (string filePath in cryptPack.paths)
                {
                    // Проверяем не отменил ли пользователь операцию
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        try
                        {
                            if (filePath.Contains(".crypto"))
                                throw new CryptographicException("Шифрование уже зашифрованного файла.");
                             
                            EncryptFileFromAES(filePath, aes.Key, aes.IV);
                        }
                        catch (Exception ex)
                        {
                            ex.HelpLink = filePath;
                            worker.ReportProgress(highestPercentageReached, ex);
                            continue;
                        }
                        
                        countDoneCrypt++;

                        // Report progress as a percentage of the total task.
                        int percentComplete =
                            countDoneCrypt * 100 / cryptPack.paths.Length;

                        highestPercentageReached = percentComplete;
                        worker.ReportProgress(percentComplete, filePath);
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Шифрование файла с помощью AES
        /// </summary>
        /// <param name="file_path"></param>
        /// <param name="key"></param>
        /// <param name="iV"></param>
        /// <returns></returns>
        private void EncryptFileFromAES(string file_path, byte[] key, byte[] iV)
        {
            // Check arguments.
            if (file_path == null || file_path.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iV == null || iV.Length <= 0)
                throw new ArgumentNullException("IV");

            
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = iV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                
                #region Old Worked
                //// Create the streams used for encryption.
                //using (MemoryStream msEncrypt = new MemoryStream())
                //{
                //    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                //    {
                //        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                //        {
                //            using (StreamReader srEncrypt = new StreamReader(file_path, Encoding.Default))
                //            {
                //                var tmp = srEncrypt.ReadToEnd();
                //                swEncrypt.Write(tmp);
                //            }
                //        }
                //    }
                //    //Перезаписываем шифрованный файл на место файла
                //    using (FileStream openFile = File.Create(file_path))
                //    {
                //        openFile.Write(msEncrypt.ToArray(), 0, msEncrypt.ToArray().Length);
                //    }
                //    File.Move(file_path, file_path + ".crypto");
                //}

                #endregion

                #region New Perfect Worked
                
                string tmpPath = Path.GetTempFileName();
                using (FileStream fsSrc = File.OpenRead(file_path))
                using (FileStream fsDst = File.Create(tmpPath))
                {
                    using (CryptoStream cs = new CryptoStream(fsDst, encryptor, CryptoStreamMode.Write, true))
                    {
                        fsSrc.CopyTo(cs);
                    }
                }
                File.Delete(file_path);
                File.Move(tmpPath, file_path + ".crypto");

                #endregion
            }
        }

        /// <summary>
        /// Обработчик для ReportProgress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            logBar.Text = $"Crypt {e.ProgressPercentage.ToString()}%";
            progressBar.Value = e.ProgressPercentage;

            if (e.UserState != null)
            {
                switch (e.UserState.GetType().Name)
                {
                    case "CrypterPack":

                        CrypterPack crypterPack = (CrypterPack)e.UserState;

                        string tmp = crypterPack.key + "-" + crypterPack.iv;

                        // Если установлен флажок доп защиты, шифруем ключ.
                        if (checkBoxTwoKeys.Checked)
                        {
                            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                            {
                                try
                                {
                                    // Если ключ уже установлен, используем его
                                    if (textBoxRsaKey.Text.Length > 0)
                                    {
                                        rsa.FromXmlString(textBoxRsaKey.Text);
                                        textBoxKey.Text = BitConverter.ToString(rsa.Encrypt(FromHex(tmp), false));
                                    }
                                    else
                                    {
                                        // Иначе генерируем и устанавливаем новый
                                        textBoxRsaKey.Text = rsa.ToXmlString(true);
                                        textBoxKey.Text = BitConverter.ToString(rsa.Encrypt(FromHex(tmp), false));
                                    }
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Ошибка при попытке зашифровать основной ключ. Оставлен оригинал основного ключа", 
                                        "Ошибка формирования дополнительного уровня защиты", 
                                        MessageBoxButtons.OK);

                                    textBoxKey.Text = tmp;
                                }
                            }
                        }
                        else
                        {
                            textBoxKey.Text = tmp;
                        }

                        break;

                    case "String":

                        string cryptFilePath = (string)e.UserState;
                        if (listViewFiles.Items.Contains(listViewFiles.FindItemWithText(cryptFilePath)))
                        {
                            int index = listViewFiles.FindItemWithText(cryptFilePath).Index;
                            
                            listViewFiles.Items[index].Text = cryptFilePath + ".crypto";
                            listViewFiles.Items[index].BackColor = Color.GreenYellow;
                            listViewFiles.Items[index].EnsureVisible();
                        }

                        break;

                    case "CryptographicException":

                        Exception ex = (Exception)e.UserState;
                        string filePath = ex.HelpLink;
                        if (listViewFiles.Items.Contains(listViewFiles.FindItemWithText(filePath)))
                        {
                            int index = listViewFiles.FindItemWithText(filePath).Index;

                            listViewFiles.Items[index].BackColor = Color.Red;
                            listViewFiles.Items[index].EnsureVisible();
                        }

                        break;
                }
            }

            
        }

        /// <summary>
        /// Когда поток закончил шифрование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }

            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                logBar.Text = "Crypting: Canceled";
                progressBar.Value = 0;
            }

            else
            {
                logBar.Text = "Crypting: done";
                progressBar.Value = 100;
            }


            CrypterStarted(false);
        }


        /// <summary>
        /// Кнопка расшифровать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            // Если установлен флажок "Доп. защиты", то расшифроваем зашифрованный AES ключ с помощью доп. ключа RSA            
            try
            {
                if (checkBoxTwoKeys.Checked)
                {
                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                    {
                        rsa.FromXmlString(textBoxRsaKey.Text);

                        textBoxKey.Text = BitConverter.ToString(rsa.Decrypt(FromHex(textBoxKey.Text), false));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно расшифровать основной ключ с помощью дополнительного.", "Неверные ключи.", MessageBoxButtons.OK);
                return;
            }

            // Считывание ключа
            string key = string.Empty;
            string iv = string.Empty;
            try
            {
                key = textBoxKey.Text.Substring(0, 95);
                iv = textBoxKey.Text.Substring(96, 47);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный ключ!", "Ошибка!", MessageBoxButtons.OK);
                return;
            }


            // Переносим все выбранные папки treeView в список
            var selectedFolders = GetAllFilesPathFrom(Descendants(treeView.Nodes)
                .Where(n => n.Checked && n.Tag != null)
                .Select(n => n.Tag.ToString())
                .ToArray()).ToArray();

            var listPaths = GetAllFilesPathFrom(GetAllItemsFrom(listViewFiles.Items)
                .Select(n => n.Text)
                .ToArray()).ToArray();

            string[] allPaths = selectedFolders.Union(listPaths).ToArray();

            listViewFiles.Clear();

            // добавляем файлы в listView
            foreach (var path in allPaths)
            {
                listViewFiles.Items.Add(path.ToString());
            }

            // Проверка элементов
            if (listViewFiles.Items.Count <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите файлы для дешифрования", "Не выбраны файлы", MessageBoxButtons.OK);
                return;
            }

            // Убираем все подсветы
            UncolorItems();

            // Начальные логи
            logBar.Text = "Logs: Decrypt started...";
            progressBar.Value = 0;
            highestPercentageReached = 0;

            if (backgroundCrypter.IsBusy == false && backgroundDecrypter.IsBusy == false)
            {
                CrypterPack decryptPack = new CrypterPack();
                decryptPack.key = key;
                decryptPack.iv = iv;
                decryptPack.paths = allPaths;

                backgroundDecrypter.RunWorkerAsync(decryptPack);
                
                CrypterStarted(true);
            }
            else
            {
                MessageBox.Show("Процесс уже запущен.", "Невозможно запустить второй раз", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Асинхронный поток для дешифрования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundDecrypter_DoWork(object sender, DoWorkEventArgs e)
        {
            // Принимаем bgWorker
            BackgroundWorker worker = sender as BackgroundWorker;

            // Принимаем пути всех файлов
            CrypterPack decryptPack = (CrypterPack)e.Argument;

            // Дешифруем файлы
            e.Result = DecryptFilesAES(decryptPack, worker, e);
        }

        /// <summary>
        /// Дешифрование файлов
        /// </summary>
        /// <param name="decryptPack"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private object DecryptFilesAES(CrypterPack decryptPack, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Количество готовых файлов
            int countDoneDecrypt = 0;

            using (Aes aes = Aes.Create())
            {
                aes.Key = FromHex(decryptPack.key);
                aes.IV = FromHex(decryptPack.iv);

                foreach (string filePath in decryptPack.paths)
                {
                    // Проверяем не отменил ли пользователь операцию
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        try
                        {
                            // Дешифруем с помощью AES и ключей
                            DecryptFileFromAES(filePath, aes.Key, aes.IV);
                        }
                        catch (Exception ex)
                        {
                            ex.HelpLink = filePath;
                            worker.ReportProgress(highestPercentageReached, ex);
                            continue;
                        }
                        
                        countDoneDecrypt++;

                        // Report progress as a percentage of the total task.
                        int percentComplete =
                            countDoneDecrypt * 100 / decryptPack.paths.Length;

                        highestPercentageReached = percentComplete;
                        worker.ReportProgress(percentComplete, filePath);
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// ProgressChanged for Decrypt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundDecrypter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            logBar.Text = $"Decrypt {e.ProgressPercentage.ToString()}%";
            progressBar.Value = e.ProgressPercentage;

            if (e.UserState != null)
            {
                switch (e.UserState.GetType().Name)
                {
                    case "String":
                        string cryptFilePath = (string)e.UserState;
                        if (listViewFiles.Items.Contains(listViewFiles.FindItemWithText(cryptFilePath)))
                        {
                            int index = listViewFiles.FindItemWithText(cryptFilePath).Index;
                            listViewFiles.Items[index].Text = cryptFilePath.Replace(".crypto", "");
                            listViewFiles.Items[index].BackColor = Color.GreenYellow;
                            listViewFiles.Items[index].EnsureVisible();
                        }
                        break;

                    case "CryptographicException":

                        Exception exCrypto = (Exception)e.UserState;
                        string filePath = exCrypto.HelpLink;
                        if (listViewFiles.Items.Contains(listViewFiles.FindItemWithText(filePath)))
                        {
                            int index = listViewFiles.FindItemWithText(filePath).Index;

                            listViewFiles.Items[index].BackColor = Color.Red;
                            listViewFiles.Items[index].EnsureVisible();
                        }

                        break;

                    case "FileNotFoundException":

                        Exception exFile = (Exception)e.UserState;
                        string file = exFile.HelpLink;
                        if (listViewFiles.Items.Contains(listViewFiles.FindItemWithText(file)))
                        {
                            int index = listViewFiles.FindItemWithText(file).Index;

                            listViewFiles.Items[index].BackColor = Color.Red;
                            listViewFiles.Items[index].EnsureVisible();
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Decrypting complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundDecrypter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }

            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                logBar.Text = "Decrypting: Canceled";
                progressBar.Value = 0;
            }

            else
            {
                logBar.Text = "Decrypting: done";
                progressBar.Value = 100;
            }

            CrypterStarted(false);
        }

        /// <summary>
        /// Функция дешифрования файла
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <param name="iV"></param>
        /// <returns></returns>
        private void DecryptFileFromAES(string filePath, byte[] key, byte[] iV)
        {
            // Check arguments.
            if (filePath == null || filePath.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iV == null || iV.Length <= 0)
                throw new ArgumentNullException("IV");
            

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = iV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                

                #region Old Worked
                //using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
                //{
                //    var tmp = Encoding.Default.GetBytes(reader.ReadToEnd());
                //    // Create the streams used for decryption.
                //    using (MemoryStream msDecrypt = new MemoryStream(tmp))
                //    {
                //        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                //        {
                //            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                //            {
                //                reader.Close();

                //                string chiperText = srDecrypt.ReadToEnd();

                //                // Перезаписываем шифрованный файл на место файла
                //                using (FileStream openFile = File.Create(filePath))
                //                {
                //                    // Перезаписываем шифрованный файл на место файла
                //                    using (StreamWriter writer = new StreamWriter(openFile, Encoding.Default))
                //                    {
                //                        writer.Write(chiperText);
                //                    }
                //                }

                //                File.Move(filePath, filePath.Replace(".crypto", ""));
                //            }
                //        }
                //    }
                //}
                #endregion

                #region New Perfect Worked
                
                string tmpPath = Path.GetTempFileName();
                using (FileStream fsSrc = File.OpenRead(filePath))
                {
                    using (CryptoStream cs = new CryptoStream(fsSrc, decryptor, CryptoStreamMode.Read, true))
                    using (FileStream fsDst = File.Create(tmpPath))
                    {
                        cs.CopyTo(fsDst);
                    }
                }
                File.Delete(filePath);
                File.Move(tmpPath, filePath.Replace(".crypto", ""));

                #endregion
            }
        }

        /// <summary>
        /// 16-чная система в байты
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        /// <summary>
        /// Кнопка отмены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_ButtonClick(object sender, EventArgs e)
        {
            if (backgroundCrypter.IsBusy)
            {
                backgroundCrypter.CancelAsync();
                buttonCancel.Enabled = false;
            }
            else if (backgroundDecrypter.IsBusy)
            {
                backgroundDecrypter.CancelAsync();
                buttonCancel.Enabled = false;
            }
        }

        /// <summary>
        /// Открыть ключ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenKey_Click(object sender, EventArgs e)
        {
            // Если папки с ключами не было, создаём...
            if (!Directory.Exists(Path.Combine(ExeDirectory, "keys"))){

                Directory.CreateDirectory(new Uri(Path.Combine(ExeDirectory, "keys")).LocalPath);
            }
            
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Для openFileDialog
                openFileDialog.InitialDirectory = new Uri(Path.Combine(ExeDirectory, "keys")).LocalPath;
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "*Key file|*.key";

                DialogResult result;
                if ((result = openFileDialog.ShowDialog()) == DialogResult.OK)
                {
                    textBoxKey.Text = OpenKeyFromPath(openFileDialog.FileName);
                }
            }
        }

        private void textBoxKey_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void textBoxKey_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (!files[0].Contains(".key"))
            {
                MessageBox.Show("Неверное расширение ключа");
                return;
            }

            textBoxKey.Text = OpenKeyFromPath(files[0]);
        }

        /// <summary>
        /// Открыть ключ по указанному пути
        /// </summary>
        /// <param name="path"></param>
        private static string OpenKeyFromPath(string path)
        {
            byte[] key = File.ReadAllBytes(path);

            return BitConverter.ToString(key);
        }

        /// <summary>
        /// Сохранить ключ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveKey_Click(object sender, EventArgs e)
        {
            if (textBoxKey.Text.Length <= 0)
            {
                MessageBox.Show("Пустой ключ");
            }
            // Если папки с ключами не было, создаём...
            if (!Directory.Exists(Path.Combine(ExeDirectory, "keys")))
            {

                Directory.CreateDirectory(new Uri(Path.Combine(ExeDirectory, "keys")).LocalPath);

            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = new Uri(Path.Combine(ExeDirectory, "keys")).LocalPath;

                saveFileDialog.Filter = "*Key file|*.key";
                saveFileDialog.FilterIndex = 0;

                saveFileDialog.FileName = DateTime.Now.Ticks.ToString();

                DialogResult result;
                if ((result = saveFileDialog.ShowDialog()) == DialogResult.OK)
                {
                    byte[] key = FromHex(textBoxKey.Text);

                    File.WriteAllBytes(saveFileDialog.FileName, key);

                    File.Move(saveFileDialog.FileName, saveFileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Обработчик, вызывающий дополнительная выдвигающуюся область с элементами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxTwoKeys_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTwoKeys.Checked)
            {
                for (int i = 0; i < 200; i++)
                {
                    Width += 1;
                }

                groupBoxRsaKey.Visible = true;

                buttonCancel.Width += 200;

            }

            if (checkBoxTwoKeys.Checked == false)
            {
                groupBoxRsaKey.Visible = false;

                buttonCancel.Width -= 200;

                for (int i = 0; i < 200; i++)
                {
                    Width -= 1;
                }               

            }
        }

        private void FormMainWindow_Load(object sender, EventArgs e)
        {
            // Получаем диски системы 
            string[] drivers = Environment.GetLogicalDrives();

            int driveImage;

            // Load the images in an ImageList.
            ImageList myImageList = new ImageList();
            myImageList.Images.Add(Image.FromFile(Path.GetFullPath("imageListIcons\\imageres_CdRom.ico")));
            myImageList.Images.Add(Image.FromFile(Path.GetFullPath("imageListIcons\\imageres_DefaultDisk.ico")));
            myImageList.Images.Add(Image.FromFile(Path.GetFullPath("imageListIcons\\imageres_NetworkDisk.ico")));
            myImageList.Images.Add(Image.FromFile(Path.GetFullPath("imageListIcons\\imageres_NoRootDir.ico")));
            myImageList.Images.Add(Image.FromFile(Path.GetFullPath("imageListIcons\\imageres_Folder.ico")));
            myImageList.Images.Add(Image.FromFile(Path.GetFullPath("imageListIcons\\imageres_OpenFolder.ico")));
            myImageList.Images.Add(Image.FromFile(Path.GetFullPath("imageListIcons\\imageres_Unauthorized.ico")));
            

            // Assign the ImageList to the TreeView.
            treeView.ImageList = myImageList;

            foreach (string drive in drivers)
            {
                DriveInfo di = new DriveInfo(drive);

                switch (di.DriveType)    //set the drive's icon
                {
                    case DriveType.CDRom:
                        driveImage = 0;
                        break;
                    case DriveType.Network:
                        driveImage = 2;
                        break;
                    case DriveType.NoRootDirectory:
                        driveImage = 3;
                        break;
                    case DriveType.Unknown:
                        driveImage = 1;
                        break;
                    default:
                        driveImage = 1;
                        break;
                }

                TreeNode node = new TreeNode(drive, driveImage, driveImage);
                node.Tag = drive;

                if (di.IsReady)
                {
                    node.Nodes.Add("...");
                }

                treeView.Nodes.Add(node);

                // Ключи использовавшийся для тестов
                //textBoxKey.Text = "75-BA-FD-82-6D-54-6B-DA-F0-40-B9-46-BB-43-79-0B-1B-8C-54-3F-52-D8-32-D3-D7-62-91-BF-98-27-7A-6D-64-B0-97-65-3D-A6-EE-5D-7C-18-14-E7-55-C1-FF-C4";

                listViewFiles.GetType()
                                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                                .SetValue(listViewFiles, true, null);

            }
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "..." && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();

                    //get the list of sub direcotires
                    string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());

                    foreach (string dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name, 4, 5);

                        try
                        {
                            //keep the directory's full path in the tag for use later
                            node.Tag = dir;

                            //if the directory has sub directories add the place holder
                            if (di.GetDirectories().Count() > 0)
                                node.Nodes.Add(null, "...", 0, 0);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //display a locked folder icon
                            node.ImageIndex = 6;
                            node.SelectedImageIndex = 6;

                            node.ForeColor = false ? SystemColors.ControlText : Color.Gray;
                            node.Tag = false ? null : "X";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "DirectoryLister",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (e.Node.Checked)
                            {
                                node.Checked = true;
                            }
                            e.Node.Nodes.Add(node);                            
                        }
                    }
                }
            }
        }

        // Updates all child tree nodes recursively.
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);

                }
            }
        }

        private void listViewFiles_DoubleClick(object sender, EventArgs e)
        {
            UncolorItems();
        }

        private void buttonCopyRsaKey_Click(object sender, EventArgs e)
        {
            if (textBoxRsaKey.Text.Length > 0)
            {
                Clipboard.SetText(textBoxRsaKey.Text);
                textBoxRsaKey.Focus();
                textBoxRsaKey.SelectAll();
            }
        }

        private void buttonOpenRsaKey_Click(object sender, EventArgs e)
        {
            // Если папки с ключами не было, создаём...
            if (!Directory.Exists(Path.Combine(ExeDirectory, "keys")))
            {
                Directory.CreateDirectory(new Uri(Path.Combine(ExeDirectory, "keys")).LocalPath);
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Для openFileDialog
                openFileDialog.InitialDirectory = new Uri(Path.Combine(ExeDirectory, "keys")).LocalPath;
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "*XML file|*.xml";

                DialogResult result;
                if ((result = openFileDialog.ShowDialog()) == DialogResult.OK)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(openFileDialog.FileName);
                    textBoxRsaKey.Text = doc.InnerXml;
                }
            }
        }

        private void buttonSaveRsaKey_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = new Uri(Path.Combine(ExeDirectory, "keys")).LocalPath;

                saveFileDialog.Filter = "*XML file|*.xml";
                saveFileDialog.FilterIndex = 0;

                saveFileDialog.FileName = "rsa" + DateTime.Now.Ticks.ToString();

                DialogResult result;
                if ((result = saveFileDialog.ShowDialog()) == DialogResult.OK)
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;

                    // Save the document to a file and auto-indent the output.
                    using (XmlWriter writer = XmlWriter.Create(saveFileDialog.FileName, settings))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(textBoxRsaKey.Text);
                        doc.Save(writer);
                    }
                }
            }
        }
    }
}
