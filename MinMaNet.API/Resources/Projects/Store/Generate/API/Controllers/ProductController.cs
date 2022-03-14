using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Interfaces;
using Store.Entities;

namespace Store.Controllers
{
[Route("api/[controller]"), ApiController]
public class ProductController : ControllerBase
{
private readonly IProductRepository repository;

public ProductController(IProductRepository repository)
{
this.repository = repository;
}

[HttpPost]
public IActionResult Post(Product model)
{
repository.Post(model);
return Ok(model);
}

[HttpGet]
public IActionResult Get()
{
return Ok(repository.Get());
}

[HttpGet("{id}")]
public IActionResult GetById(int id)
{
return Ok(repository.Get(id));
}

[HttpPut]
public IActionResult Put(Product model)
{
repository.Put(model);
return Ok(model);
}

[HttpDelete]
public IActionResult Delete(int id)
{
return Ok(repository.Delete(id));
}
}
}