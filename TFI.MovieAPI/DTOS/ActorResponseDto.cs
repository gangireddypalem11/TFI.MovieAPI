using System;
using System.ComponentModel.DataAnnotations;
namespace TFI.MovieAPI.DTOS
{
    public class ActorResponseDto
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
      
        public string Industry { get; set; }
    
        public DateTime DateOfBirth { get; set; }

    }
}
