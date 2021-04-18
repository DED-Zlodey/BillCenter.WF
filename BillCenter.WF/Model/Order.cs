using System;

namespace BillCenter.WF.Model
{
    public class Order
    {
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор плательщика
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Идентификатор оплаченной услуги
        /// </summary>
        public int ServiceId { get; set; }
        /// <summary>
        /// Дата начала рассчетного периодна
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Дата конца рассчетного периода
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
