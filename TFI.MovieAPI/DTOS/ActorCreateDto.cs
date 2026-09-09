using System;
using System.ComponentModel.DataAnnotations;

namespace TFI.MovieAPI.DTOS
{
    public class ActorCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}


