using Microsoft.EntityFrameworkCore;
using TaskManagementSystemm.Domain;
using TaskManagementSystemm.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=.;Database=TaskDB;Trusted_Connection=True;"));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=.;Database=TaskDB;Trusted_Connection=True;"));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

app.UseAuthentication();
app.UseAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRoles(roleManager);
}

// ================== TASK NOTES ==================

// 1- Refactoring & Design
// أنا قسمت المشروع لـ Layers (Domain - Infrastructure - API)
// عشان أفصل بين كل جزء في الشغل.
// استخدمت Repository Pattern علشان أعزل التعامل مع الداتا
// بدل ما يكون جوه الـ Controller.
// وكمان استخدمت Dependency Injection عشان أربط الـ Repository بالـ Controller
// بطريقة سهلة وتخلي الكود مرن وسهل يتعدل بعدين.


// 2- Custom Routing
// خليت الـ routes تكون واضحة وبسيطة زي:
// /api/tasks
// /api/tasks/{id}
// بحيث أي حد يستخدم الـ API يفهمه بسهولة
// وكل عملية (GET, POST, PUT, DELETE) ليها route واضح.


// 3- Dependency Injection (شرح بسيط)
// الـ DI فكرته إن بدل ما أعمل object جوه الكلاس
// بخلّي النظام هو اللي يديهولي جاهز.
// ده بيساعد إن الكود يبقى أنضف وأسهل في التست والتعديل.
// مثال:
builder.Services.AddScoped<ITaskRepository, TaskRepository>();


// 4- Security (Connection String)
// طبعًا مش صح إني أكتب الـ connection string في الكود مباشرة
// عشان ده ممكن يعرض الداتا للخطر.
// فممكن نحلها كذا طريقة:
// 1- نحطها في appsettings.json
// 2- نستخدم Environment Variables
// 3- نستخدم Secret Manager في .NET
// وده بيخلي الداتا الحساسة مش ظاهرة في الكود.


// 5- AJAX (Bonus)
// AJAX بيسمحلي أجيب داتا من السيرفر من غير ما أعمل reload للصفحة.
// وده بيحسن تجربة المستخدم جدًا.
// استخدمته عشان أجيب التاسكات بشكل سريع.
// مثال بسيط:
// fetch('/api/tasks').then(res => res.json())


// 6- Creational Design Patterns (Bonus)
// الـ Design Patterns دي بتساعدني أتعامل مع إنشاء الـ objects بشكل منظم.
// زي:
// - Singleton
// - Factory
// - Builder
// والـ Dependency Injection يعتبر شبههم
// لأنه بيدير إنشاء الـ objects بدل ما أعملها بإيدي.











// ================== LINKEDIN ARTICLE ==================

// Async Programming:
// أثناء شغلي على المشروع استخدمت async/await
// عشان أحسن الأداء ومايكونش فيه blocking

// الفكرة:
// بدل ما البرنامج يستنى العملية تخلص
// بيكمل شغل وبيرجع للنتيجة بعدين

// مثال:
public async Task<List<TaskItem>> GetAllAsync()
{
    return await _context.Tasks.ToListAsync();
}

// الفايدة:
// - أداء أسرع
// - استجابة أحسن
// - مناسب للـ Web Applications