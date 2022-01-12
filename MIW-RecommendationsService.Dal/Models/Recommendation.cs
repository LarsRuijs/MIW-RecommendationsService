namespace MIW_RecommendationsService.Dal.Models
{
    public class Recommendation
    {
        public Product Product { get; set; }
        public int Priority { get; set; }
    }
}