using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FlowerREST.Controllers
{
    //bestemmer URL'en til controlleren (hvis klassen hedder FlowersController bliver controlleren til "flowers" api/flowers)
    [Route("api/[controller]")]
    [ApiController]

    //opretter controllerklassen, ControllerBasen bruges til at arve Ok(), NotFound(), BadRequest()
    public class FlowersController : ControllerBase
    {
        //opretter private field, som kan kun sættes i constructoren, som kun kan bruges her
        // RepositoryFlowers er datatypen og _repo er variablen som gemmer repoet
        private readonly RepositoryFlowers _repo;

        //constructoren, asp.net giver automatisk et repositoryFlowers objekt ind her via dependency injection
        public FlowersController(RepositoryFlowers repo)
        {
            // gemmer det repo, asp.net gav i variablen _repo, så controllerens metoder kan bruge det
            _repo = repo;
        }

        // GET: api/flowers
        [HttpGet] //metode reagerer på htttp get
        // fordi metoden hedder GetAll(), retunerer den et http-resultat med en samling af Flower
        public ActionResult<IEnumerable<Flower>> GetAll()
        {
            //kalder _repo.GetAll() og sender 200ok mwd json data
            return Ok(_repo.GetAll());
        }

        // GET: api/flowers/1 - {id} betyder at id kommer fra url
        [HttpGet("{id}")]
        //metoden tager et id som parameter
        public ActionResult<Flower> GetByID(int id)
        {
            //prøver at finde blomsten, husk ?
            Flower? flower = _repo.GetByID(id);

            if (flower == null)
            {
                return NotFound();
            }

            return Ok(flower);
        }

        [Authorize]
        // POST: api/flowers
        [HttpPost] // reagerer på POST api/flowers - bruger til at oprette en blomst
        //metoden modtager en Flower, den kommer fra request body som jason. asp.net laver jason om til Flower objekt i c#
        public ActionResult<Flower> Add(Flower flower)
        {
            //kalder repoet og gemmer blomsten, repo given den også id.
            Flower createdFlower = _repo.Add(flower);

            return Created(
               $"api/flowers/{createdFlower.ID}", createdFlower);

            //return CreatedAtAction(
            //    //peger på metoden der kan hente den nye blomst igen
            //    nameof(GetByID),
            //    //fortæller hvilket id den nye blomst har 
            //    new { id = createdFlower.ID },
            //    //selve objektet der sendes tilbage som json
            //    createdFlower
            //    //retunerer 201, som betyder created 
            //);
        }
        [HttpDelete("{id}")]
        public ActionResult<Flower> Delete(int id)
        {
            Flower? flower = _repo.Delete(id); 
            if (flower == null)
            {
                return NotFound();
            }
            return Ok(flower);
        }

        [HttpPut("{id}")]
        public ActionResult<Flower> Update(int id, Flower updatedFlower)
        {
            Flower? existingFlower = _repo.Update(id, updatedFlower); 
            if (existingFlower == null)
            {
                return NotFound(); 
            }
            return Ok(existingFlower); 
        }
     
    }
}