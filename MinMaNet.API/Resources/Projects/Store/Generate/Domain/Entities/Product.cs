using System;

namespace Store.Entities
{
public class Product 
{
public string Name { get; set; }
public decimal Price { get; set; }
public string Description { get; set; }
public int CategoryId { get; set; }
}
}