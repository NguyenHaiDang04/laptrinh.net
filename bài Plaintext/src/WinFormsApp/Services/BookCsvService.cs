using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using WinFormsApp.Models;

namespace WinFormsApp.Services
{
    public static class BookCsvService
    {
        private static readonly string[] Header = { "BookId", "Title", "Author", "UnitPrice", "Quantity", "CoverPath" };
        public static List<BookModel> Read(string path)
        {
            var result = new List<BookModel>(); var ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var parser = new TextFieldParser(path, Encoding.UTF8))
            {
                parser.TextFieldType = FieldType.Delimited; parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true; parser.TrimWhiteSpace = false;
                if (parser.EndOfData || !parser.ReadFields().SequenceEqual(Header)) throw new FormatException("CSV phải có tiêu đề: " + string.Join(",", Header));
                while (!parser.EndOfData)
                {
                    long line = parser.LineNumber; string[] f = parser.ReadFields();
                    decimal price; int qty;
                    if (f == null || f.Length != 6 || string.IsNullOrWhiteSpace(f[0]) || string.IsNullOrWhiteSpace(f[1]) || string.IsNullOrWhiteSpace(f[2]) ||
                        !decimal.TryParse(f[3], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price) || price <= 0 || price > 1000000000M ||
                        !int.TryParse(f[4], NumberStyles.None, CultureInfo.InvariantCulture, out qty) || qty < 0 || qty > 1000000 || !ids.Add(f[0].Trim()))
                        throw new FormatException("Dữ liệu không hợp lệ hoặc mã trùng tại dòng " + line + ".");
                    string cover = string.IsNullOrWhiteSpace(f[5]) ? "" : Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetFullPath(path)), f[5]));
                    result.Add(new BookModel { BookId = f[0].Trim(), Title = f[1].Trim(), Author = f[2].Trim(), UnitPrice = price, Quantity = qty, CoverPath = cover });
                }
            }
            return result;
        }
        public static void Write(string path, IEnumerable<BookModel> books)
        {
            string directory = Path.GetDirectoryName(Path.GetFullPath(path));
            // Viết file tạm cùng thư mục; chỉ thay file đích sau khi ghi thành công.
            string temporary = Path.Combine(directory, ".books-" + Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                using (var writer = new StreamWriter(temporary, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine(string.Join(",", Header));
                    foreach (var b in books)
                    {
                        string cover = "";
                        if (!string.IsNullOrEmpty(b.CoverPath))
                        {
                            var root = new Uri(directory.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar);
                            var relative = root.MakeRelativeUri(new Uri(Path.GetFullPath(b.CoverPath)));
                            cover = relative.IsAbsoluteUri ? b.CoverPath : Uri.UnescapeDataString(relative.ToString());
                        }
                        writer.WriteLine(string.Join(",", new[] { b.BookId, b.Title, b.Author, b.UnitPrice.ToString(CultureInfo.InvariantCulture), b.Quantity.ToString(CultureInfo.InvariantCulture), cover }.Select(Quote)));
                    }
                }
                if (File.Exists(path)) File.Replace(temporary, path, null); else File.Move(temporary, path);
            }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
        }
        private static string Quote(string text) => "\"" + (text ?? "").Replace("\"", "\"\"") + "\"";
    }
}
