using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagement.Models.DataModels
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(60, ErrorMessage = "Title field length limit is 60 characters.")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]       // Display date only (not time)
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Genre field length limit is 30 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]   // 1. The first letter is required to be uppercase. 2. Letters only: white space, numbers, and special characters are not allowed.
        public string Genre { get; set; }

        [Required]
        [DataType(DataType.Currency)]       // Display in currency format
        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, 100)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(5, ErrorMessage = "Rating field length limit is 5 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]      // 1. The first letter is required to be uppercase. 2. Allows some special characters (double quotation mark["], single quotation mark['], whitespace[ ], minus sign[-]) and numbers in subsequent spaces.
        public string Rating { get; set; }
    }
}