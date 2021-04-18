using BillCenter.WF.Model;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BillCenter.WF
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Объект базы данных
        /// </summary>
        Hus hus;
        /// <summary>
        /// Рассчетная стоимость за один день.
        /// </summary>
        private double Price { get; set; }
        /// <summary>
        /// Начало рассчетного периода
        /// </summary>
        private DateTime Start { get; set; }
        /// <summary>
        /// Конец рассчетного периодна
        /// </summary>
        private DateTime End { get; set; }
        /// <summary>
        /// Количество проживающих
        /// </summary>
        private int Resident { get; set; } = 1;
        /// <summary>
        /// Путь до файла базы данных (по умолчанию корневой каталог программы)
        /// </summary>
        private string path = @"db.xml";
        public Form1()
        {
            InitializeComponent();
            DTPickerEnd.Value = DateTime.Now;
            DTPickerStart.Value = DateTime.Now;
            if(CheckDataBaseFile())
            {
                hus = new Hus();
                hus = hus.Load<Hus>(path);
                InitElements();
            }
            else
            {
                MessageBox.Show("Файл базы данных не найден!", "Без файла db.xml приложение работать не будет.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// Инициализация контролов
        /// </summary>
        private void InitElements()
        {
            foreach (var item in hus.HouseProviderServices)
            {
                comboBox1.Items.Add(item.ProviderName);
            }
            foreach (var item in hus.ClientAccounts)
            {
                comboBox3.Items.Add(item.Id.ToString());
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
        }
        /// <summary>
        /// Обработчик события выбора элемента выпадающего списка поставщиков услуг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            var provider = hus.HouseProviderServices.FirstOrDefault(x => x.ProviderName == selectedState);
            if (provider != null)
            {
                comboBox2.Items.Clear();
                var services = hus.ServicesHousing.Where(x => x.ProviderId == provider.Id).ToList();
                foreach (var item in services)
                {
                    comboBox2.Items.Add(item.ServiceName);
                }
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedItem = comboBox2.Items[0];
                }
            }
        }
        /// <summary>
        /// Обработчик события выбора элемента выпадающего списка перечня услуг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox2.SelectedItem.ToString();
            var service = hus.ServicesHousing.FirstOrDefault(x => x.ServiceName == selectedState);
            if (service != null)
            {
                label4.Text = service.Price.ToString() + " рублей/мес х количество проживающих";
                Price = service.Price;
            }
        }
        /// <summary>
        /// Обработчик события выбора элемента выпадающего списка лицевого счета плательщика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = -1;
            if (int.TryParse(comboBox3.SelectedItem.ToString(), out index))
            {
                var client = hus.ClientAccounts.FirstOrDefault(x => x.Id == index);
                label6.Text = client.LastName + " " + client.FirstName + " " + client.Patronymic + ", проживающих: " + client.NumberResident;
                Resident = client.NumberResident;
            }
        }
        /// <summary>
        /// Обработчик события выбора даты начала рассчетного периода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DTPickerStart_ValueChanged(object sender, EventArgs e)
        {
            Start = DTPickerStart.Value;
        }
        /// <summary>
        /// Обработчик события выбора даты конца рассчетного периода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DTPickerEnd_ValueChanged(object sender, EventArgs e)
        {
            End = DTPickerEnd.Value;
        }
        /// <summary>
        /// Обработчик нажатия на кнопку "Рассчитать начисление"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (End == null || Start == null)
            {
                return;
            }
            var period = (int)(End - Start).TotalDays;
            label7.Text = "К оплате: " + GetPricePerPeriod(period, Resident).ToString() + " рублей за период - " + period + " дней";
        }
        /// <summary>
        /// Рассчитывает стоимость за переданный период времени на количество проживающих
        /// </summary>
        /// <param name="period">Рассчетный период</param>
        /// <param name="person">Количество проживающих</param>
        /// <returns>Возвращает стоимость в зависимости от периода и количества проживающих</returns>
        private double GetPricePerPeriod(int period, int person)
        {
            var PricePerDay = Price / 30;
            return Math.Round((PricePerDay * period) * person, 2);
        }
        /// <summary>
        /// Проверяет наличие файла базы данных
        /// </summary>
        /// <returns>Возвращает true, если файл базы данных доступен и false если файл базы данных не найден</returns>
        private bool CheckDataBaseFile()
        {
            if(File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
