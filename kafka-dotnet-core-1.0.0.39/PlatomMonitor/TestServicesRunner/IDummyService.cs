

namespace TestServicesRunner
{
    public interface IDummyService
    {
        bool Enabled { get; set; }

        void Setup();
        void Run();
        void Stop();
        void WaiteForTermination();
    }
}