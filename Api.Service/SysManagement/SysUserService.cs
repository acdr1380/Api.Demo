using Api.Model.SysManagement;
using Api.IService.SysManagement;
using SqlSugar;
using Api.IRepository;
using Api.IRepository.SysManagement;
using Api.Repository.SysManagement;
using Api.Common;

namespace Api.Service.SysManagement
{
    [AutoInject(typeof(ISysUserService), InjectType.Scope)]
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        public ISysUserRepository repository;
        public SysUserService(ISysUserRepository _repository):base(_repository)
        {
            repository = _repository;
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
