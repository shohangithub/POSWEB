using GQLAttribute;

namespace POSWEB.Server
{
    [GraphQLEntity]
    public class TestingModel
    {
        public int MyProperty { get; set; }
    }

    [GraphQLEntity]
    public class TestingModel2
    {
        public int MyProperty { get; set; }
    }
}
