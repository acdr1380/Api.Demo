using Api.Model.SysManagement;
using Api.IService.SysManagement;
using SqlSugar;
using Api.IRepository;
using Api.IRepository.SysManagement;
using Api.Repository.SysManagement;
using Api.Common;
using Microsoft.Extensions.Logging;

namespace Api.Service.SysManagement
{
    [AutoInject(typeof(ISysUserService), InjectType.Scope)]
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        public ISysUserRepository _repository;
        public SysUserService(ISysUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<SysUser> GetListPage(string key, int pageIndex, int pageSize, ref int total)
        {
            return _repository.GetListPage(key, pageIndex, pageSize, ref total);
        }

        public async Task<SysUser> Login(string UserAccount, string PassWord)
        {
            return await _repository.Login(UserAccount, PassWord);
        }
    }
}
