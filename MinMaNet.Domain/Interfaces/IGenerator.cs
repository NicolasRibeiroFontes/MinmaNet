using MinMaNet.Domain.Models;

namespace MinMaNet.Domain.Interfaces
{
    public interface IGenerator
    {
        string Generate(Project project);
    }
}
