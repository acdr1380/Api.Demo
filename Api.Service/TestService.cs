using SqlSugar;
using Api.Model;

namespace Api.Service
{
    public class TestService
    {
        readonly ISqlSugarClient client;
        public TestService(ISqlSugarClient _client)
        {
            client = _client;
        }

        public TestModel GetUser(string id)
        {
            var user =  client.Queryable<TestModel>().Where(x => x.Id == id).First();
            if (user is null)
                throw new Exception("未找到！");
            return user;
        }
    }
}
