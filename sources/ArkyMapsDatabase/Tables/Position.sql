CREATE TABLE [dbo].[Position]
(
    [ID] BIGINT IDENTITY NOT NULL CONSTRAINT PK_Position PRIMARY KEY,
    [PhoneUserId] BIGINT NOT NULL CONSTRAINT FK_Position_PhoneUser REFERENCES PhoneUser (ID),
    [Longitude] REAL NOT NULL,
    [Latitude] REAL NOT NULL
)
