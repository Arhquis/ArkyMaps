CREATE TABLE [dbo].[Location]
(
    [ID] BIGINT IDENTITY NOT NULL CONSTRAINT PK_Location PRIMARY KEY,
    [PhoneUserId] BIGINT NOT NULL CONSTRAINT FK_Location_PhoneUser REFERENCES PhoneUser (ID),
    [Longitude] REAL NOT NULL,
    [Latitude] REAL NOT NULL
)
