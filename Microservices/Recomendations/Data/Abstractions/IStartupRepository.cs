namespace Recomendations.Data.Abstractions
{
    public interface IStartupRepository
    {
        public List<int> GetStartupIDsRecomandationByUserID(int user_id, int limit);
        public List<int> GetAllUserIDsRecomandationByStartupID(int startup_id, int limit);
        public List<int> GeUserIDsRecomandationByStartupIDSpecialIDs(int startup_id, int specialistID, int limit);
    }
}
