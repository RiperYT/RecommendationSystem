namespace Recomendations.Services.Abstractions
{
    public interface IPostRecomendationsService
    {
        public List<int> GetLimitRecomendation(int user_id, int limit);
    }
}
