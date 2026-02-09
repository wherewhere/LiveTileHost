#nullable enable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LiveTileHost.Models
{
    public class NotificationContextFactory : IDesignTimeDbContextFactory<NotificationContext>
    {
        public static NotificationContext CreateDbContext(string dbPath)
        {
            DbContextOptionsBuilder<NotificationContext> optionsBuilder =
                new DbContextOptionsBuilder<NotificationContext>()
                    .UseSqlite($"Data Source={dbPath}");
            return new NotificationContext(optionsBuilder.Options);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        NotificationContext IDesignTimeDbContextFactory<NotificationContext>.CreateDbContext(string[] args) =>
            CreateDbContext("Assets/wpndatabase.db");
    }

    public sealed partial class NotificationContext(DbContextOptions<NotificationContext> options) : DbContext(options)
    {
        public DbSet<HandlerAssets> HandlerAssets { get; init; }
        public DbSet<HandlerSettings> HandlerSettings { get; init; }
        public DbSet<Metadata> Metadata { get; init; }
        public DbSet<Notification> Notification { get; init; }
        public DbSet<NotificationData> NotificationData { get; init; }
        public DbSet<NotificationHandler> NotificationHandler { get; init; }
        public DbSet<TimedNotification> TimedNotification { get; init; }
        public DbSet<TransientTable> TransientTable { get; init; }
        public DbSet<WNSPushChannel> WNSPushChannel { get; init; }
    }

    [PrimaryKey(nameof(AssetKey))]
    public sealed class HandlerAssets
    {
        public int? HandlerId { get; init; }
        [Key]
        public required string AssetKey { get; init; }
        public required string? AssetValue { get; init; }
    }

    [PrimaryKey(nameof(SettingKey))]
    public sealed class HandlerSettings
    {
        public int? HandlerId { get; init; }
        [Key]
        public required string SettingKey { get; init; }
        public int? Value { get; init; }
    }

    [PrimaryKey(nameof(Key))]
    public sealed class Metadata
    {
        [Key]
        public required string? Key { get; init; }
        public required long? Value { get; init; }
    }

    [PrimaryKey(nameof(Order))]
    public sealed class Notification
    {
        [Key]
        public int Order { get; init; }
        public int Id { get; init; }
        public int? HandlerId { get; init; }
        public Guid? ActivityId { get; init; }
        public required string? Type { get; init; }
        public byte[]? Payload { get; init; }
        public string? Tag { get; init; }
        public string? Group { get; init; }
        public long? ExpiryTime { get; init; }
        public long? ArrivalTime { get; init; }
        public long? DataVersion { get; init; }
        public required string PayloadType { get; init; }
        public long? BootId { get; init; }
        public bool? ExpiresOnReboot { get; init; }
    }

    [PrimaryKey(nameof(Key))]
    public sealed class NotificationData
    {
        public int? NotificationId { get; init; }
        [Key]
        public required string? Key { get; init; }
        public string? Value { get; init; }
    }

    [PrimaryKey(nameof(RecordId))]
    public sealed class NotificationHandler
    {
        [Key]
        public int? RecordId { get; init; }
        public required string PrimaryId { get; init; }
        public string? WNSId { get; init; }
        public string? HandlerType { get; init; }
        public long? WNFEventName { get; init; }
        public byte[]? SystemDataPropertySet { get; init; }
        public DateTime? CreatedTime { get; init; }
        public DateTime? ModifiedTime { get; init; }
        public string? ParentId { get; init; }
        public string? ContainerSid { get; init; }
    }

    [PrimaryKey(nameof(TimerEventId))]
    public sealed class TimedNotification
    {
        [Key]
        public Guid TimerEventId { get; init; }
        public Guid BBIWorkId { get; init; }
        public long WnfStateName { get; init; }
        public int? HandlerId { get; init; }
        public int NotificationType { get; init; }
        public string? Url { get; init; }
    }

    [Keyless]
    public sealed class TransientTable
    {
        public int? OfflineCacheCount { get; init; }
        public int? NotificationId { get; init; }
        public string? OfflineBundleId { get; init; }
        public bool? ServerCacheRollover { get; init; }
        public string? CrossDeviceMatchId { get; init; }
        public bool? SuppressPopup { get; init; }
        public bool? IsMirroringDisabled { get; init; }
        public Guid? RecurrenceId { get; init; }
        public Guid? MessageId { get; init; }
        public int Priority { get; init; }
        public string? CV { get; init; }
    }

    [Keyless]
    public sealed class WNSPushChannel
    {
        public string? Uri { get; init; }
        public long? ExpiryTime { get; init; }
        public long? CreatedTime { get; init; }
        public long? DeviceVersion { get; init; }
    }
}
