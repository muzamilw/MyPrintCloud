namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Machine Web Api Model
    /// </summary>
    public class Machine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int? MachineCatId { get; set; }
        public int DefaultPaperId { get; set; }
        public double? Maximumsheetweight { get; set; }
        public double? Maximumsheetheight { get; set; }
        public double? Maximumsheetwidth { get; set; }
        public double? Minimumsheetheight { get; set; }
        public double? Minimumsheetwidth { get; set; }
    }
}
