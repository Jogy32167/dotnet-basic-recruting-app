namespace MatchDataManager.Api.Repositories
{
    public class ResultStatus
    {
        public ResultStatus(int status, string statusMessage)
        {
            Status = status;
            StatusMessage = statusMessage;
        }

        public int Status { get; }
        public string StatusMessage { get;}
    }
}
