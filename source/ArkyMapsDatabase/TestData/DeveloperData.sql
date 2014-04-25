/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/
    CREATE PROCEDURE #addPhoneUser
    (
        @name varchar(50)
        ,@password varchar(50)
    )
    AS
    BEGIN
        INSERT INTO PhoneUser
        (
            Name
            ,Password
        )
        VALUES
        (
            @name
            ,@password
        )
    END
    GO

    -- client users
    INSERT INTO ClientUser
    (
        Name
        ,Password
    )
    VALUES
    (
        N'test'
        ,N'test'
    )


    -- phone users
    DECLARE @counter int = 1;
    DECLARE @baseName varchar(50) = 'test'
    DECLARE @name varchar(50)

    WHILE @counter < 6
    BEGIN
        SET @name = @baseName + cast(@counter as char(1))

        EXEC #addPhoneUser
            @name
            ,@name

        SET @counter = @counter + 1
    END
