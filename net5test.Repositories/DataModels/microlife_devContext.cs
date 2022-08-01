using Microsoft.EntityFrameworkCore;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class microlife_devContext : DbContext
    {
        public microlife_devContext()
        {
        }

        public microlife_devContext(DbContextOptions<microlife_devContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<ApiActionLog> ApiActionLogs { get; set; }
        public virtual DbSet<ApiMonitoringLog> ApiMonitoringLogs { get; set; }
        public virtual DbSet<DeleteHealthcareHistoryrecord> DeleteHealthcareHistoryrecords { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceConnLog> DeviceConnLogs { get; set; }
        public virtual DbSet<DeviceErrorLog> DeviceErrorLogs { get; set; }
        public virtual DbSet<DeviceMemberLink> DeviceMemberLinks { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<JurAction> JurActions { get; set; }
        public virtual DbSet<LogBloodpressure> LogBloodpressures { get; set; }
        public virtual DbSet<LogBloodpressure2018> LogBloodpressure2018s { get; set; }
        public virtual DbSet<LogBodytemperature> LogBodytemperatures { get; set; }
        public virtual DbSet<LogBodytemperatureList> LogBodytemperatureLists { get; set; }
        public virtual DbSet<LogOxymeter> LogOxymeters { get; set; }
        public virtual DbSet<LogWeightFat> LogWeightFats { get; set; }
        public virtual DbSet<MagLog> MagLogs { get; set; }
        public virtual DbSet<MailNotification> MailNotifications { get; set; }
        public virtual DbSet<MailRecord> MailRecords { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberActionLog> MemberActionLogs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuLang> MenuLangs { get; set; }
        public virtual DbSet<MenuRole> MenuRoles { get; set; }
        public virtual DbSet<MigrationBloodpressureLog> MigrationBloodpressureLogs { get; set; }
        public virtual DbSet<OauthAccessToken> OauthAccessTokens { get; set; }
        public virtual DbSet<OauthAdmin> OauthAdmins { get; set; }
        public virtual DbSet<OauthAuthorizationCode> OauthAuthorizationCodes { get; set; }
        public virtual DbSet<OauthClientsClass> OauthClientsClasses { get; set; }
        public virtual DbSet<OauthClientsKind> OauthClientsKinds { get; set; }
        public virtual DbSet<OauthRefreshToken> OauthRefreshTokens { get; set; }
        public virtual DbSet<PortableClient> PortableClients { get; set; }
        public virtual DbSet<PushActivity> PushActivities { get; set; }
        public virtual DbSet<PushList> PushLists { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SysCountry> SysCountries { get; set; }
        public virtual DbSet<SysCountryLang> SysCountryLangs { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=127.0.0.1;user id=root;password=Moon1104;database=microlife_dev;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PRIMARY");

                entity.ToTable("admin_user");

                entity.HasComment("後臺管理者列表");

                entity.Property(e => e.AdminId)
                    .HasColumnType("tinyint(3)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("admin_id");

                entity.Property(e => e.ActionList)
                    .IsRequired()
                    .HasColumnName("action_list")
                    .HasComment("管理者擁有的權限");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email")
                    .HasComment("帳號");

                entity.Property(e => e.IsSuspended)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_suspended")
                    .HasComment("0不停權,1停權");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_login")
                    .HasComment("最後登入時間");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasComment("密碼(使用CI的加密)");

                entity.Property(e => e.UserName)
                    .HasMaxLength(60)
                    .HasColumnName("user_name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("名稱");
            });

            modelBuilder.Entity<ApiActionLog>(entity =>
            {
                entity.HasKey(e => new { e.ApilogId, e.ApiId })
                    .HasName("PRIMARY");

                entity.ToTable("api_action_log");

                entity.HasComment("API使用統計");

                entity.Property(e => e.ApilogId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("apilog_id")
                    .HasComment("流水號");

                entity.Property(e => e.ApiId)
                    .HasColumnType("int(11)")
                    .HasColumnName("api_id")
                    .HasComment("API操作編號");

                entity.Property(e => e.LogTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("log_time")
                    .HasComment("建立操作紀錄的時間");
            });

            modelBuilder.Entity<ApiMonitoringLog>(entity =>
            {
                entity.ToTable("api_monitoring_log");

                entity.HasComment("API監控");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("流水號");

                entity.Property(e => e.ApiName)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("api_name")
                    .HasComment("api 名稱(數字代替)");

                entity.Property(e => e.CallTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("call_time")
                    .HasComment("呼叫API時間");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("create_time")
                    .HasComment("建立時間");

                entity.Property(e => e.RespTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("resp_time")
                    .HasComment("回應時間(ms)");

                entity.Property(e => e.StatusCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("status_code")
                    .HasComment("HTTP 狀態碼");
            });

            modelBuilder.Entity<DeleteHealthcareHistoryrecord>(entity =>
            {
                entity.ToTable("delete_healthcare_historyrecord");

                entity.HasComment("資料刪除紀錄");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("流水號");

                entity.Property(e => e.DeleteDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("delete_date")
                    .HasComment("刪除時間");

                entity.Property(e => e.DeviceType)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("device_type")
                    .HasComment("裝置種類");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasComment("設備序號");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員編號");

                entity.Property(e => e.RecordId)
                    .HasColumnType("int(11)")
                    .HasColumnName("record_id")
                    .HasComment("會員量測紀錄的id");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("device");

                entity.HasComment("量測裝置列表(血壓機、體脂機、額溫槍)");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.DeviceModel)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("device_model")
                    .HasDefaultValueSql("''''''")
                    .HasComment("設備型號");

                entity.Property(e => e.DeviceType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("device_type")
                    .HasComment("裝置種類【1】血壓機，【2】體重機，【3】額溫槍機 ");

                entity.Property(e => e.ErrorCode1)
                    .HasColumnType("int(11)")
                    .HasColumnName("error_code1")
                    .HasComment("裝置錯誤碼(API add_device)");

                entity.Property(e => e.ErrorCode2)
                    .HasColumnType("int(11)")
                    .HasColumnName("error_code2")
                    .HasComment("裝置錯誤碼(API add_device)");

                entity.Property(e => e.ErrorCode3)
                    .HasColumnType("int(11)")
                    .HasColumnName("error_code3")
                    .HasComment("裝置錯誤碼(API add_device)");

                entity.Property(e => e.ErrorCode4)
                    .HasColumnType("int(11)")
                    .HasColumnName("error_code4")
                    .HasComment("裝置錯誤碼(API add_device)");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否停用(0:啟用，1:停用)");

                entity.Property(e => e.LastTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_time")
                    .HasComment("最後觸發時間");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("(App回傳的值)設備序號");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("modify_date")
                    .HasComment("資料異動時間");
            });

            modelBuilder.Entity<DeviceConnLog>(entity =>
            {
                entity.ToTable("device_conn_log");

                entity.HasComment("量測裝置連線紀錄");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DeviceType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("device_type")
                    .HasDefaultValueSql("'1'")
                    .HasComment("裝置種類【1】血壓機，【2】體重機，【3】額溫槍機 ");

                entity.Property(e => e.LogTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("log_time")
                    .HasComment("建立時間");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("量測裝置的mac_address");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID");
            });

            modelBuilder.Entity<DeviceErrorLog>(entity =>
            {
                entity.ToTable("device_error_log");

                entity.HasComment("量測裝置錯誤(故障)列表");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DeviceType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("device_type")
                    .HasDefaultValueSql("'1'")
                    .HasComment("裝置種類【1】血壓機，【2】體重機，【3】額溫槍機 ");

                entity.Property(e => e.LogAction)
                    .HasColumnType("int(11)")
                    .HasColumnName("log_action")
                    .HasComment("錯誤碼(PHP有錯誤碼列表)");

                entity.Property(e => e.LogTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("log_time")
                    .HasComment("建立時間");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("量測裝置的mac_address");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID");
            });

            modelBuilder.Entity<DeviceMemberLink>(entity =>
            {
                entity.ToTable("device_member_link");

                entity.HasComment("會員、量測裝置之連結表(中間表)");

                entity.HasIndex(e => new { e.MemberId, e.MacAddress }, "tmp_key1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.DeviceType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("device_type")
                    .HasComment("裝置種類【1】血壓機，【2】體重機，【3】額溫槍機 ");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否解綁(刪除)");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("量測裝置的mac_address");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID");
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.ToTable("disease");

                entity.HasComment("疾病表");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("疾病名稱");
            });

            modelBuilder.Entity<JurAction>(entity =>
            {
                entity.HasKey(e => e.JurId)
                    .HasName("PRIMARY");

                entity.ToTable("jur_action");

                entity.HasComment("後臺管理者 權限列表");

                entity.Property(e => e.JurId)
                    .HasColumnType("tinyint(3)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("jur_id");

                entity.Property(e => e.ActionCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("action_code")
                    .HasDefaultValueSql("''''''")
                    .HasComment("權限參數名稱");

                entity.Property(e => e.ActionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("action_name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("權限項目名稱");

                entity.Property(e => e.Disable)
                    .HasColumnName("disable")
                    .HasComment("0啟用,1禁用");

                entity.Property(e => e.GroupFunction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("group_function")
                    .HasDefaultValueSql("''''''")
                    .HasComment("權限群組(已無效)");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("link")
                    .HasDefaultValueSql("''''''")
                    .HasComment("連結路徑");

                entity.Property(e => e.ParentId)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("parent_id")
                    .HasComment("【0】:代表主項，【>0】代表子項");

                entity.Property(e => e.SortOrder)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("sort_order")
                    .HasComment("排序值");
            });

            modelBuilder.Entity<LogBloodpressure>(entity =>
            {
                entity.ToTable("log_bloodpressure");

                entity.HasComment("會員血壓機 量測記錄");

                entity.HasIndex(e => new { e.MemberId, e.BpmId }, "name_key1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Afib)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("afib")
                    .HasComment("Atrial fibrillation，簡稱：AF 或A-fib");

                entity.Property(e => e.BpmId)
                    .HasColumnType("int(11)")
                    .HasColumnName("bpm_id")
                    .HasComment("血壓編號(APP傳至雲端)(key之2)");

                entity.Property(e => e.Cuffokr)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("cuffokr")
                    .HasDefaultValueSql("'2'")
                    .HasComment("臂帶鬆緊0:未綁緊，1:綁緊，2:未檢測，預設值為2");

                entity.Property(e => e.Dia)
                    .HasColumnType("int(11)")
                    .HasColumnName("dia")
                    .HasComment("舒張壓");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("血壓機的mac_address(00:00:00:00:00:00代表手動新增)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID(key之1)");

                entity.Property(e => e.Mode)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("mode")
                    .HasComment("是否開啟mam模式(0:正常模式，1:正常模式+AFIB偵測，2:mam模式，3:mam模式+AFIB偵測)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("modify_date")
                    .HasComment("資料異動時間");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("note")
                    .HasDefaultValueSql("''''''")
                    .HasComment("筆記內容");

                entity.Property(e => e.Pad)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("pad")
                    .HasComment("脈搏不規則偵測(0:無，1:有量測到)");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("photo")
                    .HasDefaultValueSql("''''''")
                    .HasComment("照片檔案名稱");

                entity.Property(e => e.Pul)
                    .HasColumnType("int(11)")
                    .HasColumnName("pul")
                    .HasComment("心律");

                entity.Property(e => e.Recording)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("recording")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音檔案名稱");

                entity.Property(e => e.RecordingTime)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("recording_time")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音時間長度，幾分幾秒(xx:xx)");

                entity.Property(e => e.Sys)
                    .HasColumnType("int(11)")
                    .HasColumnName("sys")
                    .HasComment("收縮壓");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateDateH)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("update_date_H")
                    .HasComment("24時區");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("user_id")
                    .HasComment("血壓機ID(APP紀錄值9數字+2英文)");
            });

            modelBuilder.Entity<LogBloodpressure2018>(entity =>
            {
                entity.ToTable("log_bloodpressure_2018");

                entity.HasComment("會員血壓機 量測記錄");

                entity.HasIndex(e => new { e.MemberId, e.BpmId }, "name_key1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Afib)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("afib")
                    .HasComment("Atrial fibrillation，簡稱：AF 或A-fib");

                entity.Property(e => e.BpmId)
                    .HasColumnType("int(11)")
                    .HasColumnName("bpm_id")
                    .HasComment("血壓編號(APP傳至雲端)(key之2)");

                entity.Property(e => e.Cuffokr)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("cuffokr")
                    .HasDefaultValueSql("'2'")
                    .HasComment("臂帶鬆緊0:未綁緊，1:綁緊，2:未檢測，預設值為2");

                entity.Property(e => e.Dia)
                    .HasColumnType("int(11)")
                    .HasColumnName("dia")
                    .HasComment("舒張壓");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("血壓機的mac_address(00:00:00:00:00:00代表手動新增)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID(key之1)");

                entity.Property(e => e.Mode)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("mode")
                    .HasComment("是否開啟mam模式(0:正常模式，1:正常模式+AFIB偵測，2:mam模式，3:mam模式+AFIB偵測)");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("note")
                    .HasDefaultValueSql("''''''")
                    .HasComment("筆記內容");

                entity.Property(e => e.Pad)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("pad")
                    .HasComment("脈搏不規則偵測(0:無，1:有量測到)");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("photo")
                    .HasDefaultValueSql("''''''")
                    .HasComment("照片檔案名稱");

                entity.Property(e => e.Pul)
                    .HasColumnType("int(11)")
                    .HasColumnName("pul")
                    .HasComment("心律");

                entity.Property(e => e.Recording)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("recording")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音檔案名稱");

                entity.Property(e => e.RecordingTime)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("recording_time")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音時間長度，幾分幾秒(xx:xx)");

                entity.Property(e => e.Sys)
                    .HasColumnType("int(11)")
                    .HasColumnName("sys")
                    .HasComment("收縮壓");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("update_date");

                entity.Property(e => e.UpdateDateH)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("update_date_H")
                    .HasComment("24時區");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("user_id")
                    .HasComment("血壓機ID(APP紀錄值9數字+2英文)");
            });

            modelBuilder.Entity<LogBodytemperature>(entity =>
            {
                entity.ToTable("log_bodytemperature");

                entity.HasComment("會員額溫槍 事件(分類用)");

                entity.HasIndex(e => new { e.MemberId, e.EventCode }, "name_key1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("event")
                    .HasDefaultValueSql("''''''")
                    .HasComment("量測事件名稱");

                entity.Property(e => e.EventCode)
                    .HasColumnType("int(11)")
                    .HasColumnName("event_code")
                    .HasComment("事件編號(APP傳至雲端)(key之2)");

                entity.Property(e => e.EventTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("event_time")
                    .HasComment("事件時間(APP傳至雲端)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID(key之1)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("modify_date")
                    .HasComment("資料異動時間");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnName("type")
                    .HasDefaultValueSql("''''''")
                    .HasComment("事件類別");
            });

            modelBuilder.Entity<LogBodytemperatureList>(entity =>
            {
                entity.ToTable("log_bodytemperature_list");

                entity.HasComment("會員額溫槍 量測記錄");

                entity.HasIndex(e => new { e.MemberId, e.BtId }, "name_key1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.BodyTemp)
                    .HasColumnName("body_temp")
                    .HasComment("體溫");

                entity.Property(e => e.BtId)
                    .HasColumnType("int(11)")
                    .HasColumnName("bt_id")
                    .HasComment("額溫槍ID，(APP傳送至雲端)(key之3)");

                entity.Property(e => e.EventCode)
                    .HasColumnType("int(11)")
                    .HasColumnName("event_code")
                    .HasComment("事件編號，(APP傳送至雲端)(key之2)");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("額溫槍的mac_address(00:00:00:00:00:00代表手動新增)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID(key之1)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("modify_date")
                    .HasComment("資料異動時間");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("note")
                    .HasDefaultValueSql("''''''")
                    .HasComment("筆記內容");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("photo")
                    .HasDefaultValueSql("''''''")
                    .HasComment("照片檔案名稱");

                entity.Property(e => e.Recording)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("recording")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音檔案名稱");

                entity.Property(e => e.RecordingTime)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("recording_time")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音時間長度，幾分幾秒(xx:xx)");

                entity.Property(e => e.RoomTemp)
                    .HasColumnName("room_temp")
                    .HasComment("室溫");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("update_date")
                    .HasComment("最後更新時間(APP傳至雲端)");

                entity.Property(e => e.UpdateDateH)
                    .HasColumnType("int(3)")
                    .HasColumnName("update_date_H")
                    .HasComment("最後更新時間(24時區)");
            });

            modelBuilder.Entity<LogOxymeter>(entity =>
            {
                entity.ToTable("log_oxymeter");

                entity.HasComment("會員血氧計 量測記錄");

                entity.HasIndex(e => new { e.MemberId, e.OxymeterId }, "name_key1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("血氧計的mac_address(00:00:00:00:00:00代表手動新增)");

                entity.Property(e => e.MeasurementLength)
                    .HasColumnType("int(11)")
                    .HasColumnName("measurement_length")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("量測時間長度(單位: Seconds)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID(key之1)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnName("modify_date")
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasComment("資料異動時間");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("note")
                    .HasDefaultValueSql("''''''")
                    .HasComment("筆記內容");

                entity.Property(e => e.OxymeterId)
                    .HasColumnType("int(11)")
                    .HasColumnName("oxymeter_id")
                    .HasComment("APP的血氧計ID(APP傳送至雲端)(key之2)");

                entity.Property(e => e.PerfusionIndex)
                    .HasColumnName("perfusion_index")
                    .HasComment("末端循環指數(單位: %)");

                entity.Property(e => e.PerfusionIndexRawData)
                    .HasColumnName("perfusion_index_raw_data")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("末端循環指數(歷程記錄)");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("photo")
                    .HasDefaultValueSql("''''''")
                    .HasComment("照片檔案名稱");

                entity.Property(e => e.PulseRate)
                    .HasColumnName("pulse_rate")
                    .HasComment("心率(單位: Beats Per Minute)");

                entity.Property(e => e.PulseRateRawData)
                    .HasColumnName("pulse_rate_raw_data")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("心率(歷程記錄)");

                entity.Property(e => e.Recording)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("recording")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音檔案名稱");

                entity.Property(e => e.RecordingTime)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("recording_time")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音時間長度，幾分幾秒(xx:xx)");

                entity.Property(e => e.Spo2)
                    .HasColumnName("spo2")
                    .HasComment("血氧濃度 (單位: %)");

                entity.Property(e => e.Spo2RawData)
                    .HasColumnName("spo2_raw_data")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("血氧濃度歷程記錄)");

                entity.Property(e => e.Spo2WaveformData)
                    .HasColumnName("spo2_waveform_data")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasComment("最後更新時間(APP傳至雲端)");

                entity.Property(e => e.UpdateDateH)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("update_date_H")
                    .HasComment("最後更新時間(APP傳至雲端)");
            });

            modelBuilder.Entity<LogWeightFat>(entity =>
            {
                entity.ToTable("log_weight_fat");

                entity.HasComment("會員體脂機 量測記錄");

                entity.HasIndex(e => new { e.MemberId, e.WeightId }, "name_key1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bmi)
                    .HasColumnName("bmi")
                    .HasComment("BMI值(APP傳送)");

                entity.Property(e => e.Bmr)
                    .HasColumnName("bmr")
                    .HasComment("基礎代謝率(APP傳至雲端)");

                entity.Property(e => e.BodyFat)
                    .HasColumnName("body_fat")
                    .HasComment("體脂肪(APP傳至雲端)");

                entity.Property(e => e.MacAddress)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("mac_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("體脂機的mac_address(00:00:00:00:00:00代表手動新增)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID(key之1)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("modify_date")
                    .HasComment("資料異動時間");

                entity.Property(e => e.Muscle)
                    .HasColumnName("muscle")
                    .HasComment("肌肉(APP傳至雲端)");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("note")
                    .HasDefaultValueSql("''''''")
                    .HasComment("筆記內容");

                entity.Property(e => e.OrganFat)
                    .HasColumnName("organ_fat")
                    .HasComment("內臟脂肪(APP傳至雲端)");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("photo")
                    .HasDefaultValueSql("''''''")
                    .HasComment("照片檔案名稱");

                entity.Property(e => e.Recording)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("recording")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音檔案名稱");

                entity.Property(e => e.RecordingTime)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("recording_time")
                    .HasDefaultValueSql("''''''")
                    .HasComment("錄音時間長度，幾分幾秒(xx:xx)");

                entity.Property(e => e.Skeleton)
                    .HasColumnName("skeleton")
                    .HasComment("骨質量(APP傳至雲端)");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("update_date")
                    .HasComment("最後更新時間(APP傳至雲端)");

                entity.Property(e => e.UpdateDateH)
                    .HasColumnType("int(3)")
                    .HasColumnName("update_date_H")
                    .HasComment("最後更新時間(24時區)");

                entity.Property(e => e.Water)
                    .HasColumnName("water")
                    .HasComment("體水分(APP傳至雲端)");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasComment("體重");

                entity.Property(e => e.WeightId)
                    .HasColumnType("int(11)")
                    .HasColumnName("weight_id")
                    .HasComment("APP的體脂機ID(APP傳送至雲端)(key之2)");
            });

            modelBuilder.Entity<MagLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PRIMARY");

                entity.ToTable("mag_log");

                entity.HasComment("後臺管理者操作紀錄");

                entity.Property(e => e.LogId)
                    .HasColumnType("int(11)")
                    .HasColumnName("log_id");

                entity.Property(e => e.AdminEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("admin_email")
                    .HasDefaultValueSql("''''''")
                    .HasComment("管理者帳號");

                entity.Property(e => e.AdminId)
                    .HasColumnType("int(11)")
                    .HasColumnName("admin_id")
                    .HasComment("管理者ID");

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("ip_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("使用的主機IP");

                entity.Property(e => e.LogInfo)
                    .HasMaxLength(1500)
                    .HasColumnName("log_info")
                    .HasDefaultValueSql("''''''")
                    .HasComment("後臺操作內容");

                entity.Property(e => e.LogTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("log_time")
                    .HasComment("操作時間");
            });

            modelBuilder.Entity<MailNotification>(entity =>
            {
                entity.ToTable("mail_notification");

                entity.HasComment("APP的聯絡人資料表");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立日期");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .HasDefaultValueSql("''''''")
                    .HasComment("信箱");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("modify_date")
                    .HasComment("資料異動時間");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("名稱");
            });

            modelBuilder.Entity<MailRecord>(entity =>
            {
                entity.ToTable("mail_record");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("流水號");

                entity.Property(e => e.Bcc)
                    .HasColumnName("bcc")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("密件副本");

                entity.Property(e => e.Body)
                    .HasColumnName("body")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("信件內容");

                entity.Property(e => e.Cc)
                    .HasColumnName("cc")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("副本");

                entity.Property(e => e.Createdate)
                    .HasColumnName("createdate")
                    .HasComment("建立日期");

                entity.Property(e => e.From)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("from")
                    .HasComment("寄信者");

                entity.Property(e => e.Senddate)
                    .HasColumnName("senddate")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("寄送日期");

                entity.Property(e => e.Subject)
                    .HasMaxLength(500)
                    .HasColumnName("subject")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("信件標題");

                entity.Property(e => e.To)
                    .HasMaxLength(250)
                    .HasColumnName("to")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("收信者");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.HasComment("會員資料表");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立日期");

                entity.Property(e => e.Birthday)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("birthday")
                    .HasComment("生日");

                entity.Property(e => e.Bmi)
                    .HasColumnName("bmi")
                    .HasDefaultValueSql("'23'")
                    .HasComment("目標BMI(身高體重指數)");

                entity.Property(e => e.BmiActivity)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("bmi_activity")
                    .HasComment("是否開啟目標BMI");

                entity.Property(e => e.BodyFat)
                    .HasColumnName("body_fat")
                    .HasDefaultValueSql("'20'")
                    .HasComment("目標體脂");

                entity.Property(e => e.BodyFatActivity)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("body_fat_activity")
                    .HasComment("目標體脂0=OFF,1=ON");

                entity.Property(e => e.BpMeasurementArm)
                    .HasColumnType("int(11)")
                    .HasColumnName("bp_measurement_arm")
                    .HasComment("是否開啟臂的測量");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("city")
                    .HasDefaultValueSql("''''''")
                    .HasComment("城市");

                entity.Property(e => e.Conditions)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("conditions")
                    .HasDefaultValueSql("''''''")
                    .HasComment("疾病");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("country_id")
                    .HasComment("國家編號(sys_country的ID)");

                entity.Property(e => e.CuffSize).HasColumnName("cuff_size");

                entity.Property(e => e.DateFormat)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("date_format");

                entity.Property(e => e.Dia)
                    .HasColumnName("dia")
                    .HasComment("舒張壓");

                entity.Property(e => e.DiaActivity)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("dia_activity")
                    .HasComment("目標舒張壓0=OFF,1=ON");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("email")
                    .HasComment("帳號&信箱");

                entity.Property(e => e.Gender)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("gender")
                    .HasComment("性別：0女 1男");

                entity.Property(e => e.GoalWeight)
                    .HasColumnName("goal_weight")
                    .HasDefaultValueSql("'75'")
                    .HasComment("目標體重");

                entity.Property(e => e.Height)
                    .HasColumnName("height")
                    .HasComment("身高");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'1'")
                    .HasComment("狀態 0:啟用 1:停用");

                entity.Property(e => e.IsReg)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_reg")
                    .HasDefaultValueSql("'1'")
                    .HasComment("狀態 0:未認證 1:已認證");

                entity.Property(e => e.LastTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_time")
                    .HasComment("上次登入時間");

                entity.Property(e => e.MeasuringTime)
                    .HasColumnType("int(11)")
                    .HasColumnName("measuringTime")
                    .HasComment("血氧量測時間");

                entity.Property(e => e.MeasuringTimeActivity)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("measuringTime_activity")
                    .HasComment("血氧量測時間開啟");

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("member_name")
                    .HasComment("姓名");

                entity.Property(e => e.Oxygen)
                    .HasColumnType("int(11)")
                    .HasColumnName("oxygen")
                    .HasComment("血氧警戒值");

                entity.Property(e => e.OxygenActivity)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("oxygen_activity")
                    .HasComment("血氧警戒值開啟");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("password")
                    .HasComment("密碼");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("photo")
                    .HasDefaultValueSql("''''''")
                    .HasComment("大頭照URL");

                entity.Property(e => e.RegCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("reg_code")
                    .HasComment("註冊認證碼");

                entity.Property(e => e.RegThrData)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("reg_thr_data")
                    .HasDefaultValueSql("''''''")
                    .HasComment("第三方註冊資料(例如：fb_id、google_id)");

                entity.Property(e => e.RegisterType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("register_type")
                    .HasComment("註冊方式，0:本地註冊 1:第三方註冊");

                entity.Property(e => e.ResetpwdCode)
                    .HasMaxLength(200)
                    .HasColumnName("resetpwd_code")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("重設密碼代碼");

                entity.Property(e => e.ResetpwdDate)
                    .HasColumnName("resetpwd_date")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("成設密碼時間");

                entity.Property(e => e.Resistance)
                    .HasColumnType("int(11)")
                    .HasColumnName("resistance")
                    .HasComment("電阻值");

                entity.Property(e => e.Sys)
                    .HasColumnName("sys")
                    .HasComment("收縮壓");

                entity.Property(e => e.SysActivity)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("sys_activity")
                    .HasComment("目標收縮壓0=OFF,1=ON");

                entity.Property(e => e.SysUnit)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("sys_unit")
                    .HasComment("目標收縮壓單位");

                entity.Property(e => e.Threshold)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("threshold")
                    .HasDefaultValueSql("'1'")
                    .HasComment("正常值0=OFF,1=ON");

                entity.Property(e => e.UnitType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("unit_type")
                    .HasComment("0:公制 1:英制(身高體重)");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasComment("體重");

                entity.Property(e => e.WeightActivity)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("weight_activity")
                    .HasComment("目標體重0=OFF,1=ON");
            });

            modelBuilder.Entity<MemberActionLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PRIMARY");

                entity.ToTable("member_action_log");

                entity.HasComment("會員操作紀錄(APP操作紀錄)");

                entity.Property(e => e.LogId)
                    .HasColumnType("int(11)")
                    .HasColumnName("log_id");

                entity.Property(e => e.DeviceType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("device_type")
                    .HasComment("設備類型，【1】血壓機，【2】體重機，【3】額溫槍機");

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("ip_address")
                    .HasDefaultValueSql("''''''")
                    .HasComment("會員ID(操作APP時的IP)");

                entity.Property(e => e.LogAction)
                    .HasColumnType("int(11)")
                    .HasColumnName("log_action")
                    .HasComment("會員操作編號");

                entity.Property(e => e.LogTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("log_time")
                    .HasComment("建立操作紀錄的時間");

                entity.Property(e => e.LogTimeH)
                    .HasColumnType("int(3)")
                    .HasColumnName("log_time_H")
                    .HasComment("後臺統計用，12時區(例：0-2  2-4)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id)
                    .HasColumnType("tinyint(3)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("action")
                    .HasDefaultValueSql("''''''")
                    .HasComment("動作名稱");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("area")
                    .HasDefaultValueSql("''''''")
                    .HasComment("區域名稱");

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("controller")
                    .HasDefaultValueSql("''''''")
                    .HasComment("挓制器名稱");

                entity.Property(e => e.Css)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("css")
                    .HasDefaultValueSql("''''''")
                    .HasComment("css樣式");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("選單名稱");

                entity.Property(e => e.Order)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("order")
                    .HasComment("排序值");

                entity.Property(e => e.Parentid)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("parentid")
                    .HasComment("【0】:代表主項，【>0】代表子項");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0啟用,1禁用");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("url")
                    .HasDefaultValueSql("''''''")
                    .HasComment("網址");
            });

            modelBuilder.Entity<MenuLang>(entity =>
            {
                entity.ToTable("menu_lang");

                entity.HasComment("選單語系");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("流水號");

                entity.Property(e => e.Lang)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lang")
                    .HasComment("語系");

                entity.Property(e => e.MenuId)
                    .HasColumnType("int(11)")
                    .HasColumnName("menu_id")
                    .HasComment("選單id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("選單名稱");
            });

            modelBuilder.Entity<MenuRole>(entity =>
            {
                entity.ToTable("menu_role");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.MenuId)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("menu_id")
                    .HasComment("選單id");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("role_id")
                    .HasComment("角色id");
            });

            modelBuilder.Entity<MigrationBloodpressureLog>(entity =>
            {
                entity.ToTable("migration_bloodpressure_log");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("流水號");

                entity.Property(e => e.Createdate)
                    .HasColumnName("createdate")
                    .HasComment("轉移時間點");

                entity.Property(e => e.ImportId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("import_id")
                    .HasComment("匯入編號");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員數");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status")
                    .HasComment("轉移結果(0:成功/1:移轉中/2:失敗)");

                entity.Property(e => e.Transfercount)
                    .HasColumnType("int(11)")
                    .HasColumnName("transfercount")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("轉移資料數量");
            });

            modelBuilder.Entity<OauthAccessToken>(entity =>
            {
                entity.HasKey(e => e.AccessToken)
                    .HasName("PRIMARY");

                entity.ToTable("oauth_access_tokens");

                entity.HasComment("Oauth2的access_token管理");

                entity.Property(e => e.AccessToken)
                    .HasMaxLength(130)
                    .HasColumnName("access_token")
                    .HasComment("獲取資源的access_token");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("client_id")
                    .HasComment("開發者Appid");

                entity.Property(e => e.Expires)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("expires")
                    .HasComment("認證的時間date(\"Y-m-d H:i:s\")");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("用戶id");

                entity.Property(e => e.Providerticket)
                    .HasColumnName("providerticket")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Scope)
                    .IsRequired()
                    .HasMaxLength(800)
                    .HasColumnName("scope")
                    .HasDefaultValueSql("''''''")
                    .HasComment("權限容器");
            });

            modelBuilder.Entity<OauthAdmin>(entity =>
            {
                entity.ToTable("oauth_admin");

                entity.HasComment("Oauth2公司管理");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立日期");

                entity.Property(e => e.BusinessPhone)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("business_phone")
                    .HasDefaultValueSql("''''''")
                    .HasComment("商務電話");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("company_name")
                    .HasComment("公司名稱");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("email")
                    .HasDefaultValueSql("''''''")
                    .HasComment("聯絡信箱");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("聯絡人(名)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("聯絡人(姓)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("product_name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("產品名稱");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(350)
                    .HasColumnName("purpose")
                    .HasDefaultValueSql("''''''")
                    .HasComment("合作目的(內容)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title")
                    .HasDefaultValueSql("''''''")
                    .HasComment("聯絡人(職稱)");
            });

            modelBuilder.Entity<OauthAuthorizationCode>(entity =>
            {
                entity.HasKey(e => e.AuthorizationCode)
                    .HasName("PRIMARY");

                entity.ToTable("oauth_authorization_codes");

                entity.HasComment("Oauth2會員授權碼管理");

                entity.Property(e => e.AuthorizationCode)
                    .HasMaxLength(130)
                    .HasColumnName("authorization_code")
                    .HasComment("通過Authorization 獲取到的code，用於獲取access_token");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("client_id")
                    .HasComment("開發者Appid");

                entity.Property(e => e.Expires)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("expires")
                    .HasComment("認證的時間date(\"Y-m-d H:i:s\")");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("用戶id");

                entity.Property(e => e.Providerticket)
                    .HasColumnName("providerticket")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RedirectUri)
                    .IsRequired()
                    .HasMaxLength(800)
                    .HasColumnName("redirect_uri")
                    .HasDefaultValueSql("''''''")
                    .HasComment("認證後跳轉的url");

                entity.Property(e => e.Scope)
                    .IsRequired()
                    .HasMaxLength(800)
                    .HasColumnName("scope")
                    .HasDefaultValueSql("''''''")
                    .HasComment("權限容器");
            });

            modelBuilder.Entity<OauthClientsClass>(entity =>
            {
                entity.ToTable("oauth_clients_class");

                entity.HasComment("Oauth2專案管理");

                entity.HasIndex(e => e.ClientId, "client_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AccessTokenTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("access_token_time")
                    .HasComment("access_token有效期(秒)");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("client_id")
                    .HasDefaultValueSql("''''''")
                    .HasComment("開發者AppId");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("client_name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("專案(project)名稱");

                entity.Property(e => e.ClientSecret)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("client_secret")
                    .HasDefaultValueSql("''''''")
                    .HasComment("開發者AppSecret");

                entity.Property(e => e.GrantTypes)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("grant_types")
                    .HasDefaultValueSql("''''''")
                    .HasComment("認證的方式，refresh_token、authorization_code、authorization_access_token");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("狀態 0:啟用 1:停用");

                entity.Property(e => e.OauthAid)
                    .HasColumnType("int(11)")
                    .HasColumnName("oauth_aid")
                    .HasComment("開發者id");

                entity.Property(e => e.RefreshTokenTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("refresh_token_time")
                    .HasComment("refresh_token有效期(秒)");

                entity.Property(e => e.Scope)
                    .IsRequired()
                    .HasMaxLength(800)
                    .HasColumnName("scope")
                    .HasDefaultValueSql("''''''")
                    .HasComment("權限容器");
            });

            modelBuilder.Entity<OauthClientsKind>(entity =>
            {
                entity.ToTable("oauth_clients_kind");

                entity.HasComment("Oauth2串接方式管理");

                entity.HasIndex(e => new { e.OauthCcid, e.ClientType }, "oauth_project")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.AndroidBundleFun)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("android_bundle_fun")
                    .HasDefaultValueSql("''''''")
                    .HasComment("例如:com.xxx.xxx/aaa/aaa(函式名稱)");

                entity.Property(e => e.AndroidBundleId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("android_bundle_id")
                    .HasDefaultValueSql("''''''")
                    .HasComment("例如:com.xxx.xxx(套件名稱)");

                entity.Property(e => e.AndroidKey)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("android_key")
                    .HasDefaultValueSql("''''''")
                    .HasComment("[未確定]hash_key(FB)、簽署憑證的指紋(Google)");

                entity.Property(e => e.ClientType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("client_type")
                    .HasComment("0:web_page  1:android  2:ios");

                entity.Property(e => e.IosBundleId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("ios_bundle_id")
                    .HasDefaultValueSql("''''''")
                    .HasComment("例如:com.xxx.xxx(套件名稱)");

                entity.Property(e => e.IosUrlScheme)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("ios_url_scheme")
                    .HasDefaultValueSql("''''''")
                    .HasComment("例如：myappscheme  運行：myappscheme://aaaa");

                entity.Property(e => e.OauthAid)
                    .HasColumnType("int(11)")
                    .HasColumnName("oauth_aid")
                    .HasComment("開發者id");

                entity.Property(e => e.OauthCcid)
                    .HasColumnType("int(11)")
                    .HasColumnName("oauth_ccid")
                    .HasComment("開發者專案");

                entity.Property(e => e.RedirectUri)
                    .IsRequired()
                    .HasMaxLength(800)
                    .HasColumnName("redirect_uri")
                    .HasDefaultValueSql("''''''")
                    .HasComment("認證後跳轉的url");
            });

            modelBuilder.Entity<OauthRefreshToken>(entity =>
            {
                entity.HasKey(e => e.RefreshToken)
                    .HasName("PRIMARY");

                entity.ToTable("oauth_refresh_tokens");

                entity.HasComment("Oauth2的refresh_token列表");

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(130)
                    .HasColumnName("refresh_token")
                    .HasComment("更新access_token的token");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("client_id")
                    .HasComment("開發者AppId");

                entity.Property(e => e.Expires)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("expires")
                    .HasComment("有效期限");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("用戶id");

                entity.Property(e => e.Providerticket)
                    .HasColumnName("providerticket")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RedirectUri)
                    .IsRequired()
                    .HasMaxLength(800)
                    .HasColumnName("redirect_uri")
                    .HasDefaultValueSql("''''''")
                    .HasComment("認證後跳轉的url");

                entity.Property(e => e.Scope)
                    .IsRequired()
                    .HasMaxLength(800)
                    .HasColumnName("scope")
                    .HasDefaultValueSql("''''''")
                    .HasComment("權限容器");
            });

            modelBuilder.Entity<PortableClient>(entity =>
            {
                entity.ToTable("portable_client");

                entity.HasComment("手機資料表");

                entity.HasIndex(e => e.ApiKey, "api_key")
                    .IsUnique();

                entity.HasIndex(e => e.ClientUniqueId, "client_unique_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.ApiKey)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("api_key")
                    .HasComment("手機串接API的token");

                entity.Property(e => e.ClientUniqueId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("client_unique_id")
                    .HasComment("手機唯一碼(從APP傳送過來)");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(130)
                    .HasColumnName("company")
                    .HasDefaultValueSql("''''''")
                    .HasComment("手機公司");

                entity.Property(e => e.Gps)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("gps")
                    .HasDefaultValueSql("''''''")
                    .HasComment("GPS，由APP傳送至雲端");

                entity.Property(e => e.LastTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_time")
                    .HasComment("最後登入時間");

                entity.Property(e => e.MachineType)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("machine_type")
                    .HasDefaultValueSql("'''phone'''")
                    .HasComment("機器類型，平板(pad)，手機(phone)");

                entity.Property(e => e.MemberId)
                    .HasColumnType("int(11)")
                    .HasColumnName("member_id")
                    .HasComment("會員ID");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("model")
                    .HasDefaultValueSql("''''''")
                    .HasComment("設備型號");

                entity.Property(e => e.Os)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("os")
                    .HasDefaultValueSql("''''''")
                    .HasComment("手機系統(android、ios)");

                entity.Property(e => e.PushUniqueId)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("push_unique_id")
                    .HasDefaultValueSql("''''''")
                    .HasComment("推播專用碼");
            });

            modelBuilder.Entity<PushActivity>(entity =>
            {
                entity.ToTable("push_activity");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasComment("推播內容");

                entity.Property(e => e.Createdate)
                    .HasColumnName("createdate")
                    .HasComment("建立時間 ");

                entity.Property(e => e.Dontsendbeforedate)
                    .HasColumnName("dontsendbeforedate")
                    .HasComment("預計傳送時間");

                entity.Property(e => e.Failcount)
                    .HasColumnType("int(11)")
                    .HasColumnName("failcount")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("傳送失敗數目");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasComment("是否刪除");

                entity.Property(e => e.Pushname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("pushname")
                    .HasComment("推播活動標題");

                entity.Property(e => e.Pushtype)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("pushtype")
                    .HasComment("推播方式(1:App推播，2:Email推播)");

                entity.Property(e => e.Senddate)
                    .HasColumnName("senddate")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("傳送時間");

                entity.Property(e => e.Successcount)
                    .HasColumnType("int(11)")
                    .HasColumnName("successcount")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("傳送成功數目");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("title")
                    .HasComment("推播標題");

                entity.Property(e => e.ToDevicetype)
                    .HasColumnType("int(11)")
                    .HasColumnName("to_devicetype")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("推播的會員擁有裝置類型【0】所有裝置，【【1】血壓機，【2】體重機，【3】額溫槍機 ，【4】血氧計");

                entity.Property(e => e.ToEmail)
                    .HasMaxLength(100)
                    .HasColumnName("to_email")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("推播的會員帳號");

                entity.Property(e => e.ToRegion)
                    .HasColumnType("int(11)")
                    .HasColumnName("to_region")
                    .HasDefaultValueSql("'NULL'")
                    .HasComment("推播的會員區域");

                entity.Property(e => e.Updatedate)
                    .HasColumnName("updatedate")
                    .HasComment("更新時間");
            });

            modelBuilder.Entity<PushList>(entity =>
            {
                entity.ToTable("push_list");

                entity.HasComment("推播資訊列表");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("add_time")
                    .HasComment("建立時間");

                entity.Property(e => e.AdminId)
                    .HasColumnType("int(11)")
                    .HasColumnName("admin_id")
                    .HasComment("管理者ID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasComment("推播內容");

                entity.Property(e => e.DeviceType)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("device_type")
                    .HasDefaultValueSql("''''''")
                    .HasComment("裝置種類【1】血壓機，【2】體重機，【3】額溫槍機 ");

                entity.Property(e => e.EndTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("end_time")
                    .HasComment("訊息的結束時間");

                entity.Property(e => e.ErrorCount)
                    .HasColumnType("int(11)")
                    .HasColumnName("error_count")
                    .HasComment("推播失敗次數");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("0不刪除,1刪除");

                entity.Property(e => e.IsForever)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_forever")
                    .HasComment("是否永久顯示");

                entity.Property(e => e.IsPush)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_push")
                    .HasComment("是否立即推播，0:無 1:立即推播");

                entity.Property(e => e.LinkUrl)
                    .IsRequired()
                    .HasMaxLength(350)
                    .HasColumnName("link_url")
                    .HasDefaultValueSql("''''''")
                    .HasComment("推播的連結URL");

                entity.Property(e => e.MemberEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("member_email")
                    .HasDefaultValueSql("''''''")
                    .HasComment("會員email(藉此找到會員綁定的手機)");

                entity.Property(e => e.PushType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("push_type")
                    .HasComment("推播方式(1:App推播，2:Email推播)");

                entity.Property(e => e.StartTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("start_time")
                    .HasComment("訊息的開始時間");

                entity.Property(e => e.SuccessCount)
                    .HasColumnType("int(11)")
                    .HasColumnName("success_count")
                    .HasComment("推播成功次數");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("title")
                    .HasDefaultValueSql("''''''")
                    .HasComment("推播標題");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnType("tinyint(3)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''''''")
                    .HasComment("角色名稱");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("是否停用");
            });

            modelBuilder.Entity<SysCountry>(entity =>
            {
                entity.ToTable("sys_country");

                entity.HasComment("國家資料表");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ChtCountryname)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("cht_countryname")
                    .HasComment("國家名稱(中文)");

                entity.Property(e => e.Countryname)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("countryname")
                    .HasComment("國家名稱(大寫英文)");

                entity.Property(e => e.Iso)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("iso")
                    .IsFixedLength(true)
                    .HasComment("(ISO 3166)國家2位字母代碼");

                entity.Property(e => e.Iso3)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("iso3")
                    .IsFixedLength(true)
                    .HasComment("(ISO 3166)國家3位字母代碼");

                entity.Property(e => e.Numcode)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("numcode")
                    .HasComment("(ISO 3166)國家數字代碼");

                entity.Property(e => e.PrintableName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("printable_name")
                    .HasComment("國家名稱(英文)");

                entity.Property(e => e.Sortorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("sortorder")
                    .HasDefaultValueSql("'1000'")
                    .HasComment("排序值");
            });

            modelBuilder.Entity<SysCountryLang>(entity =>
            {
                entity.ToTable("sys_country_lang");

                entity.HasComment("國家語系");

                entity.HasIndex(e => new { e.CountryId, e.Lang }, "country_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("流水號");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("country_id")
                    .HasComment("國家id");

                entity.Property(e => e.Lang)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lang")
                    .HasComment("語系");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("國家");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.Property(e => e.Id)
                    .HasColumnType("tinyint(3)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("role_id")
                    .HasComment("角色id");

                entity.Property(e => e.UserId)
                    .HasColumnType("tinyint(3)")
                    .HasColumnName("user_id")
                    .HasComment("使用者id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
