using SharpLite.Domain;

namespace Karvis.Domain
{
    public class IgnoredJob : Entity
    {
        public virtual string Url { set; get; }
        public virtual AdSource AdSource { set; get; }
    }
}