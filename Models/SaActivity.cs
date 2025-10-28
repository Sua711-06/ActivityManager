using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityManager.Models {
    public class SaActivity {
        [Display(Name="Activity ID")]
        public int SaActivityId { get; set; }
        [Display(Name = "Activity Name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Activity Description")]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Activity Location")]
        public string Location { get; set; } = string.Empty;
        [Display(Name = "Activity Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Created On")]
        public DateTime Created { get; set; } = DateTime.Now;

        public string ImageFileName { get; set; } = string.Empty;

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
