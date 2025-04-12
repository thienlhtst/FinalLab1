using FinalLab1.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalLab1.Data;

public static  class Dataseed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Music" },
            new Category { Id = 2, Name = "Technology" },
            new Category { Id = 3, Name = "Art" },
            new Category { Id = 4, Name = "Business" },
            new Category { Id = 5, Name = "Education" },
            new Category { Id = 6, Name = "Health" },
            new Category { Id = 7, Name = "Fashion" },
            new Category { Id = 8, Name = "Food" },
            new Category { Id = 9, Name = "Gaming" },
            new Category { Id = 10, Name = "Literature" },
            new Category { Id = 11, Name = "Film" },
            new Category { Id = 12, Name = "Photography" },
            new Category { Id = 13, Name = "Theater" },
            new Category { Id = 14, Name = "Dance" },
            new Category { Id = 15, Name = "Charity" },
            new Category { Id = 16, Name = "Networking" },
            new Category { Id = 17, Name = "Science" },
            new Category { Id = 18, Name = "Spirituality" },
            new Category { Id = 19, Name = "Sports" },
            new Category { Id = 20, Name = "Travel" }
        );
        modelBuilder.Entity<Location>().HasData(
    new Location { Id = 1, Name = "Hội trường A", Address = "123 Lê Lợi", City = "Hà Nội", Country = "Việt Nam" },
    new Location { Id = 2, Name = "Hội trường B", Address = "234 Trần Hưng Đạo", City = "Hà Nội", Country = "Việt Nam" },
    new Location { Id = 3, Name = "Trung tâm Triển lãm 1", Address = "12 Nguyễn Huệ", City = "TP.HCM", Country = "Việt Nam" },
    new Location { Id = 4, Name = "Sân vận động A", Address = "88 Lê Duẩn", City = "Đà Nẵng", Country = "Việt Nam" },
    new Location { Id = 5, Name = "Nhà hát lớn", Address = "1 Tràng Tiền", City = "Hà Nội", Country = "Việt Nam" },
    new Location { Id = 6, Name = "Phòng họp 101", Address = "456 Lý Thường Kiệt", City = "TP.HCM", Country = "Việt Nam" },
    new Location { Id = 7, Name = "Trường Đại học ABC", Address = "789 Nguyễn Trãi", City = "Cần Thơ", Country = "Việt Nam" },
    new Location { Id = 8, Name = "Sảnh tiệc Hoa Mai", Address = "22 Phạm Văn Đồng", City = "Huế", Country = "Việt Nam" },
    new Location { Id = 9, Name = "Khách sạn XYZ", Address = "999 Lạc Long Quân", City = "Đà Lạt", Country = "Việt Nam" },
    new Location { Id = 10, Name = "Hội trường lớn", Address = "111 Nguyễn Văn Linh", City = "Hải Phòng", Country = "Việt Nam" },
    new Location { Id = 11, Name = "Khu vui chơi ABC", Address = "98 Trường Chinh", City = "TP.HCM", Country = "Việt Nam" },
    new Location { Id = 12, Name = "Văn phòng Công ty DEF", Address = "33 Pasteur", City = "Hà Nội", Country = "Việt Nam" },
    new Location { Id = 13, Name = "Sảnh tổ chức 5 sao", Address = "12B Nguyễn Văn Cừ", City = "Bắc Ninh", Country = "Việt Nam" },
    new Location { Id = 14, Name = "Rạp chiếu phim Galaxy", Address = "15 Huỳnh Thúc Kháng", City = "TP.HCM", Country = "Việt Nam" },
    new Location { Id = 15, Name = "Nhà thiếu nhi", Address = "21 Lê Văn Sỹ", City = "Nha Trang", Country = "Việt Nam" },
    new Location { Id = 16, Name = "Trung tâm hội nghị Quốc Gia", Address = "01 Đại lộ Thăng Long", City = "Hà Nội", Country = "Việt Nam" },
    new Location { Id = 17, Name = "Bảo tàng Văn hóa", Address = "22 Nguyễn Hữu Cảnh", City = "Huế", Country = "Việt Nam" },
    new Location { Id = 18, Name = "Công viên 23/9", Address = "23/9 Nguyễn Thị Minh Khai", City = "TP.HCM", Country = "Việt Nam" },
    new Location { Id = 19, Name = "Trung tâm Hội chợ", Address = "66 Võ Văn Tần", City = "Bình Dương", Country = "Việt Nam" },
    new Location { Id = 20, Name = "Trường Quốc tế DEF", Address = "888 Quốc lộ 1A", City = "Biên Hòa", Country = "Việt Nam" }
);
        modelBuilder.Entity<Event>().HasData(
    new Event { Id = 1, UserId = 1, Name = "Music Show", Information = "Live concert", LogoImage = "logo1.png", BannerImage = "banner1.png", CountEvent = 100, MaxSeatsPerUser = 4, Status = EventStatus.Upcoming },
    new Event { Id = 2, UserId = 2, Name = "Tech Talk", Information = "Seminar on AI", LogoImage = "logo2.png", BannerImage = "banner2.png", CountEvent = 200, MaxSeatsPerUser = 2, Status = EventStatus.Upcoming },
    new Event { Id = 3, UserId = 3, Name = "Art Fair", Information = "Exhibition of paintings", LogoImage = "logo3.png", BannerImage = "banner3.png", CountEvent = 150, MaxSeatsPerUser = 3, Status = EventStatus.Ongoing },
    new Event { Id = 4, UserId = 4, Name = "Business Meetup", Information = "Startup networking", LogoImage = "logo4.png", BannerImage = "banner4.png", CountEvent = 300, MaxSeatsPerUser = 5, Status = EventStatus.Upcoming },
    new Event { Id = 5, UserId = 5, Name = "Book Fair", Information = "Books and authors", LogoImage = "logo5.png", BannerImage = "banner5.png", CountEvent = 120, MaxSeatsPerUser = 1, Status = EventStatus.Upcoming },
    new Event { Id = 6, UserId = 6, Name = "Health Expo", Information = "Healthcare and wellness", LogoImage = "logo6.png", BannerImage = "banner6.png", CountEvent = 250, MaxSeatsPerUser = 2, Status = EventStatus.Finished },
    new Event { Id = 7, UserId = 7, Name = "Coding Bootcamp", Information = "Learn full-stack dev", LogoImage = "logo7.png", BannerImage = "banner7.png", CountEvent = 80, MaxSeatsPerUser = 3, Status = EventStatus.Ongoing },
    new Event { Id = 8, UserId = 8, Name = "Dance Show", Information = "Traditional & modern", LogoImage = "logo8.png", BannerImage = "banner8.png", CountEvent = 90, MaxSeatsPerUser = 4, Status = EventStatus.Upcoming },
    new Event { Id = 9, UserId = 9, Name = "Science Fair", Information = "School science projects", LogoImage = "logo9.png", BannerImage = "banner9.png", CountEvent = 110, MaxSeatsPerUser = 5, Status = EventStatus.Ongoing },
    new Event { Id = 10, UserId = 10, Name = "Film Screening", Information = "Indie movie night", LogoImage = "logo10.png", BannerImage = "banner10.png", CountEvent = 140, MaxSeatsPerUser = 2, Status = EventStatus.Finished },
    new Event { Id = 11, UserId = 11, Name = "Photography Contest", Information = "Nature and people", LogoImage = "logo11.png", BannerImage = "banner11.png", CountEvent = 160, MaxSeatsPerUser = 3, Status = EventStatus.Upcoming },
    new Event { Id = 12, UserId = 12, Name = "Startup Pitch", Information = "Pitch your ideas", LogoImage = "logo12.png", BannerImage = "banner12.png", CountEvent = 180, MaxSeatsPerUser = 4, Status = EventStatus.Cancelled },
    new Event { Id = 13, UserId = 13, Name = "Food Fest", Information = "Local cuisine", LogoImage = "logo13.png", BannerImage = "banner13.png", CountEvent = 130, MaxSeatsPerUser = 5, Status = EventStatus.Upcoming },
    new Event { Id = 14, UserId = 14, Name = "Yoga Day", Information = "Mind & Body", LogoImage = "logo14.png", BannerImage = "banner14.png", CountEvent = 90, MaxSeatsPerUser = 2, Status = EventStatus.Finished },
    new Event { Id = 15, UserId = 15, Name = "Pet Show", Information = "Dogs, cats and more", LogoImage = "logo15.png", BannerImage = "banner15.png", CountEvent = 70, MaxSeatsPerUser = 3, Status = EventStatus.Ongoing },
    new Event { Id = 16, UserId = 16, Name = "Charity Run", Information = "Run for a cause", LogoImage = "logo16.png", BannerImage = "banner16.png", CountEvent = 200, MaxSeatsPerUser = 1, Status = EventStatus.Upcoming },
    new Event { Id = 17, UserId = 17, Name = "Eco Summit", Information = "Environment & Sustainability", LogoImage = "logo17.png", BannerImage = "banner17.png", CountEvent = 175, MaxSeatsPerUser = 2, Status = EventStatus.Upcoming },
    new Event { Id = 18, UserId = 18, Name = "Fashion Gala", Information = "Runway and designers", LogoImage = "logo18.png", BannerImage = "banner18.png", CountEvent = 160, MaxSeatsPerUser = 4, Status = EventStatus.Cancelled },
    new Event { Id = 19, UserId = 19, Name = "Hackathon", Information = "24-hour coding", LogoImage = "logo19.png", BannerImage = "banner19.png", CountEvent = 300, MaxSeatsPerUser = 2, Status = EventStatus.Ongoing },
    new Event { Id = 20, UserId = 20, Name = "Meditation Workshop", Information = "Calm and relax", LogoImage = "logo20.png", BannerImage = "banner20.png", CountEvent = 95, MaxSeatsPerUser = 1, Status = EventStatus.Finished }
);
   
        modelBuilder.Entity<User>().HasData(
    new User { Id = 1, Name = "Nguyễn Văn A", Email = "nguyenvana@example.com", Phone = "0900000001", Password = "password1", Birthday = new DateTimeOffset(1990, 1, 1, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male,Role = Role.Admin},
    new User { Id = 2, Name = "Trần Thị B", Email = "tranthib@example.com", Phone = "0900000002", Password = "password2", Birthday = new DateTimeOffset(1992, 2, 2, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Female },
    new User { Id = 3, Name = "Lê Văn C", Email = "levanc@example.com", Phone = "0900000003", Password = "password3", Birthday = new DateTimeOffset(1991, 3, 3, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male },
    new User { Id = 4, Name = "Phạm Thị D", Email = "phamthid@example.com", Phone = "0900000004", Password = "password4", Birthday = new DateTimeOffset(1989, 4, 4, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Female },
    new User { Id = 5, Name = "Hoàng Minh E", Email = "hoangmine@example.com", Phone = "0900000005", Password = "password5", Birthday = new DateTimeOffset(1995, 5, 5, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Other },
    new User { Id = 6, Name = "Đỗ Quốc F", Email = "doquocf@example.com", Phone = "0900000006", Password = "password6", Birthday = new DateTimeOffset(1990, 6, 6, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male },
    new User { Id = 7, Name = "Võ Thị G", Email = "vothig@example.com", Phone = "0900000007", Password = "password7", Birthday = new DateTimeOffset(1993, 7, 7, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Female },
    new User { Id = 8, Name = "Đặng Hữu H", Email = "danghuuh@example.com", Phone = "0900000008", Password = "password8", Birthday = new DateTimeOffset(1988, 8, 8, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male },
    new User { Id = 9, Name = "Ngô Thị I", Email = "ngothii@example.com", Phone = "0900000009", Password = "password9", Birthday = new DateTimeOffset(1994, 9, 9, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Female },
    new User { Id = 10, Name = "Cao Văn J", Email = "caovanj@example.com", Phone = "0900000010", Password = "password10", Birthday = new DateTimeOffset(1990,10,10, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male },
    new User { Id = 11, Name = "Lý Thị K", Email = "lythik@example.com", Phone = "0900000011", Password = "password11", Birthday = new DateTimeOffset(1992,11,11, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Female },
    new User { Id = 12, Name = "Châu Minh L", Email = "chauminhl@example.com", Phone = "0900000012", Password = "password12", Birthday = new DateTimeOffset(1991, 12,12, 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male },
    new User { Id = 13, Name = "Tô Thị M", Email = "tothim@example.com", Phone = "0900000013", Password = "password13", Birthday = new DateTimeOffset(1993,1,13 , 0, 0, 0,TimeSpan.Zero), Gender = Gender.Female },
    new User { Id = 14, Name = "Bùi Ngọc N", Email = "buingocn@example.com", Phone = "0900000014", Password = "password14", Birthday = new DateTimeOffset(1990,2,14 , 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male },
    new User { Id = 15, Name = "Nguyễn Hoài O", Email = "nguyenhoaio@example.com", Phone = "0900000015", Password = "password15", Birthday = new DateTimeOffset(1995,3,15 , 0, 0, 0,TimeSpan.Zero), Gender = Gender.Other },
    new User { Id = 16, Name = "Trịnh Quốc P", Email = "trinhquocp@example.com", Phone = "0900000016", Password = "password16", Birthday = new DateTimeOffset(1989,4,16 , 0, 0, 0,TimeSpan.Zero) , Gender = Gender.Male },
    new User { Id = 17, Name = "Mai Thị Q", Email = "maithiq@example.com", Phone = "0900000017", Password = "password17", Birthday = new DateTimeOffset(1992,5,17 , 0, 0, 0,TimeSpan.Zero), Gender = Gender.Female },
    new User { Id = 18, Name = "Lâm Anh R", Email = "lamanhr@example.com", Phone = "0900000018", Password = "password18", Birthday = new DateTimeOffset(1988,6,18 , 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male },
    new User { Id = 19, Name = "Hồ Minh S", Email = "hominhs@example.com", Phone = "0900000019", Password = "password19", Birthday = new DateTimeOffset(1994,7,19 , 0, 0, 0,TimeSpan.Zero), Gender = Gender.Male },
    new User { Id = 20, Name = "Dương Thảo T", Email = "duongthaot@example.com", Phone = "0900000020", Password = "password20", Birthday = new DateTimeOffset(1991,8,20 , 0, 0, 0,TimeSpan.Zero), Gender = Gender.Other }
);
        modelBuilder.Entity<EventCategory>().HasData(
            new EventCategory { Id = 1, EventId = 1, CategoryId = 1 },
            new EventCategory { Id = 2, EventId = 1, CategoryId = 2 },
            new EventCategory { Id = 3, EventId = 2, CategoryId = 1 },
            new EventCategory { Id = 4, EventId = 2, CategoryId = 3 },
            new EventCategory { Id = 5, EventId = 3, CategoryId = 2 },
            new EventCategory { Id = 6, EventId = 3, CategoryId = 4 },
            new EventCategory { Id = 7, EventId = 4, CategoryId = 1 },
            new EventCategory { Id = 8, EventId = 4, CategoryId = 5 },
            new EventCategory { Id = 9, EventId = 5, CategoryId = 2 },
            new EventCategory { Id = 10, EventId = 5, CategoryId = 3 },
            new EventCategory { Id = 11, EventId = 6, CategoryId = 1 },
            new EventCategory { Id = 12, EventId = 6, CategoryId = 4 },
            new EventCategory { Id = 13, EventId = 7, CategoryId = 5 },
            new EventCategory { Id = 14, EventId = 7, CategoryId = 2 },
            new EventCategory { Id = 15, EventId = 8, CategoryId = 3 },
            new EventCategory { Id = 16, EventId = 8, CategoryId = 4 },
            new EventCategory { Id = 17, EventId = 9, CategoryId = 1 },
            new EventCategory { Id = 18, EventId = 9, CategoryId = 5 },
            new EventCategory { Id = 19, EventId = 10, CategoryId = 2 },
            new EventCategory { Id = 20, EventId = 10, CategoryId = 3 }
        );
         modelBuilder.Entity<SpecialEvent>().HasData(
    new SpecialEvent { Id = 1, EventId = 1, Reason = "Kỷ niệm thành lập", FromDate = new DateTimeOffset(2025, 5, 1, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 5, 3, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 2, EventId = 2, Reason = "Chương trình giảm giá", FromDate = new DateTimeOffset(2025, 5, 5, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 5, 6, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 3, EventId = 3, Reason = "Ngày hội gia đình", FromDate = new DateTimeOffset(2025, 6, 1, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 6, 2, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 4, EventId = 4, Reason = "Tuần lễ văn hóa", FromDate = new DateTimeOffset(2025, 7, 10, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 7, 15, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 5, EventId = 5, Reason = "Sự kiện nội bộ", FromDate = new DateTimeOffset(2025, 8, 1, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 8, 2, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 6, EventId = 1, Reason = "Kỷ niệm 10 năm", FromDate = new DateTimeOffset(2025, 9, 5, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 9, 6, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 7, EventId = 2, Reason = "Summer Festival", FromDate = new DateTimeOffset(2025, 6, 20, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 6, 22, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 8, EventId = 3, Reason = "Special Guest Show", FromDate = new DateTimeOffset(2025, 7, 1, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 7, 1, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 9, EventId = 4, Reason = "Charity Program", FromDate = new DateTimeOffset(2025, 7, 25, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 7, 26, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 10, EventId = 5, Reason = "Tech Day", FromDate = new DateTimeOffset(2025, 10, 10, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 10, 11, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 11, EventId = 6, Reason = "Ngày hội nghệ thuật", FromDate = new DateTimeOffset(2025, 11, 1, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 11, 2, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 12, EventId = 7, Reason = "Triển lãm đặc biệt", FromDate = new DateTimeOffset(2025, 12, 5, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 12, 8, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 13, EventId = 8, Reason = "Cuộc thi cosplay", FromDate = new DateTimeOffset(2025, 5, 15, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 5, 15, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 14, EventId = 9, Reason = "Chào đón sinh viên", FromDate = new DateTimeOffset(2025, 9, 1, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 9, 3, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 15, EventId = 10, Reason = "Sự kiện cuối năm", FromDate = new DateTimeOffset(2025, 12, 28, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 12, 31, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 16, EventId = 6, Reason = "Live Performance", FromDate = new DateTimeOffset(2025, 8, 10, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 8, 10, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 17, EventId = 7, Reason = "Ngày hội âm nhạc", FromDate = new DateTimeOffset(2025, 6, 30, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 7, 2, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 18, EventId = 8, Reason = "Thử thách 24h", FromDate = new DateTimeOffset(2025, 7, 15, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 7, 16, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 19, EventId = 9, Reason = "Workshop sáng tạo", FromDate = new DateTimeOffset(2025, 8, 20, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2025, 8, 21, 0, 0, 0,TimeSpan.Zero) },
    new SpecialEvent { Id = 20, EventId = 10, Reason = "Gala Dinner", FromDate = new DateTimeOffset(2025, 12, 31, 0, 0, 0,TimeSpan.Zero), ToDate = new DateTimeOffset(2026, 1, 1, 0, 0, 0,TimeSpan.Zero) }
);

        modelBuilder.Entity<EventSession>().HasData(
    new EventSession { Id = 1, EventId = 1, LocationId = 1, FromTime = new DateTimeOffset(2026, 1, 5, 9, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 5, 12, 0, 0,TimeSpan.Zero), MaxSeat = 100, Type = EventSessionType.Offline },
    new EventSession { Id = 2, EventId = 1, LocationId = 2, FromTime = new DateTimeOffset(2026, 1, 6, 14, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 6, 16, 0, 0,TimeSpan.Zero), MaxSeat = 80, Type = EventSessionType.Online },
    new EventSession { Id = 3, EventId = 2, LocationId = 3, FromTime = new DateTimeOffset(2026, 1, 7, 10, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 7, 13, 0, 0,TimeSpan.Zero), MaxSeat = 120, Type = EventSessionType.Offline },
    new EventSession { Id = 4, EventId = 2, LocationId = 4, FromTime = new DateTimeOffset(2026, 1, 8, 9, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 8, 11, 0, 0,TimeSpan.Zero), MaxSeat = 90, Type = EventSessionType.Online },
    new EventSession { Id = 5, EventId = 3, LocationId = 5, FromTime = new DateTimeOffset(2026, 1, 9, 15, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 9, 18, 0, 0,TimeSpan.Zero), MaxSeat = 60, Type = EventSessionType.Offline },
    new EventSession { Id = 6, EventId = 3, LocationId = 1, FromTime = new DateTimeOffset(2026, 1, 10, 10, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 10, 13, 0, 0,TimeSpan.Zero), MaxSeat = 75, Type = EventSessionType.Online },
    new EventSession { Id = 7, EventId = 4, LocationId = 2, FromTime = new DateTimeOffset(2026, 1, 11, 14, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 11, 17, 0, 0,TimeSpan.Zero), MaxSeat = 50, Type =EventSessionType.Offline },
    new EventSession { Id = 8, EventId = 4, LocationId = 3, FromTime = new DateTimeOffset(2026, 1, 12, 16, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 12, 19, 0, 0,TimeSpan.Zero), MaxSeat = 100, Type = EventSessionType.Online },
    new EventSession { Id = 9, EventId = 5, LocationId = 4, FromTime = new DateTimeOffset(2026, 1, 13, 9, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 13, 11, 30, 0,TimeSpan.Zero), MaxSeat = 65, Type =EventSessionType.Offline },
    new EventSession { Id = 10, EventId = 5, LocationId = 5, FromTime = new DateTimeOffset(2026, 1, 14, 13, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 14, 16, 0, 0,TimeSpan.Zero), MaxSeat = 85, Type = EventSessionType.Online },

    new EventSession { Id = 11, EventId = 6, LocationId = 1, FromTime = new DateTimeOffset(2026, 1, 15, 10, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 15, 12, 0, 0,TimeSpan.Zero), MaxSeat = 70, Type =EventSessionType.Offline },
    new EventSession { Id = 12, EventId = 6, LocationId = 2, FromTime = new DateTimeOffset(2026, 1, 16, 14, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 16, 17, 0, 0,TimeSpan.Zero), MaxSeat = 90, Type =EventSessionType.Online },
    new EventSession { Id = 13, EventId = 7, LocationId = 3, FromTime = new DateTimeOffset(2026, 1, 17, 11, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 17, 13, 30, 0,TimeSpan.Zero), MaxSeat = 110, Type = EventSessionType.Offline },
    new EventSession { Id = 14, EventId = 7, LocationId = 4, FromTime = new DateTimeOffset(2026, 1, 18, 13, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 18, 15, 0, 0,TimeSpan.Zero), MaxSeat = 95, Type =EventSessionType.Online },
    new EventSession { Id = 15, EventId = 8, LocationId = 5, FromTime = new DateTimeOffset(2026, 1, 19, 9, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 19, 12, 0, 0,TimeSpan.Zero), MaxSeat = 100, Type = EventSessionType.Offline },
    new EventSession { Id = 16, EventId = 8, LocationId = 1, FromTime = new DateTimeOffset(2026, 1, 20, 14, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 20, 17, 0, 0,TimeSpan.Zero), MaxSeat = 85, Type = EventSessionType.Online },
    new EventSession { Id = 17, EventId = 9, LocationId = 2, FromTime = new DateTimeOffset(2026, 1, 21, 10, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 21, 12, 0, 0,TimeSpan.Zero), MaxSeat = 120, Type = EventSessionType.Offline },
    new EventSession { Id = 18, EventId = 9, LocationId = 3, FromTime = new DateTimeOffset(2026, 1, 22, 13, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 22, 16, 0, 0,TimeSpan.Zero), MaxSeat = 60, Type = EventSessionType.Online },
    new EventSession { Id = 19, EventId = 10, LocationId = 4, FromTime = new DateTimeOffset(2026, 1, 23, 15, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 23, 18, 0, 0,TimeSpan.Zero), MaxSeat = 100, Type = EventSessionType.Offline },
    new EventSession { Id = 20, EventId = 10, LocationId = 5, FromTime = new DateTimeOffset(2026, 1, 24, 9, 0, 0,TimeSpan.Zero), ToTime = new DateTimeOffset(2026, 1, 24, 12, 0, 0,TimeSpan.Zero), MaxSeat = 80, Type = EventSessionType.Online }
);
             modelBuilder.Entity<Seat>().HasData(
    new Seat { Id = 1, EventSessionId = 1, Name = "A1", CountSeat = "100", Type = "Standard", Information = "Standard seating", Opentobuy = new DateTime(2025, 12, 29), Price = 29.99m },
    new Seat { Id = 2, EventSessionId = 1, Name = "A2", CountSeat = "50", Type = "VIP", Information = "Front row seats", Opentobuy = new DateTime(2025, 12, 29), Price = 59.99m },
    new Seat { Id = 3, EventSessionId = 2, Name = "B1", CountSeat = "120", Type = "Economy", Information = "Cheaper seating", Opentobuy = new DateTime(2025, 12, 30), Price = 19.99m },
    new Seat { Id = 4, EventSessionId = 2, Name = "B2", CountSeat = "60", Type = "VIP", Information = "Includes backstage access", Opentobuy = new DateTime(2025, 12, 30), Price = 69.99m },
    new Seat { Id = 5, EventSessionId = 3, Name = "C1", CountSeat = "80", Type = "Standard", Information = "Great view", Opentobuy = new DateTime(2025, 12, 31), Price = 39.99m },
    new Seat { Id = 6, EventSessionId = 3, Name = "C2", CountSeat = "90", Type = "Balcony", Information = "Upper level seating", Opentobuy = new DateTime(2025, 12, 31), Price = 24.99m },
    new Seat { Id = 7, EventSessionId = 4, Name = "D1", CountSeat = "150", Type = "Standard", Information = "Spacious seats", Opentobuy = new DateTime(2026, 1, 1), Price = 34.99m },
    new Seat { Id = 8, EventSessionId = 4, Name = "D2", CountSeat = "70", Type = "VIP", Information = "Free refreshments", Opentobuy = new DateTime(2026, 1, 1), Price = 79.99m },
    new Seat { Id = 9, EventSessionId = 5, Name = "E1", CountSeat = "200", Type = "Economy", Information = "Budget seating", Opentobuy = new DateTime(2026, 1, 2), Price = 14.99m },
    new Seat { Id = 10, EventSessionId = 5, Name = "E2", CountSeat = "40", Type = "VIP", Information = "Exclusive seats", Opentobuy = new DateTime(2026, 1, 2), Price = 89.99m },

    new Seat { Id = 11, EventSessionId = 6, Name = "F1", CountSeat = "110", Type = "Standard", Information = "Good view", Opentobuy = new DateTime(2026, 1, 3), Price = 32.50m },
    new Seat { Id = 12, EventSessionId = 6, Name = "F2", CountSeat = "75", Type = "Balcony", Information = "Top level", Opentobuy = new DateTime(2026, 1, 3), Price = 22.50m },
    new Seat { Id = 13, EventSessionId = 7, Name = "G1", CountSeat = "100", Type = "Standard", Information = "Normal seats", Opentobuy = new DateTime(2026, 1, 4), Price = 27.00m },
    new Seat { Id = 14, EventSessionId = 7, Name = "G2", CountSeat = "50", Type = "VIP", Information = "Luxury comfort", Opentobuy = new DateTime(2026, 1, 4), Price = 74.00m },
    new Seat { Id = 15, EventSessionId = 8, Name = "H1", CountSeat = "130", Type = "Economy", Information = "Affordable choice", Opentobuy = new DateTime(2026, 1, 5), Price = 18.00m },
    new Seat { Id = 16, EventSessionId = 8, Name = "H2", CountSeat = "90", Type = "VIP", Information = "Meet and greet included", Opentobuy = new DateTime(2026, 1, 5), Price = 99.99m },
    new Seat { Id = 17, EventSessionId = 9, Name = "I1", CountSeat = "85", Type = "Standard", Information = "Middle seats", Opentobuy = new DateTime(2026, 1, 6), Price = 29.00m },
    new Seat { Id = 18, EventSessionId = 9, Name = "I2", CountSeat = "45", Type = "Balcony", Information = "High view", Opentobuy = new DateTime(2026, 1, 6), Price = 26.00m },
    new Seat { Id = 19, EventSessionId = 10, Name = "J1", CountSeat = "95", Type = "Standard", Information = "General seating", Opentobuy = new DateTime(2026, 1, 7), Price = 30.00m },
    new Seat { Id = 20, EventSessionId = 10, Name = "J2", CountSeat = "65", Type = "VIP", Information = "Premium seating", Opentobuy = new DateTime(2026, 1, 7), Price = 85.00m }
);
        modelBuilder.Entity<DetailSeat>().HasData(
    // Seat Id 1 (Standard)
    new DetailSeat { Id = 1, SeatId = 1, Section = "A" },
    new DetailSeat { Id = 2, SeatId = 1, Section = "B" },
    new DetailSeat { Id = 3, SeatId = 1, Section = "C" },

    // Seat Id 2 (VIP)
    new DetailSeat { Id = 4, SeatId = 2, Section = "D" },
    new DetailSeat { Id = 5, SeatId = 2, Section = "E" },
    new DetailSeat { Id = 6, SeatId = 2, Section = "G" },

    // Seat Id 3 (Economy)
    new DetailSeat { Id = 7, SeatId = 3, Section = "X" },
    new DetailSeat { Id = 8, SeatId = 3, Section = "Y" },
    new DetailSeat { Id = 9, SeatId = 3, Section = "Z" },

    // Seat Id 4 (VIP)
    new DetailSeat { Id = 10, SeatId = 4, Section = "D" },
    new DetailSeat { Id = 11, SeatId = 4, Section = "E" },
    new DetailSeat { Id = 12, SeatId = 4, Section = "G" },

    // Seat Id 5 (Standard)
    new DetailSeat { Id = 13, SeatId = 5, Section = "A" },
    new DetailSeat { Id = 14, SeatId = 5, Section = "B" },
    new DetailSeat { Id = 15, SeatId = 5, Section = "C" },

    // Seat Id 6 (Balcony)
    new DetailSeat { Id = 16, SeatId = 6, Section = "L" },
    new DetailSeat { Id = 17, SeatId = 6, Section = "M" },
    new DetailSeat { Id = 18, SeatId = 6, Section = "N" },

    // Seat Id 7 (Standard)
    new DetailSeat { Id = 19, SeatId = 7, Section = "A" },
    new DetailSeat { Id = 20, SeatId = 7, Section = "B" },
    new DetailSeat { Id = 21, SeatId = 7, Section = "C" },

    // Seat Id 8 (Economy)
    new DetailSeat { Id = 22, SeatId = 8, Section = "X" },
    new DetailSeat { Id = 23, SeatId = 8, Section = "Y" },
    new DetailSeat { Id = 24, SeatId = 8, Section = "Z" },

    // Seat Id 9 (VIP)
    new DetailSeat { Id = 25, SeatId = 9, Section = "D" },
    new DetailSeat { Id = 26, SeatId = 9, Section = "E" },
    new DetailSeat { Id = 27, SeatId = 9, Section = "G" },

    // Seat Id 10 (Standard)
    new DetailSeat { Id = 28, SeatId = 10, Section = "A" },
    new DetailSeat { Id = 29, SeatId = 10, Section = "B" },
    new DetailSeat { Id = 30, SeatId = 10, Section = "C" },

    // Seat Id 11 (Balcony)
    new DetailSeat { Id = 31, SeatId = 11, Section = "L" },
    new DetailSeat { Id = 32, SeatId = 11, Section = "M" },
    new DetailSeat { Id = 33, SeatId = 11, Section = "N" },

    // Seat Id 12 (Economy)
    new DetailSeat { Id = 34, SeatId = 12, Section = "X" },
    new DetailSeat { Id = 35, SeatId = 12, Section = "Y" },
    new DetailSeat { Id = 36, SeatId = 12, Section = "Z" },

    // Seat Id 13 (VIP)
    new DetailSeat { Id = 37, SeatId = 13, Section = "D" },
    new DetailSeat { Id = 38, SeatId = 13, Section = "E" },
    new DetailSeat { Id = 39, SeatId = 13, Section = "G" },

    // Seat Id 14 (Standard)
    new DetailSeat { Id = 40, SeatId = 14, Section = "A" },
    new DetailSeat { Id = 41, SeatId = 14, Section = "B" },
    new DetailSeat { Id = 42, SeatId = 14, Section = "C" },

    // Seat Id 15 (Balcony)
    new DetailSeat { Id = 43, SeatId = 15, Section = "L" },
    new DetailSeat { Id = 44, SeatId = 15, Section = "M" },
    new DetailSeat { Id = 45, SeatId = 15, Section = "N" },

    // Seat Id 16 (VIP)
    new DetailSeat { Id = 46, SeatId = 16, Section = "D" },
    new DetailSeat { Id = 47, SeatId = 16, Section = "E" },
    new DetailSeat { Id = 48, SeatId = 16, Section = "G" },

    // Seat Id 17 (Standard)
    new DetailSeat { Id = 49, SeatId = 17, Section = "A" },
    new DetailSeat { Id = 50, SeatId = 17, Section = "B" }
);
        var numberSeats = new List<NumberSeat>();
        int id = 1;

        for (int detailSeatId = 1; detailSeatId <= 50; detailSeatId++)
        {
            for (int number = 1; number <= 10; number++)
            {
                numberSeats.Add(new NumberSeat
                {
                    Id = id++,
                    DetailSeatId = detailSeatId,
                    Number = number
                });
            }
        }

        modelBuilder.Entity<NumberSeat>().HasData(numberSeats);
        
        
    }
}   