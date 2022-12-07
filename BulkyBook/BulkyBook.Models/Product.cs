﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models;
public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    [Range(1, 100000)]
    [Display(Name = "List Price")]
    public double ListPrice { get; set; }
    [Required]
    [Range(1, 100000)]
    [Display(Name = "Price for 1-50")]
    public double Price { get; set; }
    [Required]
    [Range(1, 100000)]
    [Display(Name = "Price for 50-100")]
    public double Price50 { get; set; }
    [Required]
    [Range(1, 100000)]
    [Display(Name = "Price for 100+")]
    public double Price100 { get; set; }
    [ValidateNever]
    public string ImageUrl { get; set; }
    [Display(Name = "Category")]
    [Required]
    public int CategoryId { get; set; }
    [ValidateNever]
    public Category Category { get; set; }
    [Display(Name = "Cover Type")]
    [Required]
    public int CoverTypeId { get; set; }
    [ValidateNever]
    public CoverType CoverType { get; set; }
}
