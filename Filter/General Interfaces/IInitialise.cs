using System.Threading.Tasks;

namespace Filter.General_Interfaces
{
    public interface IInitialise
    {
        bool IsInitialised { get; }

        public Task Initialise();
    }

}