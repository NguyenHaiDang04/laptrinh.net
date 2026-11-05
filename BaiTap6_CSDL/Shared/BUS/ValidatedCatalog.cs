using System;
using System.Data;
using BaiTap6.Shared.DTO;
using BaiTap6.Shared.DAL;
namespace BaiTap6.Shared.BUS
{
 public sealed class ValidatedCatalog:ICatalogService
 {
  private readonly CatalogBus inner; private readonly Action<object[]> validate;
  public CatalogDefinition Definition {get{return inner.Definition;}}
  public ValidatedCatalog(Db db,CatalogDefinition def,Action<object[]> validate){inner=new CatalogBus(db,def);this.validate=validate;}
  public DataTable Search(string q){return inner.Search(q);}
  public void Save(object key,object[] values){validate(values);inner.Save(key,values);}
  public void Delete(object key){inner.Delete(key);}
 }
}
