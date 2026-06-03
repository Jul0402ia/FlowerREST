namespace FlowerREST
{

    // 
    public class RepositoryFlowers
    {
        // private:Kun denne klasse må bruge listen direkte.Andre klasser kan ikke skrive m_flower.Add() eller m_flower.Clear()
        // List<Flower>: En liste der kun kan indeholde Flower-objekter
        // m_:Member variable (field) - variabel der tilhører hele klassen
        // new List<Flower>(): Opretter en tom liste i hukommelsen
        private List<Flower> m_flower = new List<Flower>();

        // static: Der findes kun én nextID som deles mellem alle RepositoryFlowers objekter Starter ved 1 så første blomst får ID = 1
        private static int nextID = 1;

        // Constructor Kører automatisk når RepositoryFlowers oprettes. Laver ikke noget endnu, men gør det muligt at oprette objektet
        public RepositoryFlowers()
        {

        }

        // READ: Henter alle blomster en kopi af listen
        public IEnumerable<Flower> GetAll()
        {
            // Opretter en ny liste baseret på den originale liste
            List<Flower> flowersCopy =
                new List<Flower>(m_flower);

            // Returnerer kopien. Beskytter den originale liste mod fx Clear(), Remove(), Add()
            return flowersCopy;
        }



        // READ: Henter én blomst ud fra ID
        public Flower? GetByID(int id)
        {
            // Finder første blomst med samme ID
            Flower? flower =
                m_flower.FirstOrDefault
                (f => f.ID == id);


            // Hvis ingen blomst findes returneres null
            if (flower == null)
            {
                return null;
            }

            // Opretter nyt objekt (kopi). Så andre ikke får adgang til originalobjektet
            Flower flowerCopy = new Flower
            {
                ID = flower.ID,
                Color = flower.Color,
            };

            return flowerCopy;
        }

        // CREATE
        // Tilføjer ny blomst
        public Flower Add(Flower flower)
        {
            // Giver automatisk nyt ID
            flower.ID = nextID++;


            // Tilføjer til repository-listen
            m_flower.Add(flower);


            // Opretter kopi
            Flower flowerCopy = new Flower
            {
                ID = flower.ID,
                Color = flower.Color,  
            };
            return flowerCopy;
        }
        public Flower? Delete(int id)
        {
            Flower? flower = m_flower.FirstOrDefault(f => f.ID == id);
            if (flower == null)
            {
                return null; 
            }
            m_flower.Remove(flower);
            Flower flowerCopy = new Flower
            {
                ID = flower.ID,
                Color = flower.Color
            };
            return flowerCopy;
        }
        public Flower? Update(int id, Flower updatedFlower)
        {
            Flower? existingFlower = m_flower.FirstOrDefault(f => f.ID == id);
            if (existingFlower == null)
            {
                return null; 
            }
            existingFlower.Color = updatedFlower.Color;

            Flower flowerCopy = new Flower
            {
                ID = existingFlower.ID,
                Color = existingFlower.Color
            };
            return flowerCopy;
        }

    }
}