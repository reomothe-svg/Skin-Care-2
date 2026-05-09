namespace SkinCare
{
    public interface ISkinAnalyzer
    {
        string AnalyzeSkin(int[] answers); // ترجع نوع البشرة 
        Product[] GetRecommendedProducts(string skinType); // المنتجات حسب نوع البشرة 
    }
}