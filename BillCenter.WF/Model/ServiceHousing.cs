namespace BillCenter.WF.Model
{
    public class ServiceHousing
    {
        /// <summary>
        /// Идентификатор услуги
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор поставщика услуги
        /// </summary>
        public int ProviderId { get; set; }
        /// <summary>
        /// Название услуги
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Описание услуги
        /// </summary>
        public string ServiceDescription { get; set; }
        /// <summary>
        /// Стоимость за единицу потребления в месяц (30 дней)
        /// </summary>
        public double Price { get; set; }
    }
}
