using System.Diagnostics.CodeAnalysis;


namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class UpdateAwardDto
    {

        public string Title { get; set; }
        public string Company { get; set; }
        public int Year { get; set; }
               
    }
}