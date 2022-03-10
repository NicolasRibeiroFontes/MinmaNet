using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Interfaces;
using Store.Entities;

namespace Store.Controllers
{
[Route("api/[controller]"), ApiController]
public class CategoryController : ControllerBase
{
private readonly ICategoryRepository repository;

public CategoryController(ICategoryRepository repository)
{
this.repository = repository;
}

[HttpPost]
public IActionResult Post(Category model)
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
public IActionResult Put(Category model)
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
