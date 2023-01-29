using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    [HttpGet]                                 //obtener la lista de pizzas.
public ActionResult<List<Pizza>> GetAll() =>  //Devuelve una instancia ActionResult de tipo List<Pizza>. El tipo ActionResult es la clase base para todos los resultados de acción en ASP.NET Core.
    PizzaService.GetAll();                    //Consulta el servicio en busca de todas las pizzas y devuelve automáticamente los datos cuyo Content-Type es application/json.

    [HttpGet("{id}")]                         //se obtiene una sola pizza especificada por el cliente.
public ActionResult<Pizza> Get(int id)        //Consulta la base de datos en busca de una pizza que coincida con el parámetro id proporcionado.
{
    var pizza = PizzaService.Get(id);

    if(pizza == null)
        return NotFound();

    return pizza;
}

    [HttpPost]
public IActionResult Create(Pizza pizza)                                  //se crea una pizza
{            
    PizzaService.Add(pizza);
    return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);  //Se usa la palabra clave nameof para evitar codificar de forma rígida el nombre de la acción.
}

    [HttpPut("{id}")]
public IActionResult Update(int id, Pizza pizza)    // se actualiza la pizza 
{
    if (id != pizza.Id)
        return BadRequest();
           
    var existingPizza = PizzaService.Get(id);
    if(existingPizza is null)
        return NotFound();
   
    PizzaService.Update(pizza);           
   
    return NoContent();
}

    [HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    var pizza = PizzaService.Get(id);                //eliminacion de una pizza
   
    if (pizza is null)
        return NotFound();
       
    PizzaService.Delete(id);
   
    return NoContent();
}
}