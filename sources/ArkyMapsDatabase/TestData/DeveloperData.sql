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
        @userName varchar(50)
        ,@password varchar(50)
        ,@name varchar(100)
        ,@male bit
        ,@email varchar(50)
    )
    AS
    BEGIN
        INSERT INTO PhoneUser
        (
            UserName
            ,Password
            ,Name
            ,Male
            ,Email
            ,Deleted
        )
        VALUES
        (
            @userName
            ,@password
            ,@name
            ,@male
            ,@email
            ,'false'
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

    -- test1 user
    EXEC #addPhoneUser
        N'test1'
        ,N'test1'
        ,N'Kiss Tibor'
        ,'true'
        ,N'tibor.kiss@gmail.com'

    -- test2 user
    EXEC #addPhoneUser
        N'test2'
        ,N'test2'
        ,N'Hóbort Géza'
        ,'true'
        ,N'hobi@gmail.com'


    -- test3 user
    EXEC #addPhoneUser
        N'test3'
        ,N'test3'
        ,N'Bársony Melinda'
        ,'false'
        ,N'melcsi@hotmail.com'


    -- test4 user
    EXEC #addPhoneUser
        N'test4'
        ,N'test4'
        ,N'Dörmögő Dömötör'
        ,'true'
        ,N'donci@live.com'


    -- test5 user
    EXEC #addPhoneUser
        N'test5'
        ,N'test5'
        ,N'Szűcs Kinga'
        ,'false'
        ,N'kinga.szucs.1@gmail.com'
GO
