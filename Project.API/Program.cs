
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Admin.Commands;
using Project.Application.Student.Commands;
using Project.Application.Student.Queries;
using Project.Application.Teacher.Commands;
using Project.Persistance;
using Project.Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<UmDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injecting the MediatR to our DI
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

// Register MediatR handlers for each command and query
builder.Services.AddTransient(typeof(IRequestHandler<CreateCourseCommand, Course>), typeof(CreateCourseCommandHandler));
builder.Services.AddTransient(typeof(IRequestHandler<SetEnrollmentDateRangeCommand, ClassEnrollment>), typeof(SetEnrollmentDateRangeCommandHandler));
builder.Services.AddTransient(typeof(IRequestHandler<SetMaxStudentsCommand, Course>), typeof(SetEnrollmentDateRangeCommandHandler));
builder.Services.AddTransient(typeof(IRequestHandler<EnrollInCourseCommand, Course>), typeof(EnrollInCourseCommandHandler));
builder.Services.AddTransient(typeof(IRequestHandler<CheckEnrollmentDateRangeQuery, Course>), typeof(CheckEnrollmentDateRangeQuery.CheckEnrollmentDateRangeQueryHandler));
builder.Services.AddTransient(typeof(IRequestHandler<AssignCourseToTimeSlotCommand, TeacherPerCourse>), typeof(AssignCourseToTimeSlotCommandHandler));
builder.Services.AddTransient(typeof(IRequestHandler<CreateTimeSlotCommand, SessionTime>), typeof(CreateTimeSlotCommandHandler));
builder.Services.AddTransient(typeof(IRequestHandler<RegisterAsTeacherCommand, TeacherPerCourse >), typeof(RegisterAsTeacherCommandHandler));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();