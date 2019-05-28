namespace DataObjects
{
    public class ProcessInfo
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public float MemoryStatistic { get; set; }
        public float CPUStatistic { get; set; }
        public string FileLocation { get; set; }        
    }
}
