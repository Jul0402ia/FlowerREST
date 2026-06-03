namespace FlowerREST
{
    //Beskriver hvordan en blomst ser ud
    public class Flower
    {
        // unikt id for blomstern
        public int ID { get; set; }
        // Farve, string? betyder at den må godt være null
        public string? Color { get; set; }
        // Bestemmer hvordan objektet vises som tekst
        public override string ToString()
        {
            return $"ID: {ID}, Color: {Color},";
        }
    }
}
