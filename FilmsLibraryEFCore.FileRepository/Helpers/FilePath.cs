namespace FilmsLibraryEFCore.FileRepository.Helpers
{
    public class FilePath
    {
        public FilePath(string moviePath, string actorPath, string actorMoviePath)
        {
            MoviePath = moviePath;
            ActorPath = actorPath;
            ActorMoviePath = actorMoviePath;
        }

        public string MoviePath { get; }

        public string ActorPath { get; }

        public string ActorMoviePath { get; }
    }
}
