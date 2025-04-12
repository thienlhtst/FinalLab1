using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalLab1.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Methods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Information = table.Column<string>(type: "text", nullable: false),
                    LogoImage = table.Column<string>(type: "text", nullable: false),
                    BannerImage = table.Column<string>(type: "text", nullable: false),
                    CountEvent = table.Column<int>(type: "integer", nullable: false),
                    MaxSeatsPerUser = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    NameOwner = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumberBank = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameBank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventCategorys_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCategorys_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    FromTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ToTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MaxSeat = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSessions_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSessions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Information = table.Column<string>(type: "text", nullable: false),
                    LogoImage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    FromDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventSessionId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountSeat = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Information = table.Column<string>(type: "text", nullable: false),
                    Opentobuy = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_EventSessions_EventSessionId",
                        column: x => x.EventSessionId,
                        principalTable: "EventSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    Section = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailSeats_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumberSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DetailSeatId = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumberSeats_DetailSeats_DetailSeatId",
                        column: x => x.DetailSeatId,
                        principalTable: "DetailSeats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    NumberSeatId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TypeTicket = table.Column<int>(type: "integer", nullable: false),
                    QrImage = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_NumberSeats_NumberSeatId",
                        column: x => x.NumberSeatId,
                        principalTable: "NumberSeats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TicketId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    MethodId = table.Column<int>(type: "integer", nullable: false),
                    PaidAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "Music" },
                    { 2, "", "Technology" },
                    { 3, "", "Art" },
                    { 4, "", "Business" },
                    { 5, "", "Education" },
                    { 6, "", "Health" },
                    { 7, "", "Fashion" },
                    { 8, "", "Food" },
                    { 9, "", "Gaming" },
                    { 10, "", "Literature" },
                    { 11, "", "Film" },
                    { 12, "", "Photography" },
                    { 13, "", "Theater" },
                    { 14, "", "Dance" },
                    { 15, "", "Charity" },
                    { 16, "", "Networking" },
                    { 17, "", "Science" },
                    { 18, "", "Spirituality" },
                    { 19, "", "Sports" },
                    { 20, "", "Travel" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "123 Lê Lợi", "Hà Nội", "Việt Nam", "Hội trường A" },
                    { 2, "234 Trần Hưng Đạo", "Hà Nội", "Việt Nam", "Hội trường B" },
                    { 3, "12 Nguyễn Huệ", "TP.HCM", "Việt Nam", "Trung tâm Triển lãm 1" },
                    { 4, "88 Lê Duẩn", "Đà Nẵng", "Việt Nam", "Sân vận động A" },
                    { 5, "1 Tràng Tiền", "Hà Nội", "Việt Nam", "Nhà hát lớn" },
                    { 6, "456 Lý Thường Kiệt", "TP.HCM", "Việt Nam", "Phòng họp 101" },
                    { 7, "789 Nguyễn Trãi", "Cần Thơ", "Việt Nam", "Trường Đại học ABC" },
                    { 8, "22 Phạm Văn Đồng", "Huế", "Việt Nam", "Sảnh tiệc Hoa Mai" },
                    { 9, "999 Lạc Long Quân", "Đà Lạt", "Việt Nam", "Khách sạn XYZ" },
                    { 10, "111 Nguyễn Văn Linh", "Hải Phòng", "Việt Nam", "Hội trường lớn" },
                    { 11, "98 Trường Chinh", "TP.HCM", "Việt Nam", "Khu vui chơi ABC" },
                    { 12, "33 Pasteur", "Hà Nội", "Việt Nam", "Văn phòng Công ty DEF" },
                    { 13, "12B Nguyễn Văn Cừ", "Bắc Ninh", "Việt Nam", "Sảnh tổ chức 5 sao" },
                    { 14, "15 Huỳnh Thúc Kháng", "TP.HCM", "Việt Nam", "Rạp chiếu phim Galaxy" },
                    { 15, "21 Lê Văn Sỹ", "Nha Trang", "Việt Nam", "Nhà thiếu nhi" },
                    { 16, "01 Đại lộ Thăng Long", "Hà Nội", "Việt Nam", "Trung tâm hội nghị Quốc Gia" },
                    { 17, "22 Nguyễn Hữu Cảnh", "Huế", "Việt Nam", "Bảo tàng Văn hóa" },
                    { 18, "23/9 Nguyễn Thị Minh Khai", "TP.HCM", "Việt Nam", "Công viên 23/9" },
                    { 19, "66 Võ Văn Tần", "Bình Dương", "Việt Nam", "Trung tâm Hội chợ" },
                    { 20, "888 Quốc lộ 1A", "Biên Hòa", "Việt Nam", "Trường Quốc tế DEF" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthday", "Email", "Gender", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "nguyenvana@example.com", 0, "Nguyễn Văn A", "password1", "0900000001" },
                    { 2, new DateTimeOffset(new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "tranthib@example.com", 1, "Trần Thị B", "password2", "0900000002" },
                    { 3, new DateTimeOffset(new DateTime(1991, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "levanc@example.com", 0, "Lê Văn C", "password3", "0900000003" },
                    { 4, new DateTimeOffset(new DateTime(1989, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "phamthid@example.com", 1, "Phạm Thị D", "password4", "0900000004" },
                    { 5, new DateTimeOffset(new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "hoangmine@example.com", 2, "Hoàng Minh E", "password5", "0900000005" },
                    { 6, new DateTimeOffset(new DateTime(1990, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "doquocf@example.com", 0, "Đỗ Quốc F", "password6", "0900000006" },
                    { 7, new DateTimeOffset(new DateTime(1993, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "vothig@example.com", 1, "Võ Thị G", "password7", "0900000007" },
                    { 8, new DateTimeOffset(new DateTime(1988, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "danghuuh@example.com", 0, "Đặng Hữu H", "password8", "0900000008" },
                    { 9, new DateTimeOffset(new DateTime(1994, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ngothii@example.com", 1, "Ngô Thị I", "password9", "0900000009" },
                    { 10, new DateTimeOffset(new DateTime(1990, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "caovanj@example.com", 0, "Cao Văn J", "password10", "0900000010" },
                    { 11, new DateTimeOffset(new DateTime(1992, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "lythik@example.com", 1, "Lý Thị K", "password11", "0900000011" },
                    { 12, new DateTimeOffset(new DateTime(1991, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "chauminhl@example.com", 0, "Châu Minh L", "password12", "0900000012" },
                    { 13, new DateTimeOffset(new DateTime(1993, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "tothim@example.com", 1, "Tô Thị M", "password13", "0900000013" },
                    { 14, new DateTimeOffset(new DateTime(1990, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "buingocn@example.com", 0, "Bùi Ngọc N", "password14", "0900000014" },
                    { 15, new DateTimeOffset(new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "nguyenhoaio@example.com", 2, "Nguyễn Hoài O", "password15", "0900000015" },
                    { 16, new DateTimeOffset(new DateTime(1989, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "trinhquocp@example.com", 0, "Trịnh Quốc P", "password16", "0900000016" },
                    { 17, new DateTimeOffset(new DateTime(1992, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "maithiq@example.com", 1, "Mai Thị Q", "password17", "0900000017" },
                    { 18, new DateTimeOffset(new DateTime(1988, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "lamanhr@example.com", 0, "Lâm Anh R", "password18", "0900000018" },
                    { 19, new DateTimeOffset(new DateTime(1994, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "hominhs@example.com", 0, "Hồ Minh S", "password19", "0900000019" },
                    { 20, new DateTimeOffset(new DateTime(1991, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "duongthaot@example.com", 2, "Dương Thảo T", "password20", "0900000020" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "BannerImage", "CountEvent", "Information", "LogoImage", "MaxSeatsPerUser", "Name", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "banner1.png", 100, "Live concert", "logo1.png", 4, "Music Show", 0, 1 },
                    { 2, "banner2.png", 200, "Seminar on AI", "logo2.png", 2, "Tech Talk", 0, 2 },
                    { 3, "banner3.png", 150, "Exhibition of paintings", "logo3.png", 3, "Art Fair", 1, 3 },
                    { 4, "banner4.png", 300, "Startup networking", "logo4.png", 5, "Business Meetup", 0, 4 },
                    { 5, "banner5.png", 120, "Books and authors", "logo5.png", 1, "Book Fair", 0, 5 },
                    { 6, "banner6.png", 250, "Healthcare and wellness", "logo6.png", 2, "Health Expo", 2, 6 },
                    { 7, "banner7.png", 80, "Learn full-stack dev", "logo7.png", 3, "Coding Bootcamp", 1, 7 },
                    { 8, "banner8.png", 90, "Traditional & modern", "logo8.png", 4, "Dance Show", 0, 8 },
                    { 9, "banner9.png", 110, "School science projects", "logo9.png", 5, "Science Fair", 1, 9 },
                    { 10, "banner10.png", 140, "Indie movie night", "logo10.png", 2, "Film Screening", 2, 10 },
                    { 11, "banner11.png", 160, "Nature and people", "logo11.png", 3, "Photography Contest", 0, 11 },
                    { 12, "banner12.png", 180, "Pitch your ideas", "logo12.png", 4, "Startup Pitch", 3, 12 },
                    { 13, "banner13.png", 130, "Local cuisine", "logo13.png", 5, "Food Fest", 0, 13 },
                    { 14, "banner14.png", 90, "Mind & Body", "logo14.png", 2, "Yoga Day", 2, 14 },
                    { 15, "banner15.png", 70, "Dogs, cats and more", "logo15.png", 3, "Pet Show", 1, 15 },
                    { 16, "banner16.png", 200, "Run for a cause", "logo16.png", 1, "Charity Run", 0, 16 },
                    { 17, "banner17.png", 175, "Environment & Sustainability", "logo17.png", 2, "Eco Summit", 0, 17 },
                    { 18, "banner18.png", 160, "Runway and designers", "logo18.png", 4, "Fashion Gala", 3, 18 },
                    { 19, "banner19.png", 300, "24-hour coding", "logo19.png", 2, "Hackathon", 1, 19 },
                    { 20, "banner20.png", 95, "Calm and relax", "logo20.png", 1, "Meditation Workshop", 2, 20 }
                });

            migrationBuilder.InsertData(
                table: "EventCategorys",
                columns: new[] { "Id", "CategoryId", "EventId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 3, 2 },
                    { 5, 2, 3 },
                    { 6, 4, 3 },
                    { 7, 1, 4 },
                    { 8, 5, 4 },
                    { 9, 2, 5 },
                    { 10, 3, 5 },
                    { 11, 1, 6 },
                    { 12, 4, 6 },
                    { 13, 5, 7 },
                    { 14, 2, 7 },
                    { 15, 3, 8 },
                    { 16, 4, 8 },
                    { 17, 1, 9 },
                    { 18, 5, 9 },
                    { 19, 2, 10 },
                    { 20, 3, 10 }
                });

            migrationBuilder.InsertData(
                table: "EventSessions",
                columns: new[] { "Id", "EventId", "FromTime", "LocationId", "MaxSeat", "ToTime", "Type" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2026, 1, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 100, new DateTimeOffset(new DateTime(2026, 1, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, 1, new DateTimeOffset(new DateTime(2026, 1, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 80, new DateTimeOffset(new DateTime(2026, 1, 6, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 3, 2, new DateTimeOffset(new DateTime(2026, 1, 7, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, 120, new DateTimeOffset(new DateTime(2026, 1, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 4, 2, new DateTimeOffset(new DateTime(2026, 1, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4, 90, new DateTimeOffset(new DateTime(2026, 1, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 5, 3, new DateTimeOffset(new DateTime(2026, 1, 9, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 5, 60, new DateTimeOffset(new DateTime(2026, 1, 9, 18, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 6, 3, new DateTimeOffset(new DateTime(2026, 1, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 75, new DateTimeOffset(new DateTime(2026, 1, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 7, 4, new DateTimeOffset(new DateTime(2026, 1, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 50, new DateTimeOffset(new DateTime(2026, 1, 11, 17, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 8, 4, new DateTimeOffset(new DateTime(2026, 1, 12, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, 100, new DateTimeOffset(new DateTime(2026, 1, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 9, 5, new DateTimeOffset(new DateTime(2026, 1, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4, 65, new DateTimeOffset(new DateTime(2026, 1, 13, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 10, 5, new DateTimeOffset(new DateTime(2026, 1, 14, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 5, 85, new DateTimeOffset(new DateTime(2026, 1, 14, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 11, 6, new DateTimeOffset(new DateTime(2026, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 70, new DateTimeOffset(new DateTime(2026, 1, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 12, 6, new DateTimeOffset(new DateTime(2026, 1, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 90, new DateTimeOffset(new DateTime(2026, 1, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 13, 7, new DateTimeOffset(new DateTime(2026, 1, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, 110, new DateTimeOffset(new DateTime(2026, 1, 17, 13, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 14, 7, new DateTimeOffset(new DateTime(2026, 1, 18, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4, 95, new DateTimeOffset(new DateTime(2026, 1, 18, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 15, 8, new DateTimeOffset(new DateTime(2026, 1, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 5, 100, new DateTimeOffset(new DateTime(2026, 1, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 16, 8, new DateTimeOffset(new DateTime(2026, 1, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 85, new DateTimeOffset(new DateTime(2026, 1, 20, 17, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 17, 9, new DateTimeOffset(new DateTime(2026, 1, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 120, new DateTimeOffset(new DateTime(2026, 1, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 18, 9, new DateTimeOffset(new DateTime(2026, 1, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, 60, new DateTimeOffset(new DateTime(2026, 1, 22, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { 19, 10, new DateTimeOffset(new DateTime(2026, 1, 23, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4, 100, new DateTimeOffset(new DateTime(2026, 1, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 20, 10, new DateTimeOffset(new DateTime(2026, 1, 24, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 5, 80, new DateTimeOffset(new DateTime(2026, 1, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0 }
                });

            migrationBuilder.InsertData(
                table: "SpecialEvents",
                columns: new[] { "Id", "EventId", "FromDate", "Reason", "ToDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Kỷ niệm thành lập", new DateTimeOffset(new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, 2, new DateTimeOffset(new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Chương trình giảm giá", new DateTimeOffset(new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, 3, new DateTimeOffset(new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Ngày hội gia đình", new DateTimeOffset(new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 4, 4, new DateTimeOffset(new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tuần lễ văn hóa", new DateTimeOffset(new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 5, 5, new DateTimeOffset(new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Sự kiện nội bộ", new DateTimeOffset(new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 6, 1, new DateTimeOffset(new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Kỷ niệm 10 năm", new DateTimeOffset(new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 7, 2, new DateTimeOffset(new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Summer Festival", new DateTimeOffset(new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 8, 3, new DateTimeOffset(new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Special Guest Show", new DateTimeOffset(new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 9, 4, new DateTimeOffset(new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Charity Program", new DateTimeOffset(new DateTime(2025, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 10, 5, new DateTimeOffset(new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tech Day", new DateTimeOffset(new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 11, 6, new DateTimeOffset(new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Ngày hội nghệ thuật", new DateTimeOffset(new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 12, 7, new DateTimeOffset(new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Triển lãm đặc biệt", new DateTimeOffset(new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 13, 8, new DateTimeOffset(new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cuộc thi cosplay", new DateTimeOffset(new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 14, 9, new DateTimeOffset(new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Chào đón sinh viên", new DateTimeOffset(new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 15, 10, new DateTimeOffset(new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Sự kiện cuối năm", new DateTimeOffset(new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 16, 6, new DateTimeOffset(new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Live Performance", new DateTimeOffset(new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 17, 7, new DateTimeOffset(new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Ngày hội âm nhạc", new DateTimeOffset(new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 18, 8, new DateTimeOffset(new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Thử thách 24h", new DateTimeOffset(new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 19, 9, new DateTimeOffset(new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Workshop sáng tạo", new DateTimeOffset(new DateTime(2025, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 20, 10, new DateTimeOffset(new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Gala Dinner", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "CountSeat", "EventSessionId", "Information", "Name", "Opentobuy", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "100", 1, "Standard seating", "A1", new DateTimeOffset(new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 29.99m, "Standard" },
                    { 2, "50", 1, "Front row seats", "A2", new DateTimeOffset(new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 59.99m, "VIP" },
                    { 3, "120", 2, "Cheaper seating", "B1", new DateTimeOffset(new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 19.99m, "Economy" },
                    { 4, "60", 2, "Includes backstage access", "B2", new DateTimeOffset(new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 69.99m, "VIP" },
                    { 5, "80", 3, "Great view", "C1", new DateTimeOffset(new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 39.99m, "Standard" },
                    { 6, "90", 3, "Upper level seating", "C2", new DateTimeOffset(new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 24.99m, "Balcony" },
                    { 7, "150", 4, "Spacious seats", "D1", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 34.99m, "Standard" },
                    { 8, "70", 4, "Free refreshments", "D2", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 79.99m, "VIP" },
                    { 9, "200", 5, "Budget seating", "E1", new DateTimeOffset(new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 14.99m, "Economy" },
                    { 10, "40", 5, "Exclusive seats", "E2", new DateTimeOffset(new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 89.99m, "VIP" },
                    { 11, "110", 6, "Good view", "F1", new DateTimeOffset(new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 32.50m, "Standard" },
                    { 12, "75", 6, "Top level", "F2", new DateTimeOffset(new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 22.50m, "Balcony" },
                    { 13, "100", 7, "Normal seats", "G1", new DateTimeOffset(new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 27.00m, "Standard" },
                    { 14, "50", 7, "Luxury comfort", "G2", new DateTimeOffset(new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 74.00m, "VIP" },
                    { 15, "130", 8, "Affordable choice", "H1", new DateTimeOffset(new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 18.00m, "Economy" },
                    { 16, "90", 8, "Meet and greet included", "H2", new DateTimeOffset(new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 99.99m, "VIP" },
                    { 17, "85", 9, "Middle seats", "I1", new DateTimeOffset(new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 29.00m, "Standard" },
                    { 18, "45", 9, "High view", "I2", new DateTimeOffset(new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 26.00m, "Balcony" },
                    { 19, "95", 10, "General seating", "J1", new DateTimeOffset(new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 30.00m, "Standard" },
                    { 20, "65", 10, "Premium seating", "J2", new DateTimeOffset(new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 85.00m, "VIP" }
                });

            migrationBuilder.InsertData(
                table: "DetailSeats",
                columns: new[] { "Id", "SeatId", "Section" },
                values: new object[,]
                {
                    { 1, 1, "A" },
                    { 2, 1, "B" },
                    { 3, 1, "C" },
                    { 4, 2, "D" },
                    { 5, 2, "E" },
                    { 6, 2, "G" },
                    { 7, 3, "X" },
                    { 8, 3, "Y" },
                    { 9, 3, "Z" },
                    { 10, 4, "D" },
                    { 11, 4, "E" },
                    { 12, 4, "G" },
                    { 13, 5, "A" },
                    { 14, 5, "B" },
                    { 15, 5, "C" },
                    { 16, 6, "L" },
                    { 17, 6, "M" },
                    { 18, 6, "N" },
                    { 19, 7, "A" },
                    { 20, 7, "B" },
                    { 21, 7, "C" },
                    { 22, 8, "X" },
                    { 23, 8, "Y" },
                    { 24, 8, "Z" },
                    { 25, 9, "D" },
                    { 26, 9, "E" },
                    { 27, 9, "G" },
                    { 28, 10, "A" },
                    { 29, 10, "B" },
                    { 30, 10, "C" },
                    { 31, 11, "L" },
                    { 32, 11, "M" },
                    { 33, 11, "N" },
                    { 34, 12, "X" },
                    { 35, 12, "Y" },
                    { 36, 12, "Z" },
                    { 37, 13, "D" },
                    { 38, 13, "E" },
                    { 39, 13, "G" },
                    { 40, 14, "A" },
                    { 41, 14, "B" },
                    { 42, 14, "C" },
                    { 43, 15, "L" },
                    { 44, 15, "M" },
                    { 45, 15, "N" },
                    { 46, 16, "D" },
                    { 47, 16, "E" },
                    { 48, 16, "G" },
                    { 49, 17, "A" },
                    { 50, 17, "B" }
                });

            migrationBuilder.InsertData(
                table: "NumberSeats",
                columns: new[] { "Id", "DetailSeatId", "Number" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 },
                    { 6, 1, 6 },
                    { 7, 1, 7 },
                    { 8, 1, 8 },
                    { 9, 1, 9 },
                    { 10, 1, 10 },
                    { 11, 2, 1 },
                    { 12, 2, 2 },
                    { 13, 2, 3 },
                    { 14, 2, 4 },
                    { 15, 2, 5 },
                    { 16, 2, 6 },
                    { 17, 2, 7 },
                    { 18, 2, 8 },
                    { 19, 2, 9 },
                    { 20, 2, 10 },
                    { 21, 3, 1 },
                    { 22, 3, 2 },
                    { 23, 3, 3 },
                    { 24, 3, 4 },
                    { 25, 3, 5 },
                    { 26, 3, 6 },
                    { 27, 3, 7 },
                    { 28, 3, 8 },
                    { 29, 3, 9 },
                    { 30, 3, 10 },
                    { 31, 4, 1 },
                    { 32, 4, 2 },
                    { 33, 4, 3 },
                    { 34, 4, 4 },
                    { 35, 4, 5 },
                    { 36, 4, 6 },
                    { 37, 4, 7 },
                    { 38, 4, 8 },
                    { 39, 4, 9 },
                    { 40, 4, 10 },
                    { 41, 5, 1 },
                    { 42, 5, 2 },
                    { 43, 5, 3 },
                    { 44, 5, 4 },
                    { 45, 5, 5 },
                    { 46, 5, 6 },
                    { 47, 5, 7 },
                    { 48, 5, 8 },
                    { 49, 5, 9 },
                    { 50, 5, 10 },
                    { 51, 6, 1 },
                    { 52, 6, 2 },
                    { 53, 6, 3 },
                    { 54, 6, 4 },
                    { 55, 6, 5 },
                    { 56, 6, 6 },
                    { 57, 6, 7 },
                    { 58, 6, 8 },
                    { 59, 6, 9 },
                    { 60, 6, 10 },
                    { 61, 7, 1 },
                    { 62, 7, 2 },
                    { 63, 7, 3 },
                    { 64, 7, 4 },
                    { 65, 7, 5 },
                    { 66, 7, 6 },
                    { 67, 7, 7 },
                    { 68, 7, 8 },
                    { 69, 7, 9 },
                    { 70, 7, 10 },
                    { 71, 8, 1 },
                    { 72, 8, 2 },
                    { 73, 8, 3 },
                    { 74, 8, 4 },
                    { 75, 8, 5 },
                    { 76, 8, 6 },
                    { 77, 8, 7 },
                    { 78, 8, 8 },
                    { 79, 8, 9 },
                    { 80, 8, 10 },
                    { 81, 9, 1 },
                    { 82, 9, 2 },
                    { 83, 9, 3 },
                    { 84, 9, 4 },
                    { 85, 9, 5 },
                    { 86, 9, 6 },
                    { 87, 9, 7 },
                    { 88, 9, 8 },
                    { 89, 9, 9 },
                    { 90, 9, 10 },
                    { 91, 10, 1 },
                    { 92, 10, 2 },
                    { 93, 10, 3 },
                    { 94, 10, 4 },
                    { 95, 10, 5 },
                    { 96, 10, 6 },
                    { 97, 10, 7 },
                    { 98, 10, 8 },
                    { 99, 10, 9 },
                    { 100, 10, 10 },
                    { 101, 11, 1 },
                    { 102, 11, 2 },
                    { 103, 11, 3 },
                    { 104, 11, 4 },
                    { 105, 11, 5 },
                    { 106, 11, 6 },
                    { 107, 11, 7 },
                    { 108, 11, 8 },
                    { 109, 11, 9 },
                    { 110, 11, 10 },
                    { 111, 12, 1 },
                    { 112, 12, 2 },
                    { 113, 12, 3 },
                    { 114, 12, 4 },
                    { 115, 12, 5 },
                    { 116, 12, 6 },
                    { 117, 12, 7 },
                    { 118, 12, 8 },
                    { 119, 12, 9 },
                    { 120, 12, 10 },
                    { 121, 13, 1 },
                    { 122, 13, 2 },
                    { 123, 13, 3 },
                    { 124, 13, 4 },
                    { 125, 13, 5 },
                    { 126, 13, 6 },
                    { 127, 13, 7 },
                    { 128, 13, 8 },
                    { 129, 13, 9 },
                    { 130, 13, 10 },
                    { 131, 14, 1 },
                    { 132, 14, 2 },
                    { 133, 14, 3 },
                    { 134, 14, 4 },
                    { 135, 14, 5 },
                    { 136, 14, 6 },
                    { 137, 14, 7 },
                    { 138, 14, 8 },
                    { 139, 14, 9 },
                    { 140, 14, 10 },
                    { 141, 15, 1 },
                    { 142, 15, 2 },
                    { 143, 15, 3 },
                    { 144, 15, 4 },
                    { 145, 15, 5 },
                    { 146, 15, 6 },
                    { 147, 15, 7 },
                    { 148, 15, 8 },
                    { 149, 15, 9 },
                    { 150, 15, 10 },
                    { 151, 16, 1 },
                    { 152, 16, 2 },
                    { 153, 16, 3 },
                    { 154, 16, 4 },
                    { 155, 16, 5 },
                    { 156, 16, 6 },
                    { 157, 16, 7 },
                    { 158, 16, 8 },
                    { 159, 16, 9 },
                    { 160, 16, 10 },
                    { 161, 17, 1 },
                    { 162, 17, 2 },
                    { 163, 17, 3 },
                    { 164, 17, 4 },
                    { 165, 17, 5 },
                    { 166, 17, 6 },
                    { 167, 17, 7 },
                    { 168, 17, 8 },
                    { 169, 17, 9 },
                    { 170, 17, 10 },
                    { 171, 18, 1 },
                    { 172, 18, 2 },
                    { 173, 18, 3 },
                    { 174, 18, 4 },
                    { 175, 18, 5 },
                    { 176, 18, 6 },
                    { 177, 18, 7 },
                    { 178, 18, 8 },
                    { 179, 18, 9 },
                    { 180, 18, 10 },
                    { 181, 19, 1 },
                    { 182, 19, 2 },
                    { 183, 19, 3 },
                    { 184, 19, 4 },
                    { 185, 19, 5 },
                    { 186, 19, 6 },
                    { 187, 19, 7 },
                    { 188, 19, 8 },
                    { 189, 19, 9 },
                    { 190, 19, 10 },
                    { 191, 20, 1 },
                    { 192, 20, 2 },
                    { 193, 20, 3 },
                    { 194, 20, 4 },
                    { 195, 20, 5 },
                    { 196, 20, 6 },
                    { 197, 20, 7 },
                    { 198, 20, 8 },
                    { 199, 20, 9 },
                    { 200, 20, 10 },
                    { 201, 21, 1 },
                    { 202, 21, 2 },
                    { 203, 21, 3 },
                    { 204, 21, 4 },
                    { 205, 21, 5 },
                    { 206, 21, 6 },
                    { 207, 21, 7 },
                    { 208, 21, 8 },
                    { 209, 21, 9 },
                    { 210, 21, 10 },
                    { 211, 22, 1 },
                    { 212, 22, 2 },
                    { 213, 22, 3 },
                    { 214, 22, 4 },
                    { 215, 22, 5 },
                    { 216, 22, 6 },
                    { 217, 22, 7 },
                    { 218, 22, 8 },
                    { 219, 22, 9 },
                    { 220, 22, 10 },
                    { 221, 23, 1 },
                    { 222, 23, 2 },
                    { 223, 23, 3 },
                    { 224, 23, 4 },
                    { 225, 23, 5 },
                    { 226, 23, 6 },
                    { 227, 23, 7 },
                    { 228, 23, 8 },
                    { 229, 23, 9 },
                    { 230, 23, 10 },
                    { 231, 24, 1 },
                    { 232, 24, 2 },
                    { 233, 24, 3 },
                    { 234, 24, 4 },
                    { 235, 24, 5 },
                    { 236, 24, 6 },
                    { 237, 24, 7 },
                    { 238, 24, 8 },
                    { 239, 24, 9 },
                    { 240, 24, 10 },
                    { 241, 25, 1 },
                    { 242, 25, 2 },
                    { 243, 25, 3 },
                    { 244, 25, 4 },
                    { 245, 25, 5 },
                    { 246, 25, 6 },
                    { 247, 25, 7 },
                    { 248, 25, 8 },
                    { 249, 25, 9 },
                    { 250, 25, 10 },
                    { 251, 26, 1 },
                    { 252, 26, 2 },
                    { 253, 26, 3 },
                    { 254, 26, 4 },
                    { 255, 26, 5 },
                    { 256, 26, 6 },
                    { 257, 26, 7 },
                    { 258, 26, 8 },
                    { 259, 26, 9 },
                    { 260, 26, 10 },
                    { 261, 27, 1 },
                    { 262, 27, 2 },
                    { 263, 27, 3 },
                    { 264, 27, 4 },
                    { 265, 27, 5 },
                    { 266, 27, 6 },
                    { 267, 27, 7 },
                    { 268, 27, 8 },
                    { 269, 27, 9 },
                    { 270, 27, 10 },
                    { 271, 28, 1 },
                    { 272, 28, 2 },
                    { 273, 28, 3 },
                    { 274, 28, 4 },
                    { 275, 28, 5 },
                    { 276, 28, 6 },
                    { 277, 28, 7 },
                    { 278, 28, 8 },
                    { 279, 28, 9 },
                    { 280, 28, 10 },
                    { 281, 29, 1 },
                    { 282, 29, 2 },
                    { 283, 29, 3 },
                    { 284, 29, 4 },
                    { 285, 29, 5 },
                    { 286, 29, 6 },
                    { 287, 29, 7 },
                    { 288, 29, 8 },
                    { 289, 29, 9 },
                    { 290, 29, 10 },
                    { 291, 30, 1 },
                    { 292, 30, 2 },
                    { 293, 30, 3 },
                    { 294, 30, 4 },
                    { 295, 30, 5 },
                    { 296, 30, 6 },
                    { 297, 30, 7 },
                    { 298, 30, 8 },
                    { 299, 30, 9 },
                    { 300, 30, 10 },
                    { 301, 31, 1 },
                    { 302, 31, 2 },
                    { 303, 31, 3 },
                    { 304, 31, 4 },
                    { 305, 31, 5 },
                    { 306, 31, 6 },
                    { 307, 31, 7 },
                    { 308, 31, 8 },
                    { 309, 31, 9 },
                    { 310, 31, 10 },
                    { 311, 32, 1 },
                    { 312, 32, 2 },
                    { 313, 32, 3 },
                    { 314, 32, 4 },
                    { 315, 32, 5 },
                    { 316, 32, 6 },
                    { 317, 32, 7 },
                    { 318, 32, 8 },
                    { 319, 32, 9 },
                    { 320, 32, 10 },
                    { 321, 33, 1 },
                    { 322, 33, 2 },
                    { 323, 33, 3 },
                    { 324, 33, 4 },
                    { 325, 33, 5 },
                    { 326, 33, 6 },
                    { 327, 33, 7 },
                    { 328, 33, 8 },
                    { 329, 33, 9 },
                    { 330, 33, 10 },
                    { 331, 34, 1 },
                    { 332, 34, 2 },
                    { 333, 34, 3 },
                    { 334, 34, 4 },
                    { 335, 34, 5 },
                    { 336, 34, 6 },
                    { 337, 34, 7 },
                    { 338, 34, 8 },
                    { 339, 34, 9 },
                    { 340, 34, 10 },
                    { 341, 35, 1 },
                    { 342, 35, 2 },
                    { 343, 35, 3 },
                    { 344, 35, 4 },
                    { 345, 35, 5 },
                    { 346, 35, 6 },
                    { 347, 35, 7 },
                    { 348, 35, 8 },
                    { 349, 35, 9 },
                    { 350, 35, 10 },
                    { 351, 36, 1 },
                    { 352, 36, 2 },
                    { 353, 36, 3 },
                    { 354, 36, 4 },
                    { 355, 36, 5 },
                    { 356, 36, 6 },
                    { 357, 36, 7 },
                    { 358, 36, 8 },
                    { 359, 36, 9 },
                    { 360, 36, 10 },
                    { 361, 37, 1 },
                    { 362, 37, 2 },
                    { 363, 37, 3 },
                    { 364, 37, 4 },
                    { 365, 37, 5 },
                    { 366, 37, 6 },
                    { 367, 37, 7 },
                    { 368, 37, 8 },
                    { 369, 37, 9 },
                    { 370, 37, 10 },
                    { 371, 38, 1 },
                    { 372, 38, 2 },
                    { 373, 38, 3 },
                    { 374, 38, 4 },
                    { 375, 38, 5 },
                    { 376, 38, 6 },
                    { 377, 38, 7 },
                    { 378, 38, 8 },
                    { 379, 38, 9 },
                    { 380, 38, 10 },
                    { 381, 39, 1 },
                    { 382, 39, 2 },
                    { 383, 39, 3 },
                    { 384, 39, 4 },
                    { 385, 39, 5 },
                    { 386, 39, 6 },
                    { 387, 39, 7 },
                    { 388, 39, 8 },
                    { 389, 39, 9 },
                    { 390, 39, 10 },
                    { 391, 40, 1 },
                    { 392, 40, 2 },
                    { 393, 40, 3 },
                    { 394, 40, 4 },
                    { 395, 40, 5 },
                    { 396, 40, 6 },
                    { 397, 40, 7 },
                    { 398, 40, 8 },
                    { 399, 40, 9 },
                    { 400, 40, 10 },
                    { 401, 41, 1 },
                    { 402, 41, 2 },
                    { 403, 41, 3 },
                    { 404, 41, 4 },
                    { 405, 41, 5 },
                    { 406, 41, 6 },
                    { 407, 41, 7 },
                    { 408, 41, 8 },
                    { 409, 41, 9 },
                    { 410, 41, 10 },
                    { 411, 42, 1 },
                    { 412, 42, 2 },
                    { 413, 42, 3 },
                    { 414, 42, 4 },
                    { 415, 42, 5 },
                    { 416, 42, 6 },
                    { 417, 42, 7 },
                    { 418, 42, 8 },
                    { 419, 42, 9 },
                    { 420, 42, 10 },
                    { 421, 43, 1 },
                    { 422, 43, 2 },
                    { 423, 43, 3 },
                    { 424, 43, 4 },
                    { 425, 43, 5 },
                    { 426, 43, 6 },
                    { 427, 43, 7 },
                    { 428, 43, 8 },
                    { 429, 43, 9 },
                    { 430, 43, 10 },
                    { 431, 44, 1 },
                    { 432, 44, 2 },
                    { 433, 44, 3 },
                    { 434, 44, 4 },
                    { 435, 44, 5 },
                    { 436, 44, 6 },
                    { 437, 44, 7 },
                    { 438, 44, 8 },
                    { 439, 44, 9 },
                    { 440, 44, 10 },
                    { 441, 45, 1 },
                    { 442, 45, 2 },
                    { 443, 45, 3 },
                    { 444, 45, 4 },
                    { 445, 45, 5 },
                    { 446, 45, 6 },
                    { 447, 45, 7 },
                    { 448, 45, 8 },
                    { 449, 45, 9 },
                    { 450, 45, 10 },
                    { 451, 46, 1 },
                    { 452, 46, 2 },
                    { 453, 46, 3 },
                    { 454, 46, 4 },
                    { 455, 46, 5 },
                    { 456, 46, 6 },
                    { 457, 46, 7 },
                    { 458, 46, 8 },
                    { 459, 46, 9 },
                    { 460, 46, 10 },
                    { 461, 47, 1 },
                    { 462, 47, 2 },
                    { 463, 47, 3 },
                    { 464, 47, 4 },
                    { 465, 47, 5 },
                    { 466, 47, 6 },
                    { 467, 47, 7 },
                    { 468, 47, 8 },
                    { 469, 47, 9 },
                    { 470, 47, 10 },
                    { 471, 48, 1 },
                    { 472, 48, 2 },
                    { 473, 48, 3 },
                    { 474, 48, 4 },
                    { 475, 48, 5 },
                    { 476, 48, 6 },
                    { 477, 48, 7 },
                    { 478, 48, 8 },
                    { 479, 48, 9 },
                    { 480, 48, 10 },
                    { 481, 49, 1 },
                    { 482, 49, 2 },
                    { 483, 49, 3 },
                    { 484, 49, 4 },
                    { 485, 49, 5 },
                    { 486, 49, 6 },
                    { 487, 49, 7 },
                    { 488, 49, 8 },
                    { 489, 49, 9 },
                    { 490, 49, 10 },
                    { 491, 50, 1 },
                    { 492, 50, 2 },
                    { 493, 50, 3 },
                    { 494, 50, 4 },
                    { 495, 50, 5 },
                    { 496, 50, 6 },
                    { 497, 50, 7 },
                    { 498, 50, 8 },
                    { 499, 50, 9 },
                    { 500, 50, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_EventId",
                table: "BankAccounts",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailSeats_SeatId",
                table: "DetailSeats",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCategorys_CategoryId",
                table: "EventCategorys",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCategorys_EventId",
                table: "EventCategorys",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSessions_EventId",
                table: "EventSessions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSessions_LocationId",
                table: "EventSessions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberSeats_DetailSeatId",
                table: "NumberSeats",
                column: "DetailSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizers_EventId",
                table: "Organizers",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MethodId",
                table: "Payments",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TicketId",
                table: "Payments",
                column: "TicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_EventSessionId",
                table: "Seats",
                column: "EventSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialEvents_EventId",
                table: "SpecialEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_NumberSeatId",
                table: "Tickets",
                column: "NumberSeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "EventCategorys");

            migrationBuilder.DropTable(
                name: "Organizers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "SpecialEvents");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Methods");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "NumberSeats");

            migrationBuilder.DropTable(
                name: "DetailSeats");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "EventSessions");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
