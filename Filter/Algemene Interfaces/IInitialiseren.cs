using System.Threading.Tasks;

namespace Filter
{
    public interface IInitialiseren
    {
        bool IsGeinitialiseerd { get; }

        public Task Initialiseren();
    }

}