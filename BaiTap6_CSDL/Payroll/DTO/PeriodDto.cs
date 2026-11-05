using System;
using System.Globalization;
namespace Payroll.DTO
{
 public sealed class PeriodDto
 {
  public int Month {get;private set;} public int Year {get;private set;}
  public string Key {get{return Month.ToString("00")+"/"+Year;}}
  public int Days {get{return DateTime.DaysInMonth(Year,Month);}}
  public PeriodDto(int month,int year){if(month<1||month>12||year<2000||year>2100)throw new ArgumentException("Tháng/năm không hợp lệ.");Month=month;Year=year;}
 }
}
