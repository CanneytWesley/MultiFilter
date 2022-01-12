using System.Threading.Tasks;

namespace Filter
{
    public interface IInitialise
    {
        bool IsInitialised { get; }

        public Task Initialise();
    }

}