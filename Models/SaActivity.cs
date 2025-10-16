namespace ActivityManager.Models {
    public class SaActivity {
        public int SaActivityId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
