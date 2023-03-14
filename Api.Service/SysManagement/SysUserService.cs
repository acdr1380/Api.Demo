using Api.Model.SysManagement;
using Api.IService.SysManagement;
using SqlSugar;
using Api.IRepository;
using Api.IRepository.SysManagement;
using Api.Repository.SysManagement;

namespace Api.Service.SysManagement
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        public new ISysUserRepository repository;
        public SysUserService(ISqlSugarClient _client) : base(_client)
        {
            repository = new SysUserRepository(_client);
        }

        public IEnumerable<SysUser> GetListPage(string key, int pageIndex, int pageSize, ref int total)
        {
            return repository.GetListPage(key, pageIndex, pageSize, ref total);
        }

        public async Task<SysUser> Login(string UserAccount, string PassWord)
        {
            return await repository.Login(UserAccount, PassWord);
        }
    }
}
