using System.Reflection;
using System.Reflection.Metadata;

namespace Application
{
    public static class AssembleReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
