CREATE TABLE [dbo].[Position]
(
    [ID] BIGINT IDENTITY NOT NULL CONSTRAINT PK_Position PRIMARY KEY,
    [PhoneUserId] BIGINT NOT NULL CONSTRAINT FK_Position_PhoneUser REFERENCES PhoneUser (ID),
    [Longitude] FLOAT NOT NULL,
    [Latitude] FLOAT NOT NULL,
    [Timestamp] DATETIME2 NOT NULL
)
