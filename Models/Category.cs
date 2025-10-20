namespace ActivityManager.Models {
    public class Category {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public List<SaActivity> Activities { get; set; } = new List<SaActivity>();
    }
}
