using SqlSugar;
using Api.Model;

namespace Api.Service
{
    public class TestService : BaseService<TestModel>
    {
        readonly ISqlSugarClient client;

        public TestService(ISqlSugarClient _client) : base(_client)
        {
            client = _client;
        }

        public IEnumerable<TestModel> GetTestModels()
        {
            return client.Queryable<TestModel>().ToList();
        }

        public TestModel GetTestModel(string id)
        {
            var user = client.Queryable<TestModel>().Where(x => x.Id == id).First();
            if (user is null)
                throw new Exception("未找到！");
            return user;
        }
    }
}
