namespace SimpleLogger
{
    public interface ILogger
    {
        void Information(string comment);

        void Error(string comment);
        void ErrorSupplement(string comment);

        void Warning(string comment);
        void InformationSuccess(string comment);
    }
}