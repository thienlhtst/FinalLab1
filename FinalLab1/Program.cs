using System.Text;
using Confluent.Kafka.Admin;
using Confluent.Kafka;
using FinalLab1.Converter;
using FinalLab1.Data;
using FinalLab1.Hubs;
using FinalLab1.Services;
using FinalLab1.Services.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Nest;
using FinalLab1.Kafka.Consumers;
using FinalLab1.Kafka.Producers;

var builder = WebApplication.CreateBuilder(args);
var kafkaConfig = builder.Configuration.GetSection("Kafka");
var bootstrapServers = kafkaConfig["BootstrapServers"];
var topicName = kafkaConfig["TopicName"];
var numPartitions = int.Parse(kafkaConfig["NumPartitions"]);
var replicationFactor = short.Parse(kafkaConfig["ReplicationFactor"]);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // 👇 Thêm phần này
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header.
                        Nhập vào dạng: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddDbContext<LabDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<EventSearchService>();
builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
        .DefaultIndex("events_index");

    return new ElasticClient(settings);
});
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped<TicketService>();

builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddSignalR();
// Đọc chuỗi kết nối Redis từ appsettings.json
var redisConnectionString = builder.Configuration.GetSection("Redis")["ConnectionString"];

// Đăng ký IConnectionMultiplexer để sử dụng trong RedisQueueService
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
builder.Services.AddScoped<TicketQueueService>();

// ✅ Constructor đã sửa public, và không cần truyền prefix
builder.Services.AddSingleton<ActiveEventRedisService>();
builder.Services.AddSingleton<EventQueueRedisService>();

builder.Services.AddSingleton<TicketKafkaProducerService>();

builder.Services.AddSingleton<KafkaProducerService>();
builder.Services.AddHostedService<KafkaConsumerHostedService>();
builder.Services.AddHostedService<TicketKafkaConsumerService>();

/////////////////////////////////AuthenAuthor////////////
var jwtConfig = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtConfig["Secret"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.MapHub<QueueHub>("/queuehub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build())
{
    try
    {
        Console.WriteLine($"Checking if Kafka topic '{topicName}' exists...");

        // Lấy metadata từ Kafka
        var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));

        // Kiểm tra xem topic đã tồn tại hay chưa
        if (metadata.Topics.Any(t => t.Topic == topicName && t.Error.Code == ErrorCode.NoError))
        {
            Console.WriteLine($"Topic '{topicName}' already exists.");
        }
        else
        {
            Console.WriteLine($"Creating Kafka topic '{topicName}'...");

            // Định nghĩa topic cần tạo
            var topicSpecifications = new List<TopicSpecification>
            {
                new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = numPartitions,
                    ReplicationFactor = replicationFactor
                }
            };

            // Tạo topic
            adminClient.CreateTopicsAsync(topicSpecifications).Wait();

            Console.WriteLine("Topic created successfully.");
        }
    }
    catch (CreateTopicsException e)
    {
        // Xử lý lỗi nếu có vấn đề xảy ra
        foreach (var result in e.Results)
        {
            Console.WriteLine($"Error creating topic {result.Topic}: {result.Error.Reason}");
        }
    }
    catch (Exception ex)
    {
        // Xử lý các lỗi khác
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }
}
app.Run();