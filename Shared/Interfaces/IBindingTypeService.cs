using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IBindingTypeService
{
    Task<List<BindingType>> GetAllAsync();
    Task<BindingType?> GetAsyncById(int id);
    Task<BindingType> Add(BindingType bindingType);
    Task Delete(int id);
    Task Update(BindingType bindingType);
}