namespace FlowerREST
{
    public class RepositoryFlowers
    {
        private readonly FlowerDbContext _context;

        public RepositoryFlowers(FlowerDbContext context)
        {
            _context = context;
        }

        // READ: Henter alle blomster fra databasen
        public IEnumerable<Flower> GetAll()
        {
            return _context.Flowers.ToList();
        }

        // READ: Henter én blomst ud fra ID
        public Flower? GetByID(int id)
        {
            return _context.Flowers.FirstOrDefault(f => f.ID == id);
        }

        // CREATE: Tilføjer ny blomst til databasen
        public Flower Add(Flower flower)
        {
            // ID skal IKKE sættes manuelt mere.
            // Databasen giver automatisk ID via IDENTITY.
            _context.Flowers.Add(flower);

            _context.SaveChanges();

            return flower;
        }

        // DELETE: Sletter blomst fra databasen
        public Flower? Delete(int id)
        {
            Flower? flower =
                _context.Flowers.FirstOrDefault(f => f.ID == id);

            if (flower == null)
            {
                return null;
            }

            _context.Flowers.Remove(flower);

            _context.SaveChanges();

            return flower;
        }

        // UPDATE: Opdaterer blomst i databasen
        public Flower? Update(int id, Flower updatedFlower)
        {
            Flower? existingFlower =
                _context.Flowers.FirstOrDefault(f => f.ID == id);

            if (existingFlower == null)
            {
                return null;
            }

            existingFlower.Color = updatedFlower.Color;

            _context.SaveChanges();

            return existingFlower;
        }
    }
}