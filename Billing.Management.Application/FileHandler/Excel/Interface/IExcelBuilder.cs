
namespace Billing.Management.Application.FileHandler.Excel.Interface
{
    public interface IExcelBuilder<T>
    {
        byte[] CreateFile(IList<T> entities);
    }
}
