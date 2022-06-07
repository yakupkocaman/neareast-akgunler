IF OBJECT_ID(N'[Core].[Migration]') IS NULL
BEGIN
    IF SCHEMA_ID(N'Core') IS NULL EXEC(N'CREATE SCHEMA [Core];');
    CREATE TABLE [Core].[Migration] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_Migration] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'Core') IS NULL EXEC(N'CREATE SCHEMA [Core];');

GO

IF SCHEMA_ID(N'Customer') IS NULL EXEC(N'CREATE SCHEMA [Customer];');

GO

IF SCHEMA_ID(N'Job') IS NULL EXEC(N'CREATE SCHEMA [Job];');

GO

IF SCHEMA_ID(N'Staff') IS NULL EXEC(N'CREATE SCHEMA [Staff];');

GO

IF SCHEMA_ID(N'Vehicle') IS NULL EXEC(N'CREATE SCHEMA [Vehicle];');

GO

CREATE TABLE [Core].[AddressType] (
    [AddressTypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [AddressGroup] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AddressType] PRIMARY KEY ([AddressTypeId])
);

GO

CREATE TABLE [Core].[Country] (
    [CountryId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([CountryId])
);

GO

CREATE TABLE [Core].[Currency] (
    [CurrencyId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [ShortName] nvarchar(max) NULL,
    [Sign] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY ([CurrencyId])
);

GO

CREATE TABLE [Core].[DocumentType] (
    [DocumentTypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [DocumentGroup] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_DocumentType] PRIMARY KEY ([DocumentTypeId])
);

GO

CREATE TABLE [Core].[Language] (
    [LanguageId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY ([LanguageId])
);

GO

CREATE TABLE [Core].[Log] (
    [LogId] int NOT NULL IDENTITY,
    [Category] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [Message] nvarchar(max) NULL,
    [Username] nvarchar(max) NULL,
    [IpAddress] nvarchar(max) NULL,
    [CreatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY ([LogId])
);

GO

CREATE TABLE [Core].[Nationality] (
    [NationalityId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Nationality] PRIMARY KEY ([NationalityId])
);

GO

CREATE TABLE [Customer].[Contact] (
    [ContactId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [Phone1] nvarchar(max) NULL,
    [Phone2] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Note] nvarchar(max) NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY ([ContactId])
);

GO

CREATE TABLE [Customer].[Group] (
    [GroupId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY ([GroupId])
);

GO

CREATE TABLE [Job].[AccountType] (
    [AccountTypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_AccountType] PRIMARY KEY ([AccountTypeId])
);

GO

CREATE TABLE [Job].[JobType] (
    [JobTypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_JobType] PRIMARY KEY ([JobTypeId])
);

GO

CREATE TABLE [Job].[WiseTransfer] (
    [WiseTransferId] int NOT NULL IDENTITY,
    [AccommodationId] int NOT NULL,
    [CasinoId] int NOT NULL,
    [CustomerId] int NOT NULL,
    [Classify] int NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Hotel] nvarchar(max) NULL,
    [CasinoInDate] datetime2 NOT NULL,
    [CasinoOutDate] datetime2 NOT NULL,
    [DestinationFrom] nvarchar(max) NULL,
    [DestinationTo] nvarchar(max) NULL,
    [ArrivalTime] time NOT NULL,
    [DepartureTime] time NOT NULL,
    [AirlinesFrom] nvarchar(max) NULL,
    [AirlinesTo] nvarchar(max) NULL,
    [TransferCarTypeIn] nvarchar(max) NULL,
    [TransferCarTypeOut] nvarchar(max) NULL,
    [CarCheckIn] tinyint NOT NULL,
    [CarCheckOut] tinyint NOT NULL,
    [GuestCheckIn] tinyint NOT NULL,
    [GuestCheckOut] tinyint NOT NULL,
    [CarCheckInNote] nvarchar(max) NULL,
    [CarCheckOutNote] nvarchar(max) NULL,
    [CheckInDeleted] bit NOT NULL,
    [CheckOutDeleted] bit NOT NULL,
    [Deleted] tinyint NOT NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_WiseTransfer] PRIMARY KEY ([WiseTransferId])
);

GO

CREATE TABLE [Staff].[Department] (
    [DepartmentId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([DepartmentId])
);

GO

CREATE TABLE [Staff].[DriversLicenseType] (
    [DriversLicenseTypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_DriversLicenseType] PRIMARY KEY ([DriversLicenseTypeId])
);

GO

CREATE TABLE [Vehicle].[Color] (
    [ColorId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Color] PRIMARY KEY ([ColorId])
);

GO

CREATE TABLE [Vehicle].[Fuel] (
    [FuelId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Fuel] PRIMARY KEY ([FuelId])
);

GO

CREATE TABLE [Vehicle].[Make] (
    [MakeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Make] PRIMARY KEY ([MakeId])
);

GO

CREATE TABLE [Vehicle].[Transmission] (
    [TransmissionId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Transmission] PRIMARY KEY ([TransmissionId])
);

GO

CREATE TABLE [Vehicle].[VehicleType] (
    [VehicleTypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_VehicleType] PRIMARY KEY ([VehicleTypeId])
);

GO

CREATE TABLE [Core].[Address] (
    [AddressId] int NOT NULL IDENTITY,
    [AddressTypeId] int NOT NULL,
    [AddressLine] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [Note] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([AddressId]),
    CONSTRAINT [FK_Address_AddressType_AddressTypeId] FOREIGN KEY ([AddressTypeId]) REFERENCES [Core].[AddressType] ([AddressTypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Core].[City] (
    [CityId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [CountryId] int NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY ([CityId]),
    CONSTRAINT [FK_City_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Core].[Country] ([CountryId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Core].[Document] (
    [DocumentId] int NOT NULL IDENTITY,
    [FileName] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [DocumentTypeId] int NULL,
    [Name] nvarchar(max) NULL,
    [Date] datetime2 NOT NULL,
    [Total] decimal(18, 2) NOT NULL,
    [CurrencyId] int NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Document] PRIMARY KEY ([DocumentId]),
    CONSTRAINT [FK_Document_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Core].[Currency] ([CurrencyId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Document_DocumentType_DocumentTypeId] FOREIGN KEY ([DocumentTypeId]) REFERENCES [Core].[DocumentType] ([DocumentTypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Customer].[Customer] (
    [CustomerId] int NOT NULL IDENTITY,
    [WiseId] int NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Classification] int NOT NULL,
    [IsActive] bit NOT NULL,
    [GroupId] int NOT NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerId]),
    CONSTRAINT [FK_Customer_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Customer].[Group] ([GroupId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Vehicle].[Vehicle] (
    [VehicleId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [RegistrationNo] nvarchar(max) NULL,
    [MakeId] int NOT NULL,
    [Model] nvarchar(max) NULL,
    [ModelYear] int NOT NULL,
    [ColorId] int NOT NULL,
    [Mileage] decimal(18, 2) NOT NULL,
    [MileageDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [VehicleTypeId] int NOT NULL,
    [FuelId] int NOT NULL,
    [TransmissionId] int NOT NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Vehicle] PRIMARY KEY ([VehicleId]),
    CONSTRAINT [FK_Vehicle_Color_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [Vehicle].[Color] ([ColorId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Vehicle_Fuel_FuelId] FOREIGN KEY ([FuelId]) REFERENCES [Vehicle].[Fuel] ([FuelId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Vehicle_Make_MakeId] FOREIGN KEY ([MakeId]) REFERENCES [Vehicle].[Make] ([MakeId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Vehicle_Transmission_TransmissionId] FOREIGN KEY ([TransmissionId]) REFERENCES [Vehicle].[Transmission] ([TransmissionId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Vehicle_VehicleType_VehicleTypeId] FOREIGN KEY ([VehicleTypeId]) REFERENCES [Vehicle].[VehicleType] ([VehicleTypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Customer].[CustomerAddress] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [AddressId] int NOT NULL,
    CONSTRAINT [PK_CustomerAddress] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerAddress_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Core].[Address] ([AddressId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerAddress_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([CustomerId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Customer].[CustomerContact] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [ContactId] int NOT NULL,
    CONSTRAINT [PK_CustomerContact] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerContact_Contact_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Customer].[Contact] ([ContactId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerContact_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([CustomerId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Customer].[CustomerDocument] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [DocumentId] int NOT NULL,
    CONSTRAINT [PK_CustomerDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerDocument_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([CustomerId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerDocument_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Core].[Document] ([DocumentId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Job].[Job] (
    [JobId] int NOT NULL IDENTITY,
    [JobTypeId] int NOT NULL,
    [CustomerId] int NOT NULL,
    [StartAddressId] int NOT NULL,
    [AccommodationId] int NOT NULL,
    [TransferTypeId] int NOT NULL,
    [FinishAddressId] int NULL,
    [ExchangeRateUsd] decimal(18, 2) NOT NULL,
    [ExchangeRateEur] decimal(18, 2) NOT NULL,
    [StartDate] datetime2 NULL,
    [FinishDate] datetime2 NULL,
    [Note] nvarchar(max) NULL,
    [IsRf] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [HasUpdate] bit NOT NULL,
    [RowVersion] rowversion NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Job] PRIMARY KEY ([JobId]),
    CONSTRAINT [FK_Job_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer].[Customer] ([CustomerId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Job_Address_FinishAddressId] FOREIGN KEY ([FinishAddressId]) REFERENCES [Core].[Address] ([AddressId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Job_JobType_JobTypeId] FOREIGN KEY ([JobTypeId]) REFERENCES [Job].[JobType] ([JobTypeId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Job_Address_StartAddressId] FOREIGN KEY ([StartAddressId]) REFERENCES [Core].[Address] ([AddressId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Vehicle].[VehicleDocument] (
    [Id] int NOT NULL IDENTITY,
    [VehicleId] int NOT NULL,
    [DocumentId] int NOT NULL,
    CONSTRAINT [PK_VehicleDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_VehicleDocument_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Core].[Document] ([DocumentId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_VehicleDocument_Vehicle_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicle].[Vehicle] ([VehicleId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Job].[Account] (
    [AccountId] int NOT NULL IDENTITY,
    [JobId] int NOT NULL,
    [AccountTypeId] int NOT NULL,
    [CurrencyId] int NOT NULL,
    [Debit] decimal(18, 2) NOT NULL,
    [Credit] decimal(18, 2) NOT NULL,
    [Date] datetime2 NOT NULL,
    [Note] nvarchar(max) NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY ([AccountId]),
    CONSTRAINT [FK_Account_AccountType_AccountTypeId] FOREIGN KEY ([AccountTypeId]) REFERENCES [Job].[AccountType] ([AccountTypeId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Account_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Core].[Currency] ([CurrencyId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Account_Job_JobId] FOREIGN KEY ([JobId]) REFERENCES [Job].[Job] ([JobId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Job].[Freight] (
    [FreightId] int NOT NULL IDENTITY,
    [JobId] int NOT NULL,
    [Title] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Note] nvarchar(max) NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Freight] PRIMARY KEY ([FreightId]),
    CONSTRAINT [FK_Freight_Job_JobId] FOREIGN KEY ([JobId]) REFERENCES [Job].[Job] ([JobId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Staff].[StaffDocument] (
    [Id] int NOT NULL IDENTITY,
    [StaffId] int NOT NULL,
    [DocumentId] int NOT NULL,
    CONSTRAINT [PK_StaffDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StaffDocument_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Core].[Document] ([DocumentId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Staff].[Staff] (
    [StaffId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [DepartmentId] int NOT NULL,
    [NationalityId] int NOT NULL,
    [IdentityNo] nvarchar(max) NULL,
    [DocumentNo] nvarchar(max) NULL,
    [ForeignLangs] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [Birthday] datetime2 NOT NULL,
    [DriversLicenseNo] nvarchar(max) NULL,
    [DriversLicenseTypeId] int NOT NULL,
    [Phone1] nvarchar(max) NULL,
    [Phone2] nvarchar(max) NULL,
    [Phone3] nvarchar(max) NULL,
    [Phone4] nvarchar(max) NULL,
    [Email1] nvarchar(max) NULL,
    [Email2] nvarchar(max) NULL,
    [UserRoleId] int NULL,
    [CreatedBy] int NULL,
    [UpdatedBy] int NULL,
    [DeletedBy] int NULL,
    [CreatedOn] datetime2 NULL,
    [UpdatedOn] datetime2 NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Staff] PRIMARY KEY ([StaffId]),
    CONSTRAINT [FK_Staff_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Staff].[Department] ([DepartmentId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Staff_DriversLicenseType_DriversLicenseTypeId] FOREIGN KEY ([DriversLicenseTypeId]) REFERENCES [Staff].[DriversLicenseType] ([DriversLicenseTypeId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Staff_Nationality_NationalityId] FOREIGN KEY ([NationalityId]) REFERENCES [Core].[Nationality] ([NationalityId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Core].[User] (
    [UserId] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [StaffId] int NULL,
    [IsActive] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL DEFAULT 0,
    [LockoutEndDate] datetime2 NULL,
    [AccessFailedCount] int NOT NULL DEFAULT 0,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_User_Staff_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [Staff].[Staff] ([StaffId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Job].[JobStaff] (
    [JobStaffId] int NOT NULL IDENTITY,
    [JobId] int NOT NULL,
    [StaffId] int NOT NULL,
    [VehicleId] int NOT NULL,
    [StartFuelLevel] int NOT NULL,
    [FinishFuelLevel] int NULL,
    [StartVehicleMileage] decimal(18, 2) NOT NULL,
    [FinishVehicleMileage] decimal(18, 2) NULL,
    CONSTRAINT [PK_JobStaff] PRIMARY KEY ([JobStaffId]),
    CONSTRAINT [FK_JobStaff_Job_JobId] FOREIGN KEY ([JobId]) REFERENCES [Job].[Job] ([JobId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_JobStaff_Staff_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [Staff].[Staff] ([StaffId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_JobStaff_Vehicle_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicle].[Vehicle] ([VehicleId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Core].[Role] (
    [RoleId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleId]),
    CONSTRAINT [FK_Role_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Core].[User] ([UserId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Core].[UserRole] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Core].[Role] ([RoleId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [Core].[User] ([UserId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Address_AddressTypeId] ON [Core].[Address] ([AddressTypeId]);

GO

CREATE INDEX [IX_City_CountryId] ON [Core].[City] ([CountryId]);

GO

CREATE INDEX [IX_Document_CurrencyId] ON [Core].[Document] ([CurrencyId]);

GO

CREATE INDEX [IX_Document_DocumentTypeId] ON [Core].[Document] ([DocumentTypeId]);

GO

CREATE INDEX [IX_Role_UserId] ON [Core].[Role] ([UserId]);

GO

CREATE INDEX [IX_User_StaffId] ON [Core].[User] ([StaffId]);

GO

CREATE INDEX [IX_UserRole_RoleId] ON [Core].[UserRole] ([RoleId]);

GO

CREATE INDEX [IX_UserRole_UserId] ON [Core].[UserRole] ([UserId]);

GO

CREATE INDEX [IX_Customer_GroupId] ON [Customer].[Customer] ([GroupId]);

GO

CREATE INDEX [IX_CustomerAddress_AddressId] ON [Customer].[CustomerAddress] ([AddressId]);

GO

CREATE INDEX [IX_CustomerAddress_CustomerId] ON [Customer].[CustomerAddress] ([CustomerId]);

GO

CREATE INDEX [IX_CustomerContact_ContactId] ON [Customer].[CustomerContact] ([ContactId]);

GO

CREATE INDEX [IX_CustomerContact_CustomerId] ON [Customer].[CustomerContact] ([CustomerId]);

GO

CREATE INDEX [IX_CustomerDocument_CustomerId] ON [Customer].[CustomerDocument] ([CustomerId]);

GO

CREATE INDEX [IX_CustomerDocument_DocumentId] ON [Customer].[CustomerDocument] ([DocumentId]);

GO

CREATE INDEX [IX_Account_AccountTypeId] ON [Job].[Account] ([AccountTypeId]);

GO

CREATE INDEX [IX_Account_CurrencyId] ON [Job].[Account] ([CurrencyId]);

GO

CREATE INDEX [IX_Account_JobId] ON [Job].[Account] ([JobId]);

GO

CREATE INDEX [IX_Job_CustomerId] ON [Job].[Job] ([CustomerId]);

GO

CREATE INDEX [IX_Job_FinishAddressId] ON [Job].[Job] ([FinishAddressId]);

GO

CREATE INDEX [IX_Job_JobTypeId] ON [Job].[Job] ([JobTypeId]);

GO

CREATE INDEX [IX_Job_StartAddressId] ON [Job].[Job] ([StartAddressId]);

GO

CREATE INDEX [IX_JobStaff_JobId] ON [Job].[JobStaff] ([JobId]);

GO

CREATE INDEX [IX_JobStaff_StaffId] ON [Job].[JobStaff] ([StaffId]);

GO

CREATE INDEX [IX_JobStaff_VehicleId] ON [Job].[JobStaff] ([VehicleId]);

GO

CREATE INDEX [IX_Freight_JobId] ON [Job].[Freight] ([JobId]);

GO

CREATE INDEX [IX_Staff_DepartmentId] ON [Staff].[Staff] ([DepartmentId]);

GO

CREATE INDEX [IX_Staff_DriversLicenseTypeId] ON [Staff].[Staff] ([DriversLicenseTypeId]);

GO

CREATE INDEX [IX_Staff_NationalityId] ON [Staff].[Staff] ([NationalityId]);

GO

CREATE INDEX [IX_Staff_UserRoleId] ON [Staff].[Staff] ([UserRoleId]);

GO

CREATE INDEX [IX_StaffDocument_DocumentId] ON [Staff].[StaffDocument] ([DocumentId]);

GO

CREATE INDEX [IX_StaffDocument_StaffId] ON [Staff].[StaffDocument] ([StaffId]);

GO

CREATE INDEX [IX_Vehicle_ColorId] ON [Vehicle].[Vehicle] ([ColorId]);

GO

CREATE INDEX [IX_Vehicle_FuelId] ON [Vehicle].[Vehicle] ([FuelId]);

GO

CREATE INDEX [IX_Vehicle_MakeId] ON [Vehicle].[Vehicle] ([MakeId]);

GO

CREATE INDEX [IX_Vehicle_TransmissionId] ON [Vehicle].[Vehicle] ([TransmissionId]);

GO

CREATE INDEX [IX_Vehicle_VehicleTypeId] ON [Vehicle].[Vehicle] ([VehicleTypeId]);

GO

CREATE INDEX [IX_VehicleDocument_DocumentId] ON [Vehicle].[VehicleDocument] ([DocumentId]);

GO

CREATE INDEX [IX_VehicleDocument_VehicleId] ON [Vehicle].[VehicleDocument] ([VehicleId]);

GO

ALTER TABLE [Staff].[StaffDocument] ADD CONSTRAINT [FK_StaffDocument_Staff_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [Staff].[Staff] ([StaffId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Staff].[Staff] ADD CONSTRAINT [FK_Staff_UserRole_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [Core].[UserRole] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [Core].[Migration] ([MigrationId], [ProductVersion])
VALUES (N'20180904150530_Initial', N'2.1.3-rtm-32065');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Job].[WiseTransfer]') AND [c].[name] = N'CheckInDeleted');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Job].[WiseTransfer] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Job].[WiseTransfer] DROP COLUMN [CheckInDeleted];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Job].[WiseTransfer]') AND [c].[name] = N'CheckOutDeleted');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Job].[WiseTransfer] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Job].[WiseTransfer] DROP COLUMN [CheckOutDeleted];

GO

ALTER TABLE [Job].[Job] ADD [IsCancelled] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [Core].[Migration] ([MigrationId], [ProductVersion])
VALUES (N'20180904165722_WiseTransferCancel', N'2.1.3-rtm-32065');

GO

ALTER TABLE [Job].[WiseTransfer] ADD [AirportFrom] nvarchar(max) NULL;

GO

ALTER TABLE [Job].[WiseTransfer] ADD [AirportTo] nvarchar(max) NULL;

GO

ALTER TABLE [Core].[Currency] ADD [IsDeleted] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [Core].[Migration] ([MigrationId], [ProductVersion])
VALUES (N'20181009121513_AirportField', N'2.1.3-rtm-32065');

GO

