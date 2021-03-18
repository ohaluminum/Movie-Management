using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagement.Models.DataModels
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]       // Display date only (not time)
        public DateTime ReleaseDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Genre { get; set; }

        [Required]
        [DataType(DataType.Currency)]       // Display in currency format
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Rating { get; set; }

    }
}