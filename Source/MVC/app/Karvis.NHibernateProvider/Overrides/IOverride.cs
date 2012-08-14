using NHibernate.Mapping.ByCode;

namespace Karvis.NHibernateProvider.Overrides
{
    internal interface IOverride
    {
        void Override(ModelMapper mapper);
    }
}
