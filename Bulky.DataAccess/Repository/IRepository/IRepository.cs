using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        #region
        //IEnumerable<T> GetAll(): Phương thức này trả về một đối tượng
        //IEnumerable(tương tự như một danh sách) chứa các đối tượng kiểu T.
        //Nó được sử dụng để lấy về tất cả các đối tượng của kiểu T từ nguồn dữ liệu.

        #endregion
        IEnumerable<T> GetAll();
        #region
        //T Get(Expression<Func<Task, bool>> fillter):
        //Phương thức này có một tham số là một biểu thức
        //lấy dữ liệu(expression) có kiểu dữ liệu là Func<Task, bool>,
        //và trả về một đối tượng kiểu T. Phương thức này được sử dụng
        //để lấy về một đối tượng kiểu T dựa trên một điều kiện (filter) được chỉ định.
        #endregion
        T Get(Expression<Func<T, bool>> filter);
        #region
        //void Add(T entity): Phương thức này có một tham số
        //là một đối tượng kiểu T, và không trả về giá trị nào.
        //Phương thức này được sử dụng để thêm một đối tượng kiểu T vào nguồn dữ liệu.
        #endregion
        void Add(T entity);
        #region
        //void Remove(T entity): Phương thức này có một tham số
        //là một đối tượng kiểu T, và không trả về giá trị nào.
        //Phương thức này được sử dụng để xóa một đối tượng kiểu T từ nguồn dữ liệu.
        #endregion
        void Remove(T entity);
        #region
        //void RemoveRange(IEnumerable<T> entity): Phương thức
        //này có một tham số là một đối tượng kiểu IEnumerable<T>
        //(tương tự như một danh sách), và không trả về giá trị nào.
        //Phương thức này được sử dụng để xóa một tập hợp các đối
        //tượng kiểu T từ nguồn dữ liệu.
        #endregion
        void RemoveRange(IEnumerable<T> entity);
    }
}
