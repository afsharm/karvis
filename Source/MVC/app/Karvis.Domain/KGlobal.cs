using SharpLite.Domain;

namespace Karvis.Core
{
    public class KGlobal : Entity
    {
        public virtual string Key { set; get; }
        public virtual string Value { set; get; }
    }
}