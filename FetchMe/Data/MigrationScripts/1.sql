IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'3.1.0');

GO

CREATE TABLE [Photographers] (
    [Id] uniqueidentifier NOT NULL,
    [OwnerID] nvarchar(max) NULL,
    [Slug] nvarchar(150) NULL,
    [DisplayName] nvarchar(150) NULL,
    CONSTRAINT [PK_Photographers] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200625182352_PhotographerMigration', N'3.1.0');

GO

ALTER TABLE [Photographers] ADD [BannerImageId] uniqueidentifier NULL;

GO

ALTER TABLE [Photographers] ADD [CardExcerpt] nvarchar(280) NULL;

GO

ALTER TABLE [Photographers] ADD [Email] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [Facebook] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [FirstName] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [Instagram] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [IsInsured] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Photographers] ADD [LastName] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [LogoImageId] uniqueidentifier NULL;

GO

ALTER TABLE [Photographers] ADD [PhoneNumber] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [Pinterest] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [ProfileBio] nvarchar(3000) NULL;

GO

ALTER TABLE [Photographers] ADD [Tumblr] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [Twitter] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [WebSite] nvarchar(max) NULL;

GO

ALTER TABLE [Photographers] ADD [YearStarted] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

CREATE TABLE [Badges] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Caption] nvarchar(max) NULL,
    [BadgeClass] nvarchar(max) NULL,
    [Graphic] nvarchar(max) NULL,
    CONSTRAINT [PK_Badges] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [DateTimeSpans] (
    [Id] uniqueidentifier NOT NULL,
    [StartDateTime] datetime2 NOT NULL,
    [EndDateTime] datetime2 NOT NULL,
    CONSTRAINT [PK_DateTimeSpans] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [GigDateTimeTypes] (
    [Id] uniqueidentifier NOT NULL,
    [DisplayName] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_GigDateTimeTypes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [GigLocationTypes] (
    [Id] uniqueidentifier NOT NULL,
    [DisplayName] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_GigLocationTypes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [GigStatuses] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Ordinal] int NOT NULL,
    CONSTRAINT [PK_GigStatuses] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [GigTags] (
    [Id] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_GigTags] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Locations] (
    [Id] nvarchar(450) NOT NULL,
    [FormattedAddress] nvarchar(max) NULL,
    [AdministrativeAreaLevel1] nvarchar(max) NULL,
    [AdministrativeAreaLevel2] nvarchar(max) NULL,
    [AdministrativeAreaLevel3] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [Locality] nvarchar(max) NULL,
    [Neighborhood] nvarchar(max) NULL,
    [Political] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [Route] nvarchar(max) NULL,
    [StreetNumber] nvarchar(max) NULL,
    [Lat] decimal(9, 6) NULL,
    [Lng] decimal(9, 6) NULL,
    [Serviceable] bit NOT NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Messages] (
    [Id] uniqueidentifier NOT NULL,
    [Body] nvarchar(max) NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [PhotographerLikes] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(max) NULL,
    [PhotographerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PhotographerLikes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PhotographerLikes_Photographers_PhotographerId] FOREIGN KEY ([PhotographerId]) REFERENCES [Photographers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [PhotographTag] (
    [Id] nvarchar(450) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PhotographTag] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [PhotographTags] (
    [Id] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_PhotographTags] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [PhotographerBadges] (
    [BadgeId] uniqueidentifier NOT NULL,
    [PhotographerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PhotographerBadges] PRIMARY KEY ([PhotographerId], [BadgeId]),
    CONSTRAINT [FK_PhotographerBadges_Badges_BadgeId] FOREIGN KEY ([BadgeId]) REFERENCES [Badges] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PhotographerBadges_Photographers_PhotographerId] FOREIGN KEY ([PhotographerId]) REFERENCES [Photographers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Gig] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [LocationTypeId] uniqueidentifier NOT NULL,
    [MinBudget] decimal(10, 2) NOT NULL,
    [MaxBudget] decimal(10, 2) NOT NULL,
    [GigDateTimeTypeId] uniqueidentifier NOT NULL,
    [GigStatusId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Gig] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Gig_GigDateTimeTypes_GigDateTimeTypeId] FOREIGN KEY ([GigDateTimeTypeId]) REFERENCES [GigDateTimeTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Gig_GigStatuses_GigStatusId] FOREIGN KEY ([GigStatusId]) REFERENCES [GigStatuses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Gig_GigLocationTypes_LocationTypeId] FOREIGN KEY ([LocationTypeId]) REFERENCES [GigLocationTypes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [PhotographerLocations] (
    [PhotographerId] uniqueidentifier NOT NULL,
    [LocationId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_PhotographerLocations] PRIMARY KEY ([PhotographerId], [LocationId]),
    CONSTRAINT [FK_PhotographerLocations_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PhotographerLocations_Photographers_PhotographerId] FOREIGN KEY ([PhotographerId]) REFERENCES [Photographers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Photographs] (
    [Id] uniqueidentifier NOT NULL,
    [ImageTitle] nvarchar(max) NULL,
    [ImageDescription] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [HideFromGallery] bit NOT NULL,
    [PhotographerId] uniqueidentifier NOT NULL,
    [CameraBodyValue] nvarchar(450) NULL,
    [ExposureTimeValue] nvarchar(450) NULL,
    [FStopValue] nvarchar(450) NULL,
    [ISOValue] nvarchar(450) NULL,
    [LightSourceValue] nvarchar(450) NULL,
    [LensValue] nvarchar(450) NULL,
    [FocalLengthValue] nvarchar(450) NULL,
    [LocationId] nvarchar(450) NULL,
    CONSTRAINT [PK_Photographs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Photographs_PhotographTag_CameraBodyValue] FOREIGN KEY ([CameraBodyValue]) REFERENCES [PhotographTag] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Photographs_PhotographTag_ExposureTimeValue] FOREIGN KEY ([ExposureTimeValue]) REFERENCES [PhotographTag] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Photographs_PhotographTag_FStopValue] FOREIGN KEY ([FStopValue]) REFERENCES [PhotographTag] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Photographs_PhotographTag_FocalLengthValue] FOREIGN KEY ([FocalLengthValue]) REFERENCES [PhotographTag] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Photographs_PhotographTag_ISOValue] FOREIGN KEY ([ISOValue]) REFERENCES [PhotographTag] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Photographs_PhotographTag_LensValue] FOREIGN KEY ([LensValue]) REFERENCES [PhotographTag] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Photographs_PhotographTag_LightSourceValue] FOREIGN KEY ([LightSourceValue]) REFERENCES [PhotographTag] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Photographs_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Photographs_Photographers_PhotographerId] FOREIGN KEY ([PhotographerId]) REFERENCES [Photographers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Bids] (
    [Id] uniqueidentifier NOT NULL,
    [FeePercent] int NOT NULL,
    [PaymentType] int NOT NULL,
    [FullAmount] decimal(10, 2) NOT NULL,
    [FullAmountDue] datetime2 NOT NULL,
    [DepositAmount] decimal(10, 2) NOT NULL,
    [DepositAmountDue] datetime2 NOT NULL,
    [GigId] uniqueidentifier NOT NULL,
    [PhotographerId] uniqueidentifier NOT NULL,
    [MessageId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Bids] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bids_Gig_GigId] FOREIGN KEY ([GigId]) REFERENCES [Gig] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bids_Messages_MessageId] FOREIGN KEY ([MessageId]) REFERENCES [Messages] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bids_Photographers_PhotographerId] FOREIGN KEY ([PhotographerId]) REFERENCES [Photographers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [GigDateTimes] (
    [GigId] uniqueidentifier NOT NULL,
    [DateTimeSpanId] uniqueidentifier NOT NULL,
    [Id] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_GigDateTimes] PRIMARY KEY ([GigId], [DateTimeSpanId]),
    CONSTRAINT [FK_GigDateTimes_DateTimeSpans_DateTimeSpanId] FOREIGN KEY ([DateTimeSpanId]) REFERENCES [DateTimeSpans] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_GigDateTimes_Gig_GigId] FOREIGN KEY ([GigId]) REFERENCES [Gig] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [GigLocations] (
    [GigId] uniqueidentifier NOT NULL,
    [LocationId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_GigLocations] PRIMARY KEY ([GigId], [LocationId]),
    CONSTRAINT [FK_GigLocations_Gig_GigId] FOREIGN KEY ([GigId]) REFERENCES [Gig] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_GigLocations_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [GigTagGigs] (
    [GigId] uniqueidentifier NOT NULL,
    [TagId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_GigTagGigs] PRIMARY KEY ([GigId], [TagId]),
    CONSTRAINT [FK_GigTagGigs_Gig_GigId] FOREIGN KEY ([GigId]) REFERENCES [Gig] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_GigTagGigs_GigTags_TagId] FOREIGN KEY ([TagId]) REFERENCES [GigTags] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [PhotographLikes] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(max) NULL,
    [PhotographId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PhotographLikes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PhotographLikes_Photographs_PhotographId] FOREIGN KEY ([PhotographId]) REFERENCES [Photographs] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [PhotographTagsPhotographs] (
    [PhotographId] uniqueidentifier NOT NULL,
    [PhotographTagId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_PhotographTagsPhotographs] PRIMARY KEY ([PhotographId], [PhotographTagId]),
    CONSTRAINT [FK_PhotographTagsPhotographs_Photographs_PhotographId] FOREIGN KEY ([PhotographId]) REFERENCES [Photographs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PhotographTagsPhotographs_PhotographTags_PhotographTagId] FOREIGN KEY ([PhotographTagId]) REFERENCES [PhotographTags] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Photographers_BannerImageId] ON [Photographers] ([BannerImageId]);

GO

CREATE INDEX [IX_Photographers_LogoImageId] ON [Photographers] ([LogoImageId]);

GO

CREATE UNIQUE INDEX [IX_Photographers_Slug] ON [Photographers] ([Slug]) WHERE [Slug] IS NOT NULL;

GO

CREATE INDEX [IX_Bids_GigId] ON [Bids] ([GigId]);

GO

CREATE UNIQUE INDEX [IX_Bids_MessageId] ON [Bids] ([MessageId]);

GO

CREATE INDEX [IX_Bids_PhotographerId] ON [Bids] ([PhotographerId]);

GO

CREATE INDEX [IX_Gig_GigDateTimeTypeId] ON [Gig] ([GigDateTimeTypeId]);

GO

CREATE INDEX [IX_Gig_GigStatusId] ON [Gig] ([GigStatusId]);

GO

CREATE INDEX [IX_Gig_LocationTypeId] ON [Gig] ([LocationTypeId]);

GO

CREATE INDEX [IX_GigDateTimes_DateTimeSpanId] ON [GigDateTimes] ([DateTimeSpanId]);

GO

CREATE INDEX [IX_GigLocations_LocationId] ON [GigLocations] ([LocationId]);

GO

CREATE INDEX [IX_GigTagGigs_TagId] ON [GigTagGigs] ([TagId]);

GO

CREATE INDEX [IX_PhotographerBadges_BadgeId] ON [PhotographerBadges] ([BadgeId]);

GO

CREATE INDEX [IX_PhotographerLikes_PhotographerId] ON [PhotographerLikes] ([PhotographerId]);

GO

CREATE INDEX [IX_PhotographerLocations_LocationId] ON [PhotographerLocations] ([LocationId]);

GO

CREATE INDEX [IX_PhotographLikes_PhotographId] ON [PhotographLikes] ([PhotographId]);

GO

CREATE INDEX [IX_Photographs_CameraBodyValue] ON [Photographs] ([CameraBodyValue]);

GO

CREATE INDEX [IX_Photographs_ExposureTimeValue] ON [Photographs] ([ExposureTimeValue]);

GO

CREATE INDEX [IX_Photographs_FStopValue] ON [Photographs] ([FStopValue]);

GO

CREATE INDEX [IX_Photographs_FocalLengthValue] ON [Photographs] ([FocalLengthValue]);

GO

CREATE INDEX [IX_Photographs_ISOValue] ON [Photographs] ([ISOValue]);

GO

CREATE INDEX [IX_Photographs_LensValue] ON [Photographs] ([LensValue]);

GO

CREATE INDEX [IX_Photographs_LightSourceValue] ON [Photographs] ([LightSourceValue]);

GO

CREATE INDEX [IX_Photographs_LocationId] ON [Photographs] ([LocationId]);

GO

CREATE INDEX [IX_Photographs_PhotographerId] ON [Photographs] ([PhotographerId]);

GO

CREATE INDEX [IX_PhotographTagsPhotographs_PhotographTagId] ON [PhotographTagsPhotographs] ([PhotographTagId]);

GO

ALTER TABLE [Photographers] ADD CONSTRAINT [FK_Photographers_Photographs_BannerImageId] FOREIGN KEY ([BannerImageId]) REFERENCES [Photographs] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Photographers] ADD CONSTRAINT [FK_Photographers_Photographs_LogoImageId] FOREIGN KEY ([LogoImageId]) REFERENCES [Photographs] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200628040203_lotsamodels', N'3.1.0');

GO

ALTER TABLE [Photographs] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200630191731_createdDate', N'3.1.0');

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'f486ace3-7532-4df0-867a-128f07c55713', N'sajkdhjkhasfu', N'Photographer', N'PHOTOGRAPHER');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'4e52af71-092c-4f12-a2e5-feb18b274606', N'sajdfejkhasfu', N'Admin', N'ADMIN');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200702200348_roleseed', N'3.1.0');

GO

