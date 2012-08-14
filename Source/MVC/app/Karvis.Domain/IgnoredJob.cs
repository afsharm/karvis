using SharpLite.Domain;

namespace Karvis.Core
{
    public class IgnoredJob : Entity
    {
        public virtual string Url { set; get; }
        public virtual AdSource AdSource { set; get; }
    }
}