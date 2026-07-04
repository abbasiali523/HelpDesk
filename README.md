# سامانه تیکت پشتیبانی (HelpDesk)

یک برنامه ASP.NET Core MVC برای ثبت‌نام کاربران و ثبت تیکت‌های هلپ‌دسک.

## امکانات
- ثبت‌نام و ورود کاربران (ASP.NET Core Identity)
- ثبت تیکت پشتیبانی توسط کاربر عادی
- مشاهده‌ی تیکت‌های خودش توسط هر کاربر
- مشاهده‌ی همه‌ی تیکت‌ها و تغییر وضعیت آن‌ها توسط ادمین
- کاربر پیش‌فرض ادمین که هنگام اجرای اول برنامه به‌صورت خودکار ساخته می‌شود:
  - نام کاربری: `admin`
  - رمز عبور: `admin`
- پایگاه داده SQLite (فایل `helpdesk.db` در پوشه پروژه ساخته می‌شود)

## پیش‌نیازها
- نصب [.NET 8 SDK](https://dotnet.microsoft.com/download)

## اجرای پروژه
در ترمینال داخل پوشه پروژه دستورات زیر را اجرا کنید:

```bash
dotnet restore
dotnet run
```

سپس در مرورگر آدرسی که در ترمینال چاپ می‌شود (معمولاً `https://localhost:5001` یا مشابه) را باز کنید.

با اولین اجرای برنامه، دیتابیس و نقش‌ها (Admin/User) و کاربر ادمین پیش‌فرض به‌صورت خودکار ساخته می‌شوند.

## ساختار پروژه
```
HelpDesk/
├── Controllers/       # AccountController, TicketsController, HomeController
├── Models/             # ApplicationUser, Ticket
├── Data/                # ApplicationDbContext
├── Views/               # صفحات Razor
├── wwwroot/             # فایل‌های استاتیک (CSS)
├── Program.cs           # پیکربندی برنامه و seed کاربر ادمین
└── appsettings.json     # تنظیمات (رشته اتصال دیتابیس)
```

## نکات امنیتی برای محیط Production
این پروژه برای یادگیری/شروع کار طراحی شده. قبل از استفاده واقعی:
- سیاست رمز عبور را سخت‌گیرانه‌تر کنید (در `Program.cs`, بخش `options.Password`)
- رمز عبور کاربر admin را بلافاصله بعد از اولین ورود تغییر دهید
- از یک دیتابیس production-grade (مثل SQL Server یا PostgreSQL) به‌جای SQLite استفاده کنید
