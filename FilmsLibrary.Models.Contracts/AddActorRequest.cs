using FilmsLibrary.Models.Contracts.Models;

namespace FilmsLibrary.Models.Contracts
{
    public class AddActorRequest
    {
        public const string Route = "/actor";

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }
    }
}