namespace BillCenter.WF.Model
{
    public class ClientAccount
    {
        /// <summary>
        /// Номер лицевого счета плательщика
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя плательщика
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия плательщика
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество плательщика
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Количество проживающих на жилой площади плательщика
        /// </summary>
        public int NumberResident { get; set; }
    }
}
