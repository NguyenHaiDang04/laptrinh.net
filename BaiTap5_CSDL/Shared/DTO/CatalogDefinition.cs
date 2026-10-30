using System;
using System.Data;

namespace BaiTap5.Shared.DTO
{
    public sealed class Field
    {
        public string Name, Label;
        public Type Type;
        public bool Required;
        public int MaxLength;
        public Field(string name, string label, Type type, bool required = true, int maxLength = 100)
        { Name = name; Label = label; Type = type; Required = required; MaxLength = maxLength; }
    }
    // Chỉ khởi tạo bằng hằng số trong BUS; tên bảng/cột không đến từ người dùng.
    public sealed class CatalogDefinition
    {
        public string Title, Table, Key;
        public bool Identity;
        public Field[] Fields;
        public CatalogDefinition(string title, string table, string key, bool identity, params Field[] fields)
        { Title = title; Table = table; Key = key; Identity = identity; Fields = fields; }
    }
    public interface ICatalogService
    {
        CatalogDefinition Definition { get; }
        DataTable Search(string keyword);
        void Save(object key, object[] values);
        void Delete(object key);
    }
}
