namespace ActivityManager.Models {
    public class Category {
        public int CategoryId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public struct Color { 
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }
        }
        public Color CategoryColor { get; set; } = new Color { R = 0, G = 0, B = 0 };

        public List<SaActivity> Activities { get; set; } = new List<SaActivity>();
    }
}
