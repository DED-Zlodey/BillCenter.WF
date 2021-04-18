using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BillCenter.WF.Model
{
    [Serializable]
    [XmlRoot(ElementName = "Hus", Namespace = "")]
    public class Hus
    {
        /// <summary>
        /// Список клиентов
        /// </summary>
        [XmlArray("ClientAccounts")]
        public List<ClientAccount> ClientAccounts { get; set; }
        /// <summary>
        /// Список поставщиков услуг
        /// </summary>
        [XmlArray("HouseProviderServices")]
        public List<HouseProviderService> HouseProviderServices { get; set; }
        /// <summary>
        /// Список оплаченных услуг
        /// </summary>
        [XmlArray("Orders")]
        public List<Order> Orders { get; set; }
        /// <summary>
        /// Список услуг
        /// </summary>
        [XmlArray("ServicesHousing")]
        public List<ServiceHousing> ServicesHousing { get; set; }
        /// <summary>
        /// Десериализация данных из файла базы данных в формате xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">Путь до файла базы данных</param>
        /// <returns>Возвращает объект базы данных</returns>
        public T Load<T>(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(fileStream);
            }
        }
    }
}
