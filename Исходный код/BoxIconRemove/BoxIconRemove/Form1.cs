using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//мои юзынги 


using System.IO; // show dialog показать диалоговое окно сохранения

using Microsoft.Win32; //для работы с реестром

using System.Diagnostics; //для перезапуска проводника


namespace BoxIconRemove
{
    public partial class Form1 : Form
    {


        //переменные которые содержат пустой путь  (нужно для указания пути до картинки)

        string output = System.IO.Path.Combine(Application.StartupPath, "");



        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            //Смена значка
            label2.Text = "";
            label2.ForeColor = Color.Lime;

            //перезапуск проводника
            label4.Text = "";
            label4.ForeColor = Color.Yellow;

            //вернуть по умолчанию
            label5.Text = "";
            label5.ForeColor = Color.Orange;

        }
        //выбираем нужную картинку
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog PathAudioOpen = new OpenFileDialog();
            PathAudioOpen.Filter = "Иконка | * .ico";
            if (PathAudioOpen.ShowDialog() == DialogResult.OK)
            {
                output = PathAudioOpen.FileName;
                textBox1.Text = output.ToString();
            }
        }

        //поменять иконку
        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons"); //создать раздел Shell Icons
            key.SetValue("29", $"{output}"); //создать параметр 29 и дать ему значение выбранную картинку то есть вернуть путь до картинки
            key.Close();  // закрыть ветку то есть раздел   

            label2.Text = "Изменения приняты";
            label4.Text = "Нужно обновить чтобы увидеть :) ";
        }

        //вернуть иконку
        private void button2_Click(object sender, EventArgs e)
        {

            //Обычная проверка нет раздела с кастомными иконками то появится сообщение что они стандартные , а если раздел есть то он его удалит и предлжит обновить проводник

            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons") == null) //если путь не существует
            {
                MessageBox.Show("Они стандартные уже");
            }
            else
            {
                Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons"); //удалить раздел shell icons и все вложенные в него подразделы , параметры и значения (вернет ярлыки со стрелками по умолчанию)
                label4.Text = "Нужно обновить чтобы увидеть :) ";
                label5.Text = "Ты вернул стандартные значки (стрелки)";
            }
            
        }


        //перезапустить проводник чтобы изменения в реестре вступили в силу.
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    if (p.MainModule.FileName.ToLower().EndsWith(":\\windows\\explorer.exe"))
                    {
                        p.Kill();
                        break;
                    }
                }
                catch
                { }
            }
            label2.Text = "";
            label4.Text = "";
            label5.Text = "";
        }

        //ссылка на гитхаб
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/spbkit1337");
        }







    }
}
