namespace SkinCare
{
    public class User
    {
        private string name;
        private int age;
        private int[] quizAnswers;
        private SkinType detectedSkinType;

        public User(string name, int age)
        {
            this.name = name;
            this.age = age;
            this.quizAnswers = new int[8]; // حفظ اجابات المستخدم ال 8  

        }

        public string GetName() { return name; }
        public int GetAge() { return age; }
        public int[] GetQuizAnswers() { return quizAnswers; }
        public SkinType GetDetectedSkinType() { return detectedSkinType; }

        public void SetName(string name) { this.name = name; }
        public void SetAge(int age) { this.age = age; }
        public void SetDetectedSkinType(SkinType skinType) { this.detectedSkinType = skinType; }

        public void SetAnswer(int questionIndex, int answer)
        {
            if (questionIndex >= 0 && questionIndex < quizAnswers.Length) // تاكد من رقم السؤال صالح و ليس اكبر من 8
                quizAnswers[questionIndex] = answer;  // حفظ الاجابة في مكانها الصحيح
        }
    }

    public class SkinAnalyzer : ISkinAnalyzer // انترفيس ISkinAnalyzer  لتحديد نوع البشرة و المنتجات المناسبة
    {
        public string AnalyzeSkin(int[] answers)
        {
            int score = 0;
            for (int i = 0; i < answers.Length; i++) 
                score += answers[i]; // تجمع نقاط الاجابات حسب الفرع المختار للاجابة 
            if (answers[2] >= 3 && answers[5] >= 3)
                return "Sensitive";
            else if (score >= 24)
                return "Oily";
            else if (score <= 12)
                return "Dry";
            else if (answers[0] >= 3 && answers[1] >= 2)
                return "Combination";
            else
                return "Normal";
        }

        public Product[] GetRecommendedProducts(string skinType)
        {
            SkinType skin = CreateSkinType(skinType);
            return skin.GetRecommendedProducts();
        }

        public SkinType CreateSkinType(string skinType) 
        {
            if (skinType == "Oily") return new OilySkin();
            else if (skinType == "Dry") return new DrySkin();
            else if (skinType == "Sensitive") return new SensitiveSkin();
            else if (skinType == "Combination") return new CombinationSkin();
            else return new NormalSkin();
        }
    }
}