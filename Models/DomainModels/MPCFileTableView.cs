namespace MPC.Models.DomainModels
{
    /// <summary>
    /// MPC FileTable View
    /// </summary>
    public class MpcFileTableView
    {
        public System.Guid StreamId { get; set; }
        public byte[] FileStream { get; set; }
        public string Name { get; set; }
        public bool IsDirectory { get; set; }
        public string UncPath { get; set; }
    }
}
